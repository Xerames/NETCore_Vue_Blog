using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Extensions
{
    public interface IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}
