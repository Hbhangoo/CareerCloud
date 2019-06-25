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
    public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
    {
        public void Add(params CompanyJobEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {

                foreach(CompanyJobEducationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"INSERT into Company_Job_Educations
                        (
                            Id
                            ,Job
                            ,Major
                            ,Importance
                        )
                        VALUES
                        (
                            @Id
                            ,@Job
                            ,@Major
                            ,@Importance 
                        )"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Importance", poco.Importance);

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

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                    (
                    @"SELECT
                            Id
                            ,Job
                            ,Major
                            ,Importance
                            ,Time_Stamp
                    from Company_Job_Educations"
                    , conn
                    );

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                CompanyJobEducationPoco [] pocos = new CompanyJobEducationPoco[3000];
                int position = 0;

                while(reader.Read())
                {
                    CompanyJobEducationPoco poco = new CompanyJobEducationPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.Importance = reader.GetInt16(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[position] = poco;
                    position++;

                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobEducationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Delete from Company_Job_Educations
                        WHERE Id=@Id"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobEducationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Update Company_Job_Educations
                        SET
                             Id=@Id
                            ,Job=@Job
                            ,Major=@Major
                            ,Importance=@Importance  
                        WHERE Id=@Id"
                        , conn
                        );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Importance", poco.Importance);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                   }
            }
        }
    }
}
