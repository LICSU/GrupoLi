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
using System.Web.UI.HtmlControls;

public partial class sistema_medical_medidas : System.Web.UI.Page
{
    FormsIdentity fIdentity = HttpContext.Current.User.Identity as FormsIdentity;
    UsuarioAutenticado _autenticado;
    SqlConnection cn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnStringMedical"].ConnectionString);
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString);
    string sSelectSql = "", Err = "";
    string cedula = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        cargarMedidas();
    }

    private void cargarMedidas()
    {
        sSelectSql = " SELECT TOP 1 CAST(valor as DECIMAL(9,2)) as Valor FROM Resultado " +
                         " INNER JOIN TipoItem ON Resultado.IdTipoItem = TipoItem.IdTipoItem WHERE TipoItem.IDTipoItem = 3 " +
                         " WHERE Cedula = " + cedula;

        string Talla = Utilidades.EjeSQL("SELECT TOP 1 CAST(valor as DECIMAL(9,2)) as Valor FROM Resultado " +
                                          "INNER JOIN TipoItem ON Resultado.IdTipoItem = TipoItem.IdTipoItem WHERE TipoItem.IDTipoItem = 3 AND Cedula = " + cedula, cn1, ref Err, true);

        string CirCuello = Utilidades.EjeSQL("SELECT TOP 1 CAST(valor as DECIMAL(9,2)) as Valor FROM Resultado " +
                                          "INNER JOIN TipoItem ON Resultado.IdTipoItem = TipoItem.IdTipoItem WHERE TipoItem.IDTipoItem = 5 AND Cedula = " + cedula, cn1, ref Err, true);

        string CirPecho = Utilidades.EjeSQL("SELECT TOP 1 CAST(valor as DECIMAL(9,2)) as Valor FROM Resultado " +
                                          "INNER JOIN TipoItem ON Resultado.IdTipoItem = TipoItem.IdTipoItem WHERE TipoItem.IDTipoItem = 9 AND Cedula = " + cedula, cn1, ref Err, true);

        string CirCintura = Utilidades.EjeSQL("SELECT TOP 1 CAST(valor as DECIMAL(9,2)) as Valor FROM Resultado " +
                                          "INNER JOIN TipoItem ON Resultado.IdTipoItem = TipoItem.IdTipoItem WHERE TipoItem.IDTipoItem = 19 AND Cedula = " + cedula, cn1, ref Err, true);

        string IndCaderaCin = Utilidades.EjeSQL("SELECT TOP 1 CAST(valor as DECIMAL(9,2)) as Valor FROM Resultado " +
                                          "INNER JOIN TipoItem ON Resultado.IdTipoItem = TipoItem.IdTipoItem WHERE TipoItem.IDTipoItem = 44 AND Cedula = " + cedula, cn1, ref Err, true);

        string CirCabeza = Utilidades.EjeSQL("SELECT TOP 1 CAST(valor as DECIMAL(9,2)) as Valor FROM Resultado " +
                                          "INNER JOIN TipoItem ON Resultado.IdTipoItem = TipoItem.IdTipoItem WHERE TipoItem.IDTipoItem = 68 AND Cedula = " + cedula, cn1, ref Err, true);

        string CirCadera = Utilidades.EjeSQL("SELECT TOP 1 CAST(valor as DECIMAL(9,2)) as Valor FROM Resultado " +
                                          "INNER JOIN TipoItem ON Resultado.IdTipoItem = TipoItem.IdTipoItem WHERE TipoItem.IDTipoItem = 110 AND Cedula = " + cedula, cn1, ref Err, true);

        if (Talla == "") lblTalla.Text = " - Mts."; else lblTalla.Text = Talla + " Mts.";
        if (CirCuello == "") lblCirCuello.Text = " - Cm."; else lblCirCuello.Text = CirCuello + " Cm.";
        if (CirPecho == "") lblCirPecho.Text = " - Cm"; else lblCirPecho.Text = CirPecho + " Cm.";
        if (CirCintura == "") lblCirCintura.Text = " - Cm"; else lblCirCintura.Text = CirCintura + " Cm.";
        if (IndCaderaCin == "") lblIndCad.Text = " - Cm"; else lblIndCad.Text = IndCaderaCin + " Cm.";
        if (CirCabeza == "") lblCirCabeza.Text = " - Cm"; else lblCirCabeza.Text = CirCabeza + " Cm.";
        if (CirCadera == "") lblCirCade.Text = " - Cm"; else lblCirCade.Text = CirCadera + " Cm.";
    }
}