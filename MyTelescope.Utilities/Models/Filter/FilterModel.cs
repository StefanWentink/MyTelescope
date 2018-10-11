namespace MyTelescope.Utilities.Models.Filter
{
    using Helpers;
    using Sort;
    using System.Collections.Generic;
    using System.Linq;

    public class FilterModel
    {
        public string Key
        {
            get { return $"Sort:{Sort.SortItems.Select(x => x.Key).ConcatString(";")};Filter:{FilterItems.Select(x => x.Key).ConcatString(";")}"; }
        }

        public List<FilterItemModel> FilterItems { get; } = new List<FilterItemModel>();

        public SortModel Sort { get; } = new SortModel(new List<SortItemModel>());

        public FilterModel()
        {
        }

        public FilterModel(FilterItemModel filterItem)
        {
            FilterItems.Add(filterItem);
        }

        public FilterModel(List<FilterItemModel> filterItems)
        {
            FilterItems.AddRange(filterItems);
        }

        public FilterModel(SortModel sort, FilterItemModel filterItem)
            : this(filterItem)
        {
            Sort = sort;
        }

        public FilterModel(SortModel sort, List<FilterItemModel> filterItems)
            : this(filterItems)
        {
            Sort = sort;
        }

        public void Add(FilterItemModel filterItem)
        {
            FilterItems.Add(filterItem);
        }

        public void AddRange(List<FilterItemModel> filterItems)
        {
            FilterItems.AddRange(filterItems);
        }
    }
}