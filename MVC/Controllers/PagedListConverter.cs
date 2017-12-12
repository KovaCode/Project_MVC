using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Controllers
{
        public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>> where TSource : class where TDestination : class
        {
            public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
            {
                var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
                return new PagedList<TDestination>(collection, source.PageNumber, source.PageSize);
            }
        }
    
}