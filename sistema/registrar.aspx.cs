using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class registrar : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string sErr = string.Empty;
    string sSelectSQL = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarListados();
        }
    }

    protected void dplUnidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplUnidad.SelectedValue != "")
        {
            if (dplUnidad.SelectedValue == "1")
            {
                phFitness.Visible = true;
                phEmpresa.Visible = false;
            }
            else
            {
                phEmpresa.Visible = true;
                phFitness.Visible = false;
            }
        }
    }
    protected void dplRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtRol.SelectedValue != "")
        {
            if (txtRol.SelectedValue == "2")
            {
                phFitness.Visible = true;
                phEmpresa.Visible = false;
            }
            else
            {
                phEmpresa.Visible = true;
                phFitness.Visible = false;
            }
        }
    }

    private void CargarListados()
    {
        sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM [Cliente] ORDER BY TXT"; 
        Utilidades.CargarListado(ref dplUnidad, sSelectSQL, cn, ref sErr, true);

        sSelectSQL = "SELECT RolID AS VAL, RolDescripcion AS TXT FROM Rol  WHERE RolID > 1 ORDER BY VAL";
        Utilidades.CargarListado(ref txtRol, sSelectSQL, cn, ref sErr, true);
    }

    protected void ddlTipoEmpleado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTipoEmpleado.SelectedValue != "Empleado")
        {
            phTipoEmp.Visible = true;
        }
        else
        {
            phTipoEmp.Visible = false;
        }
    }

    protected void btnRegistrarEmmpresa_Click(object sender, EventArgs e)
    {
        if (EsClienteEmpleado(txtCedulaEmp.Text, dplUnidad.SelectedValue))
        {
            if (!UsuarioRegistrado(txtCedulaEmp.Text))
            {
                string Cedula = txtCedulaEmp.Text;
                string Celular = txtCelularEmp.Text;
                string Observacion = txtObservacion.Text;
                string Clave = txtClaveNuevaEmp.Text;
                string ClaveDos = txtClaveRepetirEmp.Text;
                string ActivarPlan = dplActivarPlan.SelectedValue;
                string Nombres = string.Empty;
                string Correo = string.Empty;
                if (ddlTipoEmpleado.SelectedValue.CompareTo("Empleado") == 0)
                {
                    Nombres = Utilidades.EjeSQL("SELECT EmpleadoNombre FROM EmpleadoEmp WHERE EmpleadoCedula = '" + Cedula + "'", cn, ref sErr, true);
                    Correo = Utilidades.EjeSQL("SELECT EmpleadoEmail FROM EmpleadoEmp WHERE EmpleadoCedula = '" + Cedula + "'", cn, ref sErr, true);
                }
                else
                {
                    Nombres = txtNombreEmp.Text;
                    Correo = txtEmailEmp.Text;
                }

                int usuarioID = int.Parse((Utilidades.EjeSQL("SELECT MAX(UsuarioID) FROM Usuario", cn, ref sErr, true)).Trim()) + 1;
                int rolID = int.Parse((Utilidades.EjeSQL("SELECT MAX(UsuarioRolID) FROM UsuarioRol", cn, ref sErr, true)).Trim()) + 1;

                sSelectSQL = "INSERT INTO Usuario (UsuarioID,UsuarioCedula,UsuarioClave,UsuarioNombre,UsuarioApellido,"
                                    + " UsuarioCorreo,UsuarioFechaNacimiento,UsuarioEstadoCivil,UsuarioSexo,UsuarioFoto,UsuarioTelefono,"
                                    + " UsuarioCelular1,UsuarioCelular2,UsuarioEPS,UsuarioObservacion,UsuarioActivo,UsuarioFechaRegistro,"
                                    + " UsuarioUsuarioRegistro,UsuarioRiesgos,UsuarioPension,UsuarioTipoEmp,UsuarioPrueba)"
                                    + " VALUES(" + usuarioID + "," + Utilidades.SiEsNulo(Cedula, "T") + ",'" + Clave + "','" + Nombres + "','','" + Correo + "','',"
                                    + " '','','','','" + Celular + "','','','" + Observacion + "',"
                                    + " 1,SYSDATETIME(),1,'','', Empleado, 0)";
                Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                sSelectSQL = " INSERT INTO UsuarioRol(UsuarioRolID,UsuarioID,SucursalID,RolID,ClienteID,UsuarioRolFechaRegistro,UsuarioUsuarioRegistro)"
                            + " VALUES(" + rolID + "," + usuarioID + ",1 , 3 ," + dplUnidad.SelectedValue + ", SYSDATETIME(), 1)";
                Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);

                int TotalClases = 0;
                string urlTemplate = string.Empty;

                if (dplUnidad.SelectedValue == "2")
                {
                    TotalClases = 12;
                    urlTemplate = "~/sistema/plantillas/bienvenidaProteccion.html";
                }
                else
                {
                    TotalClases = 8;
                    urlTemplate = "~/sistema/plantillas/bienvenidaSura.html";
                }

                sSelectSQL = " INSERT INTO PlanEmpresaHistorial (PlanEmpresaUsuarioID, PlanEmpresaPlanEstado, PlanEmpresaFecha, PlanEmpresaTotalClases)" +
                                " values(" + usuarioID + ", 0,  CONVERT(DATE, SYSDATETIME(), 103), " + TotalClases + ")";
                Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);

                DateTime MesProximo = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1);
                sSelectSQL = " INSERT INTO PLANEMPRESA(UsuarioID, PlanActivo, fechaUltimo, TotalClases, MesProximo, EstadoProximo) VALUES"
                            + " (" + usuarioID + ",0, CONVERT(DATE, SYSDATETIME(), 109), " + TotalClases + ", CONVERT(DATE, '" + MesProximo.ToString("d") + "',103), 0)";
                Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);

                sSelectSQL = " INSERT INTO PlanAutorizacion (AutorizacionActivo, FechaRegistro, UsuarioID, CorreoEnviado)" +
                                    " VALUES(0, SYSDATETIME(), " + usuarioID + ", 0)";
                Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);

                if (dplActivarPlan.SelectedValue == "Si")
                {
                    sSelectSQL = " UPDATE PlanEmpresa SET PlanActivo = 1, EstadoProximo = 1 WHERE UsuarioID = " + usuarioID;
                    Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                    sSelectSQL = " UPDATE PlanEmpresaHistorial SET PlanEmpresaPlanEstado = 1  WHERE PlanEmpresaUsuarioID = " + usuarioID;
                    Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                    asignarPlan(usuarioID);
                }
                if (Utilidades.EmailValido(Correo))
                {
                    string usuarioNombre = Nombres;
                    string asunto = "Bienvenido(a) a Fitness Li";
                    string destino = Correo;
                    string savePath = Server.MapPath("~/sistema/documentos/");
                    sSelectSQL = "SELECT ArchivoNombre from Archivos WHERE TipoDocumento='Descripcion' ";
                    string NombreDescripcion = savePath + Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
                    sSelectSQL = "SELECT ArchivoNombre from Archivos WHERE TipoDocumento='Tutorial' ";
                    string NombreTutorial = savePath + Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
                    sSelectSQL = "SELECT ArchivoNombre from Archivos WHERE TipoDocumento='Descuento' ";
                    string NombreDescuento = savePath + Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
                    if (dplUnidad.SelectedValue == "2")//Proteccion
                        Utilidades.EnviarCorreoRegLicsu(ref sErr, asunto, urlTemplate, destino, NombreDescripcion, NombreTutorial);
                    else if (dplUnidad.SelectedValue == "3")
                        Utilidades.EnviarCorreoRegLicsu(ref sErr, asunto, urlTemplate, destino, NombreDescripcion, NombreTutorial, NombreDescuento);
                    asunto = "Registro de Nuevo Usuario";
                    string mensaje = "Se ha registrado el usuario: " + Nombres + ", Correo: " + Correo + " Cedula: " + Cedula;
                    Utilidades.EnviarCorreoAviso(ref sErr, asunto, mensaje);
                    if (ddlTipoEmpleado.SelectedValue == "Contratado")
                    {
                        destino = "academia@licsu.com";
                        asunto = "Registro de Usuario Contratado/Temporal";
                        mensaje = "El usuario " + Nombres + ", Cedula:  " + Cedula + " se ha registrado con éxito.\n Su tipo de empleado es: " + ddlTipoEmpleado.SelectedValue + ".\n" +
                            " Sus datos de contacto son: " + Correo + ", " + Celular;
                        Utilidades.EnviarCorreo(destino, asunto, mensaje, ref sErr);
                    }
                    Response.Redirect("ingresar.aspx");
                }
            }
            else
            {
                MostrarMsjModal("La Cédula ingresada ya se encuentra registrada en el sistema.", "ERR");
            }
        }
        else
        {
            MostrarMsjModal("La Cédula: " + txtCedulaEmp.Text + " no se encuentra en la base de datos. Contacte con su Empresa.", "ADV");
        }
    }

    protected void btnAddFit_Click(object sender, EventArgs e)
    {
        //Registrar usuario fitnessLi            
        if (!UsuarioRegistrado(txtCedulaFit.Text))
        {
            string sNombre = txtNombre.Text;
            string sApellido = txtApellido.Text;
            string sClave = txtClaveNuevaFit.Text;
            string sCelular = txtCelularFit.Text;
            string sTelefono = txtTelefono.Text;
            string sCelular2 = txtCelular2.Text;
            string sCorreo = txtEmail.Text;
            string sFechaNacimiento = txtFechaNacimiento.Text;
            string sEstadoCivil = ddlEstadoCivil.SelectedValue;
            string sSexo = string.Empty; if (rdM.Checked) sSexo = "M"; else sSexo = "F";
            string sEPS = txtEPS.Text;
            string sObservacion = txtObservacionFit.Text;
            string riesgos = txtRiesgos.Text;
            string pension = txtPension.Text;
            string sCedula = txtCedulaFit.Text;
            int usuarioID = int.Parse((Utilidades.EjeSQL("SELECT MAX(UsuarioID) FROM Usuario", cn, ref sErr, true)).Trim()) + 1;
            int rolID = int.Parse((Utilidades.EjeSQL("SELECT MAX(UsuarioRolID) FROM UsuarioRol", cn, ref sErr, true)).Trim()) + 1;

            sSelectSQL = "INSERT INTO Usuario (UsuarioID,UsuarioCedula,UsuarioClave,UsuarioNombre,UsuarioApellido,"
                       + "UsuarioCorreo,UsuarioFechaNacimiento,UsuarioEstadoCivil,UsuarioSexo,UsuarioFoto,UsuarioTelefono,"
                       + "UsuarioCelular1,UsuarioCelular2,UsuarioEPS,UsuarioObservacion,UsuarioActivo,UsuarioFechaRegistro,UsuarioUsuarioRegistro,UsuarioRiesgos,UsuarioPension,UsuarioTipoEmp,UsuarioPrueba)"
                       + "VALUES(" + usuarioID + "," + Utilidades.SiEsNulo(sCedula, "T") + ",'" + sClave + "','" + sNombre + "','" + sApellido + "','" + sCorreo + "'," + Utilidades.SiEsNulo(sFechaNacimiento, "F") + ","
                       + "'" + sEstadoCivil + "','" + sSexo + "','','" + sTelefono + "','" + sCelular + "','" + sCelular2 + "','" + sEPS + "','" + sObservacion + "',"
                       + "1,SYSDATETIME(),1,'" + riesgos + "','" + pension + "','', 0)";
            Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);

            sSelectSQL = "INSERT INTO UsuarioRol(UsuarioRolID,UsuarioID,SucursalID,RolID,ClienteID,UsuarioRolFechaRegistro,UsuarioUsuarioRegistro)"
                        + " VALUES(" + rolID + "," + usuarioID + ",1 , "+txtRol.SelectedValue+" , 1 ,SYSDATETIME(),1)";
            Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);

            sSelectSQL = " INSERT INTO planAutorizacion(AutorizacionActivo, FechaRegistro, UsuarioID, CorreoEnviado) " +
                                     " VALUES (0, SYSDATETIME(), " + usuarioID + ", 0)";
            Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);

            if (Utilidades.EmailValido(sCorreo))
            {
                string asunto = "Bienvenido(a) a Fitness Li";
                string urlTemplate = "~/sistema/plantillas/bienvenidaLicsu.htm";
                string destino = sCorreo;
                string savePath = Server.MapPath("~/sistema/documentos/");
                sSelectSQL = "SELECT ArchivoNombre from Archivos WHERE TipoDocumento='Descripcion' ";
                string NombreHorario = savePath + Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
                sSelectSQL = "SELECT ArchivoNombre from Archivos WHERE TipoDocumento='Normas' ";
                string NombreNormas = savePath + Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
                Utilidades.EnviarCorreoRegLicsu(ref sErr, asunto, urlTemplate, destino, NombreHorario, NombreNormas);
                asunto = "Registro de Nuevo Usuario";
                string mensaje = "Se ha registrado el usuario: " + sNombre + " " + sApellido + ", Correo: " + sCorreo + " Cedula: " + sCedula;
                Utilidades.EnviarCorreoAviso(ref sErr, asunto, mensaje);
                Response.Redirect("ConsultarUsuarios.aspx");
            }
        }
        else
        {
            MostrarMsjModal("El número de Cédula " + txtCedulaFit.Text + " ya está registrado en el sistema.", "ERR");
        }
    }

    protected void btnCancelFit_Click(object sender, EventArgs e)
    {
        Response.Redirect("ConsultarUsuarios.aspx");
    }

    protected void asignarPlan(int UsuarioID)
    {
        string ClienteID = dplUnidad.SelectedValue;
        int cant = 0, iRes = 0;
        int ultimo = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
        string ClienteNombre = Utilidades.EjeSQL("SELECT ClienteNombre FROM Cliente WHERE ClienteID =" + ClienteID, cn, ref sErr, true);
        string PlanID = "";
        if (ClienteID == "2") PlanID = "7";
        else PlanID = "6";

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
        Utilidades.maxRegistro(ref maxPlanAlumno, sSelectSQL, cn, ref sErr);
        string CantidadClases = "0", cc2 = "", cc3 = "";
        sSelectSQL = "Select clasesRegulares as MAXIMO from [Plan] Where PlanID = " + PlanID;
        Utilidades.maxRegistro(ref cc2, sSelectSQL, cn, ref sErr);
        sSelectSQL = "Select clasesComplemen  from [Plan] Where PlanID = " + PlanID;
        cc3 = Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, true);
        cn.Close();
        string saldoNegativo = "";
        sSelectSQL = "SELECT PlanCosto as MAXIMO FROM [Plan] Where PlanID = " + PlanID;
        Utilidades.maxRegistro(ref saldoNegativo, sSelectSQL, cn, ref sErr);
        double saldoN = double.Parse(saldoNegativo);
        if (maxPlanAlumno == "") { PlanAlumnoID = 1; }
        else
        {
            PlanAlumnoID = int.Parse(maxPlanAlumno.Trim()) + 1;
        }
        string SucursalID = "1";
        int iRes3 = 0;

        iRes = insertarAlumno(PlanAlumnoID, UsuarioID, SucursalID, PlanID, FechaInicio, FechaFin, ClienteID, saldoN, cant, CantidadClases);
        if (iRes > 0)
        {
            //Insertar en PlanAlumnoAcumulado
            iRes3 = insertarPlanAcumulado(cc2, UsuarioID, cc3);
            if (iRes3 == 0)
            {
                MostrarMsjModal("No se pudo realizar la asignación: " + sErr, "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Error al Insertar el PlanAlumno: " + sErr, "ERR");
        }
    }

    protected int insertarAlumno(int PlanAlumnoID, int UsuarioID, string SucursalID, string PlanID, string FechaInicio,
        string FechaFin, string ClienteID, double saldoN, int cant, string CantidadClases)
    {
        string sSelectSQL2 = "INSERT INTO PlanAlumno" +
                            "(PlanAlumnoID, UsuarioID, SucursalID, PlanID, PlanAlumnoFechaInicio, PlanAlumnoFechaFin " +
                            ",PlanAlumnoFechaRegistro" +
                            ",PlanAlumnoUsuarioRegistro,ClienteID, SaldoPositivo, SaldoNegativo, NumeroFactura, ClasesActivas, FacturaNota)" +
                            "VALUES(" + PlanAlumnoID + "," + UsuarioID + ", " + Utilidades.SiEsNulo(SucursalID, "N") + ", " + Utilidades.SiEsNulo(PlanID, "N") + ",CONVERT(VARCHAR(24),'" + FechaInicio + "',103)" +
                            ",CONVERT(VARCHAR(24),'" + FechaFin + "',103),SYSDATETIME(), 1, " + Utilidades.SiEsNulo(ClienteID, "N") +
                            "," + saldoN + ", 0, " + Utilidades.SiEsNulo(ClienteID + "_" + cant, "T") +
                            ", " + Utilidades.SiEsNulo(CantidadClases, "T") + "," + Utilidades.SiEsNulo("", "T") + ")";
        Utilidades.EjeSQL(sSelectSQL2, cn, ref sErr, false);
        if (sErr == "")
            return 1;
        return 0;
    }

    protected int insertarPlanAcumulado(string cc2, int UsuarioID, string cc3)
    {
        string selecciono = "0";
        if (dplUnidad.SelectedValue == "1")
            selecciono = "1";

        sSelectSQL = "INSERT INTO PlanAlumnoAcumulado (totalClasesRegulares, disponiblesClasesRegulares, UsuarioID, seleccionoClases, totalClasesComplemen, disponiblesClasesComplemen)" +
                     " VALUES (" + Utilidades.SiEsNulo(cc2, "N") + ", " + Utilidades.SiEsNulo(cc2, "T") + ", " + UsuarioID + "," + selecciono + "," + Utilidades.SiEsNulo(cc3, "N") + ", " + Utilidades.SiEsNulo(cc3, "T") + ")";
        Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
        if (sErr == "")
            return 1;
        return 0;
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