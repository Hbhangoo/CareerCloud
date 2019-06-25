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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(CompanyLocationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Insert into Company_Locations
                        (
                            Id
                            ,Company
                            ,Country_Code
                            ,State_Province_Code
                            ,Street_Address
                            ,City_Town
                            ,Zip_Postal_Code

                        )
                        VALUES
                        (
                             @Id
                            ,@Company
                            ,@Country
                            ,@Province
                            ,@Address
                            ,@City
                            ,@PostalCode

                        )"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Province", poco.Province);
                    cmd.Parameters.AddWithValue("@Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City", poco.City);
                    cmd.Parameters.AddWithValue("@PostalCode", poco.PostalCode);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {

                SqlCommand cmd = new SqlCommand
                        (
                        @"SELECT
                            Id
                            ,Company
                            ,Country_Code
                            ,State_Province_Code
                            ,Street_Address
                            ,City_Town
                            ,Zip_Postal_Code
                            ,Time_Stamp
                        FROM Company_Locations
                        "
                        , conn
                        );

                CompanyLocationPoco[] pocos = new CompanyLocationPoco[500];
                int position = 0;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.CountryCode = reader.GetString(2);
                    poco.Province = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    poco.Street = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    poco.City = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    poco.PostalCode = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    poco.TimeStamp = (byte[])reader.GetValue(7);

                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyLocationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Delete from Company_Locations
                        WHERE Id=@Id"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
                using (conn)
                {
                    foreach (CompanyLocationPoco poco in items)
                    {
                        SqlCommand cmd = new SqlCommand
                            (
                            @"Update Company_Locations
                            SET
                             Company=@Company
                            ,Country_Code=@Country
                            ,State_Province_Code=@Province
                            ,Street_Address=@Address
                            ,City_Town=@City
                            ,Zip_Postal_Code=@PostalCode

                            WHERE Id=@Id"
                            , conn
                            );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Province", poco.Province);
                    cmd.Parameters.AddWithValue("@Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City", poco.City);
                    cmd.Parameters.AddWithValue("@PostalCode", poco.PostalCode);
                    conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
        }
    }
}
