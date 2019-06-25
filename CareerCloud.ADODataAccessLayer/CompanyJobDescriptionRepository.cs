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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(CompanyJobDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Insert into Company_Jobs_Descriptions
                        (
                        Id
                        ,Job
                        ,Job_Name
                        ,Job_Descriptions
                        )
                        VALUES
                        (           
                        @Id
                        ,@Job
                        ,@JobName
                        ,@JobDescription
                        )"
                        ,conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@JobName", poco.JobName);
                    cmd.Parameters.AddWithValue("@JobDescription", poco.JobDescriptions);

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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                

                SqlCommand cmd= new SqlCommand
                    (
                    @" SELECT
                         Id
                        ,Job
                        ,Job_Name
                        ,Job_Descriptions
                        ,Time_Stamp
                    
                    from Company_Jobs_Descriptions"
                    , conn
                    );

                CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[3000];
                int position = 0;

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.JobName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    poco.JobDescriptions = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    poco.TimeStamp = reader.IsDBNull(3) ? null : (byte[])reader[4];

                    pocos[position] = poco;
                    position++;
                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @" DELETE from Company_Jobs_Descriptions
                        WHERE Id=@Id"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Update Company_Jobs_Descriptions
                        SET
                         Job=@Job
                        ,Job_Name=@JobName
                        ,Job_Descriptions=@JobDescription

                        WHERE Id=@Id"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@JobName", poco.JobName);
                    cmd.Parameters.AddWithValue("@JobDescription", poco.JobDescriptions);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
