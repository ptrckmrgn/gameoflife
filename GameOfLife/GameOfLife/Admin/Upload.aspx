<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="GameOfLife.Admin.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br /><asp:Label ID="Message" runat="server" Text=""></asp:Label><br />

    <br /><br /><br />
    <asp:FileUpload ID="TemplateUpload" runat="server" /><br /><br />
    <asp:Button ID="Submit" runat="server" Text="Upload" OnClick="Submit_Click" CssClass="btn btn-primary" />
    
</asp:Content>
