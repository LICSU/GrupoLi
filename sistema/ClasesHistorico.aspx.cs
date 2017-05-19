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

public partial class sistema_ClasesHistorico : System.Web.UI.Page
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
            string cmd2 = "SELECT Clase.ClaseID as ClaseID, Clase.ClaseDescripcion as ClaseDescripcion, " +
            " (Select UsuarioNombre FROM Usuario WHERE UsuarioID = ClasePlantilla.ProfesorID) as Profesor, " +
            " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha, " +
            " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora," +
            " CAST(CASE WHEN (SELECT ca.ClaseAsistActivo FROM ClaseAsistencia ca WHERE ca.ReservaID = Reserva.ReservaID) = 1 THEN 'ASISTIO'" +
            " ELSE 'NO ASISTIO' END as VARCHAR(10))as Asistio" +
            " FROM Clase INNER JOIN" +
            " ClasePlantilla ON Clase.ClaseID = ClasePlantilla.ClaseID INNER JOIN" +
            " Reserva ON ClasePlantilla.ClasePlantillaID = Reserva.ClasePlantillaID " +
            " WHERE Reserva.UsuarioID = " + _autenticado.UsuarioID + " ORDER BY CONVERT(DATE,ClasePlantilla.ClasePlantillaFecha,103) DESC, ClasePlantilla.ClasePlantillaHora DESC ";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClaseDescripcion";
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