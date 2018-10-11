namespace MyTelescope.Utilities.Models.Sort
{
    using System;

    public class SortItemModel
    {
        public string Key => $"{Column}-{Ascending}";

        public string Column { get; set; }

        public bool Ascending { get; set; }

        [Obsolete("Only for serialisation.")]
        public SortItemModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortItemModel"/> class.
        /// Ascending
        /// </summary>
        /// <param name="column">
        /// </param>
        public SortItemModel(string column)
        : this(column, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortItemModel"/> class.
        /// </summary>
        /// <param name="column">
        /// </param>
        /// <param name="ascending">
        /// </param>
        public SortItemModel(string column, bool ascending)
        {
            Column = column;
            Ascending = ascending;
        }
    }
}