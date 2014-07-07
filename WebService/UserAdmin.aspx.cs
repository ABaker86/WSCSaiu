using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AquaGear_ITSD425_Rebuild;

public partial class UserAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbWelcomeUser.Text = Session["loggedInUserName"].ToString();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}