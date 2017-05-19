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

public partial class sistema_AgregarArchivos : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    string Err = "", sSelecSQL = "";
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
            string cmd2 = "SELECT * FROM Archivos ORDER BY ArchivoID DESC";
            SqlDataAdapter dAdapter = new SqlDataAdapter(cmd2, cn);
            DataSet ds = new DataSet();
            dAdapter.Fill(ds);
            dt = ds.Tables[0];
            string[] TablaID = new string[1];
            TablaID[0] = "ArchivoID";
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

    protected void Agregar_Click(object sender, EventArgs e)
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

    protected string fechaActual()
    {
        string fecha = "";
        fecha = (DateTime.Today.Date).ToString("dd_MM_yyyy");
        return fecha;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Agregar archivos
        if (dplTipo.SelectedValue != "")
        {
            if (txtNombreAdd.Text != "")
            {
                if (flpArchivo.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(flpArchivo.FileName);
                    if (fileExt == ".doc" || fileExt == ".DOC" || fileExt == ".DOCX" || fileExt == ".docx" || fileExt == ".pdf" || fileExt == ".PDF")
                    {
                        //Guardar el archivo en Documentos y Guardar en la BD
                        string NombreArchivo = txtNombreAdd.Text + fileExt;
                        string savePath = Server.MapPath("~/Validado/Documentos/");
                        savePath += NombreArchivo;
                        flpArchivo.SaveAs(savePath);
                        sSelecSQL = "INSERT INTO Archivos (ArchivoNombre, TipoDocumento, FechaRegistro) " +
                                    " VALUES ('" + NombreArchivo + "', '" + dplTipo.SelectedValue + "', SYSDATETIME())";
                        Utilidades.EjeSQL(sSelecSQL, cn, ref Err, false);
                        if (Err != "")
                            MostrarMsjModal("Error: " + Err, "ERR");
                        else
                        {
                            BindGridView();
                            MostrarMsjModal("Registro Modificado exitosamente", "EXI");
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append(@"<script type='text/javascript'>");
                            sb.Append("document.getElementById('closeDelete').click();");
                            sb.Append(@"</script>");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);
                        }
                    }
                    else
                    {
                        MostrarMsjModal("Solo se aceptan archivos con extención .doc , .docx, .pdf", "ERR");
                    }
                }
                else
                {
                    MostrarMsjModal("Debe seleccionar un archivo", "ERR");
                }
            }
            else
            {
                MostrarMsjModal("Debe ingresar un nombre", "ERR");
            }
        }
        else
        {
            MostrarMsjModal("Debe seleccionar un tipo de documento", "ERR");
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
        if (e.CommandName.Equals("deleteRecord"))
        {
            //Eliminar Registro
            hdArchivoID.Value = (gvrow.FindControl("ArchivoID") as Label).Text;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal({ show: true });");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        sSelecSQL = "Select ArchivoNombre from Archivos where ArchivoID = " + hdArchivoID.Value;
        string nombre = Utilidades.EjeSQL(sSelecSQL, cn, ref Err, true);
        string savePath = Server.MapPath("~/Validado/Documentos/");
        string rutaFile = savePath + nombre;
        if (System.IO.File.Exists(rutaFile))
        {
            System.IO.File.Delete(rutaFile);
        }
        sSelecSQL = "DELETE FROM Archivos WHERE ArchivoID = " + hdArchivoID.Value;
        Utilidades.EjeSQL(sSelecSQL, cn, ref Err, false);
        if (Err == "")
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
            MostrarMsjModal("Error: " + Err, "ERR");
        }
    }
}