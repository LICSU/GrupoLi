using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sistema_InicioProfesor : System.Web.UI.Page
{
    #region Variables
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string sSQL = string.Empty;
    string Err = string.Empty;
    string Clase = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            cargarUsuarios();
        }
    }

    #region CargarUsuarios
    protected void cargarUsuarios()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Usuario.UsuarioID as UsuarioID, Usuario.UsuarioFoto as Foto, Usuario.UsuarioCelular1 as Celular, "+
                          " (SELECT Observacion FROM Clase_Observacion_Usuario WHERE UsuarioID =  Usuario.UsuarioID AND ClaseID = ClasePlantilla.ClaseID) as Observacion,"+
                          " ClasePlantilla.ClasePlantillaID as ClasePlantillaID, " +
                          " ClasePlantilla.ClaseID as ClaseID, " +
                          " CONVERT(VARCHAR(11), ClasePlantilla.ClasePlantillaFecha ,103)+' '+ClasePlantilla.ClasePlantillaHora AS Fecha," +
                          " Usuario.UsuarioCedula as Cedula, Reserva.ReservaID as ReservaID, "+
                          " (Usuario.UsuarioNombre+ +Usuario.UsuarioApellido) as UsuarioNom, "+
                          " (SELECT ClaseDescripcion FROM Clase WHERE ClaseID = ClasePlantilla.ClaseID ) as Clase, "+
                          " (SELECT COUNT(*) FROM ClasePlantilla INNER JOIN Reserva ON ClasePlantilla.ClasePlantillaID = Reserva.ClasePlantillaID "+
                          " WHERE Reserva.UsuarioID = Usuario.UsuarioID) AS CantReservas, "+
                          "(SELECT Nivel.NivelID FROM Nivel INNER JOIN Alumno_Nivel_Clase ON nivel.NivelID = Alumno_Nivel_Clase.NivelID " +
                          " WHERE Alumno_Nivel_Clase.ClaseID = ClasePlantilla.ClaseID AND Alumno_Nivel_Clase.UsuarioID = Usuario.UsuarioID) as NivelID," +
                          " CAST (CASE WHEN (SELECT Nivel.NivelNombre FROM Nivel INNER JOIN Alumno_Nivel_Clase ON nivel.NivelID = Alumno_Nivel_Clase.NivelID "+
                          " WHERE Alumno_Nivel_Clase.ClaseID = ClasePlantilla.ClaseID AND Alumno_Nivel_Clase.UsuarioID = Usuario.UsuarioID) is null THEN 'No Asignado' ELSE "+
                          " (SELECT Nivel.NivelNombre FROM Nivel INNER JOIN Alumno_Nivel_Clase ON nivel.NivelID = Alumno_Nivel_Clase.NivelID "+
                          " WHERE Alumno_Nivel_Clase.ClaseID = ClasePlantilla.ClaseID AND Alumno_Nivel_Clase.UsuarioID = Usuario.UsuarioID) END AS VARCHAR) as Nivel, "+
                          " CAST(CASE WHEN (SELECT ClaseAsistActivo FROM ClaseAsistencia WHERE ClaseAsistenciaPlantillaID = ClasePlantilla.ClasePlantillaID AND ClaseAsistenciaUsuarioID = Usuario.UsuarioID) "+
                          " is null THEN 0 ELSE (SELECT ClaseAsistActivo FROM ClaseAsistencia WHERE ClaseAsistenciaPlantillaID = ClasePlantilla.ClasePlantillaID "+
                          " AND ClaseAsistenciaUsuarioID = Usuario.UsuarioID) "+
                          " END AS BIT) AS Asistio FROM Reserva INNER JOIN Usuario ON Reserva.UsuarioID = Usuario.UsuarioID INNER JOIN ClasePlantilla "+
                          " ON Reserva.ClasePlantillaID = ClasePlantilla.ClasePlantillaID WHERE	ClasePlantilla.ProfesorID = " + _autenticado.UsuarioID + " AND " +
                          " CONVERT(DATE, ClasePlantilla.ClasePlantillaFecha, 103) = CONVERT(DATE, SYSDATETIME(), 103) " +
                          " ORDER BY ClasePlantilla.ClasePlantillaFecha, ClasePlantilla.ClasePlantillaHora, Usuario.UsuarioNombre ASC";

            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioID";
            GridView2.DataKeyNames = TablaID;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }
    #endregion

    #region Cargar Elementos a evaluar
    protected void cargarElementosAEvaluar()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Nivel.NivelID as NivelID, "+
                           " Alumno_Nivel_Clase.AluNivClaseID as AluNivClaseID, "+
                           " Clase.ClaseDescripcion as ClaseDescripcion, "+
                           " Clase_Nivel_Elemento.ClaseElemNivID as ClaseElemNivID, "+
                           " Alumno_Nivel_Clase.ClaseID as ClaseID, Elemento.ElementoNombre as ElementoNombre, "+
                           " Alumno_Nivel_Clase.UsuarioID as UsuarioID FROM Alumno_Nivel_Clase INNER JOIN Clase "+
                           " ON Alumno_Nivel_Clase.ClaseID = Clase.ClaseID INNER JOIN Clase_Nivel_Elemento "+
                           " ON Clase.ClaseID = Clase_Nivel_Elemento.ClaseID INNER JOIN Nivel "+
                           " ON Alumno_Nivel_Clase.NivelID = Nivel.NivelID AND Clase_Nivel_Elemento.NivelID = Nivel.NivelID "+
                           " INNER JOIN Elemento ON Clase_Nivel_Elemento.ElementoID = Elemento.ElementoID " +
                           " WHERE (Alumno_Nivel_Clase.UsuarioID = " + ViewState["AlumnoID"] + " AND Clase.ClaseID = " + ViewState["ClaseID"] + " AND Alumno_Nivel_Clase.NivelID = " + ViewState["NivelID"] + "" +
                           " )  AND Clase_Nivel_Elemento.ElementoID" + 
                           " NOT IN (SELECT Clase_Nivel_Elemento.ElementoID "+
                           " FROM Alumno_Nivel_Clase_Elemento INNER JOIN Alumno_Nivel_Clase ON Alumno_Nivel_Clase_Elemento.AluNivClaseID = Alumno_Nivel_Clase.AluNivClaseID "+
                           " INNER JOIN Clase_Nivel_Elemento ON Alumno_Nivel_Clase_Elemento.ClaseElemNivID = Clase_Nivel_Elemento.ClaseElemNivID "+
                           " AND Alumno_Nivel_Clase_Elemento.ClaseElemNivID = Clase_Nivel_Elemento.ClaseElemNivID " +
                           " WHERE (Alumno_Nivel_Clase.ClaseID = " + ViewState["ClaseID"] + " AND Alumno_Nivel_Clase.UsuarioID = " + ViewState["AlumnoID"] + "))";
            
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "NivelID";
            GridView4.DataKeyNames = TablaID;
            GridView4.DataSource = dt;
            GridView4.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }
    #endregion

    #region Cargar Elementos evaluados
    protected void cargarElementosEvaluados()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Alumno_Nivel_Clase_Elemento.AlumNivClasElemID as AlumNivClasElemID, "+
                           " Alumno_Nivel_Clase_Elemento.CalificacionID as CalificacionID, "+
                           " (SELECT CalificacionNombre FROM Calificacion where CalificacionID = Alumno_Nivel_Clase_Elemento.CalificacionID) as CalificacionNombre, "+
                           " Alumno_Nivel_Clase.ClaseID as ClaseID, (SELECT ClaseDescripcion FROM Clase "+
                           " WHERE ClaseID = Alumno_Nivel_Clase.ClaseID) as ClaseDescripcion, Clase_Nivel_Elemento.ElementoID as ElementoID, "+
                           " (SELECT ElementoNombre FROM Elemento WHERE ElementoID = Clase_Nivel_Elemento.ElementoID) as ElementoNombre "+
                           " FROM Alumno_Nivel_Clase_Elemento INNER JOIN Alumno_Nivel_Clase ON Alumno_Nivel_Clase_Elemento.AluNivClaseID = Alumno_Nivel_Clase.AluNivClaseID "+
                           " INNER JOIN Clase_Nivel_Elemento ON Alumno_Nivel_Clase_Elemento.ClaseElemNivID = Clase_Nivel_Elemento.ClaseElemNivID "+
                           " AND Alumno_Nivel_Clase_Elemento.ClaseElemNivID = Clase_Nivel_Elemento.ClaseElemNivID " +
                           " WHERE (Alumno_Nivel_Clase.ClaseID = " + ViewState["ClaseID"] + " AND Alumno_Nivel_Clase.UsuarioID = " + ViewState["AlumnoID"] +
                           " AND Alumno_Nivel_Clase.NivelID = " + ViewState["NivelID"] + ")";
            
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "AlumNivClasElemID";
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
    #endregion

    #region RowCommand
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string comando = e.CommandArgument.ToString();
        if (comando.Length < 4)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow gvrow = GridView2.Rows[index];
            if (e.CommandName.Equals("Nivel"))
            {
                sSQL = "SELECT NivelID AS VAL, NivelNombre AS TXT FROM Nivel ORDER BY TXT";
                Utilidades.CargarListado(ref dplNivelMod, sSQL, cn, ref Err, true);
                lblClaseN.Text = "<strong>Clase: </strong>" + (gvrow.FindControl("Clase") as Label).Text;
                lblUsuarioN.Text = "<strong>Alumno: </strong>" + (gvrow.FindControl("UsuarioNom") as Label).Text;
                imgUsuNivel.ImageUrl = (gvrow.FindControl("UsuarioFoto") as ImageButton).ImageUrl;
                hdfClaseNID.Value = (gvrow.FindControl("ClaseID") as Label).Text;
                hdfUsuarioNID.Value = (gvrow.FindControl("UsuarioID") as Label).Text;
                string nivel = (gvrow.FindControl("Nivel") as Label).Text;
                if (nivel != "No Asignado")
                {
                    string NivelID = Utilidades.EjeSQL("SELECT NivelID FROM Nivel WHERE NivelNombre = '" + nivel + "'", cn, ref Err, true);
                    dplNivelMod.SelectedValue = NivelID;
                }
                MostrarNivelModal();
            }
            else if (e.CommandName.Equals("Calificacion"))
            {
                //Consultar la ultima calificacion....

                lblClase.Text = "<strong>Clase: </strong>" + (gvrow.FindControl("Clase") as Label).Text;
                lblUsuario.Text = "<strong>Alumno: </strong>" + (gvrow.FindControl("UsuarioNom") as Label).Text;
                imgAlumno.ImageUrl = (gvrow.FindControl("UsuarioFoto") as ImageButton).ImageUrl;
                ViewState["AlumnoID"] = (gvrow.FindControl("UsuarioID") as Label).Text;
                ViewState["ClaseID"] = (gvrow.FindControl("ClaseID") as Label).Text;
                ViewState["NivelID"] = (gvrow.FindControl("NivelID") as Label).Text;
                string nivel = (gvrow.FindControl("Nivel") as Label).Text;
                if (nivel != "No Asignado")
                {
                    cargarElementosAEvaluar();
                    cargarElementosEvaluados();
                    MostrarEvaluacionModal();
                }
                else
                {
                    MostrarMsjModal("Debe Asignar el Nivel para poder Calificar", "ERR");
                }
            }
            else if (e.CommandName.Equals("Observacion"))
            {
                ViewState["AlumnoID"] = (gvrow.FindControl("UsuarioID") as Label).Text;
                ViewState["ClaseID"] = (gvrow.FindControl("ClaseID") as Label).Text;
                lblClaseObs.Text = "<strong>Clase: </strong>" + (gvrow.FindControl("Clase") as Label).Text;
                lblAlumnoObs.Text = "<strong>Alumno: </strong>" + (gvrow.FindControl("UsuarioNom") as Label).Text;
                imgAlumnoObs.ImageUrl = (gvrow.FindControl("UsuarioFoto") as ImageButton).ImageUrl;
                hdfUsuarioIDObs.Value = ViewState["AlumnoID"].ToString();
                hdfClaseIDObs.Value = ViewState["ClaseID"].ToString();
                ViewState["AlumnoID"] = (gvrow.FindControl("UsuarioID") as Label).Text;
                ViewState["ClaseID"] = (gvrow.FindControl("ClaseID") as Label).Text;
                sSQL = "SELECT Observacion FROM Clase_Observacion_Usuario WHERE UsuarioID = " + ViewState["AlumnoID"] + " AND ClaseID = " + ViewState["ClaseID"] + "";
                string observacion = Utilidades.EjeSQL(sSQL, cn, ref Err, true);
                txtObservacion.Text = observacion;
                string ProfesorID = Utilidades.EjeSQL("SELECT ProfesorID FROM Clase_Observacion_Usuario WHERE UsuarioID = " + ViewState["AlumnoID"] + " AND ClaseID = " + ViewState["ClaseID"] + "", cn, ref Err, true);
                sSQL = "SELECT (UsuarioNombre+' '+UsuarioApellido) AS Usuario FROM Usuario WHERE UsuarioID = "+ProfesorID;
                string profesor = Utilidades.EjeSQL(sSQL, cn, ref Err, true);
                if (profesor == "-1")
                    profesor = "";
                txtProfesor.Text = "Profesor: "+profesor;
                MostrarObservacionModal();
            }
        }
    }

    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView3.Rows[index];
        if (e.CommandName.Equals("Modificar"))
        {
            hdfAlumNivClasElemID.Value = (gvrow.FindControl("AlumNivClasElemID") as Label).Text;
            txtElementoModificar.Text = (gvrow.FindControl("ElementoNombre") as Label).Text;
            hdfClaseIDMod.Value = (gvrow.FindControl("ClaseIDE") as Label).Text;
            sSQL = "SELECT Calificacion.CalificacionNombre as TXT, " +
                        " Calificacion.CalificacionID as VAL " +
                        " FROM Calificacion INNER JOIN" +
                        " TipCalificacion ON Calificacion.TipoCalificacionID = TipCalificacion.TipoCalificacionID INNER JOIN" +
                        " Clase ON TipCalificacion.TipoCalificacionID = Clase.TipoCalificacionID" +
                        " WHERE (Clase.ClaseID = " + hdfClaseIDMod.Value + ")";
            Utilidades.CargarListado(ref dplCalificacionMod, sSQL, cn, ref Err, true);
            dplCalificacionMod.SelectedValue = (gvrow.FindControl("CalificacionID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalModificar').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
    }

    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView4.Rows[index];
        if (e.CommandName.Equals("Evaluar"))
        {
            hdfAluNivClaseID.Value = (gvrow.FindControl("AluNivClaseID") as Label).Text;
            hdfClaseElemNivID.Value = (gvrow.FindControl("ClaseElemNivID") as Label).Text;
            hdfClaseE.Value = (gvrow.FindControl("ClaseID1") as Label).Text;
            txtElementoEdit.Text = (gvrow.FindControl("ElementoNombre1") as Label).Text;
            sSQL = "SELECT Calificacion.CalificacionNombre as TXT, " +
                        " Calificacion.CalificacionID as VAL " +
                        " FROM Calificacion INNER JOIN" +
                        " TipCalificacion ON Calificacion.TipoCalificacionID = TipCalificacion.TipoCalificacionID INNER JOIN" +
                        " Clase ON TipCalificacion.TipoCalificacionID = Clase.TipoCalificacionID" +
                        " WHERE (Clase.ClaseID = " + ViewState["ClaseID"] + ")";
            Utilidades.CargarListado(ref ddlCalificacion, sSQL, cn, ref Err, true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalEvaluar').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
    }
    #endregion

    #region Modal
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
    #endregion

    #region PageIndex
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.EditIndex = -1;
        GridView2.SelectedIndex = -1;
        GridView2.PageIndex = e.NewPageIndex;
        cargarUsuarios();
    }
    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.EditIndex = -1;
        GridView3.SelectedIndex = -1;
        GridView3.PageIndex = e.NewPageIndex;
        cargarElementosEvaluados();
    }
    protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView4.EditIndex = -1;
        GridView4.SelectedIndex = -1;
        GridView4.PageIndex = e.NewPageIndex;
        cargarElementosAEvaluar();
    }
    protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView5.EditIndex = -1;
        GridView5.SelectedIndex = -1;
        GridView5.PageIndex = e.NewPageIndex;
        cargarHistorial();
    }
    #endregion

    #region Foto Usuario
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
    #endregion

    #region Modales Evaluacion y Nivel
    private void MostrarNivelModal()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarNivelModal", "MostrarNivelModal();", true);
    }
    private void MostrarEvaluacionModal()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarEvaluacionModal", "MostrarEvaluacionModal();", true);
    }
    private void MostrarObservacionModal()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarObservacionModal", "MostrarObservacionModal();", true);
    }
    private void MostrarHistorialModal()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarHistorialModal", "MostrarHistorialModal();", true);
    }
    #endregion

    #region Observacion
    protected void btnGuardarObservacion_Click(object sender, EventArgs e)
    {
        string UsuarioID = hdfUsuarioIDObs.Value;
        string ClaseID = hdfClaseIDObs.Value;
        string ObservacionID = Utilidades.EjeSQL("SELECT Clase_Observacion_UsuarioID From Clase_Observacion_Usuario WHERE UsuarioID = " + UsuarioID + " AND ClaseID = " + ClaseID, cn, ref Err, true);

        if (ObservacionID == "" || ObservacionID == "-1")
        {
            sSQL = "INSERT INTO Clase_Observacion_Usuario (UsuarioID, ClaseID, ProfesorID, FechaObservacion, Observacion)" +
                         " VALUES (" + UsuarioID + ", " + ClaseID + ", " + _autenticado.UsuarioID + " ,SYSDATETIME(), '" + txtObservacion.Text + "')";
            Utilidades.EjeSQL(sSQL, cn, ref Err, true);
        }
        else
        {
            sSQL = "UPDATE Clase_Observacion_Usuario SET ProfesorID = " + _autenticado.UsuarioID +
                  ", Observacion = '" + txtObservacion.Text +"'"+
                  ", FechaObservacion = SYSDATETIME() " +
                  " WHERE Clase_Observacion_UsuarioID = " + ObservacionID;
            Utilidades.EjeSQL(sSQL, cn, ref Err, true);
        }

        if (Err == "")
        {
            MostrarMsjModal("Registro Editado exitosamente", "EXI");
            cargarUsuarios();
        }
        else
        {
            MostrarMsjModal("Error al Modificar el Registro", "ERR");
        }
    }
    #endregion

    #region Evaluaciones
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int iRes = 0;
        string AluNivClaseID = hdfAluNivClaseID.Value;
        string CalificacionID = ddlCalificacion.SelectedValue;
        string ClaseElemNivID = hdfClaseElemNivID.Value;
        sSQL = " INSERT INTO Alumno_Nivel_Clase_Elemento " +
               " (CalificacionID, AluNivClaseID, ClaseElemNivID, AlumNivClasElemFechaReg, AlumNivClasElemUsuaReg, SalonID)" +
               " VALUES(" + CalificacionID + ", " + AluNivClaseID + ", " + ClaseElemNivID + ", SYSDATETIME(), "+_autenticado.UsuarioID+", 0)";
        cn.Open();
        try
        {
            SqlCommand addCmd = new SqlCommand(sSQL, cn);
            iRes = addCmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (SqlException sq)
        {
            Err = sq.Message;
            cn.Close();
        }
        if (iRes > 0)
        {
            ddlCalificacion.SelectedValue = "";
            cargarElementosEvaluados();
            cargarElementosAEvaluar();
            //MostrarMsjModal("Evaluación Exitosa", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeEdit').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

        }
        else
        {
            //MostrarMsjModal("Error al guardar el registro: " + Err, "ERR");
        }
    }
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        int iRes = 0;
        string AlumNivClasElemID = hdfAlumNivClasElemID.Value;
        string CalificacionID = dplCalificacionMod.SelectedValue;
        sSQL = " UPDATE Alumno_Nivel_Clase_Elemento SET CalificacionID = " + CalificacionID +
               " ,AlumNivClasElemFechaReg = SYSDATETIME()" +
               " ,AlumNivClasElemUsuaReg = " +_autenticado.UsuarioID +
               " WHERE AlumNivClasElemID = " + AlumNivClasElemID;
        cn.Open();
        try
        {
            SqlCommand addCmd = new SqlCommand(sSQL, cn);
            iRes = addCmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (SqlException sq)
        {
            Err = sq.Message;
            cn.Close();
        }
        if (iRes > 0)
        {
            dplCalificacionMod.SelectedValue = "";
            cargarElementosEvaluados();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeEdit1').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

        }
        else
        {
            
        }
    }
    #endregion

    #region Cambiar Asistencia
    protected void cambiarAsistencia(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        string reservaID = chk.Attributes["reservaID"];
        string alumnoID = chk.Attributes["usuarioID"];
        string clasePlantillaID = chk.Attributes["clasePlantillaID"];
        string claseID = chk.Attributes["claseID"];

        sSQL = "SELECT ClaseAsistenciaID FROM ClaseAsistencia WHERE ReservaID = " + reservaID + " AND ClaseAsistenciaUsuarioID = " + alumnoID;
        string ClaseAsistenciaID = Utilidades.EjeSQL(sSQL, cn, ref Err, true);
        if (ClaseAsistenciaID.Length == 0)
        {
            sSQL = "INSERT INTO ClaseAsistencia (ReservaID, ClaseAsistenciaProfesorID, ClaseAsistenciaClaseID, ClaseAsistenciaUsuarioID, ClaseAsistenciaPlantillaID, ClaseAsistActivo)" +
                   "VALUES (" + reservaID + ", " + _autenticado.UsuarioID + ", " + claseID + ", " + alumnoID + ", " + clasePlantillaID + ", '" + chk.Checked + "')";
        }
        else
        {
            sSQL = "UPDATE ClaseAsistencia SET ClaseAsistActivo ='" + chk.Checked + "' WHERE ClaseAsistenciaID=" + ClaseAsistenciaID;
        }
        string error = Utilidades.EjeSQL(sSQL, cn, ref Err, false);
        if (error == "")
            MostrarMsjModal("Error al actualizar la asistencia: " + error, "ERR");
        else
            MostrarMsjModal("Se actualizó la asistencia con éxito", "EXI");
    }
    #endregion

    #region Guardar Nivel
    protected void btnGuardarNivel_Click(object sender, EventArgs e)
    {
        string NivelID = dplNivelMod.SelectedValue;
        string UsuarioID = hdfUsuarioNID.Value;
        string ClaseID = hdfClaseNID.Value;
        string AluNivClaseID = Utilidades.EjeSQL("SELECT AluNivClaseID From Alumno_Nivel_Clase WHERE UsuarioID = " + UsuarioID + " AND ClaseID = " + ClaseID, cn, ref Err, true);

        if (AluNivClaseID == "" || AluNivClaseID == "-1")
        {
            sSQL = "INSERT INTO Alumno_Nivel_Clase (UsuarioID, ClaseID, NivelID, AluNivClaseFechaRegistro, AluNivClaseUsuarioRegistro, ProfesorID)" +
                         " VALUES (" + UsuarioID + ", " + ClaseID + ", " + NivelID + ", SYSDATETIME(), " + _autenticado.UsuarioID + ", " + _autenticado.UsuarioID + ")";
            Utilidades.EjeSQL(sSQL, cn, ref Err, true);
        }
        else
        {
            sSQL = "UPDATE Alumno_Nivel_Clase SET NivelID = " + NivelID + " WHERE AluNivClaseID = " + AluNivClaseID;
            Utilidades.EjeSQL(sSQL, cn, ref Err, true);
        }

        if (Err == "")
        {
            cargarUsuarios();
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
    #endregion

    #region Histrial
    protected void btnHistorial_Click(object sender, EventArgs e)
    {
        cargarHistorial();
        MostrarHistorialModal();
    }
    protected void cargarHistorial()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT  Clase.ClaseID, Clase.ClaseDescripcion, Elemento.ElementoNombre, Calificacion.CalificacionNombre, " +
	                      "        CONVERT(VARCHAR(11), Alumno_Nivel_Clase_Elemento.AlumNivClasElemFechaReg, 103) AS Fecha, "+
	                      "         (SELECT UsuarioNombre+''+UsuarioApellido FROM Usuario WHERE UsuarioID = Alumno_Nivel_Clase.ProfesorID) as Profesor "+
                          "  FROM   Alumno_Nivel_Clase INNER JOIN "+
                          "         Clase ON Alumno_Nivel_Clase.ClaseID = Clase.ClaseID INNER JOIN "+
                          "         Clase_Nivel_Elemento ON Clase.ClaseID = Clase_Nivel_Elemento.ClaseID INNER JOIN "+
                          "        Alumno_Nivel_Clase_Elemento ON Clase_Nivel_Elemento.ClaseElemNivID = Alumno_Nivel_Clase_Elemento.ClaseElemNivID AND  "+
                          "         Clase_Nivel_Elemento.ClaseElemNivID = Alumno_Nivel_Clase_Elemento.ClaseElemNivID INNER JOIN "+
                          "         Calificacion ON Alumno_Nivel_Clase_Elemento.CalificacionID = Calificacion.CalificacionID INNER JOIN "+
                          "         Elemento ON Clase_Nivel_Elemento.ElementoID = Elemento.ElementoID "+
                          "  WHERE  Alumno_Nivel_Clase.UsuarioID = " + ViewState["AlumnoID"] + " AND Alumno_Nivel_Clase.ClaseID = " + ViewState["ClaseID"] + " " +
                          "  AND Alumno_Nivel_Clase.NivelID = " + ViewState["NivelID"] + "" +
                          "  ORDER BY Alumno_Nivel_Clase_Elemento.AlumNivClasElemFechaReg DESC ";

            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClaseID";
            GridView5.DataKeyNames = TablaID;
            GridView5.DataSource = dt;
            GridView5.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }
    #endregion

}