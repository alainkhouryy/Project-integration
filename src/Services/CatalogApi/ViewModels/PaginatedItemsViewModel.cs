using System;
using System.Collections.Generic;

namespace CatalogApi.ViewModels
{
    public class PaginatedItemsViewModel<TEntity> where TEntity:class
    {
        public int pageSize { get; private set; }
        public int pageIndex { get; private set; }
        public long count { get; private set; }
        public IEnumerable<TEntity> data { get; set; }

        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.count = count;
            this.data = data;
        }
    }
}
