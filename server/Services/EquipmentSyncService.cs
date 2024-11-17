using InsightWorks.DTOs.EquipmentSync;
using InsightWorks.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;
using InsightWorks.Models.Enums;
using InsightWorks.DTOs.Common;

namespace InsightWorks.Services;

public class EquipmentSyncService : IEquipmentSyncService
{
    private readonly AppDbContext _context;
    private readonly ILogger<EquipmentSyncService> _logger;

    public EquipmentSyncService(AppDbContext context, ILogger<EquipmentSyncService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SendSyncCommandAsync(SyncCommandDTO command)
    {
        if (!command.IsValid())
        {
            throw new ArgumentException("无效的同步命令参数");
        }

        // 验证设备是否存在
        var equipment = await _context.Equipment
            .Include(e => e.Manufacturer)
            .FirstOrDefaultAsync(e => e.Id == command.EquipmentId)
            ?? throw new KeyNotFoundException($"未找到ID为 {command.EquipmentId} 的设备");

        // 创建同步记录
        var syncRecord = new EquipmentSyncRecord
        {
            EquipmentId = command.EquipmentId,
            SyncType = command.SyncType,
            SyncStartTime = DateTime.UtcNow,
            Status = SyncStatus.Failed
        };

        _context.EquipmentSyncRecords.Add(syncRecord);
        await _context.SaveChangesAsync();

        try
        {
            // TODO: 实与设备的实际通信逻辑
            // 这里需要根据实际的设备通信协议来实现
            
            _logger.LogInformation(
                "向设备 {EquipmentCode}（厂商：{Manufacturer}）发送同步命令: 类型 {SyncType}",
                equipment.EquipmentCode,
                equipment.Manufacturer.Name,
                command.SyncType
            );

            // 更新同步记录状态为成功
            syncRecord.Status = SyncStatus.Success;
            syncRecord.SyncEndTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // 更新同步记录的错误信息
            syncRecord.ErrorMessage = ex.Message;
            syncRecord.SyncEndTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            throw;
        }
    }

    public async Task UploadStatusDataAsync(UploadStatusDataDTO data)
    {
        // 验证设备是否存在
        var equipment = await _context.Equipment
            .Include(e => e.Manufacturer)
            .FirstOrDefaultAsync(e => e.Id == data.EquipmentId)
            ?? throw new KeyNotFoundException($"未找到ID为 {data.EquipmentId} 的设备");

        if (data.StatusList == null || !data.StatusList.Any())
        {
            throw new ArgumentException("机况数据列表不能为空");
        }

        // 验证状态数据
        foreach (var status in data.StatusList)
        {
            if (string.IsNullOrWhiteSpace(status.Status) || 
                !Enum.TryParse<EquipmentStatus>(status.Status, true, out _))
            {
                throw new ArgumentException("无效的设备状态");
            }
        }

        // 批量插入状态记录
        var statusHistories = data.StatusList.Select(s => new EquipmentStatusHistory
        {
            EquipmentId = data.EquipmentId,
            Status = Enum.Parse<EquipmentStatus>(s.Status, true),
            StatusChangeTime = s.StatusChangeTime,
            ExecutedBy = s.ExecutedBy
        }).ToList();

        await _context.EquipmentStatusHistories.AddRangeAsync(statusHistories);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "设备 {EquipmentCode}（厂商：{Manufacturer}）上传了 {Count} 条机况数据",
            equipment.EquipmentCode,
            equipment.Manufacturer.Name,
            statusHistories.Count
        );
    }

    public async Task UploadProductionDataAsync(UploadProductionDataDTO data)
    {
        // 验证设备是否存在
        var equipment = await _context.Equipment
            .Include(e => e.Manufacturer)
            .FirstOrDefaultAsync(e => e.Id == data.EquipmentId)
            ?? throw new KeyNotFoundException($"未找到ID为 {data.EquipmentId} 的设备");

        if (data.ProductionList == null || !data.ProductionList.Any())
        {
            throw new ArgumentException("生产数据列表不能为空");
        }

        // 验证产品型号是否存在
        var productModelIds = data.ProductionList.Select(p => p.ProductModelId).Distinct();
        var existingModels = await _context.ProductModels
            .Where(m => productModelIds.Contains(m.Id))
            .ToDictionaryAsync(m => m.Id);

        foreach (var modelId in productModelIds)
        {
            if (!existingModels.ContainsKey(modelId))
            {
                throw new KeyNotFoundException($"未找到ID为 {modelId} 的产品型号");
            }
        }

        // 批量插入生产记录
        var productionRecords = data.ProductionList.Select(p => new ProductionRecord
        {
            EquipmentId = data.EquipmentId,
            ProductModelId = p.ProductModelId,
            BatchNumber = p.BatchNumber,
            PreLength = p.PreLength,
            PreWidth = p.PreWidth,
            PreHeight = p.PreHeight,
            PostLength = p.PostLength,
            PostWidth = p.PostWidth,
            PostHeight = p.PostHeight,
            ProductionStartTime = p.ProductionStartTime,
            ProductionEndTime = p.ProductionEndTime
        }).ToList();

        await _context.ProductionRecords.AddRangeAsync(productionRecords);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "设备 {EquipmentCode}（厂商：{Manufacturer}）上传了 {Count} 条生产数据",
            equipment.EquipmentCode,
            equipment.Manufacturer.Name,
            productionRecords.Count
        );
    }

    public async Task UploadExcelDataAsync(UploadExcelDTO data)
    {
        // 验证设备是否存在
        var equipment = await _context.Equipment
            .Include(e => e.Manufacturer)
            .FirstOrDefaultAsync(e => e.Id == data.EquipmentId)
            ?? throw new KeyNotFoundException($"未找到ID为 {data.EquipmentId} 的设备");

        // 验证文件
        if (data.File == null || data.File.Length == 0)
        {
            throw new ArgumentException("文件不能为空");
        }

        // 验证文件扩展名
        var extension = Path.GetExtension(data.File.FileName).ToLowerInvariant();
        if (extension != ".xlsx")
        {
            throw new ArgumentException("只支持.xlsx格式的Excel文件");
        }

        // 创建同步记录
        var syncRecord = new EquipmentSyncRecord
        {
            EquipmentId = data.EquipmentId,
            SyncType = data.SyncType,
            SyncStartTime = DateTime.UtcNow,
            Status = SyncStatus.Failed
        };

        _context.EquipmentSyncRecords.Add(syncRecord);
        await _context.SaveChangesAsync();

        try
        {
            using var stream = new MemoryStream();
            await data.File.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);
            
            // 获取第一个工作表
            var worksheet = package.Workbook.Worksheets[0];
            if (worksheet == null || worksheet.Dimension == null)
            {
                throw new ArgumentException("Excel文件为空");
            }

            var rowCount = worksheet.Dimension.Rows;
            if (rowCount <= 1)  // 只有标题行
            {
                throw new ArgumentException("Excel文件有数据");
            }

            if (data.SyncType == SyncType.Status)
            {
                await ProcessStatusExcel(worksheet, data.EquipmentId);
            }
            else if (data.SyncType == SyncType.Production)
            {
                await ProcessProductionExcel(worksheet, data.EquipmentId);
            }
            else
            {
                throw new ArgumentException("无效的同步类型");
            }

            // 更新同步记录状态为成功
            syncRecord.Status = SyncStatus.Success;
            syncRecord.SyncEndTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "设备 {EquipmentCode}（厂商：{Manufacturer}）成功导入Excel数据，类型：{SyncType}",
                equipment.EquipmentCode,
                equipment.Manufacturer.Name,
                data.SyncType
            );
        }
        catch (Exception ex)
        {
            // 更新同步记录的错误信息
            syncRecord.ErrorMessage = ex.Message;
            syncRecord.SyncEndTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            throw;
        }
    }

    private async Task ProcessStatusExcel(ExcelWorksheet worksheet, Guid equipmentId)
    {
        var statusList = new List<EquipmentStatusHistory>();
        var rowCount = worksheet.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++)
        {
            var statusText = worksheet.Cells[row, 1].Text;
            if (string.IsNullOrWhiteSpace(statusText))
            {
                continue;
            }

            if (!Enum.TryParse<EquipmentStatus>(statusText, true, out EquipmentStatus status))
            {
                throw new ArgumentException($"第{row}行的设备状态无效");
            }

            if (!DateTime.TryParse(worksheet.Cells[row, 2].Value?.ToString(), out DateTime statusChangeTime))
            {
                // 尝试使用Excel的日期时间值
                if (worksheet.Cells[row, 2].Value is double excelDate)
                {
                    try 
                    {
                        statusChangeTime = DateTime.FromOADate(excelDate);
                    }
                    catch
                    {
                        throw new ArgumentException($"第{row}行的状态变更时间格式无效，请使用标准日期时间格式（如：2024-03-21 14:30:00）");
                    }
                }
                else 
                {
                    throw new ArgumentException($"第{row}行的状态变更时间格式无效，请使用标准日期时间格式（如：2024-03-21 14:30:00）");
                }
            }

            statusList.Add(new EquipmentStatusHistory
            {
                EquipmentId = equipmentId,
                Status = status,
                StatusChangeTime = statusChangeTime,
                ExecutedBy = worksheet.Cells[row, 3].Text
            });
        }

        if (!statusList.Any())
        {
            throw new ArgumentException("Excel文件中没有有效的机况数据");
        }

        await _context.EquipmentStatusHistories.AddRangeAsync(statusList);
        await _context.SaveChangesAsync();
    }

    private async Task ProcessProductionExcel(ExcelWorksheet worksheet, Guid equipmentId)
    {
        var productionList = new List<ProductionRecord>();
        var rowCount = worksheet.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++)  // 从第2行开始（跳过标题行）
        {
            var modelCode = worksheet.Cells[row, 1].Text;
            if (string.IsNullOrWhiteSpace(modelCode))
            {
                continue;
            }

            // 查找产品型号
            var productModel = await _context.ProductModels
                .FirstOrDefaultAsync(m => m.ModelCode == modelCode);
            if (productModel == null)
            {
                throw new ArgumentException($"第{row}行的产品型号不存在：{modelCode}");
            }

            if (!DateTime.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out DateTime startTime))
            {
                // 尝试使用Excel的日期时间值
                if (worksheet.Cells[row, 2].Value is double excelDate)
                {
                    try 
                    {
                        startTime = DateTime.FromOADate(excelDate);
                    }
                    catch
                    {
                        throw new ArgumentException($"第{row}行的生产开始时间格式无效，请使用标准日期时间格式（如：2024-03-21 14:30:00）");
                    }
                }
                else 
                {
                    throw new ArgumentException($"第{row}行的生产开始时间格式无效，请使用标准日期��间格式（如：2024-03-21 14:30:00）");
                }
            }

            if (!DateTime.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out DateTime endTime))
            {
                // 尝试使用Excel的日期时间值
                if (worksheet.Cells[row, 2].Value is double excelDate)
                {
                    try 
                    {
                        endTime = DateTime.FromOADate(excelDate);
                    }
                    catch
                    {
                        throw new ArgumentException($"第{row}行的生产结束时间格式无效，请使用标准日期时间格式（如：2024-03-21 14:30:00）");
                    }
                }
                else 
                {
                    throw new ArgumentException($"第{row}行的生产结束时间格式无效，请使用标准日期时间格式（如：2024-03-21 14:30:00）");
                }
            }

            productionList.Add(new ProductionRecord
            {
                EquipmentId = equipmentId,
                ProductModelId = productModel.Id,
                BatchNumber = worksheet.Cells[row, 2].Text,
                PreLength = worksheet.Cells[row, 5].Text,
                PreWidth = worksheet.Cells[row, 6].Text,
                PreHeight = worksheet.Cells[row, 7].Text,
                PostLength = worksheet.Cells[row, 8].Text,
                PostWidth = worksheet.Cells[row, 9].Text,
                PostHeight = worksheet.Cells[row, 10].Text,
                ProductionStartTime = startTime,
                ProductionEndTime = endTime
            });
        }

        if (!productionList.Any())
        {
            throw new ArgumentException("Excel文件中没有有效的生产数据");
        }

        await _context.ProductionRecords.AddRangeAsync(productionList);
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedList<EquipmentSyncRecord>> QuerySyncRecordsAsync(SyncRecordQueryDTO query)
    {
        var queryable = _context.EquipmentSyncRecords
            .Include(r => r.Equipment)
                .ThenInclude(e => e.Manufacturer)
            .AsQueryable();

        // 应用查询条件
        if (query.EquipmentId.HasValue)
        {
            queryable = queryable.Where(r => r.EquipmentId == query.EquipmentId);
        }

        if (query.SyncType.HasValue)
        {
            queryable = queryable.Where(r => r.SyncType == query.SyncType);
        }

        if (query.Status.HasValue)
        {
            queryable = queryable.Where(r => r.Status == query.Status);
        }

        if (query.StartTime.HasValue)
        {
            queryable = queryable.Where(r => r.SyncStartTime >= query.StartTime);
        }

        if (query.EndTime.HasValue)
        {
            queryable = queryable.Where(r => r.SyncStartTime <= query.EndTime);
        }

        // 按时间倒序排序
        queryable = queryable.OrderByDescending(r => r.SyncStartTime);

        // 执行分页查询
        var records = await queryable
            .Skip((query.PageIndex - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        // 获取总记录数
        var totalCount = await queryable.CountAsync();

        return new PaginatedList<EquipmentSyncRecord>(records, totalCount, query.PageIndex, query.PageSize);
    }
} 