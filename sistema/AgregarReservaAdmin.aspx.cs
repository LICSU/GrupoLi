using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sistema_AgregarReservaAdmin : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    DataTable dt;
    DataSet ds;
    GridView grid;
    string Err = "", sSelectSQL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName == "reservar")
        {
            //Generar una reserva
            txtClaseNombreAdd.Text = (gvrow.FindControl("ClaseDescripcion") as Label).Text;
            hdfClaseID.Value = (gvrow.FindControl("ClaseID") as Label).Text;
            hdfClienteID.Value = (gvrow.FindControl("ClienteID") as Label).Text;
            hdfClasePlantillaID.Value = (gvrow.FindControl("ClasePlantillaID") as Label).Text;
            hdfClaseTipo.Value = (gvrow.FindControl("ClaseTipo") as Label).Text;
            txtUnidadAdd.Text = (gvrow.FindControl("ClienteNombre") as Label).Text;
            txtFechaAdd.Text = (gvrow.FindControl("ClasePlantillaFecha") as Label).Text;
            txtHoraAdd.Text = (gvrow.FindControl("ClasePlantillaHora") as Label).Text;
            txtProfesorAdd.Text = (gvrow.FindControl("ProfesorNombre") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#addModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    private void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT ClasePlantilla.ClaseID as ClaseID, ClasePlantilla.ClasePlantillaID as ClasePlantillaID, "
                           + " Clase.ClaseDescripcion as ClaseDescripcion, clase.ClaseTipo as ClaseTipo, ClasePlantilla.ClienteID as ClienteID, "
                           + " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha, "
                           + " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora, "
                           + " ClasePlantilla.ClasePlantillaCupo as ClasePlantillaCupo, "
                           + " (SELECT ClienteNombre FROM Cliente WHERE ClienteID = ClasePlantilla.ClienteID) as ClienteNombre , "
                           + " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = ClasePlantilla.ProfesorID) as ProfesorNombre, "
                           + " Cliente.ClienteNombre as ClienteNombre"
                           + " FROM Clase INNER JOIN"
                           + " ClasePlantilla ON Clase.ClaseID = ClasePlantilla.ClaseID INNER JOIN"
                           + " Cliente ON ClasePlantilla.ClienteID = Cliente.ClienteID WHERE (CONVERT(DATE,ClasePlantillaFecha,103) >= CONVERT(DATE,SYSDATETIME(),103)) " +
                           " " + ViewState["FiltroFechas"] + " ORDER BY ClasePlantilla.ClasePlantillaFecha ASC, ClasePlantilla.ClasePlantillaHora ASC, Clase.ClaseID ASC";
            // MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClaseID";
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

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string datoConsulta = txtUsuarioNombreAdd.Text;
        string Cliente = hdfClienteID.Value;
        BindGridView2(datoConsulta, Cliente);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#bscModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        /**Guardar la Reserva y Actualizar las Tablas Correspondientes **/
        string ClasePlantillaID = hdfClasePlantillaID.Value;
        string ClaseTipo = hdfClaseTipo.Value;
        string UsuarioID = txtUsuarioIDAdd.Value;
        string ClaseID = hdfClaseID.Value;
        string costoUnidad = "";
        sSelectSQL = "SELECT ClaseUnidad as MAXIMO FROM Clase WHERE ClaseID = " + ClaseID;
        Utilidades.maxRegistro(ref costoUnidad, sSelectSQL, cn, ref Err);
        float costoUnidadNum = float.Parse(costoUnidad.Trim());
        string clasesDisp = "";
        int bandDescuento = 0;
        if (ClaseTipo == "Regular")
        {
            //Descontar Saldo de Regulares
            sSelectSQL = "SELECT disponiblesClasesRegulares as MAXIMO FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioID;
            Utilidades.maxRegistro(ref clasesDisp, sSelectSQL, cn, ref Err);
            bandDescuento = 1;
        }
        else
        {
            //Descontar Saldo de Complementarias
            sSelectSQL = "SELECT disponiblesClasesComplemen as MAXIMO FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioID;
            Utilidades.maxRegistro(ref clasesDisp, sSelectSQL, cn, ref Err);
            bandDescuento = 2;
            if (int.Parse(clasesDisp) == 0)
            {
                sSelectSQL = "SELECT disponiblesClasesRegulares as MAXIMO FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioID;
                Utilidades.maxRegistro(ref clasesDisp, sSelectSQL, cn, ref Err);
                bandDescuento = 1;
            }
        }
        float clasesDispNum = float.Parse(clasesDisp.Trim());
        if (clasesDispNum >= costoUnidadNum)
        {
            float disponiblesUlt = clasesDispNum - costoUnidadNum;
            if (descontarCupo(ClasePlantillaID) > 0)
            {
                if (actualizarPlanAcumulado(disponiblesUlt, UsuarioID) > 0)
                {
                    if (actualizarPlanAlumno(costoUnidadNum, "reservar", UsuarioID) > 0)
                    {
                        sSelectSQL = " INSERT INTO Reserva(FechaReserva,HoraReserva,ClasePlantillaID,UsuarioID) "
                                    + " VALUES(SYSDATETIME(),'" + horaActual() + "'," + ClasePlantillaID + "," + UsuarioID + ")";
                        cn.Open();
                        SqlCommand Cmd = new SqlCommand(sSelectSQL, cn);
                        try
                        {
                            int iRes = Cmd.ExecuteNonQuery();
                            if (iRes > 0)
                            {
                                cn.Close();
                                MostrarMsjModal("Reserva Realizada con Exito", "EXI");
                                Response.Redirect("MostrarReservas.aspx");
                            }
                        }
                        catch (SqlException sq)
                        {
                            MostrarMsjModal(sq.Message, "ERR");
                            cn.Close();
                        }
                    }
                    else
                    {
                        MostrarMsjModal("Error al Actualizar el Plan del Alumno", "ERR");
                    }
                }
                else
                {
                    MostrarMsjModal("Error al Descontar el Total", "ERR");
                }
            }
            else
            {
                MostrarMsjModal("La Clase no Posse Cupos Disponibles", "ERR");
            }
        }
        else
        {
            MostrarMsjModal("El Alumno: " + txtUsuarioNombreAdd.Text + " No posee Saldo a Favor", "ERR");
        }

    }

    protected void BindGridView2(string dato, string ClienteID)
    {
        string condicion = "";
        if (dato != "")
        {
            condicion = " AND (us.UsuarioCedula LIKE '%" + dato + "%' OR us.UsuarioNombre LIKE '%" + dato + "%')";
        }
        try
        {
            cn.Open();
            string cmd2 = "SELECT DISTINCT(us.UsuarioID) as UsuarioID, "
                           + " us.UsuarioNombre+' '+us.UsuarioApellido as Nombre, "
                           + " us.UsuarioCedula as UsuarioCedula, "
                           + " paa.disponiblesClasesRegulares as DisponiblesRegulares"
                           + " FROM Usuario us, PlanAlumno pa, PlanAlumnoAcumulado paa"
                           + " WHERE pa.UsuarioID = us.UsuarioID AND paa.UsuarioID = us.UsuarioID " + condicion + " "
                           + " AND (CONVERT(DATE,pa.PlanAlumnoFechaFin,103) >= CONVERT(DATE,SYSDATETIME(),103))  AND CAST(REPLACE(disponiblesClasesRegulares,',','.') AS float) > 0 AND (pa.ClienteID = " + ClienteID + ")";
            //MostrarMsjModal(cmd2, "");
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
        if (e.CommandName.Equals("selectRecord"))
        {
            txtUsuarioNombreAdd.Text = (gvrow.FindControl("UsuarioNombreB") as Label).Text;
            txtUsuarioIDAdd.Value = (gvrow.FindControl("UsuarioIDB") as Label).Text;
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
        string Cliente = hdfClienteID.Value;
        BindGridView2("", Cliente);
    }

    protected string horaActual()
    {
        return DateTime.Now.ToString("HH:mm");
    }

    protected int descontarCupo(string ClasePlantillaID)
    {
        int iRes = 0;
        string cuposClase = "";
        sSelectSQL = "SELECT ClasePlantillaCupo as MAXIMO FROM ClasePlantilla WHERE ClasePlantillaID = " + ClasePlantillaID;
        Utilidades.maxRegistro(ref cuposClase, sSelectSQL, cn, ref Err);
        int Cupos = int.Parse(cuposClase.Trim());
        cn.Open();
        sSelectSQL = "UPDATE ClasePlantilla SET ClasePlantillaCupo = " + (Cupos - 1) + " WHERE ClasePlantillaID = " + ClasePlantillaID;
        SqlCommand Cmd = new SqlCommand(sSelectSQL, cn);
        try
        {
            iRes = Cmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal(sq.Message, "ERR");
        }
        cn.Close();
        return iRes;
    }

    protected int actualizarPlanAcumulado(float disponibles, string UsuarioID)
    {
        int iRes = 0;
        cn.Open();
        sSelectSQL = "UPDATE PlanAlumnoAcumulado SET disponiblesClasesRegulares = '" + disponibles + "' WHERE UsuarioID = " + UsuarioID;
        SqlCommand Cmd = new SqlCommand(sSelectSQL, cn);
        try
        {
            iRes = Cmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal(sq.Message, "ERR");
        }
        cn.Close();
        return iRes;
    }

    protected int actualizarPlanAlumno(float costoClase, string accion, string UsuarioID)
    {
        int iRes = 0;
        string clasesActivas = "";
        float clasesActivasNum;
        sSelectSQL = "SELECT ClasesActivas as MAXIMO FROM PlanAlumno WHERE UsuarioID = " + UsuarioID;
        Utilidades.maxRegistro(ref clasesActivas, sSelectSQL, cn, ref Err);
        if (accion == "delete")
            clasesActivasNum = float.Parse(clasesActivas.Trim()) - costoClase;
        else
            clasesActivasNum = float.Parse(clasesActivas.Trim()) + costoClase;
        cn.Open();
        sSelectSQL = "UPDATE PlanAlumno SET ClasesActivas = '" + clasesActivasNum + "' WHERE UsuarioID = " + UsuarioID;
        SqlCommand Cmd = new SqlCommand(sSelectSQL, cn);
        try
        {
            iRes = Cmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal(sq.Message, "ERR");
        }
        cn.Close();
        return iRes;
    }

    protected void btnBuscar_Click1(object sender, EventArgs e)
    {
        if (txtBuscar.Value != "")
        {
            ViewState["FiltroFechas"] = " AND ( CONVERT(DATE,ClasePlantilla.ClasePlantillaFecha,103) = CONVERT(DATE, '" + txtBuscar.Value + "', 103) )";
            BindGridView();
        }
        else
        {
            MostrarMsjModal("Debe ingresar una fecha válida", "ERR");
        }
    }
}