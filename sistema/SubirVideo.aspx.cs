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
using System.Web.UI.HtmlControls;

public partial class SubirVideo : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    string Err = "", sSelectSQL = "";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    UsuarioAutenticado _autenticado;
    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        if (!IsPostBack)
        {
            sSelectSQL = "SELECT DISTINCT(c.ClaseID) as VAL, c.ClaseDescripcion as TXT FROM Clase c, ClasePlantilla cp WHERE cp.ProfesorID = " + _autenticado.UsuarioID;
            Utilidades.CargarListado(ref dplClases, sSelectSQL, cn, ref Err, true);
            sSelectSQL = "SELECT NombreVideo FROM Videos WHERE UsuarioID = " + _autenticado.UsuarioID;
            string ultVideo = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
            if (ultVideo != "-1")
                lblFechaUltimo.Text = ultVideo;
            else
                lblFechaUltimo.Text = "No ha Subido Videos Aún";
        }
    }
    protected string fechaActual()
    {
        string fecha = "";
        fecha = (DateTime.Today.Date).ToString("dd_MM_yyyy");
        return fecha;
    }
    protected void btnSubir_Click(object sender, EventArgs e)
    {
        if (dplClases.SelectedValue != "")
        {
            if (flpVideos.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(flpVideos.FileName);
                string NombreArchivo = dplClases.SelectedValue + "_" + _autenticado.UsuarioID + "_" + fechaActual();
                //MostrarMsjModal(NombreArchivo, "");
                //VALIDAR EL FORMATO DEL VIDEO
                if (fileExt == ".txt")
                {
                    try
                    {
                        string savePath = Server.MapPath("~/videos/");
                        savePath += NombreArchivo + fileExt;
                        flpVideos.SaveAs(savePath);
                        //INSERTAMOS EN LA BD
                        sSelectSQL = "SELECT VideoID FROM Videos WHERE UsuarioID = " + _autenticado.UsuarioID + " AND ClaseID = " + dplClases.SelectedValue;
                        string videoID = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
                        //MostrarMsjModal(videoID, "");
                        if (videoID != "1")
                        {
                            //HACEMOS INSERT        
                            sSelectSQL = "INSERT INTO Videos(NombreVideo, FechaVideo, UsuarioID, ClaseID) " +
                                "VALUES('" + NombreArchivo + "',SYSDATETIME()," + _autenticado.UsuarioID + "," + dplClases.SelectedValue + ")";
                        }
                        else
                        {
                            //HACEMOS UPDATE
                            sSelectSQL = "UPDATE Videos SET NombreVideo = '" + NombreArchivo + "' WHERE VideoID = " + videoID;
                        }
                        string valor = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                        if (valor != "-1")
                            MostrarMsjModal("Video Subido con Éxito", "EXI");
                        else
                            MostrarMsjModal("Error: " + Err, "ERR");
                        dplClases.SelectedValue = "";
                    }
                    catch (Exception ex)
                    {
                        MostrarMsjModal("Error en la carga: " + ex.Message, "ERR");
                    }
                }
                else
                {
                    MostrarMsjModal("El formato del video no es válido", "ERR");
                }
            }
            else
            {
                MostrarMsjModal("Debe Seleccionar un video.", "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Seleccione un Clase", "ERR");
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
}