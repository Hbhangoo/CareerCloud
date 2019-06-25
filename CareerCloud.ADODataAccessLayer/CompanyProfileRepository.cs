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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"INSERT into Company_Profiles
                        (
                            Id
                            ,Registration_Date
                            ,Company_Website
                            ,Contact_Phone
                            ,Contact_Name
                            ,Company_Logo
                        )
                        VALUES
                        (
                            @Id
                            ,@RegistrationDate
                            ,@Website
                            ,@ContactPhone
                            ,@ContactName
                            ,@CompanyLogo

                        )"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@RegistrationDate", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@ContactPhone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@ContactName", poco.ContactName);
                    cmd.Parameters.AddWithValue("@CompanyLogo", poco.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                        (
                        @"SELECT 
                            Id
                            ,Registration_Date
                            ,Company_Website
                            ,Contact_Phone
                            ,Contact_Name
                            ,Company_Logo
                            ,Time_Stamp
                        FROM Company_Profiles"
                        , conn
                        );

                CompanyProfilePoco[] pocos= new CompanyProfilePoco[1000];
                int position = 0;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();

                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    poco.CompanyWebsite = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    poco.ContactPhone = reader.GetString(3);
                    poco.ContactName = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    poco.CompanyLogo = reader.IsDBNull(5) ? null : (byte[])reader[5];
                    poco.TimeStamp = reader.IsDBNull(6) ? null : (byte[])reader[6];

                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"DELETE From Company_Profiles
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

        public void Update(params CompanyProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Update Company_Profiles
                        SET
                             Registration_Date=@RegistrationDate
                            ,Company_Website=@Website
                            ,Contact_Phone=@ContactPhone
                            ,Contact_Name=@ContactName
                            ,Company_Logo=@CompanyLogo
                            
                         WHERE Id=@Id"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@RegistrationDate", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@ContactPhone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@ContactName", poco.ContactName);
                    cmd.Parameters.AddWithValue("@CompanyLogo", poco.CompanyLogo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
