using Microsoft.AspNetCore.Mvc;
using CRYPTIDCARE.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRYPTIDCARE.Controllers
{
    public class SearchController : Controller
    {
        public async Task<IActionResult> Index(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return View(new GlobalSearchResult { SearchQuery = q });
            }

            using var db = new SqlConnection(Context.connectionstring);
            await db.OpenAsync();

            using var multi = await db.QueryMultipleAsync("sp_GlobalSearch", new { SearchTerm = q }, commandType: CommandType.StoredProcedure);

            var model = new GlobalSearchResult
            {
                SearchQuery = q,
                Cryptids = await multi.ReadAsync<Cryptid>(),
                Enclosures = await multi.ReadAsync<Enclosure>(),
                Keepers = await multi.ReadAsync<Keeper>(),
                FeedingSchedules = await multi.ReadAsync<FeedingSchedule>()
            };

            return View(model);
        }
    }
}