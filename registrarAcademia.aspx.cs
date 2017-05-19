using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class registrarAcademia : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string sErr = string.Empty;
    string sSelectSQL = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

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

    protected void btnAddFit_Click(object sender, EventArgs e)
    {
        //Registrar usuario fitnessLi   
        if (Page.IsValid)
        {
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
                string embarazada = string.Empty;
                string menor = string.Empty;
                bool band_embarazo = true;
                if (ddlConsentimiento.SelectedValue != "")
                {
                    if (ddlConsentimiento.SelectedValue == "SI")
                    {
                        embarazada = "SI";
                    }
                    else 
                    {
                        band_embarazo = false;
                        MostrarMsjModal("No se puede procesar el registro sin consentimiento medico.", "ERR");
                    }
                }
                string rep_menor = txtNombreRepresentante.Text;
                string ced_menor = txtCedulaRepresentante.Text;
                string tel_menor = txtTelefonoRepresentante.Text;
                int usuarioID = int.Parse((Utilidades.EjeSQL("SELECT MAX(UsuarioID) FROM Usuario", cn, ref sErr, true)).Trim()) + 1;
                int rolID = int.Parse((Utilidades.EjeSQL("SELECT MAX(UsuarioRolID) FROM UsuarioRol", cn, ref sErr, true)).Trim()) + 1;
                if (band_embarazo)
                {
                    sSelectSQL = "INSERT INTO Usuario (UsuarioID,UsuarioCedula,UsuarioClave,UsuarioNombre,UsuarioApellido,"
                               + "UsuarioCorreo,UsuarioFechaNacimiento,UsuarioEstadoCivil,UsuarioSexo,UsuarioFoto,UsuarioTelefono,"
                               + "UsuarioCelular1,UsuarioCelular2,UsuarioEPS,UsuarioObservacion,UsuarioActivo,UsuarioFechaRegistro,"
                               + "UsuarioUsuarioRegistro,UsuarioRiesgos,UsuarioPension,UsuarioTipoEmp,UsuarioPrueba,UsuarioEmbarazada,UsuarioMenor,UsuarioMenorNomRep,UsuarioMenorCedRep,UsuarioMenorTelRep)"
                               + "VALUES(" + usuarioID + "," + Utilidades.SiEsNulo(sCedula, "T") + ",'" + sClave + "','" + sNombre + "','" + sApellido + "','" + sCorreo + "', "+Utilidades.SiEsNulo(sFechaNacimiento, "F")+","
                               + "'" + sEstadoCivil + "','" + sSexo + "','','" + sTelefono + "','" + sCelular + "','" + sCelular2 + "','" + sEPS + "','" + sObservacion + "',"
                               + "1,SYSDATETIME(),1,'" + riesgos + "','" + pension + "','', 0, "
                               + Utilidades.SiEsNulo(embarazada, "T") + ", " + Utilidades.SiEsNulo(menor, "T") + ", "
                               + Utilidades.SiEsNulo(rep_menor, "T") + "," + Utilidades.SiEsNulo(ced_menor, "T") + ", " + Utilidades.SiEsNulo(tel_menor, "T") + ")";
                    Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);

                    if (sErr == "")
                    {
                        sSelectSQL = "INSERT INTO UsuarioRol(UsuarioRolID,UsuarioID,SucursalID,RolID,ClienteID,UsuarioRolFechaRegistro,UsuarioUsuarioRegistro)"
                                    + " VALUES(" + rolID + "," + usuarioID + ",1 , 3 , 1 ,SYSDATETIME(),1)";
                        Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                    }
                    if (sErr == "")
                    {
                        sSelectSQL = " INSERT INTO planAutorizacion(AutorizacionActivo, FechaRegistro, UsuarioID, CorreoEnviado) " +
                                     " VALUES (0, SYSDATETIME(), " + usuarioID + ", 0)";
                        Utilidades.EjeSQL(sSelectSQL, cn, ref sErr, false);
                    }
                    if (sErr == "")
                    {
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
                        }
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
                MostrarMsjModal("El número de Cédula " + txtCedulaFit.Text + " ya está registrado en el sistema.", "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Los campos no cumplen los requisitos.", "ERR");
        }
    }
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
    
    protected void ddlConsentimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlConsentimiento.SelectedValue == "NO")
        {
            
        }
    }
    
}