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
using System.Web.Security;

public partial class sistema_cargarEmpresa : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    DataTable dt;
    string sSelect = "", Err = "", sSelectSQL = "";
    SqlDataAdapter dAdapter;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _autenticado = new UsuarioAutenticado(fIdentity);
            if (_autenticado.ClienteID == "1")
            {
                phUnidad.Visible = true;
                sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente WHERE ClienteID > 1 ORDER BY TXT ";
                Utilidades.CargarListado(ref dplUnidad, sSelectSQL, cn, ref Err, true);
                ViewState["valor"] = "";
            }
            else
            {
                phUnidad.Visible = false;
                sSelectSQL = "SELECT ClienteID AS VAL, ClienteNombre AS TXT FROM Cliente WHERE ClienteID = " + _autenticado.ClienteID + " ORDER BY TXT";
                Utilidades.CargarListado(ref dplUnidad, sSelectSQL, cn, ref Err, true);
                dplUnidad.Items.Remove(dplUnidad.Items.FindByValue(""));
                dplUnidad.Enabled = false;
                ViewState["valor"] = _autenticado.ClienteID;
            }

            BindGridView();
        }
    }

    protected void BindGridView()
    {
        if (ViewState["valor"].ToString() != "")
        {
            ViewState["cliente"] = " AND Cliente.ClienteID = " + ViewState["valor"];
        }
        try
        {
            cn.Close();
            cn.Open();
            string cmd2 = "SELECT EmpleadoEmp.EmpleadoID as EmpleadoID, "
                           + " EmpleadoEmp.EmpleadoCedula as EmpleadoCedula, "
                           + " EmpleadoEmp.EmpleadoNombre as EmpleadoNombre, "
                           + " EmpleadoEmp.EmpleadoCargo as EmpleadoCargo, "
                           + " EmpleadoEmp.EmpleadoNivel1 as EmpleadoNivel1, "
                           + " EmpleadoEmp.EmpleadoNivel2 as EmpleadoNivel2, "
                           + " EmpleadoEmp.EmpleadoNivel3 as EmpleadoNivel3, "
                           + " EmpleadoEmp.EmpleadoNivel4 as EmpleadoNivel4, "
                           + " EmpleadoEmp.EmpleadoEmail as EmpleadoEmail, "
                           + " EmpleadoEmp.EmpleadoFechaIng as EmpleadoFechaIng, "
                           + " Cliente.ClienteNombre as ClienteNombre "
                           + " FROM Cliente INNER JOIN"
                           + " EmpleadoEmp ON Cliente.ClienteID = EmpleadoEmp.ClienteID " + ViewState["sWhere"] + " " + ViewState["cliente"];
            dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "EmpleadoID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();

            if (dt.Rows.Count > 0)
            {
                //Attribute to show the Plus Minus Button.
                GridView1.HeaderRow.Cells[2].Attributes["data-class"] = "expand";

                //Attribute to hide column in Phone.                
                GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[6].Attributes["data-hide"] = "phone";

                //Adds THEAD and TBODY to GridView.
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch (SqlException ex)
        {
            Err += "Error al cargar el listado. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected void btnCargarPlantilla_Click(object sender, EventArgs e)
    {
        if (dplUnidad.SelectedValue != "")
        {
            string ClienteID = dplUnidad.SelectedValue;
            if (plantillaEmpleados.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(plantillaEmpleados.FileName);
                if (fileExt == ".csv" || fileExt == ".CSV")
                {
                    plantillaEmpleados.SaveAs(HttpContext.Current.Server.MapPath(plantillaEmpleados.FileName));
                    string rutaFile = HttpContext.Current.Server.MapPath(plantillaEmpleados.FileName).ToString();
                    StreamReader lector = new StreamReader(rutaFile);
                    do
                    {
                        int iRes = 0;
                        try
                        {
                            string lineaArchivo = lector.ReadLine();
                            string[] vectorLinea = lineaArchivo.Split(';');
                            string empleadoCedula = vectorLinea[0];
                            string empleadoNombre = vectorLinea[1];
                            string empleadoCargo = vectorLinea[2];
                            string empleadoNivel1 = vectorLinea[3];
                            string empleadoNivel2 = vectorLinea[4];
                            string empleadoNivel3 = vectorLinea[5];
                            string empleadoNivel4 = vectorLinea[6];
                            string empleadoArea = vectorLinea[7];
                            string empleadoEmail = vectorLinea[8];
                            string empleadoFecha = vectorLinea[9];
                            string empleadoTipo = vectorLinea[10];
                            sSelect = "INSERT INTO EmpleadoEmp (ClienteID,EmpleadoCedula,EmpleadoNombre,EmpleadoCargo,EmpleadoNivel1,EmpleadoNivel2,EmpleadoNivel3,EmpleadoNivel4,EmpleadoArea,EmpleadoEmail,EmpleadoFechaIng, EmpleadoTipo)"
                            + " VALUES(" + ClienteID + ",'" + empleadoCedula + "','" + empleadoNombre + "','" + empleadoCargo + "','" + empleadoNivel1 + "','" + empleadoNivel2 + "','" + empleadoNivel3 + "','" + empleadoNivel4 + "','" + empleadoArea + "','" + empleadoEmail + "','" + empleadoFecha + "', '" + empleadoTipo + "')";
                            cn.Open();
                            SqlCommand addCmd = new SqlCommand(sSelect, cn);
                            try
                            {
                                iRes = addCmd.ExecuteNonQuery();
                                BindGridView();
                            }
                            catch (SqlException sq)
                            {
                                MostrarMsjModal(sSelect + "; - " + sq.Message, "ERR");
                                // lector.Close();
                            }
                            cn.Close();
                        }
                        catch (Exception sq)
                        {
                            MostrarMsjModal("Error en el formato del Documento: " + sq.Message, "ERR");
                            lector.Dispose();
                            lector.Close();
                            System.IO.File.Delete(rutaFile);
                        }
                    } while (!lector.EndOfStream);
                    lector.Close();
                }
                else
                {
                    MostrarMsjModal("EL formato del documento debe ser CSV", "ERR");
                }
            }
            else
            {
                MostrarMsjModal("Debe cargar un documento", "ERR");
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvrow = GridView1.Rows[index];
        if (e.CommandName.Equals("editRecord"))
        {
            hdfEmpleadoIDEdit.Value = (gvrow.FindControl("EmpleadoID") as Label).Text;
            txtCedulaEdit.Text = (gvrow.FindControl("EmpleadoCedula") as Label).Text;
            txtNombreEdit.Text = (gvrow.FindControl("EmpleadoNombre") as Label).Text;
            txtCargoEdit.Text = (gvrow.FindControl("EmpleadoCargo") as Label).Text;
            txtEmailEdit.Text = (gvrow.FindControl("EmpleadoEmail") as Label).Text;
            txtNivel1Edit.Text = (gvrow.FindControl("EmpleadoNivel1") as Label).Text;
            txtNivel2Edit.Text = (gvrow.FindControl("EmpleadoNivel2") as Label).Text;
            txtNivel3Edit.Text = (gvrow.FindControl("EmpleadoNivel3") as Label).Text;
            txtNivel4Edit.Text = (gvrow.FindControl("EmpleadoNivel4") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("deleteRecord"))
        {
            hdfEmpleadoIDDel.Value = (gvrow.FindControl("EmpleadoID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ViewModalScript", sb.ToString(), false);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridView1.SelectedIndex = -1;
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " WHERE (Cliente.ClienteNombre LIKE '%" + sBuscar + "%' OR EmpleadoEmp.EmpleadoCedula LIKE '%" + sBuscar + "%' OR EmpleadoEmp.EmpleadoNombre LIKE '%" + sBuscar + "%')"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string EmpleadoID = hdfEmpleadoIDEdit.Value;
        string Nombre = txtNombreEdit.Text;
        string Cargo = txtCargoEdit.Text;
        string Email = txtEmailEdit.Text;
        string EmpladoNivel1 = txtNivel1Edit.Text;
        string EmpladoNivel2 = txtNivel2Edit.Text;
        string EmpladoNivel3 = txtNivel3Edit.Text;
        string EmpladoNivel4 = txtNivel4Edit.Text;
        int iRes = 0;
        cn.Open();
        string sSelectSQL = "UPDATE EmpleadoEmp SET EmpleadoEmp.EmpleadoNombre = '" + Nombre + "', EmpleadoEmp.EmpleadoCargo = '" + Cargo + "',"
                            + " EmpleadoEmp.EmpleadoEmail = '" + Email + "', EmpleadoNivel1 = '" + EmpladoNivel1 + "' " +
                            " , EmpleadoNivel2 = '" + EmpladoNivel2 + "' , EmpleadoNivel3 = '" + EmpladoNivel3 + "' , EmpleadoNivel4 = '" + EmpladoNivel4 + "' WHERE EmpleadoEmp.EmpleadoID = " + EmpleadoID;
        SqlCommand addCmd = new SqlCommand(sSelectSQL, cn);
        try { iRes = addCmd.ExecuteNonQuery(); }
        catch (SqlException sq)
        {
            Err += sq.Message;
            MostrarMsjModal("Error: " + Err, "ERR");
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
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string EmpleadoID = hdfEmpleadoIDDel.Value;
        cn.Open();
        string SQL_1 = "DELETE FROM EmpleadoEmp Where EmpleadoEmpID = " + EmpleadoID;
        SqlCommand addCmd = new SqlCommand(SQL_1, cn);
        int iRes = addCmd.ExecuteNonQuery();
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
}