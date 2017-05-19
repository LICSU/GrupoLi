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

public partial class sistema_TipoCalificacion : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT tc.TipoCalificacionID as TipoCalificacionID, tc.TipoCalificacionDescripcion as Descripcion, " +
                          " TipoCalificacionActivo as Activo FROM TipCalificacion tc " + ViewState["sWhere"];
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[2];
            TablaID[0] = "TipoCalificacionID";
            TablaID[1] = "Descripcion";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            //Attribute to show the Plus Minus Button.
            GridView1.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.                
            GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            { ViewState["sWhere"] = " WHERE tc.TipoCalificacionDescripcion LIKE '%" + sBuscar + "%'"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
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
        if (e.CommandName.Equals("deleteRecord"))
        {
            //Eliminar Registro
            GridViewRow gvrow = GridView1.Rows[index];
            hTipoCalificacioID.Value = (gvrow.FindControl("IDTCalificacion_1") as Label).Text;
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
            hTipoCalificacionMod.Value = (gvrow.FindControl("IDTCalificacion_1") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("Descripcion") as Label).Text;
            //chkActivoEdit.Checked = (gvrow.FindControl("Activo") as CheckBox).Checked;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }
    }
    private void limpiar()
    {
        txtNombreAdd.Text = "";
    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string TipoCalificacionDescripcion = txtNombreAdd.Text;
        bool Activo = true;
        int iRes = 0;
        if (sonValidos("INSERT"))
        {
            cn.Open();
            string sSelectSQL = "INSERT INTO TipCalificacion (TipoCalificacionDescripcion, TipoCalificacionActivo, TipoCalificacionFechaRegistro, TipoCalificacionUsuarioRegistro) " +
                          " VALUES (" + Utilidades.SiEsNulo(TipoCalificacionDescripcion, "T") + ", '" + Activo + "',SYSDATETIME(),1)";
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try
            {
                iRes = addCmd.ExecuteNonQuery();
                cn.Close();
                if (iRes > 0)
                {
                    limpiar();
                    BindGridView();
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
                Err = sq.Message;
                MostrarMsjModal(Err, "ERR");
            }

        }
        else
        {
            MostrarMsjModal(Err, "ERR");
        }
    }

    private bool sonValidos(string Accion)
    {
        bool sRes = true;
        if (Accion == "UPDATE")
        {
            string Nombre = txtNombreEdit.Text;
            if (Nombre == "") { Err = "El Campo Nombre debe ser Obligatorio"; sRes = false; }
        }
        else
        {
            string Nombre = txtNombreAdd.Text;
            if (Nombre == "") { Err = "El Campo Nombre debe ser Obligatorio"; sRes = false; }
        }
        return sRes;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string TipoCalificacionDesc = txtNombreEdit.Text;
        bool Activa = true;
        string TipoCalificacionID = hTipoCalificacionMod.Value;
        int iRes = 0;
        if (sonValidos("UPDATE"))
        {
            cn.Open();
            string sSelectSQL = "UPDATE TipCalificacion SET TipoCalificacionDescripcion ='" + TipoCalificacionDesc + "', TipoCalificacionActivo='" + Activa + "' WHERE TipoCalificacionID=" + TipoCalificacionID;
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try
            {
                iRes = addCmd.ExecuteNonQuery();
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
            catch (SqlException sq)
            {
                Err = sq.Message;
                MostrarMsjModal(Err, "ERR");
            }
        }
        else
        {
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int iRes = 0;
        string TipoCalificacioID = hTipoCalificacioID.Value;
        cn.Open();
        string SQL_1 = "DELETE FROM TipCalificacion Where TipoCalificacionID = " + TipoCalificacioID;
        SqlCommand addCmd = new SqlCommand(SQL_1, cn);
        try
        {
            iRes = addCmd.ExecuteNonQuery();
            cn.Close();
            if (iRes > 0)
            {
                BindGridView();
                MostrarMsjModal("Registro eliminado de la base de datos", "ERR");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("document.getElementById('closeDelete').click();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
            }
        }
        catch (SqlException sq)
        {
            MostrarMsjModal("Error al eliminar el registro" + sq.Message, "ERR");
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

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT tc.TipoCalificacionDescripcion as Descripcion, " +
                      " TipoCalificacionActivo as Activo FROM TipCalificacion tc " + ViewState["sWhere"];
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "Descripcion";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=tipo_calificacion_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}