using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sistema_partes_header : System.Web.UI.UserControl
{
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    DataTable TodosSubMenu = new DataTable();
    string Err = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
        {
            _autenticado = new UsuarioAutenticado(fIdentity);
            if (!IsPostBack) {
                CargarMenuLateral();
            }
            if (_autenticado.RolID != "2" )
            {
                phMenu2.Visible = true;
                phMenu3.Visible = true;
            }
            else
            {
                phMenu2.Visible = false;
                phMenu3.Visible = false;
            }
            if (_autenticado.RolID == "2")
            {
                phMenu.Visible = false;
                phMaster.Visible = true;
                phNomaster.Visible = false;
            }
            else
            {
                phMaster.Visible = false;
                phNomaster.Visible = true;
            }
        }

    private void CargarMenuLateral()
        {
            TodosSubMenu = CargarTodosSubMenu();
            rptMenuLateral.DataSource = CargarMenu();
            rptMenuLateral.DataBind();
        }

    private DataTable CargarTodosSubMenu()
        {
            string connString = ConfigurationManager.AppSettings["conexion"].ToString();
            SqlConnection connection = new SqlConnection(connString);
            SqlCommand selectCommand = new SqlCommand("SELECT M.MenuID, MenuDescripcion, MenuIdPadre, MenuURL FROM Menu M INNER JOIN MenuRol MR ON M.MenuID=MR.MenuID INNER JOIN UsuarioRol UR ON UR.RolID=MR.RolID WHERE UR.SucursalID=" + _autenticado.SucursalID + " AND UR.RolID=" + _autenticado.RolID + " AND UR.UsuarioID=" + _autenticado.UsuarioID, connection);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
                return dt;
            }
            catch (SqlException e)
            {
                Err = "Error al cargar SubMenú. Detalle: " + e.Message;
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

    private DataTable CargarMenu()
        {
            if (Err == string.Empty)
            {
                string connString = ConfigurationManager.AppSettings["conexion"].ToString();
                SqlConnection connection = new SqlConnection(connString);
                SqlCommand selectCommand = new SqlCommand("SELECT M.MenuID, MenuDescripcion, MenuURL FROM Menu M INNER JOIN MenuRol MR ON M.MenuID=MR.MenuID INNER JOIN UsuarioRol UR ON UR.RolID=MR.RolID WHERE MenuIDPadre=0 AND UR.SucursalID=" + _autenticado.SucursalID + " AND UR.RolID=" + _autenticado.RolID + " AND UR.UsuarioID=" + _autenticado.UsuarioID + " ORDER BY MR.MenuRolID", connection);
                DataTable dt = new DataTable();
                //Imagen de perfil
                
                string SQL = "SELECT UsuarioFoto FROM Usuario WHERE UsuarioID = "+_autenticado.UsuarioID;
                string foto = Utilidades.EjeSQL(SQL, connection, ref Err, true);
                string ruta = "~/sistema/fotos/";
                if (foto.Length == 0)
                {
                    string sexo = Utilidades.EjeSQL("SELECT UsuarioSexo FROM Usuario WHERE UsuarioID = " + _autenticado.UsuarioID, connection, ref Err, true);
                    if(sexo == "M")
                        ruta = ruta + "hombre.jpg";
                    else
                        ruta = ruta + "mujer.jpg";
                    imgPerfil.ImageUrl = ruta;
                }
                else
                {
                    imgPerfil.ImageUrl = foto;
                }
                try
                {
                    connection.Open();
                    SqlDataReader reader = selectCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                    reader.Close();
                    return dt;
                }
                catch (SqlException e)
                {
                    Err = "Error al cargar Menú. Detalle: " + e.Message;
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
            else { return null; }
        }

    protected void rptMenuLateral_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    if (TodosSubMenu != null)
                    {                        
                        DataRowView drv = e.Item.DataItem as DataRowView;
                        string id = drv["MenuID"].ToString();
                        DataRow[] rows = TodosSubMenu.Select("MenuIDPadre=" + id, "MenuDescripcion");
                        if (rows.Length > 0)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("<ul class='nav' id='side-menu' style='line-height:30px !important;'>");
                            foreach (var item in rows)
                            {
                                string url = " "+item["MenuURL"];
                                sb.Append("<li style='padding-left: 20px;'><a href='"+url+"'><i class='fa fa-circle-o fa-fw '></i>" + item["MenuDescripcion"] + "</a></li>");
                            }
                            sb.Append("</ul>");
                            (e.Item.FindControl("ltrlSubMenuLateral") as Literal).Text = sb.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Err = "Error al cargar Menú Lateral. Detalle: " + ex.Message;
            }
        }

    protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            string sHttpCookie = FormsAuthentication.FormsCookieName;
            if (Request.Cookies[sHttpCookie] != null)
            {
                Response.Cookies[sHttpCookie].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.Redirect("~/index.aspx");
        }

}