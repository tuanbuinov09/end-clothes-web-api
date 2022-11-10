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
        [Route("best-seller")]
        public IList<SAN_PHAM_ENTITY> GetSanPhamBanChayTrongNThangGanNhat([FromQuery(Name = "top")] string top, [FromQuery(Name = "month")] string month)
        {
            var listSanPham = new List<SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_SP_BAN_CHAY_TRONG_N_THANG", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (top != null && top != "")
                    {
                        cmd.Parameters.Add("@top", SqlDbType.Int).Value = top; // có thể null
                    }
                    if (month != null && month != "")
                    {
                        cmd.Parameters.Add("@n", SqlDbType.Int).Value = month; // có thể null
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
                        cmd.Parameters.Add("@priceFrom", SqlDbType.Int).Value = searchInputEntity.priceFrom; // có thể null
                        cmd.Parameters.Add("@priceTo", SqlDbType.Int).Value = searchInputEntity.priceTo; // có thể null
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

                using (SqlCommand cmd = new SqlCommand("LAY_HINH_ANH_CUA_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = productId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        try
                        {
                            listSanPham[0].hinhAnhSanPham = HelperFunction.DataReaderMapToList<HINH_ANH_SAN_PHAM_ENTITY>(reader).ToList();

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

        [HttpPost]
        [Route("add")]
        public RESPONSE_ENTITY InsertProduct([FromBody] SAN_PHAM_ENTITY body)
        {
            // chuyển list thành xml string để sql có thể đọc, xem store THEM_SAN_PHAM để biết thêm chi tiết
            var listHinhAnhSanPham_Xml = HelperFunction.ConvertObjectToXMLString(body.hinhAnhSanPham);
            var listChiTietSanPham_Xml = HelperFunction.ConvertObjectToXMLString(body.chiTietSanPham);
            var response = new List<RESPONSE_ENTITY>();

            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("THEM_SAN_PHAM", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@TEN_SP", SqlDbType.NVarChar).Value = body.TEN_SP;
                    cmd.Parameters.Add("@MA_TL", SqlDbType.VarChar).Value = body.MA_TL;
                    cmd.Parameters.Add("@HINH_ANH", SqlDbType.NVarChar).Value = body.HINH_ANH;
                    cmd.Parameters.Add("@MO_TA", SqlDbType.NVarChar).Value = body.MO_TA;
                    cmd.Parameters.Add("@MA_NV", SqlDbType.VarChar).Value = body.MA_NV;

                    cmd.Parameters.Add("@xml_LIST_HINH_ANH_SP_STR", SqlDbType.NVarChar).Value = listHinhAnhSanPham_Xml;
                    cmd.Parameters.Add("@xml_LIST_CHI_TIET_SP_STR", SqlDbType.NVarChar).Value = listChiTietSanPham_Xml;

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
            return response[0];
        }
        [HttpDelete]
        [Route("delete")]
        public RESPONSE_ENTITY DeleteProduct([FromQuery(Name = "productId")] string productId)
        {
            var response = new List<RESPONSE_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("XOA_SAN_PHAM", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MA_SP", SqlDbType.VarChar).Value = productId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        response = HelperFunction.DataReaderMapToList<RESPONSE_ENTITY>(reader).ToList();

                    }
                    cmd.Connection.Close();
                }

            }
            return response[0];
        }
        [HttpGet]
        [Route("detail-for-import")]
        public IList<CHI_TIET_SAN_PHAM_ENTITY> GetChiTietSanPhamForImport([FromQuery(Name = "productId")] string productId)
        {
            var listCTSanPham = new List<CHI_TIET_SAN_PHAM_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_DS_CHI_TIET_CUA_SP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = productId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listCTSanPham = HelperFunction.DataReaderMapToList<CHI_TIET_SAN_PHAM_ENTITY>(reader).ToList();

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
            return listCTSanPham;
        }

        [HttpGet]
        [Route("detail-for-price-change")]
        public IList<THAY_DOI_GIA_ENTITY> GetChiTietSanPhamForPriceChange([FromQuery(Name = "productId")] string productId)
        {
            var listCTSanPham = new List<THAY_DOI_GIA_ENTITY>();
            //using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            using (var con = new SqlConnection(_configuration.GetConnectionString("CLOTHING_STORE_CONN")))
            {
                // Use count to get all available items before the connection closes
                using (SqlCommand cmd = new SqlCommand("LAY_DS_CHI_TIET_CUA_SP_KEM_GIA_HIEN_TAI", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = productId;//có thể null

                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Map data to Order class using this way
                        listCTSanPham = HelperFunction.DataReaderMapToList<THAY_DOI_GIA_ENTITY>(reader).ToList();

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
            return listCTSanPham;
        }
    }

}
