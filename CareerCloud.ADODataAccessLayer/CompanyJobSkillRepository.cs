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
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        public void Add(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {

                foreach(CompanyJobSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @" Insert into Company_Job_Skills
                        (
                            Id
                            ,Job
                            ,Skill
                            ,Skill_Level
                            ,Importance
        
                        )
                        VALUES
                        (
                            @Id
                            ,@Job
                            ,@Skill
                            ,@SkillLevel
                            ,@Importance
 
                        )"
                        , conn
                        );
                    cmd.Parameters.AddWithValue("@Id",poco.Id);
                    cmd.Parameters.AddWithValue("@Job",poco.Job);
                    cmd.Parameters.AddWithValue("@Skill",poco.Skill);
                    cmd.Parameters.AddWithValue("@SkillLevel",poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance",poco.Importance);

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

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                    (
                    @"SELECT
                            Id
                            ,Job
                            ,Skill
                            ,Skill_Level
                            ,Importance
                            ,Time_Stamp
        
                     FROM Company_Job_Skills   
                    "
                    , conn
                    );

                    CompanyJobSkillPoco[] pocos= new CompanyJobSkillPoco[6000];
                int position = 0;

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Job = reader.GetGuid(1);
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.Importance = reader.GetInt32(4);
                    poco.TimeStamp = (byte[])reader[5];

                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @" Delete from Company_Job_Skills
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

        public void Update(params CompanyJobSkillPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (CompanyJobSkillPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                        (
                        @" Update Company_Job_Skills
                        SET
                             Job=@Job
                            ,Skill=@Skill
                            ,Skill_Level=@SkillLevel
                            ,Importance=@Importance
        
                        WHERE Id=@Id"
                        , conn
                        );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Skill", poco.Skill);
                    cmd.Parameters.AddWithValue("@SkillLevel", poco.SkillLevel);
                    cmd.Parameters.AddWithValue("@Importance", poco.Importance);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();


                }
            }
        }
    }
}
