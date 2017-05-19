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

public partial class sistema_Asistencias : System.Web.UI.Page
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
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            BindGridView();
            sSelectSQL = "SELECT DISTINCT(dbo.ClasePlantilla.ClasePlantillaID) as VAL, " +
                          "  (dbo.Clase.ClaseDescripcion+' '+ " +
                          "  dbo.ClasePlantilla.ClasePlantillaFecha+' '+" +
                          "  dbo.ClasePlantilla.ClasePlantillaHora+' '+" +
                          "  dbo.Cliente.ClienteNombre) as TXT" +
                          "  FROM dbo.Clase INNER JOIN" +
                          "  dbo.ClasePlantilla ON dbo.Clase.ClaseID = dbo.ClasePlantilla.ClaseID INNER JOIN" +
                          "  dbo.Cliente ON dbo.ClasePlantilla.ClienteID = dbo.Cliente.ClienteID INNER JOIN" +
                          "  dbo.Reserva ON dbo.ClasePlantilla.ClasePlantillaID = dbo.Reserva.ClasePlantillaID" +
                          "  WHERE (dbo.ClasePlantilla.ProfesorID = " + _autenticado.UsuarioID + ")" +
                          "  AND (CONVERT(DATE,dbo.ClasePlantilla.ClasePlantillaFecha, 103) = CONVERT(DATE, GETDATE(), 103))";

            /* sSelectSQL = "SELECT DISTINCT(ClasePlantilla.ClasePlantillaID) as ID, (Clase.ClaseDescripcion+' '+ClasePlantilla.ClasePlantillaFecha+' '+ClasePlantilla.ClasePlantillaHora) as TXT, " +
                         " Clase.ClaseID as VAL" +
                         " FROM ClasePlantilla INNER JOIN" +
                         " Reserva ON ClasePlantilla.ClasePlantillaID = Reserva.ClasePlantillaID INNER JOIN" +
                         " Clase ON ClasePlantilla.ClaseID = Clase.ClaseID" +
                         " WHERE (ClasePlantilla.ProfesorID = " + _autenticado.UsuarioID + ") AND " +
                         " CONVERT(date,ClasePlantilla.ClasePlantillaFecha,103) >= CONVERT(date,GETDATE(),103) ";
             MostrarMsjModal(sSelectSQL, "");*/
            Utilidades.CargarListado(ref dplClases, sSelectSQL, cn, ref Err, true);
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT ClaseAsistenciaID as ClaseAsistenciaID," +
                           " (SELECT UsuarioCedula FROM Usuario WHERE UsuarioID = ClaseAsistenciaUsuarioID) as UsuarioCedula," +
                           " (SELECT UsuarioNombre FROM Usuario WHERE UsuarioID = ClaseAsistenciaUsuarioID) as UsuarioNombre," +
                           " (SELECT UsuarioApellido FROM Usuario WHERE UsuarioID = ClaseAsistenciaUsuarioID) as UsuarioApellido," +
                           " (SELECT ClaseDescripcion FROM Clase WHERE ClaseID = ClaseAsistenciaClaseID) as ClaseDescripcion," +
                           " (SELECT ClasePlantillaFecha FROM ClasePlantilla WHERE ClasePlantillaID = ClaseAsistenciaPlantillaID) as FechaClase," +
                           " (SELECT ClasePlantillaHora FROM ClasePlantilla WHERE ClasePlantillaID = ClaseAsistenciaPlantillaID) as HoraClase," +
                           " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = ClaseAsistenciaProfesorID) as ProfesorNombre, " +
                           " ClaseAsistActivo as ClaseAsist FROM ClaseAsistencia" +
                           " WHERE ClaseAsistenciaProfesorID = " + _autenticado.UsuarioID + " " +
                           " AND CONVERT(DATE, (SELECT ClasePlantillaFecha FROM ClasePlantilla WHERE ClasePlantillaID = ClaseAsistenciaPlantillaID), 103) >= CONVERT(DATE, SYSDATETIME(),103) " +
                           " " + ViewState["FiltroClase"] + " " + ViewState["FiltroUsuario"] + " " + ViewState["FiltroEstado"] + "";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClaseAsistenciaID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            MostrarMsjModal(ex.Message, "ERR");
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
        string cmd2 = "SELECT " +
                           " (SELECT UsuarioCedula FROM Usuario WHERE UsuarioID = ClaseAsistenciaUsuarioID) as UsuarioCedula," +
                           " (SELECT UsuarioNombre FROM Usuario WHERE UsuarioID = ClaseAsistenciaUsuarioID) as UsuarioNombre," +
                           " (SELECT UsuarioApellido FROM Usuario WHERE UsuarioID = ClaseAsistenciaUsuarioID) as UsuarioApellido," +
                           " (SELECT ClaseDescripcion FROM Clase WHERE ClaseID = ClaseAsistenciaClaseID) as ClaseDescripcion," +
                           " (SELECT ClasePlantillaFecha FROM ClasePlantilla WHERE ClasePlantillaID = ClaseAsistenciaPlantillaID) as FechaClase," +
                           " (SELECT ClasePlantillaHora FROM ClasePlantilla WHERE ClasePlantillaID = ClaseAsistenciaPlantillaID) as HoraClase," +
                           " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = ClaseAsistenciaProfesorID) as ProfesorNombre, " +
                           " ClaseAsistActivo as ClaseAsist FROM ClaseAsistencia" +
                           " WHERE ClaseAsistenciaProfesorID = " + _autenticado.UsuarioID + " " +
                           " AND CONVERT(DATE, (SELECT ClasePlantillaFecha FROM ClasePlantilla WHERE ClasePlantillaID = ClaseAsistenciaPlantillaID), 103) >= CONVERT(DATE, SYSDATETIME(),103) " +
                           " " + ViewState["FiltroClase"] + " " + ViewState["FiltroUsuario"] + " " + ViewState["FiltroEstado"] + "";
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "ClaseAsist";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=asistencias_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    protected void Agregar_Click(object sender, EventArgs e)
    {
        //Cargar los Usuarios que pertenecen a la clase
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtBuscarClase.Text != "")
        {
            ViewState["FiltroClase"] = " AND ((SELECT ClaseDescripcion FROM Clase WHERE ClaseID = ClaseAsistenciaClaseID) LIKE '%" + txtBuscarClase.Text + "%') ";
            BindGridView();
        }
        else
        {
            ViewState["FiltroClase"] = "";
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
        if (e.CommandName.Equals("editRecord"))
        {
            hAsistenciaMod.Value = (gvrow.FindControl("AsitenciaID") as Label).Text;
            txtApellidoEdit.Text = (gvrow.FindControl("UsuarioApellido") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("UsuarioNombre") as Label).Text;
            txtClaseEdit.Text = (gvrow.FindControl("ClaseDescripcion") as Label).Text;
            txtCedulaEdit.Text = (gvrow.FindControl("UsuarioCedula") as Label).Text;
            txtFechaEdit.Text = (gvrow.FindControl("FechaClase") as Label).Text;
            txtHoraEdit.Text = (gvrow.FindControl("HoraClase") as Label).Text;
            chkAsistioEdit.Checked = (gvrow.FindControl("ClaseAsist") as CheckBox).Checked;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("deleteRecord"))
        {
            hAsitenciaDel.Value = (gvrow.FindControl("AsitenciaID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
    }


    protected void dplClases_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplClases.SelectedValue != "")
        {
            sSelectSQL = "SELECT " +
                        " DISTINCT(UsuarioID) as ID, Reserva.UsuarioID as VAL, (SELECT UsuarioNombre+' '+UsuarioApellido+' '+UsuarioCedula " +
                            " FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as TXT " +
                        " FROM ClasePlantilla INNER JOIN " +
                        " Reserva ON ClasePlantilla.ClasePlantillaID = Reserva.ClasePlantillaID " +
                        " WHERE ClasePlantilla.ClaseID = " + dplClases.SelectedValue + " AND ClasePlantilla.ProfesorID = " + _autenticado.UsuarioID + "" +
                        " AND CONVERT(date,ClasePlantilla.ClasePlantillaFecha,103) >= CONVERT(date, GETDATE(),103)";
            //LLAMAR A 
            Response.Redirect("AgregarAsistencia.aspx?ClaseID=" + dplClases.SelectedValue);

        }
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string ClaseAsistenciaID = hAsitenciaDel.Value;
        cn.Open();
        string SQL_1 = "DELETE FROM ClaseAsistencia Where ClaseAsistenciaID = " + ClaseAsistenciaID;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ClaseAsistenciaID = hAsistenciaMod.Value;
        int iRes = 0;
        cn.Open();
        string sSelectSQL = "UPDATE ClaseAsistencia SET ClaseAsistActivo ='" + chkAsistioEdit.Checked + "' WHERE ClaseAsistenciaID=" + ClaseAsistenciaID;
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

    protected void btnBuscarUsuario_Click(object sender, EventArgs e)
    {
        if (txtBuscarUsuario.Text != "")
        {
            ViewState["FiltroUsuario"] = " AND ((SELECT UsuarioCedula FROM Usuario WHERE UsuarioID = ClaseAsistenciaUsuarioID) LIKE '%" + txtBuscarUsuario.Text + "%') ";
            BindGridView();
        }
        else
        {
            ViewState["FiltroUsuario"] = "";
            BindGridView();
        }
    }

    protected void btnBuscarEstado_Click(object sender, EventArgs e)
    {
        if (dplEstado.SelectedValue != "")
        {
            ViewState["FiltroEstado"] = " AND (ClaseAsistActivo = " + dplEstado.SelectedValue + ") ";
            BindGridView();
        }
        else
        {
            ViewState["FiltroEstado"] = "";
            BindGridView();
        }
    }
}