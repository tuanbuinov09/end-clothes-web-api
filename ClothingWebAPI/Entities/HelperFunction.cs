using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClothingWebAPI.Entities
{
    public class HelperFunction
    {
        // hàm map entity với kết quả store trả về
        public static List<T> DataReaderMapToList<T>(DbDataReader dr)
        {
            // Map data to Order class using this way
            // instead of this traditional way
            // while (reader.Read())
            // {
            // var o = new Order();
            // o.OrderID = Convert.ToInt32(reader["OrderID"]);
            // o.CustomerID = reader["CustomerID"].ToString();
            // orders.Add(o);
            // }

            List<T> list = new List<T>();
            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (!Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }catch(Exception ex) //bắt lỗi không có cột trong entity trong khi store trả về có
                    {
                        Debug.Write("////--- lỗi mapping, catched exception, map tiếp cột khác");
                    }
                    
                }
                list.Add(obj);
            }
            return list;
        }

        public static T DataReaderMapToEntity<T>(DbDataReader dr)
        {
            while (dr.Read())
            {
                var obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (!Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch (Exception ex) //bắt lỗi không có cột trong entity trong khi store trả về có
                    {
                        Debug.Write("////--- lỗi mapping, catched exception, map tiếp cột khác");
                    }

                }
                return obj;
            }
            return default(T);
        }


        public static string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }

    }
}
