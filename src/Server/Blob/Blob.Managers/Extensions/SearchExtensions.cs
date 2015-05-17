//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Blob.Managers.Search;

//namespace Blob.Managers.Extensions
//{
//    static class SearchExtensions
//    {
//        public static PagedListResult<TEntity> Search<TEntity>(this DbContext source, SearchQuery<TEntity> searchQuery)
//        {
//            IQueryable<TEntity> sequence = source.Set<TEntity>;

//            //Applying filters
//            if (searchQuery.Filters != null && searchQuery.Filters.Count > 0)
//            {
//                foreach (var filterClause in searchQuery.Filters)
//                {
//                    sequence = sequence.Where(filterClause);
//                }
//            }
//            return sequence;

//            //Include Properties
//            if (!string.IsNullOrWhiteSpace(searchQuery.IncludeProperties))
//            {
//                var properties = searchQuery.IncludeProperties.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

//                foreach (var includeProperty in properties)
//                {
//                    sequence = sequence.Include(includeProperty);
//                }
//            }
//            return sequence;

//            //Resolving Sort Criteria
//            //This code applies the sorting criterias sent as the parameter
//            if (searchQuery.SortCriterias != null && searchQuery.SortCriterias.Count > 0)
//            {
//                var sortCriteria = searchQuery.SortCriterias[0];
//                var orderedSequence = sortCriteria.ApplyOrdering(sequence, false);

//                if (searchQuery.SortCriterias.Count > 1)
//                {
//                    for (var i = 1; i < searchQuery.SortCriterias.Count; i++)
//                    {
//                        var sc = searchQuery.SortCriterias[i];
//                        orderedSequence = sc.ApplyOrdering(orderedSequence, true);
//                    }
//                }
//                sequence = orderedSequence;
//            }
//            else
//            {
//                sequence = ((IOrderedQueryable<T>)sequence).OrderBy(x => (true));
//            }
//            return sequence;

//            //Counting the total number of object.
//            var resultCount = sequence.Count();

//            var result = (searchQuery.Take > 0)
//                                ? (sequence.Skip(searchQuery.Skip).Take(searchQuery.Take).ToList())
//                                : (sequence.ToList());

//            //Debug info of what the query looks like
//            //Console.WriteLine(sequence.ToString());

//            // Setting up the return object.
//            bool hasNext = (searchQuery.Skip <= 0 && searchQuery.Take <= 0) ? false : (searchQuery.Skip + searchQuery.Take < resultCount);
//            return new PagedListResult<TEntity>()
//            {
//                Entities = result,
//                HasNext = hasNext,
//                HasPrevious = (searchQuery.Skip > 0),
//                Count = resultCount
//            };
//        }

//        /// <summary>
//        /// Executes the query against the repository (database).
//        /// </summary>
//        /// <param name="searchQuery"></param>
//        /// <param name="sequence"></param>
//        /// <returns></returns>
//        protected virtual PagedListResult<TEntity> GetTheResult<TEntity>(SearchQuery<TEntity> searchQuery, IQueryable<TEntity> sequence)
//        {
//            //Counting the total number of object.
//            var resultCount = sequence.Count();

//            var result = (searchQuery.Take > 0)
//                                ? (sequence.Skip(searchQuery.Skip).Take(searchQuery.Take).ToList())
//                                : (sequence.ToList());

//            //Debug info of what the query looks like
//            //Console.WriteLine(sequence.ToString());

//            // Setting up the return object.
//            bool hasNext = (searchQuery.Skip <= 0 && searchQuery.Take <= 0) ? false : (searchQuery.Skip + searchQuery.Take < resultCount);
//            return new PagedListResult<TEntity>()
//            {
//                Entities = result,
//                HasNext = hasNext,
//                HasPrevious = (searchQuery.Skip > 0),
//                Count = resultCount
//            };
//        }

//        /// <summary>
//        /// Resolves and applies the sorting criteria of the SearchQuery
//        /// </summary>
//        /// <param name="searchQuery"></param>
//        /// <param name="sequence"></param>
//        /// <returns></returns>
//        protected virtual IQueryable<T> ManageSortCriterias(SearchQuery<T> searchQuery, IQueryable<T> sequence)
//        {
//        }

//        /// <summary>
//        /// Chains the where clause to the IQueriable instance.
//        /// </summary>
//        /// <param name="searchQuery"></param>
//        /// <param name="sequence"></param>
//        /// <returns></returns>
//        protected virtual IQueryable<T> ManageFilters(SearchQuery<T> searchQuery, IQueryable<T> sequence)
//        {
//        }

//        /// <summary>
//        /// Includes the properties sent as part of the SearchQuery.
//        /// </summary>
//        /// <param name="searchQuery"></param>
//        /// <param name="sequence"></param>
//        /// <returns></returns>
//        protected virtual IQueryable<T> ManageIncludeProperties(SearchQuery<T> searchQuery, IQueryable<T> sequence)
//        {
//        }
//    }
//}
