using InsightWorks.DTOs.EquipmentSync;
using InsightWorks.DTOs.Common;
using InsightWorks.Models;

namespace InsightWorks.Services;

public interface IEquipmentSyncService
{
    /// <summary>
    /// 发送同步命令给设备
    /// </summary>
    Task SendSyncCommandAsync(SyncCommandDTO command);

    /// <summary>
    /// 上传机况数据
    /// </summary>
    Task UploadStatusDataAsync(UploadStatusDataDTO data);

    /// <summary>
    /// 上传生产数据
    /// </summary>
    Task UploadProductionDataAsync(UploadProductionDataDTO data);

    /// <summary>
    /// 上传 Excel 数据
    /// </summary>
    Task UploadExcelDataAsync(UploadExcelDTO data);

    /// <summary>
    /// 分页查询同步记录
    /// </summary>
    Task<PaginatedList<EquipmentSyncRecord>> QuerySyncRecordsAsync(SyncRecordQueryDTO query);
} 