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

public partial class sistema_Vencidos : System.Web.UI.Page
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
            BindGridView();
        }

        protected void BindGridView()
        {
            try
            {
                cn.Open();
                string cmd2 = "SELECT  dbo.Usuario.UsuarioCedula as UsuarioCedula, "+
                              "  (dbo.Usuario.UsuarioNombre+' '+dbo.Usuario.UsuarioApellido) as UsuarioNombres, "+
                              "  dbo.[Plan].PlanNombre as PlanNombre, "+
                              "  CONVERT(VARCHAR(11),dbo.PlanAlumno.PlanAlumnoFechaFin,103) as FechaFinal,"+
                              "  DATEDIFF(DAY, PlanAlumnoFechaFin, SYSDATETIME()) as DiasVencido " +
                              "  FROM    dbo.PlanAlumno INNER JOIN "+
                              "  dbo.Usuario ON dbo.PlanAlumno.UsuarioID = dbo.Usuario.UsuarioID INNER JOIN "+
                              "  dbo.UsuarioRol ON dbo.Usuario.UsuarioID = dbo.UsuarioRol.UsuarioID INNER JOIN "+
                              "  dbo.[Plan] ON dbo.PlanAlumno.PlanID = dbo.[Plan].PlanID "+
                              "  WHERE (dbo.PlanAlumno.ClienteID = 1) "+
                              "  AND CONVERT(DATE, PlanAlumnoFechaFin, 103) < CONVERT(DATE, SYSDATETIME(), 103) "+
                              " AND dbo.PlanAlumno.PlanAlumnoID = "+
                              " (SELECT TOP 1 PlanAlumnoID FROM PlanAlumno WHERE UsuarioID = dbo.PlanAlumno.UsuarioID ORDER BY PlanAlumnoID DESC ) "+
                              " " + ViewState["sWhere"] + " " +
                              "  ORDER BY dbo.PlanAlumno.PlanAlumnoFechaFin DESC";
                SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
                DataSet ds = new DataSet();
                dAdapter.Fill(ds);
                dt = ds.Tables[0];
                string[] TablaID = new string[1];
                TablaID[0] = "UsuarioCedula";
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

        protected void dplDias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplDias.SelectedValue == "1")
                ViewState["sWhere"] = " AND (DATEDIFF(DAY, PlanAlumnoFechaFin, SYSDATETIME())>=1 AND DATEDIFF(DAY, PlanAlumnoFechaFin, SYSDATETIME()) <=30)";
            else if (dplDias.SelectedValue == "2")
                ViewState["sWhere"] = " AND (DATEDIFF(DAY, PlanAlumnoFechaFin, SYSDATETIME())>=1 AND DATEDIFF(DAY, PlanAlumnoFechaFin, SYSDATETIME()) <=60)";
            else if (dplDias.SelectedValue == "")
                ViewState["sWhere"] = " "; 
            BindGridView();

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridView1.SelectedIndex = -1;
            GridView1.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void ImgbtnArchivo_Click(object sender, ImageClickEventArgs e)
        {
            string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
            grid = new GridView();
            cn.Open();
            string cmd2 = "SELECT  dbo.Usuario.UsuarioCedula as UsuarioCedula, "+
                              "  (dbo.Usuario.UsuarioNombre+' '+dbo.Usuario.UsuarioApellido) as UsuarioNombres, "+
                              "  dbo.[Plan].PlanNombre as PlanNombre, "+
                              "  CONVERT(VARCHAR(11),dbo.PlanAlumno.PlanAlumnoFechaFin,103) as FechaFinal,"+
                              "  DATEDIFF(DAY, PlanAlumnoFechaFin, SYSDATETIME()) as DiasVencido " +
                              "  FROM    dbo.PlanAlumno INNER JOIN "+
                              "  dbo.Usuario ON dbo.PlanAlumno.UsuarioID = dbo.Usuario.UsuarioID INNER JOIN "+
                              "  dbo.UsuarioRol ON dbo.Usuario.UsuarioID = dbo.UsuarioRol.UsuarioID INNER JOIN "+
                              "  dbo.[Plan] ON dbo.PlanAlumno.PlanID = dbo.[Plan].PlanID "+
                              "  WHERE (dbo.PlanAlumno.ClienteID = 1) "+
                              "  AND CONVERT(DATE, PlanAlumnoFechaFin, 103) < CONVERT(DATE, SYSDATETIME(), 103) "+
                              " " + ViewState["sWhere"] + " " +
                              "  ORDER BY dbo.PlanAlumno.PlanAlumnoFechaFin DESC";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "UsuarioCedula";
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
            Response.AddHeader("Content-Disposition", "attachment;filename=planes_vencidos_" + Hora + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
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
    