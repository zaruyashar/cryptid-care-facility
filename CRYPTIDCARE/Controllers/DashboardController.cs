using Microsoft.AspNetCore.Mvc;
using CRYPTIDCARE.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CRYPTIDCARE.Controllers
{
    public class DashboardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cryptids = await Context.ListingAsync<Cryptid>("sp_Cryptid_ViewAll");
            var enclosures = await Context.ListingAsync<Enclosure>("sp_Enclosure_ViewAll");
            var keepers = await Context.ListingAsync<Keeper>("sp_Keeper_ViewAll");
            var feedings = await Context.ListingAsync<FeedingSchedule>("sp_FeedingSchedule_ViewAll");

            ViewBag.TotalCryptids = cryptids.Count();
            ViewBag.TotalEnclosures = enclosures.Count();
            ViewBag.TotalKeepers = keepers.Count();
            ViewBag.TotalFeedings = feedings.Count();

            ViewBag.ContainmentBreach = enclosures.Count(e => e.ContainmentBreach);

            ViewBag.RecentCryptids = cryptids.OrderByDescending(c => c.Id).Take(5).ToList();

            ViewBag.TopKeepers = keepers.OrderByDescending(k => k.HazardPayRate).Take(5).ToList();

            ViewBag.SpeciesDistribution = cryptids
                .GroupBy(c => c.SpeciesType)
                .ToDictionary(g => g.Key, g => g.Count());

            ViewBag.EnclosureCapacities = enclosures.Select(e => new
            {
                e.BiomeType,
                e.MaxCapacity,
                CurrentOccupancy = cryptids.Count(c => c.EnclosureId == e.Id)
            }).ToList();

            return View();
        }
    }
}