<%@ Page Title="Place Order" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="OrderPlace.aspx.cs" Inherits="BookstoreProject.Webpages.PlaceOrder" %>

<asp:Content ID="OrderPlaceContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3>Place Order</h3>

    <p>Delivery Address: 
        <asp:DropDownList ID="ddlAddress" runat="server">
            <asp:ListItem Selected="true" Value="Option1"> DefaultAddress </asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>Confirmation Email: 
        <asp:DropDownList ID="ddlEmail" runat="server">
            <asp:ListItem Selected="true" Value="Option1"> Default Email </asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>Credit Card Info (sure...): <asp:TextBox ID="tbxPaymentInfo" runat="server"></asp:TextBox></p>



    <br />
    <p>Order Items: </p>
    <div>
        <asp:GridView ID="gvwOrderItems" runat="server" CellSpacing="2" Width="100%" GridLines="None">
            <AlternatingRowStyle BackColor="#F0F0F0" />
        </asp:GridView>
    </div>
    <p>
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
    </p>

</asp:Content>
