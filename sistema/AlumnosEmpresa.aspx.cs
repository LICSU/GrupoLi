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

public partial class sistema_AlumnosEmpresa : System.Web.UI.Page
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    DataTable dt;
    string Err = "", sSelectSQL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente WHERE ClienteID != 1 ORDER BY TXT";
            Utilidades.CargarListado(ref dplClientes, sSelectSQL, cn, ref Err, true);
            sSelectSQL = "SELECT PlanID AS VAL, PlanNombre AS TXT FROM [Plan]";
            Utilidades.CargarListado(ref dplPlan, sSelectSQL, cn, ref Err, true);
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Usuario.UsuarioID as UsuarioId, " +
                        " Usuario.UsuarioNombre as UsuarioNombre, " +
                        " Usuario.UsuarioApellido as UsuarioApellido, " +
                        " Usuario.UsuarioCedula as UsuarioCedula, " +
                        " Cliente.ClienteNombre as ClienteNombre, " +
                        " Cliente.ClienteID as ClienteID, " +
                        " PlanEmpresa.PlanActivo as PlanActivo" +
                        " FROM PlanEmpresa INNER JOIN" +
                        " Usuario ON PlanEmpresa.UsuarioID = Usuario.UsuarioID INNER JOIN" +
                        " UsuarioRol ON Usuario.UsuarioID = UsuarioRol.UsuarioID INNER JOIN" +
                        " Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID" +
                        " WHERE (PlanEmpresa.EstadoProximo = 1) " + ViewState["sWhere"];
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            if (GridView1.Rows.Count > 0)
            {
                btnAsignarPlan.Enabled = true;
                //Attribute to show the Plus Minus Button.
                GridView1.HeaderRow.Cells[1].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.                
                GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
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

    protected void dplClientes_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (dplClientes.SelectedValue == "")
        {
            ViewState["sWhere"] = "  ";
        }
        else
        {
            ViewState["sWhere"] = "  AND Cliente.ClienteID = " + dplClientes.SelectedValue;
        }
        BindGridView();
    }

    protected void btnAsignarPlan_Click(object sender, EventArgs e)
    {
        //Asignar Plan..
        int ultimo = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);
        string[] vectorUsuariosID;
        int conUser = 0;
        if (dplClientes.SelectedValue != "" || dplPlan.SelectedValue != "")
        {
            if (DateTime.Today.Day == ultimo)
            {
                //1. Obtengo los datos seleccionados...
                string ClienteID = dplClientes.SelectedValue;
                string PlanID = dplPlan.SelectedValue;
                string UsuarioID = "";
                int cant = 0, iRes = 0;
                //Poner el primer dia del mes...
                DateTime fechatemp;
                DateTime FI;
                DateTime FF;

                fechatemp = DateTime.Today;
                FI = new DateTime(fechatemp.Year, fechatemp.Month + 1, 1);
                FF = new DateTime(fechatemp.Year, fechatemp.Month + 2, 1).AddDays(-1);

                string FechaInicio = FI.ToString();
                string FechaFin = FF.ToString();
                FechaInicio = Utilidades.FecUni(FechaInicio);
                FechaFin = Utilidades.FecUni(FechaFin);
                FechaInicio = FI.ToString("yyyy-MM-dd");
                FechaFin = FF.ToString("yyyy-MM-dd");
                //FechaInicio = Utilidades.EjeSQL("select convert(varchar(24) ,'" + FechaInicio + "', 103)", cn, ref Err, true);
                //FechaFin = Utilidades.EjeSQL("select convert(varchar(24) ,'" + FechaFin + "', 103)", cn, ref Err, true);
                int PlanAlumnoID = 0;
                string maxPlanAlumno = "";
                sSelectSQL = "SELECT MAX(PlanAlumnoID) as MAXIMO FROM PlanAlumno";
                Utilidades.maxRegistro(ref maxPlanAlumno, sSelectSQL, cn, ref Err);
                string CantidadClases = "0", cc2 = "";
                sSelectSQL = "Select clasesRegulares as MAXIMO from [Plan] Where PlanID = " + PlanID;
                Utilidades.maxRegistro(ref cc2, sSelectSQL, cn, ref Err);
                string saldoNegativo = "";
                sSelectSQL = "SELECT PlanCosto as MAXIMO FROM [Plan] Where PlanID = " + PlanID;
                Utilidades.maxRegistro(ref saldoNegativo, sSelectSQL, cn, ref Err);
                double saldoN = double.Parse(saldoNegativo);
                //string cadena = "";
                if (maxPlanAlumno == "") { PlanAlumnoID = 1; }
                else
                {
                    PlanAlumnoID = int.Parse(maxPlanAlumno.Trim()) + 1;
                }
                sSelectSQL = " UPDATE PlanEmpresa set PlanActivo = 0 WHERE (PlanEmpresa.EstadoProximo = 0) AND ( (Month(mesProximo)) = (Month(getDate()) + 1) )";
                Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);

                sSelectSQL = "SELECT COUNT(DISTINCT(Usuario.UsuarioID)) FROM PlanEmpresa INNER JOIN Usuario ON "
                           + " PlanEmpresa.UsuarioID = Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = "
                           + " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID "
                           + " WHERE (PlanEmpresa.EstadoProximo = 1)" + ViewState["sWhere"] + " ";
                int tamVectorn = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
                vectorUsuariosID = new string[tamVectorn];
                //2. Insertamos los planes.....
                cn.Open();
                sSelectSQL = "SELECT DISTINCT(Usuario.UsuarioID) as UsuarioId, " +
                            " Usuario.UsuarioNombre as UsuarioNombre, " +
                            " Usuario.UsuarioApellido as UsuarioApellido, " +
                            " Usuario.UsuarioCedula as UsuarioCedula, " +
                            " Cliente.ClienteNombre as ClienteNombre, " +
                            " Cliente.ClienteID as ClienteID, " +
                            " PlanEmpresa.PlanActivo as PlanActivo" +
                            " FROM PlanEmpresa INNER JOIN" +
                            " Usuario ON PlanEmpresa.UsuarioID = Usuario.UsuarioID INNER JOIN" +
                            " UsuarioRol ON Usuario.UsuarioID = UsuarioRol.UsuarioID INNER JOIN" +
                            " Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID" +
                            " WHERE (PlanEmpresa.EstadoProximo = 1) " + ViewState["sWhere"];
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                
                string SucursalID = "1";
                //MostrarMsjModal(sSelectSQL, "");
                int iRes3 = 0;
                try
                {
                    while (dr.Read())
                    {
                        //Guardar todos los UsuarioID en un vector para luego reccorrerlos y enviarlos
                        UsuarioID = dr["UsuarioID"].ToString();
                        vectorUsuariosID[conUser] = UsuarioID;
                        conUser++;
                    }
                    cn.Close();
                }
                catch (SqlException sq)
                {
                    Err = sq.Message;
                }
                bool band = false;
                for (int i = 0; i < vectorUsuariosID.Length; i++)
                {
                    int resp = eliminarAcumulado(vectorUsuariosID[i]);
                    int res2 = eliminarPlanAlumno(vectorUsuariosID[i]);
                    //if (resp > 0 && res2 > 0)
                    //{
                    DateTime MesProximo = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 2, 1);
                    DateTime MesActual = DateTime.Today.Date;
                    sSelectSQL = "UPDATE PlanEmpresa SET EstadoProximo = 1," +
                                " MesProximo = CONVERT(DATE, '" + MesProximo.ToString("d") + "', 103), fechaUltimo = CONVERT(DATE, '" + MesActual.ToString("d") + "', 103) WHERE UsuarioID = " + vectorUsuariosID[i];
                    Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                    iRes = insertarAlumno(PlanAlumnoID, vectorUsuariosID[i], SucursalID, PlanID, FechaInicio, FechaFin, ClienteID, saldoN, cant, CantidadClases);
                    PlanAlumnoID++;
                    if (iRes > 0)
                    {
                        //Insertar en PlanAlumnoAcumulado
                        iRes3 = insertarPlanAcumulado(cc2, vectorUsuariosID[i]);
                        if (iRes3 > 0)
                        {
                            band = true;
                            //ENVIAR CORREO

                        }
                        else
                        {
                            MostrarMsjModal("No se pudo realizar la asignación: " + Err, "ERR");
                        }
                    }
                    else
                    {
                        MostrarMsjModal("Error al Insertar el PlanAlumno: " + Err, "ERR");
                    }

                    //}
                    //else
                    //{
                    //    MostrarMsjModal("Error al limpiar la base de datos", "ERR");
                    //}  
                }
                if (band)
                    MostrarMsjModal("Asignación de Plan con Éxito", "");
                //cn.Close();

            }
            else
            {
                MostrarMsjModal("No puede asignar los planes", "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Debe Seleccionar un valor", "ERR");
        }
    }
    protected int insertarPlanAcumulado(string cc2, string UsuarioID)
    {
        int res = 0;
        sSelectSQL = "INSERT INTO PlanAlumnoAcumulado (totalClasesRegulares, disponiblesClasesRegulares, UsuarioID, seleccionoClases, totalClasesComplemen, disponiblesClasesComplemen)" +
                     " VALUES (" + Utilidades.SiEsNulo(cc2, "N") + ", " + Utilidades.SiEsNulo(cc2, "T") + ", " + Utilidades.SiEsNulo(UsuarioID, "N") + ", 0,0,0)";
        cn.Open();
        SqlCommand addCmd2 = new SqlCommand(sSelectSQL, cn);
        try { res = addCmd2.ExecuteNonQuery(); }
        catch (SqlException sq) { MostrarMsjModal(sq.Message, "ERR"); }
        cn.Close();

        return res;
    }
    protected int eliminarPlanAlumno(string UsuarioID)
    {
        int res = 0;

        sSelectSQL = "DELETE FROM PlanAlumno WHERE UsuarioID = " + UsuarioID;
        cn.Open();
        try
        {
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            res = addCmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal("Error: " + sq.Message, "ERR");
        }

        cn.Close();
        return res;
    }
    protected int eliminarAcumulado(string UsuarioID)
    {
        int res = 0;

        sSelectSQL = "DELETE FROM PlanAlumnoAcumulado WHERE UsuarioID = " + UsuarioID;
        cn.Open();
        try
        {
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            res = addCmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal("Error: " + sq.Message, "ERR");
        }
        cn.Close();
        return res;
    }
    protected int insertarAlumno(int PlanAlumnoID, string UsuarioID, string SucursalID, string PlanID, string FechaInicio,
        string FechaFin, string ClienteID, double saldoN, int cant, string CantidadClases)
    {
        int iRes = 0;
        cn.Open();
        string sSelectSQL2 = "INSERT INTO PlanAlumno" +
                            "(PlanAlumnoID, UsuarioID, SucursalID, PlanID, PlanAlumnoFechaInicio, PlanAlumnoFechaFin " +
                            ",PlanAlumnoFechaRegistro" +
                            ",PlanAlumnoUsuarioRegistro,ClienteID, SaldoPositivo, SaldoNegativo, NumeroFactura, ClasesActivas, FacturaNota)" +
                            "VALUES(" + PlanAlumnoID + "," + Utilidades.SiEsNulo(UsuarioID, "N") + ", " + Utilidades.SiEsNulo(SucursalID, "N") + ", " + Utilidades.SiEsNulo(PlanID, "N") + ",CONVERT(VARCHAR(24),'" + FechaInicio + "',103)" +
                            ",CONVERT(VARCHAR(24),'" + FechaFin + "',103),SYSDATETIME(), 1, " + Utilidades.SiEsNulo(ClienteID, "N") +
                            "," + saldoN + ", 0, " + Utilidades.SiEsNulo(ClienteID + "_" + cant, "T") +
                            ", " + Utilidades.SiEsNulo(CantidadClases, "T") + "," + Utilidades.SiEsNulo("", "T") + ")";
        //MostrarMsjModal(sSelectSQL2, "");
        SqlCommand addCmd = new SqlCommand(sSelectSQL2, cn);
        try { iRes = addCmd.ExecuteNonQuery(); }
        catch (SqlException sq) { Err = sq.Message; }
        cn.Close();

        return iRes;
    }
    protected void dplPlan_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected bool ultimoDia()
    {
        bool siUltimo = false;

        MostrarMsjModal(" " + DateTime.Today.Day, "");

        return siUltimo;
    }
}