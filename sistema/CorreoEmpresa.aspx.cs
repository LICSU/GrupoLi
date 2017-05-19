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

public partial class sistema_CorreoEmpresa : System.Web.UI.Page
{
    string Err = "", sSelectSQL = "";
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente ORDER BY TXT";
            Utilidades.CargarListado(ref dplEmpresa, sSelectSQL, cn, ref Err, true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (dplEmpresa.SelectedValue != "")
        {
            if (txtAsunto.Text != "" || txtMensaje.Text != "")
            {
                //Buscar los correos..
                cn.Open();
                sSelectSQL = "SELECT Usuario.UsuarioCorreo as UsuarioCorreo, UsuarioRol.ClienteID" +
                            " FROM Usuario INNER JOIN" +
                            " UsuarioRol ON Usuario.UsuarioID = UsuarioRol.UsuarioID" +
                            " WHERE (UsuarioRol.ClienteID = " + dplEmpresa.SelectedValue + ")";
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                string destinos = "";
                string correo;
                try
                {
                    while (dr.Read())
                    {
                        correo = dr["UsuarioCorreo"].ToString();
                        if (Utilidades.EmailValido(correo))
                            destinos = destinos + correo + ",";
                    }
                    cn.Close();
                    destinos = destinos.TrimEnd(',');
                    Utilidades.EnviarCorreo(ref Err, txtAsunto.Text, txtMensaje.Text, destinos, fipAdjuntos);
                    if (Err != "")
                        MostrarMsjModal("Error: " + Err, "ERR");
                    else
                    {
                        MostrarMsjModal("Correo masivo enviado con Éxito", "EXI");
                        dplEmpresa.SelectedValue = "";
                        txtAsunto.Text = "";
                        txtMensaje.Text = "";
                    }
                }
                catch (SqlException sq)
                {
                    Err = sq.Message;
                }
            }
            else
            {
                MostrarMsjModal("El asunto y el Mensaje no deben quedar en Blanco.", "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Debe seleccionar una Empresa", "ERR");
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