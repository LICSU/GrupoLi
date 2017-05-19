<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Empresas.aspx.cs" Inherits="sistema_Empresas" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Unidades de Negocio</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col-lg-1"></div>
                    <div class="col-lg-10">
                        <div class="col-lg-6 input-field">
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                            <label for="txtSearch">Buscar</label>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />                                                                                        
                        </div>
                        <div class="col-lg-2 col-xs-8">&nbsp;</div>
                        <div class="col-lg-1 text-right">
                            <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="btnAdd_Click" />
                        </div>
                        <div class="col-lg-1 text-right">                                                
                            <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="btnArchivo_Click" />
                        </div>
                    </div>
                    <div class="col-lg-1"></div>
                </div>
                <div class="form-group row">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="ClienteID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging"
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="Nombre" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                                <asp:Label Visible="false" ID="IDCliente_1" runat="server" Enabled="false" Text='<%# Eval("ClienteID") %>' />
                                <asp:Label Visible="false" ID="FormularioTipo" runat="server" Enabled="false" Text='<%# Eval("FormularioTipo") %>' />
                                <asp:Label Visible="false" ID="Correo" runat="server" Enabled="false" Text='<%# Eval("ClienteCorreo") %>' />
                                <asp:Label Visible="false" ID="Parroquia" runat="server" Enabled="false" Text='<%# Eval("ParroquiaID") %>' />
                                <asp:Label Visible="false" ID="Direccion" runat="server" Enabled="false" Text='<%# Eval("ClienteDireccion") %>' />
                                <asp:Label Visible="false" ID="Telefono1" runat="server" Enabled="false" Text='<%# Eval("ClienteTelefono1") %>' />
                                <asp:Label Visible="false" ID="Telefono2" runat="server" Enabled="false" Text='<%# Eval("ClienteTelefono2") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción">
                            <ItemTemplate>
                                <asp:Label ID="Descripcion" runat="server" Enabled="false" Text='<%# Eval("ClienteDescripcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contacto">
                            <ItemTemplate>
                                <asp:Label ID="Contacto" runat="server" Enabled="false" Text='<%# Eval("ClientePersonaContacto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:ButtonField CommandName="viewRecord" 
                            ButtonType="Image" ImageUrl="~/Images/ver.png" HeaderText="Ver">
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
                <h5>Agregar Cliente</h5>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                                <div class="form-group input-field">
                                    <asp:TextBox ID="txtNombreAdd" runat="server"></asp:TextBox> 
                                    <label for="txtNombreAdd">Nombre: </label> 
                                </div>
                                <div class="form-group input-field">
                                    <asp:TextBox ID="txtDescripcionAdd" runat="server"></asp:TextBox> 
                                    <label for="txtDescripcionAdd">Descripción: </label> 
                                </div>
                                <div class="form-group input-field">
                                    <asp:TextBox ID="txtPersonaContactoAdd" runat="server"></asp:TextBox> 
                                    <label for="txtPersonaContactoAdd">Persona de Contacto: </label> 
                                </div>
                                <div class="form-group input-field">
                                    <asp:DropDownList ID="txtParroquiaAdd" CssClass="browser-default" runat="server"></asp:DropDownList> 
                                </div>
                                <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtDireccionAdd" runat="server"></asp:TextBox>  
                                        <label for="txtDireccionAdd" class="active">Dirección: </label>
                                    </div>
                                <div class="form-group input-field">
                                    <asp:TextBox ID="txtTelefono1Add" runat="server"></asp:TextBox> 
                                    <label for="txtTelefono1Add">Teléfono #1: </label> 
                                </div>
                                <div class="form-group input-field">
                                    <asp:TextBox ID="txtTelefono2Add" runat="server"></asp:TextBox> 
                                    <label for="txtTelefono2Add">Teléfono #2: </label> 
                                </div>
                                <div class="form-group input-field">
                                    <asp:TextBox ID="txtCorreoAdd" runat="server"></asp:TextBox> 
                                    <label for="txtCorreoAdd">Correo: </label> 
                                </div>
                                <div class="form-group input-field">
                                    <asp:DropDownList ID="dplFormulario" runat="server" CssClass="browser-default">
                                        <asp:ListItem Text="Seleccione el Formulario" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Formulario Tipo Licsu" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Formulario Tipo Empresa" Value="2"></asp:ListItem>
                                    </asp:DropDownList>      
                                </div>
                            </div>
                            <div class="col-lg-2"></div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Label ID="Label3" Visible="false" runat="server"></asp:Label>
                        <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="waves-effect waves-light btn"  OnClick="btnAgregar_Click"/>
                        <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click"  />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<!-- Add Modal Ends here -->
<!-- View Modal Starts here -->
<div id="viewModal" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button id="Button1" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h5>Ver Cliente</h5>                                            
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">
                                <div class="form-group row">
                                    <label>Nombre</label>
                                    <asp:Label ID="lblNombreView" runat="server" ClientIDMode="Static" CssClass="form-control" Enabled="false"></asp:Label>  
                                </div>                            
                                <div class="form-group row">
                                    <label>Descripción: </label>
                                        <asp:Label ID="lblDescripcionView" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>                                                                
                                </div>    
                                <div class="form-group row">
                                    <label>Persona de Contacto: </label>
                                    <asp:Label ID="lblContactoView" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>                                                                
                                </div>    
                                <div class="form-group row">
                                    <label>Parroquia: </label>
                                    <asp:Label runat="server" ID="lblParroquiaView" ClientIDMode="Static" CssClass="form-control"></asp:Label> 
                                </div> 
                                <div class="form-group row">
                                    <label>Dirección: </label>
                                    <asp:Label ID="lblDireccionView" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>                                                                
                                </div>
                                <div class="form-group">
                                    <label>Teléfono 1: </label>
                                    <asp:Label ID="lblTelefono1View" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>                                                                
                                </div>
                                <div class="form-group">
                                    <label>Teléfono 2: </label>
                                    <asp:Label ID="lblTelefono2View" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>                                                                
                                </div>
                                <div class="form-group ">
                                    <label>Correo: </label>
                                    <asp:Label ID="lblCorreoView" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>                                                                
                                </div>  
                                <div class="form-group input-field">
                                    <asp:DropDownList ID="dplFormluarioView" Enabled="false" runat="server" ClientIDMode="Static" CssClass="browser-default">
                                        <asp:ListItem Text="Seleccione el Formulario por Defecto" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Formulario Tipo Licsu" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Formulario Tipo Empresa" Value="2"></asp:ListItem>
                                    </asp:DropDownList>                                                                
                                </div>  
                              </div>
                             <div class="col-lg-1"> </div>      
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
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
                <h5>Modificar Cliente</h5>                                            
            </div>
            <asp:UpdatePanel ID="upEdit" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <asp:HiddenField ID="hClienteMod" runat="server" /> 
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtNombreEdit" runat="server"></asp:TextBox>  
                                        <label for="txtNombreEdit" class="active">Nombre: </label>
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtDescripcionEdit" runat="server"></asp:TextBox>  
                                        <label for="txtDescripcionEdit" class="active">Descripción: </label>
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtContactoEdit" runat="server"></asp:TextBox>  
                                        <label for="txtContactoEdit" class="active">Persona de Contacto: </label>
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:DropDownList ID="txtParroquiaEdit" runat="server"  CssClass="browser-default"></asp:DropDownList>  
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtDireccionEdit" runat="server"></asp:TextBox>  
                                        <label for="txtDireccionEdit" class="active">Dirección: </label>
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtTelefono1Edit" runat="server"></asp:TextBox>  
                                        <label for="txtTelefono1Edit" class="active">Teléfono #1: </label>
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtTelefono2Edit" runat="server"></asp:TextBox>  
                                        <label for="txtTelefono2Edit" class="active">Teléfono #2: </label>
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtCorreoEdit" runat="server"></asp:TextBox>  
                                        <label for="txtCorreoEdit" class="active">Correo: </label>
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:DropDownList ID="dplFormluarioEdit" runat="server" CssClass="browser-default">
                                            <asp:ListItem Text="Seleccione el Formulario" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Formulario Tipo Licsu" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Formulario Tipo Empresa" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
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
                <h5>Eliminar Cliente</h5>
            </div>
            <asp:UpdatePanel ID="upDel" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        ¿Seguro desea eliminar este registro?
                        <asp:HiddenField ID="hClienteDel" runat="server" />
                                                    
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
        <h5><label id="lblMsjTitle"></label></h5>
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