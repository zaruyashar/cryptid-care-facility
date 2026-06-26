using Microsoft.AspNetCore.Mvc;
using CRYPTIDCARE.Models;
using Dapper;

namespace CRYPTIDCARE.Controllers
{
    public class CryptidController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var cryptids = await Context.ListingAsync<Cryptid>("sp_Cryptid_ViewAll");
            return View(cryptids);
        }

        public async Task<IActionResult> Upsert(int id = 0)
        {
            if (id == 0)
            {
                return View(new Cryptid());
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            var cryptid = await Context.GetByIdAsync<Cryptid>("sp_Cryptid_ViewById", param);

            if (cryptid == null)
            {
                return NotFound();
            }

            return View(cryptid);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Cryptid cryptid)
        {
            if (ModelState.IsValid)
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", cryptid.Id);
                param.Add("@Nickname", cryptid.Nickname);
                param.Add("@SpeciesType", cryptid.SpeciesType);
                param.Add("@EnclosureId", cryptid.EnclosureId);
                param.Add("@ImageUrl", cryptid.ImageUrl);

                await Context.ExecuteReturnAsync("sp_Cryptid_Upsert", param);
                return RedirectToAction(nameof(Index));
            }

            return View(cryptid);
        }

        public async Task<IActionResult> Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);

            await Context.ExecuteReturnAsync("sp_Cryptid_Delete", param);

            return RedirectToAction(nameof(Index));
        }
    }
}