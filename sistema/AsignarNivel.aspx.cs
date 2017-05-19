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

public partial class sistema_AsignarNivel : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    string sSelectSQL;
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            listadoClases();
        }
    }

    #region Nueva

    protected void enviarEmail(string UsuarioID, string ClaseID, string NivelID, string Accion)
    {
        string UsuarioCorreo = Utilidades.EjeSQL("SELECT UsuarioCorreo FROM Usuario WHERE UsuarioID = " + UsuarioID, cn, ref Err, true);
        //Validar Correo
        if (Utilidades.EmailValido(UsuarioCorreo))
        {
            string ClaseDescripcion = Utilidades.EjeSQL("SELECT ClaseDescripcion FROM Clase WHERE ClaseID = " + ClaseID, cn, ref Err, true);
            string NivelNombre = Utilidades.EjeSQL("SELECT NivelNombre FROM Nivel WHERE NivelID = " + NivelID, cn, ref Err, true);
            string UsuarioNombre = Utilidades.EjeSQL("SELECT UsuarioNombre FROM Usuario WHERE UsuarioID = " + UsuarioID, cn, ref Err, true);
            string ProfesorNombre = Utilidades.EjeSQL("SELECT UsuarioNombre FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
            string asunto = "Nuevo Nivel Asignado!";
            string cuerpo = "";
            if (Accion == "INS")
                cuerpo = "Sr(a) " + UsuarioNombre + "\n\n Felicitaciones el(la) Profesor(a): " + ProfesorNombre + " te ha asignado el nivel :" + NivelNombre + " para la" +
                " clase: " + ClaseDescripcion + ".\n\n Saludos.";
            else
                cuerpo = "Sr(a) " + UsuarioNombre + "\n\n Felicitaciones el(la) Profesor(a): " + ProfesorNombre + " te ha actualizado el nivel a :" + NivelNombre + " para la" +
                " clase: " + ClaseDescripcion + ".\n\n Saludos.";
            //Utilidades.EnviarCorreo(UsuarioCorreo, asunto, cuerpo, ref Err);
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

    protected void listadoClases()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT  DISTINCT(ClasePlantilla.ClaseID) as ClaseID, Clase.ClaseDescripcion as ClaseNombre "+
                           " FROM	ClasePlantilla INNER JOIN "+
                           " Clase ON ClasePlantilla.ClaseID = Clase.ClaseID INNER JOIN "+
                           " Usuario ON ClasePlantilla.ProfesorID = Usuario.UsuarioID "+
                           " WHERE  (Usuario.UsuarioID = '"+_autenticado.UsuarioID+"') "+
                           " AND YEAR(CONVERT(DATE,ClasePlantilla.ClasePlantillaFecha,103)) = YEAR(GETDATE()) " +
                           " AND MONTH(CONVERT(DATE,ClasePlantilla.ClasePlantillaFecha,103)) = MONTH(GETDATE()) " +
                           " ORDER BY Clase.ClaseDescripcion ASC";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClaseID";
            GridView3.DataKeyNames = TablaID;
            GridView3.DataSource = dt;
            GridView3.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void btnMostrarClases_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#modalClases').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView3.Rows[index];
        if (e.CommandName.Equals("seleccionarClase"))
        {
            ViewState["ClaseID"] = (gvrow.FindControl("ClaseID") as Label).Text;
            ViewState["ClaseNombre"] = (gvrow.FindControl("ClaseNombre") as Label).Text;
            listarAlumnos();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeModalClases').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
        }
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.EditIndex = -1;
        GridView3.SelectedIndex = -1;
        GridView3.PageIndex = e.NewPageIndex;
        listadoClases();
    }

    protected void listarAlumnos()
    {
        try
        {
            lblClaseSeleccionada.Text = "Ha seleccionado la Clase: <strong style='font-weight:bold;'>" + ViewState["ClaseNombre"].ToString() + ".</strong>";
            cn.Open();
            string cmd2 = "SELECT  DISTINCT(Usuario.UsuarioID), Usuario.UsuarioNombre," +
                          " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as Nombres, Usuario.UsuarioCedula, ClasePlantilla.ClaseID ," +
                          " CAST (CASE "+
                          "  WHEN (SELECT Nivel.NivelNombre FROM Nivel INNER JOIN Alumno_Nivel_Clase ON nivel.NivelID = Alumno_Nivel_Clase.NivelID "+
                          "  WHERE Alumno_Nivel_Clase.ClaseID = " + ViewState["ClaseID"].ToString() + " AND Alumno_Nivel_Clase.UsuarioID = Usuario.UsuarioID) is null " +
                          "  THEN 'No Asignado' "+
                          "  ELSE (SELECT Nivel.NivelNombre FROM Nivel INNER JOIN Alumno_Nivel_Clase ON nivel.NivelID = Alumno_Nivel_Clase.NivelID "+
                          "  WHERE Alumno_Nivel_Clase.ClaseID = " + ViewState["ClaseID"].ToString() + " AND Alumno_Nivel_Clase.UsuarioID = Usuario.UsuarioID) END AS VARCHAR) as Nivel, " +
                          "  CONVERT(VARCHAR(11),(SELECT Alumno_Nivel_Clase.AluNivClaseFechaRegistro FROM Nivel INNER JOIN Alumno_Nivel_Clase ON nivel.NivelID = Alumno_Nivel_Clase.NivelID "+
                          "  WHERE Alumno_Nivel_Clase.ClaseID = 68 AND Alumno_Nivel_Clase.UsuarioID = Usuario.UsuarioID), 103) as FechaRegistro "+
                          "  FROM Reserva INNER JOIN "+
                          "  ClasePlantilla ON Reserva.ClasePlantillaID = ClasePlantilla.ClasePlantillaID INNER JOIN "+
                          "  Usuario ON Reserva.UsuarioID = Usuario.UsuarioID AND Reserva.UsuarioID = Usuario.UsuarioID "+
                          "  WHERE (ClasePlantilla.ClaseID = " + ViewState["ClaseID"].ToString()+ ") " +
                          "  AND YEAR(CONVERT(DATE,ClasePlantilla.ClasePlantillaFecha,103)) = YEAR(GETDATE()) " +
                          "  AND MONTH(CONVERT(DATE,ClasePlantilla.ClasePlantillaFecha,103)) = MONTH(GETDATE()) " + ViewState["sWhere"] +
                          "  ORDER BY  Usuario.UsuarioNombre ASC, Usuario.UsuarioCedula ASC";
            
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
        listarAlumnos();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("ModificarNivel"))
        {
            sSelectSQL = "SELECT NivelID AS VAL, NivelNombre AS TXT FROM Nivel ORDER BY TXT";
            Utilidades.CargarListado(ref dplNivelMod, sSelectSQL, cn, ref Err, true);
            hdfUsuarioID.Value = (gvrow.FindControl("UsuarioID") as Label).Text;
            txtClaseMod.Text = ViewState["ClaseNombre"].ToString();
            txtAlumnoMod.Text = (gvrow.FindControl("UsuarioNombre") as Label).Text + " - " + (gvrow.FindControl("UsuarioCedula") as Label).Text;
            string NivelNombre = (gvrow.FindControl("Nivel") as Label).Text;
            if (NivelNombre != "No Asignado")
            {
                string NivelID = Utilidades.EjeSQL("SELECT NivelID FROM Nivel WHERE NivelNombre = '"+NivelNombre+"'", cn, ref Err, true);
                dplNivelMod.SelectedValue = NivelID;
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
         string NivelID = dplNivelMod.SelectedValue;
         string UsuarioID = hdfUsuarioID.Value;
         string ClaseID = ViewState["ClaseID"].ToString();
         string AluNivClaseID = Utilidades.EjeSQL("SELECT AluNivClaseID From Alumno_Nivel_Clase WHERE UsuarioID = " + UsuarioID+" AND ClaseID = "+ClaseID, cn, ref Err, true);

         if (AluNivClaseID == "" || AluNivClaseID == "-1")
         {
             sSelectSQL = "INSERT INTO Alumno_Nivel_Clase (UsuarioID, ClaseID, NivelID, AluNivClaseFechaRegistro, AluNivClaseUsuarioRegistro, ProfesorID)" +
                          " VALUES (" + UsuarioID + ", " + ClaseID + ", " + NivelID + ", SYSDATETIME(), " + _autenticado.UsuarioID + ", " + _autenticado.UsuarioID + ")";
             Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
         }
         else
         {
             sSelectSQL = "UPDATE Alumno_Nivel_Clase SET NivelID = " + NivelID + " WHERE AluNivClaseID = " + AluNivClaseID;
             Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
         }

         if (Err == "")
         {
             listarAlumnos();
             //enviarEmail(UsuarioID, ClaseID, NivelID, "MOD");
             MostrarMsjModal("Registro Editado exitosamente", "EXI");
             System.Text.StringBuilder sb = new System.Text.StringBuilder();
             sb.Append(@"<script type='text/javascript'>");
             sb.Append("document.getElementById('closeEdit').click();");
             sb.Append(@"</script>");
             ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
         }
         else
         {
             MostrarMsjModal("Error al Modificar el Registro", "ERR");
         }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BUSCAR
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            {
                ViewState["sWhere"] = "AND (Usuario.UsuarioNombre LIKE '%" + sBuscar + "%'" +
                                         " OR Usuario.UsuarioCedula LIKE '%" + sBuscar + "%')";
            }
            else
            { ViewState["sWhere"] = ""; }
            listarAlumnos();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    #endregion

    
}