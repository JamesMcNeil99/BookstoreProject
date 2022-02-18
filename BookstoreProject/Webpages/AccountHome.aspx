<%@ Page Title="Your Account" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="AccountHome.aspx.cs" Inherits="BookstoreProject.Webpages.AccountHome" %>

<asp:Content ID="AccountContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <p>
        <asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl1" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:TextBox runat="server" id="tbx1"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl2" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:TextBox runat="server" id="tbx2"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl3" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:TextBox runat="server" id="tbx3"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow4" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl4" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:TextBox runat="server" id="tbx4"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow5" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl5" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:TextBox runat="server" id="tbx5"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow6" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl6" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:TextBox runat="server" id="tbx6"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow7" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl7" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:TextBox runat="server" id="tbx7"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
            </asp:Table>
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" />
    </p>
    

    <div>
        <asp:Label ID="lblYourOrders" runat="server" Text="Your Orders: "></asp:Label>
        <asp:GridView ID="gvwYourOrders" runat="server" CellSpacing="2" Width="100%" GridLines="None">
            <AlternatingRowStyle BackColor="#F0F0F0" />
        </asp:GridView>
    </div>
    
    

</asp:Content>
