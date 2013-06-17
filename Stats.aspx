<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Stats.aspx.cs" Inherits="Stats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 195px;
        }
        .auto-style1 {
            width: 4px;
        }
    </style>
    <script src="http://code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.11.1/jquery.validate.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.datepicker.validation.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/redmond/jquery-ui.css" />
    <script type="text/javascript">


            jQuery(function ($)
            {
                $(function ()
                {
                    $("#MainContent_LoginView1_onDate").datepicker({
                        showOn: "button",
                        buttonImage: "Images/calendar.gif",
                        buttonImageOnly: true
                    });
                    $("#MainContent_LoginView1_betweenStartDate").datepicker({
                        showOn: "button",
                        buttonImage: "Images/calendar.gif",
                        buttonImageOnly: true
                    });
                    $("#MainContent_LoginView1_betweenEndDate").datepicker({
                        showOn: "button",
                        buttonImage: "Images/calendar.gif",
                        buttonImageOnly: true
                    });
                    
                    if ($("#MainContent_LoginView1_on").prop('checked')) {
                        enableOnDate();
                    }
                    if ($("#MainContent_LoginView1_between").prop('checked')) {
                        enableBetweenDate();
                    }

                    $("#MainContent_LoginView1_on").click(function () {
                        enableOnDate();
                        $("#MainContent_LoginView1_betweenStartDate").val("");
                        $("#MainContent_LoginView1_betweenEndDate").val("");
                    });
                    $("#MainContent_LoginView1_between").click(function () {
                        enableBetweenDate();
                        $("#MainContent_LoginView1_onDate").val("");
                    });
                });
                function enableOnDate() {
                    $("#MainContent_LoginView1_onDate").removeAttr("disabled");
                    $("#MainContent_LoginView1_onDate").datepicker("enable");
                    $("#MainContent_LoginView1_betweenStartDate").attr("disabled", "disabled");
                    $("#MainContent_LoginView1_betweenStartDate").datepicker("disable");
                    $("#MainContent_LoginView1_betweenEndDate").attr("disabled", "disabled");
                    $("#MainContent_LoginView1_betweenEndDate").datepicker("disable");
                    
                };
                function enableBetweenDate() {
                    $("#MainContent_LoginView1_onDate").attr("disabled", "disabled");
                    $("#MainContent_LoginView1_onDate").datepicker("disable");
                    $("#MainContent_LoginView1_betweenStartDate").removeAttr("disabled");
                    $("#MainContent_LoginView1_betweenStartDate").datepicker("enable");
                    $("#MainContent_LoginView1_betweenEndDate").removeAttr("disabled");
                    $("#MainContent_LoginView1_betweenEndDate").datepicker("enable");
                };
            });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            Please Log In</AnonymousTemplate>
        <LoggedInTemplate>
            <table class="style1">
                <tr>
                    <td class="style2" valign="top">
                        Show stats for:
                        <asp:DropDownList ID="ddlUsers" runat="server" DataSourceID="ASPNETDBConnectionString"
                            DataTextField="UserName" DataValueField="UserId" >
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="ASPNETDBConnectionString" runat="server" ConnectionString="<%$ ConnectionStrings:ASPNETDBConnectionString %>"
                            SelectCommand="SELECT [UserId], [UserName] FROM [vw_aspnet_Users]"></asp:SqlDataSource>
                    </td>
                    <td valign="top">
                        <asp:RadioButton ID="on" runat="server" Checked="True" GroupName="group1" Text="on" />
                        <br />
                        <asp:RadioButton ID="between" runat="server" GroupName="group1" Text="between"/>
                    </td>
                    <td>
                        <asp:TextBox ID="onDate" runat="server"></asp:TextBox>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="onDate" Display="Dynamic" EnableClientScript="False" ErrorMessage="Invalid or Missing Date" ForeColor="#FF3300" OnServerValidate="CustomValidator1_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                        <br />
                        <asp:TextBox ID="betweenStartDate" runat="server"></asp:TextBox>
                        and
                        <asp:TextBox ID="betweenEndDate" runat="server"></asp:TextBox>
                        <br />
                        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="betweenStartDate" Display="Dynamic" ErrorMessage="Invalid or Missing Start Date" ForeColor="#FF3300" ValidateEmptyText="True" EnableClientScript="False" OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="betweenEndDate" Display="Dynamic" EnableClientScript="False" ErrorMessage="Invalid or Missing End Date" ForeColor="#FF3300" OnServerValidate="CustomValidator3_ServerValidate" ValidateEmptyText="True"></asp:CustomValidator>
                    </td>
                    <td class="auto-style1">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Button ID="btnDisplay" runat="server" Text="Display" OnClick="btnDisplay_Click" />
                        <br />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>&nbsp;</td>
                    <td class="auto-style1">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvStats" runat="server" AutoGenerateColumns="False"
                DataKeyNames="Id" BorderStyle="Solid" BorderWidth="1px" CellPadding="3">
                <AlternatingRowStyle BackColor="#F0F0F0" VerticalAlign="Top" />
                <Columns>
                    <asp:TemplateField HeaderText="BugId">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Bug.Bug1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Language">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Language.LanguageShort") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ChangedOn" HeaderText="Changed On" />
                    <asp:TemplateField HeaderText="Resolution">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Status.StatusName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Changed By">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ChangedBy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time Spent">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("TimeSpent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Note">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Note") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Label ID="lblTest" runat="server"></asp:Label>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
