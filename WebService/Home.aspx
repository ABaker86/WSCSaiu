<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    
    <%-- This is the beginning of the Body Placeholder --%>


   <div style="float:right; width: 125px; padding-right:50px;">
   <div>
       <asp:Label ID="lblSort" runat="server" Text="Sort By Price"></asp:Label>
       <asp:Button ID="btnPriceASC" runat="server" Text="L -> H" Width="60px" 
           onclick="btnPriceASC_Click" />
       <asp:Button ID="btnPriceDESC" runat="server" Text="H->L" Width="60px" 
           onclick="btnPriceDESC_Click"/>

</div>
       <asp:Label ID="lbProductName" runat="server" Text="Product Name"></asp:Label><br />
       <asp:TextBox ID="tbPName" runat="server" ></asp:TextBox>
       <asp:Label ID="lbProductDesc" runat="server" Text="Description"></asp:Label><br />
       <asp:TextBox ID="tbPDesc" runat="server"></asp:TextBox>
       <asp:Label ID="lbProductPrice" runat="server" Text="Price"></asp:Label><br />
       <asp:TextBox ID="tbPPrice" runat="server"></asp:TextBox>
       <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" 
           onclick="btnAddProduct_Click" />
       <asp:Button ID="btnGetProductPrice" runat="server" Text="Get Price" onclick="btnGetProductPrice_Click" />
   </div>
    

        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" 
        onselectedindexchanged="gvData_SelectedIndexChanged" 
        onrowcommand="gvData_RowCommand" Width="376px" BackColor="LightGoldenrodYellow" 
    BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" 
    GridLines="None" DataKeyNames="product_id"  >
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:BoundField DataField="product_id" HeaderText="product_id" 
                    InsertVisible="False" ReadOnly="True" SortExpression="product_id" />
                <asp:BoundField DataField="product_name" HeaderText="product_name" 
                    SortExpression="product_name" />
                <asp:BoundField DataField="product_descritpion" 
                    HeaderText="product_descritpion" SortExpression="product_descritpion" />
                <asp:BoundField DataField="product_price" HeaderText="product_price" 
                    SortExpression="product_price" />
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


<%-- This is the end of the Body Placeholder --%>


<asp:Label ID="lblError" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>









    <asp:SqlDataSource ID="sdsHomeData" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [Products]"></asp:SqlDataSource>









</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="cphAddSpace" Runat="Server">
    this stuff is on the bottom of the screen
</asp:Content>

