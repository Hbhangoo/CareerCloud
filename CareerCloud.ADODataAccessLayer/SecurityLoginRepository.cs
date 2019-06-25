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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach(SecurityLoginPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (
                        @"Insert into Security_Logins(Id,Login,Password,Created_Date,Password_Update_Date,Agreement_Accepted_Date
                        ,Is_Locked,Is_Inactive,Email_Address,Phone_Number,Full_Name,Force_Change_Password,Prefferred_Language)
                        Values(@Id,@Login,@Password,@CreatedDate,@PasswordUpdateDate,@AgreementAcceptedDate,@IsLocked,@ISInactive,
                        @Email,@PhoneNumber,@FullName,@ForcePassword,@Language)"
                        ,conn
                     );
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@CreatedDate", poco.Created);
                    cmd.Parameters.AddWithValue("@PasswordUpdateDate", poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@AgreementAcceptedDate", poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@IsLocked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@ISInactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNumber", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@FullName", poco.FullName);
                    cmd.Parameters.AddWithValue("@ForcePassword", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Language", poco.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                SqlCommand cmd = new SqlCommand
                (
                    @"Select Id,Login,Password,Created_Date,Password_Update_Date,Agreement_Accepted_Date
                     ,Is_Locked,Is_Inactive,Email_Address,Phone_Number,Full_Name,Force_Change_Password,
                      Prefferred_Language, Time_Stamp from Security_Logins"
                    , conn
                );

                SecurityLoginPoco[] pocos = new SecurityLoginPoco[500];
                int position = 0;

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();


                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.GetString(1);
                    poco.Password =reader.GetString(2);
                    poco.Created =reader.GetDateTime(3);
                    poco.PasswordUpdate = reader.IsDBNull(4) ? null:(DateTime?)reader.GetDateTime(4);//.GetDateTime(4);
                    poco.AgreementAccepted = reader.IsDBNull(5) ? null :(DateTime?)reader.GetDateTime(5);
                    poco.IsLocked = reader.GetBoolean(6);
                    poco.IsInactive = reader.GetBoolean(7);
                    poco.EmailAddress = reader.GetString(8);
                    poco.PhoneNumber = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);//.GetDateTime(4);
                    poco.FullName = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    poco.ForceChangePassword = reader.GetBoolean(11);
                    poco.PrefferredLanguage = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    poco.TimeStamp = (byte[])reader[13];


                    pocos[position] = poco;

                    position++;
                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> poco = GetAll().AsQueryable();
            return poco.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityLoginPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"Delete from Security_Logins WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            using (conn)
            {
                foreach (SecurityLoginPoco poco in items)
                {
                    SqlCommand cmd = new SqlCommand
                    (@"Update Security_Logins
                     SET Login=@Login,Password=@Password,Created_Date=@CreatedDate,Password_Update_Date=@PasswordUpdate
                    ,Agreement_Accepted_Date=@AgreementDate,Is_Locked=@IsLocked,Is_Inactive=@IsInactive,Email_Address=@Email
                    ,Phone_Number=@Phone,Full_Name=@FullName,Force_Change_Password=@ForceChange,Prefferred_Language=@Language
                    WHERE Id=@ID"
                    , conn);
                    cmd.Parameters.AddWithValue("@ID", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@CreatedDate", poco.Created);
                    cmd.Parameters.AddWithValue("@PasswordUpdate", poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@AgreementDate", poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@IsLocked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@FullName", poco.FullName);
                    cmd.Parameters.AddWithValue("@ForceChange", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Language", poco.PrefferredLanguage);


                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
