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
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"insert into Security_Roles(Id,Role,Is_Inactive)
                      Values(@Id,@Role,@IsInactive)"
                    , conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);

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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {

                SecurityRolePoco[] pocos = new SecurityRolePoco[500];
                int position = 0;

                SqlCommand cmd = new SqlCommand
                ("Select Id,Role,Is_Inactive from Security_Roles", conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Role = reader.GetString(1);
                    poco.IsInactive = reader.GetBoolean(2);

                    pocos[position] = poco;
                    position++;

                }
                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }


        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();

        }

        public void Remove(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"Delete from Security_Roles WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rows= cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
         }

        public void Update(params SecurityRolePoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(SecurityRolePoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"Update Security_Roles
                        SET Role=@Role,
                        Is_Inactive=@IsInactive
                        WHERE Id=@ID"
                    , conn);
                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    cmd.Parameters.AddWithValue("@Role", poco.Role);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
         }
    }
}
