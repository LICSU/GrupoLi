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

public partial class sistema_Autorizacion : System.Web.UI.Page
{

    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "", sSelect = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
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
            cn.Open();
            string cmd2 = "SELECT DISTINCT (dbo.Usuario.UsuarioCedula) as UsuarioCedula, (dbo.Usuario.UsuarioNombre+' '+dbo.Usuario.UsuarioApellido) as UsuarioNombre," +
                        " AutorizacionID  as AutorizacionID, CAST(CASE WHEN planAutorizacion.AutorizacionActivo = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END AS VARCHAR) as AutorizacionActivoV, " +
                        " planAutorizacion.AutorizacionActivo as AutorizacionActivo" +
                        " FROM  planAutorizacion INNER JOIN " +
                        " dbo.Usuario ON planAutorizacion.UsuarioID = dbo.Usuario.UsuarioID " + ViewState["sWhere"];
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "AutorizacionID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            GridView1.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.   
            GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";


            //Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool AutorizacionActivo = chkBloquearEdit.Checked;
        string valor = "";
        if (AutorizacionActivo)
            valor = "1";
        else
            valor = "0";
        string AutorizacionID = hAutorizacionMod.Value;
        sSelect = "UPDATE planAutorizacion SET AutorizacionActivo = "+valor+" WHERE AutorizacionID = " + AutorizacionID;
        Utilidades.EjeSQL(sSelect, cn, ref Err, false);
        if (Err == "")
        {
            BindGridView();
            MostrarMsjModal("Registro Modificado Exitosamente", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeAdd').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
        }
        else
        {
            MostrarMsjModal("Error al Modificar el Registro : " + Err, "ERR");
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
            hAutorizacionMod.Value = (gvrow.FindControl("AutorizacionID") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("UsuarioNombre") as Label).Text;
            txtCedulaEdit.Text = (gvrow.FindControl("UsuarioCedula") as Label).Text;
            chkBloquearEdit.Checked = (gvrow.FindControl("AutorizacionActivo") as CheckBox).Checked;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " WHERE (UsuarioCedula LIKE '%" + sBuscar + "%' OR UsuarioNombre LIKE '%" + sBuscar + "%')"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }
}