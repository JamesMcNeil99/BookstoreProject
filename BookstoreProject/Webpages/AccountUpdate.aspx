<%@ Page Title="Update Account" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="AccountUpdate.aspx.cs" Inherits="BookstoreProject.Webpages.AccountUpdate" %>

<asp:Content ID="AccountUpdateContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <p>
        <asp:Label ID="lblWelcome" runat="server" Text="Update Account" Font-Size="15"></asp:Label>
    </p>
    <p>  
        <asp:Label ID="lblName" runat="server" Text="Update Name: "></asp:Label>
        <asp:TextBox ID="tbxName" runat="server" Text=""></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lblPhone" runat="server" Text="Update Phone: "></asp:Label>
        <asp:TextBox ID="tbxPhone" runat="server" Text=""></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lblAddress" runat="server" Text="Update Address: "></asp:Label>
        <asp:TextBox ID="tbxAddress" runat="server" Text=""></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="lblEmail" runat="server" Text="Update Email: "></asp:Label>
        <asp:TextBox ID="tbxEmail" runat="server" Text=""></asp:TextBox>
    </p>

    <p>
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </p>

</asp:Content>
