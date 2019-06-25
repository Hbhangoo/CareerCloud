using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SystemLanguageCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"insert into System_Language_Codes(LanguageID,Name,Native_Name) 
                     values(@LanguageId,@Name,@NativeName)"
                    , conn);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@NativeName", poco.NativeName);


                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                }

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {

                SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[500];
                int position = 0;

                SqlCommand cmd = new SqlCommand
                ("Select LanguageID,Name,Native_Name from System_Language_Codes", conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = reader.GetString(0);
                    poco.Name = reader.GetString(1);
                    poco.NativeName = reader.GetString(2);

                    pocos[position] = poco;
                    position++;

                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SystemLanguageCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"Delete from System_Language_Codes WHERE LanguageID=@LanguageId", conn);
                    cmd.Parameters.AddWithValue("@LanguageId", poco.LanguageID);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SystemLanguageCodePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"Update System_Language_Codes
                        SET Name=@Name,
                            Native_Name=@NativeName
                        WHERE LanguageID=@LanguageId"
                    , conn);
                    cmd.Parameters.AddWithValue("@LanguageId", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@NativeName", poco.NativeName);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
