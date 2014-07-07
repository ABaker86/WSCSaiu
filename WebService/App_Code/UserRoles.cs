using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlServerCe;

namespace AquaGear_ITSD425_Rebuild
{
    public class UserRoles
    {
        //connection variables
        private String _connectionString;
        private SqlCeConnection _conn;

        //Private Variables
        private long _userID;
        private String _user_name;
        private String _password;
        private bool _isAdmin = false;

        private String _extendedErrorInformation;

        #region Constructors

        /// <summary>
        /// Generic class Constructor
        /// </summary>
        public UserRoles()
        {
            //uses connection string in web.config
            _connectionString = WebConfigurationManager.ConnectionStrings["DirectConnectToSQL4"].ConnectionString;
            _conn = new SqlCeConnection(_connectionString);
            ClearFields();
        }

        /// <summary>
        /// Class Constructor that allows the passing of connection string
        /// </summary>
        /// <param name="connString"></param>
        public UserRoles(String connString)
        {
            _connectionString = connString;
            _conn = new SqlCeConnection(_connectionString);
            ClearFields();
        }

        #endregion
        #region Properties

        public long UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public String UserName
        {
            get { return _user_name; }
            set { _user_name = value; }
        }

        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public bool IsAdministrator
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        public String ExtendedErrorInformation
        {
            get { return _extendedErrorInformation; }
            //read only
            //set { _extendedErrorInformation = value;}
        }
        #endregion
        #region Private Methods

        /// <summary>
        /// Clear all private variables
        /// </summary>
        private void ClearFields()
        {
            _userID = 0;
            _user_name = "";
            _password = "";
            _isAdmin = false;
            _extendedErrorInformation = "";
        }

        /// <summary>
        /// Execute an SQL statement passed to the Method
        /// nonquery  = update, insert, or delete...not a select statment.
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        private bool ExecuteSQLStatment(String SQL)
        {
            try
            {
                SqlCeCommand Command = new SqlCeCommand();
                //SqlCommand Command = new SqlCommand();
                Command.Connection = _conn;
                Command.CommandType = CommandType.Text;
                Command.CommandText = SQL;
                Command.Connection.Open();
                Command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                _extendedErrorInformation = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Find  data and load it into a row for each variable attached to a DB column
        /// </summary>
        /// <param name="row"></param>
        private void LoadData(DataRow row)
        {
            //load user ID
            try 
            { 
                _userID = (row["user_id"] is DBNull) ? 0 : long.Parse(row["user_id"].ToString()); 
            }
            catch 
            { 
                _userID = 0; 
            }
            //load user Name
            try 
            { 
                _user_name = (row["user_name"] is DBNull) ? "" : row["user_name"].ToString(); 
            }
            catch 
            { 
                _user_name = ""; 
            }
            //load Password
            try 
            { 
                _password = (row["password"] is DBNull) ? "" : row["password"].ToString(); 
            }
            catch 
            { 
                _password = ""; 
            }
            //load isAdmin 
            try 
            { 
                _isAdmin = (row["isAdmin"] is DBNull) ? false : bool.Parse(row["isAdmin"].ToString()); 
            }
            catch 
            { 
                _isAdmin = false; 
            }
        }
        #endregion
        #region Public Functions

        /// <summary>
        /// Test database connection helper function
        /// </summary>
        /// <returns></returns>
        public bool TestConnection()
        {
            try
            {
                _conn = new SqlCeConnection(_connectionString);
                //_conn = new SqlConnection(_connectionString);
                _conn.Open();
                _conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                _extendedErrorInformation = ex.Message; return false;
            }
        }

        /// <summary>
        /// Return a dataset of all data in database
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllUsers()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM users", _conn);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM users", _conn);
                _conn.Open();
                da.Fill(ds, "users");
                return ds;
            }
            catch (Exception ex)
            {
                _extendedErrorInformation = ex.Message; return null;
            }
        }

        /// <summary>
        /// Delete product from database via product id
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool Delete(long UsersID)
        {
            try
            {
                String SQL = "DELETE FROM users WHERE user_id = " + UsersID;
                return this.ExecuteSQLStatment(SQL);
            }
            catch (Exception ex)
            {
                _extendedErrorInformation = ex.Message; 
                return false;
            }
        }

        /// <summary>
        /// Add userRoles to database
        /// </summary>
        /// <returns></returns>
        public bool Add()
        {
            try
            {
                String SQL;
                SQL = "INSERT INTO users ("
                + " user_name, password, isAdmin) VALUES ('"
                + _user_name + "', '"
                + _password + "', '"
                + _isAdmin + "')";

                return ExecuteSQLStatment(SQL);
            }
            catch (Exception ex)
            {
                _extendedErrorInformation = ex.Message; 
                return false;
            }
        }

        /// <summary>
        /// Update/Edit rows in database
        /// </summary>
        /// <returns></returns>
        public bool Edit()
        {
            try
            {
                String SQL;
                if (_userID == 0)
                    throw new Exception("Nothing to Update!");

                SQL = "UPDATE users SET "
                     + "user_name = '" + _user_name + "', "
                     + "password = '" + _password + "', "
                     + "isAdmin = '" + _isAdmin
                     + "' WHERE user_id = " + _userID;

                return ExecuteSQLStatment(SQL);
            }
            catch (Exception ex)
            {
                _extendedErrorInformation = ex.Message; return false;
            }
        }

        /// <summary>
        /// Find First Role in database based on user_id
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool FindFirstByID(long RoleID)
        {
            try
            {
                _conn.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM users WHERE (user_id = " + RoleID + ")", _conn);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM users WHERE (user_id = " + RoleID + ")", _conn);
                DataSet ds = new DataSet();
                DataRow row;
                da.Fill(ds, "users");
                row = ds.Tables["users"].Rows[0];

                if (row != null)
                {
                    LoadData(row);
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex) { _extendedErrorInformation = ex.Message; return false; }
            finally { _conn.Close(); }
        }
        #endregion

        #region Validation Method

        /// <summary>
        /// Authentication and authoriation acceptance for provided credientials 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPassword"></param>
        /// <returns></returns>
        public bool UserValidation(String UserName, String UserPassword)
        { 
            //First check if userName exists
            //Second check if userName has password
            //Return True or False
            try
            {
                _conn.Open();
                SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM users WHERE (user_name = '" + UserName + "' AND password ='"+ UserPassword +"')", _conn); 
                DataSet ds = new DataSet();
                DataRow row;

                //fill Dataset 
                da.Fill(ds, "users");
                row = ds.Tables["users"].Rows[0];


                //Check if password matches Submitted UserName
                if (row != null)
                {
                    LoadData(row);
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex) { _extendedErrorInformation = ex.Message; return false; }
            finally { _conn.Close(); }
        }
        #endregion

    }
}