using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Witchblades.Backend.Api.Utils.Attributes;

namespace Witchblades.Backend.Api.Utils
{
    public class PaginationParameters
    {
        [Range(1, 100, ErrorMessage = "The limit should be in the range from {1} to {2}")]
        public int Limit { get; set; } = 10;
        [IsPositiveNumber(ErrorMessage = "The page number must be positive")]
        public int PageNumber { get; set; } = 1;
    }

    public class PagedModel<T>
    {
        public int PageNumber { get; init; }
        public int TotalPages { get; init; }
        public int NextPageNumber { get; init; }
        public int PageElemensCount { get; init; }
        public int Limit { get; init; }
        public IEnumerable<T> Elements { get; init; }
    }

    public interface IPagedModelFactory
    {
        public Task<PagedModel<ViewModelType>> CreatePagedModelAsync<DomainModelType, ViewModelType>(
            PaginationParameters options,
            IOrderedQueryable<DomainModelType> elementsQuery);
    }

    public class PagedModelFactory : IPagedModelFactory
    {
        private readonly IMapper _mapper;

        public PagedModelFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<PagedModel<ViewModelType>> CreatePagedModelAsync<DomainModelType, ViewModelType>(
            PaginationParameters options,
            IOrderedQueryable<DomainModelType> elementsQuery)
        {
            if (elementsQuery is null)
            {
                throw new ArgumentNullException("IOrderedQueryable is null");
            }

            if (options is null)
            {
                throw new ArgumentNullException("PaginationParameters is null");
            }

            if (options.Limit < 1)
            {
                throw new ArgumentException("Limit can't be less then one");
            }

            int total = elementsQuery.Count();

            var elements = await elementsQuery
                .Skip(options.Limit * (options.PageNumber - 1))
                .Take(options.Limit)
                .Select(t => _mapper.Map<ViewModelType>(t))
                .ToArrayAsync();

            var model = new PagedModel<ViewModelType>()
            {
                Limit = options.Limit,
                NextPageNumber = options.PageNumber + 1,
                Elements = elements,
                PageNumber = options.PageNumber,
                TotalPages = total / options.Limit,
                PageElemensCount = elements.Count()
            };

            return model;
        }
    }
}
