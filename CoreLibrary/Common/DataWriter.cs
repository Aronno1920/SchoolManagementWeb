using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoreLibrary
{
    public class DataWriter
    {
        #region Initilization Component
        private SqlTransaction Trans;
        private SqlConnection AppConn = new SqlConnection(AppConnString);
        private SqlCommand Cmnd;
        #endregion

        #region Insert data using SQL Command and SQL Connection
        public int InsertByCommand(Hashtable hTable, String sourceTableName)
        {
            OpenAppConnection();
            try
            {
                if (Trans == null)
                {
                    Cmnd = new SqlCommand("SELECT * FROM  " + sourceTableName + " WHERE 1 <> 1", AppConn);
                }
                else
                {
                    Cmnd.CommandText = "SELECT * FROM  " + sourceTableName + " WHERE 1 <> 1";
                }
                SqlDataAdapter ADP = new SqlDataAdapter(Cmnd);
                DataSet DS = new DataSet();
                ADP.Fill(DS, sourceTableName);

                DataRow DR_ADDROW = DS.Tables[0].NewRow();

                foreach (object OBJ in hTable.Keys)
                {
                    string COLUMN_NAME = Convert.ToString(OBJ);
                    DR_ADDROW[COLUMN_NAME] = hTable[OBJ];
                }

                DS.Tables[0].Rows.Add(DR_ADDROW);

                SqlCommandBuilder BLD = new SqlCommandBuilder(ADP);
                ADP.InsertCommand = BLD.GetInsertCommand();
                return ADP.Update(DS, sourceTableName);

            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;

            }
            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;

            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }

            }
        }
        public int InsertByCommand(Hashtable hTable, String sourceTableName, SqlConnection Conn)
        {
            try
            {
                Cmnd = new SqlCommand("SELECT * FROM  " + sourceTableName + " WHERE 1 <> 1", Conn);
                SqlDataAdapter ADP = new SqlDataAdapter(Cmnd);
                DataSet DS = new DataSet();
                ADP.Fill(DS, sourceTableName);

                DataRow DR_ADDROW = DS.Tables[0].NewRow();

                foreach (object OBJ in hTable.Keys)
                {
                    string COLUMN_NAME = Convert.ToString(OBJ);
                    DR_ADDROW[COLUMN_NAME] = hTable[OBJ];
                }

                DS.Tables[0].Rows.Add(DR_ADDROW);

                SqlCommandBuilder BLD = new SqlCommandBuilder(ADP);
                ADP.InsertCommand = BLD.GetInsertCommand();
                return ADP.Update(DS, sourceTableName);

            }
            catch (SqlException Ex)
            {
                throw Ex;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Update data using SQL Command and SQL Connection
        public short UpdateByCommand(Hashtable HTable, String SourceTableName, String Filter)
        {
            OpenAppConnection();
            try
            {

                if (Trans == null)
                {
                    Cmnd = new SqlCommand("SELECT * FROM " + SourceTableName + " WHERE " + Filter, AppConn);
                }
                else
                {
                    Cmnd.CommandText = "SELECT * FROM " + SourceTableName + " WHERE " + Filter;
                }


                SqlDataAdapter ADP = new SqlDataAdapter(Cmnd);
                DataSet DS = new DataSet();
                ADP.Fill(DS, SourceTableName);
                int rowNumber = 0;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    DataRow DR_UPDATE = DS.Tables[0].Rows[rowNumber];
                    foreach (object OBJ in HTable.Keys)
                    {
                        string COLUMN_NAME = Convert.ToString(OBJ);
                        DR_UPDATE[COLUMN_NAME] = HTable[OBJ];
                    }
                    SqlCommandBuilder BLD = new SqlCommandBuilder(ADP);
                    ADP.UpdateCommand = BLD.GetUpdateCommand();
                    ADP.Update(DS, SourceTableName);
                    rowNumber++;

                }

                return 1;

            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }

                throw Ex;
            }

            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }

                throw Ex;
            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }

            }
        }
        public short UpdateByCommand(Hashtable HTable, String SourceTableName, String Filter, SqlConnection Conn)
        {
            try
            {

                Cmnd = new SqlCommand("SELECT * FROM " + SourceTableName + " WHERE " + Filter, Conn);
                SqlDataAdapter ADP = new SqlDataAdapter(Cmnd);
                DataSet DS = new DataSet();
                ADP.Fill(DS, SourceTableName);
                int rowNumber = 0;
                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    DataRow DR_UPDATE = DS.Tables[0].Rows[rowNumber];
                    foreach (object OBJ in HTable.Keys)
                    {
                        string COLUMN_NAME = Convert.ToString(OBJ);
                        DR_UPDATE[COLUMN_NAME] = HTable[OBJ];
                    }
                    SqlCommandBuilder BLD = new SqlCommandBuilder(ADP);
                    ADP.UpdateCommand = BLD.GetUpdateCommand();
                    ADP.Update(DS, SourceTableName);
                    rowNumber++;

                }

                return 1;

            }
            catch (SqlException Ex)
            {
                throw Ex;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Delete data using SQL Command and SQL Connection
        public int DeleteByCommand(String SourceTableName, String Filter)
        {
            OpenAppConnection();
            try
            {

                if (Trans == null)
                {
                    Cmnd = new SqlCommand("DELETE FROM " + SourceTableName + " WHERE " + Filter, AppConn);
                }
                else
                {
                    Cmnd.CommandText = "DELETE FROM " + SourceTableName + " WHERE " + Filter;
                }

                int affectedRow = Cmnd.ExecuteNonQuery();
                return affectedRow;
            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }

                throw Ex;

            }
            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }

                throw Ex;

            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }

            }
        }
        public int DeleteByCommand(String SourceTableName, String Filter, SqlConnection Conn)
        {
            try
            {
                Cmnd = new SqlCommand("DELETE FROM " + SourceTableName + " WHERE " + Filter, Conn);

                int affectedRow = Cmnd.ExecuteNonQuery();
                return affectedRow;
            }
            catch (SqlException Ex)
            {
                throw Ex;

            }
            catch (Exception Ex)
            {
                throw Ex;

            }
        }
        #endregion

        #region Modification data using SQL Command and SQL Connection
        public int ExecuteNonQuery(String strSQL)
        {
            OpenAppConnection();
            try
            {
                if (Trans == null)
                {
                    Cmnd = new SqlCommand(strSQL, AppConn);
                }
                else
                {
                    Cmnd.CommandText = strSQL;
                }

                int affectedRow = Cmnd.ExecuteNonQuery();

                return affectedRow;
            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;
            }
            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;
            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }

            }
        }
        public int ExecuteNonQuery(String strSQL, SqlConnection Conn)
        {
            Conn.Open();
            try
            {

                Cmnd = new SqlCommand(strSQL, Conn);

                int affectedRow = Cmnd.ExecuteNonQuery();

                return affectedRow;
            }
            catch (SqlException Ex)
            {

                throw Ex;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            finally
            {
                if (Trans == null)
                {
                    Conn.Close();
                }

            }

        }
        public object ExecuteScalar(String strSQL)
        {
            OpenAppConnection();

            try
            {
                if (Trans == null)
                {
                    Cmnd = new SqlCommand(strSQL, AppConn);
                }
                else
                {
                    Cmnd.CommandText = strSQL;
                }

                Cmnd.CommandType = CommandType.Text;

                return Cmnd.ExecuteScalar();
            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;

            }
            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;

            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }

            }
        }
        public void ExecuteStoredProcedure(String storedProcedureName)
        {
            OpenAppConnection();
            try
            {
                if (Trans == null)
                {
                    Cmnd = new SqlCommand();
                    Cmnd.Connection = AppConn;
                }
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.CommandText = storedProcedureName;

                Cmnd.ExecuteNonQuery();
                Cmnd.Parameters.Clear();
                Cmnd.CommandType = CommandType.Text;
            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;
            }
            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }

                throw Ex;
            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }
            }
        }
        public void ExecuteStoredProcedure(String storedProcedureName, Hashtable parameters)
        {
            OpenAppConnection();
            try
            {
                if (Trans == null)
                {
                    Cmnd = new SqlCommand();
                    Cmnd.Connection = AppConn;
                }
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.CommandText = storedProcedureName;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (string parametername in parameters.Keys)
                    {
                        Cmnd.Parameters.AddWithValue(parametername, parameters[parametername]);
                    }
                }
                Cmnd.ExecuteNonQuery();
                Cmnd.Parameters.Clear();
                Cmnd.CommandType = CommandType.Text;
            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;
            }
            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }

                throw Ex;
            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }

            }

        }
        public long ExecuteStoredProcedure(String storedProcedureName, Hashtable parameters, String idFieldName)
        {
            OpenAppConnection();
            try
            {
                long retID = -1;
                if (Trans == null)
                {
                    Cmnd = new SqlCommand();
                    Cmnd.Connection = AppConn;
                }
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.CommandText = storedProcedureName;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (string parametername in parameters.Keys)
                    {
                        Cmnd.Parameters.AddWithValue(parametername, parameters[parametername]);
                    }
                }
                SqlParameter idParam = new SqlParameter("@" + idFieldName, 0);
                idParam.Direction = ParameterDirection.Output;
                Cmnd.Parameters.Add(idParam);

                Cmnd.ExecuteNonQuery();

                retID = (long)Cmnd.Parameters[Cmnd.Parameters.Count - 1].Value;

                Cmnd.Parameters.Clear();
                Cmnd.CommandType = CommandType.Text;

                return retID;
            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;
            }
            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }

                throw Ex;
            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }

            }
        }
        public String ExecuteStoredProcedureString(String storedProcedureName, Hashtable parameters, String idFieldName)
        {
            OpenAppConnection();
            try
            {
                String retID = String.Empty; ;
                if (Trans == null)
                {
                    Cmnd = new SqlCommand();
                    Cmnd.Connection = AppConn;
                }
                Cmnd.CommandType = CommandType.StoredProcedure;
                Cmnd.CommandText = storedProcedureName;

                if (parameters != null && parameters.Count > 0)
                {
                    foreach (string parametername in parameters.Keys)
                    {
                        Cmnd.Parameters.AddWithValue(parametername, parameters[parametername]);
                    }
                }
                SqlParameter idParam = new SqlParameter("@" + idFieldName, SqlDbType.Char, 150);
                idParam.Direction = ParameterDirection.Output;
                Cmnd.Parameters.Add(idParam);

                Cmnd.ExecuteNonQuery();

                retID = (String)Cmnd.Parameters["@" + idFieldName].Value;

                Cmnd.Parameters.Clear();
                Cmnd.CommandType = CommandType.Text;

                return retID.ToString();
            }
            catch (SqlException Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }
                throw Ex;
            }
            catch (Exception Ex)
            {
                if (Trans != null)
                {
                    Trans.Rollback();
                    Trans = null;
                }

                throw Ex;
            }
            finally
            {
                if (Trans == null)
                {
                    CloseAppConnection();
                }

            }

        }
        #endregion

        #region Database Connection and Transaction Related
        public void BeginTransaction()
        {
            OpenAppConnection();
            if (Trans == null)
            {
                Cmnd = AppConn.CreateCommand();
                Trans = AppConn.BeginTransaction();
                Cmnd.Transaction = Trans;

            }
        }
        public void CommitTransaction()
        {
            if (Trans != null)
            {
                Trans.Commit();
                Trans = null;
            }
            CloseAppConnection();
        }
        public void RollbackTransaction()
        {
            if (Trans != null)
            {
                Trans.Rollback();
                Trans = null;
            }
        }
        private void CloseAppConnection()
        {
            if (AppConn.State == ConnectionState.Open)
            {
                AppConn.Close();

            }
        }
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
