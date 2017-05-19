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
public partial class sistema_Inicio : System.Web.UI.Page
{
    string Err = "", Rol = "", sSelectSQL = "";
    bool activoBool;
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        Rol = _autenticado.RolID;
        if (Rol == "3")
        {
            #region Nivel-Alumno-Clase-Membresias
            phMembresias.Visible = true;
            ViewState["sSelectSQL"] = "SELECT  TOP 10 (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = dbo.Alumno_Nivel_Clase.UsuarioID) as Usuario , " +
                                       " (SELECT ClaseDescripcion FROM Clase WHERE ClaseID = dbo.Alumno_Nivel_Clase.ClaseID) as ClaseDescripcion, " +
                                       " dbo.Elemento.ElementoNombre as Elemento, dbo.Alumno_Nivel_Clase.UsuarioID as UsuarioID , " +
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
                                       " ORDER BY CONVERT(DATETIME,dbo.Alumno_Nivel_Clase.AluNivClaseFechaRegistro,103) DESC";
            BindGridView();
            upAlumno.Visible = true;
            verCalificaciones.Visible = true;

            phMembresias.Visible = true;
            sSelectSQL = "SELECT CONVERT(VARCHAR(11), MembresiaFechaFin, 103) as Fecha FROM Membresia WHERE MembresiaUsuarioID = " + _autenticado.UsuarioID;
            lblFechaMatricula.Text = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
            #endregion
            
            if (_autenticado.ClienteID == "3" || _autenticado.ClienteID == "2" || _autenticado.ClienteID == "1")//SURA
            {
                #region Descuentos
                phDescuento.Visible = true;
                if (_autenticado.ClienteID == "3")//SURA
                {
                    chkDescuento.Text = "Marque si ya entregó el formato de autorización ";
                }
                else if (_autenticado.ClienteID == "2")//PROTECCION
                {
                    chkDescuento.Text = "Marque si ya firmó la planilla de confirmación";
                }
                else if (_autenticado.ClienteID == "1") //LICSU
                {
                    chkDescuento.Text = "Marque si Acepta los Normas y Recomendaciones";
                }
                #endregion

                #region Autorizacion
                sSelectSQL = "SELECT AutorizacionActivo FROM planAutorizacion WHERE UsuarioID = " + _autenticado.UsuarioID;
                bool activo = bool.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                chkDescuento.Checked = activo;
                if (!activo)
                {
                    sSelectSQL = "SELECT FechaRegistro FROM planAutorizacion WHERE UsuarioID =" + _autenticado.UsuarioID;
                    string fechaRegistro = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
                    DateTime fecha = Convert.ToDateTime(fechaRegistro);
                    TimeSpan diff = (DateTime.Today) - fecha;
                    int diferencia = diff.Days;
                    int valor = 0;
                    string asunto = "";
                    string mensaje = "";
                    string mensaje1 = "";
                    if (_autenticado.ClienteID == "1")
                    {
                        valor = 7;
                        asunto = "Normas y Recomendaciones";
                        mensaje = "El Usuario '" + Utilidades.EjeSQL("Select (UsuarioNombre+' '+UsuarioApellido+', '+UsuarioCedula) as Nombre FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true) + "' no ha " +
                                " confirmado que acepta las normas y recomendaciones por tanto se le han bloqueado las clases.";
                        mensaje1 = " acepta las normas y recomendaciones ";
                    }
                    else
                    {
                        valor = 3;
                        if (_autenticado.ClienteID == "2")
                        {
                            asunto = "Planilla de Confirmación";
                            mensaje = "El Usuario '" + Utilidades.EjeSQL("Select (UsuarioNombre+' '+UsuarioApellido+', '+UsuarioCedula) as Nombre FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true) + "' no ha " +
                                " confirmado la planilla de 'Confirmación' por tanto se le han bloqueado las clases.";
                            mensaje1 = " autoriza la planilla de confirmación ";
                        }
                        else
                        {
                            asunto = "Formato de Autorización";
                            mensaje = "El Usuario '" + Utilidades.EjeSQL("Select (UsuarioNombre+' '+UsuarioApellido+', '+UsuarioCedula) as Nombre FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true) + "' no ha " +
                                " confirmado aún la autorización de descuento de nómina por tanto se le han bloqueado las clases.";
                            mensaje1 = " autoriza el descuento de nómina ";
                        }
                    }
                    if (diferencia > valor)
                    {
                        //Congelar las Clases y enviar correo al admin informando
                        chkDescuento.Enabled = false;
                        sSelectSQL = "SELECT CorreoEnviado FROM planAutorizacion WHERE UsuarioID = " + _autenticado.UsuarioID;
                        bool activo2 = bool.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                        if (!activo2)
                        {
                            sSelectSQL = "UPDATE planAutorizacion SET CorreoEnviado = 1 WHERE UsuarioID = " + _autenticado.UsuarioID;
                            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                            string destino = "admin@licsu.com";
                            Utilidades.EnviarCorreo(destino, asunto, mensaje, ref Err);
                            destino = Utilidades.EjeSQL("Select UsuarioCorreo FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
                            asunto = "Reservas Bloqueadas";
                            mensaje = "Buen día.<br/>Debido a que la casilla donde usted confirma que" + mensaje1 + "no ha sido señalada, el sistema se bloqueo de manera automática y usted no puede reservar las clases." +
                                " Para desbloquearlo recuerde enviar escaneado al mail: academia@licsu.com (mientras lo entrega en físico a la profesora) y solicitar la habilitación del sistema nuevamente. Cualquier duda puede escribir al ws 3052283305 " +
                                "<br/> Gracias";
                            Utilidades.EnviarCorreo(destino, asunto, mensaje, ref Err);
                        }
                    }
                }
                else
                {
                    chkDescuento.Enabled = false;
                }
                #endregion

                if(_autenticado.ClienteID == "2" || _autenticado.ClienteID == "3")
                {
                    #region Plan
                    sSelectSQL = "SELECT Confirmo FROM PlanEmpresa WHERE UsuarioID = " + _autenticado.UsuarioID;
                    string confirmo = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
                    if (confirmo == "0")
                    {
                        sSelectSQL = "SELECT PlanActivo FROM PlanEmpresa WHERE UsuarioID = " + _autenticado.UsuarioID;
                        bool valor = Convert.ToBoolean(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                        if (!valor)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarActivarPlan", "MostrarActivarPlan();", true);
                        }
                    }
                    #endregion
                }
            }
            else
            {
                phDescuento.Visible = false;
            }

            #region TipoEmpleado
            string esEmpleado = Utilidades.EjeSQL("SELECT UsuarioTipoEmp FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
            if (esEmpleado == "Empleado")
            {
                phDesactivar.Visible = true;
                btnDesactivar.Text = "Activar Plan / Desactivar Plan";
            }
            else
            {
                phDesactivar.Visible = false;
            }
            #endregion

            BindGridView6();

        }
        else if (Rol == "2")
        {
            //PROFESOR
        }
        else if (Rol == "4")
        {
            #region Administrador Cliente
            cargarEstadisticaGeneral();
            estadisticasNivel1();
            estadisticasNivel2();
            estadisticasNivel3();
            estadisticasNivel4();
            //Empleados Activos
            /*ViewState["sSelectSQL"] = "SELECT dbo.Usuario.UsuarioID as UsuarioID,(dbo.Usuario.UsuarioNombre+' '+dbo.Usuario.UsuarioApellido) as UsuarioNombre, " +
                            " dbo.Usuario.UsuarioCedula as UsuarioCedula, " +
                            " CONVERT(VARCHAR(11),dbo.Usuario.UsuarioFechaNacimiento,103) as UsuarioFechaNacimiento, " +
                            " dbo.Usuario.UsuarioCorreo as UsuarioCorreo, " +
                            " dbo.UsuarioRol.ClienteID as ClienteID " +
                            " FROM dbo.PlanEmpresa INNER JOIN " +
                            " dbo.Usuario ON dbo.PlanEmpresa.UsuarioID = dbo.Usuario.UsuarioID " +
                            " AND dbo.PlanEmpresa.UsuarioID = dbo.Usuario.UsuarioID INNER JOIN " +
                            " dbo.UsuarioRol ON dbo.Usuario.UsuarioID = dbo.UsuarioRol.UsuarioID" +
                             " WHERE (dbo.UsuarioRol.ClienteID = " + _autenticado.ClienteID + ") ORDER BY dbo.planEmpresa.FechaUltimo DESC";
            //MostrarMsjModal(ViewState["sSelectSQL"].ToString(),"");
            BindGridView4();
            upAdminCliente.Visible = true;
            verUsuarios.Visible = true;
            //Reservas realizadas
            ViewState["sSelectSQL2"] = "SELECT TOP 10 Reserva.ReservaID as ReservaID, " +
                            " (SELECT UsuarioNombre+' '+UsuarioApellido From Usuario WHERE UsuarioID = Reserva.UsuarioID) as UsuarioNombre, " +
                            " (SELECT UsuarioCedula From Usuario WHERE UsuarioID = Reserva.UsuarioID) as UsuarioCedula,  " +
                            " (SELECT UsuarioNombre+' '+UsuarioApellido From Usuario WHERE UsuarioID = ClasePlantilla.ProfesorID) as ProfesorID,  " +
                            " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha, " +
                            " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora, " +
                            " Clase.ClaseDescripcion as ClaseDescripcion, " +
                            " ClasePlantilla.ClienteID as ClienteID " +
                            " FROM Reserva INNER JOIN " +
                            " ClasePlantilla ON Reserva.ClasePlantillaID = ClasePlantilla.ClasePlantillaID INNER JOIN " +
                            " Clase ON ClasePlantilla.ClaseID = Clase.ClaseID " +
                            " WHERE (ClasePlantilla.ClienteID = " + _autenticado.ClienteID + ") ORDER BY Reserva.FechaReserva DESC";
            //MostrarMsjModal(ViewState["sSelectSQL2"].ToString(),"");
            BindGridView5();
            upAdminCliente1.Visible = true;
            verReservas.Visible = true;*/
            #endregion
        }
        if (Rol == "1")
        {
            #region Administrador
            phAdministrador.Visible = true;
            phListasAdmin.Visible = true;
            BindGridViewListas();
            #endregion
        }
    }

    #region Activacion de Plan Empresa
    protected void GuardarPlan(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarConfirmacion", "MostrarConfirmacion();", true);
    }
    protected void ConfirmarPlan(object sender, EventArgs e)
    {
        string PlanActivo = string.Empty;
        string EstadoProximo = string.Empty;
        if (chkActual.Checked) { PlanActivo = "1"; } else { PlanActivo = "0"; }
        if (chkProximo.Checked) { EstadoProximo = "1"; } else { EstadoProximo = "0"; }
        sSelectSQL = "UPDATE PlanEmpresa SET PlanActivo = "+PlanActivo+", fechaUltimo = SYSDATETIME(), EstadoProximo = "+EstadoProximo+", Confirmo = 1 WHERE UsuarioID = "+_autenticado.UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        if (Err.Length == 0)
        {
            if (PlanActivo == "1")
            {
                sSelectSQL = "INSERT INTO PlanEmpresaHistorial(PlanEmpresaUsuarioID, PlanEmpresaPlanEstado, PlanEmpresaFecha)" +
                             " VALUES(" + _autenticado.UsuarioID + ", 1, SYSDATETIME())";
                Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            }
            asignarPlan();
            Response.Redirect("Inicio.aspx");
        }
        else
        {
            MostrarMsjModal("Ocurrio un Error: "+Err, "ERR");
        }
    }
    #endregion
    protected void estadisticasNivel1()
    {
        sSelectSQL = "select COUNT(DISTINCT(EmpleadoNivel1)) from empleadoemp " +
                     " where clienteid = " + _autenticado.ClienteID + " AND EmpleadoNivel1 != 'NINGUNO'";

        if (int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true)) > 0)
        {
            int tamVector = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
            upEstadisticas1.Visible = true;
            Button4.Visible = true;
            //Guardamos el tamaño del Vector..
            string[] vecNiveles = new string[tamVector];
            int[] vecNivelesCantidades = new int[tamVector];
            int[] vecUsuariosActivos = new int[tamVector];
            int contador = 0;
            //Se selecciona los niveles de los empleados
            try
            {
                cn.Open();
                sSelectSQL = "select DISTINCT(EmpleadoNivel1) as Nivel from empleadoemp where clienteid = " + _autenticado.ClienteID;
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    vecNiveles[contador] = dr["Nivel"].ToString();
                    contador++;
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MostrarMsjModal("Error: " + ex.Message, "ERR");
            }
            contador = 0;
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                sSelectSQL = "select COUNT(*) as Cantidad from empleadoemp where clienteid = " + _autenticado.ClienteID + " AND EmpleadoNivel1 = '" + vecNiveles[i] + "'";
                vecNivelesCantidades[i] = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                sSelectSQL = " select COUNT(DISTINCT(u.UsuarioCedula)) as Cantidad  " +
                              " from usuario u, empleadoemp e, PlanEmpresa p" +
                              " WHERE u.UsuarioCedula = e.EmpleadoCedula  AND" +
                              " u.UsuarioID = p.UsuarioID" +
                              " AND e.ClienteID = " + _autenticado.ClienteID + " AND e.EmpleadoNivel1 = '" + vecNiveles[i] + "' " +
                              " AND p.PlanActivo = 1";
                vecUsuariosActivos[i] = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
            }
            //Llenar la tabla
            Table tabla = new Table();
            tabla.CssClass = "table";
            tabla.ID = "Table1";
            TableHeaderRow theaderrow = new TableHeaderRow();
            TableHeaderCell theadercell1 = new TableHeaderCell();
            theadercell1.CssClass = "text-center";
            theadercell1.Text = "Gerencia";
            TableHeaderCell theadercell2 = new TableHeaderCell();
            theadercell2.CssClass = "text-center";
            theadercell2.Text = "Total Empleados";
            TableHeaderCell theadercell5 = new TableHeaderCell();
            theadercell5.CssClass = "text-center";
            theadercell5.Text = "# Empleados Actuales";
            TableHeaderCell theadercell3 = new TableHeaderCell();
            theadercell3.CssClass = "text-center";
            theadercell3.Text = "Muestra Ideal 90%";
            TableHeaderCell theadercell4 = new TableHeaderCell();
            theadercell4.CssClass = "text-center";
            theadercell4.Text = "Muestra Ideal 95%";
            theaderrow.Controls.Add(theadercell1);
            theaderrow.Controls.Add(theadercell2);
            theaderrow.Controls.Add(theadercell5);
            theaderrow.Controls.Add(theadercell3);
            theaderrow.Controls.Add(theadercell4);
            tabla.Controls.Add(theaderrow);
            //Ciclo para crear las filas...
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                TableRow theaderrow1 = new TableRow();
                TableCell theadercell11 = new TableCell();
                TableCell theadercell21 = new TableCell();
                theadercell21.CssClass = "text-center";
                TableCell theadercell31 = new TableCell();
                theadercell31.CssClass = "text-center";
                TableCell theadercell41 = new TableCell();
                theadercell41.CssClass = "text-center";
                TableCell theadercell51 = new TableCell();
                theadercell51.CssClass = "text-center";
                theadercell11.Text = vecNiveles[i];
                theadercell21.Text = "" + vecNivelesCantidades[i];
                theadercell51.Text = "" + vecUsuariosActivos[i];
                theadercell31.Text = "" + Utilidades.MuestraGeneral(vecNivelesCantidades[i], 90);
                theadercell41.Text = "" + Utilidades.MuestraGeneral(vecNivelesCantidades[i], 95);
                theaderrow1.Controls.Add(theadercell11);
                theaderrow1.Controls.Add(theadercell21);
                theaderrow1.Controls.Add(theadercell51);
                theaderrow1.Controls.Add(theadercell31);
                theaderrow1.Controls.Add(theadercell41);
                tabla.Controls.Add(theaderrow1);
            }
            //Fin del ciclo de crear filas..            
            panelEstad1.Controls.Add(tabla);
            //Cargar la Grafica
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "Gerencia";
            col.DataType = typeof(String);
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Tamanho";
            col.DataType = typeof(int);
            dt1.Columns.Add(col);
            DataColumn col1 = new DataColumn();
            col1.ColumnName = "Gerencia";
            col1.DataType = typeof(String);
            dt2.Columns.Add(col1);
            col1 = new DataColumn();
            col1.ColumnName = "Tamanho";
            col1.DataType = typeof(int);
            dt2.Columns.Add(col1);
            DataRow row = dt1.NewRow();
            DataRow row2 = dt2.NewRow();
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                int m11 = Utilidades.MuestraGeneral(vecNivelesCantidades[i], 90);
                int m12 = Utilidades.MuestraGeneral(vecNivelesCantidades[i], 95);
                row["Gerencia"] = "-";
                row["Tamanho"] = m11;
                dt1.Rows.Add(row);
                dt1.AcceptChanges();
                row = dt1.NewRow();
                row2["Gerencia"] = "-";
                row2["Tamanho"] = m12;
                dt2.Rows.Add(row2);
                dt2.AcceptChanges();
                row2 = dt2.NewRow();
            }
            DataView data = new DataView(dt1);
            DataView data2 = new DataView(dt2);
            Chart2.Series["Series1"].Points.DataBind(data, "Gerencia", "Tamanho", "");
            Chart2.Series["Series2"].Points.DataBind(data2, "Gerencia", "Tamanho", "");
        }
    }
    protected void estadisticasNivel2()
    {
        if (int.Parse(Utilidades.EjeSQL("select COUNT(DISTINCT(EmpleadoNivel2)) from empleadoemp where clienteid = " + _autenticado.ClienteID + " AND EmpleadoNivel2 != 'NINGUNO'", cn, ref Err, true)) > 0)
        {
            upEstadisticas2.Visible = true;
            Button1.Visible = true;
            sSelectSQL = "select COUNT(DISTINCT(EmpleadoNivel2)) from empleadoemp where clienteid = " + _autenticado.ClienteID;
            //Guardamos el tamaño del Vector..
            string[] vecNiveles = new string[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int[] vecNivelesCantidades = new int[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int[] vecUsuariosActivos = new int[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int contador = 0;
            try
            {
                cn.Open();
                sSelectSQL = "select DISTINCT(EmpleadoNivel2) as Nivel from empleadoemp where clienteid = " + _autenticado.ClienteID;
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    vecNiveles[contador] = dr["Nivel"].ToString();
                    contador++;
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MostrarMsjModal("Error: " + ex.Message, "ERR");
            }
            contador = 0;
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                sSelectSQL = "select COUNT(*) as Cantidad from empleadoemp where clienteid = " + _autenticado.ClienteID + " AND EmpleadoNivel2 = '" + vecNiveles[i] + "'";
                vecNivelesCantidades[i] = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                sSelectSQL = " select COUNT(DISTINCT(u.UsuarioCedula)) as Cantidad  " +
                              " from usuario u, empleadoemp e, PlanEmpresa p" +
                              " WHERE u.UsuarioCedula = e.EmpleadoCedula  AND" +
                              " u.UsuarioID = p.UsuarioID" +
                              " AND e.ClienteID = " + _autenticado.ClienteID + " AND e.EmpleadoNivel2 = '" + vecNiveles[i] + "' " +
                              " AND p.PlanActivo = 1";
                vecUsuariosActivos[i] = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
            }
            //Llenar la tabla
            Table tabla = new Table();
            tabla.CssClass = "table";
            tabla.ID = "Table2";
            TableHeaderRow theaderrow = new TableHeaderRow();
            TableHeaderCell theadercell1 = new TableHeaderCell();
            theadercell1.CssClass = "text-center";
            theadercell1.Text = "Gerencia";
            TableHeaderCell theadercell2 = new TableHeaderCell();
            theadercell2.CssClass = "text-center";
            theadercell2.Text = "Total Empleados";
            TableHeaderCell theadercell5 = new TableHeaderCell();
            theadercell5.CssClass = "text-center";
            theadercell5.Text = "# Empleados Actuales";
            TableHeaderCell theadercell3 = new TableHeaderCell();
            theadercell3.CssClass = "text-center";
            theadercell3.Text = "Muestra Ideal 90%";
            TableHeaderCell theadercell4 = new TableHeaderCell();
            theadercell4.CssClass = "text-center";
            theadercell4.Text = "Muestra Ideal 95%";
            theaderrow.Controls.Add(theadercell1);
            theaderrow.Controls.Add(theadercell2);
            theaderrow.Controls.Add(theadercell5);
            theaderrow.Controls.Add(theadercell3);
            theaderrow.Controls.Add(theadercell4);
            tabla.Controls.Add(theaderrow);
            //Ciclo para crear las filas...
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                TableRow theaderrow1 = new TableRow();
                TableCell theadercell11 = new TableCell();
                TableCell theadercell21 = new TableCell();
                theadercell21.CssClass = "text-center";
                TableCell theadercell31 = new TableCell();
                theadercell31.CssClass = "text-center";
                TableCell theadercell41 = new TableCell();
                theadercell41.CssClass = "text-center";
                TableCell theadercell51 = new TableCell();
                theadercell51.CssClass = "text-center";
                theadercell11.Text = vecNiveles[i];
                theadercell21.Text = "" + vecNivelesCantidades[i];
                theadercell51.Text = "" + vecUsuariosActivos[i];
                theadercell31.Text = "" + Utilidades.MuestraGeneral(vecNivelesCantidades[i], 90);
                theadercell41.Text = "" + Utilidades.MuestraGeneral(vecNivelesCantidades[i], 95);
                theaderrow1.Controls.Add(theadercell11);
                theaderrow1.Controls.Add(theadercell21);
                theaderrow1.Controls.Add(theadercell51);
                theaderrow1.Controls.Add(theadercell31);
                theaderrow1.Controls.Add(theadercell41);
                tabla.Controls.Add(theaderrow1);
            }
            //Fin del ciclo de crear filas..            
            panelEstad2.Controls.Add(tabla);
            //Cargar la Grafica
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "Gerencia";
            col.DataType = typeof(String);
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Tamanho";
            col.DataType = typeof(int);
            dt1.Columns.Add(col);
            DataColumn col1 = new DataColumn();
            col1.ColumnName = "Gerencia";
            col1.DataType = typeof(String);
            dt2.Columns.Add(col1);
            col1 = new DataColumn();
            col1.ColumnName = "Tamanho";
            col1.DataType = typeof(int);
            dt2.Columns.Add(col1);
            DataRow row = dt1.NewRow();
            DataRow row2 = dt2.NewRow();
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                int m11 = Utilidades.MuestraGeneral(vecNivelesCantidades[i], 90);
                int m12 = Utilidades.MuestraGeneral(vecNivelesCantidades[i], 95);
                row["Gerencia"] = "-";
                row["Tamanho"] = m11;
                dt1.Rows.Add(row);
                dt1.AcceptChanges();
                row = dt1.NewRow();
                row2["Gerencia"] = "-";
                row2["Tamanho"] = m12;
                dt2.Rows.Add(row2);
                dt2.AcceptChanges();
                row2 = dt2.NewRow();
            }
            DataView data = new DataView(dt1);
            DataView data2 = new DataView(dt2);
            Chart1.Series["Series1"].Points.DataBind(data, "Gerencia", "Tamanho", "");
            Chart1.Series["Series2"].Points.DataBind(data2, "Gerencia", "Tamanho", "");
        }
    }
    protected void estadisticasNivel3()
    {
        if (int.Parse(Utilidades.EjeSQL("select COUNT(DISTINCT(EmpleadoNivel3)) from empleadoemp where clienteid = " + _autenticado.ClienteID + " AND EmpleadoNivel3 != 'NINGUNO'", cn, ref Err, true)) > 0)
        {
            upEstadisticas3.Visible = true;
            Button2.Visible = true;
            sSelectSQL = "select COUNT(DISTINCT(EmpleadoNivel3)) from empleadoemp where clienteid = " + _autenticado.ClienteID;
            //Guardamos el tamaño del Vector..
            string[] vecNiveles = new string[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int[] vecNivelesCantidades = new int[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int[] vecUsuariosActivos = new int[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int contador = 0;
            try
            {
                cn.Open();
                sSelectSQL = "select DISTINCT(EmpleadoNivel3) as Nivel from empleadoemp where clienteid = " + _autenticado.ClienteID;
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    vecNiveles[contador] = dr["Nivel"].ToString();
                    contador++;
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MostrarMsjModal("Error: " + ex.Message, "ERR");
            }
            contador = 0;
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                sSelectSQL = "select COUNT(*) as Cantidad from empleadoemp where clienteid = " + _autenticado.ClienteID + " AND EmpleadoNivel3 = '" + vecNiveles[i] + "'";
                vecNivelesCantidades[i] = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                sSelectSQL = "select COUNT(DISTINCT(u.UsuarioCedula)) as Cantidad " +
                             " from usuario u, empleadoemp e " +
                             " WHERE u.UsuarioCedula = e.EmpleadoCedula " +
                             " AND e.ClienteID = " + _autenticado.ClienteID + " AND e.EmpleadoNivel3 = '" + vecNiveles[i] + "'";
                vecUsuariosActivos[i] = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
            }
            //Llenar la tabla
            Table tabla = new Table();
            tabla.CssClass = "table";
            tabla.ID = "Table3";
            TableHeaderRow theaderrow = new TableHeaderRow();
            TableHeaderCell theadercell1 = new TableHeaderCell();
            theadercell1.CssClass = "text-center";
            theadercell1.Text = "Gerencia";
            TableHeaderCell theadercell2 = new TableHeaderCell();
            theadercell2.CssClass = "text-center";
            theadercell2.Text = "Total Empleados";
            TableHeaderCell theadercell5 = new TableHeaderCell();
            theadercell5.CssClass = "text-center";
            theadercell5.Text = "# Empleados Actuales";
            TableHeaderCell theadercell3 = new TableHeaderCell();
            theadercell3.CssClass = "text-center";
            theadercell3.Text = "Muestra Ideal 90%";
            TableHeaderCell theadercell4 = new TableHeaderCell();
            theadercell4.CssClass = "text-center";
            theadercell4.Text = "Muestra Ideal 95%";
            theaderrow.Controls.Add(theadercell1);
            theaderrow.Controls.Add(theadercell2);
            theaderrow.Controls.Add(theadercell5);
            theaderrow.Controls.Add(theadercell3);
            theaderrow.Controls.Add(theadercell4);
            tabla.Controls.Add(theaderrow);
            //Ciclo para crear las filas...
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                TableRow theaderrow1 = new TableRow();
                TableCell theadercell11 = new TableCell();
                TableCell theadercell21 = new TableCell();
                theadercell21.CssClass = "text-center";
                TableCell theadercell31 = new TableCell();
                theadercell31.CssClass = "text-center";
                TableCell theadercell41 = new TableCell();
                theadercell41.CssClass = "text-center";
                TableCell theadercell51 = new TableCell();
                theadercell51.CssClass = "text-center";
                theadercell11.Text = vecNiveles[i];
                theadercell21.Text = "" + vecNivelesCantidades[i];
                theadercell51.Text = "" + vecUsuariosActivos[i];
                theadercell31.Text = "" + Utilidades.MuestraGeneral(vecNivelesCantidades[i], 90);
                theadercell41.Text = "" + Utilidades.MuestraGeneral(vecNivelesCantidades[i], 95);
                theaderrow1.Controls.Add(theadercell11);
                theaderrow1.Controls.Add(theadercell21);
                theaderrow1.Controls.Add(theadercell51);
                theaderrow1.Controls.Add(theadercell31);
                theaderrow1.Controls.Add(theadercell41);
                tabla.Controls.Add(theaderrow1);
            }
            //Fin del ciclo de crear filas..            
            panelEstad3.Controls.Add(tabla);
            //Cargar la Grafica
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "Gerencia";
            col.DataType = typeof(String);
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Tamanho";
            col.DataType = typeof(int);
            dt1.Columns.Add(col);
            DataColumn col1 = new DataColumn();
            col1.ColumnName = "Gerencia";
            col1.DataType = typeof(String);
            dt2.Columns.Add(col1);
            col1 = new DataColumn();
            col1.ColumnName = "Tamanho";
            col1.DataType = typeof(int);
            dt2.Columns.Add(col1);
            DataRow row = dt1.NewRow();
            DataRow row2 = dt2.NewRow();
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                int m11 = Utilidades.MuestraGeneral(vecNivelesCantidades[i], 90);
                int m12 = Utilidades.MuestraGeneral(vecNivelesCantidades[i], 95);
                row["Gerencia"] = "-";
                row["Tamanho"] = m11;
                dt1.Rows.Add(row);
                dt1.AcceptChanges();
                row = dt1.NewRow();
                row2["Gerencia"] = "-";
                row2["Tamanho"] = m12;
                dt2.Rows.Add(row2);
                dt2.AcceptChanges();
                row2 = dt2.NewRow();
            }
            DataView data = new DataView(dt1);
            DataView data2 = new DataView(dt2);
            Chart3.Series["Series1"].Points.DataBind(data, "Gerencia", "Tamanho", "");
            Chart3.Series["Series2"].Points.DataBind(data2, "Gerencia", "Tamanho", "");
        }
    }
    protected void estadisticasNivel4()
    {
        if (int.Parse(Utilidades.EjeSQL("select COUNT(DISTINCT(EmpleadoNivel4)) from empleadoemp where clienteid = " + _autenticado.ClienteID + " AND EmpleadoNivel4 != 'NINGUNO'", cn, ref Err, true)) > 0)
        {
            upEstadisticas4.Visible = true;
            Button3.Visible = true;
            sSelectSQL = "select COUNT(DISTINCT(EmpleadoNivel4)) from empleadoemp where clienteid = " + _autenticado.ClienteID;
            //Guardamos el tamaño del Vector..
            string[] vecNiveles = new string[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int[] vecNivelesCantidades = new int[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int[] vecUsuariosActivos = new int[int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true))];
            int contador = 0;
            try
            {
                cn.Open();
                sSelectSQL = "select DISTINCT(EmpleadoNivel4) as Nivel from empleadoemp where clienteid = " + _autenticado.ClienteID;
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    vecNiveles[contador] = dr["Nivel"].ToString();
                    contador++;
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MostrarMsjModal("Error: " + ex.Message, "ERR");
            }
            contador = 0;
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                sSelectSQL = "select COUNT(*) as Cantidad from empleadoemp where clienteid = " + _autenticado.ClienteID + " AND EmpleadoNivel4 = '" + vecNiveles[i] + "'";
                vecNivelesCantidades[i] = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                sSelectSQL = "select COUNT(DISTINCT(u.UsuarioCedula)) as Cantidad " +
                             " from usuario u, empleadoemp e " +
                             " WHERE u.UsuarioCedula = e.EmpleadoCedula " +
                             " AND e.ClienteID = " + _autenticado.ClienteID + " AND e.EmpleadoNivel4 = '" + vecNiveles[i] + "'";
                vecUsuariosActivos[i] = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
            }
            //Llenar la tabla
            Table tabla = new Table();
            tabla.CssClass = "table";
            tabla.ID = "Table4";
            TableHeaderRow theaderrow = new TableHeaderRow();
            TableHeaderCell theadercell1 = new TableHeaderCell();
            theadercell1.CssClass = "text-center";
            theadercell1.Text = "Gerencia";
            TableHeaderCell theadercell2 = new TableHeaderCell();
            theadercell2.CssClass = "text-center";
            theadercell2.Text = "Total Empleados";
            TableHeaderCell theadercell5 = new TableHeaderCell();
            theadercell5.CssClass = "text-center";
            theadercell5.Text = "# Empleados Actuales";
            TableHeaderCell theadercell3 = new TableHeaderCell();
            theadercell3.CssClass = "text-center";
            theadercell3.Text = "Muestra Ideal 90%";
            TableHeaderCell theadercell4 = new TableHeaderCell();
            theadercell4.CssClass = "text-center";
            theadercell4.Text = "Muestra Ideal 95%";
            theaderrow.Controls.Add(theadercell1);
            theaderrow.Controls.Add(theadercell2);
            theaderrow.Controls.Add(theadercell5);
            theaderrow.Controls.Add(theadercell3);
            theaderrow.Controls.Add(theadercell4);
            tabla.Controls.Add(theaderrow);
            //Ciclo para crear las filas...
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                TableRow theaderrow1 = new TableRow();
                TableCell theadercell11 = new TableCell();
                TableCell theadercell21 = new TableCell();
                theadercell21.CssClass = "text-center";
                TableCell theadercell31 = new TableCell();
                theadercell31.CssClass = "text-center";
                TableCell theadercell41 = new TableCell();
                theadercell41.CssClass = "text-center";
                TableCell theadercell51 = new TableCell();
                theadercell51.CssClass = "text-center";
                theadercell11.Text = vecNiveles[i];
                theadercell21.Text = "" + vecNivelesCantidades[i];
                theadercell51.Text = "" + vecUsuariosActivos[i];
                theadercell31.Text = "" + Utilidades.MuestraGeneral(vecNivelesCantidades[i], 90);
                theadercell41.Text = "" + Utilidades.MuestraGeneral(vecNivelesCantidades[i], 95);
                theaderrow1.Controls.Add(theadercell11);
                theaderrow1.Controls.Add(theadercell21);
                theaderrow1.Controls.Add(theadercell51);
                theaderrow1.Controls.Add(theadercell31);
                theaderrow1.Controls.Add(theadercell41);
                tabla.Controls.Add(theaderrow1);
            }
            //Fin del ciclo de crear filas..            
            panelEstad4.Controls.Add(tabla);
            //Cargar la Grafica
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataColumn col = new DataColumn();
            col.ColumnName = "Gerencia";
            col.DataType = typeof(String);
            dt1.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "Tamanho";
            col.DataType = typeof(int);
            dt1.Columns.Add(col);
            DataColumn col1 = new DataColumn();
            col1.ColumnName = "Gerencia";
            col1.DataType = typeof(String);
            dt2.Columns.Add(col1);
            col1 = new DataColumn();
            col1.ColumnName = "Tamanho";
            col1.DataType = typeof(int);
            dt2.Columns.Add(col1);
            DataRow row = dt1.NewRow();
            DataRow row2 = dt2.NewRow();
            for (int i = 0; i < vecNiveles.Length; i++)
            {
                int m11 = Utilidades.MuestraGeneral(vecNivelesCantidades[i], 90);
                int m12 = Utilidades.MuestraGeneral(vecNivelesCantidades[i], 95);
                row["Gerencia"] = "-";
                row["Tamanho"] = m11;
                dt1.Rows.Add(row);
                dt1.AcceptChanges();
                row = dt1.NewRow();
                row2["Gerencia"] = "-";
                row2["Tamanho"] = m12;
                dt2.Rows.Add(row2);
                dt2.AcceptChanges();
                row2 = dt2.NewRow();
            }
            DataView data = new DataView(dt1);
            DataView data2 = new DataView(dt2);
            Chart4.Series["Series1"].Points.DataBind(data, "Gerencia", "Tamanho", "");
            Chart4.Series["Series2"].Points.DataBind(data2, "Gerencia", "Tamanho", "");
        }
    }

    protected void cargarEstadisticaGeneral()
    {
        upAdminClienteEstadistica.Visible = true;
        int TamPoblacion = int.Parse(Utilidades.EjeSQL("SELECT COUNT(*) FROM EmpleadoEmp WHERE ClienteID = " + _autenticado.ClienteID, cn, ref Err, true));
        sSelectSQL = "SELECT COUNT(*) " +
                      " FROM dbo.PlanEmpresa INNER JOIN  " +
                      " dbo.Usuario ON dbo.PlanEmpresa.UsuarioID = dbo.Usuario.UsuarioID  " +
                      " AND dbo.PlanEmpresa.UsuarioID = dbo.Usuario.UsuarioID INNER JOIN  " +
                      " dbo.UsuarioRol ON dbo.Usuario.UsuarioID = dbo.UsuarioRol.UsuarioID " +
                      " WHERE (dbo.UsuarioRol.ClienteID = " + _autenticado.ClienteID + ") AND (dbo.PlanEmpresa.PlanActivo = 1)";
        int TamMuestra = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
        string zeta = Utilidades.NivelConfianza(TamPoblacion, TamMuestra);
        txtTotal.Text = "" + TamPoblacion;
        txtMuestra.Text = "" + TamMuestra;
        txtNivel.Text = " " + zeta;
    }
    protected void BindGridView6()
    {
        phMedical.Visible = true;
        try
        {
            //cn.Close();
            cn.Open();
            string cmd2 = "SELECT dbo.Clase.ClaseDescripcion as ClaseDescripcion, " +
                            " (dbo.ClasePlantilla.ClasePlantillaFecha+' '+dbo.ClasePlantilla.ClasePlantillaHora) as ClasePlantillaFecha, " +
                            " (SELECT Turno FROM dbo.ClaseEspacio WHERE (ClasePlantillaID = dbo.ClasePlantilla.ClasePlantillaID AND UsuarioID = " + _autenticado.UsuarioID + " AND Ocupado = 1)) as Turno, " +
                            " (SELECT HoraInicial FROM dbo.ClaseEspacio WHERE (ClasePlantillaID = dbo.ClasePlantilla.ClasePlantillaID AND UsuarioID = " + _autenticado.UsuarioID + " AND Ocupado = 1)) as HoraInicial, " +
                            " (SELECT HoraFinal FROM dbo.ClaseEspacio WHERE (ClasePlantillaID = dbo.ClasePlantilla.ClasePlantillaID AND UsuarioID = " + _autenticado.UsuarioID + " AND Ocupado = 1)) as HoraFinal " +
                            " FROM dbo.Clase INNER JOIN " +
                            " dbo.ClasePlantilla ON dbo.Clase.ClaseID = dbo.ClasePlantilla.ClaseID INNER JOIN " +
                            " dbo.Reserva ON dbo.ClasePlantilla.ClasePlantillaID = dbo.Reserva.ClasePlantillaID " +
                            " WHERE (dbo.Clase.ClaseIntervalo IS NOT NULL) AND (dbo.Reserva.UsuarioID = " + _autenticado.UsuarioID + ")" +
                            " AND (CONVERT(DATE,dbo.ClasePlantilla.ClasePlantillaFecha, 103) >= CONVERT(DATE, SYSDATETIME(), 103))" +
                            " ORDER BY dbo.ClasePlantilla.ClasePlantillaFecha ASC, dbo.ClasePlantilla.ClasePlantillaHora ASC";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            DataTable dt1 = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClaseDescripcion";
            GridView4.DataKeyNames = TablaID;
            GridView4.DataSource = dt1;
            GridView4.DataBind();
            cn.Close();

            /*if (dt.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                GridView3.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.                
                GridView3.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
            }*/
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }
    protected void BindGridView4()
    {
        try
        {
            //cn.Close();
            cn.Open();
            string cmd2 = "SELECT Usuario.UsuarioID as UsuarioID,(Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombre, " +
                            " Usuario.UsuarioCedula as UsuarioCedula, " +
                            " Usuario.UsuarioFechaNacimiento as UsuarioFechaNacimiento, " +
                            " Usuario.UsuarioCorreo as UsuarioCorreo, " +
                            " UsuarioRol.ClienteID as ClienteID " +
                            " FROM PlanEmpresa INNER JOIN " +
                            " Usuario ON PlanEmpresa.UsuarioID = Usuario.UsuarioID " +
                            " AND PlanEmpresa.UsuarioID = Usuario.UsuarioID INNER JOIN " +
                            " UsuarioRol ON Usuario.UsuarioID = UsuarioRol.UsuarioID" +
                             " WHERE (UsuarioRol.ClienteID = " + _autenticado.ClienteID + " " + ViewState["sWhere"] + ")";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioID";
            GridView2.DataKeyNames = TablaID;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            cn.Close();
            //Attribute to show the Plus Minus Button.
            /*GridView2.HeaderRow.Cells[0].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.                
            GridView2.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
            GridView2.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            GridView2.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;*/
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void BindGridView5()
    {
        try
        {
            cn.Open();
            string cmd2 = ViewState["sSelectSQL2"].ToString();
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ReservaID";
            GridView3.DataKeyNames = TablaID;
            GridView3.DataSource = dt;
            GridView3.DataBind();
            cn.Close();
            GridView2.DataKeyNames = TablaID;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            cn.Close();
            if (dt.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                GridView3.HeaderRow.Cells[3].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.                
                GridView3.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = ViewState["sSelectSQL"].ToString();
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
    protected void lnkPerfil_Click(object sender, EventArgs e)
    {
        Response.Redirect("Perfil.aspx");
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


    protected void verCalificaciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("CalificacionesAlumno.aspx");
    }
    private void MostrarPregunta(string msj)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarPregunta", "MostrarPregunta('" + msj.Replace("'", "").Replace("\r\n", " ") + "');", true);
    }
    private void MostrarPregunta2(string msj)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarPregunta2", "MostrarPregunta2('" + msj.Replace("'", "").Replace("\r\n", " ") + "');", true);
    }
    private void MostrarPregunta3(string msj)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarPregunta3", "MostrarPregunta3('" + msj.Replace("'", "").Replace("\r\n", " ") + "');", true);
    }
    private void MostrarPreguntaCerrar()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarPreguntaCerrar", "MostrarPreguntaCerrar()", true);
    }
    protected void btnDesactivar_Click(object sender, EventArgs e)
    {
        //LLenado de lista de opciones
        bool EstadoProximo, PlanActivo;
        string proximo = Utilidades.EjeSQL("SELECT EstadoProximo FROM PlanEmpresa WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
        string actual = Utilidades.EjeSQL("SELECT PlanActivo FROM PlanEmpresa WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
        EstadoProximo = bool.Parse(proximo);
        PlanActivo = bool.Parse(actual);
        dplOpciones.Items.Add(new ListItem("Seleccione una Opción", ""));
        if (EstadoProximo && PlanActivo)
        {
            //AMBOS Desactivan
            if (DateTime.Today.Day <= 3)
            {
                dplOpciones.Items.Add(new ListItem("Desactivar el Plan para el Mes Actual", "DesActual"));
                dplOpciones.Items.Add(new ListItem("Desactivar el Plan para el Mes Próximo", "DesProximo"));
            }
            else
            {
                dplOpciones.Items.Add(new ListItem("Desactivar el Plan para el Mes Próximo", "DesProximo"));
            }
        }
        else if (!EstadoProximo && PlanActivo)
        {
            if (DateTime.Today.Day <= 3)
            {
                dplOpciones.Items.Add(new ListItem("Desactivar el Plan para el Mes Actual", "DesActual"));
                dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Próximo", "ActProximo"));
            }
            else
            {
                dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Próximo", "ActProximo"));
            }
        }
        else if (EstadoProximo && !PlanActivo)
        {
            if (_autenticado.ClienteID == "3")
            {
                if (DateTime.Today.Day <= 15)
                {
                    dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Actual", "ActActual"));
                    dplOpciones.Items.Add(new ListItem("Desactivar el Plan para el Mes Próximo", "DesProximo"));
                }
                else
                {
                    dplOpciones.Items.Add(new ListItem("Desactivar el Plan para el Mes Próximo", "DesProximo"));
                }
            }
            else
            {
                dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Actual", "ActActual"));
                dplOpciones.Items.Add(new ListItem("Desactivar el Plan para el Mes Próximo", "DesProximo"));
            }
        }
        else if (!EstadoProximo && !PlanActivo)
        {
            if (_autenticado.ClienteID == "3")
            {
                if (DateTime.Today.Day <= 15)
                {
                    dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Actual", "ActActual"));
                    dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Próximo", "ActProximo"));
                }
                else
                {
                    dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Próximo", "ActProximo"));
                }
            }
            else
            {
                dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Actual", "ActActual"));
                dplOpciones.Items.Add(new ListItem("Activar el Plan para el Mes Próximo", "ActProximo"));
            }
        }
        MostrarPregunta3("Seleccione la Acción a Realizar: ");
    }
    protected void asignarPlan()
    {
        string UsuarioID = _autenticado.UsuarioID;
        string ClienteID = _autenticado.ClienteID;
        int cant = 0, iRes = 0;
        int ultimo = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
        string PlanID = "";
        if (ClienteID == "2") //Proteccion
        {
            PlanID = "7";
            if (rdbModalidad.SelectedValue == "Nueva")
            {
                PlanID = "31";
            }
            else 
            {
                PlanID = "7";
            }
        }
        else if (ClienteID == "3")//SURA
            PlanID = "6";

        DateTime fechatemp;
        DateTime fechaInicio;
        DateTime fechaFinal;
        fechatemp = DateTime.Today;
        fechaInicio = new DateTime(fechatemp.Year, fechatemp.Month, 1);
        fechaFinal = new DateTime(fechatemp.Year, fechatemp.Month + 1, 1).AddDays(-1);
        string FechaInicio = fechaInicio.ToString();
        string FechaFin = fechaFinal.ToString();

        FechaInicio = Utilidades.FecUni(FechaInicio);
        FechaFin = Utilidades.FecUni(FechaFin);

        FechaInicio = fechaInicio.ToString("yyyy-MM-dd");
        FechaFin = fechaFinal.ToString("yyyy-MM-dd");
        int PlanAlumnoID = 0;
        string maxPlanAlumno = "";
        sSelectSQL = "SELECT MAX(PlanAlumnoID) as MAXIMO FROM PlanAlumno";
        Utilidades.maxRegistro(ref maxPlanAlumno, sSelectSQL, cn, ref Err);
        string CantidadClasesComplemen = "0", CantidadClasesRegulares = "0"; // cc2 = "";
        sSelectSQL = "Select clasesComplemen as MAXIMO from [Plan] Where PlanID = " + PlanID;
        Utilidades.maxRegistro(ref CantidadClasesComplemen, sSelectSQL, cn, ref Err);
        sSelectSQL = "Select clasesRegulares as MAXIMO from [Plan] Where PlanID = " + PlanID;
        Utilidades.maxRegistro(ref CantidadClasesRegulares, sSelectSQL, cn, ref Err);
        string saldoNegativo = "";
        sSelectSQL = "SELECT PlanCosto as MAXIMO FROM [Plan] Where PlanID = " + PlanID;
        Utilidades.maxRegistro(ref saldoNegativo, sSelectSQL, cn, ref Err);
        double saldoN = double.Parse(saldoNegativo);
        if (maxPlanAlumno == "") { PlanAlumnoID = 1; }
        else
        {
            PlanAlumnoID = int.Parse(maxPlanAlumno.Trim()) + 1;
        }
        string SucursalID = "1";
        int iRes3 = 0; bool band = false;
        eliminarAcumulado(UsuarioID);
        eliminarPlanAlumno(UsuarioID);
        iRes = insertarAlumno(PlanAlumnoID, UsuarioID, SucursalID, PlanID, FechaInicio, FechaFin, ClienteID, saldoN, cant, "0");
        if (iRes > 0)
        {
            //Insertar en PlanAlumnoAcumulado
            iRes3 = insertarPlanAcumulado(CantidadClasesRegulares, CantidadClasesComplemen, UsuarioID);
            if (iRes3 > 0)
            {
                band = true;
            }
            else
            {
                MostrarMsjModal("No se pudo realizar la asignación: " + Err, "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Error al Insertar el PlanAlumno: " + Err, "ERR");
        }
        if (band)
        {
            //Enviar Correo Infromando
            string destino = "academia@licsu.com";
            string asunto = "Usuario Plan Activo";
            string mensaje = "El usuario: " + Utilidades.EjeSQL("SELECT (UsuarioCedula+', '+UsuarioNombre+' '+UsuarioApellido)" +
                             " FROM usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true) + " realizó la activación de su plan." +
                             " Empresa: " + Utilidades.EjeSQL("SELECT ClienteNombre from Cliente Where ClienteID = " + _autenticado.ClienteID, cn, ref Err, true);
            Utilidades.EnviarCorreo(destino, asunto, mensaje, ref Err);
            Response.Redirect("Inicio.aspx");
        }
    }
    protected int insertarPlanAcumulado(string CantidadClasesRegulares, string CantidadClasesComplemen, string UsuarioID)
    {
        int res = 0;
        sSelectSQL = "INSERT INTO PlanAlumnoAcumulado (totalClasesRegulares, disponiblesClasesRegulares, UsuarioID, seleccionoClases, totalClasesComplemen, disponiblesClasesComplemen)" +
                     " VALUES (" + Utilidades.SiEsNulo(CantidadClasesRegulares, "N") + ", " + Utilidades.SiEsNulo(CantidadClasesRegulares, "T") + ", " + Utilidades.SiEsNulo(UsuarioID, "N") + ", 0, " + Utilidades.SiEsNulo(CantidadClasesComplemen, "N") + " ," + Utilidades.SiEsNulo(CantidadClasesComplemen, "T") + " )";
        cn.Open();
        SqlCommand addCmd2 = new SqlCommand(sSelectSQL, cn);
        try { res = addCmd2.ExecuteNonQuery(); }
        catch (SqlException sq) { MostrarMsjModal(sq.Message, "ERR"); }
        cn.Close();

        return res;
    }
    protected int insertarAlumno(int PlanAlumnoID, string UsuarioID, string SucursalID, string PlanID, string FechaInicio,
        string FechaFin, string ClienteID, double saldoN, int cant, string CantidadClases)
    {
        int iRes = 0;
        cn.Open();
        string sSelectSQL2 = "INSERT INTO PlanAlumno" +
                            "(PlanAlumnoID, UsuarioID, SucursalID, PlanID, PlanAlumnoFechaInicio, PlanAlumnoFechaFin " +
                            ",PlanAlumnoFechaRegistro" +
                            ",PlanAlumnoUsuarioRegistro,ClienteID, SaldoPositivo, SaldoNegativo, NumeroFactura, ClasesActivas, FacturaNota)" +
                            "VALUES(" + PlanAlumnoID + "," + Utilidades.SiEsNulo(UsuarioID, "N") + ", " + Utilidades.SiEsNulo(SucursalID, "N") + ", " + Utilidades.SiEsNulo(PlanID, "N") + ",CONVERT(VARCHAR(24),'" + FechaInicio + "',103)" +
                            ",CONVERT(VARCHAR(24),'" + FechaFin + "',103),SYSDATETIME(), 1, " + Utilidades.SiEsNulo(ClienteID, "N") +
                            "," + saldoN + ", 0, " + Utilidades.SiEsNulo(ClienteID + "_" + cant, "T") +
                            ", " + Utilidades.SiEsNulo(CantidadClases, "T") + "," + Utilidades.SiEsNulo("", "T") + ")";
        //MostrarMsjModal(sSelectSQL2, "");
        SqlCommand addCmd = new SqlCommand(sSelectSQL2, cn);
        try { iRes = addCmd.ExecuteNonQuery(); }
        catch (SqlException sq) { Err = sq.Message; }
        cn.Close();
        return iRes;
    }
    protected int eliminarPlanAlumno(string UsuarioID)
    {
        int res = 0;

        sSelectSQL = "DELETE FROM PlanAlumno WHERE UsuarioID = " + UsuarioID;
        cn.Open();
        try
        {
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            res = addCmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal("Error: " + sq.Message, "ERR");
        }

        cn.Close();
        return res;
    }
    protected int eliminarAcumulado(string UsuarioID)
    {
        int res = 0;

        sSelectSQL = "DELETE FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioID;
        cn.Open();
        try
        {
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            res = addCmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal("Error: " + sq.Message, "ERR");
        }
        cn.Close();
        return res;
    }
    protected void btnCambiar_Click(object sender, EventArgs e)
    {
        //Encaso de querer cambiar el plan......
        if (activoBool)
        {
            //El Plan Esta Activo
            sSelectSQL = "SELECT totalClasesRegulares FROM PlanAlumnoAcumulado WHERE UsuarioID =  " + _autenticado.UsuarioID;
            int TotalClases = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
            sSelectSQL = "INSERT INTO PlanEmpresaHistorial (PlanEmpresaUsuarioID, PlanEmpresaPlanEstado, PlanEmpresaFecha, PlanEmpresaTotalClases)" +
                                         " values(" + _autenticado.UsuarioID + ", 0,  CONVERT(DATE, SYSDATETIME(), 103), " + TotalClases + " )";
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            //Estado proximo --> Mes siguiente desactivado
            sSelectSQL = "UPDATE PlanEmpresa SET EstadoProximo = 0 WHERE UsuarioID = " + _autenticado.UsuarioID;
            cn.Open();
            SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
            try
            {
                cmd.ExecuteNonQuery();
                activoBool = false;
                btnDesactivar.Text = "Activar el Plan";
                cn.Close();
                Response.Redirect("Inicio.aspx");
            }
            catch (SqlException sq)
            {
                MostrarMsjModal("Error: " + sq.Message, "ERR");
                cn.Close();
            }
        }
        else
        {
            //El Plan Esta Desactivado
            sSelectSQL = "SELECT totalClasesRegulares FROM PlanAlumnoAcumulado WHERE UsuarioID =  " + _autenticado.UsuarioID;
            int TotalClases = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));

            sSelectSQL = "INSERT INTO PlanEmpresaHistorial (PlanEmpresaUsuarioID, PlanEmpresaPlanEstado, PlanEmpresaFecha, PlanEmpresaTotalClases)" +
                                         " values(" + _autenticado.UsuarioID + ", 1,  CONVERT(DATE, SYSDATETIME(), 103), " + TotalClases + ")";
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            sSelectSQL = "UPDATE PlanEmpresa SET EstadoProximo = 1 WHERE UsuarioID = " + _autenticado.UsuarioID;
            cn.Open();
            SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
            try
            {
                cmd.ExecuteNonQuery();
                activoBool = true;
                btnDesactivar.Text = "Desactivar el Plan";
                cn.Close();
                Response.Redirect("Inicio.aspx");
                ///asignarPlan();
            }
            catch (SqlException sq)
            {
                MostrarMsjModal("Error: " + sq.Message, "ERR");
                cn.Close();
            }
        }
    }

    protected void verUsuarios_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmpleadosActivos.aspx");
    }

    protected void verReservas_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmpleadosReservas.aspx");
    }

    protected void chkDescuento_CheckedChanged(object sender, EventArgs e)
    {
        //Enviar Correo indicando la autorizacion de nomina
        //De PlanAlumno Obtengo La PlanAlumnoFechaRegistro para comparar si han pasado tres dias y aun esta desactivado.
        sSelectSQL = "UPDATE planAutorizacion SET AutorizacionActivo = 1 WHERE UsuarioID = " + _autenticado.UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        Response.Redirect("Inicio.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarEstadisticas", "MostrarEstadisticas();", true);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarEstadisticas3", "MostrarEstadisticas3();", true);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarEstadisticas4", "MostrarEstadisticas4();", true);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarEstadisticas1", "MostrarEstadisticas1();", true);
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        sSelectSQL = "SELECT UsuarioID FROM Usuario WHERE UsuarioCedula = '" + txtBuscar.Text + "'";
        string UsuarioBusquedaID = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
        ViewState["UsuarioID"] = UsuarioBusquedaID;
        lblResultado.Text = "" + UsuarioBusquedaID.Length;
        if (UsuarioBusquedaID.Length > 0)
        {
            //Llenamos la data
            upResultadoNoExito.Visible = false;
            upResultadoExito.Visible = true;
            tcCedula.Text = txtBuscar.Text;
            tcTipo.SelectedValue = Utilidades.EjeSQL("SELECT UsuarioTipoEmp FROM Usuario WHERE UsuarioID = " + UsuarioBusquedaID, cn, ref Err, true);
            string PlanID = Utilidades.EjeSQL("SELECT PLANID FROM PLANALUMNO WHERE USuarioID = " + UsuarioBusquedaID + " ORDER BY PlanAlumnoID DESC", cn, ref Err, true);
            string PlanAlumnoID = Utilidades.EjeSQL("SELECT PLANALUMNOID FROM PLANALUMNO WHERE USuarioID = " + UsuarioBusquedaID + " ORDER BY PlanAlumnoID DESC", cn, ref Err, true);
            ViewState["PlanAlumnoID"] = PlanAlumnoID;
            tcNombres.Text = Utilidades.EjeSQL("SELECT (UsuarioNombre+' '+UsuarioApellido) FROM Usuario WHERE UsuarioID = " + UsuarioBusquedaID, cn, ref Err, true);
            tcPlan.Text = Utilidades.EjeSQL("SELECT PlanNombre FROM [PLan] WHERE PlanID = " + PlanID, cn, ref Err, true);
            tcTotalActivas.Text = Utilidades.EjeSQL("SELECT ClasesActivas FROM PlanAlumno WHERE PLanAlumnoID = " + PlanAlumnoID, cn, ref Err, true);
            tcFechaInicio.Text = Utilidades.EjeSQL("SELECT CONVERT(VARCHAR(11),PlanAlumnoFechaInicio,103) FROM PlanAlumno WHERE PLanAlumnoID = " + PlanAlumnoID, cn, ref Err, true);
            tcFechaFin.Text = Utilidades.EjeSQL("SELECT CONVERT(VARCHAR(11),PlanAlumnoFechaFin,103)  FROM PlanAlumno WHERE PLanAlumnoID = " + PlanAlumnoID, cn, ref Err, true);
            tcRegulares.Text = Utilidades.EjeSQL("SELECT totalClasesRegulares  FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioBusquedaID, cn, ref Err, true);
            tcRegularesDisp.Text = Utilidades.EjeSQL("SELECT disponiblesClasesRegulares  FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioBusquedaID, cn, ref Err, true);
            tcComplemen.Text = Utilidades.EjeSQL("SELECT totalClasesComplemen  FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioBusquedaID, cn, ref Err, true);
            tcComplemenDisp.Text = Utilidades.EjeSQL("SELECT disponiblesClasesComplemen  FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioBusquedaID, cn, ref Err, true);
        }
        else
        {
            upResultadoExito.Visible = false;
            upResultadoNoExito.Visible = true;
            lblResultado.Text = "No se encontró ningún resultado para la cédula = " + txtBuscar.Text;
        }
    }

    protected void BindGridViewListas()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT (SELECT ClaseDescripcion FROM Clase WHERE ClaseID = (SELECT ClaseID FROM ClasePLantilla WHERE ClasePlantillaID = li.ListaEsperaClaseID)) as Clase, " +
                        " (SELECT ClasePLantillaFecha FROM ClasePLantilla WHERE ClasePlantillaID = li.ListaEsperaClaseID) as Fecha, " +
                        " (SELECT ClasePLantillaID FROM ClasePLantilla WHERE ClasePlantillaID = li.ListaEsperaClaseID) as ClasePLantillaID, " +
                        " (SELECT ClasePLantillaHora FROM ClasePLantilla WHERE ClasePlantillaID = li.ListaEsperaClaseID) as Hora,  " +
                        " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = (SELECT ProfesorID FROM ClasePlantilla WHERE ClasePlantillaID = li.ListaEsperaClaseID)) as Profesor," +
                        " (SELECT ClienteNombre FROM Cliente WHERE ClienteID = (SELECT ClienteID FROM ClasePlantilla WHERE ClasePlantillaID = li.ListaEsperaClaseID)) as Unidad, " +
                        " COUNT(*) as Usuarios " +
                        " FROM ListaEspera li " +
                        " WHERE li.ListaEsperaEstado = 0 " +
                        " AND CONVERT (DATE, (SELECT ClasePLantillaFecha FROM ClasePLantilla WHERE ClasePlantillaID = li.ListaEsperaClaseID) , 103) >= CONVERT(DATE, SYSDATETIME(), 103) " +
                        " GROUP BY li.ListaEsperaClaseID " +
                        " ORDER BY Fecha ASC, Hora ASC";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Clase";
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

    protected void GridView5_PageIndexChanged(object sender, EventArgs e)
    {
        GridView5.EditIndex = -1;
        GridView5.SelectedIndex = -1;
        //GridView5.PageIndex = e.NewPageIndex;
        BindGridViewListas();
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        string activas = tcTotalActivas.Text;
        string regulares = tcRegulares.Text;
        string regularesD = tcRegularesDisp.Text;
        string complemen = tcComplemen.Text;
        string complemenD = tcComplemenDisp.Text;
        string fechaIni = tcFechaInicio.Text;
        string fechaFin = tcFechaFin.Text;
        string tipoEmp = tcTipo.SelectedValue;

        sSelectSQL = "UPDATE Usuario SET UsuarioTipoEmp = '" + tipoEmp + "' WHERE UsuarioID = " + ViewState["UsuarioID"];
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "UPDATE PlanAlumno SET ClasesActivas = " + activas + ", PlanAlumnoFechaInicio = CONVERT(DATE,'" + fechaIni + "',103), PlanAlumnoFechaFin = CONVERT(DATE,'" + fechaFin + "',103) " +
                    " WHERE PlanAlumnoID =  " + ViewState["PlanAlumnoID"];
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "UPDATE PlanAlumnoAcumulado SET totalClasesRegulares = " + regulares + ", totalClasesComplemen = " + complemen + " " +
                    " ,disponiblesClasesRegulares = '" + regularesD + "', disponiblesClasesComplemen = '" + complemenD + "' " +
                    " WHERE UsuarioID = " + ViewState["UsuarioID"];
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        if (Err == "")
        {
            MostrarMsjModal("Cambios Realizados con Exito", "EXI");
        }
        else
        {
            MostrarMsjModal("Error: " + Err, "ERR");
        }
    }

    protected void ActivarPlan_Click(object sender, EventArgs e)
    {
        if (ViewState["OpcionSeleccionada"].ToString() == "DesActual")
        {
            //Desactivar el plan para el mes en curso                
            sSelectSQL = "UPDATE PlanEmpresa SET PlanActivo = 0, fechaUltimo = CONVERT(DATE, SYSDATETIME(), 103) " +
                         " WHERE UsuarioID = " + _autenticado.UsuarioID;
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            string TotalClases = Utilidades.EjeSQL("SELECT totalClasesRegulares FROM planalumnoacumulado" +
                                                   " WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
            sSelectSQL = "INSERT INTO PLANEMPRESAHISTORIAL(PLANEMPRESAUSUARIOID, PLANEMPRESAPLANESTADO, " +
                        " PLANEMPRESAFECHA, PLANEMPRESATOTALCLASES) VALUES (" + _autenticado.UsuarioID + ", " +
                        " 0, CONVERT(DATE, SYSDATETIME(),103), " + TotalClases + ")";
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            //Response.Redirect("Inicio.aspx");
            //Generar correo de aviso al administrador.
            string destino = Utilidades.EjeSQL("SELECT UsuarioCorreo FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
            string asunto = "Inactivación de Plan";
            string mensaje = "Señor(a). " +
                              Utilidades.EjeSQL("SELECT (UsuarioNombre+' '+UsuarioApellido) as Nombre FROM Usuario WHERE UsuarioID =" + _autenticado.UsuarioID, cn, ref Err, true) +
                              "<br> Hemos recibido su solicitud de retiro o congelación en el programa; efectiva en el Mes Próximo. Podría indicarnos si el motivo es personal o" +
                              " se relaciona en algo con el programa?. Agradecemos su colaboración con esta respuesta y de antemano bienvenido(a) " +
                              " de regreso cuando usted lo desee.";
            Utilidades.EnviarCorreoDes(ref Err, asunto, mensaje, destino);
            MostrarMsjModal("Se ha Desactivado el Plan Para el Mes Actual Correctamente.", "");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeDelete2').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }
        else if (ViewState["OpcionSeleccionada"].ToString() == "DesProximo")
        {
            //Desactivar el plan para el proximo mes
            sSelectSQL = "UPDATE PlanEmpresa SET EstadoProximo = 0 WHERE UsuarioID = " + _autenticado.UsuarioID;
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            //Response.Redirect("Inicio.aspx");
            //Generar correo de aviso al administrador.
            string destino = Utilidades.EjeSQL("SELECT UsuarioCorreo FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
            string asunto = "Inactivación de Plan";
            string mensaje = "Señor(a). " +
                              Utilidades.EjeSQL("SELECT (UsuarioNombre+' '+UsuarioApellido) as Nombre FROM Usuario WHERE UsuarioID =" + _autenticado.UsuarioID, cn, ref Err, true) +
                              "<br> Hemos recibido su solicitud de retiro o congelación en el programa; efectiva en el Mes Próximo. Podría indicarnos si el motivo es personal o" +
                              " se relaciona en algo con el programa?. Agradecemos su colaboración con esta respuesta y de antemano bienvenido(a) " +
                              " de regreso cuando usted lo desee.";
            Utilidades.EnviarCorreoDes(ref Err, asunto, mensaje, destino);
            MostrarMsjModal("Se ha Desactivado el Plan Para el Próximo Mes Correctamente.", "");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeDelete2').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }
        else if (ViewState["OpcionSeleccionada"].ToString() == "ActActual")
        {
            //Activar el plan para el mes en curso
            sSelectSQL = "SELECT PlanActivo FROM PlanEmpresa WHERE UsuarioID = " + _autenticado.UsuarioID;
            bool Activo = bool.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
            if (Activo) //Tiene Plan Activo para Este mes
            {
                MostrarMsjModal("Usted ya tiene un Plan Activo para este Mes", "");
            }
            else //Activar el plan para este mes
            {
                string TotalClases = Utilidades.EjeSQL("SELECT totalClasesRegulares FROM planalumnoacumulado" +
                                                   " WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
                sSelectSQL = "INSERT INTO PLANEMPRESAHISTORIAL(PLANEMPRESAUSUARIOID, PLANEMPRESAPLANESTADO, " +
                            " PLANEMPRESAFECHA, PLANEMPRESATOTALCLASES) VALUES (" + _autenticado.UsuarioID + ", " +
                            " 1, CONVERT(DATE, SYSDATETIME(),103), " + TotalClases + ")";
                Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                sSelectSQL = "UPDATE PlanEmpresa SET PlanActivo = 1, fechaUltimo = CONVERT(DATE, SYSDATETIME(),103) WHERE UsuarioID = " + _autenticado.UsuarioID;
                Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                DateTime MesProximo = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1);
                sSelectSQL = "UPDATE PlanEmpresa SET EstadoProximo = 1, MesProximo = CONVERT(DATE,'" + MesProximo.ToString("d") + "',103) WHERE UsuarioID = " + _autenticado.UsuarioID;
                //MostrarMsjModal(sSelectSQL, "");
                Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                sSelectSQL = "UPDATE seleccionoClases = 1 FROM PlanAlumnoAcumulado WHERE UsuarioID = " + _autenticado.UsuarioID;
                Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                asignarPlan();
            }
        }
        else if (ViewState["OpcionSeleccionada"].ToString() == "ActProximo")
        {
            //Activar el plan para el proximo mes
            DateTime MesProximo = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1);
            sSelectSQL = "UPDATE PlanEmpresa SET EstadoProximo = 1, MesProximo = CONVERT(DATE, '" + MesProximo.ToString("d") + "', 103) WHERE UsuarioID = " + _autenticado.UsuarioID;
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            //Response.Redirect("Inicio.aspx");
            MostrarMsjModal("Se ha activado el Plan Para el Próximo Mes Correctamente.", "");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeDelete2').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }
        dplOpciones.Items.Clear();
    }

    protected void btnAccion_Click(object sender, EventArgs e)
    {
        string mensaje = "";
        ViewState["OpcionSeleccionada"] = dplOpciones.SelectedValue; 
        string fecha_registro = Utilidades.EjeSQL("SELECT UsuarioFechaRegistro FROM Usuario WHERE UsuarioID =" + _autenticado.UsuarioID, cn, ref Err, true);
        DateTime fechar = DateTime.Parse(fecha_registro);
        if (dplOpciones.SelectedValue == "")
        {
            MostrarMsjModal("Debe seleccionar una opción", "ERR");
        }
        else if (dplOpciones.SelectedValue == "DesActual")
        {
            mensaje = "¿Está seguro de Desactivar el Plan para el Mes Actual?";
        }
        else if (dplOpciones.SelectedValue == "DesProximo")
        {
            mensaje = "¿Está seguro de Desactivar el Plan para el Próximo Mes?";
        }
        else if (dplOpciones.SelectedValue == "ActActual")
        {
            mensaje = "¿Está seguro de Activar el Plan para el Mes Actual?";
            if (fechar >= DateTime.Parse("01/01/2017"))
            {
                tpoModalidad.Visible = true;
            }
            else
            {
                tpoModalidad.Visible = false;
            }
        }
        else if (dplOpciones.SelectedValue == "ActProximo")
        {
            mensaje = "¿Está seguro de Activar el Plan para el Próximo Mes?";
            if (fechar >= DateTime.Parse("01/01/2017"))
            {
                tpoModalidad.Visible = true;
            }
            else
            {
                tpoModalidad.Visible = false;
            }
        }        
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('closeDelete3').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        MostrarPregunta2(mensaje);
    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inicio.aspx");
    }

    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void EnviarAvisoLista(string clase)
    {
        sSelectSQL = " SELECT TOP 1 ListaEsperaUsuarioID FROM ListaEspera " +
                     " WHERE ListaEsperaClaseID = " + clase +
                     " AND ListaEsperaEstado = 0 ORDER BY ListaEsperaID ASC";
        string UsuarioAviso = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
        sSelectSQL = " SELECT UsuarioCorreo FROM Usuario WHERE UsuarioID = " + UsuarioAviso;
        string destino = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
        if (Utilidades.EmailValido(destino))
        {
            sSelectSQL = " SELECT (UsuarioNombre+' '+UsuarioApellido) as Usuario FROM Usuario WHERE UsuarioID = " + UsuarioAviso;
            string UsuarioDatos = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
            sSelectSQL = " SELECT  (dbo.Clase.ClaseDescripcion+' '+dbo.ClasePlantilla.ClasePlantillaFecha+' '+dbo.ClasePlantilla.ClasePlantillaHora) as Clase " +
                         " FROM dbo.Clase INNER JOIN " +
                         " dbo.ClasePlantilla ON dbo.Clase.ClaseID = dbo.ClasePlantilla.ClaseID " +
                         " WHERE dbo.ClasePlantilla.ClasePlantillaID = " + clase;
            string Clase = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
            string asunto = "¡Cupo Disponible!";
            string mensaje = " Señor(a) " + UsuarioDatos + "<br>" +
                             " Se ha habilitado un cupo en la clase " + Clase + ". " +
                             " Ingrese pronto a inscribirse.";
            Utilidades.EnviarCorreo(destino, asunto, mensaje, ref Err);
            sSelectSQL = " SELECT TOP 1 ListaEsperaID FROM ListaEspera " +
                    " WHERE ListaEsperaClaseID = " + clase +
                    " AND ListaEsperaEstado = 0 ORDER BY ListaEsperaID ASC";
            string ListaEsperaID = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
            sSelectSQL = "DELETE FROM ListaEspera WHERE ListaEsperaID = " + ListaEsperaID;
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        }
    }

    protected void btnAvisar_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Inicio.aspx");
        //Revisar todas las listas....
        //Obtener los IDs de las Clases que tienen usuarios en lista de espera
        sSelectSQL = "SELECT COUNT(DISTINCT(ListaEsperaClaseID)) " +
                      " FROM ListaEspera " +
                      " WHERE " +
                      " (SELECT Convert(DATE,ClasePlantillaFecha,103) FROM ClasePlantilla WHERE ClasePlantillaID = ListaEsperaClaseID) >= " +
                      " CONVERT(DATE, SYSDATETIME(),103) ";
        int CantClases = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
        sSelectSQL = "SELECT DISTINCT(ListaEsperaClaseID) as ListaEsperaClaseID" +
                      " FROM ListaEspera " +
                      " WHERE " +
                      " (SELECT Convert(DATE,ClasePlantillaFecha,103) FROM ClasePlantilla WHERE ClasePlantillaID = ListaEsperaClaseID) >= " +
                      " CONVERT(DATE, SYSDATETIME(),103) ";
        if (CantClases > 0)
        {
            int contador = 0;
            string[] ClasePlantillaIDs = new string[CantClases];
            SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
            SqlDataReader reader;
            try
            {
                cn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClasePlantillaIDs[contador] = reader["ListaEsperaClaseID"].ToString();
                    contador++;
                }
                reader.Close();
                cn.Close();
            }
            catch (SqlException Sqlex)
            {
                cn.Close();
                Err = "Error de SQL: " + Sqlex.Message;
            }
            //Hacer los pasos por Clase
            //1. Buscar si hay cupo en la Clase
            for (int i = 0; i < ClasePlantillaIDs.Length; i++)
            {
                sSelectSQL = "SELECT ClasePlantillaCupo FROM ClasePlantilla Where ClasePlantillaID = " + ClasePlantillaIDs[i];
                int cupos = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                if (cupos > 0)
                {
                    //Buscar el Intervalo de Tiempo
                    EnviarAvisoLista(ClasePlantillaIDs[i]);
                }
            }
            Response.Redirect("Inicio.aspx");
        }
    }

    protected void Unnamed_Tick(object sender, EventArgs e)
    {
        sSelectSQL = "SELECT COUNT(DISTINCT(ListaEsperaClaseID)) " +
                      " FROM ListaEspera " +
                      " WHERE " +
                      " (SELECT Convert(DATE,ClasePlantillaFecha,103) FROM ClasePlantilla WHERE ClasePlantillaID = ListaEsperaClaseID) >= " +
                      " CONVERT(DATE, SYSDATETIME(),103) ";
        int CantClases = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
        sSelectSQL = "SELECT DISTINCT(ListaEsperaClaseID) as ListaEsperaClaseID" +
                      " FROM ListaEspera " +
                      " WHERE " +
                      " (SELECT Convert(DATE,ClasePlantillaFecha,103) FROM ClasePlantilla WHERE ClasePlantillaID = ListaEsperaClaseID) >= " +
                      " CONVERT(DATE, SYSDATETIME(),103) ";
        if (CantClases > 0)
        {
            int contador = 0;
            string[] ClasePlantillaIDs = new string[CantClases];
            SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
            SqlDataReader reader;
            try
            {
                cn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClasePlantillaIDs[contador] = reader["ListaEsperaClaseID"].ToString();
                    contador++;
                }
                reader.Close();
                cn.Close();
            }
            catch (SqlException Sqlex)
            {
                cn.Close();
                Err = "Error de SQL: " + Sqlex.Message;
            }
            //Hacer los pasos por Clase
            //1. Buscar si hay cupo en la Clase
            for (int i = 0; i < ClasePlantillaIDs.Length; i++)
            {
                sSelectSQL = "SELECT ClasePlantillaCupo FROM ClasePlantilla Where ClasePlantillaID = " + ClasePlantillaIDs[i];
                int cupos = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                if (cupos > 0)
                {
                    //Buscar el Intervalo de Tiempo
                    EnviarAvisoLista(ClasePlantillaIDs[i]);
                }
            }
            Response.Redirect("Inicio.aspx");
        }
    }
}