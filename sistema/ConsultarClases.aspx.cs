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

public partial class sistema_ConsultarClases : System.Web.UI.Page
{
    string Err = "";
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Clase.ClaseDescripcion as ClaseDescripcion, " +
                        " Clase.ClaseTipo as ClaseTipo, " +
                        " ClasePlantilla.ClaseID as ClaseID," +
                        " TipCalificacion.TipoCalificacionDescripcion as TipoCalificacionDescripcion, " +
                        " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha, " +
                        " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora, " +
                        " Cliente.ClienteNombre as ClienteNombre, " +
                        " ClasePlantilla.ProfesorID " +
                        " FROM ClasePlantilla INNER JOIN " +
                        " Clase ON ClasePlantilla.ClaseID = Clase.ClaseID INNER JOIN " +
                        " TipCalificacion ON Clase.TipoCalificacionID = TipCalificacion.TipoCalificacionID INNER JOIN " +
                        " Cliente ON ClasePlantilla.ClienteID = Cliente.ClienteID " +
                        " WHERE (ClasePlantilla.ProfesorID = " + _autenticado.UsuarioID + " " + ViewState["sWhere"] + ")";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClaseID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            if (dt.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.                
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            {
                ViewState["sWhere"] = " AND Clase.ClaseDescripcion LIKE '%" + sBuscar + "%'" +
                  " OR Clase.ClaseTipo LIKE '%" + sBuscar + "%'" +
                  " OR Cliente.ClienteNombre LIKE '%" + sBuscar + "%'" +
                  " OR ClasePlantilla.ClasePlantillaFecha LIKE '%" + sBuscar + "%' )";
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