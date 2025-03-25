using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace CoreLibrary
{
    public static class MenuDAO
    {
        public static String GetTopLevelMenuHTML(DataTable dtTopMenu, String sHomaPage)
        {
            StringBuilder sbHtml = new StringBuilder("<ul>");

            foreach (DataRow topRow in dtTopMenu.Select("ParentId=1920"))
            {
                sbHtml.Append("<li><a href=\"" + GetFullRootUrl(topRow["TargetUrl"].ToString()) + "\" class=\"active\">" + topRow["MenuName"].ToString() + "</a></li>");
            }

            foreach (DataRow topRow in dtTopMenu.Select("ParentId=0"))
            {
                if (topRow["IsOnlyParent"].ToString() == "True")
                {
                    sbHtml.Append("<li><a href=\"" + GetFullRootUrl(topRow["TargetUrl"].ToString()) + "\">" + topRow["MenuName"].ToString() + "</a></li>");
                }
                else
                {
                    sbHtml.Append("<li>");
                    sbHtml.Append("<a href=\"" + (GetFullRootUrl(topRow["TargetUrl"].ToString()) == "null" ? "#" : GetFullRootUrl(topRow["TargetUrl"].ToString())) + "\">" + topRow["MenuName"].ToString() + " <i class=\"fa fa-caret-down\"></i></a>");
                    sbHtml.Append(GetChildMenus(topRow["id"].ToString(), dtTopMenu));
                    sbHtml.Append("</li>");
                }
            }
            sbHtml.Append("</ul>");

            String a = sbHtml.ToString();

            return sbHtml.ToString();
        }

        private static String GetChildMenus(String parentID, DataTable dtMenu)
        {
            DataRow[] childRows = dtMenu.Select("ParentId=" + parentID);
            if (childRows.Length == 0)
                return "";

            StringBuilder subbldr = new StringBuilder("<ul>");
            foreach (DataRow crow in childRows)
            {
                DataRow[] childSubRows = dtMenu.Select("ParentId=" + crow["id"].ToString());
                if (childSubRows.Length == 0)
                {
                    subbldr.Append("<li><a href=\"" + GetFullRootUrl(crow["TargetUrl"].ToString()) + "\">" + crow["MenuName"].ToString() + "</a></li>");
                }
                else
                {
                    subbldr.Append("<li><a href=\"" + (GetFullRootUrl(crow["TargetUrl"].ToString()) == "null" ? "#" : GetFullRootUrl(crow["TargetUrl"].ToString())) + "\">" + crow["MenuName"].ToString() + " <i class=\"fa fa-caret-right\"></i></a>");
                    subbldr.Append(GetChildSubMenus(crow["id"].ToString(), dtMenu));
                    subbldr.Append("</li>");
                }
            }
            subbldr.Append("</ul>");
            return subbldr.ToString();
        }

        private static String GetChildSubMenus(String parentID, DataTable dtMenu)
        {
            DataRow[] childRows = dtMenu.Select("ParentId=" + parentID);
            if (childRows.Length == 0)
                return "";

            StringBuilder minibldr = new StringBuilder("<ul class=\"miniMenu\">");
            foreach (DataRow crow in childRows)
            {
                minibldr.Append("<li><a href=\"" + GetFullRootUrl(crow["TargetUrl"].ToString()) + "\">" + crow["MenuName"].ToString() + "</a></li>");
            }
            minibldr.Append("</ul>");
            return minibldr.ToString();
        }

        public static String GetFullRootUrl(String pagePath)
        {
            HttpRequest request = HttpContext.Current.Request;
            if (pagePath != "#")
                return request.Url.AbsoluteUri.Replace(request.Url.AbsolutePath, "/" + pagePath);
            else
                return request.Url.AbsoluteUri.Replace(request.Url.AbsolutePath, request.Url.AbsolutePath);
        }
    }
}