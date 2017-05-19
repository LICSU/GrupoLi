<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusquedaporAlumno.aspx.cs" Inherits="sistema_BusquedaporAlumno" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Busqueda Individual</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col s6 input-field">
                        <asp:TextBox runat="server" ID="txtUsuario"></asp:TextBox>
                        <label for="txtUsuario" class="active">Cédula</label>
                    </div>
                    <div class="col s2 input-field">
                        <asp:Button runat="server" ID="btnBuscar" CssClass="waves-effect waves-light btn" Text="Buscar" OnClick="btnBuscar_Click" />                                                                                         
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <asp:PlaceHolder runat="server" ID="phInicial" Visible="true">  
                    <p>Ingresa el número de cédula y pulsa buscar para consultar la información referente al alumno.</p>
                </asp:PlaceHolder>
               <asp:PlaceHolder runat="server" ID="upDatos" Visible="false">  
                   <div class="row form-group">
                       <div class="col-lg-12 form-group">
                           <h5 class="text-center">Datos Personales</h5>
                       </div>
                       <div class="col-lg-2">
                           <!-- Foto -->
                           <asp:Image runat="server" ID="imagPerfil" CssClass="perfil"/>
                       </div>
                       <div class="col-lg-10">
                           <asp:Label ID="lblNombres" runat="server" CssClass="text-left control-label"></asp:Label><br />
                           <asp:Label ID="lblCedula" runat="server" CssClass="text-left control-label"></asp:Label><br />
                           <asp:PlaceHolder runat="server" ID="phLicsu" Visible="false">
                                <asp:Label ID="lblFechaMatricula" runat="server" CssClass="text-left control-label"></asp:Label><br />
                                <asp:Label ID="lblEstado" runat="server" CssClass="text-left control-label" Text="Estado: "></asp:Label><br />
                           </asp:PlaceHolder>
                           <asp:PlaceHolder runat="server" ID="phEmpresa" Visible="false">
                               <asp:Label ID="lblMesActual" runat="server" CssClass="text-left control-label" Text="Mes Actual: "></asp:Label><br />
                               <asp:Label ID="lblMesProximo" runat="server" CssClass="text-left control-label" Text="Mes Próximo: "></asp:Label><br />
                            </asp:PlaceHolder>
                       </div>
                   </div>     

                   <div class="row form-group">
                       <div class="col-lg-12 form-group">
                           <h5 class="text-center">Datos de Matricula</h5>
                       </div>
                       <div class="col-lg-2"></div>
                       <div class="col-lg-10">
                           <div class="col s6 input-field">
                                <asp:TextBox runat="server" ID="txtPagoMatricula"></asp:TextBox>
                                <label for="txtUsuario" class="active">Total Cancelado</label>
                           </div>
                           <div class="col s6 input-field">
                                <asp:TextBox runat="server" ID="txtFechaMatricula"></asp:TextBox>
                                <label for="txtUsuario" class="active">Fecha Matricula</label>
                           </div>
                       </div>
                   </div>
                     
                   <div class="row form-group">
                       <div class="col-lg-12 form-group">
                           <h5 class="text-center">Último Plan Activo</h5>
                       </div>
                       <div class="col-lg-2 form-group"></div>
                       <div class="col-lg-10 form-group">
                           <div class="col-lg-4">
                               <asp:Label ID="lblPlanNombre" runat="server" CssClass="control-label"></asp:Label>
                           </div>
                           <div class="col-lg-4">
                               <asp:Label ID="lblClasesActivas" runat="server" CssClass="control-label"></asp:Label>
                           </div>
                           <div class="col-lg-4">
                               <asp:Label ID="lblDeuda" runat="server" CssClass="control-label"></asp:Label>
                           </div>
                       </div>
                       <div class="col-lg-2 form-group"></div>
                       <div class="col-lg-10 form-group">
                           <div class="col s6 input-field">
                                <asp:TextBox runat="server" ID="lblTotalClasesR"></asp:TextBox>
                                <label for="txtUsuario" class="active">Total Regulares</label>
                           </div>
                           <div class="col s6 input-field">
                                <asp:TextBox runat="server" ID="lblDispClasesR"></asp:TextBox>
                                <label for="txtUsuario" class="active">Disponibles Regulares</label>
                           </div>
                       </div>
                       <div class="col-lg-2 form-group"></div>
                       <div class="col-lg-10 form-group">
                           <div class="col s6 input-field">
                                <asp:TextBox runat="server" ID="lblTotalClasesC"></asp:TextBox>
                                <label for="txtUsuario" class="active">Total Complementarias</label>
                           </div>
                           <div class="col s6 input-field">
                                <asp:TextBox runat="server" ID="lblDispClasesC"></asp:TextBox>
                                <label for="txtUsuario" class="active">Disponibles Complementarias</label>
                           </div>
                       </div>
                       <div class="col-lg-2 form-group"></div>
                       <div class="col-lg-10 form-group">
                           <div class="col s6 input-field">
                                <asp:TextBox runat="server" ID="lblFechaInicio"></asp:TextBox>
                                <label for="txtUsuario" class="active">Fecha de Inicio</label>
                           </div>
                           <div class="col s6 input-field">
                                <asp:TextBox runat="server" ID="lblFechaFin"></asp:TextBox>
                                <label for="txtUsuario" class="active">Fecha Final</label>
                           </div>
                       </div>
                       <asp:HiddenField ID="hdfPlanAlumnoID" runat="server" />
                       <asp:HiddenField ID="hdfCedulaID" runat="server" />
                       <asp:HiddenField ID="hdfUsuarioID" runat="server" />
                   </div>

                    <div class="col-lg-12 text-center form-group">
                        <asp:Button runat="server" ID="btnActualizar" CssClass="waves-effect waves-light btn" Text="Actualizar" OnClick="btnActualizar_Click" />                        
                    </div>
                    
                    <div class="col-lg-12"  style="text-align:left !important;">
                        <div class="col-lg-12"><h5 class="text-left"><strong>Reservas Realizadas</strong></h5></div> 
                        <asp:GridView ID="GridView1" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="BonoID" CssClass="footable" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        EmptyDataText="No existen Reservas para este Usuario">
                        <Columns>  
                            <asp:TemplateField HeaderText="Clase">
                                <ItemTemplate>
                                    <asp:Label ID="ClaseDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Clase">
                                <ItemTemplate>
                                    <asp:Label ID="FechaClase" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaFecha") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Reservada el">
                                <ItemTemplate>
                                    <asp:Label ID="FechaReserva" runat="server" Enabled="false" Text='<%# Eval("FechaReserva") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Profesor">
                                <ItemTemplate>
                                    <asp:Label ID="Profesor" runat="server" Enabled="false" Text='<%# Eval("Profesor") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                                           
                        </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <div class="col-lg-12"  style="text-align:left !important;">
                        <div class="col-lg-12"><h5 class="text-left"><strong>Pagos Realizadas</strong></h5></div> 
                        <asp:GridView ID="GridView2" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="FacturaNumero" CssClass="footable" PageSize="20"
                        onpageindexchanging="GridView2_PageIndexChanging"
                        EmptyDataText="No existen Pagos realizados por este Usuario">
                        <Columns>  
                            <asp:TemplateField HeaderText="Plan">
                                <ItemTemplate>
                                    <asp:Label ID="PlanNombre" runat="server" Enabled="false" Text='<%# Eval("PlanNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Factura #">
                                <ItemTemplate>
                                    <asp:Label ID="FacturaNumero" runat="server" Enabled="false" Text='<%# Eval("FacturaNumero") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Factura Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="FacturaFecha" runat="server" Enabled="false" Text='<%# Eval("FacturaFecha") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Factura Monto">
                                <ItemTemplate>
                                    <asp:Label ID="FacturaMonto" runat="server" Enabled="false" Text='<%# Eval("FacturaMonto") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                                           
                        </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-lg-12"  style="text-align:left !important;">
                        <div class="col-lg-12"><h5 class="text-left"><strong>Calificaciones</strong></h5></div> 
                        <asp:GridView ID="GridView3" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="ClaseDescripcion" CssClass="footable" PageSize="20"
                        onpageindexchanging="GridView3_PageIndexChanging"
                        EmptyDataText="No existen Calificaciones para este Usuario">
                        <Columns>  
                            <asp:TemplateField HeaderText="Profesor">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Enabled="false" Text='<%# Eval("Profesor") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clase">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Enabled="false" Text='<%# Eval("ClaseNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Elemento">
                                <ItemTemplate>
                                    <asp:Label ID="ElementoNombre" runat="server" Enabled="false" Text='<%# Eval("ElementoNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Calificacion">
                                <ItemTemplate>
                                    <asp:Label ID="CalificacionNombre" runat="server" Enabled="false" Text='<%# Eval("CalificacionNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                                           
                        </Columns>
                        </asp:GridView>
                    </div>
                <div class="row">
                    <div class="col-lg-12"></div>
                </div>
            </asp:PlaceHolder>   
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
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
    </script>

    </form>