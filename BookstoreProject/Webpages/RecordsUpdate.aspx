<%@ Page Title="Update Records" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="RecordsUpdate.aspx.cs" Inherits="BookstoreProject.Webpages.RecordsUpdate" %>

<asp:Content ID="RecordsUpdateContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <h3 runat="server" id="h3Display">Update Records</h3>
    
    <br />
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
        <asp:TableRow ID="TableRow8" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl8" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:TextBox runat="server" id="tbx8"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow9" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl9" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:DropDownList ID="ddl1" runat="server"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow10" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl10" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:DropDownList ID="ddl2" runat="server"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow><asp:TableRow ID="TableRow11" runat="server">
            <asp:TableCell Style="white-space:nowrap">
                <asp:Label runat="server" id="lbl11" Wrap="false"></asp:Label>
            </asp:TableCell>
            <asp:TableCell Style="white-space:nowrap">
                <asp:DropDownList ID="ddl3" runat="server"></asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>


    <p>
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </p>


</asp:Content>
