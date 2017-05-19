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

public partial class sistema_CalificacionesAlumno : System.Web.UI.Page
{
    string Err = "", Rol = "", sSelectSQL = "";
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;

    protected void Page_Load(object sender, EventArgs e)
    {

        _autenticado = new UsuarioAutenticado(fIdentity);
        Rol = _autenticado.RolID;
        if (!IsPostBack)
        {
            sSelectSQL = "SELECT DISTINCT(dbo.Clase.ClaseID) as VAL, " +
                        " dbo.Clase.ClaseDescripcion as TXT" +
                        " FROM dbo.Clase INNER JOIN" +
                        " dbo.ClasePlantilla ON dbo.Clase.ClaseID = dbo.ClasePlantilla.ClaseID INNER JOIN" +
                        " dbo.Reserva ON dbo.ClasePlantilla.ClasePlantillaID = dbo.Reserva.ClasePlantillaID" +
                        " WHERE (dbo.Reserva.UsuarioID = " + _autenticado.UsuarioID + ")" +
                        " ORDER BY dbo.Clase.ClaseDescripcion ASC";
            Utilidades.CargarListado(ref dplClases, sSelectSQL, cn, ref Err, true);
        }
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = dbo.Alumno_Nivel_Clase.UsuarioID) as Usuario, " +
                            " (SELECT ClaseDescripcion FROM Clase WHERE ClaseID = dbo.Alumno_Nivel_Clase.ClaseID) as ClaseDescripcion, " +
                            " dbo.Elemento.ElementoNombre as Elemento, dbo.Alumno_Nivel_Clase.UsuarioID as UsuarioID, " +
                            " dbo.Calificacion.CalificacionNombre as Calificacion, " +
                            " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID=dbo.Alumno_Nivel_Clase.ProfesorID) as Profesor," +
                            " CONVERT(VARCHAR(11),dbo.Alumno_Nivel_Clase_Elemento.AlumNivClasElemFechaReg,103) as Fecha," +
                            " CONVERT(VARCHAR(5),dbo.Alumno_Nivel_Clase_Elemento.AlumNivClasElemFechaReg,108) as Hora" +
                            " FROM dbo.Alumno_Nivel_Clase INNER JOIN" +
                            " dbo.Alumno_Nivel_Clase_Elemento ON dbo.Alumno_Nivel_Clase.AluNivClaseID = dbo.Alumno_Nivel_Clase_Elemento.AluNivClaseID INNER JOIN" +
                            " dbo.Calificacion ON dbo.Alumno_Nivel_Clase_Elemento.CalificacionID = dbo.Calificacion.CalificacionID INNER JOIN" +
                            " dbo.Clase_Nivel_Elemento ON dbo.Alumno_Nivel_Clase_Elemento.ClaseElemNivID = dbo.Clase_Nivel_Elemento.ClaseElemNivID AND " +
                            " dbo.Alumno_Nivel_Clase_Elemento.ClaseElemNivID = dbo.Clase_Nivel_Elemento.ClaseElemNivID INNER JOIN" +
                            " dbo.Elemento ON dbo.Clase_Nivel_Elemento.ElementoID = dbo.Elemento.ElementoID" +
                            " WHERE dbo.Alumno_Nivel_Clase.UsuarioID = " + _autenticado.UsuarioID +
                            " " + ViewState["condicion"] +
                            " ORDER BY CONVERT(DATETIME,dbo.Alumno_Nivel_Clase.AluNivClaseFechaRegistro,103) DESC";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioID";
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

    protected void dplClases_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplClases.SelectedValue != "")
        {
            ViewState["condicion"] = " AND dbo.Alumno_Nivel_Clase.ClaseID = " + dplClases.SelectedValue;
            BindGridView();
        }
        else
        {
            ViewState["condicion"] = "";
            BindGridView();
        }
    }
}