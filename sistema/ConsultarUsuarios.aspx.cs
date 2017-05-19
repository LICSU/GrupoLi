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
using System.IO;
using System.Web.UI.HtmlControls;
using System.Reflection;

public partial class sistema_ConsultarUsuarios : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    bool bHecho = false;
    string Err = "", sSelectSQL = "";
    DataTable dataTable;
    DataView vista;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string sMsj = "";
    private string _sortDirection;
    GridView grid;
    string[] tipoParametro;
    string[] labelParametro;
    string[] labelIDparametro;
    string[] clienteID;
    TextBox textBox;
    Panel panel, panel_2;
    Label label;
    int tamVector = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["registro"] == "exito")
        {
            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            // make collection editable
            isreadonly.SetValue(this.Request.QueryString, false, null);
            // remove
            this.Request.QueryString.Remove("registro");
            MostrarMsjModal("Registro realizado con éxito.", "EXI");
        }
        this.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
        _autenticado = new UsuarioAutenticado(fIdentity);
        panel = (Panel)FindControl("panel1");
        panel_2 = (Panel)FindControl("panel11");
        //Campos Adicionales
        cargarAdicionales();
        //Clientes
        if (Request.QueryString["val"] == "1")
        {
            MostrarMsjModal("Usuario Agregado con Éxito", "EXI");
        }
        if (!IsPostBack)
        {
            string sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente ORDER BY TXT";
            Utilidades.CargarListado(ref dplEmpresas, sSelectSQL, cn, ref Err, true);
            string sSelectSQL2 = "SELECT RolID AS VAL, RolDescripcion AS TXT FROM Rol ORDER BY TXT";
            Utilidades.CargarListado(ref dplRol, sSelectSQL2, cn, ref Err, true);
            ViewState["RolID"] = "r.RolID";
            ViewState["Rol"] = "r.RolDescripcion";
            ViewState["SortExpression"] = "UsuarioNombre";
            BindGridView();
        }
    }

    protected void cargarAdicionales()
    {
        sSelectSQL = "SELECT COUNT(*) FROM Parametros WHERE Parametros.activoParametro = 1";
        tamVector = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
        if (tamVector > 0)
        {
            tipoParametro = new string[tamVector];
            labelParametro = new string[tamVector];
            labelIDparametro = new string[tamVector];
            clienteID = new string[tamVector];
            int contParametros = 0;
            sSelectSQL = "SELECT Parametros.idParametro, "
                        + " Parametros.tipoParametro as tipoParametro, "
                        + " Parametros.labelParametro as labelParametro, "
                        + " Parametros.labelIdParametro as labelIDparametro, "
                        + " Parametros.obserParametro obserParametro, "
                        + " Parametros.activoParametro as activoParametro, "
                        + " Parametros.ClienteID as ClienteID "
                        + " FROM Cliente INNER JOIN "
                        + " Parametros ON Cliente.ClienteID = Parametros.ClienteID INNER JOIN "
                        + " TipoParametro ON Parametros.tipoParametro = TipoParametro.idTipoParametro "
                        + " WHERE (Parametros.activoParametro = 1) ";
            cn.Open();
            SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            string tipoParametro1 = "";
            try
            {
                while (dr.Read())
                {
                    ViewState["adicionales"] = ViewState["adicionales"] + "," + dr["labelIDparametro"].ToString();
                    tipoParametro[contParametros] = tipoParametro1 = dr["tipoParametro"].ToString();
                    labelParametro[contParametros] = dr["labelParametro"].ToString();
                    labelIDparametro[contParametros] = dr["labelIDparametro"].ToString();
                    clienteID[contParametros] = dr["ClienteID"].ToString();
                    if (tipoParametro1 == "1")
                    {
                        //Creamos TEXTBOX
                        agregarTextBoxVer(dr["labelIdParametro"].ToString(), dr["labelParametro"].ToString());
                        agregarTextBoxEdit(dr["labelIdParametro"].ToString(), dr["labelParametro"].ToString());
                    }
                    contParametros++;
                }
                cn.Close();
            }
            catch (SqlException sq)
            {
                Err = sq.Message;
                cn.Close();
            }
        }
    }
    protected void agregarTextBoxVer(string ID, string etiqueta)
    {
        Literal literal;
        literal = new Literal();
        literal.Text = "<div class='form-group'>";
        panel1.Controls.Add(literal);
        literal = new Literal();
        literal.Text = "<div class='col-xs-2'></div>";
        panel1.Controls.Add(literal);
        label = new Label();
        label.CssClass = "col-xs-2 control-label";
        label.Text = etiqueta;
        label.ID = "lbl1" + ID;
        panel1.Controls.Add(label);
        literal = new Literal();
        literal.Text = "<div class='col-xs-6'>";
        panel1.Controls.Add(literal);
        textBox = new TextBox();
        textBox.CssClass = "form-control";
        textBox.ID = "p1" + ID;
        textBox.Enabled = false;
        panel1.Controls.Add(textBox);
        literal = new Literal();
        literal.Text = "</div>";
        panel1.Controls.Add(literal);
        literal = new Literal();
        literal.Text = "<div class='col-xs-4'></div>";
        panel1.Controls.Add(literal);
        literal = new Literal();
        literal.Text = "</div>";
        panel1.Controls.Add(literal);
    }
    protected void agregarTextBoxEdit(string ID, string etiqueta)
    {
        Literal literal;
        literal = new Literal();
        literal.Text = "<div class='form-group'>";
        panel11.Controls.Add(literal);
        literal = new Literal();
        literal.Text = "<div class='col-xs-2'></div>";
        panel11.Controls.Add(literal);
        label = new Label();
        label.CssClass = "col-xs-2 control-label";
        label.Text = etiqueta;
        label.ID = "lbl2" + ID;
        panel11.Controls.Add(label);
        literal = new Literal();
        literal.Text = "<div class='col-xs-6'>";
        panel11.Controls.Add(literal);
        textBox = new TextBox();
        textBox.CssClass = "form-control";
        textBox.ID = "p2" + ID;
        panel11.Controls.Add(textBox);
        literal = new Literal();
        literal.Text = "</div>";
        panel11.Controls.Add(literal);
        literal = new Literal();
        literal.Text = "<div class='col-xs-4'></div>";
        panel11.Controls.Add(literal);
        literal = new Literal();
        literal.Text = "</div>";
        panel11.Controls.Add(literal);
    }
    private void Page_PreRenderComplete(object sender, EventArgs e)
    { }
    protected void BindGridView()
    {
        try
        {
            string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string cmd2 = "SELECT DISTINCT(us.UsuarioCedula) as UsuarioCedula, us.UsuarioID as UsuarioID,us.UsuarioClave as UsuarioClave" +
                         ",us.UsuarioFechaRegistro, " +
                         " us.UsuarioEPS as UsuarioEPS, us.UsuarioObservacion as UsuarioObservacion, us.UsuarioCelular2 as UsuarioCelular2," +
                         " us.UsuarioCelular1 as UsuarioCelular1,us.UsuarioTelefono as UsuarioTelefono, us.UsuarioSexo as UsuarioSexo," +
                         " us.UsuarioFechaNacimiento as UsuarioFechaNacimiento, us.UsuarioEstadoCivil as UsuarioEstadoCivil, us.UsuarioCorreo as UsuarioCorreo " +
                         " " + ViewState["adicionales"] + ",us.UsuarioNombre as UsuarioNombre, us.UsuarioApellido as UsuarioApellido, (us.UsuarioNombre+' '+us.UsuarioApellido) as UsuarioNombres," +
                         " " + ViewState["Rol"] + " as RolDescripcion, (Select c.ClienteNombre as Cliente FROM Cliente c Where c.ClienteID=ur.ClienteID) as ClienteID, (Select c.ClienteID as Cliente FROM Cliente c Where c.ClienteID=ur.ClienteID) as EmpresaID" +
                         " FROM Usuario us, Rol r, UsuarioRol ur WHERE us.UsuarioID = ur.UsuarioID AND  " +
                         " ur.RolID = " + ViewState["RolID"] + ViewState["sWhere"] + ViewState["EmpresaID"];
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, conn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dataTable = ds.Tables[0];
            string[] TablaID = new string[2];
            TablaID[0] = "UsuarioID";
            TablaID[1] = "UsuarioNombres";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();

        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " AND (us.UsuarioNombre LIKE '%" + sBuscar + "%' OR us.UsuarioCedula LIKE '%" + sBuscar + "%' OR us.UsuarioApellido LIKE '%" + sBuscar + "%')"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }
    protected void btnAgregar_Click(object sender, EventArgs e)
    {

        Response.Redirect("registrar.aspx");

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

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
        //txtSearch.Text = e.CommandName;
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName.Equals("Sort"))
        {

            if (ViewState["SortExpression"] != null)
            {
                if (this.ViewState["SortExpression"].ToString() == e.CommandArgument.ToString())
                {
                    if (ViewState["SortOrder"].ToString() == "ASC")
                        ViewState["SortOrder"] = "DESC";
                    else
                        ViewState["SortOrder"] = "ASC";
                }
                else
                {
                    ViewState["SortOrder"] = "ASC";
                    ViewState["SortExpression"] = e.CommandArgument.ToString();
                }

            }
            else
            {
                ViewState["SortExpression"] = e.CommandArgument.ToString();
                ViewState["SortOrder"] = "ASC";
            }
        }
        if (e.CommandName.Equals("deleteRecord"))
        {
            GridViewRow gvrow = GridView1.Rows[index];
            string UsuarioID = (gvrow.FindControl("IDUsuario_1") as Label).Text;//
            hfCedula.Value = UsuarioID;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        if (e.CommandName.Equals("viewRecord"))
        {
            TextBox textBox2 = new TextBox();
            Label label1 = new Label();
            GridViewRow gvrow = GridView1.Rows[index];
            string ClienteID = (gvrow.FindControl("ClienteID") as Label).Text;
            string UsuarioID = (gvrow.FindControl("IDUsuario_1") as Label).Text;
            string contrsenha = (gvrow.FindControl("Clave") as Label).Text;
            txtClaveVer.Attributes.Add("value", contrsenha);
            /*SELECT para los Adicionales */
            /* Validar si los Adicionales pertenecen al usuario **/
            if (tamVector > 0)
            {
                for (int i = 0; i < clienteID.Length; i++)
                {
                    textBox2 = (TextBox)panel.FindControl("p1" + labelIDparametro[i]);
                    label1 = (Label)panel.FindControl("lbl1" + labelIDparametro[i]);
                    sSelectSQL = "Select " + labelIDparametro[i] + " FROM Usuario WHERE UsuarioID = " + UsuarioID;
                    textBox2.Text = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
                    if (clienteID[i] != ClienteID)
                    {
                        textBox2.Visible = false;
                        label1.Visible = false;
                    }
                    else
                    {
                        textBox2.Visible = true;
                        label1.Visible = true;
                    }
                }
            }
            txtCedulaVer.Text = (gvrow.FindControl("Cedula") as Label).Text;
            txtNombreVer.Text = (gvrow.FindControl("Nombre") as Label).Text;
            txtApellidoVer.Text = (gvrow.FindControl("Apellido") as Label).Text;
            txtRolVer.Text = (gvrow.FindControl("Rol") as Label).Text;
            txtCorreoVer.Text = (gvrow.FindControl("Correo") as Label).Text;
            txtFechaNVer.Text = (gvrow.FindControl("FechaN") as Label).Text;
            txtEstadoCVer.Text = (gvrow.FindControl("EstadoC") as Label).Text;
            if (txtEstadoCVer.Text == "S") txtEstadoCVer.Text = "Soltero(a)";
            if (txtEstadoCVer.Text == "C") txtEstadoCVer.Text = "Casado(a)";
            if (txtEstadoCVer.Text == "V") txtEstadoCVer.Text = "Viudo(a)";
            if (txtEstadoCVer.Text == "D") txtEstadoCVer.Text = "Divorciado(a)";
            if (txtEstadoCVer.Text == "O") txtEstadoCVer.Text = "Otro(a)";
            txtSexoVer.Text = (gvrow.FindControl("Sexo") as Label).Text;
            if (txtSexoVer.Text == "M") txtSexoVer.Text = "Masculino";
            if (txtSexoVer.Text == "F") txtSexoVer.Text = "Femenino";
            txtTelefonoVer.Text = (gvrow.FindControl("Telefono") as Label).Text;
            txtCelular1Ver.Text = (gvrow.FindControl("Celular1") as Label).Text;
            txtCelular2Ver.Text = (gvrow.FindControl("Celular2") as Label).Text;
            txtEPSVer.Text = (gvrow.FindControl("EPS") as Label).Text;
            txtObserVer.Text = (gvrow.FindControl("Observacion") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#viewModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
        if (e.CommandName.Equals("editRecord"))
        {
            GridViewRow gvrow = GridView1.Rows[index];
            TextBox textBox2 = new TextBox();
            Label label1 = new Label();
            string ClienteID = (gvrow.FindControl("ClienteID") as Label).Text;
            string UsuarioID = (gvrow.FindControl("IDUsuario_1") as Label).Text;
            if (tamVector > 0)
            {
                for (int i = 0; i < clienteID.Length; i++)
                {
                    textBox2 = (TextBox)panel_2.FindControl("p2" + labelIDparametro[i]);
                    label1 = (Label)panel_2.FindControl("lbl2" + labelIDparametro[i]);
                    sSelectSQL = "Select " + labelIDparametro[i] + " FROM Usuario WHERE UsuarioID = " + UsuarioID;
                    textBox2.Text = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
                    if (clienteID[i] != ClienteID)
                    {
                        textBox2.Visible = false;
                        label1.Visible = false;
                    }
                    else
                    {
                        textBox2.Visible = true;
                        label1.Visible = true;
                    }
                }
            }
            string contrsenha = (gvrow.FindControl("Clave") as Label).Text;
            txtClave.Attributes.Add("value", contrsenha);
            txtClave2.Attributes.Add("value", contrsenha);
            hClienteIDMod.Value = (gvrow.FindControl("IDUsuario_1") as Label).Text;
            txtCedulaEdit.Text = (gvrow.FindControl("Cedula") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("Nombre") as Label).Text;
            txtApellidoEdit.Text = (gvrow.FindControl("Apellido") as Label).Text;
            txtRolEdit.Text = (gvrow.FindControl("Rol") as Label).Text;
            txtCorreoEdit.Text = (gvrow.FindControl("Correo") as Label).Text;
            txtFechaNEdit2.Value = (gvrow.FindControl("FechaN") as Label).Text;
            //txtEstadoCEdit.Text = (gvrow.FindControl("EstadoC") as Label).Text;
            //txtSexoEdit.Text = (gvrow.FindControl("Sexo") as Label).Text;
            txtTelefonoEdit.Text = (gvrow.FindControl("Telefono") as Label).Text;
            txtCelular1Edit.Text = (gvrow.FindControl("Celular1") as Label).Text;
            txtCelular2Edit.Text = (gvrow.FindControl("Celular2") as Label).Text;
            txtEPSEdit.Text = (gvrow.FindControl("EPS") as Label).Text;
            txtObserEdit.Text = (gvrow.FindControl("Observacion") as Label).Text;
            if ((gvrow.FindControl("EstadoC") as Label).Text == "S")
                txtEstCiv.SelectedValue = "S";
            if ((gvrow.FindControl("EstadoC") as Label).Text == "C")
                txtEstCiv.SelectedValue = "C";
            if ((gvrow.FindControl("EstadoC") as Label).Text == "D")
                txtEstCiv.SelectedValue = "D";
            if ((gvrow.FindControl("EstadoC") as Label).Text == "V")
                txtEstCiv.SelectedValue = "V";
            if ((gvrow.FindControl("EstadoC") as Label).Text == "O")
                txtEstCiv.SelectedValue = "O";
            if ((gvrow.FindControl("Sexo") as Label).Text == "M")
                rdM.Checked = true;
            else
                rdF.Checked = true;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //ID del Usuario a Eliminar
        string UsuarioID = hfCedula.Value;
        sSelectSQL = "delete from Reserva Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from Alumno_Nivel_Clase Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from Reserva Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from PlanAlumnoAcumulado Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from PlanEmpresa Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from PlanEmpresaHistorial Where PlanEmpresaUsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from PlanAutorizacion Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from PlanAlumno Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from UsuarioRol Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        sSelectSQL = "delete from Usuario Where UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
        BindGridView();
        MostrarMsjModal("Registro Eliminado exitosamente", "EXI");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('closeDelete').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteHideModalScript", sb.ToString(), false);
    }
    private bool sonValidos()
    {
        bool bRes = true;
        //Datos Obligatorios Nombre, Apellido, Clave, Activo
        string Clave = txtClave.Text;
        string Clave2 = txtClave2.Text;
        string Nombre = txtNombreEdit.Text;
        string Apellido = txtApellidoEdit.Text;
        if (Clave == "" || Clave2 == "") { sMsj += "El campo Clave es requerido. <br/>"; bRes = false; }
        if (Nombre == "") { sMsj += "El campo Nombre es requerido. <br/>"; bRes = false; }
        if (Apellido == "") { sMsj += "El campo Apellido es requerido. <br/>"; bRes = false; }
        if (Clave != Clave2) { sMsj += "Las Contraseñas deben Ser Iguales. <br/>"; bRes = false; }
        return bRes;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //ID del Usuario a Modificar
        string Clave = txtClave.Text;
        string UsuarioID12 = hClienteIDMod.Value;
        string Nombre = txtNombreEdit.Text;
        string Apellido = txtApellidoEdit.Text;
        string Correo = txtCorreoEdit.Text;
        string FechaNacimiento = txtFechaNEdit2.Value;
        string EstadoCivil = txtEstCiv.SelectedValue;
        string Sexo = ""; if (rdM.Checked) Sexo = "M"; else Sexo = "F";
        string Telefono = txtTelefonoEdit.Text;
        string Celular1 = txtCelular1Edit.Text;
        string Celular2 = txtCelular2Edit.Text;
        string EPS = txtEPSEdit.Text;
        string Obs = txtObserEdit.Text;
        FechaNacimiento = Utilidades.FecUni(FechaNacimiento);
        string cadena = "";
        TextBox tb = new TextBox();
        for (int i = 0; i < clienteID.Length; i++)
        {
            tb = (TextBox)panel_2.FindControl("p2" + labelIDparametro[i]);
            cadena = cadena + ", " + labelIDparametro[i] + " = " + Utilidades.SiEsNulo(tb.Text, "T");
        }

        if (sonValidos())
        {
            cn.Open();
            int iRes = 0;
            string SQL_1 = "UPDATE Usuario SET UsuarioClave=" + Utilidades.SiEsNulo(Clave, "T") + cadena + " , UsuarioNombre =" + Utilidades.SiEsNulo(Nombre, "T") + " ,UsuarioApellido =" + Utilidades.SiEsNulo(Apellido, "T") + ", UsuarioCorreo=" + Utilidades.SiEsNulo(Correo, "T") + ", UsuarioEstadoCivil='" + EstadoCivil + "', UsuarioSexo='" + Sexo + "', UsuarioTelefono=" + Utilidades.SiEsNulo(Telefono, "N") + ", UsuarioCelular1=" + Utilidades.SiEsNulo(Celular1, "N") + ", UsuarioCelular2=" + Utilidades.SiEsNulo(Celular2, "N") + ", UsuarioFechaNacimiento=" + Utilidades.SiEsNulo(FechaNacimiento, "F") + ", UsuarioEPS=" + Utilidades.SiEsNulo(EPS, "T") + ", UsuarioObservacion=" + Utilidades.SiEsNulo(Obs, "T") + " WHERE UsuarioID = " + UsuarioID12;

            SqlCommand addCmd = new SqlCommand(SQL_1, cn);
            try { iRes = addCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { MostrarMsjModal(ex.Message, "ERR"); }
            cn.Close();

            if (iRes > 0)
            {
                BindGridView();
                MostrarMsjModal("Registro modificado exitosamente", "EXI");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("document.getElementById('closeEdit').click();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }
        }
        else
        {
            MostrarMsjModal(sMsj, "ERR");
        }

    }
    protected void btnAddRecord_Click(object sender, EventArgs e)
    { }
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

    protected void dplEmpresas_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["EmpresaID"] = " AND ClienteID = " + dplEmpresas.SelectedValue;
        BindGridView();
    }

    protected void dplRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Rol"] = "(SELECT RolDescripcion FROM Rol WHERE RolId = " + dplRol.SelectedValue + ")";
        ViewState["RolID"] = dplRol.SelectedValue;
        BindGridView();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        txtSearch.Text = e.SortExpression;
        try
        {
            string[] strSortExpression = ViewState["SortExpression"].ToString().Split(' ');
            if (strSortExpression[0] == e.SortExpression)
            {
                if (strSortExpression[1] == "ASC")
                {
                    ViewState["SortExpression"] = e.SortExpression + " " + "DESC";
                }
                else
                {
                    ViewState["SortExpression"] = e.SortExpression + " " + "ASC";
                }
            }
            // If sorting column is another column,  
            // then specify the sort order to "Ascending".
            else
            {
                ViewState["SortExpression"] = e.SortExpression + " " + "ASC";
            }
            BindGridView();
        }
        catch (FormatException ft)
        {
            Console.WriteLine("Error: " + ft.Message);
        }
    }

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        string connString = ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString;
        SqlConnection conn = new SqlConnection(connString);
        conn.Open();
        string cmd2 = "SELECT DISTINCT(us.UsuarioCedula) as UsuarioCedula," +
                     " us.UsuarioSexo as UsuarioSexo," +
                     " us.UsuarioNombre as UsuarioNombre, us.UsuarioApellido as UsuarioApellido," +
                     " " + ViewState["Rol"] + " as RolDescripcion, (Select c.ClienteNombre as Cliente FROM Cliente c Where c.ClienteID=ur.ClienteID) as ClienteID" +
                     " FROM Usuario us, Rol r, UsuarioRol ur WHERE us.UsuarioID = ur.UsuarioID AND  " +
                     " ur.RolID = " + ViewState["RolID"] + ViewState["sWhere"] + ViewState["EmpresaID"];
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, conn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dataTable = ds.Tables[0];
        string[] TablaID = new string[2];
        TablaID[0] = "UsuarioCedula";
        TablaID[1] = "UsuarioNombre";
        grid.DataKeyNames = TablaID;
        grid.DataSource = dataTable;
        grid.DataBind();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.IO.StringWriter sw = new System.IO.StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page page = new Page();
        System.Web.UI.HtmlControls.HtmlForm form = new System.Web.UI.HtmlControls.HtmlForm();
        GridView Grid = new GridView();
        grid.DataSource = dataTable;
        grid.DataBind();
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
        Response.AddHeader("Content-Disposition", "attachment;filename=usuarios_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}