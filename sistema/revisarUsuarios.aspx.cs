using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sistema_revisarUsuarios : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    String Err = string.Empty;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utilidades.CargarListado(ref dplClientes, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente WHERE ClienteID > 1 ORDER BY VAL ", cn, ref Err, true);
        }
        BindGridView();
    }
    
    protected void dplClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplClientes.SelectedValue != "")
        {
            ViewState["sWhere"] = " AND dbo.UsuarioRol.ClienteID = " + dplClientes.SelectedValue;
            BindGridView();
        }
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEstado.SelectedValue != "")
        {
            ViewState["sWhere2"] = " AND dbo.PlanEmpresa.PlanActivo = " + ddlEstado.SelectedValue;
            BindGridView();
        }
    }

    protected void ddlEstadoP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEstadoP.SelectedValue != "")
        {
            ViewState["sWhere3"] = " AND dbo.PlanEmpresa.EstadoProximo = " + ddlEstadoP.SelectedValue;
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Close();
            cn.Open();
            string cmd2 = " SELECT DISTINCT(dbo.Usuario.UsuarioID) as UsuarioID, dbo.Usuario.UsuarioCedula, " +
                           " (dbo.Usuario.UsuarioNombre+' '+dbo.Usuario.UsuarioApellido) as UsuarioNombre, " +
                           " CAST(CASE WHEN dbo.PlanEmpresa.PlanActivo = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END as VARCHAR(10)) as EstadoAct, "+
                            " CAST(CASE WHEN dbo.PlanEmpresa.EstadoProximo = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END as VARCHAR(10)) as EstadoSig, " +
                           " dbo.UsuarioRol.ClienteID, dbo.PlanEmpresa.PlanActivo, dbo.PlanEmpresa.EstadoProximo," +
                           " (SELECT ClienteNombre FROM Cliente WHERE ClienteID =  dbo.UsuarioRol.ClienteID) as Empresa," +
                           " (select COUNT(*) from Reserva WHERE MONTH(FechaReserva) = MONTH(SYSDATETIME()) AND YEAR(FechaReserva) = YEAR(SYSDATETIME()) AND UsuarioId = dbo.Usuario.UsuarioID) as NroReservas," +
                           " (select COUNT(*) from Reserva WHERE MONTH(FechaReserva) = MONTH(SYSDATETIME()) - 1 AND YEAR(FechaReserva) = YEAR(SYSDATETIME()) AND UsuarioId = dbo.Usuario.UsuarioID) as NroReservasAnt," +
                           " (select top 1 p.plannombre from planalumno pa join [plan] p on pa.planID = p.PlanId and pa.UsuarioID = dbo.Usuario.UsuarioID order by pa.PlanAlumnoID desc) as PlanNombre," +
                           " (select top 1 TotalClases from planempresa where usuarioid = dbo.Usuario.UsuarioID order by planempresaid desc) as NroClases" +
                           " FROM  dbo.PlanEmpresa INNER JOIN "+
                           " dbo.Usuario ON dbo.PlanEmpresa.UsuarioID = dbo.Usuario.UsuarioID INNER JOIN "+
                           " dbo.UsuarioRol ON dbo.Usuario.UsuarioID = dbo.UsuarioRol.UsuarioID "+
                           " WHERE dbo.UsuarioRol.ClienteID != 1 " + ViewState["sWhere"] + ViewState["sWhere2"] + ViewState["sWhere3"] +
                           " ORDER BY dbo.UsuarioRol.ClienteID ASC";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioCedula";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            //MostrarMsjModal(Err, "ERR");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.EditIndex = -1;
        GridView2.SelectedIndex = -1;
        GridView2.PageIndex = e.NewPageIndex;
        BindGridView2();
    }

    protected void BindGridView2()
    {
        try
        {
            cn.Close();
            cn.Open();
            string cmd2 = "SELECT "+
                          " (SELECT UsuarioNombre+''+UsuarioApellido FROM Usuario WHERE USuarioID = ph.PlanEmpresaUsuarioID) as Usuario," +
                          " CAST(CASE WHEN PlanEmpresaPlanEstado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END as VARCHAR(10)) as Estado, " +
                          " CONVERT(VARCHAR(11), PlanEmpresaFecha, 103) as Fecha "+
                          " FROM PlanEmpresaHistorial ph WHERE ph.PlanEmpresaUsuarioID = "+hdfUsuarioID.Value+" ORDER BY PlanEmpresaHistorial DESC ";            
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Usuario";
            GridView2.DataKeyNames = TablaID;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            //MostrarMsjModal(Err, "ERR");
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("Actual"))
        {
            hUsuarioAct.Value = (gvrow.FindControl("UsuarioID") as Label).Text;
            hEstatusActual.Value = (gvrow.FindControl("EstadoActual") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#ActualModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("Proximo"))
        {
            hUsuarioPro.Value = (gvrow.FindControl("UsuarioID") as Label).Text;
            hEstatusProx.Value = (gvrow.FindControl("EstadoProximo") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#ProximoModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("Historial"))
        {
            hdfUsuarioID.Value = (gvrow.FindControl("UsuarioID") as Label).Text;
            BindGridView2();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalHistorial').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string UsuarioID = hUsuarioAct.Value;
        string EstadoActual = hEstatusActual.Value;
        string sSelect = string.Empty;
        DateTime fechatemp = DateTime.Today;
        string fechaActual = fechatemp.ToString("dd-MM-yyyy");
        if (EstadoActual == "False")
        {
            sSelect = "UPDATE PlanEmpresa SET PlanActivo = 1, fechaUltimo = CONVERT(DATE, '" + fechaActual + "', 103) WHERE UsuarioID = " + UsuarioID;
            EstadoActual = "1";
        }
        else
        {
            sSelect = "UPDATE PlanEmpresa SET PlanActivo = 0, fechaUltimo = CONVERT(DATE, '" + fechaActual + "', 103)  WHERE UsuarioID = " + UsuarioID;
            EstadoActual = "0";
        }
        Utilidades.EjeSQL(sSelect, cn, ref Err, true);
        //Insertar en plan historial el estado nuevo....
        sSelect = " INSERT into planempresahistorial (PlanEmpresaUsuarioID, PlanEmpresaPlanEstado, PlanEmpresaFecha)" +
                   " VALUES (" + UsuarioID + ", '" + EstadoActual + "', CONVERT(DATE, '" + fechaActual + "', 103))";
        Utilidades.EjeSQL(sSelect, cn, ref Err, true);
        //Actualizar la fecha del Plan y Clases....
        
        DateTime FF = new DateTime(fechatemp.Year, fechatemp.Month + 1, 1).AddDays(-1);
        sSelect = "UPDATE PlanAlumno SET PlanAlumnoFechaInicio =  " + Utilidades.FecUni(DateTime.Today.Date.ToString()) + "," +
                  "PlanAlumnoFechaFin = '" + Utilidades.FecUni(FF.ToString()) + "', ClasesActivas = 0 WHERE UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(sSelect, cn, ref Err, true);

        int cRegulares = 0, cComplement = 0;
        if (dplClientes.SelectedValue == "2")
        {
            cRegulares = 12;
        }
        else
        {
            cRegulares = 8;
        }
        sSelect = " UPDATE PlanAlumnoAcumulado SET disponiblesClasesRegulares = '" + cRegulares + "', totalClasesRegulares = " + cRegulares + ", " +
                   " disponiblesClasesComplemen = '" + cComplement + "', totalClasesComplemen= " + cComplement + ", " +
                   " seleccionoClases = 0 Where UsuarioID = 20";

        Utilidades.EjeSQL(sSelect, cn, ref Err, true);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('closeDeleteA').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);

        BindGridView();
        MostrarMsjModal("Se actualizo el plan para el mes actual con exito", "");
    }

    protected void btnUpdateP_Click(object sender, EventArgs e)
    {
        string UsuarioID = hUsuarioPro.Value;
        string EstadoProximo = hEstatusProx.Value;
        string sSelect = string.Empty;
        if (EstadoProximo == "False")
        {
            sSelect = "UPDATE PlanEmpresa SET EstadoProximo = '1' WHERE UsuarioID = " + UsuarioID;            
            EstadoProximo = "1";
        }
        else
        {
            sSelect = "UPDATE PlanEmpresa SET EstadoProximo = '0' WHERE UsuarioID = " + UsuarioID;
            EstadoProximo = "0";
        }
        Utilidades.EjeSQL(sSelect, cn, ref Err, true);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('closeDeleteP').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);

        BindGridView();
        MostrarMsjModal("Se actualizo el plan para el mes proximo con exito", "");
        
    }

    protected void btnDescargar_Click(object sender, ImageClickEventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        GridView grid = new GridView();
        cn.Open();
        string cmd2 = " SELECT DISTINCT(dbo.Usuario.UsuarioID) as UsuarioID,  " +
                           " (dbo.Usuario.UsuarioNombre+' '+dbo.Usuario.UsuarioApellido) as UsuarioNombre, dbo.Usuario.UsuarioCedula," +
                           " (SELECT ClienteNombre FROM Cliente WHERE ClienteID =  dbo.UsuarioRol.ClienteID) as Empresa," +       
                           " (select COUNT(*) from Reserva WHERE MONTH(FechaReserva) = MONTH(SYSDATETIME()) AND YEAR(FechaReserva) = YEAR(SYSDATETIME()) AND UsuarioId = dbo.Usuario.UsuarioID) as NroReservas," +
                           " (select COUNT(*) from Reserva WHERE MONTH(FechaReserva) = MONTH(SYSDATETIME()) - 1 AND YEAR(FechaReserva) = YEAR(SYSDATETIME()) AND UsuarioId = dbo.Usuario.UsuarioID) as NroReservasAnt," +
                           " CAST(CASE WHEN dbo.PlanEmpresa.PlanActivo = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END as VARCHAR(10)) as EstadoAct, " +
                            " CAST(CASE WHEN dbo.PlanEmpresa.EstadoProximo = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END as VARCHAR(10)) as EstadoSig, " +
                            " (select top 1 p.plannombre from planalumno pa join [plan] p on pa.planID = p.PlanId and pa.UsuarioID = dbo.Usuario.UsuarioID order by pa.PlanAlumnoID desc) as PlanNombre," +
                            " (select top 1 TotalClases from planempresa where usuarioid = dbo.Usuario.UsuarioID order by planempresaid desc) as NroClases" +
                           " FROM  dbo.PlanEmpresa INNER JOIN " +
                           " dbo.Usuario ON dbo.PlanEmpresa.UsuarioID = dbo.Usuario.UsuarioID INNER JOIN " +
                           " dbo.UsuarioRol ON dbo.Usuario.UsuarioID = dbo.UsuarioRol.UsuarioID " +
                           " WHERE dbo.UsuarioRol.ClienteID != 1 " + ViewState["sWhere"] + ViewState["sWhere2"] + ViewState["sWhere3"] +
                           " ORDER BY Empresa ASC";
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte_Usuarios_Estado_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }
}