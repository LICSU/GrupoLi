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

public partial class sistema_AsignarPlan : System.Web.UI.Page
{
    string Err = "", sSelectSQL = "";
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string entrada = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string sSelectSQL = "SELECT PlanID AS VAL, PlanNombre AS TXT FROM [Plan] WHERE PlanActivo = 1 ORDER BY VAL";
            Utilidades.CargarListado(ref dplPlan, sSelectSQL, cn, ref Err, true);
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

    protected void dplAcumuladoC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAcumuladoC.SelectedValue != "")
        {
            if (ddlAcumuladoC.SelectedValue == "SI")
            {
                phAcumuladoC.Visible = true;
            }
            else
            {
                phAcumuladoC.Visible = false;
            }
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
                if (PlanALumnoAcumuladoID == "-1" || PlanALumnoAcumuladoID == "")
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
                                        " (" + UsuarioID + ", 1, CONVERT(DATE, SYSDATETIME(),103), " + clasesRegulares + ",CONVERT(DATE,'" + MesProximo.ToString("d") + "',103), 1)";
                            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                            sSelectSQL = " INSERT INTO PLANEMPRESAHISTORIAL (PLANEMPRESAUSUARIOID, PLANEMPRESAPLANESTADO, PLANEMPRESAFECHA, PLANEMPRESATOTALCLASES) VALUES  " +
                                        " (" + UsuarioID + ", 1, CONVERT(DATE, SYSDATETIME(),103), " + clasesRegulares + ")";
                            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                            if (Err == "")
                            {
                                Response.Redirect("AsignarPlanes.aspx?exito=1");
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
                        TotalRegulares = int.Parse(claseRegAcum) + int.Parse(clasesRegulares);
                        DisponiblesReg = int.Parse(claseRegDisAcum) + int.Parse(clasesRegulares);                        
                    }
                    else
                    {
                        TotalRegulares = int.Parse(clasesRegulares);
                        DisponiblesReg = int.Parse(clasesRegulares);
                    }

                    if(ddlAcumuladoC.SelectedValue == "SI")
                    {
                        string claseComAcum = Utilidades.EjeSQL("SELECT totalClasesComplemen FROM PlanAlumnoAcumulado WHERE plaAlumnoAcumuladoID = " + PlanALumnoAcumuladoID, cn, ref Err, true);
                        string claseComDisAcum = Utilidades.EjeSQL("SELECT disponiblesClasesComplemen FROM PlanAlumnoAcumulado WHERE plaAlumnoAcumuladoID = " + PlanALumnoAcumuladoID, cn, ref Err, true);
                        TotalComple = int.Parse(claseComAcum) + int.Parse(clasesComplemen);
                        DisponiblesCom = int.Parse(claseComDisAcum) + int.Parse(clasesComplemen);
                    }
                    else
                    {
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
            string FechaInicio = txtFechaInicio.Text;
            string FechaFin = txtFechaFin.Text;
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
                        //limpiarCampos();
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
                                int totalR = 0, disponR = 0, totalC = 0, disponC = 0;
                                if (dplAcumulado.SelectedValue == "SI")
                                {
                                    totalR = int.Parse(totalClasesRegulares.Trim()) + int.Parse(CantidadClasesRegulares.Trim());
                                    disponR = int.Parse(disponiblesClasesRegulares.Trim()) + int.Parse(CantidadClasesRegulares.Trim());                                    
                                }
                                if(ddlAcumuladoC.SelectedValue == "SI")
                                {
                                    totalC = int.Parse(totalClasesComplemen.Trim()) + int.Parse(CantidadClasesComplemen.Trim());
                                    disponC = int.Parse(disponiblesClasesComplemen.Trim()) + int.Parse(CantidadClasesComplemen.Trim());
                                }
                                if (dplAcumulado.SelectedValue == "NO")
                                {
                                    totalR = int.Parse(CantidadClasesRegulares.Trim());
                                    disponR = int.Parse(CantidadClasesRegulares.Trim());                                    
                                }
                                if (ddlAcumuladoC.SelectedValue == "NO")
                                {
                                    totalC = int.Parse(CantidadClasesComplemen.Trim());
                                    disponC = int.Parse(CantidadClasesComplemen.Trim());
                                }
                                dr.Close();
                                cn.Close();
                                cn.Open();
                                sSelectSQL = " UPDATE PlanAlumnoAcumulado SET totalClasesRegulares = " + totalR + " ," +
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
                        if (ddlMatricula.SelectedValue == "SI")
                        {
                            string Documento = Utilidades.EjeSQL("Select UsuarioCedula From Usuario WHERE UsuarioID = " + UsuarioID, cn, ref Err, true);
                            sSelectSQL = " INSERT INTO Membresia (MembresiaUsuarioID, MembresiaFechaInicio," +
                                         " MembresiaFechaFin, MembresiaDocumento, MembresiaPago, MembresiaObservacion, MembresiaFechaRegistro) VALUES( " +
                                         " " + UsuarioID + ", CONVERT(DATE, '" + txtFechaIniMatricula.Text + "', 103), CONVERT(DATE, '" + txtFechaFinMatricula.Text + "', 103), '" + Documento + "', " +
                                         " '" + txtPagoMatricula.Text + "', '', SYSDATETIME()) ";
                            Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                            Response.Redirect("AsignarPlanes.aspx?exito=1");
                        }
                        else {
                            Response.Redirect("AsignarPlanes.aspx?exito=1");
                        }
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
                            "(us.UsuarioNombre+' '+us.UsuarioApellido) as UsuarioNombre, " +
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

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView2.Rows[index];
        //DateTime FechaActual = System.DateTime.Now;
        DateTime FechaActual = DateTime.Today.Date;
        if (e.CommandName.Equals("selectRecord"))
        {
            txtUsuario.Text = (gvrow.FindControl("UsuarioNombreB") as Label).Text;
            hdfUsuarioID.Value = (gvrow.FindControl("UsuarioIDB") as Label).Text;
            hdfSucursalID.Value = (gvrow.FindControl("SucursalIDB") as Label).Text;
            hdfClienteID.Value = (gvrow.FindControl("ClienteIDB") as Label).Text;
            sSelectSQL = "select top 1 convert(varchar(11),MembresiaFechaFin,103) as Fecha FROM Membresia WHERE MembresiaUsuarioID = "+hdfUsuarioID.Value+"  ORDER BY MembresiaID DESC";
            string fechaVencimiento = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
            if (fechaVencimiento != "")
                txtFechaVenMatricula.Text = fechaVencimiento;
            else
                txtFechaVenMatricula.Text = "No ha cancelado la Matricula";
            string FechaUltimaUsuario = "";
            sSelectSQL = "SELECT MAX(PlanAlumnoFechaFin) as MAXIMO FROM PlanAlumno Where UsuarioID = " + hdfUsuarioID.Value;
            Utilidades.maxRegistro(ref FechaUltimaUsuario, sSelectSQL, cn, ref Err);
            lblFechaVencimiento.Text = Utilidades.EjeSQL("SELECT CONVERT(VARCHAR(11), PlanAlumnoFechaFin, 103) as Fecha FROM PlanAlumno WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PLANALUMNOID DESC", cn, ref Err, true);
            lblFechaVencimiento2.Text = lblFechaVencimiento.Text;
            string rt = Utilidades.EjeSQL("SELECT totalClasesRegulares FROM PlanAlumnoAcumulado WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlaAlumnoAcumuladoID DESC", cn, ref Err, true);
            string rd = Utilidades.EjeSQL("SELECT disponiblesClasesRegulares FROM PlanAlumnoAcumulado WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlaAlumnoAcumuladoID DESC", cn, ref Err, true);
            string ct = Utilidades.EjeSQL("SELECT totalClasesComplemen FROM PlanAlumnoAcumulado WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlaAlumnoAcumuladoID DESC", cn, ref Err, true);
            string cd = Utilidades.EjeSQL("SELECT disponiblesClasesComplemen FROM PlanAlumnoAcumulado WHERE UsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlaAlumnoAcumuladoID DESC", cn, ref Err, true);
            lblClasesRegulares.Text = "Total: " + rt + ", Disponibles: " + rd;
            lblClasesComplementarias.Text = "Total: " + ct + ", Disponibles: " + cd;
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

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.EditIndex = -1;
        GridView2.SelectedIndex = -1;
        GridView2.PageIndex = e.NewPageIndex;
        string datoConsulta = txtUsuario.Text;
        BindGridView2(datoConsulta);
    }

    protected void dplPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplPlan.SelectedValue != "") 
        {
            asignarFechas(dplPlan.SelectedValue);        
        }
    }

    private void asignarFechas(string p)
    {
        DateTime FechaActual = DateTime.Today.Date;
        sSelectSQL = "SELECT MAX(PlanAlumnoFechaFin) as MAXIMO FROM PlanAlumno Where UsuarioID = " + hdfUsuarioID.Value;
        string FechaUltimaUsuario = "";
        Utilidades.maxRegistro(ref FechaUltimaUsuario, sSelectSQL, cn, ref Err);
        int dias = Int32.Parse(Utilidades.EjeSQL("SELECT PlanDias FROM [Plan] WHERE PlanID = "+p, cn, ref Err, true));
        if (FechaUltimaUsuario != "")
        {

            DateTime fechaUlt = Convert.ToDateTime(FechaUltimaUsuario);
            if (fechaUlt >= FechaActual)
            {
                txtFechaInicio.Text = fechaUlt.ToString("dd/MM/yyyy");
                hdfFechaINI.Value = fechaUlt.ToString("MM/dd/yyyy");
                txtFechaFin.Text = fechaUlt.AddDays(dias).Date.ToString("dd/MM/yyyy");
                hdfFechaFIN.Value = fechaUlt.AddDays(dias).Date.ToString("MM/dd/yyyy");
            }
            else
            {
                txtFechaInicio.Text = FechaActual.ToString("dd/MM/yyyy");
                hdfFechaINI.Value = FechaActual.ToString("MM/dd/yyyy");
                txtFechaFin.Text = FechaActual.AddDays(dias).Date.ToString("dd/MM/yyyy");
                hdfFechaFIN.Value = FechaActual.AddDays(dias).Date.ToString("MM/dd/yyyy");
            }
        }
        else
        {
            txtFechaInicio.Text = FechaActual.ToString("dd/MM/yyyy");
            hdfFechaINI.Value = FechaActual.ToString("MM/dd/yyyy");
            txtFechaFin.Text = FechaActual.AddDays(dias).Date.ToString("dd/MM/yyyy");
            hdfFechaFIN.Value = FechaActual.AddDays(dias).Date.ToString("MM/dd/yyyy");
        }
    }

    protected void ddlMatricula_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMatricula.SelectedValue != "")
        {
            if (ddlMatricula.SelectedValue == "SI")
            {
                //Consultar los datos en Membresias...
                string UsuarioID = hdfUsuarioID.Value;
                sSelectSQL = "SELECT TOP 1 Membresia.MembresiaID, " +
                     " CONVERT(varchar(11),Membresia.MembresiaFechaInicio, 103) as FechaInicio, " +
                     " CONVERT(varchar(11),Membresia.MembresiaFechaFin,103) as FechaFin, " +
                     " Membresia.MembresiaPago as Pago " +
                     " FROM Membresia INNER JOIN " +
                     " Usuario ON Membresia.MembresiaUsuarioID = Usuario.UsuarioID " +
                     " WHERE Usuario.UsuarioID = '" + UsuarioID + "' ORDER BY Membresia.MembresiaID DESC";
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader reader;
                try
                {
                    cn.Open();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        txtFechaIniMatricula.Text = reader["FechaInicio"].ToString();
                        txtFechaFinMatricula.Text = reader["FechaFin"].ToString();
                        txtPagoMatricula.Text = reader["Pago"].ToString();
                    }
                    reader.Close();
                    cn.Close();
                }
                catch (SqlException Sqlex)
                {
                    MostrarMsjModal("Error: " + Sqlex.Message, "ERR");
                }
                phMatricula.Visible = true;
            }
            else
            {
                phMatricula.Visible = false;
            }
        }

    }
}