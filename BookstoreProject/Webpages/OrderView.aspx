<%@ Page Title="View Orders" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="OrderView.aspx.cs" Inherits="BookstoreProject.Webpages.OrderView" %>

<asp:Content ID="OrderViewContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3>View Order</h3>

    <p>Order Number: </p>
    <p>Customer Name: </p>
    <p>Delivery Address: </p>
    <p>Confirmation Email: </p>

    <div>
        <asp:GridView ID="gvwOrderItems" runat="server" CellSpacing="2" Width="100%" GridLines="None">
            <AlternatingRowStyle BackColor="#F0F0F0" />
        </asp:GridView>
    </div>


</asp:Content>
