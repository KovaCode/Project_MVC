using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Controllers
{
    public class Converter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, PagedViewModel<TDestination>>
    {
        public PagedViewModel<TDestination> Convert(IPagedList<TSource> source, PagedViewModel<TDestination> destination, ResolutionContext context)
        {
            return new PagedViewModel<TDestination>()
            {
                FirstItemOnPage = source.FirstItemOnPage,
                HasNextPage = source.HasNextPage,
                HasPreviousPage = source.HasPreviousPage,
                IsFirstPage = source.IsFirstPage,
                IsLastPage = source.IsLastPage,
                LastItemOnPage = source.LastItemOnPage,
                PageCount = source.PageCount,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                TotalItemCount = source.TotalItemCount,
                Subset = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source) //User mapper to go from "foo" to "bar"
            };
        }
    }
}