<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Asistencias.aspx.cs" Inherits="sistema_Asistencias" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h4>Asistencias</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate> 
                <div class="row form-group">
                    <div class="col-lg-1"></div>
                    <div class="col s6 input-field">
                        <asp:DropDownList ID="dplEstado" runat="server" CssClass="browser-default">
                            <asp:ListItem Value="" Text="Seleccione un Valor"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Asistió"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No Asistió"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col s4 text-left">
                        <asp:Button ID="btnBuscarEstado" runat="server" Text="Buscar por Estado"  CssClass="waves-effect waves-light btn" OnClick="btnBuscarEstado_Click" />                                                                                        
                    </div>
                    <div class="col-lg-1"></div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-1"></div>
                    <div class="col s6 input-field">
                        <asp:TextBox ID="txtBuscarUsuario" runat="server"></asp:TextBox>
                        <label for="txtBuscarUsuario">Cédula</label>
                    </div>
                    <div class="col s4 text-left">
                        <asp:Button ID="btnBuscarUsuario" runat="server" Text="Buscar por Usuario"  CssClass="waves-effect waves-light btn" OnClick="btnBuscarUsuario_Click" />                                                                                        
                    </div>
                    <div class="col-lg-1"></div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-1"></div>
                    <div class="col s6 input-field">
                        <asp:TextBox ID="txtBuscarClase" runat="server"></asp:TextBox>
                        <label for="txtBuscarClase">Clase</label>
                    </div>
                    <div class="col s4 text-left">
                        <asp:Button ID="Button1" runat="server" Text="Buscar por Clase"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />                                                                                        
                    </div>
                    <div class="col-lg-1"></div>
                </div>
                <div class="row form-group text-right">
                    <div class="col-lg-10"></div>
                    <div class="col-lg-1">
                        <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="Agregar_Click" />
                    </div>
                    <div class="col-lg-1">
                        <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="btnArchivo_Click" />
                    </div>
                </div>
                <div class="row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="ClaseAsistenciaID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existen Asistencias Registradas"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>                                            
                            <asp:ButtonField CommandName="editRecord" ButtonType="Button" Text="M" HeaderText="Mod">
                                <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="deleteRecord" ButtonType="Button" Text="E" HeaderText="Eli">
                                <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="AsitenciaID" runat="server" Enabled="false" Text='<%# Eval("ClaseAsistenciaID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Apellido">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioApellido" runat="server" Enabled="false" Text='<%# Eval("UsuarioApellido") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cédula">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clase">
                                <ItemTemplate>
                                    <asp:Label ID="ClaseDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="FechaClase" runat="server" Enabled="false" Text='<%# Eval("FechaClase") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Hora">
                                <ItemTemplate>
                                    <asp:Label ID="HoraClase" runat="server" Enabled="false" Text='<%# Eval("HoraClase") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Asistió">
                                <ItemTemplate>
                                    <asp:CheckBox ID="ClaseAsist" runat="server" Enabled="false" Checked='<%# Eval("ClaseAsist") %>' />
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
    <!-- Add Modal Starts here -->
        <div id="addModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4>Agregar Asistencia</h4>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="row">
                                        <div class="form-group input-field">    
                                            <asp:DropDownList OnSelectedIndexChanged="dplClases_SelectedIndexChanged" 
                                                              CssClass="browser-default" runat="server" ID="dplClases" AutoPostBack="true"></asp:DropDownList>                                                              
                                        </div>              
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>                                
                            </div>
                            <div class="modal-footer">
                                <asp:Label ID="Label3" Visible="false" runat="server"><br /></asp:Label>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
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
                        <h3 id="editModalLabel">Modificar Asistencia</h3>                                            
                    </div>
                    <asp:UpdatePanel ID="upEdit" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <div class="row">
                                    <asp:HiddenField ID="hAsistenciaMod" runat="server" /> 
                                        <div class="form-group">
                                        <label class="col-xs-4 control-label">Nombre: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtNombreEdit" runat="server"  Enabled="false" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                        <div class="col-xs-4"></div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Apellido: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtApellidoEdit" runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                        <div class="col-xs-4"></div>
                                    </div> 
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Cédula: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtCedulaEdit" runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                        <div class="col-xs-4"></div>
                                    </div>     
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Clase: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtClaseEdit" runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                        <div class="col-xs-4"></div>
                                    </div>    
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Fecha: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox runat="server" ID="txtFechaEdit" Enabled="false" ClientIDMode="Static" CssClass="form-control"></asp:TextBox> 
                                        </div>
                                        <div class="col-xs-4"></div>
                                    </div>     
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Hora: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtHoraEdit" runat="server" ClientIDMode="Static" Enabled ="false" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                        <div class="col-xs-4"></div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Asistió: </label>
                                        <div class="col-xs-6">
                                            <asp:CheckBox ID="chkAsistioEdit" runat="server" ClientIDMode="Static"  CssClass="checkbox"></asp:CheckBox>                                                                
                                        </div>
                                        <div class="col-xs-4"></div>
                                    </div>                                                
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                                <asp:Button ID="btnSave" runat="server" Text="Modificar" CssClass="waves-effect waves-light btn" OnClick="btnSave_Click" />
                                <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
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
                        <h3 id="delModalLabel">Eliminar Cliente</h3>
                    </div>
                    <asp:UpdatePanel ID="upDel" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                ¿Seguro desea eliminar este registro?
                                <asp:HiddenField ID="hAsitenciaDel" runat="server" />
                                                    
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="waves-effect waves-light btn" OnClick="btnDelete_Click" />
                                <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
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
