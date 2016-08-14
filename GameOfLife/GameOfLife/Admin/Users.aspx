<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="GameOfLife.Admin.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Admin - Users</h1>

    <asp:GridView class="table table-striped" ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Email], [UserName], [Id] FROM [AspNetUsers] WHERE ([UserName] &lt;&gt; @UserName)" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [AspNetUsers] WHERE [Id] = @original_Id AND (([Email] = @original_Email) OR ([Email] IS NULL AND @original_Email IS NULL)) AND [UserName] = @original_UserName" InsertCommand="INSERT INTO [AspNetUsers] ([Email], [UserName], [Id]) VALUES (@Email, @UserName, @Id)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [AspNetUsers] SET [Email] = @Email, [UserName] = @UserName WHERE [Id] = @original_Id AND (([Email] = @original_Email) OR ([Email] IS NULL AND @original_Email IS NULL)) AND [UserName] = @original_UserName">
        <DeleteParameters>
            <asp:Parameter Name="original_Id" Type="String" />
            <asp:Parameter Name="original_Email" Type="String" />
            <asp:Parameter Name="original_UserName" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="Id" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="admin@gmail.com" Name="UserName" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="original_Id" Type="String" />
            <asp:Parameter Name="original_Email" Type="String" />
            <asp:Parameter Name="original_UserName" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
