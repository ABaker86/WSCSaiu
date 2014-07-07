using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AquaGear_ITSD425_Rebuild;

public partial class Login : System.Web.UI.Page
{
    private UserRoles _user;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        _user = new UserRoles();

        #region Validation Method
        
        //Do a Validation Check
        if (_user.UserValidation(tbUserName.Text, tbPassword.Text))
        {
            Session["loggedInUserName"] = _user.UserName;
            Response.Redirect("~/UserAdmin.aspx");
        }
        else
        {
            Response.Redirect("~/Home.aspx");
        }
        #endregion

    }
}