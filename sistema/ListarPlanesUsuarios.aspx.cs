using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Security;
using System.IO;
using System.Web.UI.HtmlControls;


public partial class sistema_ListarPlanesUsuarios : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "", sSelectSQL = "";
    DataTable dataTable;
    DataView vista;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente ORDER BY TXT";
            Utilidades.CargarListado(ref dplEmpresas, sSelectSQL, cn, ref Err, true);
        }
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string cmd2 = "SELECT dbo.Usuario.UsuarioID as UsuarioID, "
                        + " (dbo.Usuario.UsuarioNombre+' '+dbo.Usuario.UsuarioApellido) as UsuarioNombre,  "
                        + " dbo.Usuario.UsuarioCedula as UsuarioCedula,  "
                        + " dbo.[Plan].PlanNombre as PlanNombre, "
                        + " dbo.[Plan].ClasesRegulares as TotalR, "
                        + " dbo.[Plan].ClasesComplemen as TotalC, "
                        + " dbo.PlanAlumno.ClasesActivas as PlanCantidadClases, "
                        + " (SELECT ClienteNombre FROM Cliente WHERE ClienteID=dbo.PlanAlumno.ClienteID) as ClienteNombre, "
                        + " dbo.PlanAlumno.ClienteID as ClienteID, "
                        + " CONVERT(VARCHAR(11),dbo.PlanAlumno.PlanAlumnoFechaFin,103) as PlanAlumnoFechaFin"
                        + " FROM dbo.PlanAlumno INNER JOIN"
                        + " dbo.Usuario ON dbo.PlanAlumno.UsuarioID = dbo.Usuario.UsuarioID INNER JOIN"
                        + " dbo.[Plan] ON dbo.PlanAlumno.PlanID = dbo.[Plan].PlanID "
                        + ViewState["EmpresaID"] + " ORDER BY UsuarioID ASC";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, conn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dataTable = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
            //Attribute to show the Plus Minus Button.
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void dplEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplEmpresas.SelectedValue != "")
        {
            ViewState["EmpresaID"] = " WHERE dbo.PlanAlumno.ClienteID = " + dplEmpresas.SelectedValue;
            BindGridView();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            {
                ViewState["EmpresaID"] = " WHERE UsuarioNombre LIKE '%" + sBuscar + "%' OR UsuarioCedula  LIKE '%" + sBuscar + "%'";
            }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected void btnArchivo_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    private void MostrarMsjModal(string msj, string tipo)
    {
        string sTitulo = "Información";
        string sCcsClase = "fa fa-check fa-2x text-info";
        switch (tipo)
        {
            case "ERR":
                sTitulo = "ERROR";
                sCcsClase = "fa fa-times fa-2x text-danger";
                break;
            case "ADV":
                sTitulo = "ADVERTENCIA"; //
                sCcsClase = "fa fa-exclamation-triangle fa-2x text-warning";
                break;
            case "EXI":
                sTitulo = "ÉXITO";
                sCcsClase = "fa fa-check fa-2x text-success";
                break;
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarMsjModal", "MostrarMsjModal('" + msj.Replace("'", "").Replace("\r\n", " ") + "','" + sTitulo + "','" + sCcsClase + "');", true);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}