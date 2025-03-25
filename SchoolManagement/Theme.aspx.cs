using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class Theme : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        DataWriter _writer = new DataWriter();
        String sPageName = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            sPageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            divEntry.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
        }

        protected void ddlTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            divEntry.Attributes.Add("class", ddlTheme.SelectedValue.ToString());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                _writer = new DataWriter();
                String sQuery = "UPDATE EmployeeInfo SET UsedTheme='" + ddlTheme.SelectedValue.ToString() + "' WHERE EmployeeID='" + user.UserID.ToString() + "'";
                _writer.ExecuteNonQuery(sQuery);

                ActivityLog.SaveProcess(ActionType.Update, "InLine Query", "Udpate", user.UserID.ToString());
                DisplayMessage("Theme change successfully. Please login again");
            }
            catch (Exception ex)
            {
                ErrorTracking.SaveError(user.UserID, sPageName, "btnSave_Click", ex);
                DisplayMessage("Theme change failed. Please try again. \n" + ex.Message);
            }
        }

        #region Common Method for Display Message
        private void DisplayMessage(String sMessage)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('" + sMessage.ToString() + "');", true);
            return;
        }
        #endregion
    }
}