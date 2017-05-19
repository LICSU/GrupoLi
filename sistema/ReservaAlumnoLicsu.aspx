<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReservaAlumnoLicsu.aspx.cs" Inherits="sistema_ReservaAlumnoLicsu" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h4>Reservar Clases</h4>
        </div>
        <div class="row"><br /></div>
        <div class="row">
            <!-- Panel de Informacion -->
        <div class="col-lg-12">
            <div class="col-lg-4">
                <asp:Calendar ID="calendario" runat="server" OnSelectionChanged="calendario_SelectionChanged" 
                        CssClass="centrado" OnDayRender="calendario_DayRender" 
                    BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" 
                    Font-Size="9pt" ForeColor="Black" NextPrevFormat="ShortMonth">
                        
                    <DayHeaderStyle HorizontalAlign="Center" CssClass="text-center" BorderStyle="Solid" BorderWidth="1px" 
                                    BorderColor="#000" Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Size="6pt" ForeColor="#333333" VerticalAlign="Bottom"  />
                    <OtherMonthDayStyle ForeColor="#999999"  />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White"  />
                    <SelectorStyle />
                    <TitleStyle Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="text-center" 
                                BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Size="10pt" ForeColor="#333399"  />
                    <TodayDayStyle BackColor="#CCCCCC"  />
                </asp:Calendar>    
            </div>
            <div class="col-lg-8">
                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                        <div class="col-lg-12">
                            <div class="col-lg-6" style="font-weight: bold;">
                                <asp:Label runat="server" ID="Label1" CssClass="text-1" Text="Nombre Alumna(o):"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label runat="server" ID="lblNombreAlumna" CssClass="text-primary" ></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-6" style="font-weight: bold;">
                                <asp:Label runat="server" ID="Label3" CssClass="text-1" Text="Plan Actual:"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label runat="server" ID="lblPlan" CssClass="text-primary" ></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-6" style="font-weight: bold;">
                                <asp:Label runat="server" ID="Label4" CssClass="text-1"  Text="Fecha de Vencimiento:"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label runat="server" ID="lblFechaFin" CssClass="text-primary"  ></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-6" style="font-weight: bold;">
                                <asp:Label runat="server" ID="Label5" CssClass="text-1"  Text="Deudas: "></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label runat="server" ID="lblDeuda" CssClass="text-primary"  Text=""></asp:Label>
                            </div>
                        </div>
                        <asp:PlaceHolder runat="server" ID="phMatricula" Visible="false">
                            <div class="col-lg-12">
                                <div class="col-lg-6" style="font-weight: bold;">
                                    <asp:Label runat="server" ID="Label16" CssClass="text-1"  Text="Matricula Anual: "></asp:Label>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label runat="server" ID="lblMatriculaAnual" CssClass="text-primary"  Text=""></asp:Label>
                                </div>
                            </div>   
                        </asp:PlaceHolder>     
                        <div class="col-lg-12">
                            <div class="col-lg-6" style="font-weight: bold;">
                                <asp:Label runat="server" ID="Label7" CssClass="text-1"  Text="Acumulado Regulares: "></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label runat="server" ID="lblTotalClasesRegulares" CssClass="text-primary"  Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-6" style="font-weight: bold;">
                                <asp:Label runat="server" ID="Label11" CssClass="text-1"  Text="Regulares Disponibles: "></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label runat="server" ID="lblClasesRegularesDisp" CssClass="text-primary"  Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-6" style="font-weight: bold;">
                                <asp:Label runat="server" ID="Label14" CssClass="text-1"  Text="Acumulado Complementarias: "></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label runat="server" ID="lblTotalClasesComplemen" CssClass="text-primary"  Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="col-lg-6" style="font-weight: bold;">
                                <asp:Label runat="server" ID="Label12" CssClass="text-1" Text="Complementarias Disponibles: "></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:Label runat="server" ID="lblClasesComplemenDisp" CssClass="text-primary"  Text=""></asp:Label>
                            </div>
                        </div>
                                        
                    </ContentTemplate>                
                </asp:UpdatePanel>
            </div>            
        </div>
        <!-- Fin de Panel -->
        <br />
        <div class="row col-lg-12" style="margin-top:20px;">
            <div class="col-lg-6">
                <div class="text-center">
                    <asp:Label runat="server" ID="lblTituloClasesReservas" Text="Reservas Disponibles" CssClass="h3 text-info text-center"></asp:Label>
                </div>   <br />
                <asp:UpdatePanel ID="upCrudGrid" runat="server">
                    <ContentTemplate>                        
                        <asp:GridView ID="GridView1" runat="server" 
                            AutoGenerateColumns="false" AllowPaging="true"
                            DataKeyNames="ClasePlantillaID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                            onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No Existen Clases Para Esa Fecha"
                            PagerStyle-CssClass="pagination" 
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            OnRowCommand="GridView1_RowCommand">
                            <Columns>
                            <asp:ButtonField CommandName="reservar"
                                    ButtonType="Button" Text="Reservar" HeaderText="Reservar">
                                    <ControlStyle CssClass="waves-effect waves-light btn"></ControlStyle>
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="Día" >
                                <ItemTemplate>
                                    <asp:Label runat="server" Visible="false"  ID="ClaseID" Text='<%# Eval("ClaseID") %>'  />
                                    <asp:Label runat="server" Visible="false"  ID="ClasePlantillaCupos" Text='<%# Eval("ClasePlantillaCupo") %>' />
                                    <asp:Label runat="server" Visible="false"  ID="ClasePlantillaID" Text='<%# Eval("ClasePlantillaID") %>' />
                                    <asp:Label ID="ClasePlantillaFecha" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaFecha") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Clase" >
                                <ItemTemplate>
                                    <asp:Label ID="ClaseDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo" >
                                <ItemTemplate>
                                    <asp:Label ID="ClaseTipo" runat="server" Enabled="false" Text='<%# Eval("ClaseTipo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
            </div>
            <div class="col-lg-6"> 
                <div class="text-center">
                    <asp:Label runat="server" ID="Label8" Text="Mis Reservas" CssClass="h3 text-info center"></asp:Label>     
                </div>         
                <br /> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>  
                    <asp:GridView ID="GridView2" runat="server" 
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="ReservasID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" 
                        PageSize="20"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                        onpageindexchanging="GridView2_PageIndexChanging" EmptyDataText="No Tiene Reservas"
                        OnRowCommand="GridView2_RowCommand">
                        <Columns>
                        <asp:ButtonField CommandName="cancelar"
                                ButtonType="Button" Text="Cancelar" HeaderText="Cancelar">
                        <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="Código" Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdfPrueba" runat="server" />
                                <asp:Label runat="server"  ID="ReservasID" Text='<%# Eval("ReservaID") %>'  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Clase"  Visible="false">
                            <ItemTemplate>
                                <asp:Label runat="server"  ID="ClasePlantillaIDRes" Text='<%# Eval("ClasePlantillaID") %>' />
                                <asp:Label runat="server"  ID="ClaseIDRes" Text='<%# Eval("ClaseID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Clase" >
                            <ItemTemplate>
                                <asp:Label runat="server"  ID="ClaseDescripcionRes" Text='<%# Eval("ClaseDescripcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha" >
                            <ItemTemplate>
                                <asp:Label runat="server"  ID="ClasePlantillaFechaRes" Text='<%# Eval("ClasePlantillaFecha") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo" >
                            <ItemTemplate>
                                <asp:Label ID="ClaseTipoRes" runat="server" Enabled="false" Text='<%# Eval("ClaseTipo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                    </Columns>
                    </asp:GridView>
                </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>
            </div>
        </div>
        </div>
    </div>

    <!---------------------------------- Modals --------------------------------->
    <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button id="Button2" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h5>Seleccion de Nro de Clases</h5>
          </div>
          <div class="modal-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <p class="text-justify" style="padding-top:0px;">Por favor seleccione la cantidad de clases que desea en su plan para este mes.</p>
                            <div class="input-field">
                                <asp:DropDownList runat="server" ID="dplCantidadClases" CssClass="browser-default">
                                    <asp:ListItem Text="Seleccione la Cantidad" Value=""></asp:ListItem>
                                    <asp:ListItem Text="8 Clases" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="12 Clases" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div style="margin-top:20px;text-align: right;">
                            <asp:Label ID="Label13" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="Aceptar" runat="server" Text="Aceptar" CssClass="waves-effect waves-light btn"  OnClick="Aceptar_Click"/>
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                                                
                    </Triggers>
                </asp:UpdatePanel>
                <div class="clearfix"></div>   
          </div><!-- /modal-body -->
          <div class="modal-footer" style="margin-top:0px;">
          </div>
        </div> 
      </div>
    </div>
    <!-- Msj Modal -->
    <!---------------------------------- Modals --------------------------------->
    <div class="modal fade" id="selectPuesto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
            <h4 class="modal-title"><label id="Label15"></label></h4>
          </div>
          <div class="modal-body">
              <asp:UpdatePanel runat="server" ID="panelSelect" >
                  <ContentTemplate>
                      <div class="row">
                    <div class="form-group">
                        <label class="col-xs-4 control-label">Seleccione el Puesto: </label>
                        <div class="col-xs-6">
                            <asp:DropDownList runat="server" ID="dplPuestoClase" ClientIDMode="Static"
                                 AutoPostBack="true" OnSelectedIndexChanged="dplPuestoClase_SelectedIndexChanged" CssClass="form-control">
                                <asp:ListItem  Value="" Text="Seleccione el Puesto"></asp:ListItem>
                            </asp:DropDownList>                                                           
                        </div>
                    </div>
                </div>
                  </ContentTemplate>
              </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                    <h4 class="text-info">Guardando..</h4><br />
                    <img src="img/loading.gif" class="img-responsive center-block"><br />
                    <h4 class="text-info">Espere Por Favor</h4>
                </ProgressTemplate>
            </asp:UpdateProgress>
                <div class="clearfix"></div>      </div><!-- /modal-body -->
          <div class="modal-footer">
          </div>
        </div> 
      </div>
    </div>
    <!-- Msj Modal -->
    <div class="modal fade" id="Div1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
            <h4 class="modal-title"><label id="Label17"></label></h4>
          </div>
          <div class="modal-body">
                <div class="row">
                    <div class="col-md-1">
                        <span id="Span1" class="fa fa-times fa-2x text-danger"></span>
                    </div>
                    <div class="col-md-11">
                        <label id="Label18"></label>
                    </div>
                </div>
                <div class="clearfix"></div>      </div><!-- /modal-body -->
          <div class="modal-footer">
                <button type="button" class="waves-effect waves-light btn" data-dismiss="modal">Cerrar</button>
          </div>
        </div> 
      </div>
    </div>
    <!-- Msj Modal -->
    <div class="modal fade" id="modalPrueba" tabindex="-1" data-keyboard="false" data-backdrop="static" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <!--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>-->
            <h4 class="modal-title"><label id="Label19"></label></h4>
          </div>
          <div class="modal-body">   
            <div>
                <asp:Panel runat="server" ID="pnDisponibles">
                </asp:Panel> 
            </div>                               
           <div class="clearfix"></div>      </div><!-- /modal-body -->
          <div class="modal-footer">
                <asp:Button CssClass="waves-effect waves-light btn" runat="server" ID ="btnCerrarPuesto" OnClick="btnCerrarPuesto_Click" Text="Cerrar"></asp:Button>
          </div>
        </div> 
      </div>
    </div>
    <!-- Modal PleaseWait-->
    <div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false">
      <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-body">
                <div class="clearfix">
                    <img src="img/loading.gif" class="img-responsive center-block" />
                </div>
            </div>
        </div> 
      </div>
    </div>
    <!-- Modal PleaseWait -->
    <!-- Msj Modal -->
    <div class="modal fade" id="Msjmodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
            <h4 class="modal-title"><label id="lblMsjTitle"></label></h4>
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

    <!-- Aviso Modal -->
    <div class="modal fade" id="modalLista" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
            <h4 class="modal-title">Lista de Espera</h4>
          </div>
          <div class="modal-body">
              <div class="row">
                  <asp:Label runat="server" ID="label222" CssClass="col-lg-10 control-label">No hay Cupos disponibles. ¿Desea se le informe cuando haya cupo.?</asp:Label>
              </div>
              <div class="row">
                <asp:DropDownList runat="server" ID="dplListaEspera" CssClass="form-control" OnSelectedIndexChanged="dplListaEspera_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Seleccione una opción" Value=""></asp:ListItem>
                    <asp:ListItem Text="Si por favor" Value="SI"></asp:ListItem>
                    <asp:ListItem Text="No gracias" Value="NO"></asp:ListItem>
                </asp:DropDownList>
                </div>
          <div class="modal-footer">
               <asp:Button ID="Button1" runat="server" OnClick="btnAviso_Click" CssClass="waves-effect waves-light btn" Text="Cerrar"></asp:Button>
          </div>
        </div> 
      </div>
    </div>
    </div>


     <!-- Modal autorizacion -->
    <div class="modal" id="modalAutorizacion" tabindex="-1" data-keyboard="false" data-backdrop="static" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Normas y Recomendaciones</h5>
          </div>
          <div class="modal-body">
              <div class="row">
                  <asp:Label runat="server" ID="label2" CssClass="col-lg-10 control-label">¿Conoce las normas y recomendaciones?</asp:Label>
              </div>
              <div><p>&nbsp;</p></div>
              <div class="row form-group">
                  <label for="ddlNormas">Conoce las normas: </label>
                <asp:DropDownList runat="server" ID="ddlNormas" CssClass="form-control" OnSelectedIndexChanged="ddlNormas_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Seleccione una opción" Value=""></asp:ListItem>
                    <asp:ListItem Text="Ya conozco y acepto las normas y recomendaciones." Value="UNO"></asp:ListItem>
                    <asp:ListItem Text="No conozco las normas y deseo verlas." Value="DOS"></asp:ListItem>
                    <asp:ListItem Text="Ya las conozco y no las acepto." Value="TRES"></asp:ListItem>
                </asp:DropDownList>
                </div>
          <div class="modal-footer">
               
          </div>
        </div> 
      </div>
    </div>
    </div>
    <!-- Fin Modal autorizacion -->
    <div id="aceptarAut"  class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDelete" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Confirmación</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea aceptar las normas?
                            <asp:HiddenField runat="server" ID="hdfAceptar" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAceptarNormas" runat="server" Text="Aceptar" CssClass="waves-effect waves-light btn" OnClick="btnAceptarNormas_Click" />
                            <asp:Button ID="btnCancelarNormas" runat="server" Text="Cancelar" CssClass="waves-effect waves-light btn" OnClick="btnCancelarNormas_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
<!--Fin Modal de Confirmacion de Autorizacion -->

    <!-- Fin Modal autorizacion -->
    <div id="negarAut"  class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="Button3" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Confirmación</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea NO aceptar las normas?
                            <asp:HiddenField runat="server" ID="HiddenField1" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnNegarNormas" runat="server" Text="Aceptar" CssClass="waves-effect waves-light btn" OnClick="btnNegarNormas_Click" />
                            <asp:Button ID="Button5" runat="server" Text="Cancelar" CssClass="waves-effect waves-light btn" OnClick="btnCancelarNormas_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
<!--Fin Modal de Confirmacion de Autorizacion -->

    <!-- Aviso Modal -->
    <div class="modal fade" id="modalAviso" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
            <h4 class="modal-title">Aviso...</h4>
          </div>
          <div class="modal-body">
              <div class="row">
                  <asp:Label runat="server" ID="label20" CssClass="col-lg-10 control-label">Se ha agregado correctamente a la lista de espera, a penas haya disponibilidad de cupo le avisaremos a través del correo electrónico.</asp:Label>
              </div>
          <div class="modal-footer">
                <asp:Button ID="btnAviso" runat="server" OnClick="btnAviso_Click" CssClass="waves-effect waves-light btn" Text="Aceptar"></asp:Button>
          </div>
        </div> 
      </div>
    </div>
    </div>

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

        function MostrarLista() {
            $('#modalLista').modal('show');
            return true;
        }

        function MostrarCantidad() {
            $('#editModal').modal('show');
            return true;
        }

        function MostrarAviso() {
            $('#modalAviso').modal('show');
            return true;
        }

        function VentanaPrueba() {
            $('#modalPrueba').modal('show');
            return true;
        }

        function MostrarAutorizacion() {
            $('#modalAutorizacion').modal('show');
            return true;
        }
        function NegarAutorizacion() {
            $('#negarAut').modal('show');
            return true;
        }

        function ConfirmaAutorizacion() {
            $('#aceptarAut').modal('show');
            return true;
        }
    </script>
</form>

