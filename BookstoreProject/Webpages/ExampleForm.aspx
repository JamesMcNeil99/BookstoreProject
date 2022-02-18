<%@ Page Title="Example Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExampleForm.aspx.cs" Inherits="BookstoreProject.ExampleForm" %>
<asp:Content ID="ExampleContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <table>
        <tr>
            <td>
                <asp:Button ID="btnExample" runat="server" Text="Click Me" OnClick="btnExampleOnClick" BorderWidth="2px" Width="150px" Height="50px" Font-Size="15pt" />
            </td>
            <td dir="rtl">
                <asp:Label ID="lblExample" runat="server" Text="Hello" Visible="False" Font-Size="15pt" Width="150px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnLinkToAbout" runat="server" Text="To About Page" PostBackUrl="About.aspx" />
            </td>
        </tr>

    </table>

</asp:Content>
