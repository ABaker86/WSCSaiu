<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserAdmin.aspx.cs" Inherits="UserAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

 <asp:Label ID="lbWelcomeUser" runat="server" Font-Bold="True" Font-Italic="True"></asp:Label> _Welcome to the User Roles Page




    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="user_id" DataSourceID="SqlDataSource1" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
        CellPadding="2" ForeColor="Black" GridLines="None" Width="433px">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="user_id" HeaderText="user_id" InsertVisible="False" 
                ReadOnly="True" SortExpression="user_id" />
            <asp:BoundField DataField="user_name" HeaderText="user_name" 
                SortExpression="user_name" />
            <asp:BoundField DataField="password" HeaderText="password" 
                SortExpression="password" />
            <asp:CheckBoxField DataField="isAdmin" HeaderText="isAdmin" 
                SortExpression="isAdmin" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
            HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <SortedAscendingCellStyle BackColor="#FAFAE7" />
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
        <SortedDescendingCellStyle BackColor="#E1DB9C" />
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [users]"></asp:SqlDataSource>






</asp:Content>


