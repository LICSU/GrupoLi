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
public partial class sistema_MostrarReservas : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    DataTable dt;
    DataSet ds;
    GridView grid;
    string Err = "", sSelectSQL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente ORDER BY TXT";
            Utilidades.CargarListado(ref dplClientes, sSelectSQL, cn, ref Err, true);
            BindGridView();
        }
    }
    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT  Reserva.ReservaID as ReservaID, " +
                        " Clase.ClaseDescripcion as ClaseDescripcion, " +
                        " Clase.ClaseID as ClaseID, " +
                        " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha, " +
                        " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora, " +
                        " Reserva.FechaReserva as FechaReserva, " +
                        " Reserva.UsuarioID as UsuarioID, " +
                        " (SELECT UsuarioCorreo FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as Correo, " +
                        " (SELECT UsuarioCelular1 FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as Celular, " +
                        " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario Where UsuarioID = Reserva.UsuarioID) as Usuario, " +
                        " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario Where UsuarioID = ClasePlantilla.ProfesorID) as Profesor, " +
                        " (SELECT UsuarioCedula FROM Usuario Where UsuarioID = Reserva.UsuarioID) as UsuarioCedula, " +
                        " (SELECT UsuarioCedula FROM Usuario Where UsuarioID = ClasePlantilla.ProfesorID) as ProfesorCedula, " +
                        " ClasePlantilla.ProfesorID as ProfesorID" +
                        " FROM  Clase INNER JOIN" +
                        " ClasePlantilla ON Clase.ClaseID = ClasePlantilla.ClaseID INNER JOIN" +
                        " Reserva ON ClasePlantilla.ClasePlantillaID = Reserva.ClasePlantillaID "
                        + " WHERE(CONVERT(DATE,ClasePlantillaFecha,103) >= CONVERT(DATE, SYSDATETIME(), 103)" + ViewState["sWhere"] + ")" +
                        " " + ViewState["FiltroBusqueda"] + " " + ViewState["FiltroBusqueda1"] +
                        " ORDER BY ClasePlantilla.ClasePlantillaFecha ASC, ClasePlantilla.ClasePlantillaHora ASC, ClasePlantilla.ProfesorID ASC";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ReservaID";
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
    protected void dplClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplClientes.SelectedValue == "")
        {
            ViewState["sWhere"] = "  AND ClasePlantilla.ClienteID = 1";
        }
        else
        {
            ViewState["sWhere"] = "  AND ClasePlantilla.ClienteID = " + dplClientes.SelectedValue;
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
        //int index = Convert.ToInt32(e.CommandArgument);
        //GridViewRow gvrow = GridView1.Rows[index];
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName == "usuario")
        {
            Response.Redirect("Pagos.aspx?user=" + (gvrow.FindControl("UsuarioCedula") as Label).Text);
        }
        else if (e.CommandName == "profesor")
        {
            Response.Redirect("verCalificaciones.aspx?clase=" + (gvrow.FindControl("ClaseID") as Label).Text + "&user=" + (gvrow.FindControl("UsuarioID") as Label).Text);
        }
    }
    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT   " +
                    " Clase.ClaseDescripcion as ClaseDescripcion, " +
                    " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha, " +
                    " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora, " +
                    " Reserva.FechaReserva as FechaReserva, " +
                    " (SELECT UsuarioNombre FROM Usuario Where UsuarioID = Reserva.UsuarioID) as Usuario, " +
                    " (SELECT UsuarioCorreo FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as Correo, " +
                    " (SELECT UsuarioCelular1 FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as Celular, " +
                    " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario Where UsuarioID = Reserva.UsuarioID) as Usuario, " +
                    " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario Where UsuarioID = ClasePlantilla.ProfesorID) as Profesor, " +
                    " (SELECT UsuarioCedula FROM Usuario Where UsuarioID = ClasePlantilla.ProfesorID) as ProfesorCedula " +
                    " FROM  Clase INNER JOIN" +
                    " ClasePlantilla ON Clase.ClaseID = ClasePlantilla.ClaseID INNER JOIN" +
                    " Reserva ON ClasePlantilla.ClasePlantillaID = Reserva.ClasePlantillaID " + ViewState["sWhere"] + " " +
                    " " + ViewState["FiltroBusqueda"] + " " + ViewState["FiltroBusqueda1"] +
                    " ORDER BY ClasePlantilla.ClasePlantillaFecha ASC, ClasePlantilla.ClasePlantillaHora ASC, ClasePlantilla.ProfesorID ASC";
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "ClaseDescripcion";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=reservas_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnReservar_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgregarReservaAdmin.aspx");
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        if (txtBuscar.Value != "")
        {
            ViewState["FiltroBusqueda"] = " AND (CONVERT(DATETIME, ClasePlantilla.ClasePlantillaFecha,103) = CONVERT(DATETIME, '" + txtBuscar.Value + "', 103))";
            BindGridView();
        }
        else
        {
            MostrarMsjModal("Ingrese un valor válido", "ERR");
        }
    }

    protected void btnFiltrarClase_Click(object sender, EventArgs e)
    {
        if (txtBuscarClase.Text != "")
        {
            ViewState["FiltroBusqueda1"] = " AND (Clase.ClaseDescripcion LIKE '%" + txtBuscarClase.Text + "%')";
            BindGridView();
        }
        else
        {
            MostrarMsjModal("Ingrese un valor válido", "ERR");
        }
    }
}