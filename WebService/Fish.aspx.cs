using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Fish : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //using Web Service to populate Details List with Products in presentation Layer
        ProductShare _ps = new ProductShare();
        DataSet ds = _ps.SortByPrice(1);
        dlFish.DataSource = ds;
        dlFish.DataBind();
    }
}