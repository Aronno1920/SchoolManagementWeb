using CoreLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolManagement
{
    public partial class ChartOfAccountsViewer : System.Web.UI.Page
    {
        Cookie user = new Cookie();
        DataReader _reader = new DataReader();
        String sPageName = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Check User Login Status and Menu Permission
            if (String.IsNullOrEmpty(user.GetCookie(CookieKey.UserID.ToString())) || user.GetCookie(CookieKey.UserID.ToString()) == "0")
            {
                Response.Redirect(String.Format("~/Login.aspx"), false);
            }
            try
            {
                DataTable dtMenu = (DataTable)Session["Menu"];
                sPageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                bool isPermitted = dtMenu.AsEnumerable().Any(row => sPageName == row.Field<String>("TargetUrl"));
                if (!isPermitted)
                {
                    ActivityLog.SaveProcess(ActionType.AccessDenied, sPageName);
                    Response.Redirect(String.Format("~/UnauthorizedPage.aspx"), false);
                }
            }
            catch
            {
                Response.Redirect(String.Format("~/Login.aspx"), false);
            }
            #endregion

            if (!IsPostBack)
            {
                divAccountViewerPanel.Attributes.Add("class", user.ApplicationTheme.ToString().Trim());
                ActivityLog.SaveProcess(ActionType.Access, sPageName);

                LoadTreeView();
            }
        }

        public void LoadTreeView()
        {
            _reader = new DataReader();
            DataTable dtParent = _reader.GetDataTableByCommand("SELECT AccountID, AccountName +' [' + Code + ']' as AccountName,ParentID FROM AccountInfo ORDER BY Serial ASC");
            this.PopulateTreeView(dtParent, 0, null);
        }

        public void PopulateTreeView(DataTable dtParent, int ParentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Select("ParentId=0"))
            {
                TreeNode _node = new TreeNode
                {
                    Text = row["AccountName"].ToString(),
                    Value = row["AccountID"].ToString()
                };

                tvChartOfAccount.Nodes.Add(_node);
                GetChildMenus(_node.Value, dtParent, _node);
            }
        }

        private void GetChildMenus(String parentID, DataTable dtMenu, TreeNode treeNode)
        {
            DataRow[] childRows = dtMenu.Select("ParentId=" + parentID);

            if (childRows.Length > 0)
            {
                foreach (DataRow crow in childRows)
                {
                    TreeNode _subnode = new TreeNode
                    {
                        Text = crow["AccountName"].ToString(),
                        Value = crow["AccountID"].ToString()
                    };

                    treeNode.ChildNodes.Add(_subnode);

                    GetChildMenus(_subnode.Value, dtMenu, _subnode);
                }
            }
        }
    }
}