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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand cmd= new SqlCommand
                        (
                        @"Insert into Applicant_Work_History
                         (
                            Id,Applicant,Company_Name,Country_Code,Location
                           ,Job_Title,Job_Description,Start_Month,Start_Year,
                             End_Month,End_Year
                          )
                           Values
                          (
                            @Id,@Applicant,@CompanyName,@CountryCode,@Location
                            ,@JobTitle,@JobDescription,@StartMonth,@StartYear
                            ,@EndMonth,@EndYear
                           )"
                        ,conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@CompanyName", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@JobTitle", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@StartMonth", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", poco.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", poco.EndYear);

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

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[500];
                int position = 0;

                SqlCommand cmd = new SqlCommand
                 (
                    @"Select Id,Applicant,Company_Name,Country_Code,Location
                           ,Job_Title,Job_Description,Start_Month,Start_Year,
                             End_Month,End_Year,Time_Stamp
                       from Applicant_Work_History
                    "
                    , conn
                  );

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.CompanyName = reader.GetString(2);
                    poco.CountryCode = reader.GetString(3);
                    poco.Location = reader.GetString(4);
                    poco.JobTitle = reader.GetString(5);
                    poco.JobDescription = reader.GetString(6);
                    poco.StartMonth = reader.GetInt16(7);
                    poco.StartYear = reader.GetInt32(8);
                    poco.EndMonth = reader.GetInt16(9);
                    poco.EndYear = reader.GetInt32(10);
                    poco.TimeStamp = (byte[])reader[11];

                    pocos[position] = poco;
                    position++;


                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Delete from Applicant_Work_History
                        Where Id=@Id"
                        ,conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @"Update Applicant_Work_History
                        SET
                         Applicant=@Applicant
                        ,Company_Name=@CompanyName
                        ,Country_Code=@CountryCode
                        ,Location=@Location
                        ,Job_Title=@JobTitle
                        ,Job_Description=@JobDescription
                        ,Start_Month=@StartMonth
                        ,Start_Year=@StartYear
                        ,End_Month=@EndMonth
                        ,End_Year=@EndYear

                        WHERE Id=@Id"
                        , conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@CompanyName", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@JobTitle", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@StartMonth", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", poco.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", poco.EndYear);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }
    }
}
