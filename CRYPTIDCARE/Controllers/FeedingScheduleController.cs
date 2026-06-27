using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRYPTIDCARE.Models;
using Dapper;

namespace CRYPTIDCARE.Controllers
{
    public class FeedingScheduleController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var schedules = await Context.ListingAsync<FeedingSchedule>("sp_FeedingSchedule_ViewAll");
            return View(schedules);
        }

        public async Task<IActionResult> Upsert(int id = 0)
        {
            var cryptids = await Context.ListingAsync<Cryptid>("sp_Cryptid_ViewAll");
            ViewBag.Cryptids = cryptids.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nickname });

            var keepers = await Context.ListingAsync<Keeper>("sp_Keeper_ViewAll");
            ViewBag.Keepers = keepers.Select(k => new SelectListItem { Value = k.Id.ToString(), Text = k.FullName });

            if (id == 0)
            {
                return View(new FeedingSchedule());
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var schedule = await Context.GetByIdAsync<FeedingSchedule>("sp_FeedingSchedule_ViewById", param);

            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(FeedingSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", schedule.Id);
                param.Add("@CryptidId", schedule.CryptidId);
                param.Add("@KeeperId", schedule.KeeperId);
                param.Add("@DietaryItems", schedule.DietaryItems);

                await Context.ExecuteReturnAsync("sp_FeedingSchedule_Upsert", param);
                return RedirectToAction(nameof(Index));
            }

            return View(schedule);
        }

        public async Task<IActionResult> Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            await Context.ExecuteReturnAsync("sp_FeedingSchedule_Delete", param);

            return RedirectToAction(nameof(Index));
        }
    }
}