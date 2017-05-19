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

public partial class sistema_Bonos : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "";
    DataTable dt;
    DataSet ds;
    GridView grid;
    string[] vecCuppos, vecTam1;
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        _autenticado = new UsuarioAutenticado(fIdentity);
        BindGridView();
        if (!IsPostBack)
            Utilidades.CargarListado(ref dplCliente, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente ORDER BY VAL", cn, ref Err, true);
    }

    protected void BindGridView()
    {
        try
        {
            cn.Open();
            string cmd2 = "SELECT Bonos.BonoID as BonoID, " +
                           " Bonos.BonoNumero as BonoNumero, " +
                           " dbo.Cliente.ClienteID as ClienteID," +
                           " dbo.Cliente.ClienteNombre as ClienteNombre, " +
                           " Bonos.BonoEstado as BonoEstado, " +
                           " Bonos.BonoUsuarioID as BonoUsuarioID, " +
                           " CONVERT(VARCHAR(11),Bonos.FechaInicio,103) as FechaInicio, " +
                           " CONVERT(VARCHAR(11),Bonos.FechaFin,103) as FechaFin " +
                           " FROM Bonos INNER JOIN " +
                           " dbo.Cliente ON Bonos.BonoEmpresa = dbo.Cliente.ClienteID" + ViewState["sWhere"];
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "BonoID";
            GridView1.DataKeyNames = TablaID;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            cn.Close();
            if (dt.Rows.Count > 0)
            {
                GridView1.HeaderRow.Cells[3].Attributes["data-class"] = "expand";
                //Attribute to hide column in Phone. 
                GridView1.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
                GridView1.HeaderRow.Cells[1].Attributes["data-hide"] = "phone";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sBuscar = txtSearch.Text.Trim();
            if (sBuscar != "")
            { ViewState["sWhere"] = " WHERE n.NivelNombre LIKE '%" + sBuscar + "%'"; }
            else
            { ViewState["sWhere"] = ""; }
            BindGridView();
        }
        catch (Exception ex)
        {
            Err += "Error al buscar el registro. Detalle: " + ex.Message.Replace("'", "") + ". ";
        }
    }

    protected string generarCupon()
    {
        string bonoid = "";
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[15];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);

        bonoid = Utilidades.EjeSQL("SELECT BonoNumero FROM Bonos WHERE BonoNumero = " + finalString, cn, ref Err, true);
        if (bonoid == finalString)
            generarCupon();

        return finalString;
    }

    protected string[] generarCupon(int tamVector)
    {
        bool bandListo = false;
        vecCuppos = new string[int.Parse(dplCantidad.SelectedValue)];
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[15];
        var random = new Random();
        int cantidad = 0;

        do
        {
            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = chars[random.Next(chars.Length)];
            var finalString = new String(stringChars);
            var bonoid = Utilidades.EjeSQL("SELECT BonoNumero FROM Bonos WHERE BonoNumero = " + finalString, cn, ref Err, true);
            if (bonoid != finalString)
            {
                //Guardar en el vector...                    
                if (!noRepetido(vecCuppos, finalString))
                {
                    vecCuppos[cantidad] = finalString;
                    cantidad++;
                }
            }
            if (cantidad == int.Parse(dplCantidad.SelectedValue))
                bandListo = true;
        } while (!bandListo);

        return vecCuppos;
    }
    protected bool noRepetido(string[] vecCuppos, string finalString)
    {
        bool bandSi = false;
        for (int i = 0; i < vecCuppos.Length; i++)
        {
            if (vecCuppos[i] == finalString)
            {
                bandSi = true;
                break;
            }
        }
        return bandSi;
    }
    protected void Add_Click(object sender, ImageClickEventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal({ show: true });");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddModalScript", sb.ToString(), false);
    }

    protected void ImgbtnArchivo_Click(object sender, ImageClickEventArgs e)
    {
        string Hora = DateTime.Now.ToString("dd_MM_yyy_HH_mm");
        grid = new GridView();
        cn.Open();
        string cmd2 = "SELECT  " +
                           " Bonos.BonoNumero as BonoNumero, " +
                           " dbo.Cliente.ClienteNombre as ClienteNombre, " +
                           " CONVERT(VARCHAR(11),Bonos.FechaInicio,103) as FechaInicio, " +
                           " CONVERT(VARCHAR(11),Bonos.FechaFin,103) as FechaFin " +
                           " FROM Bonos INNER JOIN " +
                           " dbo.Cliente ON Bonos.BonoEmpresa = dbo.Cliente.ClienteID" + ViewState["sWhere"];
        SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
        ds = new DataSet();
        dAdapter.Fill(ds);
        dt = ds.Tables[0];
        string[] TablaID = new string[1];
        TablaID[0] = "BonoNumero";
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
        Response.AddHeader("Content-Disposition", "attachment;filename=bonos_" + Hora + ".xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();
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
            hdfBonoIDEli.Value = (gvrow.FindControl("BonoID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
        if (e.CommandName.Equals("editRecord"))
        {
            //Editar Registro            
            Utilidades.CargarListado(ref dplEmpresaEdit, "SELECT ClienteID as VAL, ClienteNombre as TXT FROM Cliente", cn, ref Err, true);
            hdfBonoID.Value = (gvrow.FindControl("BonoID") as Label).Text;
            txtNumeroEdit.Text = (gvrow.FindControl("BonoNumero") as Label).Text;
            txtFechaInicioEdit.Text = (gvrow.FindControl("FechaInicio") as Label).Text;
            txtFechaFinEdit.Text = (gvrow.FindControl("FechaFin") as Label).Text;
            dplEmpresaEdit.SelectedValue = (gvrow.FindControl("ClienteID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string BonoNumero = txtNumeroAdd.Text;
        string BonoEmpresa = dplCliente.SelectedValue;
        string FechaInicio = txtFechaInicio.Text;
        string FechaFin = txtFechaFinal.Text;
        int iRes = 0;
        if (BonoEmpresa == "" || FechaFin == "" || FechaInicio == "")
        {
            MostrarMsjModal("Todos los campos son obligatorios", "ERR");
        }
        else
        {
            FechaInicio = Utilidades.FecUni(FechaInicio);
            FechaFin = Utilidades.FecUni(FechaFin);
            string[] bonos = BonoNumero.Split(',');
            for (int i = 0; i < bonos.Length - 1; i++)
            {
                //Recorrer vector para guardar varios bonos..
                string sSelect = " INSERT INTO Bonos(BonoNumero, BonoEmpresa, BonoEstado, BonoUsuarioID, FechaInicio, FechaFin) VALUES(" +
                                " '" + bonos[i] + "', " + BonoEmpresa + ", 0, 0, CONVERT(DATETIME, '" + FechaInicio + "', 103), CONVERT(DATETIME, '" + FechaFin + "', 103))";

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

            if (iRes > 0)
            {
                BindGridView();
                MostrarMsjModal("Registro Agregado Exitosamente", "EXI");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("document.getElementById('closeEdit').click();");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string BonoID = hdfBonoIDEli.Value;
        Utilidades.EjeSQL("DELETE FROM Bonos WHERE BonoID = " + BonoID, cn, ref Err, false);
        BindGridView();
        MostrarMsjModal("Registro eliminado de la base de datos", "EXI");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('closeDelete').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);

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

    protected void dplCantidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Cuando seleccione se generan los cupos
        txtNumeroAdd.Text = "";
        vecTam1 = new string[int.Parse(dplCantidad.SelectedValue)];
        vecTam1 = generarCupon(int.Parse(dplCantidad.SelectedValue));
        txtNumeroAdd.Enabled = false;

        for (int i = 0; i < vecTam1.Length; i++)
            txtNumeroAdd.Text += vecTam1[i] + ", ";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string bonoID = hdfBonoID.Value;
        string fechaInicio = txtFechaInicioEdit.Text;
        string fechaFin = txtFechaFinEdit.Text;
        string bonoEmpresa = dplEmpresaEdit.SelectedValue;

        Utilidades.EjeSQL("UPDATE Bonos SET FechaInicio = '" + fechaInicio + "', FechaFin = '" + fechaFin + "', BonoEmpresa = " + bonoEmpresa + "  WHERE BonoID = " + bonoID, cn, ref Err, false);
        //MostrarMsjModal(Err, "");
        BindGridView();
        MostrarMsjModal("Registro Modificado Exitosamente", "EXI");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("document.getElementById('closeEdit').click();");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
    }
}