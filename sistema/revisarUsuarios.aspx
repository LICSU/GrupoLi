<%@ Page Language="C#" AutoEventWireup="true" CodeFile="revisarUsuarios.aspx.cs" Inherits="sistema_revisarUsuarios" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:150px;"> 
        <div class="row text-center">
            <h4>Actualizar Usuarios (Estado)</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <asp:DropDownList runat="server" ID="dplClientes" AutoPostBack="true" CssClass="browser-default" OnSelectedIndexChanged="dplClientes_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <label for="ddlEstado">Estado para el Mes Actual</label>
                        <asp:DropDownList runat="server" ID="ddlEstado" AutoPostBack="true" CssClass="browser-default" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                            <asp:ListItem Value="" Text="Seleccione el Estado"></asp:ListItem>
                            <asp:ListItem Value="1" Text="ACTIVO"></asp:ListItem>
                            <asp:ListItem Value="0" Text="INACTIVO"></asp:ListItem>
                        </asp:DropDownList>
                        
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <label for="ddlEstado">Estado para el Mes Proximo</label>
                        <asp:DropDownList runat="server" ID="ddlEstadoP" AutoPostBack="true" CssClass="browser-default" OnSelectedIndexChanged="ddlEstadoP_SelectedIndexChanged">
                            <asp:ListItem Value="" Text="Seleccione el Estado"></asp:ListItem>
                            <asp:ListItem Value="1" Text="ACTIVO"></asp:ListItem>
                            <asp:ListItem Value="0" Text="INACTIVO"></asp:ListItem>
                        </asp:DropDownList>
                        
                    </div>
                    <div class="col-lg-2">
                        <asp:ImageButton ID="btnDescargar" runat="server" ImageUrl="~/Images/descargar.png"   OnClick="btnDescargar_Click"/> 
                    </div>
                </div>
                <div class="row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="BonoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        EmptyDataText="No existen Usuarios Activos"
                        PagerStyle-CssClass="pagination"  OnRowCommand="GridView1_RowCommand"
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="Cédula">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                    <asp:Label ID="UsuarioID" runat="server" Enabled="false" Visible="false" Text='<%# Eval("UsuarioID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombres">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Empresa">
                                <ItemTemplate>
                                    <asp:Label ID="Empresa" runat="server" Enabled="false" Text='<%# Eval("Empresa") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reservas Anterior">
                                <ItemTemplate>
                                    <asp:Label ID="NroReservasAnt" runat="server" Enabled="false" Text='<%# Eval("NroReservasAnt") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reservas Actuales">
                                <ItemTemplate>
                                    <asp:Label ID="NroReservas" runat="server" Enabled="false" Text='<%# Eval("NroReservas") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual">
                                <ItemTemplate>
                                    <asp:Label ID="EstadoAct1" runat="server" Enabled="false" Text='<%# Eval("EstadoAct") %>' />
                                    <asp:Label ID="EstadoActual" runat="server" Enabled="false" Visible="false" Text='<%# Eval("PlanActivo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proximo">
                                <ItemTemplate>
                                    <asp:Label ID="EstadoSig" runat="server" Enabled="false" Text='<%# Eval("EstadoSig") %>' />
                                    <asp:Label ID="EstadoProximo" runat="server" Enabled="false" Visible="false" Text='<%# Eval("EstadoProximo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="Historial"
                                ButtonType="Image" ImageUrl="~/Images/historial.png" HeaderText="Historial">
                                <ControlStyle></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Actual"
                                ButtonType="Image" ImageUrl="~/Images/editar.png" HeaderText="Actualizar Actual">
                                <ControlStyle></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Proximo"
                                ButtonType="Image" ImageUrl="~/Images/editar.png" HeaderText="Actualizar Proximo">
                                <ControlStyle></ControlStyle>
                            </asp:ButtonField>  
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDescargar" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!-- Add Modal Starts here -->
    <div id="ActualModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDeleteA" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Actualizar Actual</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea actualizar el plan para el Mes Actual?
                            <asp:HiddenField ID="hEstatusActual" runat="server" />                                                    
                            <asp:HiddenField ID="hUsuarioAct" runat="server" />                                                    
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-purple" OnClick="btnUpdate_Click" />
                            <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Add Modal Ends here -->
    <!-- Add Modal Starts here -->
    <div id="ProximoModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDeleteP" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Actualizar Actual</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea actualizar el plan para el Mes Proximo?
                            <asp:HiddenField ID="hUsuarioPro" runat="server" />   
                            <asp:HiddenField ID="hEstatusProx" runat="server" />                                                   
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button2" runat="server" Text="Actualizar" CssClass="btn btn-purple" OnClick="btnUpdateP_Click" />
                            <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Add Modal Ends here -->
    <!-- Modal de historial -->
    <div id="modalHistorial" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="Button1" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Historial de Usuario</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                           <asp:HiddenField ID="hdfUsuarioID" runat="server" />   
                             <asp:GridView ID="GridView2" runat="server" Width="90%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" AllowPaging="true"
                                DataKeyNames="BonoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="10"
                                onpageindexchanging="GridView2_PageIndexChanging"
                                EmptyDataText="El Usuario aun no posee historial"
                                PagerStyle-CssClass="pagination"
                                PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                                PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <Columns>
                                     <asp:TemplateField HeaderText="Usuario">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Enabled="false" Text='<%# Eval("Usuario") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Enabled="false" Text='<%# Eval("Estado") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Enabled="false" Text='<%# Eval("Fecha") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>                                                
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Fin modal de historial -->
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
