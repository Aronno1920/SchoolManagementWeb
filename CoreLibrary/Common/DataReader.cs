using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CoreLibrary
{
    public class DataReader
    {
        #region Initilization Component
        private SqlConnection AppConn = null;
        #endregion

        #region Get Scalar from Database executing Query or Store Procudure
        public object ExecuteScalar(String strSQL)
        {
            OpenAppConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, AppConn);

                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins

                return cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                AppConn.Close();
            }
        }
        public object ExecuteScalar(String strSQL, SqlConnection Conn)
        {

            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, Conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                return cmd.ExecuteScalar();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Get Single Value from Database executing Query or Store Procudure
        public String GetValueByCommand(String strSQL)
        {
            OpenAppConnection();
            String sResult = String.Empty;
            try
            {
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSQL, AppConn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                AppConn.Close();
            }
            return sResult;
        }
        public String GetValueByStoreProcudure(String spName, Hashtable parameters)
        {
            OpenAppConnection();
            String sResult = String.Empty;
            try
            {
                //SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSQL, AppConn);
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandTimeout = 300;// 5 mins
                //DataSet ds = new DataSet();
                //SqlDataAdapter adp = new SqlDataAdapter();
                //adp.SelectCommand = cmd;
                //adp.Fill(ds);


                SqlCommand cmd = new SqlCommand(spName, AppConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;// 5 mins
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (string parametername in parameters.Keys)
                    {
                        cmd.Parameters.AddWithValue(parametername, parameters[parametername]);
                    }
                }//if(parameters!= null && parameters.Count>0)

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    sResult = dt.Rows[0][0].ToString();
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                AppConn.Close();
            }
            return sResult;
        }
        public Int32 HasRows(String strQueryString)
        {
            DataTable objTable = new DataTable();
            objTable = GetDataTableByCommand(strQueryString);
            return objTable.Rows.Count;
        }
        public long GetMaxNo(String strTable, String strField, String strWhere)
        {
            OpenAppConnection();

            try
            {
                SqlCommand CmdMax = new SqlCommand(string.Format("SELECT ISNULL(MAX({0}),0) FROM {1} WHERE {2}", strField, strTable, strWhere), AppConn);
                long lngMaxno = Convert.ToInt64(CmdMax.ExecuteScalar());
                return lngMaxno;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseAppConnection();

            }

        }
        public long GetMaxNo(String tableName, String columnName)
        {
            OpenAppConnection();

            try
            {

                SqlCommand CmdMax = new SqlCommand(String.Format("SELECT ISNULL(MAX({0}),0) FROM {1}", columnName, tableName), AppConn);
                long lngMaxno = Convert.ToInt64(CmdMax.ExecuteScalar());

                return lngMaxno;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseAppConnection();

            }
        }
        public long GetMaxNo(String tableName, String columnName, SqlConnection Conn)
        {
            try
            {

                SqlCommand CmdMax = new SqlCommand(String.Format("SELECT ISNULL(MAX({0}),0) FROM {1}", columnName, tableName), Conn);
                long lngMaxno = Convert.ToInt64(CmdMax.ExecuteScalar());

                return lngMaxno;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Get SQL Data Reader from Database executing Query or Store Procudure
        public SqlDataReader GetDataReaderByCommand(string strSQL)
        {
            OpenAppConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, AppConn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                SqlDataReader dReader = cmd.ExecuteReader();
                return dReader;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                AppConn.Close();
            }
        }
        public SqlDataReader GetDataReaderByCommand(string strSQL, SqlConnection Conn)
        {

            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, Conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                SqlDataReader dReader = cmd.ExecuteReader();
                return dReader;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Get DataTable from Database executing Query or Store Procudure
        public DataTable GetDataTableByCommand(String strSQL)
        {
            OpenAppConnection();

            try
            {
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSQL, AppConn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                AppConn.Close();
            }
        }
        public DataTable GetDataTableByCommand(String tableName, String strFilter)
        {
            OpenAppConnection();

            try
            {
                string strSQL = "SELECT * FROM " + tableName + " WHERE " + strFilter;
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSQL, AppConn);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                AppConn.Close();
            }
        }
        public DataTable GetDataTableByCommand(String strSQL, SqlConnection Conn)
        {
            try
            {

                SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSQL, Conn);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        public DataTable GetDataTableByCommand(String tableName, String strFilter, SqlConnection Conn)
        {
            try
            {
                string strSQL = "SELECT * FROM " + tableName + " WHERE " + strFilter;
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSQL, Conn);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        public DataTable GetDataTableByStoredProcedure(String StoreProcedureName, Boolean IsParam = false)
        {
            OpenAppConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(StoreProcedureName, AppConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;// 5 mins

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseAppConnection();
            }
        }
        public DataTable GetDataTableByStoredProcedure(String spName, Hashtable parameters)
        {
            OpenAppConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(spName, AppConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;// 5 mins

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (string parametername in parameters.Keys)
                    {
                        cmd.Parameters.AddWithValue(parametername, parameters[parametername]);
                    }
                }

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                return dt;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                CloseAppConnection();
            }
        }
        public DataTable GetDataTableByStoredProcedure(String spName, Hashtable parameters, SqlConnection Conn)
        {

            try
            {
                SqlCommand cmd = new SqlCommand(spName, Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;// 5 mins
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (string parametername in parameters.Keys)
                    {
                        cmd.Parameters.AddWithValue(parametername, parameters[parametername]);
                    }
                }//if(parameters!= null && parameters.Count>0)

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


        }
        #endregion

        #region Get DataSet from Database executing Query or Store Procudure
        public DataSet GetDataSetByCommand(String strSQL)
        {
            OpenAppConnection();

            try
            {
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strSQL, AppConn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = 300;// 5 mins
                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                AppConn.Close();
            }
        }
        public DataSet GetDataSetByStoredProcedure(String spName, Hashtable parameters)
        {
            OpenAppConnection();

            try
            {
                SqlCommand cmd = new SqlCommand(spName, AppConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;// 5 mins
                if (parameters != null && parameters.Count > 0)
                {
                    foreach (string parametername in parameters.Keys)
                    {
                        //cmd.Parameters.AddWithValue(parametername, parameters[parametername]);
                        SqlParameter param = new SqlParameter("@" + parametername, parameters[parametername]);
                        cmd.Parameters.Add(param);
                    }
                }

                DataSet ds = new DataSet();
                SqlDataAdapter adp = new SqlDataAdapter();
                adp.SelectCommand = cmd;
                adp.Fill(ds);
                return ds;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally { CloseAppConnection(); }

        }
        #endregion

        #region Get Database Connection
        public SqlConnection GetConnection(string connectionString)
        {
            if (!connectionString.Equals("") && connectionString != null)
            {
                SqlConnection Conn = new SqlConnection(connectionString);
                return Conn;
            }
            return null;
        }
        public static SqlConnection GetProjectConnection(string strServer, string strDB, String strUser, String strPassword)
        {
            SqlConnection ConnAccounts = new SqlConnection();
            ConnAccounts.ConnectionString = "Data Source=" + strServer + ";Initial Catalog=" + strDB + ";User ID=" + strUser + ";Password=" + strPassword + "";
            ConnAccounts.Open();
            return ConnAccounts;
        }
        #endregion

        #region Database Connection Open and Close 
        public static String AppConnString
        {
            get
            {
                string strConnectionString = "";
                if (ConfigurationManager.ConnectionStrings["ConString"] != null)
                {
                    return ConfigurationManager.ConnectionStrings["ConString"].ToString();
                }
                return strConnectionString;
            }
        }
        private void OpenAppConnection()
        {
            string ConnectionString = AppConnString;

            if (!ConnectionString.Equals(""))
            {
                AppConn = new SqlConnection(ConnectionString);

                if (AppConn.State != ConnectionState.Open && UtilityClass.CheckValidation(AppConn.Database))
                {
                    AppConn.Open();
                }
            }
        }
        private void CloseAppConnection()
        {
            if (AppConn.State == ConnectionState.Open)
            {
                AppConn.Close();

            }
        }
        public void CloseConnection(SqlConnection Conn)
        {

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();

            }
        }
        #endregion
    }
}