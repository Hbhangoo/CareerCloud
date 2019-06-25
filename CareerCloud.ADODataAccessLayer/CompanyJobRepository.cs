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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        public void Add(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(CompanyJobPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Insert into Company_Jobs
                        (
                            Id
                            ,Company
                            ,Profile_Created
                            ,Is_Inactive
                            ,Is_Company_Hidden
                        )
                        VALUES
                        (
                            @Id
                            ,@Company
                            ,@ProfileCreated
                            ,@IsInactive
                            ,@CompanyHidden                            

                        )"
                        , conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@ProfileCreated", poco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@CompanyHidden", poco.IsCompanyHidden);

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

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                        (
                        @" SELECT
                            Id
                            ,Company
                            ,Profile_Created
                            ,Is_Inactive
                            ,Is_Company_Hidden
                            ,Time_Stamp
                        FROM Company_Jobs"
                        , conn
                        );
                CompanyJobPoco[] pocos = new CompanyJobPoco[3000];
                int position = 0;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.ProfileCreated = reader.GetDateTime(2);
                    poco.IsInactive = reader.GetBoolean(3);
                    poco.IsCompanyHidden = reader.GetBoolean(4);
                    poco.TimeStamp = reader.IsDBNull(5) ? null : (byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"DELETE from Company_Jobs
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

        public void Update(params CompanyJobPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @" Update Company_Jobs
                        SET 
                        Company=@Company
                        ,Profile_Created=@ProfileCreated
                        ,Is_Inactive=@IsInactive
                        ,Is_Company_Hidden=@CompanyHidden
                        WHERE Id=@Id"
                        , conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@ProfileCreated", poco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@CompanyHidden", poco.IsCompanyHidden);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
