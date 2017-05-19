<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="sistema_Inicio" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />

<div id="page-wrapper" style="min-height: 292px; padding-top:130px;">    
    <!-- Contenido -->
    <asp:PlaceHolder runat="server" ID="phDesactivar" Visible="false">
        <div class="row" style="padding-top: 15px;">
            <div class="col-lg-6"></div>
            <div class="col-lg-6" style="text-align:right;">
                    <asp:Button ID="btnDesactivar" CssClass="btn btn-purple" runat="server" OnClick="btnDesactivar_Click" />          
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phDescuento" Visible="false">
        <div class="row">
            <div class="col-lg-6"></div>
            <div class="col-lg-6 text-right" >
                <asp:CheckBox runat="server" ID="chkDescuento" CssClass="checkbox derecha filled-in"  AutoPostBack="true" OnCheckedChanged="chkDescuento_CheckedChanged" /> 
                <label for="chkDescuento" style="color:#8E248C;"></label>     
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phAdministrador" runat="server" Visible="false"> 
    <div class="row">
        <div class="col-md-12">            
            <h5>Búsqueda Individual</h5>
            <div class="input-field col s6">
                <asp:TextBox runat="server" ID ="txtBuscar"></asp:TextBox>
                <label for="txtBuscar">Ingrese Nro Cédula</label>
            </div>
            <div class="col-md-3"><asp:Button runat="server" ID="btnBuscar" Text="Buscar Alumno" CssClass="btn waves-effect waves-light" OnClick="btnBuscar_Click"/></div>
            <div class="col-md-3"></div>
            <div class="col-lg-12">
                <asp:UpdatePanel ID="upResultadoNoExito" runat="server" Visible="false">
                    <ContentTemplate>
                        <h5>Resultado de la Búsqueda</h5>
                        <asp:Label runat="server" ID="lblResultado" CssClass="control-label"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="upResultadoExito" runat="server" Visible="false">
                <ContentTemplate>
                    <h5>Resultado de la Búsqueda</h5>
                    <asp:Table CssClass="table" ID="tablaDatos" runat="server">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell CssClass="text-center">Nombres</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Cédula</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Plan</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Total Activas</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Regulares</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Regulares Disp.</asp:TableHeaderCell>                                
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="text-center" ID="tcNombres"></asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tcCedula"></asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tcPlan"></asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tc1">
                                <asp:TextBox ID="tcTotalActivas" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tc2">
                                <asp:TextBox ID="tcRegulares" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tc3">
                                <asp:TextBox ID="tcRegularesDisp" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>                                
                        </asp:TableRow>
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell CssClass="text-center">Complementarias</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Complementarias Disp.</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Fecha Inicio</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Fecha Final</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Tipo</asp:TableHeaderCell>
                            <asp:TableHeaderCell CssClass="text-center">Acción</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell CssClass="text-center" ID="tc4">
                                <asp:TextBox ID="tcComplemen" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tc5">
                                <asp:TextBox ID="tcComplemenDisp" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tc6">
                                <asp:TextBox ID="tcFechaInicio" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tc7">
                                <asp:TextBox ID="tcFechaFin" runat="server" CssClass="form-control"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="tc8">
                                <asp:DropDownList ID="tcTipo" runat="server" CssClass="browser-default">
                                    <asp:ListItem Text="Seleccione un Tipo" Value=""  />
                                    <asp:ListItem Text="Empleado" Value="Empleado"  />
                                    <asp:ListItem Text="Contratado/Temporal" Value="Contratado"  />
                                    <asp:ListItem Text="Otro" Value="Otro"  />
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell CssClass="text-center" ID="TableCell1">
                                <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" CssClass="btn btn-purple" Text="Actualizar"></asp:Button>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="phAdminClienteEstadistica" Visible="false">
        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel ID="upAdminClienteEstadistica" runat="server" Visible="false">
                    <ContentTemplate>
                        <h5>Población General</h5>
                        <asp:Table CssClass="table" ID="tablaValores" runat="server">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell CssClass="text-center">Total Empleados </asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="text-center">Muestra </asp:TableHeaderCell>
                                <asp:TableHeaderCell CssClass="text-center">Nivel de Confianza </asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell CssClass="text-center" ID="txtTotal"></asp:TableCell>
                                <asp:TableCell CssClass="text-center" ID="txtMuestra"></asp:TableCell>
                                <asp:TableCell CssClass="text-center" ID="txtNivel"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- Fin estadisticas -->
        <br />
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phListasAdmin" runat="server" Visible="false">
        <div class="row">  
            <div class="col-md-6 text-left">
                <h5 class="text-left">Lista de Espera</h5>
            </div>          
            <div class="col-md-6 text-right">
                <asp:Timer runat="server" OnTick="Unnamed_Tick" ID="tmPrueba" Interval="3600000"></asp:Timer>
                <asp:Button runat="server" CssClass="btn waves-effect waves-light" ID="btnAvisar" Text="Enviar Aviso" OnClick="btnAvisar_Click" />
            </div>      
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView5" runat="server" Width="100%" HorizontalAlign="Center"
                    AutoGenerateColumns="false" AllowPaging="true"                        
                    DataKeyNames="Clase" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="10"
                    onpageindexchanging="GridView5_PageIndexChanged"
                    EmptyDataText="No existen usuarios la lista de espera"
                    OnRowCommand="GridView5_RowCommand"
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="Clase">
                                <ItemTemplate>
                                    <asp:Label ID="Clase" runat="server" Enabled="false" Text='<%# Eval("Clase") %>' />
                                    <asp:HiddenField Visible="false" runat="server" ID="ClasePlantillaID" Value='<%# Eval("ClasePlantillaID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="Fecha" runat="server" Enabled="false" Text='<%# Eval("Fecha") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hora">
                                <ItemTemplate>
                                    <asp:Label ID="Hora" runat="server" Enabled="false" Text='<%# Eval("Hora") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Profesor">
                                <ItemTemplate>
                                    <asp:Label ID="Profesor" runat="server" Enabled="false" Text='<%# Eval("Profesor") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unidad">
                                <ItemTemplate>
                                    <asp:Label ID="Cliente" runat="server" Enabled="false" Text='<%# Eval("Unidad") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="# Usuarios">
                                <ItemTemplate>
                                    <asp:Label ID="Usuarios" runat="server" Enabled="false" Text='<%# Eval("Usuarios") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </asp:PlaceHolder>

        <!-- Estadistica por Nivel 1 -->
    <asp:UpdatePanel runat="server" ID="upEstadisticas1" Visible="false">
        <ContentTemplate>
            <h5>Muestra ideal por Nivel 1</h5>
            <asp:Panel runat="server" ID="panelEstad1"></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button Visible="false" runat="server" ID="Button4" Text="Ver Grafica" OnClick="Button4_Click" CssClass="btn btn-purple" />
    <!-- Fin estadisticas -->
    <!-- Estadistica por Nivel 2 -->
    <asp:UpdatePanel runat="server" ID="upEstadisticas2" Visible="false">
        <ContentTemplate>
            <h5>Muestra ideal por Nivel 2</h5>
            <asp:Panel runat="server" ID="panelEstad2"></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button Visible="false" runat="server" ID="Button1" Text="Ver Grafica" OnClick="Button1_Click" CssClass="btn btn-purple" />
    <!-- Fin estadisticas -->
    <!-- Estadistica por Nivel 3 -->
    <asp:UpdatePanel runat="server" ID="upEstadisticas3" Visible="false">
        <ContentTemplate>
            <h5>Muestra ideal por Nivel 3</h5>
            <asp:Panel runat="server" ID="panelEstad3"></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button Visible="false" runat="server" ID="Button2" Text="Ver Grafica" OnClick="Button2_Click" CssClass="btn btn-purple" />
    <!-- Fin estadisticas -->

    <!-- Estadistica por Nivel 4 -->
    <asp:UpdatePanel runat="server" ID="upEstadisticas4" Visible="false">
        <ContentTemplate>
            <h5>Muestra ideal por Nivel 4</h5>
            <asp:Panel runat="server" ID="panelEstad4"></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button Visible="false" runat="server" ID="Button3" Text="Ver Grafica" OnClick="Button3_Click" CssClass="btn btn-purple" />
    <!-- Fin estadisticas -->
    <div class="row">
        <div class="col-lg-12">
            <asp:PlaceHolder runat="server" ID="phMembresias" Visible="false" >
                <h5 class="text-left">Datos de Matrícula</h5>
                <div class="col-lg-6">
                    <asp:Label runat="server" ID="lb1" Text="Matrícula: "></asp:Label>
                    <asp:Label runat="server" ID="lblMatricula" Text="" CssClass="control-label"></asp:Label>
                </div>
                <div class="col-lg-6">
                    <asp:Label runat="server" ID="lb2" Text="Fecha de Vencimiento: "></asp:Label>
                    <asp:Label runat="server" ID="lblFechaMatricula" Text="" CssClass="control-label"></asp:Label>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <asp:PlaceHolder ID="phMedical" runat="server" Visible="false">
                <h5 class="text-left">Reservas Médical</h5>
                <asp:UpdatePanel ID="upReservaMedical" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView4" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="UsuarioID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        EmptyDataText="No existen Reservas Medical"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                                <asp:TemplateField HeaderText="Clase">
                                    <ItemTemplate>
                                        <asp:Label ID="ClaseDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha">
                                    <ItemTemplate>
                                        <asp:Label ID="ClasePlantillaFecha" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaFecha") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Turno">
                                    <ItemTemplate>
                                        <asp:Label ID="Turno" runat="server" Enabled="false" Text='<%# Eval("Turno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hora Inicial">
                                    <ItemTemplate>
                                        <asp:Label ID="HoraInicial" runat="server" Enabled="false" Text='<%# Eval("HoraInicial") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hora Final">
                                    <ItemTemplate>
                                        <asp:Label ID="HoraFinal" runat="server" Enabled="false" Text='<%# Eval("HoraFinal") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:PlaceHolder>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
        <asp:UpdatePanel ID="upAlumno" runat="server" Visible="false">
                <ContentTemplate>
                    <h5 class="text-left">Ultimas 10 Calificaciones</h5>
                    <asp:GridView ID="GridView1" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="UsuarioID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        EmptyDataText="No existen Calificaciones"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                        <Columns>
                            <asp:TemplateField HeaderText="Usuario">
                                <ItemTemplate>
                                    <asp:Label ID="Usuario" runat="server" Enabled="false" Text='<%# Eval("Usuario") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clase">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Elemento">
                                <ItemTemplate>
                                    <asp:Label ID="Elemento" runat="server" Enabled="false" Text='<%# Eval("Elemento") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Calificacion">
                                <ItemTemplate>
                                    <asp:Label ID="Calificacion" runat="server" Enabled="false" Text='<%# Eval("Calificacion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Profesor">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Enabled="false" Text='<%# Eval("Profesor") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Enabled="false" Text='<%# Eval("Fecha") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Hora">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Enabled="false" Text='<%# Eval("Hora") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                                                                         
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button Visible="false" runat="server" ID="verCalificaciones" Text="Ver Todas" OnClick="verCalificaciones_Click" CssClass="btn btn-purple" />
        </div>
        <!-- DataGriew para Usuario Administrador de Clientes -->
        <div class="col-md-12">
        <asp:UpdatePanel ID="upAdminCliente" runat="server" Visible="false">
                <ContentTemplate>
                    <h5 class="text-left">Últimos 10 Usuarios Activos</h5>
                    <asp:GridView ID="GridView2" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="UsuarioID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        EmptyDataText="No existen Usuarios Activos"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                        <Columns>     
                    <asp:TemplateField HeaderText="Nombre" >
                        <ItemTemplate>
                            <asp:Label runat="server" Visible="false" ID="UsuarioID" Text='<%# Eval("UsuarioID") %>' />
                            <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cédula" >
                        <ItemTemplate>
                            <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha de Nacimiento" >
                        <ItemTemplate>
                            <asp:Label ID="UsuarioFechaNacimiento" runat="server" Enabled="false" Text='<%# Eval("UsuarioFechaNacimiento") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Correo" >
                        <ItemTemplate>
                            <asp:Label ID="UsuarioCorreo" runat="server" Enabled="false" Text='<%# Eval("UsuarioCorreo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>               
                </Columns>
                    </asp:GridView>
                </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button Visible="false" runat="server" ID="verUsuarios" Text="Ver Usuarios" OnClick="verUsuarios_Click" CssClass="btn btn-purple" />
        </div>
        <!-- -------------------------------------------------->
        <!-- DataGriew para Usuario Administrador de Clientes -->
        <div class="col-md-12">
        <asp:UpdatePanel ID="upAdminCliente1" runat="server" Visible="false">
                <ContentTemplate>
                    <h5 class="text-left">Últimas 10 Reservas</h5>
                    <asp:GridView ID="GridView3" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="ReservaID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        EmptyDataText="No existen reservas activas."
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cédula">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de Nacimiento">
                                <ItemTemplate>
                                    <asp:Label ID="ProfesorID" runat="server" Enabled="false" Text='<%# Eval("ProfesorID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clase" >
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha " >
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaFecha") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Hora " >
                            <ItemTemplate>
                                <asp:Label ID="ClasePlantillaHora" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaHora") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                                                                                    
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button Visible="false" runat="server" ID="verReservas" Text="Ver Reservas" OnClick="verReservas_Click" CssClass="btn btn-purple" />
    <!-- Fin Contenido -->
</div>


</div>
    
</div>

<!-- VENTANAS MODALES -->
<!-- Delete Record Modal Starts here-->
<div id="cambiarModal"  class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button id="closeDelete" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3 id="delModalLabel">Información</h3>
            </div>
            <asp:UpdatePanel ID="upDel" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <label id="lblPregunta"></label>
                        <asp:HiddenField ID="hClaseDel" runat="server" />
                                                    
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCambiar" runat="server" Text="Cambiar" CssClass="waves-effect waves-light btn" OnClick="btnCambiar_Click" />
                        <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCambiar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<!--Delete Record Modal Ends here -->
<!-- Delete Record Modal Starts here-->
<div id="cambiarModal2"  class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button id="closeDelete2" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3 id="H1">Información</h3>
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <label id="lblPregunta2"></label>   
                        <asp:PlaceHolder runat="server" ID="tpoModalidad" Visible="false">
                            <div class="form-group">
                                <asp:RadioButtonList runat="server" ID="rdbModalidad">
                                    <asp:ListItem Selected="true" Value="Clasica" Text="Modalidad Clasica - 12 Clases"></asp:ListItem>
                                    <asp:ListItem Value="Nueva" Text="Modalidad Nueva - 4 Clases"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </asp:PlaceHolder>                                           
                    </div>                        
                    <div class="modal-footer">
                        <asp:Button ID="ActivarPlan" runat="server" Text="Aceptar" CssClass="btn btn-purple" OnClick="ActivarPlan_Click" />
                        <asp:Button ID="Button5" CssClass="waves-effect waves-light btn" runat="server" Text="Cancelar" OnClick="btnSalir_Click" ></asp:Button>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCambiar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<!--Delete Record Modal Ends here -->
<!-- Delete Record Modal Starts here-->
<div id="cambiarModal3"  class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button id="closeDelete3" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h3 id="H2">Información</h3>
            </div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <label id="lblPregunta3"></label>    
                        <div class="input-field">
                            <asp:DropDownList runat="server" ID="dplOpciones" CssClass="browser-default" >
                            </asp:DropDownList>
                        </div>                                                
                    </div>                        
                    <div class="modal-footer">
                        <asp:Button ID="btnAccion" runat="server" Text="Aceptar" CssClass="btn btn-purple" OnClick="btnAccion_Click" />
                        <asp:Button ID="btnSalir" CssClass="btn btn-purple" runat="server" Text="Cancelar" OnClick="btnSalir_Click" ></asp:Button>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCambiar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<!--Delete Record Modal Ends here -->
<!-- Msj Modal -->
<div class="modal fade" id="Msjmodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h5 class="modal-title"><label id="lblMsjTitle"></label></h5>
        </div>
        <div class="modal-body">
            <div class="row">
                <div class="col-md-1">
                    <span id="icoModal" class="fa fa-times fa-2x text-danger"></span>
                </div>
                <div class="col-md-11">
                    <label id="lblMsjModal"></label>
                </div>
            </div>
            <div class="clearfix"></div>      </div><!-- /modal-body -->
        <div class="modal-footer">
            <button type="button" class="waves-effect waves-light btn" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>
<!-- Grafica Nivel 2 -->
<div class="modal fade" id="modalGrafica2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h5 class="modal-title"><label id="Label9"></label></h5>
        </div>
        <div class="modal-body">
            <div class="col-lg-8">
            <asp:Chart ID="Chart1" runat="server" Width="530px" Height="360px" Palette="BrightPastel" BackColor="Silver" BackSecondaryColor="White" BackGradientStyle="TopBottom">
                <Series>
                    <asp:Series Name="Series1" ChartType="Column" ChartArea="ChartArea1" Legend="Default" Label="#VALY">
                    </asp:Series>
                    <asp:Series Name="Series2" ChartType="Column" ChartArea="ChartArea1" Legend="Default" Label="#VALY" >
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackSecondaryColor="White" BackColor="Gainsboro" BackGradientStyle="DiagonalLeft">
                    </asp:ChartArea>
                </ChartAreas>
                <Titles>
                    <asp:Title Text="Muestras ideales por Nivel" />
                </Titles>
                <BorderSkin SkinStyle="Emboss" />
            </asp:Chart>
        </div>         
        <div class="clearfix"></div>      
        </div>
        <div class="modal-footer">
            <button type="button" class="waves-effect waves-light btn" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>
    <!-- Grafica Nivel 3 -->
<div class="modal fade" id="modalGrafica3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h5 class="modal-title"><label id="Label10"></label></h5>
        </div>
        <div class="modal-body">
            <div class="col-lg-8">
            <asp:Chart ID="Chart3" runat="server" Width="530px" Height="360px" Palette="BrightPastel" BackColor="Silver" BackSecondaryColor="White" BackGradientStyle="TopBottom">
                <Series>
                    <asp:Series Name="Series1" ChartType="Column" ChartArea="ChartArea1" Legend="Default" Label="#VALY">
                    </asp:Series>
                    <asp:Series Name="Series2" ChartType="Column" ChartArea="ChartArea1" Legend="Default" Label="#VALY" >
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackSecondaryColor="White" BackColor="Gainsboro" BackGradientStyle="DiagonalLeft">
                    </asp:ChartArea>
                </ChartAreas>
                <Titles>
                    <asp:Title Text="Muestras ideales por Nivel 3" />
                </Titles>
                <BorderSkin SkinStyle="Emboss" />
            </asp:Chart>
        </div>         
        <div class="clearfix"></div>      
        </div>
        <div class="modal-footer">
            <button type="button" class="waves-effect waves-light btn" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>
    <!-- Grafica Nivel 4 -->
<div class="modal fade" id="modalGrafica4" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h5 class="modal-title"><label id="Label11"></label></h5>
        </div>
        <div class="modal-body">
            <div class="col-lg-8">
            <asp:Chart ID="Chart4" runat="server" Width="530px" Height="360px" Palette="BrightPastel" BackColor="Silver" BackSecondaryColor="White" BackGradientStyle="TopBottom">
                <Series>
                    <asp:Series Name="Series1" ChartType="Column" ChartArea="ChartArea1" Legend="Default" Label="#VALY">
                    </asp:Series>
                    <asp:Series Name="Series2" ChartType="Column" ChartArea="ChartArea1" Legend="Default" Label="#VALY" >
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackSecondaryColor="White" BackColor="Gainsboro" BackGradientStyle="DiagonalLeft">
                    </asp:ChartArea>
                </ChartAreas>
                <Titles>
                    <asp:Title Text="Muestras ideales por Nivel 4" />
                </Titles>
                <BorderSkin SkinStyle="Emboss" />
            </asp:Chart>
        </div>         
        <div class="clearfix"></div>      
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>
<!-- Grafica Nivel 1 -->
<div class="modal fade" id="modalGrafica1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
    <div class="modal-content">
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h5 class="modal-title"><label id="Label12"></label></h5>
        </div>
        <div class="modal-body">
            <div class="col-lg-8">
            <asp:Chart ID="Chart2" runat="server" Width="530px" Height="360px" Palette="BrightPastel" BackColor="Silver" BackSecondaryColor="White" BackGradientStyle="TopBottom">
                <Series>
                    <asp:Series Name="Series1" ChartType="Column" ChartArea="ChartArea1" Legend="Default" Label="#VALY">
                    </asp:Series>
                    <asp:Series Name="Series2" ChartType="Column" ChartArea="ChartArea1" Legend="Default" Label="#VALY" >
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BackSecondaryColor="White" BackColor="Gainsboro" BackGradientStyle="DiagonalLeft">
                    </asp:ChartArea>
                </ChartAreas>
                <Titles>
                    <asp:Title Text="Muestras ideales por Nivel 1" />
                </Titles>
                <BorderSkin SkinStyle="Emboss" />
            </asp:Chart>
        </div>         
        <div class="clearfix"></div>      
        </div>
        <div class="modal-footer">
            <button type="button" class="waves-effect waves-light btn" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>
<!-- FIN DE VENTANAS MODALES -->

<!-- Activar Plan Modal Starts here-->
<div id="activarModal"  class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button id="Button6" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h5>¿Desea Activar el Plan?</h5>
            </div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <label class="text-danger">Tome en cuenta si Acepta activar el Plan para este Mes, esto genera automáticamente el descuento de nómina. </label> 
                        <div class="form-group" style="margin-left:40px;margin-top:15px;">                     
                            <asp:CheckBox runat="server" Text="Activar el plan para el mes ACTUAL" ID="chkActual"></asp:CheckBox>
                            <asp:CheckBox runat="server" Text="Activar el plan para el mes PROXIMO" ID="chkProximo"></asp:CheckBox>
                        </div>                                       
                    </div>                        
                    <div class="modal-footer">
                        <asp:Button ID="Button7" runat="server" Text="Guardar" CssClass="btn btn-purple" OnClick="GuardarPlan" />
                        <asp:Button ID="Button8" CssClass="waves-effect waves-light btn" runat="server" Text="Cancelar" OnClick="btnSalir_Click" ></asp:Button>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCambiar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<!--Activar Plan Modal Ends here -->
<!-- Activar Plan Modal Starts here-->
<div id="confirmarModal"  class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button id="Button9" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h5>¿Confirma los datos sumnistrados?</h5>
            </div>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <label class="text-danger">Tome en cuenta si Acepta activar el Plan para este Mes, esto genera automáticamente el descuento de nómina. </label>                                      
                    </div>                        
                    <div class="modal-footer">
                        <asp:Button ID="Button10" runat="server" Text="Aceptar" CssClass="btn btn-purple" OnClick="ConfirmarPlan" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCambiar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<!--Activar Plan Modal Ends here -->
<uc3:ucFooter runat="server" ID="ucFooter" />

<script type="text/javascript">

    function MostrarMsjModal(message, title, ccsclas) {
        var vIcoModal = document.getElementById("icoModal");
        vIcoModal.className = ccsclas;
        $('#lblMsjTitle').html(title);
        $('#lblMsjModal').html(message);
        $('#Msjmodal').modal('show');
        return true;
    }

    function MostrarActivarPlan() {
        $('#activarModal').modal('show');
        return false;
    }

    function MostrarConfirmacion() {
        $('#confirmarModal').modal('show');
        return false;
    }

    function MostrarEstadisticas1() {
        $('#modalGrafica1').modal('show');
        return true;
    }
    function MostrarEstadisticas() {
        $('#modalGrafica2').modal('show');
        return true;
    }
    function MostrarEstadisticas3() {
        $('#modalGrafica3').modal('show');
        return true;
    }
    function MostrarEstadisticas4() {
        $('#modalGrafica4').modal('show');
        return true;
    }
    function MostrarPregunta(message) {
        $('#lblPregunta').html(message);
        $('#cambiarModal').modal('show');
        return true;
    }

    function MostrarPregunta3(message) {
        $('#lblPregunta3').html(message);
        $('#cambiarModal3').modal('show');
        return true;
    }

    function MostrarPregunta2(message) {
        $('#lblPregunta2').html(message);
        $('#cambiarModal2').modal('show');
        return true;
    }

    function MostrarPreguntaCerrar() {
        $('#cambiarModal').modal('show');
        return false;
    }


</script>

</form>
