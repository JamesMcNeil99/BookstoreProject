<%@ Page Title="Create Account" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="AccountCreate.aspx.cs" Inherits="BookstoreProject.Webpages.AccountCreate" %>

<asp:Content ID="AccountCreateContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <p>
        <asp:Label ID="lblWelcome" runat="server" Text="Create Account" Font-Size="15"></asp:Label>
    </p>
    
    <p>
        <asp:Label ID="lblUsername" runat="server" Text="Username: "></asp:Label>
        <asp:TextBox ID="tbxUsername" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="tbxPassword" runat="server" Text=""></asp:TextBox>
    </p>
    <p>  
        <asp:Label ID="lblFirstName" runat="server" Text="First Name: "></asp:Label>
        <asp:TextBox ID="tbxFirstName" runat="server" Text=""></asp:TextBox>
    </p>
    <p>  
        <asp:Label ID="lblLastName" runat="server" Text="Last Name: "></asp:Label>
        <asp:TextBox ID="tbxLastName" runat="server" Text=""></asp:TextBox>
    </p>
    <p>  
        For multiple entries, use a comma to separate:</p>
    <p>
        <asp:Label ID="lblPhone" runat="server" Text="Phone: "></asp:Label>
        <asp:TextBox ID="tbxPhone" runat="server" Text=""></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lblAddress" runat="server" Text="Address: "></asp:Label>
        <asp:TextBox ID="tbxAddress" runat="server" Text=""></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox ID="tbxEmail" runat="server" Text=""></asp:TextBox>
    </p>

    <p>
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        <asp:Label ID="lblWarning" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
    </p>


</asp:Content>
