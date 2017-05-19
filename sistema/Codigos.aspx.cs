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

public partial class sistema_Codigos : System.Web.UI.Page
{
    string Err = "";
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridViewClases();
        }
    }

    protected void BindGridViewSalones()
    {
        try
        {
            cn.Open();
            string cmd2 = "Select SalonID as Codigo, SalonNombre as Nombre from Salon " + ViewState["sWhere"] + " order by Codigo";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Codigo";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            MostrarMsjModal(ex.Message, "ERR");
        }
    }

    protected void BindGridViewClases()
    {
        try
        {
            cn.Open();
            string cmd2 = "Select ClaseID as Codigo, ClaseDescripcion as Nombre from Clase " + ViewState["sWhere"] + " order by Codigo";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Codigo";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            MostrarMsjModal(ex.Message, "ERR");
        }
    }

    protected void BindGridViewEmpresas()
    {
        try
        {
            cn.Open();
            string cmd2 = "Select ClienteID as Codigo, ClienteNombre as Nombre from Cliente " + ViewState["sWhere"] + " order by Codigo";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Codigo";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            MostrarMsjModal(ex.Message, "ERR");
        }
    }

    protected void BindGridViewProfesores()
    {
        try
        {
            cn.Open();
            string cmd2 = "Select DISTINCT(us.UsuarioID) as Codigo, (us.UsuarioNombre+' '+us.UsuarioApellido) as Nombre " +
                           " from Usuario us, UsuarioRol ur, Rol r where us.UsuarioID = ur.UsuarioID AND ur.RolID = 2 " + ViewState["sWhere"] + " order by Codigo";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "Codigo";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
        }
        catch (SqlException ex)
        {
            MostrarMsjModal(ex.Message, "ERR");
        }
    }

    protected void dpllistaCodigos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dpllistaCodigos.SelectedValue == "" || dpllistaCodigos.SelectedValue == "Clases")
        {
            BindGridViewClases();
        }
        else if (dpllistaCodigos.SelectedValue == "Profesores")
        {
            BindGridViewProfesores();
        }
        else if (dpllistaCodigos.SelectedValue == "Empresas")
        {
            BindGridViewEmpresas();
        }
        else if (dpllistaCodigos.SelectedValue == "Salones")
        {
            BindGridViewSalones();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        if (dpllistaCodigos.SelectedValue == "Clases" || dpllistaCodigos.SelectedValue == "")
        {
            BindGridViewClases();
        }
        else if (dpllistaCodigos.SelectedValue == "Profesores")
        {
            BindGridViewProfesores();
        }
        else if (dpllistaCodigos.SelectedValue == "Empresas")
        {
            BindGridViewEmpresas();
        }
        else if (dpllistaCodigos.SelectedValue == "Salones")
        {
            BindGridViewSalones();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            {
                if (dpllistaCodigos.SelectedValue == "Clases" || dpllistaCodigos.SelectedValue == "")
                {
                    ViewState["sWhere"] = " WHERE ClaseDescripcion LIKE '%" + sBuscar + "%'";
                    BindGridViewClases();
                }
                else if (dpllistaCodigos.SelectedValue == "Profesores")
                {
                    ViewState["sWhere"] = " AND (us.UsuarioNombre LIKE '%" + sBuscar + "%' OR us.UsuarioApellido LIKE '%" + sBuscar + "%')";
                    BindGridViewProfesores();
                }
                else if (dpllistaCodigos.SelectedValue == "Empresas")
                {
                    ViewState["sWhere"] = " WHERE ClienteNombre LIKE '%" + sBuscar + "%'";
                    BindGridViewEmpresas();
                }
                else if (dpllistaCodigos.SelectedValue == "Salones")
                {
                    ViewState["sWhere"] = " WHERE SalonNombre LIKE '%" + sBuscar + "%'";
                    BindGridViewSalones();
                }
                ViewState["sWhere"] = "";
            }
            else
            { ViewState["sWhere"] = ""; }

        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }
}