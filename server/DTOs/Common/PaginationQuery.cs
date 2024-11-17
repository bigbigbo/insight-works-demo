namespace InsightWorks.DTOs.Common;

public class PaginationQuery
{
    private int _pageSize = 10;
    private int _pageIndex = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 100 ? 100 : value;  // 限制每页最大数量为100
    }

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value < 1 ? 1 : value;
    }
} 