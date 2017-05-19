using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class recuperarClave : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string err = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {        

    }
    protected void btnRecuperar_Click(object sender, EventArgs e)
    {
        string cedula = Cedula.Text;
        string email = Email.Text;

        if (Utilidades.EmailValido(email))
        {
            string query = "SELECT UsuarioClave FROM Usuario WHERE UsuarioCedula = '"+cedula+"' AND UsuarioCorreo = '"+email+"'";
            string clave = Utilidades.EjeSQL(query, cn, ref err, true);
            if (clave == "-1" || clave =="")
            {
                MostrarMsjModal("La combinacion Usuario - Email no se reconoce", "ERR");
            }
            else
            {
                string mensaje = "Recibimos una solicitud de recuperacion de contraseña al sistema de Grupo Li <br>. Su contraseña actual es: "+clave;
                Utilidades.EnviarCorreo(email, "Recuperacion de Contraseña", mensaje, ref err);
                Response.Redirect("ingresar.aspx?rec=1");
            }
        }
        else
        {
            MostrarMsjModal("Direccion de correo no valida", "ERR");
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
}