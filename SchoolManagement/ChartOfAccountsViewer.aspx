<%@ Page Title="Chart Of Accounts Viewer | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChartOfAccountsViewer.aspx.cs" Inherits="SchoolManagement.ChartOfAccountsViewer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">

    <style type="text/css">
        /* custom tree styles */
        .custom-tree {
            font-family: Verdana, Geneva, Tahoma, sans-serif;
        }

            /* default nodes */
            .custom-tree .wj-node {
            }

            /* level 0 and deeper nodes */
            .custom-tree .wj-nodelist > .wj-node {
                font-weight: bold;
                color: #80044d;
            }

            /* level 1 and deeper nodes (smaller font, vertical line along the left) */
            .custom-tree .wj-nodelist > .wj-nodelist > .wj-node,
            .custom-tree .wj-nodelist > .wj-nodelist > .wj-nodelist {
                font-weight: normal;
                border-left: 4px solid rgba(128, 4, 77, 0.3);
            }

                /* level 2 and deeper nodes (smaller font, thinner border) */
                .custom-tree .wj-nodelist > .wj-nodelist > .wj-nodelist > .wj-node,
                .custom-tree .wj-nodelist > .wj-nodelist > .wj-nodelist > .wj-nodelist {
                    font-style: italic;
                    opacity: 0.8;
                    border-left: 2px solid rgba(128, 4, 77, 0.3);
                }

            /* expanded node glyph */
            .custom-tree .wj-nodelist .wj-node:before {
                content: "\e114";
                font-family: Verdana, Geneva, Tahoma, sans-serif;
                top: 4px;
                border: none;
                opacity: .3;
                transition: all .3s cubic-bezier(.4,0,.2,1);
            }

            /* collapsed node glyph */
            .custom-tree .wj-nodelist .wj-node.wj-state-collapsed:before,
            .custom-tree .wj-nodelist .wj-node.wj-state-collapsing:before {
                transform: rotate(-180deg);
                transition: all .3s cubic-bezier(.4,0,.2,1);
            }

            /* selected node */
            .custom-tree .wj-node.wj-state-selected {
                color: white;
                background: rgba(128, 4, 77, 0.70);
            }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divAccountViewerPanel">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle"></i>Chart Of Accounts - Viewer</header>
                        </div>
                        <div class="card-body style-default-bright">
                            <asp:TreeView ID="tvChartOfAccount" runat="server" CssClass="custom-tree" ExpandDepth="0" ShowLines="true">
                            </asp:TreeView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
