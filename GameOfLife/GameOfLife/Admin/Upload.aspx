<%@ Page Title="Upload Template" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="GameOfLife.Admin.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Upload Template</h1>

    <br />
    <asp:FileUpload ID="TemplateUpload" runat="server" /><br /><br />
    <asp:Button ID="Submit" runat="server" Text="Upload" OnClick="Submit_Click" CssClass="btn btn-success" />&nbsp&nbsp<asp:Label ID="Message" runat="server" Text=""></asp:Label>
    
</asp:Content>