using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BaseArchitecture.Application
{
    public class Pagination<Entity> : List<Entity>
    {

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalR { get; private set; }
        public Pagination(List<Entity> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalR = count;
            this.AddRange(items);
        }

        public Pagination(List<Entity> items)
        {
            this.AddRange(items);
        }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }


        public static async Task<Pagination<Entity>> CreateAsync(IQueryable<Entity> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new Pagination<Entity>(items, count, pageIndex, pageSize);
        }

        public static Pagination<Entity> Create(IQueryable<Entity> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new Pagination<Entity>(items, count, pageIndex, pageSize);
        }



        public static async Task<Pagination<Entity>> CreateCustomAsync(IQueryable<Entity> source, int desde, int cantidad, CancellationToken cancellationToken = default)
        {
            //var count = await source.CountAsync();
            var items = await source.Skip(desde).Take(cantidad).ToListAsync(cancellationToken);
            return new Pagination<Entity>(items);
        }

        /// <summary>
        /// Paginación perzonalizada
        /// </summary>
        /// <param name="source">query que contiene los filtros.</param>
        /// <param name="desde">indice de registro desde comenzará la paginación.</param>
        /// <param name="cantidad">Cantidad de registros que traera la consulta.</param>
        /// <returns></returns>
        public static Pagination<Entity> CreateCustom(IQueryable<Entity> source, int desde, int cantidad)
        {
            //var count = source.Count();
            var items = source.Skip(desde).Take(cantidad).ToList();
            return new Pagination<Entity>(items);
        }

    }

}

