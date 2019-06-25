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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Insert into Applicant_Resumes
                        (
                         Id
                        ,Applicant
                        ,Resume
                        ,Last_Updated
                        )
                        Values
                        (
                            @Id,
                            @Applicant,
                            @Resume,
                            @LastUpdated

                        )"
                     , conn
                     );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@LastUpdated", poco.LastUpdated);

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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                ApplicantResumePoco[] pocos = new ApplicantResumePoco[500];
                int position = 0;

                SqlCommand cmd = new SqlCommand
                    (
                        @"Select 
                             Id
                            ,Applicant
                            ,Resume
                             ,Last_Updated
                        from Applicant_Resumes"
                     , conn
                     );

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Resume = reader.GetString(2);
                    poco.LastUpdated = reader.IsDBNull(3) ? null : (DateTime?)reader.GetDateTime(3); 

                    pocos[position] = poco;
                    position++;
                }
                
                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Delete from Applicant_Resumes
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

        public void Update(params ApplicantResumePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Update Applicant_Resumes 
                         SET Applicant=@Applicant
                            ,Resume=@Resume
                            ,Last_Updated=@LastUpdated
                            Where Id=@Id"
                        , conn
                     );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@LastUpdated", poco.LastUpdated);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }
    }
}
