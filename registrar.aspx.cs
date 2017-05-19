using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class registrar : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string sErr = string.Empty;
    string sSelectSQL = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utilidades.CargarListado(ref dplUnidad, "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente ORDER BY TXT ", cn, ref sErr, true);
        }
    }
 
    protected void dplUnidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplUnidad.SelectedValue != "")
        {
            string idEmpresa = dplUnidad.SelectedValue;
            if (idEmpresa == "1")
            {
                Response.Redirect("registrarAcademia.aspx");
                phTipo.Visible = false;
            }
            else
            {
                phTipo.Visible = true;  
            }
        }
    }
    protected void ddlTipoEmpleado_SelectedIndexChanged(object sender, EventArgs e)
    {
        string idEmpresa = dplUnidad.SelectedValue;
        if (ddlTipoEmpleado.SelectedValue != "")
        {
            if (ddlTipoEmpleado.SelectedValue == "Empleado")
            {
                Response.Redirect("registrarEmpresa.aspx?idEmpresa=" + idEmpresa + "&tpo=1");
            }
            else
            {
                Response.Redirect("registrarEmpresa.aspx?idEmpresa=" + idEmpresa + "&tpo=2");
            }
        }
    }
}