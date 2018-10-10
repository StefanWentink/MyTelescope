namespace MyTelescope.Utilities.Models.Sort
{
    using System;
    using System.Collections.Generic;

    public class SortModel
    {
        public List<SortItemModel> SortItems { get; set; } = new List<SortItemModel>();

        public int Skip { get; set; }

        public int Take { get; set; }

        public int RecordRequestNumber => Skip + Take;

        [Obsolete("Only for serialisation.")]
        public SortModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="column"></param>
        public SortModel(string column)
            : this(new SortItemModel(column))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="ascending"></param>
        public SortModel(string column, bool ascending)
            : this(new SortItemModel(column, ascending))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="filterItem">
        /// </param>
        public SortModel(SortItemModel filterItem)
            : this(filterItem, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="filterItems"></param>
        public SortModel(List<SortItemModel> filterItems)
            : this(filterItems, 0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        public SortModel(int skip, int take)
            : this(new List<SortItemModel>(), skip, take)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        public SortModel(string column, int skip, int take)
            : this(new SortItemModel(column), skip, take)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="column"></param>
        /// <param name="ascending"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        public SortModel(string column, bool ascending, int skip, int take)
            : this(new SortItemModel(column, ascending), skip, take)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="filterItem">
        /// </param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        public SortModel(SortItemModel filterItem, int skip, int take)
        {
            SortItems.Add(filterItem);
            Skip = skip;
            Take = take;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortModel"/> class. 
        /// </summary>
        /// <param name="filterItems"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        public SortModel(List<SortItemModel> filterItems, int skip, int take)
        {
            SortItems.AddRange(filterItems);
            Skip = skip;
            Take = take;
        }

        public void Add(SortItemModel filterItem)
        {
            SortItems.Add(filterItem);
        }

        public void AddRange(List<SortItemModel> filterItems)
        {
            SortItems.AddRange(filterItems);
        }
    }
}
