using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PageList<T> : List<T>
{
    /// <summary>
    /// 当前页
    /// </summary>
    public int CurrentPage { get; private set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int TotalPages { get; private set; }

    /// <summary>
    /// 分页大小
    /// </summary>
    public int PageSize { get; private set; }

    /// <summary>
    /// 总行数
    /// </summary>
    public int TotalCount { get; private set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PageList(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);
        AddRange(items);
    }

    public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var totalCount = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        var list = new PageList<T>(items, totalCount, pageNumber, pageSize);
        return await Task.FromResult(list);
    }
}
