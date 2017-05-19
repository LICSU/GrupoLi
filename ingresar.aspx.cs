using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ingresar : System.Web.UI.Page
{
    //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["conexion"].ToString());
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string sErr = "";
    HttpCookie ckUsuario, ckPass, ckCheck; 
    

    protected void Page_Load(object sender, EventArgs e)
    {
        ckUsuario = new HttpCookie("Usuario");
        ckPass = new HttpCookie("Pass");
        if (Request.QueryString["rec"] == "1")
        {
            MostrarMsjModal("Su recuperacion de contraseña ha sido exitosa, favor revisar su mail.", "EXI");
        }
        if (Request.QueryString["exito"] == "1")
        {
            MostrarMsjModal("Su registro ha sido exitoso, favor revisar su mail con los pasos a seguir, si no llega revisar su carpeta de spam o escribirnos.", "EXI");
        }
        if (!IsPostBack)
        {
            if (Request.Cookies["Usuario"] != null)
            {
                Usuario.Text = Request.Cookies["Usuario"].Value;
                Clave.Attributes.Add("Value", Request.Cookies["Pass"].Value);
                chkRecordar.Checked = true;
            }
        }
    }

    protected void bAceptar_Click(object sender, EventArgs e)
    {
        string vSql = "";
        string CodigoUsuario = Usuario.Text.Trim();
        string ClaveEncriptada = Clave.Text.Trim();
        if (CodigoUsuario != "" && ClaveEncriptada != "")
        {
            vSql = "SELECT CAST(UsuarioActivo as VARCHAR(1))+'|'+CAST(U.UsuarioID as VARCHAR(20))+'|'+UsuarioNombre+' '+UsuarioApellido+'|'+CAST(SucursalID as VARCHAR(12))+'|'+CAST(ISNULL(ClienteID,'') as VARCHAR(20))+'|'+CAST(RolID as VARCHAR(4)) FROM dbo.Usuario U INNER JOIN dbo.UsuarioRol UR ON U.UsuarioID=UR.UsuarioID WHERE UsuarioCedula='" + CodigoUsuario + "' AND UsuarioClave='" + ClaveEncriptada + "'";
            lblValidado.ForeColor = System.Drawing.Color.Red;
            string ResUsuario = Utilidades.EjeSQL(vSql, cn, ref sErr);
            if (sErr == string.Empty && ResUsuario != "-1")
            {
                if (ResUsuario != string.Empty)
                {
                    string[] aUsuario = ResUsuario.Split('|');
                    if (aUsuario.Length == 6)
                    {
                        if (aUsuario[0] == "1") //Usuario Activo
                        {
                            string sUsuarioID = aUsuario[1];
                            string sPlanID = "";
                            sPlanID = Utilidades.EjeSQL("SELECT TOP 1 PlanID FROM PlanAlumno WHERE UsuarioID=" + sUsuarioID, cn, ref sErr);
                            //0: UsuarioID  1:Nombre  2:SucursalID  3:ClienteID 4:PlanID
                            FormsAuthenticationTicket Tck = new FormsAuthenticationTicket(1, sUsuarioID + "|" + aUsuario[2] + "|" + aUsuario[3] + "|" + aUsuario[4] + "|" + sPlanID, DateTime.Now, new DateTime(2200, 12, 25), false, aUsuario[5], FormsAuthentication.FormsCookiePath);
                            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(Tck)));
                            //Guardar datos del usuario...
                            if (chkRecordar.Checked)
                            {
                                ckUsuario = new HttpCookie("Usuario", Usuario.Text.Trim());
                                ckPass = new HttpCookie("Pass", Clave.Text.Trim());
                                ckCheck = new HttpCookie("Check", "1");
                                ckUsuario.Expires = new DateTime(2200, 12, 25);
                                ckPass.Expires = new DateTime(2200, 12, 25);
                                ckCheck.Expires = new DateTime(2200, 12, 25);
                                Response.Cookies.Add(ckUsuario);
                                Response.Cookies.Add(ckPass);
                                Response.Cookies.Add(ckCheck);
                            }
                            else
                            {
                                ckUsuario.Expires = DateTime.Now;
                                Response.Cookies.Set(ckUsuario);
                                ckPass.Expires = DateTime.Now;
                                Response.Cookies.Set(ckPass);
                            }
                            string Error = string.Empty;
                            string sSQL = "SELECT RolID FROM UsuarioRol WHERE UsuarioID = "+sUsuarioID;
                            string rol = Utilidades.EjeSQL(sSQL, cn, ref Error, true);
                            if(rol == "2")
                                Response.Redirect("~/sistema/InicioProfesor.aspx");
                            else
                                Response.Redirect("~/sistema/inicio.aspx");
                        }
                        else
                        {
                            sErr = "Usuario no activo, consulte al Administrador";
                            MostrarMsjModal(sErr, "ERR");
                        }
                    }
                    else
                    {
                        sErr = "Error en la sentencia de búsqueda a la base de datos";
                        MostrarMsjModal(sErr, "ERR");
                    }
                }
                else
                {
                    sErr = "Usuario no existe o clave inválida";
                    MostrarMsjModal(sErr, "ERR");
                }
            }
        }
        else
        {
            MostrarMsjModal("Ingrese los datos solicitados", "ERR");
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
    protected void lbRecuperarClave_Click(object sender, EventArgs e)
    {
        
    }
}