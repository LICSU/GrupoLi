<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usuariosActivos.aspx.cs" Inherits="sistema_usuariosActivos" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Usuarios Activos</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <asp:PlaceHolder runat="server" ID="phClientes">
                    <div class="row text-center form-group">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8 input-field">                       
                            <asp:DropDownList runat="server" ID="dplClientes" AutoPostBack="true" CssClass="browser-default" OnSelectedIndexChanged="dplClientes_SelectedIndexChanged"></asp:DropDownList>                       
                        </div>
                                       
                    </div>
                </asp:PlaceHolder>
                <div class="row text-right form-group">
                        <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="ImgbtnArchivo_Click" />
                </div>
                
                <div class="row text-center form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="BonoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        EmptyDataText="No existen Usuarios Activos" OnRowCommand="GridView1_RowCommand"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>                             
                            <asp:TemplateField HeaderText="Nombre(s) y Apellido(s)">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioID" runat="server" Enabled="false" Visible="false" Text='<%# Eval("UsuarioID") %>' />
                                    <asp:Label ID="UsuarioNombres" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombres") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Cédula">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Empresa">
                                <ItemTemplate>
                                    <asp:Label ID="ClienteNombre" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Fecha de Activación">
                                <ItemTemplate>
                                    <asp:Label ID="fechaUltima" runat="server" Enabled="false" Text='<%# Eval("fechaUltima") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Total Clases">
                                <ItemTemplate>
                                    <asp:Label ID="TotalClases" runat="server" Enabled="false" Text='<%# Eval("TotalClases") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>    
                                                                                
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgbtnArchivo" />
            </Triggers>
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