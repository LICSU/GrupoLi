using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Security;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data.OleDb;
using System.Drawing;
using System.Web.UI.HtmlControls;

public partial class sistema_Pagos : System.Web.UI.Page
{
    string Err = "", sSelectSQL = "";
    GridView grid;
    DataSet ds;
    DataTable dt;
    string cedulaAlumno = "";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cedulaAlumno = Request.QueryString["user"];
            if (cedulaAlumno != "")
            {
                ViewState["sWhere"] = " WHERE (Usuario.UsuarioCedula LIKE '%" + cedulaAlumno + "%')";
                BindGridView();
            }
            else
            {
                BindGridView();
                ViewState["sWhere"] = "";
                ViewState["wFechas"] = "";
            }

        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Usuario.UsuarioNombre as UsuarioNombre, " +
                           " Usuario.UsuarioID as UsuarioID, " +
                           " Usuario.UsuarioApellido as UsuarioApellido, " +
                           " Usuario.UsuarioCedula as UsuarioCedula, " +
                           " PlanAlumno.PlanAlumnoID as PlanAlumnoID, " +
                           " [Plan].PlanNombre as PlanNombre, " +
                           " PlanPago.ClasePagoID as ClasePagoID," +
                           " PlanPago.FacturaNumero as FacturaNumero," +
                           " PlanPago.FacturaMonto as FacturaMonto," +
                           " CONVERT(VARCHAR(11), PlanPago.FacturaFecha, 103) as FacturaFecha " +
                           " FROM [Plan] INNER JOIN " +
                           " PlanAlumno ON [Plan].PlanID = PlanAlumno.PlanID INNER JOIN " +
                           " PlanPago ON PlanAlumno.PlanAlumnoID = PlanPago.PlanAlumnoID INNER JOIN " +
                           " Usuario ON PlanAlumno.UsuarioID = Usuario.UsuarioID " + ViewState["sWhere"] + ViewState["wFechas"];
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClasePagoID";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            {
                ViewState["sWhere"] = " WHERE (Usuario.UsuarioCedula LIKE '%" + sBuscar + "%' OR " +
                                      " Usuario.UsuarioNombre LIKE '%" + sBuscar + "%' OR " +
                                      " [Plan].PlanNombre LIKE '%" + sBuscar + "%' OR " +
                                      " Usuario.UsuarioApellido LIKE '%" + sBuscar + "%')";
            }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected void Agregar_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("verBalance"))
        {
            //Ver el balance
            string cedula = (gvrow.FindControl("UsuarioCedula") as Label).Text;
            Response.Redirect("DetallesUsuario.aspx?user=" + cedula);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //Eliminar
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

    protected void UsuarioCedula_Click(object sender, EventArgs e)
    {
    }

    protected void Filtrar_Click(object sender, EventArgs e)
    {
        string txtFechaInferior = txtFecNac1.Value;
        string txtFechaSuperior = txtFecNac2.Value;
        if (txtFechaInferior != "" && txtFechaSuperior != "")
        {
            ViewState["wFechas"] = " AND (PlanPago.FacturaFecha <= '" + txtFechaSuperior + "' AND PlanPago.FacturaFecha >= '" + txtFechaInferior + "')";
        }
        else if (txtFechaInferior == "" && txtFechaSuperior != "")
        {
            ViewState["wFechas"] = " AND (PlanPago.FacturaFecha <= '" + txtFechaSuperior + "')";
        }
        else if (txtFechaInferior != "" && txtFechaSuperior == "")
        {
            ViewState["wFechas"] = " AND ( PlanPago.FacturaFecha >= '" + txtFechaInferior + "')";
        }
        BindGridView();
    }

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT Usuario.UsuarioNombre as UsuarioNombre, " +
                       " Usuario.UsuarioApellido as UsuarioApellido, " +
                       " Usuario.UsuarioCedula as UsuarioCedula, " +
                       " [Plan].PlanNombre as PlanNombre, " +
                       " PlanPago.FacturaNumero as FacturaNumero," +
                       " PlanPago.FacturaMonto as FacturaMonto," +
                       " PlanPago.FacturaFecha as FacturaFecha " +
                       " FROM [Plan] INNER JOIN " +
                       " PlanAlumno ON [Plan].PlanID = PlanAlumno.PlanID INNER JOIN " +
                       " PlanPago ON PlanAlumno.PlanAlumnoID = PlanPago.PlanAlumnoID INNER JOIN " +
                       " Usuario ON PlanAlumno.UsuarioID = Usuario.UsuarioID " + ViewState["sWhere"] + ViewState["wFechas"];
        //MostrarMsjModal(cmd2, "");
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "UsuarioNombre";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=pagos_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}