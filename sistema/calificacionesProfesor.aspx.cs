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
using System.Collections;

public partial class sistema_calificacionesProfesor : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = string.Empty, sSelecSQL = string.Empty;
    DataSet ds;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarLista();
        }
    }

    protected void cargarLista()
    {
        sSelecSQL = "SELECT  (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as TXT, " +
                    "Usuario.UsuarioID as VAL " +
                    "FROM Rol INNER JOIN " +
                    "UsuarioRol ON Rol.RolID = UsuarioRol.RolID INNER JOIN " +
                    "Usuario ON UsuarioRol.UsuarioID = Usuario.UsuarioID " +
                    "WHERE Rol.RolID = 2 " +
                    "ORDER BY TXT ASC";
        Utilidades.CargarListado(ref dplProfesor, sSelecSQL, cn, ref Err, true);
        sSelecSQL = " select DISTINCT(CONVERT(INT,MONTH(ClasePlantillaFecha))) as VAL, " +
                    " ((DATENAME(MONTH,ClasePlantillaFecha))) as TXT " +
                    " FROM ClasePlantilla WHERE YEAR(ClasePlantillaFecha) = YEAR(GETDATE())" +
                    " ORDER BY VAL ASC";
        Utilidades.CargarListado(ref ddlMeses, sSelecSQL, cn, ref Err, true);
    }
    protected void BindGridView()
    {
        string[] clases = cargarClases();
        string[] alumnos = cargarAlumnos(clases);
        string[] promedios = cargarPromedios(clases);
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Clase", typeof(string)));
        dt.Columns.Add(new DataColumn("Alumnos", typeof(string)));
        dt.Columns.Add(new DataColumn("Promedios", typeof(string)));
        for (int i = 0; i < clases.Length; i++)
        {
            sSelecSQL = "SELECT ClaseDescripcion FROM Clase WHERE ClaseID = " + clases[i];
            dr = dt.NewRow();
            dr["Clase"] = Utilidades.EjeSQL(sSelecSQL, cn, ref Err, true);
            dr["Alumnos"] = alumnos[i];
            dr["Promedios"] = promedios[i];
            dt.Rows.Add(dr);
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();
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

    protected void ddlProfesor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplProfesor.SelectedValue != "")
        {
            ViewState["ProfesorID"] = dplProfesor.SelectedValue;
            BindGridView();
        }
        else
        {
            MostrarMsjModal("Debe seleccionar un profesor", "ERR");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected string[] cargarClases()
    {
        string[] data = new string[0];

        sSelecSQL = "SELECT COUNT(DISTINCT(Clase.ClaseID))" +
                    " FROM Clase INNER JOIN " +
                    " Alumno_Nivel_Clase ON Clase.ClaseID = Alumno_Nivel_Clase.ClaseID " +
                    " WHERE Alumno_Nivel_Clase.ProfesorID = " + ViewState["ProfesorID"];
        string tamaño = Utilidades.EjeSQL(sSelecSQL, cn, ref Err, true);
        if (tamaño != "" || tamaño != "0")
        {
            int tamVector = int.Parse(tamaño.Trim());
            data = new string[tamVector];

            sSelecSQL = " SELECT DISTINCT(Clase.ClaseID) as Clase " +
                        " FROM Clase INNER JOIN " +
                        " Alumno_Nivel_Clase ON Clase.ClaseID = Alumno_Nivel_Clase.ClaseID " +
                        " WHERE Alumno_Nivel_Clase.ProfesorID = " + ViewState["ProfesorID"];
            int contador = 0;
            cn.Open();
            SqlCommand cmd = new SqlCommand(sSelecSQL, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    data[contador] = dr["Clase"].ToString();
                    contador++;
                }
                dr.Close();
                cn.Close();
            }
            catch (SqlException sq)
            {
                Err = sq.Message;
                cn.Close();
            }
        }

        return data;
    }

    private string[] cargarAlumnos(string[] clases)
    {
        string[] data = new string[clases.Length];
        for (int i = 0; i < data.Length; i++)
        {
            sSelecSQL = "SELECT COUNT(*) " +
                        " FROM Reserva" +
                        " WHERE Reserva.ClasePlantillaID IN (SELECT ClasePlantillaID " +
                        " FROM ClasePlantilla " +
                        " WHERE ClaseID = " + clases[i] + " AND ProfesorID = " + ViewState["ProfesorID"] +
                        "" + ViewState["Mes"] + ")";
            data[i] = Utilidades.EjeSQL(sSelecSQL, cn, ref Err, true);
        }

        return data;
    }
    private string[] cargarPromedios(string[] clases)
    {
        string[] data = new string[clases.Length];
        if (ddlMeses.SelectedValue != "")
        {
            ViewState["Mes1"] = " AND MONTH(AlumNivClasElemFechaReg) = " + ddlMeses.SelectedValue;
        }
        for (int i = 0; i < data.Length; i++)
        {
            sSelecSQL = " SELECT AVG(CONVERT(INT,Calificacion.CalificacionNombre)) as PROMEDIO " +
                        " FROM Alumno_Nivel_Clase_Elemento INNER JOIN " +
                        " Alumno_Nivel_Clase ON Alumno_Nivel_Clase_Elemento.AluNivClaseID = Alumno_Nivel_Clase.AluNivClaseID INNER JOIN " +
                        " Calificacion ON Alumno_Nivel_Clase_Elemento.CalificacionID = Calificacion.CalificacionID " +
                        " WHERE Alumno_Nivel_Clase.ClaseID = " + clases[i] + " " +
                        " AND Alumno_Nivel_Clase.ProfesorID = " + ViewState["ProfesorID"] + "" + ViewState["Mes1"];
            data[i] = Utilidades.EjeSQL(sSelecSQL, cn, ref Err, true);
        }

        return data;
    }

    protected void ddlMeses_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMeses.SelectedValue != "")
        {
            ViewState["Mes"] = " AND MONTH(ClasePlantillaFecha) = " + ddlMeses.SelectedValue;
            BindGridView();
        }
        else
        {
            MostrarMsjModal("Debe Seleccionar un Mes", "ERR");
        }
    }

    protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        string[] clases = cargarClases();
        string[] alumnos = cargarAlumnos(clases);
        string[] promedios = cargarPromedios(clases);
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Clase", typeof(string)));
        dt.Columns.Add(new DataColumn("Alumnos", typeof(string)));
        dt.Columns.Add(new DataColumn("Promedios", typeof(string)));
        for (int i = 0; i < clases.Length; i++)
        {
            sSelecSQL = "SELECT ClaseDescripcion FROM Clase WHERE ClaseID = " + clases[i];
            dr = dt.NewRow();
            dr["Clase"] = Utilidades.EjeSQL(sSelecSQL, cn, ref Err, true);
            dr["Alumnos"] = alumnos[i];
            dr["Promedios"] = promedios[i];
            dt.Rows.Add(dr);
        }

        grid.DataSource = dt;
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Calificaciones_Profesor" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

}