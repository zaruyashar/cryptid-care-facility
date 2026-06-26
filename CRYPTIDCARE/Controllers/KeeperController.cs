using Microsoft.AspNetCore.Mvc;
using CRYPTIDCARE.Models;
using Dapper;

namespace CRYPTIDCARE.Controllers
{
    public class KeeperController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var keepers = await Context.ListingAsync<Keeper>("sp_Keeper_ViewAll");
            return View(keepers);
        }

        public async Task<IActionResult> Upsert(int id = 0)
        {
            if (id == 0)
            {
                return View(new Keeper());
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var keeper = await Context.GetByIdAsync<Keeper>("sp_Keeper_ViewById", param);

            if (keeper == null)
            {
                return NotFound();
            }

            return View(keeper);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Keeper keeper)
        {
            if (ModelState.IsValid)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", keeper.Id);
                param.Add("@FullName", keeper.FullName);
                param.Add("@Specialty", keeper.Specialty);
                param.Add("@HazardPayRate", keeper.HazardPayRate);

                await Context.ExecuteReturnAsync("sp_Keeper_Upsert", param);
                return RedirectToAction(nameof(Index));
            }

            return View(keeper);
        }

        public async Task<IActionResult> Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            await Context.ExecuteReturnAsync("sp_Keeper_Delete", param);

            return RedirectToAction(nameof(Index));
        }
    }
}