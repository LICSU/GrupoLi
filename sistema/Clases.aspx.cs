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

public partial class sistema_Clases : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    bool bandOrden;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            string sSelectSQL = "SELECT DISTINCT(tc.TipoCalificacionID) AS VAL, tc.TipoCalificacionDescripcion AS TXT FROM TipCalificacion tc, Calificacion c  WHERE tc.TipoCalificacionActivo = 1 " +
                                " AND tc.TipoCalificacionID = c.TipoCalificacionID order by tc.TipoCalificacionID";
            Utilidades.CargarListado(ref dplTipoCalificacion, sSelectSQL, cn, ref Err, true);
        }
        BindGridView();
    }
    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT (SELECT TipoCalificacionDescripcion FROM TipCalificacion n WHERE n.TipoCalificacionID = c.TipoCalificacionID) as TipoCalificacion," +
                          " c.TipoCalificacionID as TipoCalificacionID, c.ClaseID as ClaseID, c.ClaseDescripcion as ClaseDescripcion, c.ClaseActiva, " +
                          " c.ClaseTipo as Tipo, c.ClaseUnidad as Unidad,c.TiempoCambio as TiempoCambio, c.ClaseIntervalo as Intervalo," +
                          " c.ClaseSensor as Sensor, c.Estacion as Estacion FROM Clase c " + ViewState["sWhere"];
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[2];
            TablaID[0] = "ClaseID";
            TablaID[1] = "ClaseDescripcion";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            //Attribute to show the Plus Minus Button.
            GridView1.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.                
            GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " WHERE c.ClaseDescripcion LIKE '%" + sBuscar + "%'"; }
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
            hClaseDel.Value = (gvrow.FindControl("IDClase_1") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("editRecord"))
        {
            //Modificar Registro                
            string sSelectSQL = "SELECT TipoCalificacionID AS VAL, TipoCalificacionDescripcion AS TXT FROM TipCalificacion ORDER BY VAL";
            Utilidades.CargarListado(ref dplTipoCalificacionEdit, sSelectSQL, cn, ref Err, true);
            GridViewRow gvrow = GridView1.Rows[index];
            hClaseMod.Value = (gvrow.FindControl("IDClase_1") as Label).Text;
            string Filas = "", Columnas = "";
            Filas = Utilidades.EjeSQL("SELECT ClaseFilas FROM Clase WHERE ClaseID = " + hClaseMod.Value, cn, ref Err, true);
            if (Filas != "")
            {
                Columnas = Utilidades.EjeSQL("SELECT ClaseColumnas FROM Clase WHERE ClaseID = " + hClaseMod.Value, cn, ref Err, true);
                plhOrdenEdit.Visible = true;
                bandOrden = true;
            }
            else
            {
                plhOrdenEdit.Visible = false;
                bandOrden = false;
            }
            txtColumnasEdit.Text = Columnas;
            txtFilasEdit.Text = Filas;
            txtNombreEdit.Text = (gvrow.FindControl("Nombre") as Label).Text;
            dplTipoEdit.SelectedValue = (gvrow.FindControl("Tipo") as Label).Text;
            dplUnidadEdit.SelectedValue = (gvrow.FindControl("Unidad") as Label).Text;
            dplIntervaloEdit.SelectedValue = (gvrow.FindControl("Intervalo") as Label).Text;
            dplSensoresEdit.SelectedValue = (gvrow.FindControl("Sensor") as Label).Text;
            string[] Estaciones = ((gvrow.FindControl("Estacion") as Label).Text).Split('|');
            foreach (ListItem li in ddlEstacionesEdit.Items)
            {
                for (int i = 0; i < Estaciones.Length; i++)
                {
                    if (li.Text == Estaciones[i])
                    {
                        li.Selected = true;
                    }
                }
            }

            txtBreakEdit.Text = (gvrow.FindControl("Break") as Label).Text;
            dplTipoCalificacionEdit.SelectedValue = (gvrow.FindControl("TipoCalificacionID") as Label).Text;
            chkActivaEdit.Checked = (gvrow.FindControl("Activa") as CheckBox).Checked;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Buscar el maximo de Clases..
        string ClaseFilas = "", ClaseColumnas = "";
        if (plhOrdenEdit.Visible == true)
        {
            ClaseFilas = txtFilasEdit.Text;
            ClaseColumnas = txtColumnasEdit.Text;
        }
        string ClaseDescripcion = txtNombreEdit.Text;
        bool ClaseActiva = chkActivaEdit.Checked;
        string ClaseID = hClaseMod.Value;
        string TipoCalificacionID = dplTipoCalificacionEdit.SelectedValue;
        string UnidadID = dplUnidadEdit.SelectedValue;
        string TipoID = dplTipoEdit.SelectedValue;
        string Intervalo = dplIntervaloEdit.SelectedValue;
        string Sensor = dplSensoresEdit.SelectedValue;
        string Estacion = string.Empty;
        string Break = txtBreakEdit.Text;
        int iRes = 0;
        string claseActiva = "0";
        if (ClaseActiva) claseActiva = "1";
        if (sonValidos("UPDATE"))
        {
            cn.Open();
            string sSelectSQL = "";

            if (dplIntervaloEdit.SelectedValue != "")
            {
                foreach (ListItem li in ddlEstacionesEdit.Items)
                {
                    if (li.Selected == true)
                    {
                        //Concatenamos con una coma para separar.
                        Estacion += li.Text + ",";
                    }
                }
                Estacion = Estacion.TrimEnd(',');
                ViewState["Medical"] = ", ClaseIntervalo = " + Intervalo +
                                       ", ClaseSensor = " + Sensor + ", TiempoCambio = " + Break + " , Estacion = '" + Estacion + "'";
            }
            if (plhOrdenEdit.Visible == true)
            {
                sSelectSQL = " UPDATE Clase SET ClaseDescripcion =" + Utilidades.SiEsNulo(ClaseDescripcion, "T") + ", " +
                            " ClaseUnidad = " + Utilidades.SiEsNulo(UnidadID, "N") + ", TipoCalificacionID = " + Utilidades.SiEsNulo(TipoCalificacionID, "N") + ", " +
                            " ClaseTipo = " + Utilidades.SiEsNulo(TipoID, "T") + " ,ClaseActiva='" + claseActiva +
                            "' ,ClaseFilas = " + ClaseFilas + ", ClaseColumnas = " + ClaseColumnas + ViewState["Medical"] + " WHERE ClaseID = " + ClaseID;
            }
            else
            {
                sSelectSQL = "UPDATE Clase SET ClaseDescripcion =" + Utilidades.SiEsNulo(ClaseDescripcion, "T") + ", " +
                            " ClaseUnidad = " + Utilidades.SiEsNulo(UnidadID, "N") + ", TipoCalificacionID = " + Utilidades.SiEsNulo(TipoCalificacionID, "N") + ", " +
                            " ClaseTipo = " + Utilidades.SiEsNulo(TipoID, "T") + " ,ClaseActiva='" + claseActiva +
                            "' " + ViewState["Medical"] + " WHERE ClaseID=" + ClaseID;
            }
            //MostrarMsjModal(sSelectSQL, "");
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try { iRes = addCmd.ExecuteNonQuery(); }
            catch (SqlException sq)
            {
                Err = sq.Message;
                MostrarMsjModal(Err, "ERR");
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
        string ClaseID = hClaseDel.Value;
        cn.Open();
        string SQL_1 = "DELETE FROM Clase Where ClaseID = " + ClaseID;
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
        //Buscar el maximo de Clases..
        cn.Close();
        string ClaseDescripcion = txtNombreAdd.Text;
        bool ClaseActiva = chkActivoAdd.Checked;
        string TipoCalificacionID = dplTipoCalificacion.SelectedValue;
        string TipoID = dplTipo.SelectedValue;
        string UnidadID = dplUnidad.SelectedValue;
        string Intervalo = dplIntervaloAdd.SelectedValue;
        string Sensor = dplSensoresAdd.SelectedValue;
        string Break = txtBreakAdd.Text;
        string Estacion = "";
        string ClaseFilas = "", ClaseColumnas = "";
        foreach (ListItem li in ddlEstaciones.Items)
        {
            if (li.Selected == true)
            {
                //Concatenamos con una coma para separar.
                Estacion += li.Text + "|";
            }
        }
        Estacion = Estacion.TrimEnd('|');
        if (dplOrden.SelectedValue != "")
        {
            ClaseFilas = txtFilasAdd.Text;
            ClaseColumnas = txtColumnasAdd.Text;
        }
        int iRes = 0;
        if (sonValidos("INSERT"))
        {
            cn.Open();
            string sSelectSQL = "INSERT INTO Clase (ClaseDescripcion, ClaseActiva,ClaseFechaRegistro,ClaseUsuarioRegistro, ClaseTipo, ClaseUnidad,TipoCalificacionID,ClaseFilas,ClaseColumnas,ClaseIntervalo,ClaseSensor,TiempoCambio,Estacion) " +
                          " VALUES ( " + Utilidades.SiEsNulo(ClaseDescripcion, "T") + ", '" + ClaseActiva + "',SYSDATETIME()," +
                          " 1," + Utilidades.SiEsNulo(TipoID, "T") + ", " + Utilidades.SiEsNulo(UnidadID, "N") + "," + Utilidades.SiEsNulo(TipoCalificacionID, "N") +
                          ", " + Utilidades.SiEsNulo(ClaseFilas, "T") + "," + Utilidades.SiEsNulo(ClaseColumnas, "T") + ", " + Utilidades.SiEsNulo(Intervalo, "N") +
                          ", " + Utilidades.SiEsNulo(Sensor, "N") + ", " + Utilidades.SiEsNulo(Break, "N") + ", " + Utilidades.SiEsNulo(Estacion, "T") + ")";
            //MostrarMsjModal(sSelectSQL, "");
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try { iRes = addCmd.ExecuteNonQuery(); }
            catch (SqlException sq)
            {
                Err = sq.Message;
                MostrarMsjModal("Error: " + Err, "ERR");
            }
            cn.Close();
            if (iRes > 0)
            {
                limpiarCampos();
                BindGridView();
                MostrarMsjModal("Registro Agregado exitosamente", "EXI");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("document.getElementById('closeAdd').click();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
            }
        }
        else
        {
            MostrarMsjModal(Err, "ERR");
        }


    }
    private void limpiarCampos()
    {
        txtNombreAdd.Text = "";
        dplUnidad.SelectedValue = "";
        dplTipo.SelectedValue = "";
        dplTipoCalificacion.SelectedValue = "";
    }
    private bool sonValidos(string Accion)
    {
        bool bRes = true;
        //Datos Obligatorios Nombre, Nivel
        if (Accion == "INSERT")
        {
            string Nombre = txtNombreAdd.Text;
            string Nivel = dplTipoCalificacion.SelectedValue;
            string Tipo = dplTipo.SelectedValue;
            string Unidad = dplUnidad.SelectedValue;
            if (Nombre == "") { Err += "El campo Nombre es Obligatorio. <br/>"; bRes = false; }
            if (Nivel == "") { Err += "Debe Seleccionar un Nivel. <br/>"; bRes = false; }
            if (Tipo == "") { Err += "Debe Seleccionar un Tipo. <br/>"; bRes = false; }
            if (Unidad == "") { Err += "Debe Seleccionar un Unidad. <br/>"; bRes = false; }
        }
        else
        {
            string Nombre = txtNombreEdit.Text;
            string Nivel = dplTipoCalificacionEdit.SelectedValue;
            string Tipo = dplTipoEdit.SelectedValue;
            string Unidad = dplUnidadEdit.SelectedValue;
            if (Nombre == "") { Err += "El campo Nombre es Obligatorio. <br/>"; bRes = false; }
            if (Nivel == "") { Err += "Debe Seleccionar un Nivel. <br/>"; bRes = false; }
            if (Tipo == "") { Err += "Debe Seleccionar un Tipo. <br/>"; bRes = false; }
            if (Unidad == "") { Err += "Debe Seleccionar un Unidad. <br/>"; bRes = false; }
        }

        return bRes;
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
        string cmd2 = "SELECT (SELECT TipoCalificacionDescripcion FROM TipCalificacion n WHERE n.TipoCalificacionID = c.TipoCalificacionID)" +
                       "as TipoCalificacion," +
                      " c.ClaseDescripcion as ClaseDescripcion, c.ClaseActiva, " +
                      " c.ClaseTipo as Tipo, c.ClaseUnidad as Unidad FROM Clase c " + ViewState["sWhere"];
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "ClaseDescripcion";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=clases_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void dplOrden_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplOrden.SelectedValue != "")
        {
            if (dplOrden.SelectedValue == "Si")
                plhOrdenAdd.Visible = true;
            else
                plhOrdenAdd.Visible = false;
        }
        else
        {
            plhOrdenAdd.Visible = false;
        }
    }
}