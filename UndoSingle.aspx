<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="UndoSingle.aspx.cs" Inherits="UndoSingle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 69px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            Please Log In
        </AnonymousTemplate>
        <LoggedInTemplate>
            <table class="style1">
                <tr>
                    <td class="style2">
                        Bug ID:
                    </td>
                    <td>
                        <asp:Label ID="lblBugID" runat="server"></asp:Label>
                        <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Time spent:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtTime" Display="Dynamic" ErrorMessage="Value missing" 
                    ForeColor="#FF3300"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtTime" Display="Dynamic" ErrorMessage="Invalid value" ForeColor="#FF3300" MaximumValue="525600" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Button ID="btnUndo" runat="server" Text="Undo" onclick="btnUndo_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
