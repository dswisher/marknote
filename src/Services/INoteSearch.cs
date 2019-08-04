
using System.Collections.Generic;
using marknote.Models;

namespace marknote.Services
{
    public interface INoteSearch
    {
        List<SearchResultModel> Search(string query);
    }
}
