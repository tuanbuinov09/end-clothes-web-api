using ClothingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BangMauController : ControllerBase
    {
        private readonly ILogger<BangMauController> _logger;

        public BangMauController(ILogger<BangMauController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<BANG_MAU> GetAll()
        {
            _logger.LogInformation("GetAll_BANG_MAU");
            using (var db = new CLOTHING_STOREContext())
            {
                var listBangMau = db.BANG_MAU.Include(bangMau => bangMau.CHI_TIET_SAN_PHAM).OrderBy(bangMau => bangMau.TEN_MAU).ToList();
                return listBangMau;
            }
        }

        [HttpGet("{id}")]
        public BANG_MAU GetById(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var bangMau = db.BANG_MAU.Include(bangMau => bangMau.CHI_TIET_SAN_PHAM).Where(bangMau => bangMau.MA_MAU == id).FirstOrDefault();
                return bangMau;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(BANG_MAU bangMau)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                db.BANG_MAU.Add(bangMau);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetById", new { id = bangMau.MA_MAU }, bangMau);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, BANG_MAU bangMau)
        {
            if (id != bangMau.MA_MAU)
            {
                return BadRequest();
            }

            using (var db = new CLOTHING_STOREContext())
            {
                db.Entry(bangMau).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BANG_MAU_exist(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BANG_MAU>> DeletePublisher(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var bangMau = await db.BANG_MAU.FindAsync(id);
                if (bangMau == null)
                {
                    return NotFound();
                }

                db.BANG_MAU.Remove(bangMau);
                await db.SaveChangesAsync();

                return bangMau;
            }
        }

        private bool BANG_MAU_exist(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                return db.BANG_MAU.Any(e => e.MA_MAU == id);
            }
        }
    }
}
