<%@ Page Title="Login" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="BookstoreProject.LoginPage" %>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="~/Content/LoginStyle.css" rel="stylesheet" type="text/css" />
    <h1>Welcome to LumberBooks!</h1>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Username: "></asp:Label>
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        
        <asp:Label ID="lblInvalid" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
        
    </p>
    <p>
        <asp:HyperLink ID="HyperLink1" runat="server" href="AccountCreate.aspx">Need to create an account? Click here!</asp:HyperLink>
    </p>


</asp:Content>
