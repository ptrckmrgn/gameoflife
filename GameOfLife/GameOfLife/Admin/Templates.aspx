<%@ Page Title="Templates" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Templates.aspx.cs" Inherits="GameOfLife.Admin.Templates" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Templates</h1><br />

    <asp:GridView class="table table-striped" ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
            <asp:BoundField DataField="Height" HeaderText="Height" SortExpression="Height" />
            <asp:BoundField DataField="Width" HeaderText="Width" SortExpression="Width" />
            <asp:BoundField DataField="Cells" HeaderText="Cells" SortExpression="Cells" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM [Templates] WHERE [Id] = @original_Id" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT Templates.Name, Templates.Height, Templates.Width, Templates.Cells, Templates.Id, AspNetUsers.UserName FROM Templates INNER JOIN AspNetUsers ON Templates.UserId = AspNetUsers.Id">
        <DeleteParameters>
            <asp:Parameter Name="original_Id" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>
