<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            Please Log In
        </AnonymousTemplate>
        <LoggedInTemplate>
            <asp:Label ID="Label1" runat="server" Text="Bug ID: "></asp:Label>
            <asp:TextBox ID="txtBugId" runat="server" MaxLength="10" ToolTip="Insert bug name in project-number format (i.e. ABC-123)"></asp:TextBox>
            <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" OnClick="btnCheckOut_Click" />
            <asp:Label ID="lblAlreadyCheckedOut" runat="server" ForeColor="#FF3300" Text="Already Checked Out!"
                Visible="False"></asp:Label>
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtBugId"
                Display="Dynamic" ErrorMessage="Incorrect Bug Id" ForeColor="#FF3300" ValidationExpression="^[A-Za-z]{3}-[0-9]+$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBugId"
                Display="Dynamic" ErrorMessage="Missing Bug ID" ForeColor="#FF3300" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <br />
            <table cellpadding="0" class="style1">
                <tr>
                    <td>
                        <strong>Checked Out By Me:</strong>
                    </td>
                    <td>
                        <strong>Checked Out By Others:</strong>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:GridView ID="gvMe" runat="server" AutoGenerateColumns="False" Width="308px"
                            DataKeyNames="Id" CellPadding="3" EmptyDataText="0 bugs checked out by me">
                            <AlternatingRowStyle BackColor="#F0F0F0" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="BugId">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Bug.Bug1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ChangedOn" HeaderText="Checked Out" />
                                <asp:TemplateField HeaderText="Undo">
                                    <ItemTemplate>
                                        <asp:HyperLink NavigateUrl='<%# "~/UndoSingle.aspx?BugID=" + Eval("Bug.Bug1") %>'
                                            runat="server" Text="Undo" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Check In">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "~/CheckInSingle.aspx?BugID=" + Eval("Bug.Bug1") %>'
                                            runat="server" Text="Check In" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblException" runat="server" Enabled="False" ForeColor="Red" Visible="False"></asp:Label>
                    </td>
                    <td valign="top">
                        <asp:GridView ID="gvOthers" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CellPadding="3" EmptyDataText="0 bugs checked out by others">
                            <AlternatingRowStyle BackColor="#F0F0F0" />
                            <Columns>
                                <asp:TemplateField HeaderText="BugId" SortExpression="BugId">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Bug.Bug1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ChangedBy" HeaderText="Checked Out By" />
                                <asp:BoundField DataField="ChangedOn" HeaderText="Checked Out At" />
                                <asp:TemplateField HeaderText="Undo">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# "~/UndoSingle.aspx?BugID=" + Eval("Bug.Bug1") %>'
                                            runat="server" Text="Undo" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Check In">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "~/CheckInSingle.aspx?BugID=" + Eval("Bug.Bug1") %>'
                                            runat="server" Text="Check In" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </LoggedInTemplate>
    </asp:LoginView>
    <br />
</asp:Content>
