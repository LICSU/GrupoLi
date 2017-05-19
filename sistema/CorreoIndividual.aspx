<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CorreoIndividual.aspx.cs" Inherits="sistema_CorreoIndividual" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Correo Individual</h4>
        </div>
        <div class="row"><br /></div>
        <div class="row text-center">
            <div class="col-lg-2"></div>
            <div class="col-lg-8">
                <asp:UpdatePanel ID="upCrudGrid" runat="server" UpdateMode="Always" >
                    <ContentTemplate>
                        <div class="row input-field">
                            <div class="col s8">
                                <asp:TextBox  ID="txtDestinatario" runat="server"></asp:TextBox> 
                                <label for="txtDestinatario" class="active">Destinatario</label>
                            </div>
                            <div class="col s4">
                                <asp:Button ID="BuscarUsr" runat="server" Text="Buscar Email" CssClass="waves-effect waves-light btn"  OnClick="BuscarUsr_Click"/>
                            </div>                            
                        </div>
                        <div class="row input-field">
                            <asp:TextBox ID="txtAsunto" runat="server"></asp:TextBox>        
                            <label for="txtAsunto">Asunto</label>
                        </div>
                        <div class="row input-field">
                            <asp:TextBox ID="txtMensaje" TextMode="MultiLine" CssClass="materialize-textarea" Rows="10" runat="server"></asp:TextBox>        
                            <label for="txtMensaje">Mensaje</label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row input-field">
                    <asp:FileUpload ID="fipAdjuntos" runat="server"   /> 
                </div>
                <div class="row input-field">
                    <asp:Button ID="Button1" runat="server" Text="Enviar" CssClass="waves-effect waves-light btn" OnClick="btnEnviar_Click" />
                </div>
            </div>
            <div class="col-lg-2"></div>
        </div>
    </div>

    <!-- Buscar Modal Starts here -->
    <div id="bscModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="Button2" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Buscar Usuario</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>                                    
                        <asp:GridView ID="GridView2" runat="server" Width="95%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="true"
                            CssClass="table table-hover table-striped" PageSize="20"
                            OnRowCommand="GridView2_RowCommand" 
                            EmptyDataText="No existen Usuarios..."
                            onpageindexchanging="GridView2_PageIndexChanging"
                            PagerStyle-CssClass="pagination" 
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                                <asp:TemplateField HeaderText="Nombre" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioNombreB" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apellido" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioApellidoB" runat="server" Enabled="false" Text='<%# Eval("UsuarioApellido") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Correo" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioCorreo" runat="server" Enabled="false" Text='<%# Eval("UsuarioCorreo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="ClienteNombreB" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioIDB" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />
                                        <asp:Label ID="SucursalIDB" runat="server" Enabled="false" Text='<%# Eval("SucursalID") %>' />
                                        <asp:Label ID="ClienteIDB" runat="server" Enabled="false" Text='<%# Eval("ClienteID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField CommandName="selectRecord" ButtonType="Button" Text="Seleccionar" HeaderText="Seleccionar">
                                    <ControlStyle CssClass="waves-effect waves-light btn"></ControlStyle>
                                </asp:ButtonField>
                                                                   
                            </Columns>
                        </asp:GridView>
                        <div class="modal-footer">
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- BUscar Modal Ends here -->
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