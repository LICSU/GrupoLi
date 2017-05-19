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
using System.Xml;
using System.IO;
using System.Xml.Serialization;
public partial class sistema_XmlMedical : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "", sSelectSQL = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    string[] vecCuppos, vecTam1;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT DISTINCT(ClasePlantilla.ClasePlantillaID) as ClasePlantillaID," +
                           " Clase.ClaseDescripcion as ClaseDescripcion, " +
                           " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha, " +
                           " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora, " +
                           " Clase.ClaseIntervalo as ClaseIntervalo," +
                           " Clase.ClaseSensor as ClaseSensor, Clase.Estacion as Estacion" +
                           " FROM ClasePlantilla INNER JOIN" +
                           " Clase ON ClasePlantilla.ClaseID = Clase.ClaseID INNER JOIN" +
                           " Reserva ON ClasePlantilla.ClasePlantillaID = Reserva.ClasePlantillaID" +
                           " WHERE(NOT (Clase.ClaseSensor IS NULL)) AND (NOT (Clase.ClaseIntervalo IS NULL))" +
                           " ORDER BY ClasePlantilla.ClasePlantillaFecha ASC, ClasePlantilla.ClasePlantillaHora ASC";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ClasePlantillaID";
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("descargar"))
        {
            //Armar el XML con la data....
            Medical medical = new Medical();
            string ClasePlantillaID = (gvrow.FindControl("ClasePlantillaID") as Label).Text;
            string ClaseN = (gvrow.FindControl("ClaseDescripcion") as Label).Text;
            string Fecha = (gvrow.FindControl("ClasePlantillaFecha") as Label).Text;
            string Hora = (gvrow.FindControl("ClasePlantillaHora") as Label).Text;
            string Intervalo = (gvrow.FindControl("ClaseIntervalo") as Label).Text;
            string Sensor = (gvrow.FindControl("ClaseSensor") as Label).Text;
            string Estacion = (gvrow.FindControl("Estacion") as Label).Text;

            sSelectSQL = "Select COUNT(*)" +
                       " from reserva " +
                       " where claseplantillaid = " + ClasePlantillaID + "";
            int CantidadUsuarios = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));

            Clase clase = new Clase();
            clase.Nombre = ClaseN;
            clase.Fecha = Fecha;
            clase.Hora = Hora;
            clase.Sensor = Sensor;
            clase.Intervalo = int.Parse(Intervalo);
            clase.SensorNombre = Estacion;

            List<Usuario> lstUsuario = new List<Usuario>(CantidadUsuarios);

            sSelectSQL = "Select " +
                       " (SELECT UsuarioCedula FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as Cedula," +
                       " (SELECT UsuarioNombre+' '+UsuarioApellido FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as Nombre," +
                       " (SELECT CONVERT(VARCHAR(11), UsuarioFechaNacimiento, 103) as FN FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as FechaNac," +
                       " (SELECT UsuarioSexo FROM Usuario WHERE UsuarioID = Reserva.UsuarioID) as Sexo," +
                       " (SELECT Turno From ClaseEspacio Where ClasePlantillaID = " + ClasePlantillaID + " AND UsuarioID = Reserva.UsuarioID) as Turno," +
                       " (SELECT HoraInicial From ClaseEspacio Where ClasePlantillaID = " + ClasePlantillaID + " AND UsuarioID = Reserva.UsuarioID) as HoraInicial," +
                       " (SELECT HoraFinal From ClaseEspacio Where ClasePlantillaID = " + ClasePlantillaID + " AND UsuarioID = Reserva.UsuarioID) as HoraFinal" +
                       " from reserva " +
                       " where claseplantillaid = " + ClasePlantillaID + "";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string cedula = dr["Cedula"].ToString();
                    string nombre = dr["Nombre"].ToString();
                    string sexo = dr["Sexo"].ToString();
                    string fecnaNac = dr["FechaNac"].ToString();
                    int turno = int.Parse(dr["Turno"].ToString());
                    string horainicial = dr["HoraInicial"].ToString();
                    string horafinal = dr["HoraFinal"].ToString();
                    lstUsuario.Add(new Usuario() { Cedula = cedula, Nombre = nombre, Sexo = sexo, FechaNacimiento = fecnaNac, Turno = turno, HoraInicial = horainicial, HoraFinal = horafinal });
                }
            }
            catch (SqlException sq)
            {
                MostrarMsjModal(sq.Message, "ERR");
            }


            medical.Clase = clase;
            medical.Usuario = lstUsuario;

            XmlSerializer serializer = new XmlSerializer(typeof(Medical));
            TextWriter writer = new StreamWriter(Server.MapPath(Path.Combine(@"~/sistema/documentos", @"medical.xml")));
            serializer.Serialize(writer, medical);
            writer.Close();

            string savePath1 = Server.MapPath("Documentos/medical.xml");
            ViewState["MapPath"] = savePath1;
            ViewState["Nombre"] = "Documentos/medical.xml";
            MostrarDescarga();
        }
    }

    protected void ImgbtnArchivo_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

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

    private void MostrarDescarga()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "MostrarDescarga", "MostrarDescarga();", true);
    }
    private void QuitarDescarga()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "QuitarDescarga", "QuitarDescarga();", true);
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {

        System.IO.FileInfo toDownload = new System.IO.FileInfo(ViewState["MapPath"].ToString());
        Response.Clear();
        Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
        Response.AddHeader("Content-Length", toDownload.Length.ToString());
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(ViewState["Nombre"].ToString());
        Response.End();
        QuitarDescarga();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("XmlMedical.aspx");
    }
}