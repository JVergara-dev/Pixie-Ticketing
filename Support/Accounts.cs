using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace Support
{
    public class Accounts
    {
        public class Account
        {
            public int ID;
            public string Name;
            public string Username;
            public string Password;
            public int DepartmentID;
            public int RoleID;
            public DateTime CreatedTS;
            public int CreatedBy_User_ID;
            public string CreatedBy_User_Name;
            public DateTime UpdatedTS;
            public int UpdatedBy_User_ID;
            public string UpdatedBy_User_Name;
            public bool IsActive; 
        }

        public Account SelectUserByUsername(string Username) {
            Account retObj = new Account();
            SqlConnection conn = new SqlConnection(Parameters.PixieConnectionString);
            SqlCommand cmd = new SqlCommand("sp_SelectUserByUsername",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", Username);
            try {
                conn.Open();
                if(conn.State != ConnectionState.Open) { throw new Exception(); }
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        retObj.ID = (int)rdr["ID"];
                        retObj.Name = (string)rdr["Name"];
                        retObj.Username = (string)rdr["Username"];
                        retObj.Password = (string)rdr["Password"];
                        retObj.DepartmentID = (int)rdr["Department_ID"];
                        retObj.RoleID = (int)rdr["Role_ID"];
                        retObj.CreatedTS = (DateTime)rdr["CreatedTS"];
                        retObj.CreatedBy_User_ID = (int)rdr["CreatedBy_User_ID"];
                        retObj.CreatedBy_User_Name = (string)rdr["CreatedBy_User_Name"];
                        retObj.UpdatedTS = (DateTime)rdr["UpdatedTS"];
                        retObj.UpdatedBy_User_ID = (int)rdr["UpdatedBy_User_ID"];
                        retObj.UpdatedBy_User_Name = (string)rdr["UpdatedBy_User_Name"];
                        retObj.IsActive = (bool)rdr["IsActive"];
                    }
                }

            }
            catch (SqlException ex){
                retObj = new Account();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }

            return retObj;
        }
    }
}
