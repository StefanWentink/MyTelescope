namespace MyTelescope.Utilities.Models.Filter
{
    using Enums;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FilterItemModel
    {
        public string Key => $"{Column}-{Filter}-{Value}";

        public string Column { get; set; }

        public ColumnType Type { get; set; }

        public FilterType Filter { get; set; }

        public object Value { get; set; }

        [Obsolete("Only for serialisation.")]
        public FilterItemModel()
        {
        }

        private FilterItemModel(string column)
        {
            Column = column;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterItemModel"/> class.
        /// Bool column
        /// </summary>
        /// <param name="column">
        /// </param>
        /// <param name="value">
        /// </param>
        public FilterItemModel(string column, bool value)
        : this(column, ColumnType.BoolColumn, FilterType.Equal, value)
        {
            Column = column;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterItemModel"/> class.
        /// In - filter
        /// </summary>
        /// <param name="column"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public FilterItemModel(string column, ColumnType type, List<object> value)
            : this(column, type, value.Count == 1 ? FilterType.Equal : FilterType.In, value.Count == 1 ? value.Single() : value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterItemModel"/> class.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        public FilterItemModel(string column, ColumnType type, FilterType filter, object value)
            : this(column)
        {
            if (!ColumnTypeHelper.GetValidFilterTypes(type).Contains(filter))
            {
                throw new ArgumentOutOfRangeException(nameof(filter), $"{filter} is invalid for {type}.");
            }

            Column = column;
            Type = type;
            Filter = filter;
            Value = value;
        }
    }
}