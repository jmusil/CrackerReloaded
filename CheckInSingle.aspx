<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CheckInSingle.aspx.cs" Inherits="CheckInSingle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            Please Log In
        </AnonymousTemplate>
        <LoggedInTemplate>
            <table class="style1">
        <tr>
            <td class="style2">
                Bug ID:</td>
            <td>
                <asp:Label ID="lblBugId" runat="server"></asp:Label>
                <asp:Label ID="lblWarning" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Language:</td>
            <td>
                <asp:DropDownList ID="ddlLanguages" runat="server" 
                    DataSourceID="EntityDataSource1" DataTextField="LanguageShort" 
                    DataValueField="ID" TabIndex="1">
                </asp:DropDownList>
                <asp:EntityDataSource ID="EntityDataSource1" runat="server" 
                    ConnectionString="name=CrackerEntities" DefaultContainerName="CrackerEntities" 
                    EnableFlattening="False" EntitySetName="Languages" OrderBy="it.LanguageShort" 
                    Select="it.[ID], it.[LanguageShort]" EntityTypeFilter="" 
                    Where="it.Id != 14">
                </asp:EntityDataSource>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Resolution:</td>
            <td>
                <asp:DropDownList ID="ddlResolution" runat="server" 
                    DataSourceID="EntityDataSource2" DataTextField="StatusName" DataValueField="Id" 
                    TabIndex="2">
                </asp:DropDownList>
                <asp:EntityDataSource ID="EntityDataSource2" runat="server" 
                    ConnectionString="name=CrackerEntities" DefaultContainerName="CrackerEntities" 
                    EnableFlattening="False" EntitySetName="Statuses" EntityTypeFilter="" 
                    OrderBy="it.StatusName" Select="it.[Id], it.[StatusName]" 
                    Where="it.Id != 8 and it.Id != 9">
                </asp:EntityDataSource>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Time Spent:</td>
            <td>
                <asp:TextBox ID="txtTime" runat="server" TabIndex="3" Width="80px"></asp:TextBox>
&nbsp;minutes<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtTime" Display="Dynamic" ErrorMessage="Value missing" 
                    ForeColor="#FF3300"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtTime" Display="Dynamic" ErrorMessage="Invalid value" ForeColor="#FF3300" MaximumValue="525600" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Add Note:</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Height="67px" 
                    TabIndex="4" Width="182px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnCheckIn" runat="server" Text="Check In" TabIndex="5" 
                    onclick="btnCheckIn_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" 
                    onclick="btnCancel_Click" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
        </LoggedInTemplate>
    </asp:LoginView>
    
</asp:Content>

