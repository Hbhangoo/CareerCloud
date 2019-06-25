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
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(SecurityLoginsRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"insert into Security_Logins_Roles(Id,Login,Role)
                      Values(@ID,@Login,@Role)"
                    ,conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
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

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                (@"Select Id,Login,Role,Time_Stamp from Security_Logins_Roles"
                 , conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[500];
                int position = 0;

                while(reader.Read())
                {
                    SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetGuid(1);
                    poco.Role = reader.GetGuid(2);
                    poco.TimeStamp = reader.IsDBNull(3) ? null : (byte[])reader[3];

                    pocos[position] = poco;
                    position++;
                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();

            }
        }

            public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    ("Delete from Security_Logins_Roles WHERE Id=@Id"
                    , conn);
                    cmd.Parameters.AddWithValue("Id",poco.Id);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public void Update(params SecurityLoginsRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(SecurityLoginsRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                    @"Update Security_Logins_Roles
                      SET Login=@Login
                      ,Role=@Role Where Id=@ID"
                      , conn);
                    cmd.Parameters.AddWithValue("ID", poco.Id);
                    cmd.Parameters.AddWithValue("Login", poco.Login);
                    cmd.Parameters.AddWithValue("Role", poco.Role);
                    conn.Open();
                    int rows=cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
        }
    }
}
