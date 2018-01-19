using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlocknotSQLForms
{
    class Context<T>
    {
        private  string connectionString;
        public Context()
        {
            connectionString = Utils.ConnectionString;
        }

        public ICollection<T>  LoadFromDB()
        {
            ICollection<T> data=  new BindingList<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                PropertyInfo[] properties = typeof(T).GetProperties();
                string commandStr = $"SELECT * FROM {typeof(T).Name} ";
                SqlCommand command = new SqlCommand(commandStr,connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        T newItem = Activator.CreateInstance<T>();
                        foreach (PropertyInfo p in properties)
                        {
                            MethodInfo mi = typeof(Convert).GetMethod($"To{p.PropertyType.Name}", new Type[] { typeof(object) });
                            p.SetValue(newItem, mi.Invoke(null, new object[] { reader[p.Name] }));
                        }
                        data.Add(newItem);
                    }
                }
                return data;
            }
        }


        public void InsertToDB(T t)
        {
            using (SqlConnection connection = new SqlConnection(Utils.ConnectionString))
            {
                connection.Open();
                StringBuilder strColumns = new StringBuilder($"INSERT INTO {typeof(T).Name}(");
                StringBuilder strValues = new StringBuilder("VALUES(");
                foreach (PropertyInfo pi in typeof(T).GetProperties())
                {
                    if (pi.Name.ToLower() != "id")
                    {
                        strColumns.Append($"{pi.Name}, ");
                        if (pi.PropertyType.IsValueType)
                            strValues.Append($"{pi.GetValue(t)}, ");
                        else
                            strValues.Append($"'{pi.GetValue(t)}', ");
                    }
                }

                strColumns.Remove(strColumns.Length - 2, 1);
                strColumns.Append(") ");
                strValues.Remove(strValues.Length - 2, 1);
                strValues.Append(")");
                string strInsert = strColumns.Append(strValues.ToString()).ToString();
                SqlCommand cmd = new SqlCommand(strInsert,connection);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
