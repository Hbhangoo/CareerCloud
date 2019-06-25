using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.
                ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantEducationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (

                     @"insert into applicant_educations
                     ([Id],[Applicant],[Major],[Certificate_Diploma],[Start_Date],[Completion_Date],
                    [Completion_Percent]) VALUES(@Id,@Applicant,@Major,@Certificate_Diploma,@Start_Date,@End_Date,@Completion_Percent)"
                    , conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@End_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("Completion_Percent", poco.CompletionPercent);

                    conn.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }

//                throw new NotImplementedException();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            //try(){ 
            string connectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"Select [Id],[Applicant],[Major],[Certificate_Diploma],[Start_Date],
                [Completion_Date],[Completion_Percent],[Time_Stamp] from [JOB_PORTAL_DB].[dbo].Applicant_Educations";

                int position = 0;
                ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[500];

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Major = reader.GetString(2);
                    poco.CertificateDiploma = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    poco.StartDate = reader.IsDBNull(4) ? null : (DateTime?)reader[4];
                    poco.CompletionDate = reader.IsDBNull(5) ? null : (DateTime?)reader[5]; 
                    poco.CompletionPercent = reader.IsDBNull(5) ? null : (byte?)reader[6];
                    poco.TimeStamp = (byte[])reader.GetValue(7);

                    pocos[position] = poco;
                    position++;

                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }

            // finally()
            //  { }

            //throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();

           // throw new NotImplementedException();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection
                (
                ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString 
                );
            using (conn)
            {
                foreach (ApplicantEducationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                            @"Delete Applicant_Educations 
                            where Id =@Id", conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rowsEffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
             //   throw new NotImplementedException();
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantEducationPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand(@"update [dbo].[Applicant_Educations]
                        SET Applicant=@Applicant
                        ,Major=@Major
                        ,Certificate_Diploma=@Cert
                        ,Start_Date=@StartDate
                        ,Completion_Date=@CompletionDate
                        ,Completion_Percent=@Percent
                          WHERE Id=@Id", conn);

                    cmd.Parameters.AddWithValue("@Id",poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Cert", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@StartDate", poco.StartDate);
                    cmd.Parameters.AddWithValue("@CompletionDate", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Percent", poco.CompletionPercent);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

          //  throw new NotImplementedException();
        }
    }
}
    

