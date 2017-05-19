using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.IO;

/// <summary>
/// Descripción breve de Utilidades
/// </summary>
public class Utilidades
{
	public Utilidades()
	{

	}
    /**
        * Calculo de la Muestra 
        * @returns Tamaño de la muestra
        */
    public static int MuestraGeneral(int TamPoblacion, int nivelConfianza)
    {
        double TamMuestra = 0;
        //Nivel de Confianza Zeta 75%->1,15; 80%->1,28; 85%->1,44; 90%->1,65%; 95%->1,96; 97,5%->2,24; 99%->2,58
        double contanteZeta = 0;
        double epsilon = 0.05;
        double omega = 0.50;
        if (nivelConfianza == 90)
            contanteZeta = 1.65;
        else if (nivelConfianza == 95)
            contanteZeta = 1.96;
        else if (nivelConfianza == 99)
            contanteZeta = 2.58;

        TamMuestra = (TamPoblacion * (omega * omega) * (contanteZeta * contanteZeta)) / (((epsilon * epsilon) * (TamPoblacion - 1)) + ((omega * omega) * (contanteZeta * contanteZeta)));

        return (int)TamMuestra;
    }
    /**
     * Calculo del Nivel de confianza dado n
     * @returns Nivel de Confianza
     */
    public static string NivelConfianza(int TamPoblacion, int TamMuestra)
    {
        //Nivel de Confianza Zeta 75%->1,15; 80%->1,28; 85%->1,44; 90%->1,65%; 95%->1,96; 97,5%->2,24; 99%->2,58
        double contanteZeta = 0;
        double epsilon = 0.05;
        double omega = 0.50;
        string nivelConfianza = "";

        contanteZeta = Math.Sqrt((TamMuestra * (Math.Pow(epsilon, 2) * (TamPoblacion - 1))) / (Math.Pow(omega, 2) * (TamPoblacion - TamMuestra)));

        if (contanteZeta < 1.15) nivelConfianza = "Menos del 75%"; //Menos de 75%
        else if (contanteZeta >= 1.15 && contanteZeta < 1.28) nivelConfianza = "~75%";
        else if (contanteZeta >= 1.28 && contanteZeta < 1.44) nivelConfianza = "~80%";
        else if (contanteZeta >= 1.44 && contanteZeta < 1.65) nivelConfianza = "~85%";
        else if (contanteZeta >= 1.65 && contanteZeta < 1.96) nivelConfianza = "~90%";
        else if (contanteZeta >= 1.96 && contanteZeta < 2.24) nivelConfianza = "~95%";
        else if (contanteZeta >= 2.24 && contanteZeta < 2.58) nivelConfianza = "~97%";
        else if (contanteZeta >= 2.58) nivelConfianza = "~99%";

        return nivelConfianza;
    }
    //Correo de registro de alumnos licsu
    public static void EnviarCorreoRegLicsu(ref string err, string asunto, string mensaje, string destino, string NombreHorario, string NombreNormas)
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
        //string sSelectSQL = "";
        try
        {
            /*MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("info@licsu.com", "Administrador", Encoding.UTF8);
            mail.IsBodyHtml = true;
            mail.Subject = asunto;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(mensaje)))
            {
                mail.Body = reader.ReadToEnd();
            }
            mail.To.Add(destino);
            mail.Attachments.Add(new Attachment(NombreHorario));
            mail.Attachments.Add(new Attachment(NombreNormas));
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@licsu.com", "roberth123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            mail.Dispose();*/
        }
        catch (Exception ex)
        {
            err += "Correos de LICSU " + ex.Message;
        }
    }
    //      
    public static void EnviarCorreoRegLicsu(ref string err, string asunto, string mensaje, string destino, string NombreHorario, string NombreNormas, string NombreAutorizacion)
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
        //string sSelectSQL = "";
        try
        {
           /* MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("info@licsu.com", "Administrador", Encoding.UTF8);
            mail.IsBodyHtml = true;
            mail.Subject = asunto;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(mensaje)))
            {
                mail.Body = reader.ReadToEnd();
            }
            mail.To.Add(destino);
            //Adjuntar los archivos horario del mes actual y normas y recomendaciones                
            mail.Attachments.Add(new Attachment(NombreHorario));
            mail.Attachments.Add(new Attachment(NombreNormas));
            mail.Attachments.Add(new Attachment(NombreAutorizacion));
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@licsu.com", "roberth123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            mail.Dispose();*/
        }
        catch (Exception ex)
        {
            err += "Correos de LICSU " + ex.Message;
        }
    }
    public static void EnviarCorreoAviso(ref string err, string asunto, string mensaje)
    {
        try
        {
            /*MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("admin@licsu.com", "Fitness Li", Encoding.UTF8);
            mail.IsBodyHtml = true;
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.To.Add("academia@licsu.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@licsu.com", "roberth123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            mail.Dispose();*/
        }
        catch (Exception ex)
        {
            err += ex.Message;
        }
    }
    public static void EnviarCorreoDes(ref string err, string asunto, string mensaje, string destino)
    {
        try
        {
           /* MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("admin@licsu.com", "Fitness Li", Encoding.UTF8);
            mail.IsBodyHtml = true;
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.To.Add(destino);
            mail.Bcc.Add("academia@licsu.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@licsu.com", "roberth123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            mail.Dispose();*/
        }
        catch (Exception ex)
        {
            err += ex.Message;
        }
    }

    public static void EnviarCorreo(ref string err, string asunto, string mensaje, string destino, FileUpload fipAdjuntos)
    {
        /*-------------------------MENSAJE DE CORREO----------------------
        string rutaFile = "";
        try
        {
            //Configuración del Mensaje
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
            mail.From = new MailAddress("info@licsu.com", "Administrador", Encoding.UTF8);
            mail.IsBodyHtml = true;
            //Aquí ponemos el asunto del correo
            mail.Subject = asunto;
            //Aquí ponemos el mensaje que incluirá el correo
            mail.Body = mensaje;
            //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
            //mail.To.Add(destino);
            mail.Bcc.Add(destino);
            //string rutaFile = HttpContext.Current.Server.MapPath(fipAdjuntos.FileName).ToString();
            if (fipAdjuntos.HasFile)
            {
                fipAdjuntos.SaveAs(HttpContext.Current.Server.MapPath(fipAdjuntos.FileName));
                rutaFile = HttpContext.Current.Server.MapPath(fipAdjuntos.FileName).ToString();
                mail.Attachments.Add(new Attachment(rutaFile));
            }
            //Configuracion del SMTP
            SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
            //Especificamos las credenciales con las que enviaremos el mail
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@licsu.com", "roberth123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            mail.Dispose();
            try
            {
                if (System.IO.File.Exists(rutaFile))
                {
                    System.IO.File.Delete(rutaFile);
                }
            }
            catch (Exception ex)
            {
                err += ex.Message;
            }

        }
        catch (Exception ex)
        {
            err += ex.Message;
        }*/
    }

    public static void EnviarCorreo(string destino, string asunto, string mensaje, ref string err)
    {
        /*-------------------------MENSAJE DE CORREO----------------------

        try
        {
            //Configuración del Mensaje
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //Especificamos el correo desde el que se enviará el Email y el nombre de la persona que lo envía
            mail.From = new MailAddress("info@licsu.com", "Administrador", Encoding.UTF8);
            mail.IsBodyHtml = true;
            //Aquí ponemos el asunto del correo
            mail.Subject = asunto;
            //Aquí ponemos el mensaje que incluirá el correo
            mail.Body = mensaje;
            //Especificamos a quien enviaremos el Email, no es necesario que sea Gmail, puede ser cualquier otro proveedor
            mail.To.Add(destino);

            //Configuracion del SMTP
            SmtpServer.Port = 587; //Puerto que utiliza Gmail para sus servicios
            //Especificamos las credenciales con las que enviaremos el mail
            SmtpServer.Credentials = new System.Net.NetworkCredential("info@licsu.com", "roberth123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            err += ex.Message;
        }*/
    }

    public static Boolean EmailValido(String email)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, String.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static string EjeSQL(string query, SqlConnection connection, ref string err, bool EjecutaEscalar = true)
    {
        string functionReturnValue = null;
        SqlCommand cmd = new SqlCommand();
        object result = null;
        bool SinAbrir = true;
        try
        {
            if (connection.State == ConnectionState.Open)
                SinAbrir = false;
            if (SinAbrir)
                connection.Open();
            cmd.CommandText = query;
            cmd.Connection = connection;
            cmd.CommandTimeout = connection.ConnectionTimeout;
            if (EjecutaEscalar)
                result = cmd.ExecuteScalar();
            else
                result = cmd.ExecuteNonQuery();
            try
            {
                functionReturnValue = result.ToString().Trim();
                connection.Close();
            }
            catch (Exception)
            {
                functionReturnValue = "";
                connection.Close();
            }
        }
        catch (SqlException sqlex)
        {
            err += "Función EjeSQL, Error de ejecución del SQL: " + sqlex.Message;
            return "-1";
        }
        catch (Exception ex)
        {
            err += "Función EjeSQL, Error: " + ex.Message;
            return "-1";
        }
        finally
        {
            if (SinAbrir)
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
        return functionReturnValue;
    }
    //
    public static string FecUni(string sFecha)
    {
        string sRes = "";
        ////{
        sRes = sFecha.Substring(6, 4) + "-" + sFecha.Substring(0, 2) + "-" + sFecha.Substring(3, 2);
        //}
        return sRes;
    }
    //
    public static Boolean EsFecha(String fecha)
    {
        try
        {
            string sFechaUni = fecha.Substring(6, 4) + fecha.Substring(5, 1) + fecha.Substring(3, 2) + fecha.Substring(2, 1) + fecha.Substring(0, 2);
            DateTime.Parse(sFechaUni);
            return true;
        }
        catch
        {
            return false;
        }
    }
    //
    public static string SiEsNulo(string sValor, string sTipo)
    {
        string sRes = "";
        //T: texto  N: número  F: fecha   L: Lógico
        sValor = sValor.Trim();
        switch (sTipo)
        {
            case "T":
                if (sValor.Length == 0) { sRes = "NULL"; } else { sRes = "'" + sValor + "'"; };
                break;
            case "F":
                if (sValor.Length == 0) { sRes = "NULL"; } else { sRes = "'" + sValor + "'"; };
                break;
            case "N":
                if (sValor.Length == 0) { sRes = "NULL"; } else { sRes = sValor; };
                break;
            case "L":
                if (sValor.Length == 0) { sRes = "NULL"; } else { sRes = sValor; };
                break;
            default:
                sRes = sValor;
                break;
        }
        return sRes;
    }
    //
    public static string SiEsLogico(string sValor)
    {
        if (sValor == "True") return "1"; else return "0";
    }

    public static void rolUsuario(ref string rol, string sSelectSQL, SqlConnection cn)
    {
        string sRes = "";
        SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
        SqlDataReader reader;
        try
        {
            cn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
                rol = reader["ROL"].ToString();
            reader.Close();
            cn.Close();
        }
        catch (SqlException Sqlex)
        {
            sRes = "Error de SQL buscando el Mayor: " + Sqlex.Message;
        }
    }
    //      
    public static void maxRegistro(ref string max, string sSelectSQL, SqlConnection cn, ref string sErr)
    {
        string sRes = "";
        SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
        SqlDataReader reader;
        try
        {
            cn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
                max = reader["MAXIMO"].ToString();
            reader.Close();
            cn.Close();
        }
        catch (SqlException Sqlex)
        {
            sRes = "Error de SQL buscando el Mayor: " + Sqlex.Message;
        }
    }
    public static void InsertarRegistro(ref int resultado, string sSelectSQL, SqlConnection cn, ref string sErr)
    {
        string sRes = "";
        SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
        try
        {
            resultado = cmd.ExecuteNonQuery();
        }
        catch (SqlException Sqlex)
        {
            sRes = "Error de SQL cargando el Listado " + Sqlex.Message;
        }
    }
    public static void CargarListado(ref DropDownList lst, string sSelectSQL, SqlConnection cn, ref string sErr, bool bIndex0 = false)
    {
        string sRes = "";
        string seleccione = "";
        lst.Items.Clear();
        SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
        SqlDataReader reader;
        try
        {
            cn.Open();
            reader = cmd.ExecuteReader();
            Int16 i = 0;
            if (lst.ID == "txtRol" || lst.ID == "dplRol") seleccione = "Seleccione el Rol";
            if (lst.ID == "ddlPlan" || lst.ID == "dplClientes" || lst.ID == "dplUnidad" || lst.ID == "dplEmpresa") seleccione = "Seleccione La Unidad";
            if (lst.ID == "dplClaseAdd" || lst.ID == "dplClases" || lst.ID == "dplClaseProfesor" || lst.ID == "ddlClases") seleccione = "Seleccione La Clase";
            if (lst.ID == "ddlAlumnos") seleccione = "Seleccione El Alumno";
            if (lst.ID == "txtParroquiaAdd") seleccione = "Seleccione La Parroquia";
            if (lst.ID == "dplEmpresas") seleccione = "Seleccione la Empresa";
            if (lst.ID == "dplNivel" || lst.ID == "dplNivelEdit" || lst.ID == "dplNivelAdd" || lst.ID == "dplNivelMod") seleccione = "Seleccione el Nivel";
            if (lst.ID == "dplTipoCalificacion" || lst.ID == "dplTpoCal") seleccione = "Seleccione el Tipo";
            if (lst.ID == "dplTipoPar") seleccione = "Seleccione el Tipo";
            if (lst.ID == "dplPlan") seleccione = "Seleccione el Plan";
            if (lst.ID == "dplProfesor") seleccione = "Seleccione el Profesor";
            if (lst.ID == "ddlMeses") seleccione = "Seleccione el Mes";
            if (lst.ID == "ddlFechas") seleccione = "Seleccione la Fecha";
            if (lst.ID == "ddlCalificacion" || lst.ID == "dplCalificacionMod") seleccione = "Seleccione la Calificación";
            if (lst.ID == "ddlElementos" || lst.ID == "ddElementosEvaluados" || lst.ID == "ddlElementosaEvaluar") seleccione = "Seleccione el Elemento";
            if (lst.ID != "txtSucursal")
                if (bIndex0)
                {
                    lst.Items.Add(new ListItem(seleccione, ""));
                    i += 1;
                }
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["TXT"].ToString();
                newItem.Value = reader["VAL"].ToString();
                lst.Items.Add(newItem);
            }
            reader.Close();
            if (lst.Items.Count == 1) { lst.Enabled = false; } else { lst.Enabled = true; }
        }
        catch (SqlException Sqlex)
        {
            sRes = "Error de SQL cargando el Listado " + lst.ID + ", detalle: " + Sqlex.Message;
        }
        catch (Exception err)
        {
            sRes = "Error cargando el Listado " + lst.ID + ", detalle: " + err.Message;
        }
        finally
        {
            cn.Close();
        }
        sErr += sRes;
    }
}