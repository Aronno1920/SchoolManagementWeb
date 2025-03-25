using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace CoreLibrary
{
    public static class FillList
    {
        public static void PopulateDropDownList(DataTable DropDownListData, DropDownList DropDownListId, String TextField, String ValueField)
        {
            DataTable dtDropDown = new DataTable();
            dtDropDown.Columns.Add(new DataColumn("ItemValue", typeof(string)));
            dtDropDown.Columns.Add(new DataColumn("ItemText", typeof(string)));

            foreach (DataRow myRow in DropDownListData.Rows)
            {
                DataRow dRow = dtDropDown.NewRow();
                dRow["ItemValue"] = myRow[ValueField];
                dRow["ItemText"] = myRow[TextField];
                dtDropDown.Rows.Add(dRow);
            }
            DropDownListId.DataSource = dtDropDown;
            DropDownListId.DataTextField = "itemText";
            DropDownListId.DataValueField = "itemValue";
            DropDownListId.DataBind();
        }

        public static void PopulateDropDownList(DataTable DropDownListData, DropDownList DropDownListId, String TextField, String ValueField,  Boolean IsExtraTop, String TopText, String TopValue)
        {
            DataTable dtDropDown = new DataTable();
            dtDropDown.Columns.Add(new DataColumn("ItemValue", typeof(string)));
            dtDropDown.Columns.Add(new DataColumn("ItemText", typeof(string)));

            if (IsExtraTop == true)
            {
                DataRow dRow = dtDropDown.NewRow();
                try
                {
                    dRow["ItemValue"] = TopValue;
                }
                catch (Exception)
                {
                    dRow["ItemValue"] = "-1";
                }

                try
                {
                    dRow["itemText"] = TopText;
                }
                catch (Exception)
                {
                    dRow["itemText"] = "";
                }
                dtDropDown.Rows.Add(dRow);
            }

            // add list contents from the Dataset
            foreach (DataRow myRow in DropDownListData.Rows)
            {
                DataRow dRow = dtDropDown.NewRow();
                dRow["ItemValue"] = myRow[ValueField];
                dRow["ItemText"] = myRow[TextField];
                dtDropDown.Rows.Add(dRow);
            }
            DropDownListId.DataSource = dtDropDown;
            DropDownListId.DataTextField = "itemText";
            DropDownListId.DataValueField = "itemValue";
            DropDownListId.DataBind();
        }
    }
}
