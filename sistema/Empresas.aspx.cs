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

public partial class sistema_Empresas : System.Web.UI.Page
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
        this.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
        _autenticado = new UsuarioAutenticado(fIdentity);
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT c.ClienteID as ClienteID, c.ClienteNombre as ClienteNombre, c.FormularioTipo as FormularioTipo, "
                        + " c.ClienteDescripcion as ClienteDescripcion, c.ClientePersonaContacto as ClientePersonaContacto, "
                        + " c.ParroquiaID as ParroquiaID, c.ClienteDireccion as ClienteDireccion, c.ClienteTelefono1 as ClienteTelefono1, "
                        + " c.ClienteTelefono2 as ClienteTelefono2, c.ClienteCorreo as ClienteCorreo FROM Cliente c " + ViewState["sWhere"];
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[2];
            TablaID[0] = "ClienteID";
            TablaID[1] = "ClienteDescripcion";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            //Attribute to show the Plus Minus Button.
            GridView1.HeaderRow.Cells[3].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.                
            GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
            GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";

            //Adds THEAD and TBODY to GridView.
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " WHERE c.ClienteNombre LIKE '%" + sBuscar + "%'"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string sSelectSQL = "SELECT ParroquiaID AS VAL, ParroquiaNombre AS TXT FROM Parroquia ORDER BY TXT";
        string sErr = "";
        Utilidades.CargarListado(ref txtParroquiaAdd, sSelectSQL, cn, ref sErr, true);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
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
        string parroquia = "";
        string sSelectSQL1 = "";
        if (e.CommandName.Equals("viewRecord"))
        {
            GridViewRow gvrow = GridView1.Rows[index];
            lblNombreView.Text = (gvrow.FindControl("Nombre") as Label).Text;
            lblDescripcionView.Text = (gvrow.FindControl("Descripcion") as Label).Text;
            lblContactoView.Text = (gvrow.FindControl("Contacto") as Label).Text;
            lblParroquiaView.Text = (gvrow.FindControl("Parroquia") as Label).Text;
            dplFormluarioView.SelectedValue = (gvrow.FindControl("FormularioTipo") as Label).Text;
            if (lblParroquiaView.Text != "")
            {
                try
                {
                    cn.Open();
                    sSelectSQL1 = "Select ParroquiaNombre as Parroquia FROM Parroquia WHERE ParroquiaID =" + lblParroquiaView.Text;
                    SqlCommand cmd = new SqlCommand(sSelectSQL1, cn);
                    SqlDataReader reader;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        parroquia = reader["Parroquia"].ToString();
                    reader.Close();
                    cn.Close();
                }
                catch (SqlException ex)
                {
                    Err += ex.Message + ", SQL: " + sSelectSQL1;
                    MostrarMsjModal(Err, "ERR");
                }
            }
            lblParroquiaView.Text = parroquia;
            lblDireccionView.Text = (gvrow.FindControl("Direccion") as Label).Text;
            lblTelefono1View.Text = (gvrow.FindControl("Telefono1") as Label).Text;
            lblTelefono2View.Text = (gvrow.FindControl("Telefono2") as Label).Text;
            lblCorreoView.Text = (gvrow.FindControl("Correo") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#viewModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("editRecord"))
        {
            GridViewRow gvrow = GridView1.Rows[index];
            hClienteMod.Value = (gvrow.FindControl("IDCliente_1") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("Nombre") as Label).Text;
            txtDescripcionEdit.Text = (gvrow.FindControl("Descripcion") as Label).Text;
            txtContactoEdit.Text = (gvrow.FindControl("Contacto") as Label).Text;
            string sSelectSQL = "SELECT ParroquiaID AS VAL, ParroquiaNombre AS TXT FROM Parroquia ORDER BY TXT";
            string sErr = "";
            Utilidades.CargarListado(ref txtParroquiaEdit, sSelectSQL, cn, ref sErr, true);
            txtParroquiaEdit.SelectedValue = (gvrow.FindControl("Parroquia") as Label).Text;
            txtDireccionEdit.Text = (gvrow.FindControl("Direccion") as Label).Text;
            txtTelefono1Edit.Text = (gvrow.FindControl("Telefono1") as Label).Text;
            txtTelefono2Edit.Text = (gvrow.FindControl("Telefono2") as Label).Text;
            txtCorreoEdit.Text = (gvrow.FindControl("Correo") as Label).Text;
            dplFormluarioEdit.SelectedValue = (gvrow.FindControl("FormularioTipo") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("deleteRecord"))
        {
            GridViewRow gvrow = GridView1.Rows[index];
            hClienteDel.Value = (gvrow.FindControl("IDCliente_1") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int ClienteID = int.Parse(hClienteMod.Value.ToString());
        string ClienteNombre = txtNombreEdit.Text;
        string ClienteDescripcion = txtDescripcionEdit.Text;
        string ClienteContacto = txtContactoEdit.Text;
        string Parroquia = txtParroquiaEdit.SelectedValue;
        string ClienteDireccion = txtDireccionEdit.Text;
        string ClienteTelefono1 = txtTelefono1Edit.Text;
        string ClienteTelefono2 = txtTelefono2Edit.Text;
        string ClienteCorreo = txtCorreoEdit.Text;
        string TipoFormulario = dplFormulario.SelectedValue;
        int iRes = 0;
        if (sonValidos("UPDATE"))
        {
            cn.Open();
            string sSelectSQL = "UPDATE Cliente SET ClienteNombre =" + Utilidades.SiEsNulo(ClienteNombre, "T") +
                                ", ClienteDescripcion=" + Utilidades.SiEsNulo(ClienteDescripcion, "T") +
                                ", ClientePersonaContacto=" + Utilidades.SiEsNulo(ClienteContacto, "T") +
                                ", ParroquiaID= " + Utilidades.SiEsNulo(Parroquia, "N") +
                                ", ClienteDireccion = " + Utilidades.SiEsNulo(ClienteDescripcion, "T") +
                                ", ClienteTelefono1 = " + Utilidades.SiEsNulo(ClienteTelefono1, "T") +
                                ", ClienteTelefono2 = " + Utilidades.SiEsNulo(ClienteTelefono2, "T") +
                                ", FormularioTipo = " + Utilidades.SiEsNulo(TipoFormulario, "N") +
                                ", ClienteCorreo = " + Utilidades.SiEsNulo(ClienteCorreo, "T") + " WHERE ClienteID=" + ClienteID;
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
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
        // Nombre, Parroquia
        if (Accion == "INSERT")
        {
            string Nombre = txtNombreAdd.Text;
            string Parroquia = txtParroquiaAdd.SelectedValue;
            string Formulario = dplFormulario.SelectedValue;
            if (Nombre == "") { Err += "El Campo Nombre es Obligatorio \n"; bRes = false; }
            if (Parroquia == "") { Err += "Debe Seleccionar una Parroquia \n"; bRes = false; }
            if (Formulario == "") { Err += "Debe Seleccionar el Tipo de Formulario \n"; bRes = false; }

        }
        else
        {
            string Nombre = txtNombreEdit.Text;
            string Parroquia = txtParroquiaEdit.SelectedValue;
            if (Nombre == "") { Err += "El Campo Nombre es Obligatorio \n"; bRes = false; }
            if (Parroquia == "") { Err += "Debe Seleccionar una Parroquia \n"; bRes = false; }
        }

        return bRes;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string ClienteID = hClienteDel.Value;
        cn.Open();
        string SQL_1 = "DELETE FROM Cliente Where ClienteID = " + ClienteID;
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
        string maxiClienteID = "";
        string sErr = "";
        string sSelectSQL = "SELECT MAX(ClienteID) as MAXIMO FROM Cliente";
        Utilidades.maxRegistro(ref maxiClienteID, sSelectSQL, cn, ref sErr);
        string clienteNombre = txtNombreAdd.Text;
        string clienteDescripcion = txtDescripcionAdd.Text;
        string clientePersonaContacto = txtPersonaContactoAdd.Text;
        string parroquiaID = txtParroquiaAdd.SelectedValue;
        string clienteDireccion = txtDireccionAdd.Text;
        string clienteTelefono1 = txtTelefono1Add.Text;
        string clienteTelefono2 = txtTelefono2Add.Text;
        string clienteCorreo = txtCorreoAdd.Text;
        string formularioTipo = dplFormulario.SelectedValue;
        int clienteID = int.Parse(maxiClienteID.Trim()) + 1;
        int iRes = 0;
        if (sonValidos("INSERT"))
        {
            cn.Open();
            sSelectSQL = "INSERT INTO Cliente (ClienteID, ClienteNombre, ClienteDescripcion, ClientePersonaContacto, ParroquiaID, ClienteDireccion, ClienteTelefono1, ClienteTelefono2, ClienteCorreo, ClienteFechaRegistro, ClienteUsuarioRegistro, FormularioTipo) " +
                         " VALUES (" + clienteID + ", " + Utilidades.SiEsNulo(clienteNombre, "T") + ", " + Utilidades.SiEsNulo(clienteDescripcion, "T") + "," + Utilidades.SiEsNulo(clientePersonaContacto, "T") + "," + parroquiaID + "," + Utilidades.SiEsNulo(clienteDireccion, "T") + "," + Utilidades.SiEsNulo(clienteTelefono1, "T") + "," + Utilidades.SiEsNulo(clienteTelefono2, "T") + "," + Utilidades.SiEsNulo(clienteCorreo, "T") + ",SYSDATETIME(),1, " + Utilidades.SiEsNulo(formularioTipo, "N") + ")";
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try { iRes = addCmd.ExecuteNonQuery(); }
            catch (SqlException sq) { txtCorreoAdd.Text = sq.Message; }
            cn.Close();
            if (iRes > 0)
            {
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
        string cmd2 = "SELECT c.ClienteNombre as ClienteNombre, c.ClienteDescripcion as ClienteDescripcion, c.ClientePersonaContacto as ClientePersonaContacto, c.ClienteDireccion as ClienteDireccion, c.ClienteTelefono1 as ClienteTelefono1, c.ClienteTelefono2 as ClienteTelefono2, c.ClienteCorreo as ClienteCorreo FROM Cliente c " + ViewState["sWhere"];
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "ClienteNombre";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=empresas_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}