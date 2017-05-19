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

public partial class sistema_AsignarPlanes : System.Web.UI.Page
{
    string Err = "", sSelectSQL = "";
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string entrada = "", exito = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (entrada == "")
            {
                ViewState["sWhere2"] = " AND PlanAlumno.SaldoNegativo > 0 ";
            }
            exito = Request.QueryString["exito"];
            if (exito == "1") {
                MostrarMsjModal("Se asigno el plan de forma exitosa.", "EXI");
                exito = string.Empty;
            }
            BindGridView();
            string sSelectSQL = "SELECT PlanID AS VAL, PlanNombre AS TXT FROM [Plan] ORDER BY VAL";
            Utilidades.CargarListado(ref dplPlan, sSelectSQL, cn, ref Err, true);
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = " SELECT PlanAlumno.PlanAlumnoID as PlanAlumnoID," +
                        " PlanAlumno.PlanAlumnoFechaInicio as FechaInicio , " +
                        " PlanAlumno.NumeroFactura as NumeroFactura , " +
                        " PlanAlumno.ClasesActivas as ClasesActivas, " +
                        " PlanAlumno.FacturaNota as FacturaNota, " +
                        " PlanAlumno.SaldoPositivo as SaldoPositivo , " +
                        " PlanAlumno.SaldoNegativo as SaldoNegativo , " +
                        " CONVERT(VARCHAR(11),PlanAlumno.PlanAlumnoFechaFin,103) as FechaFinal , " +
                        " PlanAlumno.PlanAlumnoFechaFin as FechaFin , " +
                        " PlanAlumno.UsuarioID as UsuarioID , " +
                        " Usuario.UsuarioCedula as UsuarioCedula, " +
                        " Usuario.UsuarioNombre as UsuarioNombre, " +
                        " Usuario.UsuarioApellido as UsuarioApellido, " +
                        " [Plan].PlanNombre as PlanNombre," +
                        " [Plan].PlanCosto as PlanCosto" +
                        " FROM [Plan] INNER JOIN" +
                        " PlanAlumno ON [Plan].PlanID = PlanAlumno.PlanID INNER JOIN" +
                        " Usuario ON PlanAlumno.UsuarioID = Usuario.UsuarioID " + ViewState["sWhere2"] + ViewState["sWhere"];
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "PlanAlumnoID";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            {
                ViewState["sWhere"] = " AND (Usuario.UsuarioCedula LIKE '%" + sBuscar + "%' OR " +
                                      " Usuario.UsuarioNombre LIKE '%" + sBuscar + "%' OR " +
                                      " Usuario.UsuarioApellido LIKE '%" + sBuscar + "%')";
            }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected void Agregar_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("verBalance"))
        {
            string cedula = (gvrow.FindControl("UsuarioCedula") as Label).Text;
            Response.Redirect("DetallesUsuario.aspx?user=" + cedula);
        }
        if (e.CommandName.Equals("viewRecord"))
        {
            lblNombre.Text = (gvrow.FindControl("UsuarioNombre") as Label).Text;
            lblApellido.Text = (gvrow.FindControl("UsuarioApellido") as Label).Text;
            lblCedula.Text = (gvrow.FindControl("UsuarioCedula") as Label).Text;
            lblPlan.Text = (gvrow.FindControl("PlanNombre") as Label).Text;
            lblFechaInicio.Text = (gvrow.FindControl("PlanAlumnoFechaInicio") as Label).Text;
            lblFechaFin.Text = (gvrow.FindControl("PlanAlumnoFechaFin") as Label).Text;
            lblFactura.Text = (gvrow.FindControl("NumeroFactura") as Label).Text;
            lblNotaFactura.Text = (gvrow.FindControl("FacturaNota") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#viewModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("editRecord"))
        {
            lblNombreEdit.Text = (gvrow.FindControl("UsuarioNombre") as Label).Text;
            lblApellidoEdit.Text = (gvrow.FindControl("UsuarioApellido") as Label).Text;
            lblCedulaEdit.Text = (gvrow.FindControl("UsuarioCedula") as Label).Text;
            lblPlanEdit.Text = (gvrow.FindControl("PlanNombre") as Label).Text;
            lblInicioEdit.Text = (gvrow.FindControl("PlanAlumnoFechaInicio") as Label).Text;
            lblFinEdit.Text = (gvrow.FindControl("PlanAlumnoFechaFin") as Label).Text;
            hdfPlanAlumnoIDEdit.Value = (gvrow.FindControl("PlanAlumnoID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("deleteRecord"))
        {
            hPlanAlumnoID.Value = (gvrow.FindControl("PlanAlumnoID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("agregarPago"))
        {
            txtFacturaNumeroAdd.Text = (gvrow.FindControl("NumeroFactura") as Label).Text;
            string UsuarioID = (gvrow.FindControl("UsuarioID") as Label).Text;
            hdfUsuarioID.Value = (gvrow.FindControl("UsuarioID") as Label).Text;
            txtPlanAlumnoAdd.Text = Utilidades.EjeSQL("SELECT (UsuarioNombre+' '+UsuarioApellido) as Nombre FROM Usuario WHERE UsuarioID = "+hdfUsuarioID.Value, cn, ref Err, true);
            txtPlanAdd.Text = (gvrow.FindControl("PlanNombre") as Label).Text;
            txtDeudaAdd.Text = (gvrow.FindControl("SaldoNegativo") as Label).Text;
            hdfPlanAlumnoID.Value = (gvrow.FindControl("PlanAlumnoID") as Label).Text;
            hdfDeuda.Value = (gvrow.FindControl("SaldoNegativo") as Label).Text;
            hdfPositivo.Value = (gvrow.FindControl("SaldoPositivo") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addPago').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("agregarPagoMatricula")) 
        {
            string AlumnoMatricula =  (gvrow.FindControl("UsuarioNombre") as Label).Text + " " + (gvrow.FindControl("UsuarioApellido") as Label).Text;
            string cedula = (gvrow.FindControl("UsuarioCedula") as Label).Text;
            hdfAlumnoID.Value = (gvrow.FindControl("UsuarioID") as Label).Text;
            txtAlumnoMatricula.Text = cedula+ " "+AlumnoMatricula.ToUpper();
            //Validar si ya ha realizado un pago vigente.
            string []resultado = new string[3];
            resultado = buscarMembresia(cedula);
            if (resultado[0] != null)
            {                
                txtFechaInicioMatricula.Text = resultado[0].ToString();
                txtFechaFinalMatricula.Text = resultado[1].ToString();
                txtPagoMatricula.Text = resultado[2].ToString();
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addPagoMatricula').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }

    private string[] buscarMembresia(string cedula)
    {
        string[] datos = new string[3];
        sSelectSQL = "SELECT TOP 1 Membresia.MembresiaID, " +
                     " CONVERT(varchar(11),Membresia.MembresiaFechaInicio, 103) as FechaInicio, " +
                     " CONVERT(varchar(11),Membresia.MembresiaFechaFin,103) as FechaFin, " +
                     " Membresia.MembresiaPago as Pago "+
                     " FROM Membresia INNER JOIN "+
                     " Usuario ON Membresia.MembresiaUsuarioID = Usuario.UsuarioID "+
                     " WHERE Usuario.UsuarioCedula = '" + cedula + "' ORDER BY Membresia.MembresiaID DESC";
        SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
        SqlDataReader reader;
        try
        {
            cn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                datos[0] = reader["FechaInicio"].ToString();
                datos[1] = reader["FechaFin"].ToString();
                datos[2] = reader["Pago"].ToString();
            }
            reader.Close();
            cn.Close();
            return datos;
        }
        catch (SqlException Sqlex)
        {
            MostrarMsjModal("Error: " + Sqlex.Message, "ERR");
            return null;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
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
        //Guardamos las nuevas Fechas...
        string PlanAlumnoID = hdfPlanAlumnoIDEdit.Value;
        string FechaInicio = lblInicioEdit.Text;
        string FechaFin = lblFinEdit.Text;
        FechaInicio = Utilidades.FecUni(FechaInicio);
        FechaFin = Utilidades.FecUni(FechaFin);
        int iRes = 0;
        cn.Open();
        string cmd2 = "UPDATE PlanAlumno SET PlanAlumnoFechaInicio = " + Utilidades.SiEsNulo(FechaInicio, "F") +
                      ", PlanAlumnoFechaFin = " + Utilidades.SiEsNulo(FechaFin, "F") +
                      " WHERE PlanAlumnoID = " + PlanAlumnoID;
        SqlCommand addCmd = new SqlCommand(cmd2, cn);
        try { iRes = addCmd.ExecuteNonQuery(); }
        catch (SqlException sq) { Err = sq.Message; }
        cn.Close();
        if (iRes > 0)
        {
            BindGridView();
            MostrarMsjModal("Registro Editado exitosamente", "EXI");
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int iRes = 0;
        string PlanAlumnoID = hPlanAlumnoID.Value;
        /*cn.Open();
        string SLQ_2 = "ALTER TABLE PlanPago nocheck CONSTRAINT FK_PlanPago_PlanAlumno ";
        SqlCommand addCmd2 = new SqlCommand(SLQ_2, cn);
        int iRes2 = addCmd2.ExecuteNonQuery();
        cn.Close();*/
        cn.Open();
        string SQL_1 = "DELETE FROM PlanAlumno Where PlanAlumnoID = " + PlanAlumnoID;
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
            /*cn.Open();
            string SLQ_23 = "ALTER TABLE PlanPago check CONSTRAINT FK_PlanPago_PlanAlumno ";
            SqlCommand addCmd23 = new SqlCommand(SLQ_23, cn);
            int iRes23 = addCmd23.ExecuteNonQuery();
            cn.Close();*/
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
            MostrarMsjModal("No se puede Eliminar Debido a que Tiene Pagos Asociados " + Err, "ERR");
        }
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

    protected void BindGridView2(string dato)
    {
        string condicion = "";
        if (dato != null)
        {
            //condicion = " AND (ur.ClienteID = (Select c.ClienteID as Cliente FROM Cliente c Where c.ClienteNombre LIKE '%"+dato+"%') OR us.UsuarioCedula LIKE '%" + dato + "%' OR us.UsuarioNombre LIKE '%" + dato + "%')";
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

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView2.Rows[index];
        //DateTime FechaActual = System.DateTime.Now;
        DateTime FechaActual = DateTime.Today.Date;
        if (e.CommandName.Equals("selectRecord"))
        {
            txtUsuario.Text = (gvrow.FindControl("UsuarioNombreB") as Label).Text + " " + (gvrow.FindControl("UsuarioApellidoB") as Label).Text;
            hdfUsuarioID.Value = (gvrow.FindControl("UsuarioIDB") as Label).Text;
            hdfSucursalID.Value = (gvrow.FindControl("SucursalIDB") as Label).Text;
            hdfClienteID.Value = (gvrow.FindControl("ClienteIDB") as Label).Text;
            string FechaUltimaUsuario = "";
            sSelectSQL = "SELECT MAX(PlanAlumnoFechaFin) as MAXIMO FROM PlanAlumno Where UsuarioID = " + hdfUsuarioID.Value;
            Utilidades.maxRegistro(ref FechaUltimaUsuario, sSelectSQL, cn, ref Err);
            lblFechaVencimiento.Text = Utilidades.EjeSQL("SELECT CONVERT(VARCHAR(11), PlanAlumnoFechaFin, 103) as Fecha FROM PlanAlumno WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PLANALUMNOID DESC", cn, ref Err, true);
            string rt = Utilidades.EjeSQL("SELECT totalClasesRegulares FROM PlanAlumnoAcumulado WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlaAlumnoAcumuladoID DESC", cn, ref Err, true);
            string rd = Utilidades.EjeSQL("SELECT disponiblesClasesRegulares FROM PlanAlumnoAcumulado WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlaAlumnoAcumuladoID DESC", cn, ref Err, true);
            string ct = Utilidades.EjeSQL("SELECT totalClasesComplemen FROM PlanAlumnoAcumulado WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlaAlumnoAcumuladoID DESC", cn, ref Err, true);
            string cd = Utilidades.EjeSQL("SELECT disponiblesClasesComplemen FROM PlanAlumnoAcumulado WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlaAlumnoAcumuladoID DESC", cn, ref Err, true);
            lblClasesRegulares.Text = "Total: " + rt + ", Disponibles: " + rd;
            lblClasesComplemen.Text = "Total: " + ct + ", Disponibles: " + cd;
            if (FechaUltimaUsuario != "")
            {

                DateTime fechaUlt = Convert.ToDateTime(FechaUltimaUsuario);
                if (fechaUlt >= FechaActual)
                {
                    txtFechaInicio.Text = fechaUlt.ToString("dd/MM/yyyy");
                    hdfFechaINI.Value = fechaUlt.ToString("MM/dd/yyyy");
                    txtFechaFin.Text = fechaUlt.AddMonths(1).Date.ToString("dd/MM/yyyy");
                    hdfFechaFIN.Value = fechaUlt.AddMonths(1).Date.ToString("MM/dd/yyyy");
                }
                else
                {
                    txtFechaInicio.Text = FechaActual.ToString("dd/MM/yyyy");
                    hdfFechaINI.Value = FechaActual.ToString("MM/dd/yyyy");
                    txtFechaFin.Text = FechaActual.AddMonths(1).Date.ToString("dd/MM/yyyy");
                    hdfFechaFIN.Value = FechaActual.AddMonths(1).Date.ToString("MM/dd/yyyy");
                }
            }
            else
            {
                txtFechaInicio.Text = FechaActual.ToString("dd/MM/yyyy");
                hdfFechaINI.Value = FechaActual.ToString("MM/dd/yyyy");
                txtFechaFin.Text = FechaActual.AddMonths(1).Date.ToString("dd/MM/yyyy");
                hdfFechaFIN.Value = FechaActual.AddMonths(1).Date.ToString("MM/dd/yyyy");
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('bscModal').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Guardamos el Plan-Alumno
        int iRes = 0;
        string ClienteID = hdfClienteID.Value;
        if (ClienteID != "1")
        {
            //Asignamos a Alumno Empresa..
            //1. Asignar en PlanAlumno
            int PlanAlumnoID = int.Parse(Utilidades.EjeSQL("SELECT MAX(PlanAlumnoID) FROM PlanAlumno", cn, ref Err, true)) + 1;
            string FechaInicio = txtFechaInicio.Text;
            string FechaFin = txtFechaFin.Text;
            string PlanID = dplPlan.SelectedValue;
            string UsuarioID = hdfUsuarioID.Value;
            string saldoPositivo = Utilidades.EjeSQL("SELECT PlanCosto FROM [Plan] WHERE PlanID = " + PlanID, cn, ref Err, true);
            string clasesRegulares = Utilidades.EjeSQL("SELECT clasesRegulares FROM [Plan] WHERE PlanID = " + PlanID, cn, ref Err, true);
            string clasesComplemen = Utilidades.EjeSQL("SELECT clasesComplemen FROM [Plan] WHERE PlanID = " + PlanID, cn, ref Err, true);
            sSelectSQL = "INSERT INTO PlanAlumno(PlanAlumnoID, UsuarioID, SucursalID, PlanId, PlanAlumnoFechaInicio," +
                        " PlanAlumnoFechaFin, PlanAlumnoFechaRegistro, PlanAlumnoUsuarioRegistro,ClienteID,SaldoPositivo," +
                        " SaldoNegativo, NumeroFactura, ClasesActivas, FacturaNota) VALUES " +
                        " (" + PlanAlumnoID + ", " + UsuarioID + ", 1, " + PlanID + ", Convert(datetime, '" + FechaInicio + "', 103),  " +
                        " Convert(datetime, '" + FechaFin + "', 103), SYSDATETIME(), '1', " + ClienteID + ", " + saldoPositivo + ", 0, 'F_0202', 0, NULL )";
            Err = "";
            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
            if (Err == "")
            {
                //Buscamos si tiene registro
                sSelectSQL = "SELECT PlaAlumnoAcumuladoID FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioID;
                string PlanALumnoAcumuladoID = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
                if (PlanALumnoAcumuladoID == "-1")
                {
                    //Insertamos el planalumnoacumulado
                    Err = "";
                    sSelectSQL = "INSERT INTO PLANALUMNOACUMULADO (totalClasesRegulares, disponiblesclasesregulares, usuarioid," +
                                " seleccionoclases, totalclasescomplemen, disponiblesclasescomplemen) " +
                                " values (" + Utilidades.SiEsNulo(clasesRegulares, "N") + "," +
                                Utilidades.SiEsNulo(clasesRegulares, "T") + "," + UsuarioID + ",0," + Utilidades.SiEsNulo(clasesComplemen, "N") +
                                "," + Utilidades.SiEsNulo(clasesComplemen, "T") + ")";
                    Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                    if (Err == "")
                    {
                        //Insertamos en PLanAutorizacion
                        sSelectSQL = " INSERT INTO PlanAutorizacion (AutorizacionActivo, FechaRegistro, UsuarioID, CorreoEnviado) " +
                                     " VALUES (1, SYSDATETIME(), " + UsuarioID + ", 1)";
                        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                        if (Err == "")
                        {
                            //Insertamos en planempresa
                            DateTime MesProximo = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1);
                            sSelectSQL = " INSERT INTO PLANEMPRESA (USUARIOID, PLANACTIVO, FECHAULTIMO, TOTALCLASES, MESPROXIMO, ESTADOPROXIMO) VALUES  " +
                                        " (" + UsuarioID + ", 0, CONVERT(DATE, SYSDATETIME(),103), " + clasesRegulares + ",CONVERT(DATE,'" + MesProximo.ToString("d") + "',103), 0)";
                            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                            sSelectSQL = " INSERT INTO PLANEMPRESAHISTORIAL (PLANEMPRESAUSUARIOID, PLANEMPRESAPLANESTADO, PLANEMPRESAFECHA, PLANEMPRESATOTALCLASES) VALUES  " +
                                        " (" + UsuarioID + ", 0, CONVERT(DATE, SYSDATETIME(),103), " + clasesRegulares + ")";
                            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                            if (Err == "")
                            {
                                BindGridView();
                                limpiarCampos();
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
                            MostrarMsjModal("Error: " + Err, "ERR");
                        }
                    }
                    else
                    {
                        MostrarMsjModal("Error: " + Err, "ERR");
                    }
                }
                else
                {
                    //Actualizamos todos los campos 
                    int TotalRegulares, DisponiblesReg, TotalComple, DisponiblesCom;
                    if (dplAcumulado.SelectedValue == "SI")
                    {
                        //ACUMULAR
                        string claseRegAcum = Utilidades.EjeSQL("SELECT totalClasesRegulares FROM PlanAlumnoAcumulado WHERE plaAlumnoAcumuladoID = " + PlanALumnoAcumuladoID, cn, ref Err, true);
                        string claseRegDisAcum = Utilidades.EjeSQL("SELECT disponiblesClasesRegulares FROM PlanAlumnoAcumulado WHERE plaAlumnoAcumuladoID = " + PlanALumnoAcumuladoID, cn, ref Err, true);
                        string claseComAcum = Utilidades.EjeSQL("SELECT totalClasesComplemen FROM PlanAlumnoAcumulado WHERE plaAlumnoAcumuladoID = " + PlanALumnoAcumuladoID, cn, ref Err, true);
                        string claseComDisAcum = Utilidades.EjeSQL("SELECT disponiblesClasesComplemen FROM PlanAlumnoAcumulado WHERE plaAlumnoAcumuladoID = " + PlanALumnoAcumuladoID, cn, ref Err, true);
                        TotalRegulares = int.Parse(claseRegAcum) + int.Parse(clasesRegulares);
                        DisponiblesReg = int.Parse(claseRegDisAcum) + int.Parse(clasesRegulares);
                        TotalComple = int.Parse(claseComAcum) + int.Parse(clasesComplemen);
                        DisponiblesCom = int.Parse(claseRegDisAcum) + int.Parse(clasesComplemen);
                    }
                    else
                    {
                        //LIMPIAR
                        TotalRegulares = int.Parse(clasesRegulares);
                        DisponiblesReg = int.Parse(clasesRegulares);
                        TotalComple = int.Parse(clasesComplemen);
                        DisponiblesCom = int.Parse(clasesComplemen);
                    }
                    sSelectSQL = "UPDATE PlanAlumnoAcumulado SET totalClasesRegulares = " + TotalRegulares + "," +
                                 " disponiblesClasesRegulares = '" + DisponiblesReg + "', " +
                                 " totalClasesComplemen = " + TotalComple + "," +
                                 " seleccionoClases = 0," +
                                 " disponiblesClasesComplemen = '" + DisponiblesCom + "' WHERE " +
                                 " plaAlumnoAcumuladoID = " + PlanALumnoAcumuladoID + "";
                    Err = "";
                    Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                    if (Err == "")
                    {
                        if (Err == "")
                        {
                            sSelectSQL = "SELECT PlanAutorizacionID FROM PlanAutorizacion WHERE UsuarioID = " + UsuarioID;
                            string PlanAutorizacion = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
                            if (PlanAutorizacion.Length > 0)
                            {
                                //Ya tienen insertado el Plan
                                BindGridView();
                                limpiarCampos();
                                MostrarMsjModal("Registro Modificado exitosamente", "EXI");
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("document.getElementById('closeAdd').click();");
                                sb.Append(@"</script>");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
                            }
                            else
                            {
                                Err = "";
                                sSelectSQL = "insert into planautorizacion (autorizacionactivo, fecharegistro, usuarioid, correoenviado)" +
                                            " values (0, sysdatetime(), " + UsuarioID + ", 0)";
                                Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                                if (Err == "")
                                {
                                    BindGridView();
                                    limpiarCampos();
                                    MostrarMsjModal("Registro Modificado exitosamente", "EXI");
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    sb.Append(@"<script type='text/javascript'>");
                                    sb.Append("document.getElementById('closeAdd').click();");
                                    sb.Append(@"</script>");
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
                                }
                            }
                        }

                    }
                    else
                    {
                        MostrarMsjModal("Error: " + Err, "ERR");
                    }
                }
            }
            else
            {
                MostrarMsjModal("Error: " + Err, "ERR");
            }
        }
        else
        {
            string SucursalID = hdfSucursalID.Value;
            string UsuarioID = hdfUsuarioID.Value;
            string NumeroFactura = txtFacturaNumeroAsignar.Text;
            string FacturaNota = txtFacturaNotaAdd.Text;
            //Buscar el Ultimo ID de PlanAlumno
            int PlanAlumnoID = 0;
            string maxPlanAlumno = "";
            sSelectSQL = "SELECT MAX(PlanAlumnoID) as MAXIMO FROM PlanAlumno";
            Utilidades.maxRegistro(ref maxPlanAlumno, sSelectSQL, cn, ref Err);
            if (maxPlanAlumno == "") { PlanAlumnoID = 1; }
            else
            {
                PlanAlumnoID = int.Parse(maxPlanAlumno.Trim()) + 1;
            }
            // string FechaInicio = hdfFechaINI.Value;
            // string FechaFin = hdfFechaFIN.Value;
            string FechaInicio = txtFechaInicio.Text;
            string FechaFin = txtFechaFin.Text;
            //MostrarMsjModal("FI: " + FechaInicio + " FF: " + FechaFin, "");
            string PlanID = dplPlan.SelectedValue;
            //Buscamos la cantidad de clases del plan seleccionado
            string CantidadClasesComplemen = "0", CantidadClasesRegulares = "0"; // cc2 = "";
            sSelectSQL = "Select clasesComplemen as MAXIMO from [Plan] Where PlanID = " + PlanID;
            Utilidades.maxRegistro(ref CantidadClasesComplemen, sSelectSQL, cn, ref Err);
            sSelectSQL = "Select clasesRegulares as MAXIMO from [Plan] Where PlanID = " + PlanID;
            Utilidades.maxRegistro(ref CantidadClasesRegulares, sSelectSQL, cn, ref Err);
            string saldoPositivo = "0";
            string saldoNegativo = "";
            sSelectSQL = "SELECT PlanCosto as MAXIMO FROM [Plan] Where PlanID = " + PlanID;
            Utilidades.maxRegistro(ref saldoNegativo, sSelectSQL, cn, ref Err);
            double saldoN = double.Parse(saldoNegativo);
            FechaInicio = Utilidades.FecUni(FechaInicio);
            FechaFin = Utilidades.FecUni(FechaFin);
            //FechaInicio = Utilidades.EjeSQL("select convert(varchar(24) ,'" + FechaInicio + "', 103)", cn, ref Err, true);
            // FechaFin = Utilidades.EjeSQL("select convert(varchar(24) ,'" + FechaFin + "', 103)", cn, ref Err, true);
            if (!Utilidades.EsFecha(FechaInicio) || !Utilidades.EsFecha(FechaFin))
            {
                if (txtUsuario.Text != "" && dplPlan.SelectedValue != "" && NumeroFactura != "")
                {
                    cn.Close();
                    cn.Open();
                    sSelectSQL = "INSERT INTO PlanAlumno" +
                                "(PlanAlumnoID, UsuarioID, SucursalID, PlanID, PlanAlumnoFechaInicio, PlanAlumnoFechaFin " +
                                ",PlanAlumnoFechaRegistro" +
                                ",PlanAlumnoUsuarioRegistro,ClienteID, SaldoPositivo, SaldoNegativo, NumeroFactura, ClasesActivas, FacturaNota)" +
                                "VALUES(" + PlanAlumnoID + "," + Utilidades.SiEsNulo(UsuarioID, "N") + ", " + Utilidades.SiEsNulo(SucursalID, "N") + ", " + Utilidades.SiEsNulo(PlanID, "N") + ", CONVERT(DATETIME,'" + FechaInicio + "',103)" +
                                ", CONVERT(DATETIME,'" + FechaFin + "',103),SYSDATETIME(), 1, " + Utilidades.SiEsNulo(ClienteID, "N") +
                                "," + Utilidades.SiEsNulo(saldoPositivo, "T") + "," + saldoN + ", " + Utilidades.SiEsNulo(NumeroFactura, "T") +
                                ", 0," + Utilidades.SiEsNulo(FacturaNota, "T") + ")";

                    SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
                    try { iRes = addCmd.ExecuteNonQuery(); cn.Close(); }
                    catch (SqlException sq) { Err = sq.Message; MostrarMsjModal(Err, "ERR"); cn.Close(); }

                    if (iRes > 0)
                    {
                        limpiarCampos();
                        //Insertamos en la tabla de acumulado
                        cn.Open();
                        sSelectSQL = "SELECT * FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioID;
                        SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        try
                        {
                            if (dr.Read())
                            {
                                string totalClasesRegulares = dr["totalClasesRegulares"].ToString();
                                string disponiblesClasesRegulares = dr["disponiblesClasesRegulares"].ToString();
                                string totalClasesComplemen = dr["totalClasesComplemen"].ToString();
                                string disponiblesClasesComplemen = dr["disponiblesClasesComplemen"].ToString();
                                //MostrarMsjModal(totalClasesRegulares + " " + disponiblesClasesRegulares + " " + totalClasesComplemen + " " + disponiblesClasesComplemen, "");
                                //Insertar registro en PlanAlumnoAcumulado
                                int totalR, disponR, totalC, disponC;
                                if (dplAcumulado.SelectedValue == "SI")
                                {
                                    totalR = int.Parse(totalClasesRegulares.Trim()) + int.Parse(CantidadClasesRegulares.Trim());
                                    disponR = int.Parse(disponiblesClasesRegulares.Trim()) + int.Parse(CantidadClasesRegulares.Trim());
                                    totalC = int.Parse(totalClasesComplemen.Trim()) + int.Parse(CantidadClasesComplemen.Trim());
                                    disponC = int.Parse(disponiblesClasesComplemen.Trim()) + int.Parse(CantidadClasesComplemen.Trim());
                                }
                                else
                                {
                                    totalR = int.Parse(CantidadClasesRegulares.Trim());
                                    disponR = int.Parse(CantidadClasesRegulares.Trim());
                                    totalC = int.Parse(CantidadClasesComplemen.Trim());
                                    disponC = int.Parse(CantidadClasesComplemen.Trim());
                                }
                                dr.Close();
                                cn.Close();
                                cn.Open();
                                sSelectSQL = "UPDATE PlanAlumnoAcumulado SET totalClasesRegulares = " + totalR + " ," +
                                             " disponiblesClasesRegulares = '" + disponR + "', totalClasesComplemen = " + totalC +
                                             " , disponiblesClasesComplemen = '" + disponC + "' WHERE UsuarioID = " + UsuarioID;
                                SqlCommand addCmd2 = new SqlCommand(sSelectSQL, cn);
                                try
                                {
                                    int iRes2 = addCmd2.ExecuteNonQuery();
                                }
                                catch (SqlException sq)
                                {
                                    MostrarMsjModal(sq.Message, "ERR");
                                }
                                cn.Close();
                            }
                            else
                            {
                                cn.Close();
                                cn.Open();

                                sSelectSQL = "INSERT INTO PlanAlumnoAcumulado (totalClasesRegulares, disponiblesClasesRegulares, UsuarioID, seleccionoClases, totalClasesComplemen, disponiblesClasesComplemen)" +
                                             " VALUES (" + Utilidades.SiEsNulo(CantidadClasesRegulares, "N") + ", " + Utilidades.SiEsNulo(CantidadClasesRegulares, "T") + ", " + Utilidades.SiEsNulo(UsuarioID, "N") + ", 1, " + Utilidades.SiEsNulo(CantidadClasesComplemen, "T") + ", " + Utilidades.SiEsNulo(CantidadClasesComplemen, "N") + ")";

                                SqlCommand addCmd2 = new SqlCommand(sSelectSQL, cn);
                                try { int iRes3 = addCmd2.ExecuteNonQuery(); }
                                catch (SqlException sq) { MostrarMsjModal(sq.Message, "ERR"); }
                                cn.Close();
                            }
                        }
                        catch (SqlException sq)
                        {
                            MostrarMsjModal(sq.Message, "ERR");
                        }
                        cn.Close();
                        BindGridView();
                        limpiarCampos();
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
                    MostrarMsjModal("Debe ingresar el Usuario y Seleccionar un Plan", "ERR");
                }
            }
            else
            {
                MostrarMsjModal(Err, "ERR");
            }
        }
    }

    protected void limpiarCampos()
    {
        dplPlan.SelectedValue = "";
        txtFechaInicio.Text = "";
        txtFechaFin.Text = "";
        txtUsuario.Text = "";
        txtFacturaNumeroAsignar.Text = "";
        txtMontoAdd.Text = "";
        txtFechaFacturaAdd2.Text = "";
    }

    protected void Pagar_Click(object sender, EventArgs e)
    {
        try
        {
            int iRes = 0, iRes2 = 0;
            double deudaAnt;
            string PlanAlumnoID = hdfPlanAlumnoID.Value;
            string NumeroFactura = txtFacturaNumeroAdd.Text;
            string FechaFactura = txtFechaFacturaAdd2.Text;
            string Monto = txtMontoAdd.Text;
            string Positivo = hdfPositivo.Value;
            string Deuda = hdfDeuda.Value;
            string UsuarioID = hdfUsuarioID.Value;
            string DeudaAnterior = "";
            double descuento = 0;
            double MontoNuevo = double.Parse(Monto);
            sSelectSQL = "Select SaldoNegativo as MAXIMO From PlanALumno Where PlanAlumnoID < " + PlanAlumnoID + " AND UsuarioID = " + UsuarioID;
            Utilidades.maxRegistro(ref DeudaAnterior, sSelectSQL, cn, ref Err);
            if (ddlDescuento.SelectedValue != "NO")
            {
                descuento = double.Parse(txtDescuento.Text.Trim());
                if (ddlDescuento.SelectedValue == "PORC")
                {
                    MontoNuevo = MontoNuevo + (double.Parse(Deuda) * (descuento / 100));
                }
                else if (ddlDescuento.SelectedValue == "MONT")
                {
                    MontoNuevo = MontoNuevo + descuento;
                }
            }
            if (DeudaAnterior != "")
            {
                deudaAnt = double.Parse(DeudaAnterior);
                if (deudaAnt == 0)
                {
                    if (MontoNuevo > double.Parse(Deuda))
                    {
                        MostrarMsjModal("El monto ingresado supera la Deuda", "ERR");
                    }
                    else
                    {
                        //FechaFactura = Utilidades.FecUni(FechaFactura);
                        //Buscar el Ultimo ID de PlanAlumno
                        int ClasePagoID = 0;
                        string maxClasePagoID = "";
                        sSelectSQL = "SELECT MAX(ClasePagoID) as MAXIMO FROM PlanPago";
                        Utilidades.maxRegistro(ref maxClasePagoID, sSelectSQL, cn, ref Err);
                        if (maxClasePagoID == "") { ClasePagoID = 1; }
                        else
                        {
                            ClasePagoID = int.Parse(maxClasePagoID.Trim()) + 1;
                        }
                        if (sonValidos("Pagos_Insert"))
                        {
                            cn.Open();
                            sSelectSQL = "INSERT INTO PlanPago" +
                                        "(ClasePagoID, PlanAlumnoID, FacturaNumero, FacturaFecha, FacturaMonto, PlanPagoFechaRegistro, PlanPagoUsuarioRegistro) " +
                                        " VALUES(" + ClasePagoID + "," + Utilidades.SiEsNulo(PlanAlumnoID, "N") + ", " + Utilidades.SiEsNulo(NumeroFactura, "T") +
                                        ", CONVERT(DATETIME, '" + FechaFactura + "', 103)," + Utilidades.SiEsNulo(MontoNuevo.ToString(), "N") +
                                        ",SYSDATETIME() , 1)";

                            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
                            try
                            {
                                iRes = addCmd.ExecuteNonQuery();
                            }
                            catch (SqlException sq) { MostrarMsjModal(sq.Message, "ERR"); }
                            cn.Close();
                            if (iRes > 0)
                            {
                                limpiarCampos();
                                double deudaNueva = (double.Parse(Deuda) - MontoNuevo);
                                double positivoNuevo = (double.Parse(Positivo) + MontoNuevo);
                                cn.Open();
                                sSelectSQL = "UPDATE PlanAlumno SET SaldoPositivo = " + positivoNuevo + " , SaldoNegativo = " + deudaNueva +
                                             " WHERE PlanAlumnoID = " + PlanAlumnoID;
                                SqlCommand addCmd2 = new SqlCommand(sSelectSQL, cn);
                                try { iRes2 = addCmd2.ExecuteNonQuery(); }
                                catch (SqlException sq) { Err = sq.Message; }
                                cn.Close();
                                if (iRes2 > 0)
                                {
                                    BindGridView();
                                    MostrarMsjModal("Registro Agregado exitosamente", "EXI");
                                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                    sb.Append(@"<script type='text/javascript'>");
                                    sb.Append("document.getElementById('closeAddPago').click();");
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
                        else
                        {
                            MostrarMsjModal(Err, "ERR");
                        }
                    }
                }
                else
                {
                    MostrarMsjModal("Debe cancelar su Plan y/o Factura anterior", "ERR");
                }
            }
            else
            {
                if (MontoNuevo > double.Parse(Deuda))
                {
                    MostrarMsjModal("El monto ingresado supera la Deuda", "ERR");
                }
                else
                {
                    //FechaFactura = Utilidades.FecUni(FechaFactura);
                    //Buscar el Ultimo ID de PlanAlumno
                    int ClasePagoID = 0;
                    string maxClasePagoID = "";
                    sSelectSQL = "SELECT MAX(ClasePagoID) as MAXIMO FROM PlanPago";
                    Utilidades.maxRegistro(ref maxClasePagoID, sSelectSQL, cn, ref Err);
                    if (maxClasePagoID == "") { ClasePagoID = 1; }
                    else
                    {
                        ClasePagoID = int.Parse(maxClasePagoID.Trim()) + 1;
                    }
                    if (sonValidos("Pagos_Insert"))
                    {
                        cn.Open();
                        sSelectSQL = "INSERT INTO PlanPago" +
                                    "(ClasePagoID, PlanAlumnoID, FacturaNumero, FacturaFecha, FacturaMonto, PlanPagoFechaRegistro, PlanPagoUsuarioRegistro) " +
                                    " VALUES(" + ClasePagoID + "," + Utilidades.SiEsNulo(PlanAlumnoID, "N") + ", " + Utilidades.SiEsNulo(NumeroFactura, "T") +
                                    ", CONVERT(DATETIME, '" + FechaFactura + "', 103)," + Utilidades.SiEsNulo(MontoNuevo.ToString(), "N") +
                                    ",SYSDATETIME() , 1)";
                        //MostrarMsjModal(sSelectSQL, "");
                        SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
                        try { iRes = addCmd.ExecuteNonQuery(); }
                        catch (SqlException sq) { Err = sq.Message; }
                        cn.Close();
                        if (iRes > 0)
                        {
                            limpiarCampos();
                            double deudaNueva = (double.Parse(Deuda) - MontoNuevo);
                            double positivoNuevo = (double.Parse(Positivo) + MontoNuevo);
                            cn.Open();
                            sSelectSQL = "UPDATE PlanAlumno SET SaldoPositivo = " + positivoNuevo + " , SaldoNegativo = " + deudaNueva +
                                         " WHERE PlanAlumnoID = " + PlanAlumnoID;
                            SqlCommand addCmd2 = new SqlCommand(sSelectSQL, cn);
                            try { iRes2 = addCmd2.ExecuteNonQuery(); }
                            catch (SqlException sq) { Err = sq.Message; }
                            cn.Close();
                            if (iRes2 > 0)
                            {
                                BindGridView();
                                MostrarMsjModal("Registro Agregado exitosamente", "EXI");
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append(@"<script type='text/javascript'>");
                                sb.Append("document.getElementById('closeAddPago').click();");
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
                    else
                    {
                        MostrarMsjModal(Err, "ERR");
                    }
                }
            }
            limpiarCampos();
        }
        catch (Exception ex)
        {
            MostrarMsjModal("Error: " + ex.Message, "ERR");
        }

    }

    private bool sonValidos(string Accion)
    {
        bool bRes = true;
        string FechaFin = "";
        string FacturaNum = "";
        string Monto = "";

        if (Accion == "Pagos_Insert")
        {
            FacturaNum = txtFacturaNumeroAdd.Text;
            FechaFin = txtFechaFacturaAdd2.Text;
            Monto = txtMontoAdd.Text;

            if (FechaFin == "") { Err += "Debe ingresar una Fecha<br/>"; bRes = false; }
            if (FacturaNum == "") { Err += "Debe ingresar el Número de la Factura <br/>"; bRes = false; }
            if (Monto == "") { Err += "Debe ingresar el monto de la Factura <br/>"; bRes = false; }
        }

        return bRes;
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.EditIndex = -1;
        GridView2.SelectedIndex = -1;
        GridView2.PageIndex = e.NewPageIndex;
        string datoConsulta = txtUsuario.Text;
        BindGridView2(datoConsulta);
    }

    protected void dplFiltroAsignacion_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (dplFiltroAsignacion.SelectedValue == "cancelados")
        {
            ViewState["sWhere2"] = "  AND PlanAlumno.SaldoNegativo = 0 ";
            entrada = "1";
        }
        else if (dplFiltroAsignacion.SelectedValue == "abonados")
        {
            ViewState["sWhere2"] = " AND PlanAlumno.SaldoPositivo > 0 AND PlanAlumno.SaldoNegativo != 0";
            entrada = "1";
        }
        else if (dplFiltroAsignacion.SelectedValue == "sinAbonar")
        {
            ViewState["sWhere2"] = " AND PlanAlumno.SaldoPositivo = 0 ";
            entrada = "1";
        }
        else
        {
            ViewState["sWhere2"] = " AND PlanAlumno.SaldoNegativo > 0 ";
        }
        BindGridView();
    }

    protected void dplAcumulado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplAcumulado.SelectedValue != "")
        {
            if (dplAcumulado.SelectedValue == "SI")
            {
                phAcumulado.Visible = true;
            }
            else
            {
                phAcumulado.Visible = false;
            }
        }
    }

    protected void PagarMatricula_Click(object sender, EventArgs e)
    {
        string Documento = Utilidades.EjeSQL("Select UsuarioCedula From Usuario WHERE UsuarioID = " + hdfAlumnoID.Value, cn, ref Err, true);
        sSelectSQL = " INSERT INTO Membresia (MembresiaUsuarioID, MembresiaFechaInicio," +
                     " MembresiaFechaFin, MembresiaDocumento, MembresiaPago, MembresiaObservacion, MembresiaFechaRegistro) VALUES( " +
                     " " + hdfAlumnoID.Value + ", CONVERT(DATE, '" + txtFechaInicioMatricula.Text + "', 103), CONVERT(DATE, '" + txtFechaFinalMatricula.Text + "', 103), '" + Documento + "', " +
                     " '" + txtPagoMatricula.Text + "', '', SYSDATETIME()) ";
        Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);

        BindGridView();
        MostrarMsjModal("Membresia guardada exitosamente", "EXI");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('closeMat').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
    }

    protected void ddlDescuento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDescuento.SelectedValue != "NO")
        {
            phDescuento.Visible = true;
        }
        else 
        {
            phDescuento.Visible = false;
        }
    }
}