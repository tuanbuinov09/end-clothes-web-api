using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using ClothingWebAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BangSizeController : ControllerBase
    {
        private readonly ILogger<BangSizeController> _logger;

        private readonly IConfiguration _configuration;
        public BangSizeController(IConfiguration configuration, ILogger<BangSizeController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }


        //[HttpGet]
        //public JsonResult Get()
        //{
        //    string query = @"Select * from SAN_PHAM";
        //    DataTable dataTable = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("CLOTHING_STORE_CONN");
        //    SqlDataReader sqlDataReader;
        //    using (SqlConnection myConn = new SqlConnection(sqlDataSource))
        //    {
        //        myConn.Open();
        //        using (SqlCommand myCommand = new SqlCommand(query, myConn))
        //        {
        //            sqlDataReader = myCommand.ExecuteReader();
        //            dataTable.Load(sqlDataReader);
        //            sqlDataReader.Close();
        //            myConn.Close();
        //        }
        //    }

        //    return new JsonResult(dataTable);
        //}
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<BANG_SIZE>> GetAll()
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var listBangSize = db.BANG_SIZE.Include(bangSize => bangSize.CHI_TIET_SAN_PHAM).OrderBy(bangSize => bangSize.TEN_SIZE).ToList();
                return listBangSize;

            }
            return null;
        }
        [HttpGet("{id}")]

        public async Task<BANG_SIZE> GetById(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var bangSize = db.BANG_SIZE.Include(bangSize => bangSize.CHI_TIET_SAN_PHAM).Where(bangSize => bangSize.MA_SIZE == id).FirstOrDefault();
                return bangSize;
            }
            return null;
        }
        [HttpPost]

        public async Task<IActionResult> Post(BANG_SIZE bangSize)
        {

            using (var db = new CLOTHING_STOREContext())
            {
                db.BANG_SIZE.Add(bangSize);
                await db.SaveChangesAsync();
                return CreatedAtAction("GetById", new { id = bangSize.MA_SIZE }, bangSize);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, BANG_SIZE bangSize)
        {
            if (id != bangSize.MA_SIZE)
            {
                return BadRequest();
            }

            using (var db = new CLOTHING_STOREContext())
            {
                db.Entry(bangSize).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BANG_SIZE_exist(id))
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
        public async Task<ActionResult<BANG_SIZE>> DeletePublisher(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                var bangSize = await db.BANG_SIZE.FindAsync(id);
                if (bangSize == null)
                {
                    return NotFound();
                }

                db.BANG_SIZE.Remove(bangSize);
                await db.SaveChangesAsync();

                return bangSize;
            }
               
        }
        private bool BANG_SIZE_exist(string id)
        {
            using (var db = new CLOTHING_STOREContext())
            {
                return db.BANG_SIZE.Any(e => e.MA_SIZE == id); 
            }
        }
    }
}
