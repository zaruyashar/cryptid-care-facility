using Microsoft.AspNetCore.Mvc;
using CRYPTIDCARE.Models;
using Dapper;

namespace CRYPTIDCARE.Controllers
{
    public class EnclosureController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var enclosures = await Context.ListingAsync<Enclosure>("sp_Enclosure_ViewAll");
            return View(enclosures);
        }

        public async Task<IActionResult> Upsert(int id = 0)
        {
            if (id == 0)
            {
                return View(new Enclosure());
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var enclosure = await Context.GetByIdAsync<Enclosure>("sp_Enclosure_ViewById", param);

            if (enclosure == null)
            {
                return NotFound();
            }

            return View(enclosure);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Enclosure enclosure)
        {
            if (ModelState.IsValid)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", enclosure.Id);
                param.Add("@BiomeType", enclosure.BiomeType);
                param.Add("@MaxCapacity", enclosure.MaxCapacity);
                param.Add("@ContainmentBreach", enclosure.ContainmentBreach);

                await Context.ExecuteReturnAsync("sp_Enclosure_Upsert", param);
                return RedirectToAction(nameof(Index));
            }

            return View(enclosure);
        }

        public async Task<IActionResult> Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            await Context.ExecuteReturnAsync("sp_Enclosure_Delete", param);

            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}