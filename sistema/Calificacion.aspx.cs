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

public partial class sistema_Calificacion : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    DataTable dt;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        this.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            string sSelectSQL = "SELECT TipoCalificacionID AS VAL, TipoCalificacionDescripcion AS TXT" +
                               " FROM TipCalificacion WHERE TipoCalificacionActivo = 1 ORDER BY VAL";
            Utilidades.CargarListado(ref dplTpoCal, sSelectSQL, cn, ref Err, true);
            Utilidades.CargarListado(ref ddlTipoEdit, sSelectSQL, cn, ref Err, true);
        }
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT DISTINCT(c.CalificacionID) as CalificacionID,c.CalificacionNombre as CalificacionNombre, " +
                         " c.TipoCalificacionID as TipoCalificacionID,  " +
                         " (SELECT tc.TipoCalificacionDescripcion FROM TipCalificacion tc WHERE tc.TipoCalificacionID = c.TipoCalificacionID) as TipoCalificacion " +
                         " FROM Calificacion c " + ViewState["sWhere"] + " ORDER BY c.TipoCalificacionID";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "CalificacionID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddModalScript", sb.ToString(), false);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " WHERE c.CalificacionNombre LIKE '%" + sBuscar + "%'"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            MostrarMsjModal("Error: " + ex.Message, "ERR");
        }
    }
    private void limpiar()
    {
        txtNombreAdd.Text = "";
        dplTpoCal.SelectedValue = "";
        ddlTipoEdit.SelectedValue = "";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected bool sonValidos(string Accion)
    {
        bool sRes = true;
        if (Accion == "INSERT")
        {
            string Nombre = txtNombreAdd.Text;
            if (Nombre == "") { Err = "El Campo Nombre es Obligatorio"; sRes = false; }
        }
        else
        {
            string Nombre = txtNombreEdit.Text;
            if (Nombre == "") { Err = "El Campo Nombre es Obligatorio"; sRes = false; }
        }

        return sRes;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName.Equals("deleteRecord"))
        {
            //Eliminar Registro
            GridViewRow gvrow = GridView1.Rows[index];
            hCalificacionDel.Value = (gvrow.FindControl("IDCalificacion_1") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("editRecord"))
        {
            //Modificar Registro
            GridViewRow gvrow = GridView1.Rows[index];
            hCalificacionMod.Value = (gvrow.FindControl("IDCalificacion_1") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("Nombre") as Label).Text;
            ddlTipoEdit.SelectedValue = (gvrow.FindControl("TipoID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Buscar el maximo de Clases..
        string CalificacionNombre = txtNombreEdit.Text;
        string CalificacionTipo = ddlTipoEdit.SelectedValue;
        string CalificacionID = hCalificacionMod.Value;
        int iRes = 0;
        if (sonValidos("UPDATE"))
        {
            cn.Open();
            string sSelectSQL = "UPDATE Calificacion SET CalificacionNombre =" + Utilidades.SiEsNulo(CalificacionNombre, "T") + ", TipoCalificacionID=" + ddlTipoEdit.SelectedValue+ " WHERE CalificacionID=" + CalificacionID;
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try { iRes = addCmd.ExecuteNonQuery(); }
            catch (SqlException sq)
            {
                MostrarMsjModal("Error: " + sq.Message, "ERR");
            }
            cn.Close();
            if (iRes > 0)
            {
                BindGridView();
                MostrarMsjModal("Registro Modificado exitosamente", "EXI");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("document.getElementById('closeEdit').click();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }
        }
        else
        {
            MostrarMsjModal(Err, "ERR");
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string CalificacionID = hCalificacionDel.Value;
        cn.Open();
        string SQL_1 = "DELETE FROM Calificacion Where CalificacionID = " + CalificacionID;
        SqlCommand addCmd = new SqlCommand(SQL_1, cn);

        int iRes = addCmd.ExecuteNonQuery();
        cn.Close();
        if (iRes > 0)
        {
            BindGridView();
            MostrarMsjModal("Registro eliminado de la base de datos", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeDelete').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string CalificacionNombre = txtNombreAdd.Text;
        string TipoCalificacionID = dplTpoCal.SelectedValue;
        cn.Close();
        int iRes = 0;
        if (sonValidos("INSERT"))
        {
            cn.Open();
            string sSelectSQL = "INSERT INTO Calificacion (CalificacionNombre,CalificacionFechaRegistro,CalificacionUsuarioRegistro, TipoCalificacionID) " +
                          " VALUES ( " + Utilidades.SiEsNulo(CalificacionNombre, "T") + ", SYSDATETIME(),1," + TipoCalificacionID + ")";
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try
            {
                iRes = addCmd.ExecuteNonQuery();
                cn.Close();
                if (iRes > 0)
                {
                    BindGridView();
                    limpiar();
                    MostrarMsjModal("Registro Agregado exitosamente", "EXI");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("document.getElementById('closeAdd').click();");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
                }
            }
            catch (SqlException sq)
            {
                MostrarMsjModal("Error al insertar el regstro: " + sq.Message, "ERR");
            }
        }
        else
        {
            MostrarMsjModal(Err, "ERR");
        }

    }
    private void Page_PreRenderComplete(object sender, EventArgs e)
    {

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

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT c.CalificacionNombre as CalificacionNombre, " +
                     " (SELECT tc.TipoCalificacionDescripcion FROM TipCalificacion tc WHERE tc.TipoCalificacionID = c.TipoCalificacionID) as TipoCalificacion " +
                     " FROM Calificacion c " + ViewState["sWhere"] + " ORDER BY c.TipoCalificacionID";
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "CalificacionNombre";
        grid.DataKeyNames = TablaID;
        grid.DataSource = dt;
        grid.DataBind();
        cn.Close();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.IO.StringWriter sw = new System.IO.StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page page = new Page();
        System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();
        GridView Grid = new GridView();
        grid.AllowPaging = false;
        grid.DataBind();
        grid.EnableViewState = false;
        page.EnableEventValidation = false;
        page.DesignerInitialize();
        page.Controls.Add(form);
        form.Controls.Add(grid);
        page.RenderControl(htw);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=calificacion_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}