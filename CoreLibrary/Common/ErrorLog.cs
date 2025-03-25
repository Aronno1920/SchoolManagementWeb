using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CoreLibrary
{
    public class ErrorTracking
    {
        public static void SaveError(String sMethodName, Exception ex)
        {
            DataWriter _writer = new DataWriter();
            String sQuery = "INSERT INTO ErrorLogger(ComputerName,ComputerIPAddress,PageName,MethodName,ErrorSource,ErrorMessage,ErrorDate)" +
            " VALUES('" + UtilityClass.GetComputerName() + "','" + UtilityClass.GetIPAddress() + "','PopulateLists','" + sMethodName + "','" + ex.Source.Replace("'", "\"") + "','" + ex.Message.Replace("'", "\"") + "',GETDATE())";

            _writer.ExecuteNonQuery(sQuery);
        }

        public static void SaveError(String sUserID, String sPageName, String sMethodName, Exception ex)
        {
            DataWriter _writer = new DataWriter();
            String sQuery = "INSERT INTO ErrorLogger(UserID,ComputerName,ComputerIPAddress,PageName,MethodName,ErrorSource,ErrorMessage,ErrorDate)" +
            " VALUES('" + sUserID + "','" + UtilityClass.GetComputerName() + "','" + UtilityClass.GetIPAddress() + "','" + sPageName + "','" + sMethodName + "','" + ex.Source.Replace("'", "\"") + "','" + ex.Message.Replace("'", "\"") + "',GETDATE())";

            _writer.ExecuteNonQuery(sQuery);
        }
    }
}
