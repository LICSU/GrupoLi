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

public partial class sistema_ListadoProvisional : System.Web.UI.Page
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
                Utilidades.CargarListado(ref dplClientes, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente WHERE ClienteID > 1 ORDER BY VAL ", cn, ref Err, true);
            }
            else if (_autenticado.RolID == "4")
            {
                Utilidades.CargarListado(ref dplClientes, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente WHERE ClienteID > 1 ORDER BY VAL ", cn, ref Err, true);
                dplClientes.SelectedValue = _autenticado.ClienteID;
                dplClientes.Enabled = false;
                ViewState["sWhere"] = "AND dbo.UsuarioRol.ClienteID = " + _autenticado.ClienteID;
            }
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Close();
            cn.Open();
            string cmd2 = "SELECT  " +
                          " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres, Usuario.UsuarioCedula as UsuarioCedula, " +
                          " Cliente.ClienteNombre as ClienteNombre,  " +
                          "  " +
                          " CONVERT(VARCHAR(11),PlanEmpresa.MesProximo,103) as MesProximo " +
                          " FROM PlanEmpresa INNER JOIN Usuario ON PlanEmpresa.UsuarioID = " +
                          " Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = " +
                          " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID " +
                          " WHERE (PlanEmpresa.EstadoProximo = 1) " + ViewState["sWhere"] + " ORDER BY Cliente.ClienteNombre, Usuario.UsuarioCedula ASC ";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioCedula";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            //Attribute to show the Plus Minus Button.
            if (dt.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.             
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void dplClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplClientes.SelectedValue != "")
        {
            ViewState["sWhere"] = " AND dbo.UsuarioRol.ClienteID = " + dplClientes.SelectedValue;
            BindGridView();
        }
    }

    protected void ImgbtnArchivo_Click(object sender, ImageClickEventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT  " +
                          " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres, Usuario.UsuarioCedula as UsuarioCedula, " +
                          " Cliente.ClienteNombre as ClienteNombre,  " +
                          "  " +
                          " CONVERT(VARCHAR(11),PlanEmpresa.MesProximo,103) as MesProximo " +
                          " FROM PlanEmpresa INNER JOIN Usuario ON PlanEmpresa.UsuarioID = " +
                          " Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = " +
                          " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID " +
                          " WHERE (PlanEmpresa.EstadoProximo = 1) " + ViewState["sWhere"] + " ORDER BY Cliente.ClienteNombre, Usuario.UsuarioCedula ASC ";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=listado_provisional_" + Hora + ".xls");
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
}