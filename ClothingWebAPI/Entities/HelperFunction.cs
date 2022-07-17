using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class HelperFunction
    {
        // hàm map entity với kết quả store trả về
        public static List<T> DataReaderMapToList<T>(DbDataReader dr)
        {
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
    }
}
