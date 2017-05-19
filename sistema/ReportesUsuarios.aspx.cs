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

public partial class sistema_ReportesUsuarios : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "", sSelecSQL = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        //BindGridView();
        if (!IsPostBack)
        {                
            Utilidades.CargarListado(ref dplUnidad, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente", cn, ref Err, true);
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT  Usuario.UsuarioCedula as Cedula, (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as Nombre, "+
                               "     Usuario.UsuarioClave as Clave, Usuario.UsuarioCorreo as Correo, "+
                               "     Usuario.UsuarioTelefono as Telefono, Usuario.UsuarioCelular1 as Celular,"+ 
                               "     Cliente.ClienteNombre as Unidad, [Plan].PlanNombre as PlanNombre,"+
                               "     CONVERT(VARCHAR(11),p.PlanAlumnoFechaFin, 103) as FechaFin, "+
                               "     p.ClasesActivas as ClasesActivas, "+
                               "     CONVERT(VARCHAR(11),Usuario.UsuarioFechaRegistro,103) as FechaRegistro"+
                           " FROM PlanAlumno p INNER JOIN"+
                           " Usuario ON p.UsuarioID = Usuario.UsuarioID INNER JOIN"+
                           " Cliente ON p.ClienteID = Cliente.ClienteID INNER JOIN"+
                           " [Plan] ON p.PlanID = [Plan].PlanID"+
                           " WHERE (1 = 1) "+ViewState["FiltroCedula"] + ViewState["FiltroEmpresa"] + ViewState["FiltroRegistro"]+
                           " order by p.ClienteID ASC, p.PlanAlumnoFechaFin Asc";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Cedula";
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

    protected void Buscar_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text != "")
        {
            ViewState["FiltroCedula"] = " AND (Usuario.UsuarioCedula LIKE '%" + txtSearch.Text + "%')";                
        }
        BindGridView();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void btnUnidad_Click(object sender, EventArgs e)
    {
        if (dplUnidad.SelectedValue != "")
        {
            ViewState["FiltroEmpresa"] = " AND (p.ClienteID = "+dplUnidad.SelectedValue+")";                
        }
        else
        {
            MostrarMsjModal("Seleccione un valor de la lista", "ERR");
        }
        BindGridView();
    }

    protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT  Usuario.UsuarioCedula as Cedula, (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as Nombre, " +
                        "     Usuario.UsuarioClave as Clave, Usuario.UsuarioCorreo as Correo, " +
                        "     Usuario.UsuarioTelefono as Telefono, Usuario.UsuarioCelular1 as Celular," +
                        "     Cliente.ClienteNombre as Unidad, [Plan].PlanNombre as PlanNombre," +
                        "     CONVERT(VARCHAR(11),p.PlanAlumnoFechaFin, 103) as FechaFin, " +
                        "     p.ClasesActivas as ClasesActivas, " +
                        "     CONVERT(VARCHAR(11),Usuario.UsuarioFechaRegistro,103) as FechaRegistro" +
                          " FROM PlanAlumno p INNER JOIN" +
                          " Usuario ON p.UsuarioID = Usuario.UsuarioID INNER JOIN" +
                          " Cliente ON p.ClienteID = Cliente.ClienteID INNER JOIN" +
                          " [Plan] ON p.PlanID = [Plan].PlanID" +
                          " WHERE (1 = 1) " + ViewState["FiltroCedula"] + ViewState["FiltroEmpresa"] + ViewState["FiltroRegistro"] +
                          " order by p.ClienteID ASC, p.PlanAlumnoFechaFin Asc";
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "Cedula";
        grid.DataKeyNames = TablaID;
        grid.DataSource = dt;
        grid.DataBind();
        cn.Close();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.IO.StringWriter sw = new System.IO.StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page page = new Page();
        System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();
        GridView Grid = new GridView();
        grid.AllowPaging = false;
        grid.DataBind();
        grid.EnableViewState = false;
        page.EnableEventValidation = false;
        page.DesignerInitialize();
        page.Controls.Add(form);
        form.Controls.Add(grid);
        page.RenderControl(htw);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte_Usuarios_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    protected void btnFiltroFecha_Click(object sender, EventArgs e)
    {
        if (txtFechaRegistroDesde.Text != "" && txtFechaRegistroHasta.Text == "")
        {
            ViewState["FiltroRegistro"] = " AND (CONVERT(DATE, Usuario.UsuarioFechaRegistro,103) >= CONVERT(DATE, '" + txtFechaRegistroDesde.Text + "', 103))";
        }
        else if (txtFechaRegistroDesde.Text == "" && txtFechaRegistroHasta.Text != "")
        {
            ViewState["FiltroRegistro"] = " AND (CONVERT(DATE, Usuario.UsuarioFechaRegistro,103) <= CONVERT(DATE, '" + txtFechaRegistroHasta.Text + "', 103))";
        }
        else if (txtFechaRegistroDesde.Text != "" && txtFechaRegistroHasta.Text != "")
        {
            ViewState["FiltroRegistro"] = " AND ( (CONVERT(DATE, Usuario.UsuarioFechaRegistro,103) <= CONVERT(DATE, '" + txtFechaRegistroHasta.Text + "', 103)) AND (CONVERT(DATE, Usuario.UsuarioFechaRegistro,103) >= CONVERT(DATE, '" + txtFechaRegistroDesde.Text + "', 103)) )";
        }
        BindGridView();
    }
}