using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class registrarEmpresa : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string sErr = string.Empty;
    string sSelectSQL = string.Empty;
    string idEmpresa = string.Empty;
    string empleado = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            idEmpresa = Request.QueryString["idEmpresa"];
            empleado = Request.QueryString["tpo"];
            if (empleado == "1")
            {
                empleado = "Empleado";
                ViewState["empleado"] = "Empleado";
                ViewState["idEmpresa"] = idEmpresa;
                phTipoEmp.Visible = false;
            }
            else 
            {
                empleado = "Contratado/Otro";
                ViewState["empleado"] = "Contratado/Otro";
                ViewState["idEmpresa"] = idEmpresa;
                phTipoEmp.Visible = true;
            }
            
        }
        
    }
    
    protected void btnRegistrarEmmpresa_Click(object sender, EventArgs e)
    {
        bool band_empleado = false;
        if (!UsuarioRegistrado(txtCedulaEmp.Text))
        {
            btnRegistrarEmmpresa.Enabled = true;
            string Cedula = txtCedulaEmp.Text;
            string Celular = txtCelular.Text;
            string Observacion = txtObservacion.Text;
            string Clave = txtClaveNuevaFit.Text;
            string ClaveDos = txtClaveRepetirFit.Text;
            //string ActivarPlan = dplActivarPlan.SelectedValue;
            string Nombres = string.Empty;
            string Correo = string.Empty;
            empleado = ViewState["empleado"].ToString();
            idEmpresa = ViewState["idEmpresa"].ToString();

            if (empleado == "Empleado")
            {
                if (EsClienteEmpleado(txtCedulaEmp.Text, idEmpresa))
                {
                    Nombres = Utilidades.EjeSQL("SELECT EmpleadoNombre FROM EmpleadoEmp WHERE EmpleadoCedula = '" + Cedula + "'", cn, ref sErr, true);
                    Correo = Utilidades.EjeSQL("SELECT EmpleadoEmail FROM EmpleadoEmp WHERE EmpleadoCedula = '" + Cedula + "'", cn, ref sErr, true);
                    band_empleado = true;
                    sSelectSQL = string.Empty;
                }
            }
            else
            {
                Nombres = txtNombre.Text;
                Correo = txtEmail.Text;
                band_empleado = true;
            }

            if (band_empleado)
            {
                int usuarioID = int.Parse((Utilidades.EjeSQL("SELECT MAX(UsuarioID) FROM Usuario", cn, ref sErr, true)).Trim()) + 1;
                int rolID = int.Parse((Utilidades.EjeSQL("SELECT MAX(UsuarioRolID) FROM UsuarioRol", cn, ref sErr, true)).Trim()) + 1;
                sSelectSQL = string.Empty;
                sSelectSQL = "INSERT INTO Usuario (UsuarioID,UsuarioCedula,UsuarioClave,UsuarioNombre,UsuarioApellido,"
                                    + " UsuarioCorreo,UsuarioFechaNacimiento,UsuarioEstadoCivil,UsuarioSexo,UsuarioFoto,UsuarioTelefono,"
                                    + " UsuarioCelular1,UsuarioCelular2,UsuarioEPS,UsuarioObservacion,UsuarioActivo,UsuarioFechaRegistro,"
                                    + " UsuarioUsuarioRegistro,UsuarioRiesgos,UsuarioPension,UsuarioTipoEmp,UsuarioPrueba)"
                                    + " VALUES(" + usuarioID + "," + Utilidades.SiEsNulo(Cedula, "T") + ",'" + Clave + "','" + Nombres + "','','" + Correo + "','',"
                                    + " '','','','','" + Celular + "','','','" + Observacion + "',"
                                    + " 1,SYSDATETIME(),1,'','', '" + empleado + "', 0)";
                Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                
                if (sErr.Length == 0)
                {
                    sSelectSQL = string.Empty;
                    sSelectSQL = " INSERT INTO UsuarioRol(UsuarioRolID,UsuarioID,SucursalID,RolID,ClienteID,UsuarioRolFechaRegistro,UsuarioUsuarioRegistro)"
                                + " VALUES(" + rolID + "," + usuarioID + ",1 , 3 ," + idEmpresa + ", SYSDATETIME(), 1)";
                    Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                }
                int TotalClases = 0;
                string urlTemplate = string.Empty;

                if (idEmpresa == "2")
                {
                    TotalClases = 12;
                    urlTemplate = "~/sistema/plantillas/bienvenidaProteccion.html";
                }
                else
                {
                    TotalClases = 8;
                    urlTemplate = "~/sistema/plantillas/bienvenidaSura.html";
                }
                if (sErr.Length == 0)
                {
                    sSelectSQL = string.Empty;
                    sSelectSQL = " INSERT INTO PlanEmpresaHistorial (PlanEmpresaUsuarioID, PlanEmpresaPlanEstado, PlanEmpresaFecha, PlanEmpresaTotalClases)" +
                                " values(" + usuarioID + ", 0,  CONVERT(DATE, SYSDATETIME(), 103), " + TotalClases + ")";
                    Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                }

                DateTime MesProximo = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1);
                if (sErr.Length == 0)
                {
                    sSelectSQL = string.Empty;
                    sSelectSQL = " INSERT INTO PLANEMPRESA(UsuarioID, PlanActivo, fechaUltimo, TotalClases, MesProximo, EstadoProximo, Confirmo) VALUES"
                                + " (" + usuarioID + ",0, CONVERT(DATE, SYSDATETIME(), 109), " + TotalClases + ", CONVERT(DATE, '" + MesProximo.ToString("d") + "',103), 0, 0)";
                    Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                }

                if (sErr.Length == 0)
                {
                    sSelectSQL = string.Empty;
                    sSelectSQL = " INSERT INTO PlanAutorizacion (AutorizacionActivo, FechaRegistro, UsuarioID, CorreoEnviado)" +
                                        " VALUES(0, SYSDATETIME(), " + usuarioID + ", 0)";
                    Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                }

                if (Utilidades.EmailValido(Correo) && sErr.Length == 0)
                {
                    string usuarioNombre = Nombres;
                    string asunto = "Bienvenido(a) a Fitness Li";
                    string destino = Correo;
                    string savePath = Server.MapPath("~/sistema/documentos/");
                    sSelectSQL = "SELECT ArchivoNombre from Archivos WHERE TipoDocumento='DescripcionEmp' ";
                    string NombreDescripcion = savePath + Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
                    sSelectSQL = "SELECT ArchivoNombre from Archivos WHERE TipoDocumento='Tutorial' ";
                    string NombreTutorial = savePath + Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
                    sSelectSQL = "SELECT ArchivoNombre from Archivos WHERE TipoDocumento='Descuento' ";
                    string NombreDescuento = savePath + Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
                    if (idEmpresa == "2")//Proteccion
                        Utilidades.EnviarCorreoRegLicsu(ref sErr, asunto, urlTemplate, destino, NombreDescripcion, NombreTutorial);
                    else if (idEmpresa == "3")
                        Utilidades.EnviarCorreoRegLicsu(ref sErr, asunto, urlTemplate, destino, NombreDescripcion, NombreTutorial, NombreDescuento);
                    asunto = "Registro de Nuevo Usuario";
                    string mensaje = "Se ha registrado el usuario: " + Nombres + ", Correo: " + Correo + " Cedula: " + Cedula;
                    Utilidades.EnviarCorreoAviso(ref sErr, asunto, mensaje);
                    if (empleado != "Empleado")
                    {
                        destino = "academia@licsu.com";
                        asunto = "Registro de Usuario Contratado/Temporal";
                        mensaje = "El usuario " + Nombres + ", Cedula:  " + Cedula + " se ha registrado con éxito.\n Su tipo de empleado es: " + empleado + ".\n" +
                            " Sus datos de contacto son: " + Correo + ", " + Celular;
                        Utilidades.EnviarCorreo(destino, asunto, mensaje, ref sErr);
                    }
                    if (sErr == "")
                    {
                        Response.Redirect("ingresar.aspx?exito=1");
                    }
                    else
                    {
                        MostrarMsjModal("Ocurrio un error al registrarse: " + sErr, "ERR");
                    }
                }
            }
            else
            {
                MostrarMsjModal("La Cédula: " + txtCedulaEmp.Text + " no se encuentra en la base de datos. Contacte con su Empresa.", "ADV");
            }
        }
        else
        {
            MostrarMsjModal("La Cédula ingresada ya se encuentra registrada en el sistema.", "ERR");
        }
    }
    private bool UsuarioRegistrado(string sCedula)
    {
        sSelectSQL = "SELECT UsuarioID FROM Usuario WHERE UsuarioCedula='" + sCedula + "'";
        string id = Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
        if (id == "")
        {
            return false;
        }
        return true;
    }

    private bool esValido()
    {
        return true;
    }

    private bool EsClienteEmpleado(string sCedula, string sClienteID)
    {
        sSelectSQL = "SELECT CE.EmpleadoCedula FROM EmpleadoEmp CE WHERE CE.EmpleadoCedula='" + sCedula + "' AND CE.ClienteID=" + sClienteID;
        string cedula = Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
        if (cedula != "-1")
        {
            return true;
        }
        return false;
    }

    private void limpiarCampos()
    { }

    public static Boolean EsFecha(String fecha)
    {
        try
        {
            string sFechaUni = fecha.Substring(6, 4) + fecha.Substring(5, 1) + fecha.Substring(3, 2) + fecha.Substring(2, 1) + fecha.Substring(0, 2);
            DateTime.Parse(sFechaUni);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static Boolean EsCorreo(String correo)
    {
        bool isEmail = false;
        try
        {
            isEmail = Regex.IsMatch(correo, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
        catch
        {
            return false;
        }
        return isEmail;
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