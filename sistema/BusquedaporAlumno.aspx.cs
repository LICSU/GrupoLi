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

public partial class sistema_BusquedaporAlumno : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    DataTable dt;
    string Err = "", SselectSQL = "";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string UsuarioID = "", PlanID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        //Mostramos lo relacionado al almuno...
        if (txtUsuario.Text == "")
        {
            MostrarMsjModal("Debe ingresar un valor de búsqueda.", "ERR");
        }
        else
        {
            busqueda(txtUsuario.Text);
            phInicial.Visible =false;
        }

    }
    protected void busqueda(string busqueda)
    {
        if (Utilidades.EjeSQL("SELECT UsuarioCedula FROM Usuario WHERE UsuarioCedula = '" + busqueda + "'", cn, ref Err, true) != "")
        {
            string SQL = "SELECT UsuarioFoto FROM Usuario WHERE UsuarioCedula = '" + busqueda + "'";
            string foto = Utilidades.EjeSQL(SQL, cn, ref Err, true);
            string ruta = "~/sistema/fotos/";
            if (foto.Length == 0)
            {
                string sexo = Utilidades.EjeSQL("SELECT UsuarioSexo FROM Usuario WHERE UsuarioCedula = '"+busqueda+"'", cn, ref Err, true);
                if (sexo == "M")
                    ruta = ruta + "hombre.jpg";
                else
                    ruta = ruta + "mujer.jpg";
                    imagPerfil.ImageUrl = ruta;
            }
            else
            {
                imagPerfil.ImageUrl = foto;
            }
            hdfCedulaID.Value = busqueda;
            string usuarioid = Utilidades.EjeSQL("SELECT UsuarioID FROM Usuario WHERE UsuarioCedula = '" + busqueda + "'", cn, ref Err, true);
            string clienteid = Utilidades.EjeSQL("SELECT ClienteID FROM UsuarioRol WHERE UsuarioID = " + usuarioid, cn, ref Err, true);
            SselectSQL = "select membresiapago from membresia where membresiausuarioid =  " + usuarioid;
            txtPagoMatricula.Text = Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
            SselectSQL = "select Convert(varchar(11),membresiaFechaFin,103) as membresiaFechaFin from membresia where membresiausuarioid =  " + usuarioid;
            txtFechaMatricula.Text = Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
            if (clienteid == "1")
            {
                phLicsu.Visible = true;
                phEmpresa.Visible = false;
                SselectSQL = "SELECT " +
                              "  CASE WHEN  " +
                              "  CONVERT(DATE, p.planalumnofechafin, 103) >= CONVERT(DATE, sysdatetime(), 103) " +
                              "  THEN 'ACTIVO' ELSE 'VENCIDO' END Estado " +
                              "  FROM planalumno p WHERE p.UsuarioID = " + usuarioid + "" +
                              " ORDER BY p.planalumnofechafin DESC";
                lblEstado.Text = "Estado: "+Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
            }
            else
            {
                phLicsu.Visible = false;
                phEmpresa.Visible = true;
                SselectSQL = " SELECT " +
                             " CASE WHEN p.PlanActivo = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END Estado " +
                             " FROM PlanEmpresa p WHERE UsuarioID = " + usuarioid;
                lblMesActual.Text = "Mes Actual: " + Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
                SselectSQL = " SELECT " +
                             " CASE WHEN p.EstadoProximo = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END Estado " +
                             " FROM PlanEmpresa p WHERE UsuarioID = " + usuarioid;
                lblMesProximo.Text = "Mes Próximo: " + Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
            }
            lblCedula.Text = "Cédula: " + Utilidades.EjeSQL("SELECT UsuarioCedula FROM Usuario WHERE UsuarioCedula = '" + busqueda + "'", cn, ref Err, true);
            lblNombres.Text = "Nombres Completos: " + Utilidades.EjeSQL("SELECT (UsuarioNombre+' '+UsuarioApellido) as UsuarioNombres FROM Usuario WHERE UsuarioCedula = '" + busqueda + "'", cn, ref Err, true);
            ViewState["UsuarioID"] = Utilidades.EjeSQL("SELECT UsuarioID FROM Usuario WHERE UsuarioCedula = '" + busqueda + "'", cn, ref Err, true);
            lblFechaMatricula.Text = "Vencimiento Matrícula: " + Utilidades.EjeSQL("SELECT CONVERT(VARCHAR(11), MembresiaFechaFin, 103) as Fecha FROM Membresia WHERE MembresiaUsuarioID = " + ViewState["UsuarioID"].ToString(), cn, ref Err, true);
            hdfUsuarioID.Value = ViewState["UsuarioID"].ToString();
            SselectSQL = "SELECT dbo.[Plan].PlanNombre as PlanNombre,  dbo.[Plan].PlanID as PlanID," +
                           " dbo.PlanAlumno.PlanAlumnoID as PlanAlumnoID, " +
                           " CONVERT(VARCHAR(11), dbo.PlanAlumno.PlanAlumnoFechaInicio, 103) as PlanAlumnoFechaInicio, " +
                           " CONVERT(VARCHAR(11), dbo.PlanAlumno.PlanAlumnoFechaFin, 103) as PlanAlumnoFechaFin, " +
                           " dbo.PlanAlumno.ClasesActivas as ClasesActivas, " +
                           " dbo.PlanAlumno.SaldoPositivo as SaldoPositivo, " +
                           " dbo.PlanAlumno.SaldoNegativo as SaldoNegativo " +
                           " FROM dbo.[Plan] INNER JOIN " +
                           " dbo.PlanAlumno ON dbo.[Plan].PlanID = dbo.PlanAlumno.PlanID " +
                           " WHERE dbo.PlanAlumno.UsuarioID = " + ViewState["UsuarioID"];
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(SselectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PlanID = dr["PlanID"].ToString();
                    lblPlanNombre.Text = "Plan: " + dr["PlanNombre"].ToString();
                    hdfPlanAlumnoID.Value = dr["PlanAlumnoID"].ToString();
                    lblFechaInicio.Text = dr["PlanAlumnoFechaInicio"].ToString();
                    lblFechaFin.Text = dr["PlanAlumnoFechaFin"].ToString();
                    lblClasesActivas.Text = "Total de Reservas: " + dr["ClasesActivas"].ToString();
                    lblDeuda.Text = "Deuda: " + dr["SaldoNegativo"].ToString();
                }
                cn.Close();
                SselectSQL = "SELECT totalClasesRegulares FROM PlanAlumnoAcumulado WHERE UsuarioID = " + ViewState["UsuarioID"];
                lblTotalClasesR.Text = Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
                SselectSQL = "SELECT totalClasesComplemen FROM PlanAlumnoAcumulado WHERE UsuarioID = " + ViewState["UsuarioID"];
                lblTotalClasesC.Text = Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
                SselectSQL = "SELECT disponiblesClasesRegulares FROM PlanAlumnoAcumulado WHERE UsuarioID = " + ViewState["UsuarioID"];
                lblDispClasesR.Text = Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
                SselectSQL = "SELECT disponiblesClasesComplemen FROM PlanAlumnoAcumulado WHERE UsuarioID = " + ViewState["UsuarioID"];
                lblDispClasesC.Text = Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);
                BindGridView();
                BindGridView2();
                BindGridView3();
            }
            catch (SqlException sq)
            {
                MostrarMsjModal("Error: " + sq.Message, "ERR");
                cn.Close();
            }

            upDatos.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "Cerrar", "Cerrar();", true);
        }
        else
        {
            MostrarMsjModal("La búsqueda no arrojó ningún resultado", "ERR");
        }
    }
    protected void BindGridView3()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT dbo.Alumno_Nivel_Clase.UsuarioID as UsuarioID, " +
                       " (SELECT UsuarioNombre+''+UsuarioApellido FROM Usuario WHERE UsuarioID = dbo.Alumno_Nivel_Clase.ProfesorID) as Profesor, " +
                       " (SELECT ClaseDescripcion FROM Clase Where ClaseID = dbo.Clase_Nivel_Elemento.ClaseID) as ClaseNombre, " +
                       " dbo.Elemento.ElementoNombre as ElementoNombre, " +
                       " dbo.Calificacion.CalificacionNombre as CalificacionNombre " +
                       " FROM dbo.Alumno_Nivel_Clase_Elemento INNER JOIN " +
                       " dbo.Calificacion ON dbo.Alumno_Nivel_Clase_Elemento.CalificacionID = dbo.Calificacion.CalificacionID INNER JOIN " +
                       " dbo.Alumno_Nivel_Clase ON dbo.Alumno_Nivel_Clase_Elemento.AluNivClaseID = dbo.Alumno_Nivel_Clase.AluNivClaseID INNER JOIN " +
                       " dbo.Clase_Nivel_Elemento ON dbo.Alumno_Nivel_Clase_Elemento.ClaseElemNivID = dbo.Clase_Nivel_Elemento.ClaseElemNivID AND  " +
                       " dbo.Alumno_Nivel_Clase_Elemento.ClaseElemNivID = dbo.Clase_Nivel_Elemento.ClaseElemNivID INNER JOIN " +
                       " dbo.Elemento ON dbo.Clase_Nivel_Elemento.ElementoID = dbo.Elemento.ElementoID " +
                       " WHERE dbo.Alumno_Nivel_Clase.UsuarioID = " + ViewState["UsuarioID"];
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioID";
            GridView3.DataKeyNames = TablaID;
            GridView3.DataSource = dt;
            GridView3.DataBind();
            cn.Close();
            if (dt.Rows.Count > 0)
            {
                GridView3.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone. 
                GridView3.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView3.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView3.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }
    protected void BindGridView2()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT dbo.[Plan].PlanNombre AS PlanNombre, " +
                            " dbo.PlanPago.FacturaNumero AS FacturaNumero, " +
                            " CONVERT(VARCHAR(11),dbo.PlanPago.FacturaFecha,103) AS FacturaFecha, " +
                            " dbo.PlanPago.FacturaMonto AS FacturaMonto " +
                            " FROM dbo.PlanAlumno INNER JOIN dbo.PlanPago ON dbo.PlanAlumno.PlanAlumnoID = dbo.PlanPago.PlanAlumnoID " +
                            " INNER JOIN dbo.[Plan] ON dbo.PlanAlumno.PlanID = dbo.[Plan].PlanID " +
                            " WHERE dbo.PlanAlumno.UsuarioID = " + ViewState["UsuarioID"] + " order by dbo.PlanPago.FacturaFecha desc";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "FacturaNumero";
            GridView2.DataKeyNames = TablaID;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            cn.Close();
            if (dt.Rows.Count > 0)
            {
                GridView2.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone. 
                GridView2.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView2.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView2.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();

            string cmd2 = "SELECT dbo.Clase.ClaseDescripcion as ClaseDescripcion, " +
                           " (dbo.ClasePlantilla.ClasePlantillaFecha+' '+dbo.ClasePlantilla.ClasePlantillaHora) as ClasePlantillaFecha, " +
                           " ((CONVERT(VARCHAR(11), dbo.Reserva.FechaReserva,103))+' '+(dbo.Reserva.HoraReserva)) as FechaReserva, " +
                           " (SELECT UsuarioNombre+' '+UsuarioApellido from Usuario WHERE UsuarioID = dbo.ClasePlantilla.ProfesorID) as Profesor " +
                           "  FROM dbo.Clase INNER JOIN " +
                           " dbo.ClasePlantilla ON dbo.Clase.ClaseID = dbo.ClasePlantilla.ClaseID INNER JOIN " +
                           " dbo.Reserva ON dbo.ClasePlantilla.ClasePlantillaID = dbo.Reserva.ClasePlantillaID " +
                           " WHERE dbo.Reserva.UsuarioID = " + ViewState["UsuarioID"];

            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClaseDescripcion";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            if (dt.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone. 
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
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

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView3.EditIndex = -1;
        GridView3.SelectedIndex = -1;
        GridView3.PageIndex = e.NewPageIndex;
        BindGridView3();
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        string TotalRegulares = lblTotalClasesR.Text;
        string TotalComplementarias = lblTotalClasesC.Text;
        string DisponiblesRegulares = lblDispClasesR.Text;
        string DisponiblesComplementarias = lblDispClasesC.Text;
        string FechaVencimiento = lblFechaFin.Text;
        string FechaInicio = lblFechaInicio.Text;
        string PlanAlumnoID = hdfPlanAlumnoID.Value;
        string UsuarioID = hdfUsuarioID.Value;
        string cedulaUser = hdfCedulaID.Value;
        Err = "";
        SselectSQL = "UPDATE PlanAlumno SET PlanAlumnoFechaInicio = CONVERT(DATE,'" + FechaInicio + "',103), PlanAlumnoFechaFin = CONVERT(DATE,'" + FechaVencimiento + "',103) " +
                    " WHERE PlanAlumnoID =  " + PlanAlumnoID;
        Utilidades.EjeSQL(SselectSQL, cn, ref Err, false);
        SselectSQL = "UPDATE PlanAlumnoAcumulado SET totalClasesRegulares = " + TotalRegulares + ", totalClasesComplemen = " + TotalComplementarias + " " +
                    " ,disponiblesClasesRegulares = " + DisponiblesRegulares + ", disponiblesClasesComplemen = " + DisponiblesComplementarias + " " +
                    " WHERE UsuarioID = " + UsuarioID;
        Utilidades.EjeSQL(SselectSQL, cn, ref Err, false);

        SselectSQL = "SELECT MembresiaID FROM Membresia WHERE MembresiaUsuarioID =" + UsuarioID;
        string resp = Utilidades.EjeSQL(SselectSQL, cn, ref Err, true);

        if (resp == "-1" || resp == "")
        {
            SselectSQL = "INSERT INTO Membresia (MembresiaUsuarioID, MembresiaFechaInicio, MembresiaFechaFin, MembresiaDocumento, MembresiaPago, MembresiaObservacion, MembresiaFechaRegistro)"+
                         " VALUES(" + UsuarioID + ", SYSDATETIME(), CONVERT(DATE, '" + txtFechaMatricula.Text + "', 103), '" + cedulaUser + "', "+txtPagoMatricula.Text+", '', SYSDATETIME())";
            Utilidades.EjeSQL(SselectSQL, cn, ref Err, false);
        }
        else
        {
            SselectSQL = "UPDATE Membresia SET MembresiaPago = " + txtPagoMatricula.Text + ", MembresiaFechaFin = CONVERT(DATE, '" + txtFechaMatricula.Text + "', 103) WHERE MembresiaUsuarioID =" + UsuarioID;
            Utilidades.EjeSQL(SselectSQL, cn, ref Err, false);
        }

        if (Err == "")
        {
            MostrarMsjModal("Cambios Realizados con Exito", "EXI");
            busqueda(cedulaUser);
        }
        else
        {
            MostrarMsjModal("Error: " + Err, "ERR");
        }

    }
}