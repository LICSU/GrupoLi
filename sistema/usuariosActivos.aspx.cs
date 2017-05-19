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

public partial class sistema_usuariosActivos : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);

        if (!IsPostBack)
        {
            if (_autenticado.RolID == "1")
            {
                Utilidades.CargarListado(ref dplClientes, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente WHERE ClienteID > 1 ORDER BY VAL ", cn, ref Err, true);
                phClientes.Visible = true;
            }
            else if (_autenticado.RolID == "4")
            {
                Utilidades.CargarListado(ref dplClientes, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente WHERE ClienteID > 1 ORDER BY VAL ", cn, ref Err, true);
                dplClientes.SelectedValue = _autenticado.ClienteID;
                dplClientes.Enabled = false;
                phClientes.Visible = false;
                ViewState["sWhere"] = "AND dbo.UsuarioRol.ClienteID = " + _autenticado.ClienteID;
            }
            BindGridView();
        }
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            //USuarios Activos
            string cmd2 = "SELECT DISTINCT(Usuario.UsuarioID), " +
                          " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres, Usuario.UsuarioCedula as UsuarioCedula, " +
                          " Cliente.ClienteNombre as ClienteNombre,  " +
                          "  " +
                          " CONVERT(VARCHAR(11),PlanEmpresa.fechaUltimo,103) as fechaUltima, " +
                          " PlanEmpresa.TotalClases as TotalClases " +
                          " FROM PlanEmpresa INNER JOIN Usuario ON PlanEmpresa.UsuarioID = " +
                          " Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = " +
                          " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID " +
                          " WHERE (PlanEmpresa.PlanActivo = 1) AND Cliente.ClienteID > 1 " + ViewState["sWhere"] +
                          " ORDER BY Cliente.ClienteNombre, Usuario.UsuarioCedula ASC ";

            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioCedula";
            cn.Close();

            try
            {
                //Usuarios No Activos
                string Inactivos = "SELECT Usuario.UsuarioID as UsuarioId, " +
                          " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres, Usuario.UsuarioCedula as UsuarioCedula, " +
                          " Cliente.ClienteNombre as ClienteNombre, Cliente.ClienteID as ClienteID, " +
                          " PlanEmpresa.PlanActivo as PlanActivo, " +
                          " CONVERT(VARCHAR(11),PlanEmpresa.fechaUltimo,103) as fechaUltima, " +
                          " PlanEmpresa.TotalClases as TotalClases " +
                          " FROM PlanEmpresa INNER JOIN Usuario ON PlanEmpresa.UsuarioID = " +
                          " Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = " +
                          " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID " +
                          " WHERE (PlanEmpresa.PlanActivo = 0) AND Cliente.ClienteID > 1 " + ViewState["sWhere"] + " ORDER BY PlanEmpresa.fechaUltimo DESC";
                
                string InactivosCant = "SELECT COUNT(Usuario.UsuarioID) as UsuarioId " +
                          " FROM PlanEmpresa INNER JOIN Usuario ON PlanEmpresa.UsuarioID = " +
                          " Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = " +
                          " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID " +
                          " WHERE (PlanEmpresa.PlanActivo = 0) AND Cliente.ClienteID > 1 " + ViewState["sWhere"];

                string[] usuarioIn = new string[int.Parse(Utilidades.EjeSQL(InactivosCant, cn, ref Err, true))];
                cn.Open();
                SqlCommand cmd3 = new SqlCommand(Inactivos, cn);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                int contador = 0;
                while (dr3.Read())
                {
                    usuarioIn[contador] = dr3["UsuarioId"].ToString();
                    contador++;
                }
                cn.Close();
                //Comparar cada ID con los inactivos en PlanEmpresaHistorial
                for (int i = 0; i < usuarioIn.Length; i++)
                {
                    string query = "SELECT fechaUltimo FROM PlanEmpresa WHERE UsuarioID = " + usuarioIn[i];
                    int valido = CompararFechas(query);
                    if (valido == 1)
                    {
                        //Usuario Valido y se ingresa al reporte
                        DataRow datos = dt.NewRow();
                        datos["UsuarioCedula"] = Utilidades.EjeSQL("SELECT UsuarioCedula as UsuarioCedula FROM Usuario WHERE UsuarioID = " + usuarioIn[i], cn, ref Err, true);
                        datos["UsuarioNombres"] = Utilidades.EjeSQL("SELECT (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres FROM Usuario WHERE UsuarioID = " + usuarioIn[i], cn, ref Err, true);
                        datos["ClienteNombre"] = Utilidades.EjeSQL("SELECT  dbo.Cliente.ClienteNombre FROM dbo.Cliente " +
                                                      " INNER JOIN dbo.UsuarioRol ON dbo.Cliente.ClienteID = dbo.UsuarioRol.ClienteID INNER JOIN " +
                                                      " dbo.Usuario ON dbo.UsuarioRol.UsuarioID = dbo.Usuario.UsuarioID " +
                                                      "  WHERE (dbo.Usuario.UsuarioID = " + usuarioIn[i] + ")", cn, ref Err, true);
                        datos["fechaUltima"] = Utilidades.EjeSQL("SELECT TOP 1 CONVERT(VARCHAR(11),PlanEmpresaFecha,103) as PlanEmpresaFecha FROM PlanEmpresaHistorial WHERE PlanEmpresaUsuarioID = " + usuarioIn[i] + " ORDER BY PlanEmpresaFecha DESC", cn, ref Err, true);
                        datos["TotalClases"] = Utilidades.EjeSQL("SELECT TotalClases FROM PlanEmpresa WHERE UsuarioID = " + usuarioIn[i], cn, ref Err, true);
                        //ds.Tables["dt"].Rows.Add(datos);
                        dt.Rows.Add(datos);
                        //MostrarMsjModal(datos["UsuarioCedula"] + " " + datos["UsuarioNombres"] + " " + datos["ClienteNombre"] + "" + datos["fechaUltima"] + " " + datos["TotalClases"], "");

                    }
                }

                GridView1.DataKeyNames = TablaID;
                GridView1.DataSource = dt;
                GridView1.DataBind();

                if (dt.Rows.Count > 0)
                {
                    GridView1.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
                    //Attribute to hide column in Phone. 
                    GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                    GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                    //Adds THEAD and TBODY to GridView.
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MostrarMsjModal(ex.Message, "ERR");
            }


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
        GridViewRow gvrow = GridView1.Rows[index]; if (e.CommandName.Equals("Historial"))
        {
            hdfUsuarioID.Value = (gvrow.FindControl("UsuarioID") as Label).Text;
            BindGridView2();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#modalHistorial').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.EditIndex = -1;
        GridView2.SelectedIndex = -1;
        GridView2.PageIndex = e.NewPageIndex;
        BindGridView2();
    }

    protected void BindGridView2()
    {
        try
        {
            cn.Close();
            cn.Open();
            string cmd2 = "SELECT " +
                          " (SELECT UsuarioNombre+''+UsuarioApellido FROM Usuario WHERE USuarioID = ph.PlanEmpresaUsuarioID) as Usuario," +
                          " CAST(CASE WHEN PlanEmpresaPlanEstado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END as VARCHAR(10)) as Estado, " +
                          " CONVERT(VARCHAR(11), PlanEmpresaFecha, 103) as Fecha " +
                          " FROM PlanEmpresaHistorial ph WHERE ph.PlanEmpresaUsuarioID = " + hdfUsuarioID.Value + " ORDER BY PlanEmpresaHistorial DESC ";
            //MostrarMsjModal(cmd2, "");
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Usuario";
            GridView2.DataKeyNames = TablaID;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
            //MostrarMsjModal(Err, "ERR");
        }
    }

    protected int CompararFechas(string query)
    {
        DateTime fechaUsuario = DateTime.Parse(Utilidades.EjeSQL(query, cn, ref Err, true));//FECHA Meno
        DateTime fechaActual = DateTime.Today;//FECHA MAYOR
        int activo = 0;
        if (fechaActual.Month == fechaUsuario.Month)
        {
            //Mismo MES
            if (fechaUsuario.Day >= 3)
                activo = 1;
        }
        return activo;
    }
    protected void dplClientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dplClientes.SelectedValue != "")
        {
            ViewState["sWhere"] = " AND dbo.UsuarioRol.ClienteID = " + dplClientes.SelectedValue;
            BindGridView();
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

    protected void ImgbtnArchivo_Click(object sender, ImageClickEventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT Distinct(Usuario.UsuarioID) as UsuarioID,  " +
                          " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres, Usuario.UsuarioCedula as UsuarioCedula, " +
                          " PlanEmpresa.TotalClases as TotalClases, " +
                          " Cliente.ClienteNombre as ClienteNombre, " +
                          " CONVERT(VARCHAR(11),PlanEmpresa.fechaUltimo,103) as fechaUltima " +
                          " FROM PlanEmpresa INNER JOIN Usuario ON PlanEmpresa.UsuarioID = " +
                          " Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = " +
                          " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID " +
                          " WHERE (PlanEmpresa.PlanActivo = 1) AND Cliente.ClienteID > 1 " + ViewState["sWhere"] + " ORDER BY Cliente.ClienteNombre, Usuario.UsuarioCedula ASC ";
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "UsuarioCedula";

        try
        {
            //Usuarios No Activos
            string Inactivos = "SELECT Usuario.UsuarioID as UsuarioId, " +
                      " (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres, Usuario.UsuarioCedula as UsuarioCedula, " +
                      " Cliente.ClienteNombre as ClienteNombre, Cliente.ClienteID as ClienteID, " +
                      " PlanEmpresa.PlanActivo as PlanActivo, " +
                      " CONVERT(VARCHAR(11),PlanEmpresa.fechaUltimo,103) as fechaUltima, " +
                      " PlanEmpresa.TotalClases as TotalClases " +
                      " FROM PlanEmpresa INNER JOIN Usuario ON PlanEmpresa.UsuarioID = " +
                      " Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = " +
                      " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID " +
                      " WHERE (PlanEmpresa.PlanActivo = 0) AND Cliente.ClienteID > 1 " + ViewState["sWhere"] + " ORDER BY PlanEmpresa.fechaUltimo DESC";

            string InactivosCant = "SELECT COUNT(Usuario.UsuarioID) as UsuarioId " +
                      " FROM PlanEmpresa INNER JOIN Usuario ON PlanEmpresa.UsuarioID = " +
                      " Usuario.UsuarioID INNER JOIN UsuarioRol ON Usuario.UsuarioID = " +
                      " UsuarioRol.UsuarioID INNER JOIN Cliente ON UsuarioRol.ClienteID = Cliente.ClienteID " +
                      " WHERE (PlanEmpresa.PlanActivo = 0) AND Cliente.ClienteID > 1 " + ViewState["sWhere"];

            string[] usuarioIn = new string[int.Parse(Utilidades.EjeSQL(InactivosCant, cn, ref Err, true))];
            cn.Open();
            SqlCommand cmd3 = new SqlCommand(Inactivos, cn);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            int contador = 0;
            while (dr3.Read())
            {
                usuarioIn[contador] = dr3["UsuarioId"].ToString();
                contador++;
            }
            cn.Close();
            //Comparar cada ID con los inactivos en PlanEmpresaHistorial
            for (int i = 0; i < usuarioIn.Length; i++)
            {
                string query = "SELECT fechaUltimo FROM PlanEmpresa WHERE UsuarioID =" + usuarioIn[i];
                MostrarMsjModal(query, "");
                int valido = CompararFechas(query);
                if (valido == 1)
                {
                    //Usuario Valido y se ingresa al reporte
                    DataRow datos = dt.NewRow();
                    datos["UsuarioCedula"] = Utilidades.EjeSQL("SELECT UsuarioCedula as UsuarioCedula FROM Usuario WHERE UsuarioID = " + usuarioIn[i], cn, ref Err, true);
                    datos["UsuarioID"] = Utilidades.EjeSQL("SELECT UsuarioID as UsuarioID FROM Usuario WHERE UsuarioID = " + usuarioIn[i], cn, ref Err, true);
                    datos["UsuarioNombres"] = Utilidades.EjeSQL("SELECT (Usuario.UsuarioNombre+' '+Usuario.UsuarioApellido) as UsuarioNombres FROM Usuario WHERE UsuarioID = " + usuarioIn[i], cn, ref Err, true);
                    datos["ClienteNombre"] = Utilidades.EjeSQL("SELECT  dbo.Cliente.ClienteNombre FROM dbo.Cliente " +
                                                  " INNER JOIN dbo.UsuarioRol ON dbo.Cliente.ClienteID = dbo.UsuarioRol.ClienteID INNER JOIN " +
                                                  " dbo.Usuario ON dbo.UsuarioRol.UsuarioID = dbo.Usuario.UsuarioID " +
                                                  "  WHERE (dbo.Usuario.UsuarioID = " + usuarioIn[i] + ")", cn, ref Err, true);
                    datos["fechaUltima"] = Utilidades.EjeSQL("SELECT TOP 1 CONVERT(VARCHAR(11),PlanEmpresaFecha,103) as PlanEmpresaFecha FROM PlanEmpresaHistorial WHERE PlanEmpresaUsuarioID = " + usuarioIn[i], cn, ref Err, true);
                    datos["TotalClases"] = Utilidades.EjeSQL("SELECT TotalClases FROM PlanEmpresa WHERE UsuarioID = " + usuarioIn[i], cn, ref Err, true);
                    //ds.Tables["dt"].Rows.Add(datos);
                    dt.Rows.Add(datos);
                }
            }

            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            cn.Close();
            MostrarMsjModal(ex.Message, "ERR");
        }

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
        Response.AddHeader("Content-Disposition", "attachment;filename=Usuarios_Activos_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }
}