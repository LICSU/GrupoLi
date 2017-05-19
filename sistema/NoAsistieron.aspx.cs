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

public partial class sistema_NoAsistieron : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            if (_autenticado.RolID == "1")
            {
                phClientes.Visible = true;
                Utilidades.CargarListado(ref dplClientes, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente ORDER BY VAL ", cn, ref Err, true);
            }
            else if (_autenticado.RolID == "4")
            {
                phClientes.Visible = false;
                Utilidades.CargarListado(ref dplClientes, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente ORDER BY VAL ", cn, ref Err, true);
                dplClientes.SelectedValue = _autenticado.ClienteID;
                dplClientes.Enabled = false;
                ViewState["sWhere"] = "AND dbo.Cliente.ClienteID = " + _autenticado.ClienteID;
            }
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT dbo.Reserva.ReservaID as ReservaID, " +
                        " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = dbo.Reserva.UsuarioID) as UsuarioNombre, " +
                        " (SELECT UsuarioCedula FROM Usuario WHERE UsuarioID = dbo.Reserva.UsuarioID) as UsuarioCedula, " +
                        " dbo.Clase.ClaseDescripcion as ClaseDescripcion, " +
                        " CONVERT(VARCHAR(11),(dbo.ClasePlantilla.ClasePlantillaFecha+' '+dbo.ClasePlantilla.ClasePlantillaHora),103) as ClaseFecha,  " +
                        " dbo.Cliente.ClienteNombre as ClienteNombre " +
                        " FROM dbo.Reserva INNER JOIN " +
                        " dbo.ClasePlantilla ON dbo.Reserva.ClasePlantillaID = dbo.ClasePlantilla.ClasePlantillaID INNER JOIN " +
                        " dbo.Cliente ON dbo.ClasePlantilla.ClienteID = dbo.Cliente.ClienteID INNER JOIN " +
                        " dbo.Clase ON dbo.ClasePlantilla.ClaseID = dbo.Clase.ClaseID " +
                        " WHERE  NOT ReservaID IN (Select ReservaID From ClaseAsistencia)  " + ViewState["sWhere"] + " " + ViewState["fecha"] + " " +
                        " ORDER BY clasefecha desc,dbo.Cliente.ClienteNombre asc, dbo.Clase.ClaseDescripcion asc";
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


    protected void ImgbtnArchivo_Click(object sender, ImageClickEventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT  " +
                    " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = dbo.Reserva.UsuarioID) as UsuarioNombre, " +
                    " (SELECT UsuarioCedula FROM Usuario WHERE UsuarioID = dbo.Reserva.UsuarioID) as UsuarioCedula, " +
                    " dbo.Clase.ClaseDescripcion as ClaseDescripcion, " +
                    " (dbo.ClasePlantilla.ClasePlantillaFecha+' '+dbo.ClasePlantilla.ClasePlantillaHora) as ClaseFecha,  " +
                    " dbo.Cliente.ClienteNombre as ClienteNombre " +
                    " FROM dbo.Reserva INNER JOIN " +
                    " dbo.ClasePlantilla ON dbo.Reserva.ClasePlantillaID = dbo.ClasePlantilla.ClasePlantillaID INNER JOIN " +
                    " dbo.Cliente ON dbo.ClasePlantilla.ClienteID = dbo.Cliente.ClienteID INNER JOIN " +
                    " dbo.Clase ON dbo.ClasePlantilla.ClaseID = dbo.Clase.ClaseID " +
                    " WHERE  NOT ReservaID IN (Select ReservaID From ClaseAsistencia)  " + ViewState["sWhere"] + " " + ViewState["fecha"] + " " +
                    " clasefecha desc, dbo.Cliente.ClienteNombre asc, dbo.Clase.ClaseDescripcion asc";

        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "UsuarioCedula";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Usuarios_No_Asistieron_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
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

    protected void dplClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplClientes.SelectedValue != "")
        {
            ViewState["sWhere"] = " AND dbo.Cliente.ClienteID = " + dplClientes.SelectedValue;
            BindGridView();
        }
    }

    protected void Filtrar_Click(object sender, EventArgs e)
    {
        string txtFechaInferior = txtFecNac1.Text;
        string txtFechaSuperior = txtFecNac2.Text;
        if (txtFechaInferior != "" && txtFechaSuperior != "")
        {
            ViewState["fecha"] = " AND (CONVERT(DATE, dbo.ClasePlantilla.ClasePlantillaFecha, 103) < CONVERT(DATE, '" + txtFechaSuperior + "', 103)) " +
                                   " AND (CONVERT(DATE, dbo.ClasePlantilla.ClasePlantillaFecha, 103) > CONVERT(DATE, '" + txtFechaInferior + "', 103)) ";
            txtFecNac1.Text = "";
            txtFecNac2.Text = "";
            BindGridView();
        }
        else
        {
            MostrarMsjModal("Debe seleccionar un intervalo", "ERR");
        }
    }
}