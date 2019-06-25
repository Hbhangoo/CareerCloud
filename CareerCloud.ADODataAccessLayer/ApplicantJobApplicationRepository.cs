using CareerCloud.DataAccessLayer;
//using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn =new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(
                       @"Insert into Applicant_Job_Applications (Id,Applicant,job,Application_Date)
                   values(@Id,@Applicant,@Job,@ApplicationDate)", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@ApplicationDate", poco.ApplicationDate);

                    conn.Open();
                   int rowsAffected=cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            //throw new NotImplementedException();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                (
                  @"Select Id,Applicant,Job,Application_Date,Time_Stamp from [dbo].[Applicant_Job_Applications]"
                 , conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[500];
                int x = 0;

                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Job = reader.GetGuid(2);
                    poco.ApplicationDate = reader.GetDateTime(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[x] = poco;
                    x++;


                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
            //throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetList(System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, bool>> where, params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();

            //throw new NotImplementedException();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                      @"Delete from Applicant_Job_Applications where Id=@Id", conn
                     );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowsAffected=cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
           // throw new NotImplementedException();
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);

            using (conn)
            {
                foreach (ApplicantJobApplicationPoco pocos in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"Update Applicant_Job_Applications
                       SET Applicant=@Applicant
                       ,Job=@job
                       ,Application_Date=@AppDate
                        Where Id=@Id"
                     ,conn);

                    cmd.Parameters.AddWithValue("@Id", pocos.Id);
                    cmd.Parameters.AddWithValue("@Applicant", pocos.Applicant);
                    cmd.Parameters.AddWithValue("@Job", pocos.Job);
                    cmd.Parameters.AddWithValue("@AppDate", pocos.ApplicationDate);

                    conn.Open();
                    int rowsAffected=cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
                //throw new NotImplementedException();
            }
    }
}
