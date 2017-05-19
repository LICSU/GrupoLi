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

public partial class sistema_cargarVideosAdmin : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string sSelectSQL = "";
    string Err = "";
    DataSet ds;
    GridView grid;
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("deleteRecord"))
        {
            //Eliminar Registro
            hdArchivoID.Value = (gvrow.FindControl("VideoID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            sSelectSQL = "SELECT VideoID, UrlVideo, CONVERT(VARCHAR(11), FechaInicioVideo, 103) as FechaInicioVideo, CONVERT(VARCHAR(11), FechaFinalVideo, 103) as FechaFinalVideo, NumeroVideo From Videos ORDER BY NumeroVideo ASC";
            SqlDataAdapter dAdapter = new SqlDataAdapter(sSelectSQL, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "VideoID";
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

    protected void btnSubir_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddModalScript", sb.ToString(), false);
    }

    protected string fechaActual()
    {
        string fecha = "";
        fecha = (DateTime.Today.Date).ToString("dd_MM_yyyy");
        return fecha;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (dplVideos.SelectedValue != "" && txtUrlVideo.Text != "" && txtFechaInicio.Text != "" && txtFechaFin.Text != "")
        {
            //Guardamos..
            string NumeroVideos = dplVideos.SelectedValue;
            string UrlVideo = txtUrlVideo.Text;
            string FechaInicioVideo = txtFechaInicio.Text;
            string FechaFinalVideo = txtFechaFin.Text;
            sSelectSQL = "INSERT INTO Videos (UrlVideo, FechaInicioVideo, FechaFinalVideo, NumeroVideo) VALUES (" +
                " '" + UrlVideo + "', CONVERT(DATE,'" + FechaInicioVideo + "',103), CONVERT(DATE,'" + FechaFinalVideo + "',103), '" + NumeroVideos + "')";
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            BindGridView();
            MostrarMsjModal("Registro Agregado exitosamente", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeEdit').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

        }
        else
        {
            MostrarMsjModal("Todos los campos son Obligatorios", "ERR");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sSelectSQL = "DELETE FROM Videos WHERE VideoID = " + hdArchivoID.Value;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        BindGridView();
        MostrarMsjModal("Registro Eliminado exitosamente", "EXI");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('closeDelete').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
    }
}