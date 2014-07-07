using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AquaGear_ITSD425_Rebuild;
using System.Data;


public partial class Home : System.Web.UI.Page
{
    //Private Members
    private ProductAccess _products;

    protected void Page_Load(object sender, EventArgs e)
    {
        _products = new ProductAccess();
        DataSet ds = _products.GetAllProducts();
        //set for a single table database
        gvData.DataSource = ds.Tables[0];
        gvData.DataBind();
    }

    #region Presentation Layer Methods
    /// <summary>
    /// Clear the text boxes on the GUI
    /// </summary>
    private void clear_fields()
    { 
        //Clear GUI
        tbPName.Text = "";
        tbPDesc.Text = "";
        tbPPrice.Text = "";

        //Clear Session Variables
        Session["isEdit"] = null;
        Session["selectedProductID"] = null;

        //Reset button text
        btnAddProduct.Text = "Add Product";
    }

    /// <summary>
    /// Selects appropriate row from gridview and saves data to BL class
    /// Sets Session isEdit = true
    /// Sets Lable text to "Update"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Get the selected row and fill the GUI with the information from the selection.
        _products = new ProductAccess();
        GridViewRow row = gvData.SelectedRow;
        long id = long.Parse(row.Cells[1].Text);
        _products.FindFirstByID(id);

        //Because I want to be able to add or edit a record, if Select is clicked, I want to 
        //create a session variable to hold this fact so that I can change the behavior of the add button to Edit instead of ADD
        //Also, save the selected Product ID in the session state
        Session.Add("isEdit", true);
        Session.Add("selectedProductID", _products.ProductID);

        //Change the lable so that the user knows that it is an Edit
        btnAddProduct.Text = "Update";
        
        displayInGUI_members();
    }

    /// <summary>
    /// Load GUI Layer information to Business Layer
    /// </summary>
    private void loadToBL_members()
    {
        _products.ProductName = tbPName.Text;
        _products.ProductDescription = tbPDesc.Text;
        _products.ProductPrice = decimal.Parse(tbPPrice.Text);
    }

    /// <summary>
    /// Load Buisness Layer Information to GUI
    /// </summary>
    private void displayInGUI_members()
    {
        tbPName.Text = _products.ProductName;
        tbPDesc.Text = _products.ProductDescription;
        tbPPrice.Text = _products.ProductPrice.ToString();
    }

    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        //This will execute an Add or Update command based on whether select has been clicked.
        //Check session variable and determin action accordingly

        bool isEdit;

        if (tbPDesc.Text == "" | tbPName.Text == "" | tbPPrice.Text == "") { lblError.Text = "Please Enter all Required Fields."; }
        else
        {

        try
        {
            //if this throws and Error it is becasue no Session variable Exists. Therefore treat it as an AddNew
            isEdit = bool.Parse(Session["isEdit"].ToString());
        }
        catch
        { 
            //set isEdit to False
            isEdit = false;
        }

        //Save information to business layer

        if (!isEdit) //isEdit is a New Item, Not and Edit.
        {
            //Initialize, this is a new object
            _products = new ProductAccess();
            loadToBL_members();
            try
            {
                //Try to Insert. If error occurs, catch the error.
                if (_products.Add() == false)
                    throw new Exception(_products.ExtendedErrorInformation);
                clear_fields();
            }
            catch (Exception ex)
            {
                //Make sure that error is displayed.
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        else //It is an isEdit. Therefore, treat accordingly
        { 
            //This is not a new Object, I need to get it back
            try
            {
                long id = long.Parse(Session["selectedProductID"].ToString());
                _products = new ProductAccess();
                _products.FindFirstByID(id);
                loadToBL_members();
                if (_products.Edit() == false)
                    throw new Exception(_products.ExtendedErrorInformation);

                clear_fields();
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        //Refresh Datagrid for updated infoamtion
        _products = new ProductAccess();
        gvData.DataSource = _products.GetAllProducts();
        gvData.DataBind();
    }
    }
    /// <summary>
    /// Delete data from database
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //Check to see if the delete button was pressed.
        if (e.CommandName.ToString() == "btnDelete")
        { 
            //Get the ID (Primary Key) of the selected Row.
            long selectedId = Convert.ToInt32(gvData.DataKeys[int.Parse(e.CommandArgument.ToString())].Value);
            _products = new ProductAccess();
            try
            {
                //Try to delet. If if fails, catch the Exception and display the error.
                if (_products.Delete(selectedId) == false)
                    throw new Exception(_products.ExtendedErrorInformation);

                //Refresh Datagrid for updat information
                gvData.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
            //
        }
    }
#endregion

    #region Web Service Methods

    /// <summary>
    /// Updates text field with price of product based on product name
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGetProductPrice_Click(object sender, EventArgs e)
    {
        //Reference to Web Serivice
        ProductShare _ps = new ProductShare();
        tbPPrice.Text = _ps.GetProductPrice(tbPName.Text).ToString();
    }

    /// <summary>
    /// Updates gridview with data ordered by Price ASC
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPriceASC_Click(object sender, EventArgs e)
    {
        try
        {
            ProductShare _ps = new ProductShare();
            DataSet ds = _ps.SortByPrice(1);
            //set for a single table database
                gvData.DataSource = ds.Tables[0];
                gvData.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
        }
    }

    /// <summary>
    /// Updates gridview with data ordered by Price DESC
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPriceDESC_Click(object sender, EventArgs e)
    {
        try
        {
            ProductShare _ps = new ProductShare();
            DataSet ds = _ps.SortByPrice(2);
            //set for a single table database
            gvData.DataSource = ds.Tables[0];
            gvData.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
        }
    }
    #endregion
}