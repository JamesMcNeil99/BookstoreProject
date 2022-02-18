<%@ Page Title="Browse Books" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="BrowseInventory.aspx.cs" Inherits="BookstoreProject.Webpages.BrowseInventory" %>

<asp:Content ID="BrowseContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3>Browse Books</h3>

     <asp:Table ID="tblSearchData" runat="server">
         <asp:TableRow>
             <asp:TableCell>
                 <asp:Label ID="lblBookName" runat="server" Text="Title: " Font-Size="10" Width="75"></asp:Label>
                 <asp:TextBox ID="tbxBookName" runat="server" Font-Size="10"></asp:TextBox>
             </asp:TableCell>
         </asp:TableRow>

         <asp:TableRow>
             <asp:TableCell>
                 <asp:Label ID="lblAuthorName" runat="server" Text="Author: " Font-Size="10" Width="75"></asp:Label>
                 <asp:TextBox ID="tbxAuthorName" runat="server" Font-Size="10"></asp:TextBox>
             </asp:TableCell>
         </asp:TableRow>

         <asp:TableRow>
             <asp:TableCell>
                 <asp:Label ID="lblSupplierName" runat="server" Text="Supplier: " Font-Size="10" Width="75"></asp:Label>
                 <asp:TextBox ID="tbxSupplierName" runat="server" Font-Size="10"></asp:TextBox>
             </asp:TableCell>
         </asp:TableRow>
     </asp:Table>
    

    <div>

        
            
    </div>

    <div align="Right">
        <asp:Button ID="btnRefreshTable" runat="server" Text="Search" OnClick="btnRefresh" width="100"/>
        <asp:Button ID="btnResetFilters" runat="server" Text="Reset" OnClick="btnReset" width="100"/>

    </div>

    <div>
        <asp:GridView ID="gvwBooks" runat="server" CellSpacing="2" Width="100%" GridLines="None" AutoGenerateSelectButton="True" OnSelectedIndexChanging="AddToCart">
            <AlternatingRowStyle BackColor="#F0F0F0" />
        </asp:GridView>
    </div>

</asp:Content>
