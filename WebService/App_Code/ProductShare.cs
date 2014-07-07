using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AquaGear_ITSD425_Rebuild;
using System.Data;

/// <summary>
/// Summary description for ProductShare
/// </summary>
[WebService(Namespace = "http://AquaGear.com/", Description="This a sample Web Service for ITSD425", Name="ProductShareService")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ProductShare : System.Web.Services.WebService {

    private ProductAccess _pShare;

    public ProductShare () {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    /// <summary>
    /// Returns the price of a product based on provided name
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Product price as decimal</returns>
    public string GetProductPrice(string name)
    {
        try {
                _pShare = new ProductAccess();
                return _pShare.GetProductPrice(name);
            }
        catch (Exception ex)
            {
            return "error";}
            }


    [WebMethod]
    /// <summary>
    /// returns a dataset of proudcts ordered by price
    /// </summary>
    /// <param name="name">1= Acending, 2= Decending</param>
    /// <returns>DataSet</returns>
    public DataSet SortByPrice(int x)
    {
        try 
            {
                _pShare = new ProductAccess();
                return _pShare.SortAllProductsbyPrice(x);
            }
        catch (Exception ex)
            {
            return null;
            }
    }
   
}
