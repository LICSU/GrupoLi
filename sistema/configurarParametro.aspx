<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configurarParametro.aspx.cs" Inherits="sistema_configurarParametro" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Configurar Parámetros</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="form-group text-center row">
                    <div class="col-lg-2"></div>
                    <div class="col s6 input-field">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <label for="txtSearch">Empresa</label>
                    </div>
                    <div class="col s2">
                        <asp:Button ID="Agregar" runat="server" Text="Agregar"  CssClass="waves-effect waves-light btn"  OnClick="Agregar_Click"/>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="form-group text-center">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="idParametro" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existen Parámetros Configurados."
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <asp:ButtonField CommandName="viewRecord"
                            ButtonType="Button" Text="V" HeaderText="Ver">
                            <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="editRecord"
                            ButtonType="Button" Text="M" HeaderText="Mod">
                            <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="deleteRecord"
                            ButtonType="Button" Text="E" HeaderText="Eli">
                            <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                        </asp:ButtonField>
                        <asp:TemplateField HeaderText="" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="idParametro" runat="server" Enabled="false" Text='<%# Eval("idParametro") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="labelParametro" runat="server" Enabled="false" Text='<%# Eval("labelParametro") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="tipoParametro" runat="server" Enabled="false" Text='<%# Eval("tipoParametro") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Label ID="labelIdParametro" runat="server" Enabled="false" Text='<%# Eval("labelIdParametro") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo">
                            <ItemTemplate>
                                <asp:Label ID="nombreTipoParametro" runat="server" Enabled="false" Text='<%# Eval("nombreTipoParametro") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                                            
                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <asp:CheckBox ID="activoParametro" runat="server" Enabled="false" Checked='<%# Eval("activoParametro") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Cliente" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="ClienteID" runat="server" Enabled="false" Text='<%# Eval("ClienteID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Unidad">
                            <ItemTemplate>
                                <asp:Label ID="ClienteNombre" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                                 
                    </Columns>
                </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Add Modal Starts here -->
    <div id="addModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Agregar Parámetro</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row text-center">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtNombrePar" runat="server"></asp:TextBox>
                                        <label for="txtNombrePar">Nombre: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:DropDownList ID="dplTipoPar" runat="server" ClientIDMode="Static" CssClass="browser-default"></asp:DropDownList>  
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="labelIdPar" runat="server"></asp:TextBox>
                                        <label for="labelIdPar">ID: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtObservacionPar" runat="server"></asp:TextBox>
                                        <label for="txtObservacionPar">Observacion: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:DropDownList ID="dplUnidad" runat="server" ClientIDMode="Static" CssClass="browser-default"></asp:DropDownList>      
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>                                
                            </div>  
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label3" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="waves-effect waves-light btn"  OnClick="btnAdd_Click"/>
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- Add Modal Ends here -->
        <!-- Edit Modal Starts here -->
    <div id="editModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Modificar Parámetro</h5>                                            
                </div>
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row text-center">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtNombreEdit" runat="server"></asp:TextBox>    
                                        <label for="txtNombreEdit">Nombre: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:DropDownList ID="dplTipoParEdit" runat="server" CssClass="browser-default"></asp:DropDownList>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtObserEdit" runat="server"></asp:TextBox>    
                                        <label for="txtObserEdit">Observación: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:DropDownList ID="dplUnidadEdit" runat="server" ClientIDMode="Static" CssClass="browser-default"></asp:DropDownList>  
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>                            
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Modificar" CssClass="waves-effect waves-light btn" OnClick="btnSave_Click" />
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Edit Modal Ends here -->
    <!-- Delete Record Modal Starts here-->
    <div id="deleteModal"  class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDelete" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Eliminar Parámetro</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este parámetro?
                            <asp:HiddenField ID="hClienteDel" runat="server" />
                                                    
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="waves-effect waves-light btn" OnClick="btnDelete_Click" />
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
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