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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Insert into Applicant_Skills 
                        (Id,Applicant,Skill,Skill_Level,Start_Month,Start_Year,End_Month,End_Year)
                        Values
                        (@Id
                        ,@Applicant
                        ,@Skill
                        ,@SkillLevel
                        ,@StartMonth
                        ,@StartYear
                        ,@EndMonth  
                        ,@EndYear)"
                        , conn
                     );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@SkillLevel", poco.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[500];
                int position = 0;

                conn.Open();
                SqlCommand cmd = new SqlCommand
                (
                    @"Select 
                    Id,Applicant,Skill,Skill_Level
                    ,Start_Month,Start_Year
                    ,End_Month,End_Year,Time_Stamp
                    from Applicant_Skills"
                    , conn
                 );
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.StartMonth = reader.GetByte(4);
                    poco.StartYear = reader.GetInt32(5);
                    poco.EndMonth = reader.GetByte(6);
                    poco.EndYear = reader.GetInt32(7);
                    poco.TimeStamp = (byte[])reader[8];

                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }

        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Delete from Applicant_Skills
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

        public void Update(params ApplicantSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (ApplicantSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Update  Applicant_Skills 
                        
                            SET Applicant=@Applicant
                            ,Skill=@Skill
                            ,Skill_Level=@SkillLevel
                            ,Start_Month=@StartMonth
                            ,Start_Year=@StartYear
                            ,End_Month=@EndMonth
                            ,End_Year=@EndYear
                            Where Id=@Id
                        "
                        , conn
                     );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@SkillLevel", poco.SkillLevel);
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
