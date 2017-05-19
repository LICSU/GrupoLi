using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sistema_Salir : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        string sHttpCookie = FormsAuthentication.FormsCookieName;
        if (Request.Cookies[sHttpCookie] != null)
        {
            Response.Cookies[sHttpCookie].Expires = DateTime.Now.AddDays(-1);
        }
        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        Response.Redirect("~/ingresar.aspx");
    }
}