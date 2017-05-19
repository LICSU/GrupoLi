using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

public partial class sistema_Plantilla : System.Web.UI.Page
{
    DataTable dt;
    string sSelect = "", Err = "", sSelectSQL = "";
    GridView grid;
    SqlDataAdapter dAdapter;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGridView();
        if (!IsPostBack)
        {
        }
    }
    protected void BindGridView()
    {
        try
        {
            cn.Close();
            cn.Open();
            string cmd2 = "SELECT ClasePlantilla.ClasePlantillaID as ClasePlantillaID, " +
            " dbo.Clase.ClaseDescripcion as ClaseDescripcion, " +
            " dbo.Cliente.ClienteNombre as ClienteDescripcion, " +
            " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora, " +
            " ClasePlantilla.ClasePlantillaCupo as ClasePlantillaCupo," +
            " ClasePlantilla.ClasePlantillaActiva as ClasePlantillaActiva," +
            " (SELECT SalonNombre From Salon WHERE SalonID = ClasePlantilla.SalonID) as SalonNombre," +
            " ClasePlantilla.ClaseID as ClaseID, " +
            " ClasePlantilla.ProfesorID as ProfesorID," +
            " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha," +
            " ClasePlantilla.ClienteID as ClienteID, " +
            " (SELECT (UsuarioNombre+' '+UsuarioApellido) as UsuarioNombre FROM Usuario WHERE UsuarioID = ClasePlantilla.ProfesorID) as UsuarioNombre " +
            " FROM dbo.Clase INNER JOIN" +
            " ClasePlantilla ON dbo.Clase.ClaseID = ClasePlantilla.ClaseID INNER JOIN" +
            " dbo.Cliente ON ClasePlantilla.ClienteID = dbo.Cliente.ClienteID INNER JOIN" +
            " dbo.Usuario ON ClasePlantilla.ProfesorID = dbo.Usuario.UsuarioID " +
            " WHERE (CONVERT(DATE,ClasePlantilla.ClasePlantillaFecha,103)  >= CONVERT(DATE, SYSDATETIME(), 103)) " +
            " " + ViewState["FiltroBusqueda"] + " ORDER BY ClasePlantillaFecha, ClasePlantillaHora";
            //MostrarMsjModal(cmd2, "");
            dAdapter = new SqlDataAdapter(cmd2, cn);
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

    protected void btnCargarPlantilla_Click(object sender, EventArgs e)
    {
        if (plantillaExcel.HasFile)
        {
            string fileExt = System.IO.Path.GetExtension(plantillaExcel.FileName);
            if (fileExt == ".csv" || fileExt == ".CSV")
            {
                plantillaExcel.SaveAs(HttpContext.Current.Server.MapPath(plantillaExcel.FileName));
                string rutaFile = HttpContext.Current.Server.MapPath(plantillaExcel.FileName).ToString();
                StreamReader lector = new StreamReader(rutaFile);
                try
                {
                    do
                    {
                        int iRes = 0;
                        try
                        {
                            string lineaArchivo = lector.ReadLine();
                            string[] vectorLinea = lineaArchivo.Split(';');
                            string claseFecha = vectorLinea[0];
                            string claseHora = vectorLinea[1];
                            string claseEstado = vectorLinea[2];
                            string claseID = vectorLinea[3];
                            string claseCupos = vectorLinea[4];
                            string claseDocente = vectorLinea[5];
                            string claseEmpresa = vectorLinea[6];
                            string SalonID = vectorLinea[7];

                            try
                            {
                                sSelect = "INSERT INTO ClasePlantilla(ClasePlantillaHora,ClasePlantillaCupo, ClasePlantillaActiva," +
                            " ClaseID, ProfesorID, ClasePlantillaFechaRegistro, ClasePlantillaUsuarioRegistro, ClienteID, ClasePlantillaFecha, SalonID) VALUES ('" + claseHora + "'," +
                            "" + claseCupos + "," + claseEstado + "," + claseID + "," + claseDocente + ",SYSDATETIME(),1, " + claseEmpresa + ",'" + claseFecha + "', " + SalonID + ")";
                                cn.Open();
                                SqlCommand addCmd = new SqlCommand(sSelect, cn);
                                iRes = addCmd.ExecuteNonQuery();
                                cn.Close();
                                string ClasePlantillaID = Utilidades.EjeSQL("SELECT MAX(ClasePlantillaID) FROM ClasePlantilla", cn, ref Err, true);
                                //string ClaseID = Utilidades.EjeSQL("SELECT ClaseID From ClasePlantilla WHERE ClasePlantillaID = " + ClasePlantillaID, cn, ref Err, true);
                                string ClaseID = claseID;
                                string Filas = "", Columnas = "";
                                Filas = Utilidades.EjeSQL("SELECT ClaseFilas FROM Clase WHERE ClaseID = " + ClaseID, cn, ref Err, true);
                                //MostrarMsjModal(Filas, "");
                                if (Filas != "")
                                {
                                    Columnas = Utilidades.EjeSQL("SELECT ClaseColumnas FROM Clase WHERE ClaseID = " + ClaseID, cn, ref Err, true);
                                    insertarEspacios(Filas, Columnas, ClasePlantillaID, claseHora, claseID);
                                }


                            }
                            catch (SqlException sq)
                            {
                                MostrarMsjModal("Alguna(s) Clase(s) ya están registradas verifique nuevamente el archivo: " + sq.Message, "ERR");
                                lector.Dispose();
                                lector.Close();
                                System.IO.File.Delete(rutaFile);
                                cn.Close();
                            }

                        }
                        catch (Exception sq)
                        {
                            MostrarMsjModal("Error en el formato del Documento: " + sq.Message, "ERR");
                            lector.Dispose();
                            lector.Close();
                            System.IO.File.Delete(rutaFile);
                        }
                    } while (!lector.EndOfStream);
                    MostrarMsjModal("Plantilla Cargada con Éxito", "EXI");
                    BindGridView();
                    lector.Dispose();
                    lector.Close();
                    System.IO.File.Delete(rutaFile);
                }
                catch (ObjectDisposedException ex)
                {
                    MostrarMsjModal("Alguna(s) Clase(s) ya está verifique nuevamenete el archivo: " + ex.Message, "ERR");
                }
            }
            else
            {
                MostrarMsjModal("EL formato del documento debe ser TXT", "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Debe cargar un documento", "ERR");
        }

    }
    private void insertarEspacios(string Filas, string Columnas, string ClasePlantillaID, string ClaseHora, string ClaseID)
    {
        int filas = int.Parse(Filas), iRes = 0;
        int columnas = int.Parse(Columnas);
        int contador = 1, contador2 = 0;
        int Intervalo = 0;
        //DateTime horaInicial = DateTime.ParseExact(ClaseHora, "HH:mm", CultureInfo.InvariantCulture);
        string InservaloS = Utilidades.EjeSQL("SELECT ClaseIntervalo FROM Clase WHERE ClaseID = " + ClaseID, cn, ref Err, true);

        if (InservaloS != "")
        {
            string SensorS = Utilidades.EjeSQL("SELECT ClaseSensor FROM Clase WHERE ClaseID = " + ClaseID, cn, ref Err, true);
            string TiempoS = Utilidades.EjeSQL("SELECT TiempoCambio FROM Clase WHERE ClaseID = " + ClaseID, cn, ref Err, true);
            Intervalo = int.Parse(InservaloS);
            int Sensor = int.Parse(SensorS);
            int TiempoCambio = int.Parse(TiempoS);
            //DateTime horaFinal = DateTime.ParseExact(ClaseHora, "HH:mm", CultureInfo.InvariantCulture).AddMinutes(Intervalo);
            TimeSpan HoraInicial = TimeSpan.Parse(ClaseHora);
            TimeSpan HoraFinal = TimeSpan.Parse(ClaseHora).Add(TimeSpan.FromMinutes(Intervalo));
            for (int i = 1; i <= filas; i++)
            {
                for (int j = 1; j <= columnas; j++)
                {
                    contador2++;
                    sSelect = "INSERT INTO ClaseEspacio(ClasePlantillaID, Fila, Columna, Ocupado,UsuarioID, Turno, HoraInicial, HoraFinal) " +
                              " VALUES(" + ClasePlantillaID + "," + i + "," + j + ", 0,0," + contador + ", '" + HoraInicial + "', '" + HoraFinal + "')";
                    if (contador2 == Sensor)
                    {
                        HoraInicial = HoraFinal.Add(TimeSpan.FromMinutes(TiempoCambio));
                        HoraFinal = HoraInicial.Add(TimeSpan.FromMinutes(Intervalo));
                        contador++;
                        contador2 = 0;
                    }

                    try
                    {
                        cn.Open();
                        SqlCommand addCmd = new SqlCommand(sSelect, cn);
                        iRes = addCmd.ExecuteNonQuery();
                        cn.Close();
                    }
                    catch (SqlException sq)
                    {
                        MostrarMsjModal("Error: " + sq.Message, "ERR");
                        cn.Close();
                    }
                }
            }
        }
        else
        {
            for (int i = 1; i <= filas; i++)
            {
                for (int j = 1; j <= columnas; j++)
                {
                    sSelect = "INSERT INTO ClaseEspacio(ClasePlantillaID, Fila, Columna, Ocupado,UsuarioID, Turno, HoraInicial, HoraFinal) " +
                              " VALUES(" + ClasePlantillaID + "," + i + "," + j + ", 0,0," + contador + ", 0, 0)";

                    try
                    {
                        cn.Open();
                        SqlCommand addCmd = new SqlCommand(sSelect, cn);
                        iRes = addCmd.ExecuteNonQuery();
                        cn.Close();
                    }
                    catch (SqlException sq)
                    {
                        MostrarMsjModal("Error: " + sq.Message, "ERR");
                        cn.Close();
                    }
                }
            }
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("deleteRecord"))
        {
            //Eliminar Registro                
            hClaseDel.Value = (gvrow.FindControl("ClasePlantillaID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("editRecord"))
        {
            sSelect = "SELECT u.UsuarioID as VAL, (u.UsuarioNombre+' '+u.UsuarioApellido) as TXT FROM Usuario u, UsuarioRol ur WHERE (u.UsuarioID = ur.UsuarioID AND ur.RolID = 2) ORDER BY VAL desc";
            Utilidades.CargarListado(ref dplProfesor, sSelect, cn, ref Err, true);
            hClaseMod.Value = (gvrow.FindControl("ClasePlantillaID") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("ClaseDescripcion") as Label).Text;
            txtFechaEdit.Text = (gvrow.FindControl("ClasePlantillaFecha") as Label).Text;
            txtHoraEdit.Text = (gvrow.FindControl("ClasePlantillaHora") as Label).Text;
            txtCupoEdit.Text = (gvrow.FindControl("ClasePlantillaCupo") as Label).Text;
            //txtNombrePEdit.Text = (gvrow.FindControl("UsuarioNombre") as Label).Text;
            dplProfesor.SelectedValue = (gvrow.FindControl("ProfesorID") as Label).Text;
            txtEmpresaEdit.Text = (gvrow.FindControl("ClienteDescripcion") as Label).Text;
            chkActivaEdit.Checked = (gvrow.FindControl("chkActiva") as CheckBox).Checked;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fecha = txtFechaEdit.Text;
        string hora = txtHoraEdit.Text;
        string cupo = txtCupoEdit.Text;
        bool activa = chkActivaEdit.Checked;
        string ClasePlantillaID = hClaseMod.Value;
        int iRes = 0;
        //Validamos si cambio el cupo
        sSelect = "SELECT ClasePlantillaCupo FROM ClasePlantilla WHERE ClasePlantillaID = " + ClasePlantillaID;
        string cupoAnterior = Utilidades.EjeSQL(sSelect, cn, ref Err, true);
        if (int.Parse(cupo) > int.Parse(cupoAnterior))
        {
            //Se envia el correo a la lista de espera 
            EnviarAvisoLista(ClasePlantillaID);
        }
        cn.Open();
        string sSelectSQL = "UPDATE ClasePlantilla SET ClasePlantillaFecha = '" + fecha + "', " +
                            " ClasePlantillaHora = '" + hora + "' , " +
                            " ProfesorID = " + dplProfesor.SelectedValue + " , " +
                            " ClasePlantillaCupo = " + cupo + " , " +
                            " ClasePlantillaActiva ='" + activa + "' WHERE ClasePlantillaID = " + ClasePlantillaID;
        SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
        try { iRes = addCmd.ExecuteNonQuery(); }
        catch (SqlException sq)
        {
            Err = sq.Message;
            MostrarMsjModal(Err, "ERR");
        }
        cn.Close();
        if (iRes > 0)
        {
            BindGridView();
            MostrarMsjModal("Registro Modificado exitosamente", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeEdit').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
        }
        else
        {
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void EnviarAvisoLista(string ClasePlantillaID)
    {
        sSelectSQL = "SELECT COUNT(*) FROM ListaEspera WHERE ListaEsperaClaseID = " + ClasePlantillaID + " AND ListaEsperaEstado = 0";
        int tamanhoVector = int.Parse(Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true));
        int contador = 0;
        if (tamanhoVector > 0)
        {
            string[] ListaUsuarios = new string[tamanhoVector];
            string[] ListaEsperaID = new string[tamanhoVector];
            sSelectSQL = "SELECT * FROM ListaEspera WHERE ListaEsperaClaseID = " + ClasePlantillaID + " AND ListaEsperaEstado = 0";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sSelectSQL, cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ListaUsuarios[contador] = dr["ListaEsperaUsuarioID"].ToString();
                    ListaEsperaID[contador] = dr["ListaEsperaID"].ToString();
                    contador++;
                }
                cn.Close();
                sSelectSQL = "SELECT  (dbo.Clase.ClaseDescripcion+' '+dbo.ClasePlantilla.ClasePlantillaFecha+' '+dbo.ClasePlantilla.ClasePlantillaHora) as Clase " +
                              " FROM dbo.Clase INNER JOIN " +
                              " dbo.ClasePlantilla ON dbo.Clase.ClaseID = dbo.ClasePlantilla.ClaseID " +
                              " WHERE dbo.ClasePlantilla.ClasePlantillaID = " + ClasePlantillaID;
                string Clase = Utilidades.EjeSQL(sSelectSQL, cn, ref Err, true);
                for (int i = 0; i < tamanhoVector; i++)
                {
                    string CorreoAviso = Utilidades.EjeSQL("SELECT UsuarioCorreo FROM Usuario WHERE UsuarioID = " + ListaUsuarios[i], cn, ref Err, true);
                    if (Utilidades.EmailValido(CorreoAviso))
                    {
                        string Usuario = Utilidades.EjeSQL("SELECT (UsuarioNombre+' '+UsuarioApellido) as UsuarioN FROM Usuario WHERE UsuarioID = " + ListaUsuarios[i], cn, ref Err, true);
                        string mensaje = "Señor(a). " + Usuario + " \n Se ha habilitado un cupo en la clase: " + Clase + ". Ingrese pronto a inscribirse.";
                        Utilidades.EnviarCorreo(CorreoAviso, "¡Cupo Disponible!", mensaje, ref Err);
                    }
                    sSelectSQL = "UPDATE ListaEspera SET ListaEsperaEstado = 1 WHERE ListaEsperaID = " + ListaEsperaID[i];
                    Utilidades.EjeSQL(sSelectSQL, cn, ref Err, false);
                }

            }
            catch (SqlException sq)
            {
                MostrarMsjModal(sq.Message, "ERR");
                cn.Close();
            }
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string ClasePlantillaID = hClaseDel.Value;
        sSelect = "DELETE From ClaseEspacio WHERE ClasePlantillaID = " + ClasePlantillaID;
        Utilidades.EjeSQL(sSelect, cn, ref Err, false);
        int iRes = 0;
        cn.Open();
        string SQL_1 = "DELETE FROM ClasePlantilla Where ClasePlantillaID = " + ClasePlantillaID;
        try
        {
            SqlCommand addCmd = new SqlCommand(SQL_1, cn);
            iRes = addCmd.ExecuteNonQuery();
        }
        catch (SqlException sq)
        {
            MostrarMsjModal(sq.Message, "ERR");
        }
        cn.Close();
        if (iRes > 0)
        {
            BindGridView();
            MostrarMsjModal("Registro eliminado de la base de datos", "EXI");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("document.getElementById('closeDelete').click();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
        }
    }

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT ClasePlantilla.ClasePlantillaID as ClasePlantillaID, " +
            " dbo.Clase.ClaseDescripcion as ClaseDescripcion, " +
            " dbo.Cliente.ClienteNombre as ClienteDescripcion, " +
            " ClasePlantilla.ClasePlantillaHora as ClasePlantillaHora, " +
            " ClasePlantilla.ClasePlantillaCupo as ClasePlantillaCupo," +
            " ClasePlantilla.ClasePlantillaActiva as ClasePlantillaActiva," +
            " ClasePlantilla.ClaseID as ClaseID, " +
            " ClasePlantilla.ProfesorID as ProfesorID," +
            " ClasePlantilla.ClasePlantillaFecha as ClasePlantillaFecha," +
            " ClasePlantilla.ClienteID as ClienteID, " +
            " dbo.Usuario.UsuarioNombre as UsuarioNombre, " +
            " dbo.Usuario.UsuarioApellido as UsuarioApellido" +
            " FROM dbo.Clase INNER JOIN" +
            " ClasePlantilla ON dbo.Clase.ClaseID = ClasePlantilla.ClaseID INNER JOIN" +
            " dbo.Cliente ON ClasePlantilla.ClienteID = dbo.Cliente.ClienteID INNER JOIN" +
            " dbo.Usuario ON ClasePlantilla.ProfesorID = dbo.Usuario.UsuarioID";
        dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "ClasePlantillaID";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=plantilla_clase_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtBuscar.Value != "")
        {
            ViewState["FiltroBusqueda"] = " AND (CONVERT(DATE, ClasePlantilla.ClasePlantillaFecha,103) = CONVERT(DATE, '" + txtBuscar.Value + "', 103))";
            BindGridView();
        }
    }
}