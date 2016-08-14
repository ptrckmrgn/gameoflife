<%@ Page Title="Templates" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Templates.aspx.cs" Inherits="GameOfLife.Admin.Templates" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Admin - Templates</h1>
    <asp:GridView class="table table-striped" ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="UserName" HeaderText="Created By" SortExpression="UserName" />
            <asp:BoundField DataField="Height" HeaderText="Height" SortExpression="Height" />
            <asp:BoundField DataField="Width" HeaderText="Width" SortExpression="Width" />
            <asp:BoundField DataField="Cells" HeaderText="Cells" SortExpression="Cells" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM [Templates] WHERE [Id] = @original_Id AND [Name] = @original_Name AND [Height] = @original_Height AND [Width] = @original_Width AND [Cells] = @original_Cells AND (([User_Id] = @original_User_Id) OR ([User_Id] IS NULL AND @original_User_Id IS NULL))" InsertCommand="INSERT INTO [Templates] ([Name], [Height], [Width], [Cells], [User_Id]) VALUES (@Name, @Height, @Width, @Cells, @User_Id)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT Templates.Name, Templates.Height, Templates.Width, Templates.Cells, Templates.User_Id, Templates.Id, AspNetUsers.UserName FROM Templates INNER JOIN AspNetUsers ON Templates.User_Id = AspNetUsers.Id" UpdateCommand="UPDATE [Templates] SET [Name] = @Name, [Height] = @Height, [Width] = @Width, [Cells] = @Cells, [User_Id] = @User_Id WHERE [Id] = @original_Id AND [Name] = @original_Name AND [Height] = @original_Height AND [Width] = @original_Width AND [Cells] = @original_Cells AND (([User_Id] = @original_User_Id) OR ([User_Id] IS NULL AND @original_User_Id IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_Id" Type="Int32" />
            <asp:Parameter Name="original_Name" Type="String" />
            <asp:Parameter Name="original_Height" Type="Int32" />
            <asp:Parameter Name="original_Width" Type="Int32" />
            <asp:Parameter Name="original_Cells" Type="String" />
            <asp:Parameter Name="original_User_Id" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" />
            <asp:Parameter Name="Height" />
            <asp:Parameter Name="Width" />
            <asp:Parameter Name="Cells" />
            <asp:Parameter Name="User_Id" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" />
            <asp:Parameter Name="Height" />
            <asp:Parameter Name="Width" />
            <asp:Parameter Name="Cells" />
            <asp:Parameter Name="User_Id" />
            <asp:Parameter Name="original_Id" />
            <asp:Parameter Name="original_Name" />
            <asp:Parameter Name="original_Height" />
            <asp:Parameter Name="original_Width" />
            <asp:Parameter Name="original_Cells" />
            <asp:Parameter Name="original_User_Id" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
