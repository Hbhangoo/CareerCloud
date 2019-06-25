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
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"Insert into Security_Logins_Log(Id,Login,Source_IP,Logon_Date,Is_Succesful)Values(@Id,@Login,@SourceIP,@LogonDate,@IsSuccessful)"
                    , conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@SourceIP", poco.SourceIP);
                    cmd.Parameters.AddWithValue("@LogonDate", poco.LogonDate);
                    cmd.Parameters.AddWithValue("@IsSuccessful", poco.IsSuccesful);

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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                (@"Select Id,Login,Source_IP,Logon_Date,Is_Succesful from Security_Logins_Log"
                , conn);

                SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[2000];
                int position = 0;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.SourceIP = reader.GetString(2);
                    poco.LogonDate = reader.GetDateTime(3);
                    poco.IsSuccesful = reader.GetBoolean(4);

                    pocos[position] = poco;
                    position++;

                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Delete from Security_Logins_Log Where Id=@ID"
                       , conn
                    );
                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Update Security_Logins_Log
                        SET Login=@Login, Source_IP=@SourceIP, Logon_Date=@LogonDate,Is_Succesful=@IsSucessful
                         Where Id=@Id"
                       , conn
                    );

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@SourceIP", poco.SourceIP);
                    cmd.Parameters.AddWithValue("@LogonDate", poco.LogonDate);
                    cmd.Parameters.AddWithValue("@IsSucessful", poco.IsSuccesful);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
        }
    }
}
