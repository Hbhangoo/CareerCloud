using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        public void Add(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                     (
                        @"insert into Applicant_Profiles
                        (
                            Id,Login,Current_Salary,Current_Rate,Currency
                            ,Country_Code,State_Province_Code,Street_Address
                            ,City_Town,Zip_Postal_Code
                        )
                        Values
                        (
                            @Id,@Login,@CurrentSalary,@CurrentRate,@Currency
                           ,@CountryCode,@Province,@Address,@City,@ZipCode
                        )"
                        , conn
                     );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CurrentRate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.Country);
                    cmd.Parameters.AddWithValue("@Province", poco.Province);
                    cmd.Parameters.AddWithValue("@Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City", poco.City);
                    cmd.Parameters.AddWithValue("@ZipCode", poco.PostalCode);

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

        public IList<ApplicantProfilePoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[500];
                int position = 0;
                
                    SqlCommand cmd = new SqlCommand
                     (
                        @"Select 
                            Id,Login,Current_Salary
                            ,Current_Rate,Currency
                            ,Country_Code,State_Province_Code
                            ,Street_Address
                            ,City_Town,Zip_Postal_Code,Time_Stamp
                        
                        from Applicant_Profiles"
                        , conn
                     );

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.CurrentSalary = reader.IsDBNull(2) ? null : (Decimal?)reader.GetDecimal(2);
                    poco.CurrentRate = reader.IsDBNull(3) ? null : (Decimal?)reader.GetDecimal(3);
                    poco.Currency = reader.IsDBNull(4) ? string.Empty:reader.GetString(4);
                    poco.Country = reader.IsDBNull(5) ? string.Empty:reader.GetString(5);
                    poco.Province = reader.IsDBNull(6) ? string.Empty:reader.GetString(6);
                    poco.Street = reader.IsDBNull(7) ? string.Empty:reader.GetString(7);
                    poco.City = reader.IsDBNull(8) ? string.Empty:reader.GetString(8);
                    poco.PostalCode = reader.IsDBNull(9) ? string.Empty:reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];

                    pocos[position] = poco;

                    position++;

                }
                

                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantProfilePoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                     (
                        @"Delete from Applicant_Profiles
                        Where Id=@Id"
                        , conn
                     );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                     (
                        @"Update Applicant_Profiles
                        SET
                        Id=@Id,
                        Login=@Login
                        ,Current_Salary=@CurrentSalary
                        ,Current_Rate=@CurrentRate
                        ,Currency=@Currency
                        ,Country_Code=@CountryCode
                        ,State_Province_Code=@Province
                        ,Street_Address=@Address
                        ,City_Town=@City
                        ,Zip_Postal_Code=@Zipcode

                        Where Id=@ID"
                        , conn
                     );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@CurrentRate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.Country);
                    cmd.Parameters.AddWithValue("@Province", poco.Province);
                    cmd.Parameters.AddWithValue("@Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City", poco.City);
                    cmd.Parameters.AddWithValue("@ZipCode", poco.PostalCode);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();


                }
            }
        }
    }
}
