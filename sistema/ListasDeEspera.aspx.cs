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

public partial class sistema_ListasDeEspera : System.Web.UI.Page
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
        BindGridView();
    }
    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT DISTINCT(le.ListaEsperaID) as ListaEsperaID, " +
                           " (SELECT ClaseDescripcion FROM Clase WHERE ClaseID = (SELECT ClaseID FROM ClasePlantilla WHERE ClasePlantillaID = le.ListaEsperaClaseID)) as ClaseDescripcion, " +
                           " (SELECT (ClasePlantillaFecha+' '+ClasePlantillaHora) FROM ClasePlantilla WHERE ClasePlantillaID = le.ListaEsperaClaseID) as FechaClase , " +
                           " le.ListaEsperaEstado as CorreoEnviado " +
                           " FROM ListaEspera le, ClasePlantilla p, Clase c " +
                           " WHERE le.ListaEsperaUsuarioID = " + _autenticado.UsuarioID + " " + ViewState["sWhere"] + " ORDER BY le.ListaEsperaID DESC";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ListaEsperaID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            //Attribute to show the Plus Minus Button.
            if (dt.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.                
                GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " AND ((SELECT ClaseDescripcion FROM Clase WHERE ClaseID = (SELECT ClaseID FROM ClasePlantilla WHERE ClasePlantillaID = le.ListaEsperaClaseID))) LIKE '%" + sBuscar + "%'"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
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
        if (e.CommandName.Equals("deleteRecord"))
        {
            //Eliminar Registro
            GridViewRow gvrow = GridView1.Rows[index];
            hClaseDel.Value = (gvrow.FindControl("ListaEsperaID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Err = "";
        string sSelectSQL = "DELETE FROM ListaEspera Where ListaEsperaID = " + hClaseDel.Value;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        if (Err == "")
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