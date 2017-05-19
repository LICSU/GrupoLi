using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de UsuarioAutenticado
/// </summary>
public class UsuarioAutenticado
{
    public string UsuarioID { get; set; }
    public string Nombre { get; set; }
    public string SucursalID { get; set; }
    public string ClienteID { get; set; }
    public string PlanID { get; set; }
    public string RolID { get; set; }
    //
    public UsuarioAutenticado(System.Web.Security.FormsIdentity fIdentity)
    {
        string[] usuarioData = fIdentity.Ticket.Name.Split('|');
        UsuarioID = usuarioData[0];
        Nombre = usuarioData[1];
        SucursalID = usuarioData[2];
        ClienteID = usuarioData[3];
        PlanID = usuarioData[4];
        RolID = fIdentity.Ticket.UserData; //int.Parse(
    }
	public UsuarioAutenticado()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}