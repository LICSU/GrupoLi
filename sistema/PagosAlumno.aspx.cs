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

public partial class sistema_PagosAlumno : System.Web.UI.Page
{
    string Err = "", Rol = "", sSelectSQL = "";
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
        }
        BindGridView();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT [Plan].PlanNombre as PlanNombre, " +
                           " PlanPago.FacturaNumero as FacturaNumero," +
                           " CONVERT(VARCHAR(11),PlanPago.FacturaFecha,103) as FacturaFecha, " +
                           " PlanAlumno.UsuarioID as UsuarioID, " +
                           " PlanPago.FacturaMonto as FacturaMonto " +
                           " FROM [Plan] INNER JOIN" +
                           " PlanAlumno ON [Plan].PlanID = PlanAlumno.PlanID INNER JOIN" +
                           " PlanPago ON PlanAlumno.PlanAlumnoID = PlanPago.PlanAlumnoID INNER JOIN" +
                           " Usuario ON PlanAlumno.UsuarioID = Usuario.UsuarioID" +
                           " WHERE PlanAlumno.UsuarioID = " + _autenticado.UsuarioID;
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
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
}