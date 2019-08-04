
using System.Collections.Generic;

namespace marknote.Models
{
    public class SearchModel
    {
        public string Query { get; set; }

        public List<SearchResultModel> Results { get; set; }
    }
}
