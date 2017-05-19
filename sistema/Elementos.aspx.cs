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

public partial class sistema_Elementos : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    GridView grid;
    DataTable dt;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sSelectSQL = "SELECT ClaseID AS VAL, ClaseDescripcion AS TXT" +
                               " FROM Clase WHERE ClaseActiva = 1 ORDER BY TXT";
            Utilidades.CargarListado(ref dplClaseAdd, sSelectSQL, cn, ref Err, true);
            string sSelectSQL2 = "SELECT NivelID AS VAL, NivelNombre AS TXT" +
                               " FROM Nivel WHERE NivelActivo = 1 ORDER BY TXT";
            Utilidades.CargarListado(ref dplNivelAdd, sSelectSQL2, cn, ref Err, true);
        }
        BindGridView();
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();

            string cmd2 = "SELECT Elemento.ElementoID as ElementoID, Elemento.ElementoNombre as ElementoNombre," +
                         " Elemento.ElementoActivo as ElementoActivo, Clase.ClaseID as ClaseID, Clase.ClaseDescripcion as ClaseDescripcion," +
                         " Clase_Nivel_Elemento.ClaseElemNivID as ClaseElemNivID, Nivel.NivelNombre as NivelNombre, Nivel.NivelID as NivelID " +
                         " FROM  Clase INNER JOIN Clase_Nivel_Elemento ON Clase.ClaseID = Clase_Nivel_Elemento.ClaseID INNER JOIN " +
                         " Elemento ON Clase_Nivel_Elemento.ElementoID = Elemento.ElementoID INNER JOIN " +
                         " Nivel ON Clase_Nivel_Elemento.NivelID = Nivel.NivelID";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[2];
            TablaID[0] = "ElementoID";
            TablaID[1] = "ElementoNombre";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            if (dt.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[2].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone.  
                GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        catch (SqlException ex)
        {
            MostrarMsjModal("Error: " + ex.Message, "ERR");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " WHERE e.ElementoNombre LIKE '%" + sBuscar + "%'"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            MostrarMsjModal("Error: " + ex.Message, "ERR");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddModalScript", sb.ToString(), false);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    private void limpiar()
    {
        txtNombreAdd.Text = "";
    }

    private bool sonValidos(string Accion)
    {
        bool sRes = true;
        if (Accion == "UPDATE")
        {
            string Nombre = txtNombreEdit.Text;
            if (Nombre == "") { Err = "El Campo Nombre debe ser Obligatorio"; sRes = false; }
        }
        else
        {
            string Nombre = txtNombreAdd.Text;
            if (Nombre == "") { Err = "El Campo Nombre debe ser Obligatorio"; sRes = false; }
        }
        return sRes;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string ElementoNombre = txtNombreEdit.Text;
        bool Activa = chkActivoEdit.Checked;
        string ElementoID = hElementoMod.Value;
        int iRes = 0;
        if (sonValidos("UPDATE"))
        {
            cn.Open();
            string sSelectSQL = "UPDATE Elemento SET ElementoNombre ='" + ElementoNombre + "', ElementoActivo='" + Activa + "' WHERE ElementoID=" + ElementoID;
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try
            {
                iRes = addCmd.ExecuteNonQuery();
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
            }
            catch (SqlException sq)
            {
                Err = sq.Message;
                MostrarMsjModal(Err, "ERR");
            }
        }
        else
        {
            MostrarMsjModal(Err, "ERR");
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int iRes = 0;
        string ElementoID = hElementoID.Value;
        string ClaseElemNivID = hClaseElemNivID.Value;
        cn.Open();
        string SQL_2 = "DELETE FROM Clase_Nivel_Elemento Where ClaseElemNivID = " + ClaseElemNivID;
        SqlCommand addCmd2 = new SqlCommand(SQL_2, cn);
        int iRes2 = addCmd2.ExecuteNonQuery();
        if (iRes2 > 0)
        {
            string SQL_1 = "DELETE FROM Elemento Where ElementoID = " + ElementoID;
            SqlCommand addCmd = new SqlCommand(SQL_1, cn);
            try
            {
                iRes = addCmd.ExecuteNonQuery();
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
            catch (SqlException sq)
            {
                MostrarMsjModal("Error al eliminar el registro" + sq.Message, "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Error al eliminar el registro", "ERR");
        }


    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        string ElementoNombre = txtNombreAdd.Text;
        string[] ElementosNombre = ElementoNombre.Split(',');
        string ClaseID = dplClaseAdd.SelectedValue;
        string NivelID = dplNivelAdd.SelectedValue;
        bool Activo = ActivoAdd.Checked;
        int iRes = 0;

        if (sonValidos("INSERT"))
        {
            //cn.Open();
            string sSelectSQL = "";
            string maxElemento = "";
            string Query_1 = "SELECT MAX(ElementoID) as MAXIMO FROM Elemento";
            Utilidades.maxRegistro(ref maxElemento, Query_1, cn, ref Err);
            int maxELem = int.Parse(maxElemento.Trim()) + 1;
            string sSelectSQL1 = "";
            string sql = "";
            if (ElementosNombre.Length == 1)
            {
                sSelectSQL = "INSERT INTO Elemento (ElementoNombre, ElementoActivo, ElementoFechaRegistro, ElementoUsuarioRegistro) " +
                          " VALUES (" + Utilidades.SiEsNulo(ElementoNombre, "T") + ", '" + Activo + "',SYSDATETIME(),1)";

                sSelectSQL1 = "INSERT INTO Clase_Nivel_Elemento (ClaseID, ElementoID, NivelID, ClaseElemNivFechaRegistro , ClaseElemNivUsuarioRegistro) " +
                          " VALUES (" + ClaseID + "," + maxELem + "," + NivelID + ",SYSDATETIME(),1)";
            }
            else
            {
                sSelectSQL = "INSERT INTO Elemento (ElementoNombre, ElementoActivo, ElementoFechaRegistro, ElementoUsuarioRegistro) " +
                            " VALUES (" + Utilidades.SiEsNulo(ElementosNombre[0], "T") + ", '" + Activo + "',SYSDATETIME(),1)";

                sSelectSQL1 = "INSERT INTO Clase_Nivel_Elemento (ClaseID, ElementoID, NivelID, ClaseElemNivFechaRegistro , ClaseElemNivUsuarioRegistro) " +
                          " VALUES (" + ClaseID + "," + maxELem + "," + NivelID + ",SYSDATETIME(),1)";


                //(" + Utilidades.SiEsNulo(ElementoNombre, "T") + ", '" + Activo + "',SYSDATETIME(),1)";
                for (int i = 1; i < ElementosNombre.Length; i++)
                {
                    maxELem++;
                    sSelectSQL += ", (" + Utilidades.SiEsNulo(ElementosNombre[i], "T") + ", '" + Activo + "',SYSDATETIME(),1) ";
                    sSelectSQL1 += ", (" + ClaseID + "," + maxELem + "," + NivelID + ",SYSDATETIME(),1) ";
                    sql += sSelectSQL1;
                }
            }
            SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
            try
            {
                //Insertamos los Elementos
                cn.Open();
                iRes = addCmd.ExecuteNonQuery();
                cn.Close();
                if (iRes > 0)
                {
                    limpiar();
                    //Agregamos en la tabla Clase_Nivel_Elemento
                    cn.Open();
                    SqlCommand addCmd2 = new SqlCommand(sSelectSQL1, cn);
                    int iRes_1 = addCmd2.ExecuteNonQuery();
                    cn.Close();
                    if (iRes_1 > 0)
                    {
                        BindGridView();
                        MostrarMsjModal("Registro Agregado exitosamente", "EXI");
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("document.getElementById('closeAdd').click();");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
                    }
                    else
                    {
                        MostrarMsjModal("Error al Agregar los Clase_El: " + Err + " maxReg: " + sql, "ERR");
                    }

                }
            }
            catch (SqlException sq)
            {
                Err = sq.Message;
                MostrarMsjModal(Err, "ERR");
            }

        }
        else
        {
            MostrarMsjModal(Err, "ERR");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName.Equals("deleteRecord"))
        {
            //Eliminar Registro
            GridViewRow gvrow = GridView1.Rows[index];
            hElementoID.Value = (gvrow.FindControl("IDElemento_1") as Label).Text;
            hClaseElemNivID.Value = (gvrow.FindControl("ClaseElemNivID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("editRecord"))
        {
            //Modificar Registro
            GridViewRow gvrow = GridView1.Rows[index];
            hElementoMod.Value = (gvrow.FindControl("IDElemento_1") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("Nombre") as Label).Text;
            chkActivoEdit.Checked = (gvrow.FindControl("Activo") as CheckBox).Checked;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
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

    protected void btnArchivo_Click(object sender, EventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();

        string cmd2 = "SELECT Elemento.ElementoNombre as ElementoNombre," +
                     " Elemento.ElementoActivo as ElementoActivo, Clase.ClaseDescripcion as ClaseDescripcion," +
                     " Nivel.NivelNombre as NivelNombre " +
                     " FROM  Clase INNER JOIN Clase_Nivel_Elemento ON Clase.ClaseID = Clase_Nivel_Elemento.ClaseID INNER JOIN " +
                     " Elemento ON Clase_Nivel_Elemento.ElementoID = Elemento.ElementoID INNER JOIN " +
                     " Nivel ON Clase_Nivel_Elemento.NivelID = Nivel.NivelID";
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        DataSet ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "ElementoNombre";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=elementos_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}