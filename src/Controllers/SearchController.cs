using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using marknote.Models;
using marknote.Services;

namespace marknote.Controllers
{
    public class SearchController : Controller
    {
        private readonly INoteSearch search;

        public SearchController(INoteSearch search)
        {
            this.search = search;
        }

        public IActionResult Index([FromQuery] string query)
        {
            SearchModel model = new SearchModel
            {
                Query = query
            };

            if ((query != null) && (query.Length > 0))
            {
                model.Results = search.Search(query);
            }

            return View(model);
        }
    }
}
