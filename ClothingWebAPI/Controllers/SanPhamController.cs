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
using ClothingWebAPI.Entities;
using System.Data.Common;
using System.Reflection;
using System.Diagnostics;

namespace ClothingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanPhamController : ControllerBase
    {
        private readonly ILogger<SanPhamController> _logger;

        private readonly IConfiguration _configuration;
        public SanPhamController(IConfiguration configuration, ILogger<SanPhamController> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        //public static List<T> DataReaderMapToList<T>(DbDataReader dr)
        //{
        //    List<T> list = new List<T>();
        //    while (dr.Read())
        //    {
        //        var obj = Activator.CreateInstance<T>();
        //        foreach (PropertyInfo prop in obj.GetType().GetProperties())
        //        {
        //            if (!Equals(dr[prop.Name], DBNull.Value))
        //            {
        //                prop.SetValue(obj, dr[prop.Name], null);
        //            }
        //        }
        //        list.Add(obj);
        //    }
        //    return list;
        //}


        [HttpGet]
        [Route("all")]
        public IList<SAN_PHAM_ENTITY> GetAllSanPham([FromQuery(Name = "top")] string top)
        {
            var listSanPham = new List<SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_TAT_CA_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@top", SqlDbType.Int).Value = top;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }
            }
            return listSanPham;
        }
        [HttpGet]
        [Route("new-arrivals")]
        public IList<SAN_PHAM_ENTITY> GetSanPhamMoiTao([FromQuery(Name = "top")] string top)
        {
            var listSanPham = new List<SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_SP_MOI_VE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(top!=null && top != "")
                    {
                        cmd.Parameters.Add("@top", SqlDbType.Int).Value = top; // có thể null
                    }
                   
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }
            }
            return listSanPham;
        }
        [HttpGet]
        [Route("most-viewed")]
        public IList<SAN_PHAM_ENTITY> GetSanPhamXemNhieu([FromQuery(Name = "top")] string top)
        {
            var listSanPham = new List<SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_SP_XEM_NHIEU", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (top != null && top != "")
                    {
                        cmd.Parameters.Add("@top", SqlDbType.Int).Value = top; // có thể null
                    }

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }
            }
            return listSanPham;
        }
        [HttpGet]
        [Route("sale-off")]
        public IList<SAN_PHAM_ENTITY> GetSanPhamDangKhuyenMai([FromQuery(Name = "top")] string top)
        {
            var listSanPham = new List<SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_SP_DANG_KHUYEN_MAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (top != null && top != "")
                    {
                        cmd.Parameters.Add("@top", SqlDbType.Int).Value = top; // có thể null
                    }

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }
            }
            return listSanPham;
        }

        [HttpGet]
        [Route("search")]
        public IList<SAN_PHAM_ENTITY> SearchSanPham([FromQuery(Name = "top")] string top, [FromQuery(Name = "keyword")] string keyword)
        {
            var listSanPham = new List<SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("TIM_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (top != null && top != "")
                    {
                        cmd.Parameters.Add("@top", SqlDbType.Int).Value = top; // có thể null
                    }
                    if (keyword != null && keyword != "")
                    {
                        cmd.Parameters.Add("@keyword", SqlDbType.VarChar).Value = keyword; // có thể null
                    }

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }
            }
            return listSanPham;
        }

        [HttpPost]
        [Route("search2")]
        public async Task<IList<SAN_PHAM_ENTITY>> PostSearchKeyword(SearchInputEntity searchInputEntity)
        {
            var listSanPham = new List<SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("TIM_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //if (searchInputEntity.top != null && searchInputEntity.top != 0)
                    //{
                    //    cmd.Parameters.Add("@top", SqlDbType.Int).Value = searchInputEntity.top; // có thể null
                    //}
                    if (searchInputEntity.keyword != null && searchInputEntity.keyword != "")
                    {
                        cmd.Parameters.Add("@keyword", SqlDbType.NVarChar).Value = searchInputEntity.keyword; // có thể null
                    }
                    Debug.WriteLine(searchInputEntity.keyword);
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }
            }
            return listSanPham;
        }

        [HttpGet]
        [Route("")]
        public IList<SAN_PHAM_ENTITY> GetOneSanPham([FromQuery(Name = "productId")] string productId)
        {
            var listSanPham = new List<SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_MOT_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = productId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }

                using (SqlCommand cmd = new SqlCommand("LAY_CT_SP_CUA_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = productId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        try
                        {
                            listSanPham[0].chiTietSanPham = HelperFunction.DataReaderMapToList<CHI_TIET_SAN_PHAM_ENTITY>(reader).ToList();

                        }catch(Exception ex)
                        {
                            Debug.Write("catch truowngf hop ma k ton tai nen k the tim ctsp");
                        }

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }
            }
            return listSanPham;
        }
        [HttpPut]
        [Route("incre-view")]
        public IList<RESPONSE_ENTITY> IncreaseViewCount([FromQuery(Name = "productId")] string productId)
        {
            var response = new List<RESPONSE_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("TANG_LUOT_XEM", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = productId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        response = HelperFunction.DataReaderMapToList<RESPONSE_ENTITY>(reader).ToList();

                        // Map data to Order class using this way
                        //listSanPham = HelperFunction.DataReaderMapToList<SAN_PHAM_ENTITY>(reader).ToList();

                        // instead of this traditional way
                        // while (reader.Read())
                        // {
                        // var o = new Order();
                        // o.OrderID = Convert.ToInt32(reader["OrderID"]);
                        // o.CustomerID = reader["CustomerID"].ToString();
                        // orders.Add(o);
                        // }
                    }
                    cmd.Connection.Close();
                }

            }
            return response;
        }
        //--- older

        //[HttpGet]
        //[Route("")]
        //public JsonResult GetAll()
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
        //public List<SAN_PHAM_ENTITY> GetAll()
        //{
        //    var context = new CLOTHING_STOREContext();
        //    var param = new SqlParameter("@top", null);
        //    //or
        //    /*var param = new SqlParameter() {
        //                        ParameterName = "@FirstName",
        //                        SqlDbType =  System.Data.SqlDbType.VarChar,
        //                        Direction = System.Data.ParameterDirection.Input,
        //                        Size = 50,
        //                        Value = "Bill"
        //    };*/

        //    var listSanPham = contex.("GetStudents @FirstName", param).ToList();
        //}---------//

        /*--------old (using ef core)

        //private List<SAN_PHAM> toBeRemovedFromResult = new List<SAN_PHAM>();
        //[HttpGet]
        //[Route("")]
        //public IEnumerable<SAN_PHAM> GetAll()
        //{
        //    using (var db = new CLOTHING_STOREContext())
        //    {
        //        var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM).Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI).Include(sanpham=>sanpham.MA_TLNavigation).OrderByDescending(sanPham => sanPham.NGAY_TAO).ToList();
        //        return listSanPham;

        //    }
        //    return null;
        //}

        //[HttpGet]
        //[Route("ById")]
        //public IEnumerable<SAN_PHAM> GetOne([FromQuery(Name = "productId")] string productId)
        //{
        //    if (productId != null)
        //    {
        //        using (var db = new CLOTHING_STOREContext())
        //        {
        //            var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM)
        //                .Include(sanPham => sanPham.MA_TLNavigation)
        //                .Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI)
        //                .ThenInclude(ctkm=>ctkm.MA_KMNavigation).Where(sanPham => sanPham.MA_SP == productId).ToList();
        //            // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
        //            foreach (var a in listSanPham.ToList())
        //            {
        //                foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
        //                {
        //                    if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
        //                    {
        //                        System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
        //                        if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
        //                        {
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            a.CHI_TIET_KHUYEN_MAI.Remove(b);
        //                            continue;
        //                        }
        //                    }
        //                }
        //            }

        //            return listSanPham;

        //        }
        //    }

        //    return null;
        //}
        //[HttpGet]
        //[Route("new-arrivals")]
        //public IEnumerable<SAN_PHAM> GetNewArrivals([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        //{

        //    if (offset != null & limit != null)
        //    {
        //        using (var db = new CLOTHING_STOREContext())
        //        {
        //            var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM)
        //                .Include(sanPham=>sanPham.MA_TLNavigation)
        //                .Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI)
        //                .ThenInclude(ctkm => ctkm.MA_KMNavigation)
        //                .OrderByDescending(sanPham => sanPham.NGAY_TAO).Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();

        //            // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
        //            foreach (var a in listSanPham.ToList())
        //            {
        //                foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
        //                {
        //                    if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
        //                    {
        //                        System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
        //                        if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
        //                        {
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            a.CHI_TIET_KHUYEN_MAI.Remove(b);
        //                            continue;
        //                        }
        //                    }
        //                }
        //            }
        //            return listSanPham;

        //        }
        //    }
        //    using (var db = new CLOTHING_STOREContext())
        //    {
        //        var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM).Include(sanPham => sanPham.MA_TLNavigation).Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI).OrderByDescending(sanPham => sanPham.NGAY_TAO).ToList();
        //        // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
        //        foreach (var a in listSanPham.ToList())
        //        {
        //            foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
        //            {
        //                if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
        //                {
        //                    System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
        //                    if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
        //                    {
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        a.CHI_TIET_KHUYEN_MAI.Remove(b);
        //                        continue;
        //                    }
        //                }
        //            }
        //        }
        //        return listSanPham;

        //    }
        //    return null;
        //}

        //[HttpGet]
        //[Route("most-viewed")]
        //public IEnumerable<SAN_PHAM> GetMostViewed([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        //{
        //    if (offset != null & limit != null)
        //    {
        //        using (var db = new CLOTHING_STOREContext())
        //        {
        //            var listSanPham = db.SAN_PHAM.Include(sanPham => sanPham.CHI_TIET_SAN_PHAM).Include(sanPham => sanPham.MA_TLNavigation).Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI).ThenInclude(ctkm => ctkm.MA_KMNavigation).OrderByDescending(sanPham => sanPham.LUOT_XEM).Skip(int.Parse(offset)).Take(int.Parse(limit)).ToList();
        //            // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
        //            foreach (var a in listSanPham.ToList())
        //            {
        //                foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
        //                {
        //                    if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
        //                    {
        //                        System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
        //                        if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
        //                        {
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            a.CHI_TIET_KHUYEN_MAI.Remove(b);
        //                            continue;
        //                        }
        //                    }
        //                }
        //            }

        //            return listSanPham;

        //        }
        //    }
        //    using (var db = new CLOTHING_STOREContext())
        //    {
        //        var listSanPham = db.SAN_PHAM
        //            .Include(sanPham => sanPham.CHI_TIET_SAN_PHAM).Include(sanPham => sanPham.MA_TLNavigation)
        //            .Include(sanPham => sanPham.CHI_TIET_KHUYEN_MAI)
        //            .ThenInclude(ctkm => ctkm.MA_KMNavigation).OrderByDescending(sanPham => sanPham.LUOT_XEM).ToList();
        //        // kiểm tra xem nếu khuyến mãi không đúng thời gian khuyến mãi thì xóa chi tiết khuyến mãi (chỉ lúc lấy lên k xóa trong db)
        //        foreach (var a in listSanPham.ToList())
        //        {
        //            foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
        //            {
        //                if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
        //                {
        //                    System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
        //                    if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
        //                    {
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        a.CHI_TIET_KHUYEN_MAI.Remove(b);
        //                        continue;
        //                    }
        //                }
        //            }
        //        }
        //        return listSanPham;

        //    }

        //    return null;
        //}

        //[HttpGet]
        //[Route("sale-off")]
        //// lấy tất cả sản phẩm đang khuyến mãi
        //public IEnumerable<SAN_PHAM> GetSaleOffProduct([FromQuery(Name = "offset")] string offset, [FromQuery(Name = "limit")] string limit)
        //{
        //    DateTime start = DateTime.Now.Date;
        //    //var end = start.AddDays(1);

        //    //Where(x => x.CreatedDate >= start && x.CreatedDate < end)

        //    if (offset != null & limit != null)
        //    {
        //        using (var db = new CLOTHING_STOREContext())
        //        {
        //            var listSanPhamKhuyenMai = db.SAN_PHAM.Include(sanpham => sanpham.CHI_TIET_SAN_PHAM)
        //                 .Include(sanPham => sanPham.MA_TLNavigation)
        //                .Include(sanpham => sanpham.CHI_TIET_KHUYEN_MAI)
        //                .ThenInclude(ctkm=>ctkm.MA_KMNavigation).Skip(int.Parse(offset)).Take(int.Parse(limit))
        //                .OrderByDescending(sanPham => sanPham.LUOT_XEM).ToList();
        //            foreach (var a in listSanPhamKhuyenMai.ToList())
        //            {
        //               foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
        //                {
        //                    if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
        //                    {
        //                        System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN))>=DateTime.Now));
        //                        if((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
        //                        {
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            a.CHI_TIET_KHUYEN_MAI.Remove(b);
        //                            continue;
        //                        }
        //                    }
        //                }
        //            }

        //            toBeRemovedFromResult.Clear();
        //            foreach (var a in listSanPhamKhuyenMai)
        //            {
        //                if (!(a.CHI_TIET_KHUYEN_MAI.Count > 0))
        //                {
        //                    toBeRemovedFromResult.Add(a);
        //                }
        //            }
        //            foreach (var a in toBeRemovedFromResult)
        //            {
        //                listSanPhamKhuyenMai.Remove(a);
        //            }
        //            return listSanPhamKhuyenMai;

        //        }
        //    }
        //    using (var db = new CLOTHING_STOREContext())
        //    {
        //        var listSanPhamKhuyenMai = db.SAN_PHAM.Include(sanpham => sanpham.CHI_TIET_SAN_PHAM)
        //             .Include(sanPham => sanPham.MA_TLNavigation)
        //                .Include(sanpham => sanpham.CHI_TIET_KHUYEN_MAI)
        //                .ThenInclude(ctkm => ctkm.MA_KMNavigation).ToList();
        //        foreach (var a in listSanPhamKhuyenMai.ToList())
        //        {
        //            foreach (var b in a.CHI_TIET_KHUYEN_MAI.ToList())
        //            {
        //                if (b.MA_KMNavigation.NGAY_AP_DUNG != null)
        //                {
        //                    System.Diagnostics.Debug.WriteLine("---------" + (b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now));
        //                    if ((b.MA_KMNavigation.NGAY_AP_DUNG < DateTime.Now && (b.MA_KMNavigation.NGAY_AP_DUNG.AddDays((double)b.MA_KMNavigation.THOI_GIAN)) >= DateTime.Now))
        //                    {
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        a.CHI_TIET_KHUYEN_MAI.Remove(b);
        //                        continue;
        //                    }
        //                }
        //            }
        //        }
        //        // dùng cách này tránh lỗi System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
        //        toBeRemovedFromResult.Clear();
        //        foreach (var a in listSanPhamKhuyenMai)
        //        {
        //            if (!(a.CHI_TIET_KHUYEN_MAI.Count > 0))
        //            {
        //                toBeRemovedFromResult.Add(a);
        //            }
        //        }
        //        foreach (var a in toBeRemovedFromResult)
        //        {
        //            listSanPhamKhuyenMai.Remove(a);
        //        }
        //        return listSanPhamKhuyenMai;

        //    }

        //    return null;
        //}

        //private bool logDatetiem(DateTime a)
        //{
        //    Console.WriteLine("--------" + a);
        //    return false;
        //}*/
    }

}
