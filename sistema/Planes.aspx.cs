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

public partial class sistema_Planes : System.Web.UI.Page
{
    string Err = "";
    DataSet ds;
    GridView grid;
    DataTable dt;
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
            string cmd2 = "SELECT PlanID, PlanNombre, CONVERT(VARCHAR(11),PlanFechaInicio,103) as PlanFechaInicio,  CONVERT(VARCHAR(11),PlanFechaFin, 103) as PlanFechaFin, PlanActivo, " +
                            "clasesComplemen, clasesRegulares, PlanCosto, PlanDescripcion, PlanDias " +
                            "FROM [Plan] " + ViewState["sWhere"];
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "PlanID";
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " AND PlanNombre LIKE '%" + sBuscar + "%'"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
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
        if (e.CommandName.Equals("editRecord"))
        {
            GridViewRow gvrow = GridView1.Rows[index];
            hPlanIDEdit.Value = (gvrow.FindControl("PlanID") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("PlanNombre") as Label).Text;
            txtDescripcionEdit.Text = (gvrow.FindControl("PlanDescripcion") as Label).Text;
            txtFechaIniEdit.Text = (gvrow.FindControl("PlanFechaInicio") as Label).Text;
            txtFechaFinEdit.Text = (gvrow.FindControl("PlanFechaFin") as Label).Text;
            //chkActivoEdit.Checked = (gvrow.FindControl("PlanActivo") as CheckBox).Checked;
            clasesComplemenEdit.Text = (gvrow.FindControl("clasesComplemen") as Label).Text;
            clasesRegularesEdit.Text = (gvrow.FindControl("clasesRegulares") as Label).Text;
            txtCostoEdit.Text = (gvrow.FindControl("PlanCosto") as Label).Text;
            txtDuracionEdit.Text = (gvrow.FindControl("PlanDias") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("deleteRecord"))
        {
            GridViewRow gvrow = GridView1.Rows[index];
            hPlanDel.Value = (gvrow.FindControl("PlanID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("Desactivar"))
        {
            GridViewRow gvrow = GridView1.Rows[index];
            hPlanDes.Value = (gvrow.FindControl("PlanID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#desactivarModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {

        string Nombre = txtNombreAdd.Text;
        string Descripcion = txtDescripcionAdd.Text;
        string FechaInicio = "", FechaFin = "";
        if (txtFechaIniAdd.Text == "")
            FechaInicio = DateTime.Today.Date.ToString();
        else
            FechaInicio = txtFechaIniAdd.Text;
        if (txtFechaFinAdd.Text == "")
            FechaFin = DateTime.Today.Date.ToString();
        else
            FechaFin = txtFechaFinAdd.Text;
        bool Activo = true;
        string ClasesComplemen = clasesComplemenAdd.Text;
        string ClasesRegulares = clasesRegularesAdd.Text;
        string Costo = txtCostoAdd.Text;
        string maxiPlanID = "";
        string sErr = "";
        string sSelectSQL = "SELECT MAX(PlanID) as MAXIMO FROM [Plan]";
        Utilidades.maxRegistro(ref maxiPlanID, sSelectSQL, cn, ref sErr);
        int PlanID = int.Parse(maxiPlanID.Trim()) + 1;
        FechaFin = Utilidades.FecUni(FechaFin);
        FechaInicio = Utilidades.FecUni(FechaInicio);
        int iRes = 0;
        if (sonValidos("INSERT"))
        {
            cn.Open();
            sSelectSQL = "INSERT INTO [Plan] (PlanID, PlanNombre, PlanDescripcion, PlanFechaInicio, PlanFechaFin, " +
                         " PlanUsuarioRegistro, PlanFechaRegistro,PlanActivo, clasesComplemen, PlanCosto, clasesRegulares, PlanDias)" +
                         " VALUES (" + PlanID + ", " + Utilidades.SiEsNulo(Nombre, "T") + ", " + Utilidades.SiEsNulo(Descripcion, "T") + "" +
                         " ,CONVERT(DATETIME, '" + FechaInicio + "', 103), CONVERT(DATETIME, '" + FechaFin + "', 103)" + "" +
                         " ,1,SYSDATETIME(),'" + Activo + "'," + Utilidades.SiEsNulo(ClasesComplemen, "T") + "," + Utilidades.SiEsNulo(Costo, "T") + "," + Utilidades.SiEsNulo(ClasesRegulares, "T") + "," + Utilidades.SiEsNulo(txtDuracionAdd.Text, "N") + ")";
            //MostrarMsjModal(sSelectSQL, "");
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try { iRes = addCmd.ExecuteNonQuery(); }
            catch (SqlException sq) { Err = sq.Message; }
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
            else
            {
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
        bool bRes = true;
        string Nombre = "";
        string ClasesRegulares = "";
        string ClasesComplemen = "";
        string Costo = "";
        if (Accion == "INSERT")
        {
            Nombre = txtNombreAdd.Text;
            ClasesRegulares = clasesComplemenAdd.Text;
            ClasesComplemen = clasesRegularesAdd.Text;
            Costo = txtCostoAdd.Text;
        }
        else
        {
            Nombre = txtNombreEdit.Text;
            ClasesRegulares = clasesComplemenEdit.Text;
            ClasesComplemen = clasesRegularesEdit.Text;
            Costo = txtCostoEdit.Text;
        }

        if (Nombre == "") { Err += "El Campo Nombre es Obligatorio"; bRes = false; }
        if (ClasesComplemen == "") { Err += "El Campo  Cantidad de Clases Complementarias es Obligatorio"; bRes = false; }
        if (ClasesRegulares == "") { Err += "El Campo  Cantidad de Clases Regulares es Obligatorio"; bRes = false; }
        if (Costo == "") { Err += "El Campo Costo es Obligatorio"; bRes = false; }

        return bRes;
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

    protected void Editar_Click(object sender, EventArgs e)
    {
        string Nombre = txtNombreEdit.Text;
        string Descripcion = txtDescripcionEdit.Text;
        string FechaInicio = txtFechaIniEdit.Text;
        string FechaFin = txtFechaFinEdit.Text;
        bool Activo = true;
        string ClasesComplemen = clasesComplemenEdit.Text;
        string ClasesRegulares = clasesRegularesEdit.Text;
        string Costo = txtCostoEdit.Text;
        int PlanID = int.Parse(hPlanIDEdit.Value);
        FechaInicio = Utilidades.FecUni(FechaInicio);
        FechaFin = Utilidades.FecUni(FechaFin);
        int iRes = 0;
        if (sonValidos("UPDATE"))
        {
            cn.Open();
            string sSelectSQL = "UPDATE [Plan] SET PlanNombre =" + Utilidades.SiEsNulo(Nombre, "T") +
                                ", PlanDescripcion=" + Utilidades.SiEsNulo(Descripcion, "T") +
                                ", PlanFechaInicio= CONVERT(DATETIME, '" + FechaInicio + "'" +
                                ", 103), PlanFechaFin= CONVERT(DATETIME, '" + FechaFin + "'" +
                                ", 103), PlanActivo = '" + Activo +
                                "',clasesComplemen = " + Utilidades.SiEsNulo(ClasesComplemen, "T") +
                                ",clasesRegulares = " + Utilidades.SiEsNulo(ClasesRegulares, "T") +
                                ", PlanCosto = " + Utilidades.SiEsNulo(Costo, "T") +
                                ", PlanDias = " + Utilidades.SiEsNulo(txtDuracionEdit.Text, "N") +
                                " WHERE PlanID=" + PlanID;
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try { iRes = addCmd.ExecuteNonQuery(); }
            catch (SqlException sq)
            {
                Err += sq.Message;
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
            }
            else
            {
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
        string PlanID = hPlanDel.Value;
        int iRes = 0;
        cn.Open();
        string SQL_1 = "DELETE FROM [Plan] Where PlanID = " + PlanID;
        SqlCommand addCmd = new SqlCommand(SQL_1, cn);
        try
        {
            iRes = addCmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            Err = sq.Message;
        }
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
        else
        {
            MostrarMsjModal("No se puede eliminar un Plan con alumnos asignados. Puede proceder a desactivarlo.", "ERR");
        }

    }

    protected void btnDesactivar_Click(object sender, EventArgs e)
    {
        string PlanID = hPlanDes.Value;
        int iRes = 0;
        cn.Open();
        string SQL_1 = "UPDATE [Plan] SET PlanActivo = 0 Where PlanID = " + PlanID;
        SqlCommand addCmd = new SqlCommand(SQL_1, cn);
        try
        {
            iRes = addCmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            Err = sq.Message;
        }
        cn.Close();
        if (iRes > 0)
        {
            BindGridView();
            MostrarMsjModal("Registro actualizado con exito", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeDesact').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
        }
        else
        {
            MostrarMsjModal("Error: " + iRes, "ERR");
        }

    }
    public void limpiarCampos()
    {
        txtNombreAdd.Text = "";
        txtDescripcionAdd.Text = "";
        txtFechaFinAdd.Text = "";
        txtFechaFinAdd.Text = "";
        clasesComplemenAdd.Text = "";
        clasesRegularesAdd.Text = "";
        txtCostoAdd.Text = "";
    }
    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT PlanNombre, PlanFechaInicio, PlanFechaFin, PlanActivo, " +
                        "clasesComplemen, clasesRegulares, PlanCosto, PlanDescripcion " +
                        "FROM [Plan] " + ViewState["sWhere"];
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "PlanNombre";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=planes_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}