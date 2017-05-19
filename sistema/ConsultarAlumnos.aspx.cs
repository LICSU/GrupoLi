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

public partial class sistema_ConsultarAlumnos : System.Web.UI.Page
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

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT DISTINCT(Reserva.UsuarioID) as Usuario, ClasePlantilla.ClaseID as ClaseID, " +
                            " (SELECT ClaseDescripcion FROM Clase WHERE ClaseID =  ClasePlantilla.ClaseID) as ClaseDescripcion, " +
                            " ClasePlantilla.ProfesorID as ProfesorID, " +
                            " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID =  Reserva.UsuarioID) as UsuarioNombre, " +
                            " (SELECT UsuarioFoto FROM Usuario WHERE UsuarioID =  Reserva.UsuarioID) as UsuarioFoto, " +
                            " (SELECT UsuarioCorreo FROM Usuario WHERE UsuarioID =  Reserva.UsuarioID) as UsuarioEmail, " +
                            " (SELECT UsuarioCedula FROM Usuario WHERE UsuarioID =  Reserva.UsuarioID) as UsuarioCedula, " +
                            " (SELECT TOP 1 Nivel.NivelNombre FROM Alumno_Nivel_Clase INNER JOIN "+
                            " Nivel ON Alumno_Nivel_Clase.NivelID = Nivel.NivelID AND UsuarioID = Reserva.UsuarioID " +
                            " AND ClaseID = ClasePlantilla.ClaseID ORDER BY AluNivClaseID DESC) as Nivel," +
                            " (SELECT UsuarioCelular1 FROM Usuario WHERE UsuarioID =  Reserva.UsuarioID) as UsuarioCelular, " +
                            " (SELECT CONVERT(VARCHAR(11), UsuarioFechaRegistro, 103) FROM Usuario WHERE UsuarioID =  Reserva.UsuarioID) as UsuarioFechaRegistro, " +
                            " (SELECT CAST(CASE WHEN UsuarioObservacion = '' THEN 'Ninguna' ELSE UsuarioObservacion END AS varchar(200)) as UsuarioObservacion FROM Usuario WHERE UsuarioID =  Reserva.UsuarioID) as UsuarioObservacion, " +
                            " Reserva.UsuarioID as UsuarioID" +
                            " FROM ClasePlantilla INNER JOIN" +
                            " Reserva ON ClasePlantilla.ClasePlantillaID " +
                            " = Reserva.ClasePlantillaID WHERE ClasePlantilla.ProfesorID = " + _autenticado.UsuarioID + " " + ViewState["sWhere"] + " ORDER BY ClasePlantilla.ClaseID DESC";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Usuario";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            if (dt.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                GridView1.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.                
                GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
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

    protected void VerFoto(object sender, CommandEventArgs e)
    {
        
        if (e.CommandName == "VerFoto")
        {
            string a = e.CommandArgument.ToString();
            MostrarFotoModal(a);
        }
    }

    private void MostrarFotoModal(string foto)
    {
        imgFoto.ImageUrl = foto;
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarFotoModal", "MostrarFotoModal();", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            {
                ViewState["sWhere"] = "AND ((SELECT UsuarioCedula FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) LIKE '%" + sBuscar + "%'" +
                  " OR (SELECT UsuarioNombre FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) LIKE '%" + sBuscar + "%'" +
                  " OR (SELECT ClaseDescripcion FROM Clase WHERE ClaseID =  ClasePlantilla.ClaseID) LIKE '%" + sBuscar + "%'" +
                  " OR (SELECT UsuarioApellido FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) LIKE '%" + sBuscar + "%' )";
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}