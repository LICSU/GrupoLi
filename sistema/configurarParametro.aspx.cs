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

public partial class sistema_configurarParametro : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "", sSelectSQL = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente ORDER BY TXT";
            Utilidades.CargarListado(ref dplUnidad, sSelectSQL, cn, ref Err, true);
            sSelectSQL = "SELECT idTipoParametro AS VAL, nombreTipoParametro AS TXT FROM TipoParametro ORDER BY TXT";
            Utilidades.CargarListado(ref dplTipoPar, sSelectSQL, cn, ref Err, true);
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Cliente.ClienteNombre as ClienteNombre , "
                           + " Parametros.idParametro as idParametro, "
                           + " Parametros.tipoParametro as tipoParametro, "
                           + " (SELECT nombreTipoParametro FROM TipoParametro "
                           + " WHERE idTipoParametro = tipoParametro) as nombreTipoParametro, "
                           + " Parametros.labelParametro as labelParametro, "
                           + " Parametros.obserParametro as obserParametro, "
                           + " Parametros.activoParametro as activoParametro, "
                           + " Parametros.labelIdParametro as labelIdParametro, "
                           + " Parametros.ClienteID as ClienteID "
                           + " FROM Cliente INNER JOIN Parametros "
                           + " ON Cliente.ClienteID = Parametros.ClienteID";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "idParametro";
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

    protected void Agregar_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Guardamos el Parametro
        int iRes = 0;
        sSelectSQL = "INSERT INTO Parametros"
       + " (tipoParametro,labelParametro,obserParametro,activoParametro,ClienteID,labelIdParametro)"
       + " VALUES (" + dplTipoPar.SelectedValue + ",'" + txtNombrePar.Text + "'"
       + " ,'" + txtObservacionPar.Text + "',1," + dplUnidad.SelectedValue + ",'" + labelIdPar.Text + "')";
        cn.Open();
        SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
        try
        {
            iRes = addCmd.ExecuteNonQuery();
            if (iRes > 0)
            {
                cn.Close();
                /** 
                 * Actualizamos la Tabla Usuario **/
                updateUsuarios(dplTipoPar.SelectedValue, labelIdPar.Text);
                BindGridView();
                MostrarMsjModal("Registro Agregado exitosamente", "EXI");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("document.getElementById('closeAdd').click();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
            }
        }
        catch (SqlException sq) { MostrarMsjModal(sq.Message, "ERR"); cn.Close(); }

    }
    private void updateUsuarios(string tipoParametro, string nombreParametro)
    {
        int iRes = 0;
        if (tipoParametro == "1")
            sSelectSQL = "ALTER TABLE Usuario ADD " + nombreParametro + " VARCHAR(200)";
        else if (tipoParametro == "2")
            sSelectSQL = "ALTER TABLE Usuario ADD " + nombreParametro + " BIT";
        cn.Open();
        SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
        try
        {
            iRes = addCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MostrarMsjModal("Error: " + ex.Message, "ERR");
        }
        cn.Close();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
}