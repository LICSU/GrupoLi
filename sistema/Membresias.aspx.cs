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

public partial class sistema_Membresias : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    string[] vecCuppos, vecTam1;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Membresia.MembresiaID as MembresiaID, " +
                         " CONVERT(VARCHAR(11),Membresia.MembresiaFechaInicio,103) as FechaInicio, " +
                         " CONVERT(VARCHAR(11),Membresia.MembresiaFechaFin,103) as FechaFin, " +
                         " Membresia.MembresiaDocumento as MembresiaDocumento," +
                         " Membresia.MembresiaPago as MembresiaPago, " +
                         " Membresia.MembresiaObservacion as Observación, " +
                         " Membresia.MembresiaFechaRegistro as FechaRegistro," +
                         " Usuario.UsuarioCedula as UsuarioCedula," +
                         " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres" +
                         " FROM Membresia INNER JOIN" +
                         " Usuario ON Membresia.MembresiaUsuarioID = Usuario.UsuarioID " + ViewState["sWhere"];
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "MembresiaID";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " WHERE Usuario.UsuarioCedula LIKE '%" + sBuscar + "%'"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected void Add_Click(object sender, ImageClickEventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddModalScript", sb.ToString(), false);
    }

    protected void ImgbtnArchivo_Click(object sender, ImageClickEventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT  " +
                    " Membresia.MembresiaFechaInicio as FechaInicio, " +
                    " Membresia.MembresiaFechaFin as FechaFin, " +
                    " Membresia.MembresiaDocumento as MembresiaDocumento," +
                    " Membresia.MembresiaPago as MembresiaPago, " +
                    " Membresia.MembresiaFechaRegistro as FechaRegistro," +
                    " Usuario.UsuarioCedula as UsuarioCedula," +
                    " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres" +
                    " FROM Membresia INNER JOIN" +
                    " Usuario ON Membresia.MembresiaUsuarioID = Usuario.UsuarioID " + ViewState["sWhere"];
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "UsuarioCedula";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=membresias_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("deleteRecord"))
        {
            hdfMembresiaIDEli.Value = (gvrow.FindControl("MembresiaID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        if (e.CommandName.Equals("editRecord"))
        {
            hdfMembresiaID.Value = (gvrow.FindControl("MembresiaID") as Label).Text;
            txtCedulaEdit.Text = (gvrow.FindControl("UsuarioCedula") as Label).Text;
            txtNombresEdit.Text = (gvrow.FindControl("UsuarioNombres") as Label).Text;
            txtFechaInicioEdit.Text = (gvrow.FindControl("MembresiaFechaInicio") as Label).Text;
            txtFechaFinEdit.Text = (gvrow.FindControl("MembresiaFechaFin") as Label).Text;
            txtDocumentoEdit.Text = (gvrow.FindControl("MembresiaDocumento") as Label).Text;
            txtPagoEdit.Text = (gvrow.FindControl("MembresiaPago") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void BuscarUsr_Click(object sender, EventArgs e)
    {
        //Mostrar la lista de Usuarios encontrados con ese Valor..
        string datoConsulta = txtUsuario.Text;
        txtFechaInicio.Text = "";
        txtFechaFin.Text = "";
        BindGridView2(datoConsulta);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#bscModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtMembresiaDoc.Text != "" && txtMembresiaPago.Text != "" && txtFechaInicio.Text != "" && txtFechaFin.Text != "")
        {
            string UsuarioID = hdfUsuarioID.Value;
            string Documento = txtMembresiaDoc.Text;
            string Pago = txtMembresiaPago.Text;
            string FechaInicio = txtFechaInicio.Text;
            string FechaFin = txtFechaFin.Text;
            string Observacion = "";
            string sSelect = " INSERT INTO Membresia (MembresiaUsuarioID, MembresiaFechaInicio," +
                             " MembresiaFechaFin, MembresiaDocumento, MembresiaPago, MembresiaObservacion, MembresiaFechaRegistro) VALUES( " +
                             " " + UsuarioID + ", CONVERT(DATE, '" + FechaInicio + "', 103), CONVERT(DATE, '" + FechaFin + "', 103), '" + Documento + "', " +
                             " '" + Pago + "', '" + Observacion + "', SYSDATETIME()) ";
            Utilidades.EjeSQL(sSelect, cn, ref Err, false);
            if (Err == "")
            {
                txtMembresiaDoc.Text = string.Empty;
                txtMembresiaPago.Text = string.Empty;
                txtFechaInicio.Text = string.Empty;
                txtFechaFin.Text = string.Empty;
                txtUsuario.Text = string.Empty;
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
                MostrarMsjModal("Error: " + Err, "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Debe ingresar todos los datos solicitados", "ERR");
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView2.Rows[index];
        if (e.CommandName.Equals("selectRecord"))
        {
            txtUsuario.Text = (gvrow.FindControl("UsuarioNombreB") as Label).Text + " " + (gvrow.FindControl("UsuarioApellidoB") as Label).Text;
            hdfUsuarioID.Value = (gvrow.FindControl("UsuarioIDB") as HiddenField).Value;
            txtFechaInicio.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtFechaFin.Text = DateTime.Today.AddYears(1).Date.ToString("dd/MM/yyyy");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('bscModal').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.EditIndex = -1;
        GridView2.SelectedIndex = -1;
        GridView2.PageIndex = e.NewPageIndex;
        string datoConsulta = txtUsuario.Text;
        BindGridView2(datoConsulta);
    }

    protected void BindGridView2(string dato)
    {
        string condicion = "";
        if (dato != null)
        {
            condicion = " AND (us.UsuarioCedula LIKE '%" + dato + "%' OR us.UsuarioNombre LIKE '%" + dato + "%')";
        }
        try
        {
            cn.Open();
            string cmd2 = "SELECT DISTINCT(us.UsuarioCedula) as UsuarioCedula, us.UsuarioID as UsuarioID," +
                            "us.UsuarioNombre as UsuarioNombre, us.UsuarioApellido as UsuarioApellido, " +
                            " ur.SucursalID as SucursalID, " +
                            "(SELECT RolDescripcion FROM Rol WHERE RolId = 3) as RolDescripcion, (" +
                            "Select c.ClienteID as Cliente FROM Cliente c Where c.ClienteID=ur.ClienteID) as ClienteID, (" +
                            "Select c.ClienteNombre as ClienteN FROM Cliente c Where c.ClienteID=ur.ClienteID) as ClienteNombre " +
                            "FROM Usuario us, Rol r, UsuarioRol ur " +
                            "WHERE us.UsuarioID = ur.UsuarioID AND ur.RolID = 3  " + condicion;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDocumentoEdit.Text != "" && txtPagoEdit.Text != "" && txtFechaInicioEdit.Text != "" && txtFechaFinEdit.Text != "")
        {
            string MembresiaID = hdfMembresiaID.Value;
            string sSelect = "UPDATE Membresia SET MembresiaDocumento = '" + txtDocumentoEdit.Text + "', " +
                             " MembresiaPago = '" + txtPagoEdit.Text + "', " +
                             " MembresiaFechaInicio = CONVERT(DATE, '" + txtFechaInicioEdit.Text + "', 103), " +
                             " MembresiaFechaFin = CONVERT(DATE, '" + txtFechaFinEdit.Text + "',103) " +
                             " WHERE MembresiaID = " + MembresiaID;
            Utilidades.EjeSQL(sSelect, cn, ref Err, false);
            if (Err == "")
            {
                BindGridView();
                MostrarMsjModal("Registro Editado Exitosamente", "EXI");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("document.getElementById('closeEdit').click();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
            }
            else
            {
                MostrarMsjModal("Error: " + Err, "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Debe llenar todos los campos", "ERR");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string sSelect = "DELETE FROM Membresia WHERE MembresiaID = " + hdfMembresiaIDEli.Value;
        Utilidades.EjeSQL(sSelect, cn, ref Err, false);
        if (Err == "")
        {
            BindGridView();
            MostrarMsjModal("Registro Eliminado Exitosamente", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeDelete').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
        }
        else
        {
            MostrarMsjModal("Error: " + Err, "ERR");
        }
    }
}