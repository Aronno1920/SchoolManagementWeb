<%@ Page Title="Process Flow Diagram | SMS" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProcessFlow.aspx.cs" Inherits="SchoolManagement.ProcessFlow" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:UpdatePanel runat="server" ID="updatePanel">
        <ContentTemplate>
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div runat="server" id="divAcademicSetup">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle">Step 1</i></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                                <div class="col-sm-3">
                                    <div class="module red"></div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="module yellow"></div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="module blue"></div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="module green"></div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divBasicSetup">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle">Step 2</i></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divUserSetup">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle">Step 2</i></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divCandidateSetup">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle">Step 3</i></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divStudentSetup">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle">Step 3</i></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divAccountSetup">
                        <div class="card-head">
                            <header><i style="padding-left: 3px; padding-right: 6px" class="fa fa-plus-circle">Step 3</i></header>
                        </div>
                        <div class="card-body style-default-bright">
                            <div class="col-sm-12">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
