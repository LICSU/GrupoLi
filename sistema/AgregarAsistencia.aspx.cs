using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sistema_AgregarAsistencia : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "", sSelectSQL = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    TableRow tableRow;
    TableCell tableCell1, tableCell2, tableCell3, tableCell4;
    CheckBox chkAsistio;
    Table table;
    static string[] vectorID;
    static string ClasePLantillaID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        ClasePLantillaID = Request.QueryString["ClaseID"];
        _autenticado = new UsuarioAutenticado(fIdentity);
        table = (Table)FindControl("tabUsuarios");
        int contIDs = 0;
        sSelectSQL = "SELECT dbo.Usuario.UsuarioID as ID,dbo.Usuario.UsuarioNombre, dbo.Usuario.UsuarioApellido, " +
                       " dbo.Usuario.UsuarioCedula,  " +
                       " dbo.Reserva.ClasePlantillaID " +
                       " FROM dbo.Reserva INNER JOIN " +
                       " dbo.Usuario ON dbo.Reserva.UsuarioID = dbo.Usuario.UsuarioID " +
                       " WHERE (dbo.Reserva.ClasePlantillaID = " + ClasePLantillaID + ")" +
                       " AND dbo.Usuario.UsuarioID  " +
                       " NOT IN (SELECT ClaseAsistenciaUsuarioID FROM ClaseAsistencia WHERE ClaseAsistenciaPlantillaID =  " + ClasePLantillaID + ")";

        string sSelectSQL2 = "SELECT COUNT(*) as ID " +
                       " FROM dbo.Reserva INNER JOIN " +
                       " dbo.Usuario ON dbo.Reserva.UsuarioID = dbo.Usuario.UsuarioID " +
                       " WHERE (dbo.Reserva.ClasePlantillaID = " + ClasePLantillaID + ")" +
                       " AND dbo.Usuario.UsuarioID  " +
                       " NOT IN (SELECT ClaseAsistenciaUsuarioID FROM ClaseAsistencia WHERE ClaseAsistenciaPlantillaID = " + ClasePLantillaID + ")";

        vectorID = new string[int.Parse(Utilidades.EjeSQL(sSelectSQL2, cn, ref Err, true))];
        //Cargar Alumnos...
        try
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                vectorID[contIDs] = dr["ID"].ToString();
                contIDs++;
            }
            cn.Close();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal("ERROR: " + sq.Message, "ERR");
            cn.Close();
        }
        for (int i = 0; i < vectorID.Length; i++)
        {
            sSelectSQL = "SELECT UsuarioNombre, UsuarioApellido,UsuarioCedula FROM Usuario WHERE UsuarioID = " + vectorID[i];
            try
            {
                Literal _literal;
                cn.Open();
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr1 = cmd.ExecuteReader();
                while (dr1.Read())
                {
                    tableRow = new TableRow();
                    tableCell1 = new TableCell();
                    tableCell1.Text = dr1["UsuarioNombre"].ToString();
                    tableRow.Controls.Add(tableCell1);
                    tableCell2 = new TableCell();
                    tableCell2.Text = dr1["UsuarioApellido"].ToString();
                    tableRow.Controls.Add(tableCell2);
                    tableCell3 = new TableCell();
                    tableCell3.Text = dr1["UsuarioCedula"].ToString();
                    tableRow.Controls.Add(tableCell3);
                    chkAsistio = new CheckBox();
                    string _name = "chk" + i;
                    chkAsistio.ID = _name;
                    _literal = new Literal();
                    _literal.Text = "<label for='"+_name+"'></label>";
                    tableCell4 = new TableCell();
                    tableCell4.Controls.Add(chkAsistio);
                    tableCell4.Controls.Add(_literal);
                    tableRow.Controls.Add(tableCell4);
                    table.Controls.Add(tableRow);
                }
                cn.Close();
            }
            catch (SqlException sq)
            {
                MostrarMsjModal("Error: " + sq.Message, "ERR");
            }

        }
    }
    private void MostrarVolver()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarVolver", "MostrarVolver();", true);
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        CheckBox chk;
        bool band = false;
        int iRes = 0;
        for (int i = 0; i < vectorID.Length; i++)
        {
            chk = (CheckBox)FindControl("chk" + i);
            string AlumnoID = vectorID[i];
            sSelectSQL = " SELECT dbo.Reserva.ReservaID " +
                    " FROM dbo.Reserva INNER JOIN " +
                    " dbo.ClasePlantilla ON dbo.Reserva.ClasePlantillaID = dbo.ClasePlantilla.ClasePlantillaID " +
                    " WHERE (dbo.ClasePlantilla.ClasePlantillaID = " + ClasePLantillaID + ") AND (dbo.Reserva.UsuarioID = " + AlumnoID + ") ";
            string ReservaID = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
            string ClaseID = Utilidades.EjeSQL("SELECT ClaseID FROM ClasePlantilla WHERE ClasePLantillaID = " + ClasePLantillaID, cn, ref Err, true);
            sSelectSQL = "SELECT * FROM ClaseAsistencia WHERE ClaseAsistenciaPlantillaID =" + ClasePLantillaID + " " +
                        " AND ClaseAsistenciaUsuarioID = " + AlumnoID + " AND ClaseAsistenciaClaseID = " + ClaseID + " AND ClaseAsistenciaProfesorID = " + _autenticado.UsuarioID + " AND ReservaID = " + ReservaID;
            string data = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
            if (data.Length == 0)
            {
                try
                {
                    cn.Open();
                    sSelectSQL = "INSERT INTO ClaseAsistencia(ReservaID,ClaseAsistenciaProfesorID,ClaseAsistenciaClaseID" +
                                    ",ClaseAsistenciaUsuarioID,ClaseAsistenciaPlantillaID,ClaseAsistActivo)"
                    + " VALUES (" + ReservaID + "," + _autenticado.UsuarioID + "," + ClaseID + "," + AlumnoID + "," + ClasePLantillaID + ",'" + chk.Checked + "')";
                    SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
                    iRes = addCmd.ExecuteNonQuery();
                    if (iRes > 0)
                    {
                        band = true;
                    }
                    cn.Close();

                }
                catch (SqlException sq)
                {
                    cn.Close();
                    Err = sq.Message;
                    band = false;
                    break;
                }
            }
            if (band)
                MostrarVolver();
            else
                MostrarMsjModal("Error: " + Err, "ERR");
        }

    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("Asistencias.aspx");
    }
}