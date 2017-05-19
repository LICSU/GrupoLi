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

public partial class sistema_Perfil : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    GridView grid;
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string sSelectSQL = "";
    string foto = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!fIdentity.IsAuthenticated)
        {
            Response.Redirect("default.aspx");
        }
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            if (_autenticado.RolID == "2")
            {
                phProfesor.Visible = true;
            }
            else
            {
                phProfesor.Visible = false;
            }
            CargarDatosUsuario();
        }
        if (Request.QueryString["exi"] == "1")
        {
            MostrarMsjModal("Datos Actualizados con Éxito", "EXI");
        }
    }
    //
    private void CargarDatosUsuario()
    {
        sSelectSQL = "SELECT CONVERT(VARCHAR(11),UsuarioFechaNacimiento,103) as FN, * FROM Usuario WHERE UsuarioID=" + _autenticado.UsuarioID;
        SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = cn;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        try
        {
            while (dr.Read())
            {
                txtNombre.Text = dr["UsuarioNombre"].ToString();
                txtApellido.Text = dr["UsuarioApellido"].ToString();
                txtCedula.Text = dr["UsuarioCedula"].ToString();
                txtCedula.Enabled = false;
                txtEmail.Text = dr["UsuarioCorreo"].ToString();
                txtFechaNacimiento.Text = dr["FN"].ToString();
                ddlEstadoCivil.SelectedValue = dr["UsuarioEstadoCivil"].ToString();
                if (dr["UsuarioSexo"].ToString() == "F")
                    rdF.Checked = true;
                else
                    rdM.Checked = true;
                txtTelefono.Text = dr["UsuarioTelefono"].ToString();
                txtCelular.Text = dr["UsuarioCelular1"].ToString();
                txtCelular2.Text = dr["UsuarioCelular2"].ToString();
                txtClaveNueva.Attributes.Add("value", dr["UsuarioClave"].ToString());
                txtClaveRepetir.Attributes.Add("value", dr["UsuarioClave"].ToString());
                txtObservacion.Text = dr["UsuarioObservacion"].ToString();
                txtEPS.Text = dr["UsuarioEPS"].ToString();
                txtRiesgos.Text = dr["UsuarioRiesgos"].ToString();
                txtPension.Text = dr["UsuarioPension"].ToString();
                foto = dr["UsuarioFoto"].ToString();
            }
            //foto
            string ruta = "~/sistema/fotos/";
            if (foto.Length == 0)
            {
                string sexo = Utilidades.EjeSQL("SELECT UsuarioSexo FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
                if (sexo == "M")
                    ruta = ruta + "hombre.jpg";
                else
                    ruta = ruta + "mujer.jpg";
                imgPerfil.ImageUrl = ruta;
            }
            else
            {
                imgPerfil.ImageUrl = foto;
            }
        }
        catch (Exception ex)
        {
            MostrarMsjModal("Error al obtener los datos del usuario. Detalle" + ex, "ERR");
        }
        finally
        {
            dr.Close();
            cn.Close();
        }
    }
    //
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            string sConn = System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString;
            SqlConnection cn = new SqlConnection(sConn);
            string UsuarioNombre = txtNombre.Text;
            string UsuarioApellido = txtApellido.Text;
            string UsuarioCorreo = txtEmail.Text;
            string UsuarioFechaNacimiento = txtFechaNacimiento.Text;
            string UsuarioTelefono = txtTelefono.Text;
            string UsuarioCelular1 = txtCelular.Text;
            string UsuarioCelular2 = txtCelular2.Text;
            string UsuarioRiesgo = txtRiesgos.Text;
            string UsuarioPension = txtPension.Text;
            string UsuarioClave = "";
            if (chkCambioClave.Checked)
            {
                UsuarioClave = txtClaveNueva.Text;
            }
            else
            {
                UsuarioClave = Utilidades.EjeSQL("SELECT UsuarioClave FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, cn, ref Err, true);
            }

            string UsuarioEdoCivil = ddlEstadoCivil.SelectedValue;
            string UsuarioSexo = "";
            string FotoPerfil = "";
            if (rdM.Checked == true) UsuarioSexo = "M";
            else UsuarioSexo = "F";
            string UsuarioEPS = txtEPS.Text;
            string UsuarioObservacion = txtObservacion.Text;
            string ruta = "~/sistema/fotos/";
            if (fuPerfil.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(fuPerfil.FileName);
                if (fileExt == ".jpg" || fileExt == ".JPG" || fileExt == ".png" || fileExt == ".PNG")
                {
                    string nombreArchivo = txtCedula.Text + fileExt;
                    string strRuta = ruta + nombreArchivo;
                    fuPerfil.SaveAs(HttpContext.Current.Server.MapPath(strRuta));
                    FotoPerfil = strRuta;
                }
            }

            string sSelect = "UPDATE Usuario SET UsuarioNombre = '" + UsuarioNombre + "'," +
                            " UsuarioApellido = '" + UsuarioApellido + "'," +
                            " UsuarioCorreo = '" + UsuarioCorreo + "'," +
                            " UsuarioFechaNacimiento = CONVERT(DATE, '" + UsuarioFechaNacimiento + "', 103)," +
                            " UsuarioTelefono = '" + UsuarioTelefono + "'," +
                            " UsuarioCelular1 = '" + UsuarioCelular1 + "'," +
                            " UsuarioCelular2 = '" + UsuarioCelular2 + "'," +
                            " UsuarioPension = '" + UsuarioPension + "'," +
                            " UsuarioRiesgos = '" + UsuarioRiesgo + "'," +
                            " UsuarioClave = '" + UsuarioClave + "'," +
                            " UsuarioEstadoCivil = '" + UsuarioEdoCivil + "'," +
                            " UsuarioSexo = '" + UsuarioSexo + "'," +
                            " UsuarioEPS = '" + UsuarioEPS + "'," +
                            " UsuarioObservacion = '" + UsuarioObservacion + "'," +
                            " UsuarioFoto = '" + FotoPerfil + "'" +
                            " WHERE UsuarioID = " + _autenticado.UsuarioID;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sSelect, cn);
                int iRes = cmd.ExecuteNonQuery();
                if (iRes > 0)
                    Response.Redirect("Perfil.aspx?exi=1");
                cn.Close();

            }
            catch (SqlException sq)
            {
                MostrarMsjModal("Error: " + sq.Message, "ERR");
                cn.Close();
            }
        }
        catch (Exception ex)
        {
            MostrarMsjModal("Error tratanto de actualizar datos del usuario " + ex.Message, "ERR");
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

    protected void lkAtras_Click(object sender, EventArgs e)
    {
        //Buscar Regresar a la Ultima Pagina
    }
    protected void chkCambioClave_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCambioClave.Checked)
        {
            txtClaveNueva.Enabled = true;
            txtClaveRepetir.Enabled = true;
        }
        else
        {
            txtClaveNueva.Enabled = false;
            txtClaveRepetir.Enabled = false;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        
    }
}