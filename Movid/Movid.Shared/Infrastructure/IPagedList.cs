using System.Collections;
using System.Collections.Generic;

namespace Movid.Shared.Infrastructure
{
    public interface IPagedList<T> : IPagedList, IList<T>
    {
    }

    public interface IPagedList : IEnumerable
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}