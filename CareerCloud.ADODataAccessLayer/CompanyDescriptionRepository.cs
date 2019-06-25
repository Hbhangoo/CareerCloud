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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Insert into Company_Descriptions (Id,Company,LanguageID,Company_Name,Company_Description)
                        Values(@ID,@Company,@LanguageId,@CompanyName,@CompanyDescription)"
                    , conn);

                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageId", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@CompanyName", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyDescription", poco.CompanyDescription);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();


                }

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand(@"Select Id,Company,LanguageID,Company_Name,
                Company_Description,Time_Stamp from Company_Descriptions"
                , conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[5000];
                int position = 0;

                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.LanguageId = reader.GetString(2);
                    poco.CompanyName = reader.GetString(3);
                    poco.CompanyDescription = reader.GetString(4);
                    poco.TimeStamp = (byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }

        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"Delete from Company_Descriptions WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"Update Company_Descriptions
                    SET Company=@Company
                    ,LanguageID=@LanguageID
                    ,Company_Name=@CompanyName
                    ,Company_DEscription=@CompanyDescription
                    WHERE Id=@ID", conn);

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@CompanyName", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyDescription", poco.CompanyDescription);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
