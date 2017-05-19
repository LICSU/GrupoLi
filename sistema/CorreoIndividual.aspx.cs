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
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;
using System.Net;
using System.Net.Security;

public partial class sistema_CorreoIndividual : System.Web.UI.Page
{
    string Err = "", sSelectSQL = "";
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Mostrar Ventana Individual                
        }
    }

    protected void btnEnvioInd_Click(object sender, EventArgs e)
    {
        txtAsunto.Text = "";
        txtDestinatario.Text = "";
        txtMensaje.Text = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddModalScript", sb.ToString(), false);
    }

    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        if (txtAsunto.Text != "" || txtDestinatario.Text != "" || txtMensaje.Text != "")
        {
            if (IsValidEmail(txtDestinatario.Text))
            {
                Utilidades.EnviarCorreo(ref Err, txtAsunto.Text, txtMensaje.Text, txtDestinatario.Text, fipAdjuntos);
                if (Err != "")
                    MostrarMsjModal("Error: " + Err, "ERR");
                else
                {
                    MostrarMsjModal("Correo enviado con Éxito", "EXI");
                    txtAsunto.Text = "";
                    txtMensaje.Text = "";
                    txtDestinatario.Text = "";
                }
            }
            else
                MostrarMsjModal("Dirección de Correo no válida", "ERR");
        }
        else
        {
            MostrarMsjModal("Debe ingresar todos los campos", "ERR");
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

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    protected void BuscarUsr_Click(object sender, EventArgs e)
    {
        string datoConsulta = txtDestinatario.Text;
        BindGridView2(datoConsulta);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#bscModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView2.Rows[index];
        if (e.CommandName.Equals("selectRecord"))
        {
            txtDestinatario.Text = (gvrow.FindControl("UsuarioCorreo") as Label).Text;
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('bscModal').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void BindGridView2(string dato)
    {
        string condicion = "";
        if (dato != null)
        {
            //condicion = " AND (ur.ClienteID = (Select c.ClienteID as Cliente FROM Cliente c Where c.ClienteNombre LIKE '%"+dato+"%') OR us.UsuarioCedula LIKE '%" + dato + "%' OR us.UsuarioNombre LIKE '%" + dato + "%')";
            condicion = " AND (us.UsuarioCedula LIKE '%" + dato + "%' OR us.UsuarioNombre LIKE '%" + dato + "%')";
        }
        try
        {
            cn.Open();
            string cmd2 = "SELECT DISTINCT(us.UsuarioCedula) as UsuarioCedula, us.UsuarioID as UsuarioID," +
                            "us.UsuarioNombre as UsuarioNombre, us.UsuarioApellido as UsuarioApellido,us.UsuarioCorreo as UsuarioCorreo, " +
                            " ur.SucursalID as SucursalID, " +
                            "(SELECT RolDescripcion FROM Rol WHERE RolId = 3) as RolDescripcion, (" +
                            "Select c.ClienteID as Cliente FROM Cliente c Where c.ClienteID=ur.ClienteID) as ClienteID, (" +
                            "Select c.ClienteNombre as ClienteN FROM Cliente c Where c.ClienteID=ur.ClienteID) as ClienteNombre " +
                            "FROM Usuario us, Rol r, UsuarioRol ur " +
                            "WHERE us.UsuarioID = ur.UsuarioID AND ur.ClienteID = 1 AND ur.RolID = 3  " + condicion;
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
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }
}