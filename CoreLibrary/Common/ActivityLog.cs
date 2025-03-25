using System;

namespace CoreLibrary
{
    public static class ActivityLog
    {
        public static void SaveProcess(Enum eActionType)
        {
            Cookie UserInfo = new Cookie();
            String strResult = String.Empty;
            String strAction = String.Empty;
            String strProcedureName = String.Empty;

            try
            {
                switch (eActionType.ToString())
                {
                    case "Login":
                        strAction = "Login";
                        strProcedureName = "SP_USER_LOGIN";
                        strResult = UserInfo.UserName + " has been logged In using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    case "Logout":
                        strAction = "LogOut";
                        strResult = UserInfo.UserName + " has been logged Out from " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    default:
                        strResult = "";
                        strAction = "";
                        break;
                }

                DataWriter _writer = new DataWriter();
                String InsertCommand = "INSERT INTO AuditLog (UserName, LoginID, ProcedureName, ActionName, ActionType, ActionDetails) " +
                    "VALUES ('" + UserInfo.UserName + "','" + UserInfo.LoginID + "','" + strProcedureName + "','" + strAction + "','" + strAction + "','" + strResult + "')";
                _writer.ExecuteNonQuery(InsertCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveProcess(Enum eActionType, String sPageName)
        {
            Cookie UserInfo = new Cookie();
            String strResult = String.Empty;
            String strAction = String.Empty;

            try
            {
                switch (eActionType.ToString())
                {
                    case "Access":
                        strAction = "Access";
                        strResult = "Successful access to " + sPageName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    case "AccessDenied":
                        strAction = "Access Denied";
                        strResult = "Try to unauthorized access to " + sPageName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    default:
                        strResult = "";
                        strAction = "";
                        break;
                }

                DataWriter _writer = new DataWriter();
                String InsertCommand = "INSERT INTO AuditLog (UserName, LoginID, ActionType, ActionDetails) " +
                    "VALUES ('" + UserInfo.UserName + "', '" + UserInfo.LoginID + "', '" + strAction + "', '" + strResult + "')";
                _writer.ExecuteNonQuery(InsertCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveProcess(Enum eActionType, String sProcedureName, String sActionName, String sPrimaryData)
        {
            Cookie UserInfo = new Cookie();
            String strResult = String.Empty;
            String strAction = String.Empty;

            try
            {
                switch (eActionType.ToString())
                {
                    case "Insert":
                        strAction = "Insert";
                        strResult = "Inserted record " + sPrimaryData + " by execute " + sProcedureName + " store procedure with action " + sActionName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    case "Update":
                        strAction = "Update";
                        strResult = "Updated record " + sPrimaryData + "  by execute " + sProcedureName + " store procedure with action " + sActionName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    case "Delete":
                        strAction = "Delete";
                        strResult = "Deleted record " + sPrimaryData + "  by execute " + sProcedureName + " store procedure with action " + sActionName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    case "Active":
                        strAction = "Active";
                        strResult = "Actived record " + sPrimaryData + " by execute " + sProcedureName + " store procedure with action " + sActionName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    case "Inactive":
                        strAction = "Inactive";
                        strResult = "Inactived record " + sPrimaryData + " by execute " + sProcedureName + " store procedure with action " + sActionName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    case "Approved":
                        strAction = "Approved";
                        strResult = "Approved record " + sPrimaryData + " by execute " + sProcedureName + " store procedure with action " + sActionName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    case "Disapproved":
                        strAction = "Disapproved";
                        strResult = "Disapproved record " + sPrimaryData + " by execute " + sProcedureName + " store procedure with action " + sActionName + " using " + UtilityClass.GetComputerName() + " computer with IP address " + UtilityClass.GetIPAddress();
                        break;
                    default:
                        strResult = "";
                        strAction = "";
                        break;
                }

                DataWriter _writer = new DataWriter();
                String InsertCommand = "INSERT INTO AuditLog (UserName, LoginID, ProcedureName, ActionName, PrimaryID, ActionType, ActionDetails) " +
                    "VALUES ('" + UserInfo.UserName + "', '" + UserInfo.LoginID + "', '" + sProcedureName + "',  '" + sActionName + "','" + sPrimaryData + "', '" + strAction + "', '" + strResult + "')";
                _writer.ExecuteNonQuery(InsertCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public enum ActionType { Insert, Update, Delete, Active, Inactive, Access, AccessDenied, Login, Logout, Approved, Disapproved }
}