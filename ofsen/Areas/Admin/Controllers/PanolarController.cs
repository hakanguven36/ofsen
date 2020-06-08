using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ofsen.Models;
using ofsen.Araclar;

namespace ofsen.Controllers
{
	[Area("Admin")]
	//[Yetki(Role.admin)]
    public class PanolarController : Controller
    {
        private readonly OfsenContext db;
		private readonly IWebHostEnvironment hostEnvironment;
		private string uploadsRoot;

		public PanolarController(OfsenContext db, IWebHostEnvironment hostEnvironment)
        {
            this.db = db;
			this.hostEnvironment = hostEnvironment;
			uploadsRoot = hostEnvironment.WebRootPath + "/uploads";
		}

        // GET: Panolar
        public async Task<IActionResult> Index()
        {
            return View(await db.Pano.ToListAsync());
        }

        // GET: Panolar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pano = await db.Pano
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pano == null)
            {
                return NotFound();
            }

            return View(pano);
        }

        // GET: Panolar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Panolar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,anatab,title,thumb,metin,yayinTarihi,aktif")] Pano pano)
        {
            if (ModelState.IsValid)
            {
				// dosya kaydet
				if (pano.thumb != null)
				{
					pano.thumbName = Path.GetFileNameWithoutExtension(pano.thumb.FileName) +
							"_" + DateTime.Now.ToString("yyMMddHHmm") +
							Path.GetExtension(pano.thumb.FileName);

					string path = Path.Combine(uploadsRoot, pano.thumbName);
					using (var filestream = new FileStream(path, FileMode.Create))
					{
						await pano.thumb.CopyToAsync(filestream);
					}
				}

				// database kaydet
				pano.yayinTarihi = DateTime.Now;
				db.Add(pano);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pano);
        }

        // GET: Panolar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pano = await db.Pano.FindAsync(id);
            if (pano == null)
            {
                return NotFound();
            }
            return View(pano);
        }

        // POST: Panolar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,anatab,title,thumb,metin,yayinTarihi,aktif")] Pano pano)
        {
            if (id != pano.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					// dosya kaydet
					if (pano.thumb != null)
					{
						pano.thumbName = Path.GetFileNameWithoutExtension(pano.thumb.FileName) +
						"_" + DateTime.Now.ToString("yyMMddHHmm") +
						Path.GetExtension(pano.thumb.FileName);

						string path = Path.Combine(uploadsRoot, pano.thumbName);
						using (var filestream = new FileStream(path, FileMode.Create))
						{
							await pano.thumb.CopyToAsync(filestream);
						}
					}

					// database kaydet
					pano.yayinTarihi = DateTime.Now;
					db.Update(pano);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PanoExists(pano.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pano);
        }

        // GET: Panolar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pano = await db.Pano
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pano == null)
            {
                return NotFound();
            }

            return View(pano);
        }

        // POST: Panolar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pano = await db.Pano.FindAsync(id);
            db.Pano.Remove(pano);
            await db.SaveChangesAsync();
			await Task.Run(() => System.IO.File.Delete(Path.Combine(uploadsRoot, pano.thumbName)));
			return RedirectToAction(nameof(Index));
        }

        private bool PanoExists(int id)
        {
            return db.Pano.Any(e => e.ID == id);
        }
    }
}
