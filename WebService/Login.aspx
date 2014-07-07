<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">  
    <style type="text/css">
        .style1
        {
            width: 415px;
        }
        .style2
        {
            width: 55px;
        }
        .style3
        {
            width: 113px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

    <table class="style1" >
        <tr>
            <td class="style3">
                <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="Login" 
                    onclick="btnLogin_Click" />
                <asp:Button ID="btnCancle" runat="server" Text="Cancle" />
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="2">
                <asp:Label ID="lblErrorText" runat="server" ForeColor="Red" 
                    style="text-align: center" Visible="False" Width="100%"></asp:Label>
            </td>
        </tr>
    </table>



</asp:Content>


