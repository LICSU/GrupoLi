using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

public partial class sistema_TotalEmpleados : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    DataTable dt;
    string sSelect = "", Err = "", sSelectSQL = "";
    GridView grid;
    SqlDataAdapter dAdapter;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Close();
            cn.Open();
            string cmd2 = "SELECT EmpleadoEmp.EmpleadoID as EmpleadoID, "
                           + " EmpleadoEmp.EmpleadoCedula as EmpleadoCedula, "
                           + " EmpleadoEmp.EmpleadoNombre as EmpleadoNombre, "
                           + " EmpleadoEmp.EmpleadoCargo as EmpleadoCargo, "
                           + " EmpleadoEmp.EmpleadoEmail as EmpleadoEmail, "
                           + " EmpleadoEmp.EmpleadoFechaIng as EmpleadoFechaIng, "
                           + " Cliente.ClienteNombre as ClienteNombre "
                           + " FROM Cliente INNER JOIN"
                           + " EmpleadoEmp ON Cliente.ClienteID = EmpleadoEmp.ClienteID WHERE Cliente.ClienteID = " + _autenticado.ClienteID + " " + ViewState["sWhere"];
            dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "EmpleadoID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            //Attribute to show the Plus Minus Button.
            GridView1.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.                
            GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("editRecord"))
        {
            hdfEmpleadoIDEdit.Value = (gvrow.FindControl("EmpleadoID") as Label).Text;
            txtCedulaEdit.Text = (gvrow.FindControl("EmpleadoCedula") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("EmpleadoNombre") as Label).Text;
            txtCargoEdit.Text = (gvrow.FindControl("EmpleadoCargo") as Label).Text;
            txtEmailEdit.Text = (gvrow.FindControl("EmpleadoEmail") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("deleteRecord"))
        {
            hdfEmpleadoIDDel.Value = (gvrow.FindControl("EmpleadoID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " AND (Cliente.ClienteNombre LIKE '%" + sBuscar + "%' OR EmpleadoEmp.EmpleadoCedula LIKE '%" + sBuscar + "%' OR EmpleadoEmp.EmpleadoNombre LIKE '%" + sBuscar + "%')"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string EmpleadoID = hdfEmpleadoIDDel.Value;
        cn.Open();
        string SQL_1 = "DELETE FROM EmpleadoEmp Where EmpleadoEmpID = " + EmpleadoID;
        SqlCommand addCmd = new SqlCommand(SQL_1, cn);
        int iRes = addCmd.ExecuteNonQuery();
        cn.Close();
        if (iRes > 0)
        {
            BindGridView();
            MostrarMsjModal("Registro eliminado de la base de datos", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeDelete').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string EmpleadoID = hdfEmpleadoIDEdit.Value;
        string Nombre = txtNombreEdit.Text;
        string Cargo = txtCargoEdit.Text;
        string Email = txtEmailEdit.Text;
        int iRes = 0;
        cn.Open();
        string sSelectSQL = "UPDATE EmpleadoEmp SET EmpleadoEmp.EmpleadoNombre = '" + Nombre + "', EmpleadoEmp.EmpleadoCargo = '" + Cargo + "',"
                            + " EmpleadoEmp.EmpleadoEmail = '" + Email + "' WHERE EmpleadoEmp.EmpleadoID = " + EmpleadoID;
        SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
        try { iRes = addCmd.ExecuteNonQuery(); }
        catch (SqlException sq)
        {
            Err += sq.Message;
            MostrarMsjModal("Error: " + Err, "ERR");
        }
        cn.Close();
        if (iRes > 0)
        {
            BindGridView();
            MostrarMsjModal("Registro Modificado exitosamente", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeEdit').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }
    }

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT "
                           + " EmpleadoEmp.EmpleadoCedula as EmpleadoCedula, "
                           + " EmpleadoEmp.EmpleadoNombre as EmpleadoNombre, "
                           + " EmpleadoEmp.EmpleadoCargo as EmpleadoCargo, "
                           + " EmpleadoEmp.EmpleadoEmail as EmpleadoEmail, "
                           + " EmpleadoEmp.EmpleadoFechaIng as EmpleadoFechaIng, "
                           + " Cliente.ClienteNombre as ClienteNombre "
                           + " FROM Cliente INNER JOIN"
                           + " EmpleadoEmp ON Cliente.ClienteID = EmpleadoEmp.ClienteID WHERE Cliente.ClienteID = " + _autenticado.ClienteID + " " + ViewState["sWhere"];
        dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "EmpleadoCedula";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=total_empleados_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }
}