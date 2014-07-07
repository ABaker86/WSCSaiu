using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Web.Configuration;
using System.Data.SqlServerCe;

namespace AquaGear_ITSD425_Rebuild{
    public class ProductAccess
    {
        //connection variables
        private String _connectionString;
        private SqlCeConnection _conn;

        //Private Variables
        private long _productID;
        private String _productName;
        private String _productDesc;
        private decimal _productPrice;
        private String _extendedErrorInformation;

        #region Constructors

        /// <summary>
        /// Generic class Constructor
        /// </summary>
        public ProductAccess()
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
        public ProductAccess(String connString)
        {
            _connectionString = connString;
            _conn = new SqlCeConnection(_connectionString);
            ClearFields();
        }

        #endregion

        #region Properties

        public long ProductID
        {
            get { return _productID;}
            set { _productID = value; }
        }

        public String ProductName
        {
            get { return _productName; }
            set { _productName = value;}
        }

        public String ProductDescription
        {
            get { return _productDesc; }
            set { _productDesc = value; }
        }

        public decimal ProductPrice
        {
            get { return _productPrice; }
            set { _productPrice = value; }
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
            _productID = 0;
            _productName = "";
            _productDesc = "";
            _productPrice = 0;
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
                //SqlCeCommand Command = new SqlCeCommand(SQL);
                SqlCeCommand Command = new SqlCeCommand();
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
        /// Find row data and load it into this BL class
        /// </summary>
        /// <param name="row"></param>
        private void LoadData(DataRow row)
        {
            //load Product ID
            try   { _productID = (row["product_id"] is DBNull) ? 0 : long.Parse(row["product_id"].ToString());}
            catch { _productID = 0; }
            //load Product Name
            try { _productName = (row["product_name"] is DBNull) ? "" : row["product_name"].ToString(); }
            catch { _productName = ""; }
            //load Product Description
            try { _productDesc = (row["product_descritpion"] is DBNull) ? "" : row["product_descritpion"].ToString(); }
            catch { _productDesc = ""; }
            //load Product Price
            try { _productPrice = (row["product_price"] is DBNull) ? 0 : Convert.ToDecimal(row["product_price"]); }
            catch { _productPrice = 0; }
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
        public DataSet GetAllProducts()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM Products", _conn);
                _conn.Open();
                da.Fill(ds, "Products");
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
        public bool Delete(long productID)
        {
            try
            {
                String SQL = "DELETE FROM Products WHERE (product_id = " + productID + " )";
                return this.ExecuteSQLStatment(SQL);
            }
            catch (Exception ex)
            {
                _extendedErrorInformation = ex.Message; return false;
            }
        }

        /// <summary>
        /// Add product to database
        /// </summary>
        /// <returns></returns>
            public bool Add()
            {
                try
                {
                    String SQL;
                    //Query builder code...This should look like this: INSERT INTO Products(product_name, product_descritpion, product_price)VALUES        (N'Black Fish', N'A Black Fish', 3.33)
                    SQL = "INSERT INTO Products ("
                    + "product_name, product_descritpion, product_price) VALUES (N'"
                    +_productName+"', N'"
                    +_productDesc+"', "
                    +_productPrice+")";
                   
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
                    if (_productID == 0)
                        throw new Exception("Nothing to Update!");
                    //Query builder code...This should look like this: UPDATE Products SET product_name = N'Tan Fish', product_descritpion = N'A Tan Fish', product_price = 5.25 WHERE (product_id = 2)
                    SQL = "UPDATE Products SET product_name = N'" + _productName
                        + "', product_descritpion = N'" + _productDesc
                        + "', product_price = " + _productPrice
                        + " WHERE (product_id = " + _productID + ")";

                    return ExecuteSQLStatment(SQL);
                }
                catch (Exception ex)
                {
                    _extendedErrorInformation = ex.Message; return false;
                }
            }

        /// <summary>
        /// Find First item in database based on productID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
            public bool FindFirstByID(long productID)
            {
                try
                {
                    _conn.Open();
                    SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM Products WHERE (product_id =" + productID+ " )", _conn );
                    DataSet ds = new DataSet();
                    DataRow row;
                    da.Fill(ds, "Products");
                    row = ds.Tables["Products"].Rows[0];

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

        #region Service Methods
        /// <summary>
        /// This method takes a product name and returns 
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
            public string GetProductPrice(string pName)
            {
                try
                {
                    _conn.Open();
                    SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM Products WHERE (product_name = '" + pName + "')", _conn);
                    DataSet ds = new DataSet();
                    DataRow row;
                    da.Fill(ds, "Products");
                    row = ds.Tables["Products"].Rows[0];

                    if (row != null)
                    {
                        LoadData(row);
                        return _productPrice.ToString();
                    }
                    else { return "No Data!"; }
                }
                catch (Exception ex) { _extendedErrorInformation = ex.Message; return _extendedErrorInformation; }
                finally { _conn.Close(); }
            }

            /// <summary>
            /// Return a dataset of all data in database sorted by price
            /// </summary>
            /// <returns></returns>
            public DataSet SortAllProductsbyPrice(int order)
            {
                try
                {
                    string loadString = null;
                    switch (order){
                        case 1:
                            loadString = "SELECT * FROM Products ORDER BY product_price ASC";
                            break;
                        case 2:
                            loadString = "SELECT * FROM Products ORDER BY product_price DESC";
                            break;
                    }
                    DataSet ds = new DataSet();
                    SqlCeDataAdapter da = new SqlCeDataAdapter(loadString, _conn);
                    _conn.Open();
                    da.Fill(ds, "Products");
                    _conn.Close();
                    return ds;
                }
                catch (Exception ex)
                {
                    _extendedErrorInformation = ex.Message; return null;
                }
            }

        #endregion
    }
}