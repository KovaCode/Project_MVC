using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Controllers
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            return new PagedList<TDestination>(collection, source.PageNumber, source.PageSize);
        }
    }
}