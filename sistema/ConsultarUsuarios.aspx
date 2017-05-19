<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultarUsuarios.aspx.cs" Inherits="sistema_ConsultarUsuarios" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Listar Usuarios</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row text-center form-group">
                    <div class="col-lg-3"></div>
                    <div class="col-lg-6">                       
                         <asp:DropDownList ID="dplEmpresas" runat="server" CssClass="browser-default" 
                                           AutoPostBack="true" OnSelectedIndexChanged="dplEmpresas_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-lg-3"></div>
                 </div>
                <div class="row text-center form-group">
                    <div class="col-lg-3"></div>
                    <div class="col-lg-6">
                        <asp:DropDownList ID="dplRol" runat="server" CssClass="browser-default" AutoPostBack="true" OnSelectedIndexChanged="dplRol_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-lg-3"></div>
                </div>
                <div class="row text-center form-group">
                    <div class="col-lg-3"></div>
                    <div class="col-lg-6">
                        <div class="col s8 input-field">
                            <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                            <label for="txtSearch">Buscar</label>
                        </div>
                        <div class="col s4">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />                                                                                       
                        </div>
                    </div>
                    <div class="col-lg-3"></div>
                </div>
                <div class="row text-center form-group">
                    <div class="col-lg-12">
                        <div class="col-lg-10"></div>
                        <div class="col-lg-1 text-right">
                            <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="btnAgregar_Click" />
                        </div>
                        <div class="col-lg-1 text-right">
                            <asp:ImageButton ID="ImgButton3" runat="server"  ImageUrl="~/Images/descargar.png" OnClick="btnArchivo_Click" />     
                        </div>
                    </div>
                </div>
                <div class="row text-center form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="95%" HorizontalAlign="Center"
                    AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="UsuarioID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging" AllowSorting="true" OnSorting="GridView1_Sorting"
                    onrowcommand="GridView1_RowCommand"
                    EmptyDataText="No Existen Usuarios."
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <Columns>                        
                        
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="Nombres" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombres") %>' />
                                <asp:Label ID="Nombre" runat="server" Visible="false" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                <asp:Label ID="Apellido" runat="server" Visible="false" Enabled="false" Text='<%# Eval("UsuarioApellido") %>' />
                                <asp:Label ID="IDUsuario_1" Visible="false" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />  
                            </ItemTemplate>
                        </asp:TemplateField>
                         
                        <asp:TemplateField HeaderText="Cédula">
                            <ItemTemplate>
                                <asp:Label ID="Cedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="Contraseña">
                            <ItemTemplate>
                                <asp:Label ID="Clave" runat="server" Enabled="false" Text='<%# Eval("UsuarioClave") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="Correo" runat="server" Enabled="false" Text='<%# Eval("UsuarioCorreo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> 
                       <asp:TemplateField HeaderText="Teléfono">
                            <ItemTemplate>
                                <asp:Label ID="Telefono" runat="server" Enabled="false" Text='<%# Eval("UsuarioTelefono") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="Unidad">
                            <ItemTemplate>
                                <asp:Label ID="Empresa" runat="server" Enabled="false"  Text='<%# Eval("ClienteID") %>'/>
                                <asp:Label Visible="false" ID="ClienteID" runat="server" Enabled="false"  Text='<%# Eval("EmpresaID") %>'/>
                                <asp:Label Visible="false" ID="Observacion" runat="server" Enabled="false"  Text='<%# Eval("UsuarioObservacion") %>'/>
                                <asp:Label Visible="false" ID="EPS" runat="server" Enabled="false"  Text='<%# Eval("UsuarioEPS") %>'/>
                                <asp:Label Visible="false" ID="Celular2" runat="server" Enabled="false"  Text='<%# Eval("UsuarioCelular2") %>'/>
                                <asp:Label Visible="false" ID="Celular1" runat="server" Enabled="false"  Text='<%# Eval("UsuarioCelular1") %>'/>
                                <asp:Label Visible="false" ID="Sexo" runat="server" Enabled="false"  Text='<%# Eval("UsuarioSexo") %>'/>
                                <asp:Label Visible="false" ID="EstadoC" runat="server" Enabled="false"  Text='<%# Eval("UsuarioEstadoCivil") %>'/>
                                <asp:Label Visible="false" ID="FechaN" runat="server" Enabled="false"  Text='<%# Eval("UsuarioFechaNacimiento") %>'/>
                                
                            </ItemTemplate>
                        </asp:TemplateField>                                              
                        <asp:TemplateField HeaderText="Rol">
                            <ItemTemplate>
                                <asp:Label ID="Rol" runat="server" Enabled="false"  Text='<%# Eval("RolDescripcion") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>   
                        <asp:ButtonField CommandName="viewRecord" 
                            ButtonType="Image" ImageUrl="~/Images/ver.png" HeaderText="Ver" >
                            <ControlStyle ></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="editRecord"
                            ButtonType="Image" ImageUrl="~/Images/editar.png" HeaderText="Mod">
                            <ControlStyle ></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="deleteRecord"
                            ButtonType="Image" ImageUrl="~/Images/eliminar.png" HeaderText="Eli">
                            <ControlStyle ></ControlStyle>
                        </asp:ButtonField>                                             
                    </Columns>
                </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgButton3" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!-- View Modal Starts here -->
        <div id="viewModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="Button1" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h5>Ver Usuario</h5>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="HiddenField1" runat="server" />  
                                <div class="row">
                                    <div class="col s2"></div>
                                    <div class="col s8">
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtCedulaVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtCedulaVer" class="active">Cedula</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtClaveVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtClaveVer" class="active">Clave</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtNombreVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtNombreVer" class="active">Nombre</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtApellidoVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtApellidoVer" class="active">Apellido</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtRolVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtRolVer" class="active">Rol</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtCorreoVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtCorreoVer" class="active">Correo</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtFechaNVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtFechaNVer" class="active">Fecha de Nacimiento</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtEstadoCVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtEstadoCVer" class="active">Estado Civíl</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtSexoVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtSexoVer" class="active">Sexo</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtTelefonoVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtTelefonoVer" class="active">Teléfono</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtCelular1Ver" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtCelular1Ver" class="active">Celular #1</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtCelular2Ver" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtCelular2Ver" class="active">Celular #2</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtEPSVer" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtEPSVer" class="active">EPS</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtObserVer"  runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtObserVer" class="active">Observacion</label>
                                        </div>
                                    </div>
                                    <asp:Panel ID="panel1" runat="server"></asp:Panel>   
                                    <div class="col s2"></div>                                    
                                </div>                               
                            </div>
                            <div class="modal-footer">
                                <asp:Label ID="Label3" Visible="false" runat="server"></asp:Label>
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
        <!-- View Modal Ends here -->
            <!-- Edit Modal Starts here -->
        <div id="editModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h5>Modificar Usuario</h5>                                            
                    </div>
                    <asp:UpdatePanel ID="upEdit" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <asp:HiddenField ID="hClienteIDMod" runat="server" />  
                                <div class="row">
                                    <div class="col s2"></div>
                                    <div class="col s8">
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtCedulaEdit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtCedulaEdit" class="active">Cedula</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="TextBox2" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtClaveVer" class="active">Clave</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtNombreEdit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtNombreEdit" class="active">Nombre</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtApellidoEdit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtApellidoEdit" class="active">Apellido</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtRolEdit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtRolEdit" class="active">Rol</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtCorreoEdit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtCorreoEdit" class="active">Correo</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <input ID="txtFechaNEdit2" runat="server" Enabled="false" />
                                            <label for="txtFechaNEdit2" class="active">Fecha de Nacimiento</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:DropDownList runat="server" ID="txtEstCiv" CssClass="browser-default" >
                                                <asp:ListItem Text="Soltero(a)" Value="S"  />
                                                <asp:ListItem Text="Casado(a)" Value="C"  />
                                                <asp:ListItem Text="Divorciado(a)" Value="D"  />
                                                <asp:ListItem Text="Viudo(a)" Value="V"  />
                                                <asp:ListItem Text="Otro(a)" Value="O"  />
                                            </asp:DropDownList> 
                                        </div>
                                        <div class="form-group row">
                                            <input  type="radio" runat="server" name="SexoOpcion" id="rdM" value="M" checked="true"/>
                                            <label for="rdM">Masculino</label>
                                            <input type="radio" runat="server" name="SexoOpcion" id="rdF" value="F" /> 
                                            <label for="rdF">Femenino</label>       
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtTelefonoEdit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtTelefonoEdit" class="active">Teléfono</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtCelular1Edit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtCelular1Edit" class="active">Celular #1</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtCelular2Edit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtCelular2Edit" class="active">Celular #2</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtEPSEdit" runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtEPSEdit" class="active">EPS</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtObserEdit"  runat="server" Enabled="false"></asp:TextBox> 
                                            <label for="txtObserEdit" class="active">Observacion</label>
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtClave" runat="server"></asp:TextBox> 
                                            <label for="txtClave" class="active">Clave</label>                                                               
                                        </div>
                                        <div class="form-group row input-field">
                                            <asp:TextBox ID="txtClave2" runat="server"></asp:TextBox> 
                                            <label for="txtClave2" class="active">Repita la Clave</label>                                                               
                                        </div>
                                    </div> 
                                    <asp:Panel ID="panel11" runat="server">
                                        <!-- Campos Adicionales -->
                                    </asp:Panel>  
                                    <div class="col s2"></div>                                    
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                                <asp:Button ID="btnSave" runat="server" Text="Modificar" CssClass="btn btn-purple" OnClick="btnSave_Click" />
                                <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
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
                        <h3 id="delModalLabel">Eliminar Usuario</h3>
                    </div>
                    <asp:UpdatePanel ID="upDel" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                ¿Seguro desea eliminar este registro?
                                <asp:HiddenField ID="hfCedula" runat="server" />
                                                    
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-purple" OnClick="btnDelete_Click" />
                                <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cancelar</button>
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