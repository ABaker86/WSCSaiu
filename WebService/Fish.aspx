<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Fish.aspx.cs" Inherits="Fish" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <h1>Lorem Ipsum Fish</h1>
<p>Suspendisse rutrum odio vitae lacus tincidunt pharetra. Ut eros neque, iaculis in lacus sit amet, fermentum elementum lacus. Nunc vitae odio tortor. Nunc ultricies, augue sit amet iaculis laoreet, est nunc ultricies dolor, in vehicula orci arcu et nibh. Donec tristique vitae purus nec accumsan. Cras tempus pulvinar nibh. Phasellus id enim vitae neque tincidunt vehicula.</p>


<div style="center">
<asp:DataList ID="dlFish" runat="server" 
            BackColor="White" BorderColor="White" BorderStyle="None" BorderWidth="0px" 
            RepeatColumns="3" ShowFooter="False" ShowHeader="False" Width="500px" 
        HorizontalAlign="Center">
    <ItemTemplate>
    
        <p style="margin-left:8px;
          -moz-border-radius: 5px;
          border-radius: 5px;
          padding:8px;
          text-align:center;
          border:1px solid #ccc;
          background-color:#efefef;
          float:left;
          font-size:11px;
          font-family:Verdana,Arial,sans-serif;">
                  <img border="0" src="img/60x60.gif" style="border:none !important; padding:0px !important;" />
                 <br /><asp:Label ID="nameLabel" runat="server" Text='<%# Eval("product_name") %>' />
                 <br /><asp:Label ID="Label6" runat="server" Text='<%# Eval("product_price") %>' />
                 <br /><br />
        <button type="btnBuyNow">Buy Now</button>
        </p>
  </ItemTemplate>
</asp:DataList>




</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphAddSpace" Runat="Server">
</asp:Content>

