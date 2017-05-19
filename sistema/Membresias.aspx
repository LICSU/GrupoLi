<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Membresias.aspx.cs" Inherits="sistema_Membresias" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Matricula</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col s6 input-field">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <label for="txtSearch">Cédula</label>
                    </div>
                    <div class="col s2 input-field">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />                                                                                        
                    </div>
                    <div class="col-lg-1 text-right">
                        <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="Add_Click" />
                    </div>
                    <div class="col-lg-1 text-right">
                        <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="ImgbtnArchivo_Click" />
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="BonoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        EmptyDataText="No existen Matricula Agregadas"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>     
                            <asp:TemplateField HeaderText="Número">
                                <ItemTemplate>
                                    <asp:Label ID="MembresiaID" Visible="false" runat="server" Enabled="false" Text='<%# Eval("MembresiaID") %>' />                                
                                    <asp:Label ID="MembresiaDocumento" runat="server" Enabled="false" Text='<%# Eval("MembresiaDocumento") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cédula">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Usuario">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioNombres" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombres") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Fecha Inicio">
                                <ItemTemplate>
                                    <asp:Label ID="MembresiaFechaInicio" runat="server" Enabled="false" Text='<%# Eval("FechaInicio") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Fin">
                                <ItemTemplate>
                                    <asp:Label ID="MembresiaFechaFin" runat="server" Enabled="false" Text='<%# Eval("FechaFin") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pago">
                                <ItemTemplate>
                                    <asp:Label ID="MembresiaPago" runat="server" Enabled="false" Text='<%# Eval("MembresiaPago") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>      
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
        </asp:UpdatePanel>
    </div>
    <!-- Add Modal Starts here -->
        <div id="addModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h5>Agregar Matricula</h5>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <div class="row">
                                    <asp:HiddenField runat="server" ID="hdfUsuarioID" />
                                    <div class="col-lg-2"></div>
                                    <div class="col-lg-8">
                                        <div class="form-group row">
                                            <div class="col s8 input-field">
                                                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                                                <label for="txtUsuario" class="active">Usuario: </label>
                                            </div>
                                            <div class="col s4 input-field">
                                                <asp:Button ID="BuscarUsr" runat="server" Text="Buscar" CssClass="waves-effect waves-light btn"  OnClick="BuscarUsr_Click"/>
                                            </div>
                                        </div>
                                        <div class="form-group input-field">
                                            <asp:TextBox ID="txtMembresiaDoc" runat="server"></asp:TextBox>
                                            <label for="txtMembresiaDoc">Número de Documento</label>
                                        </div>
                                        <div class="form-group input-field">
                                            <asp:TextBox ID="txtMembresiaPago" runat="server"></asp:TextBox>
                                            <label for="txtMembresiaPago">Pago</label>
                                        </div>
                                        <div class="form-group input-field">
                                            <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                                            <label for="txtFechaInicio">Fecha de Inicio</label>
                                        </div>
                                        <div class="form-group input-field">
                                            <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                                            <label for="txtFechaFin">Fecha Final</label>
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
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <img src="../Images/cargando2.gif" class="img-responsive center-block"><br />
                                <h5 class="text-center">Espere por Favor..</h5>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
            </div>
        </div>
        <!-- Add Modal Ends here -->
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
                            onpageindexchanging="GridView2_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Nombre" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioNombreB" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                        <asp:HiddenField ID="UsuarioIDB" runat="server" Value='<%# Eval("UsuarioID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Apellido" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioApellidoB" runat="server" Enabled="false" Text='<%# Eval("UsuarioApellido") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cedula" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioCedulaB" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="ClienteNombreB" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField CommandName="selectRecord" ButtonType="Button" Text="Seleccionar" HeaderText="Seleccionar">
                                    <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                                </asp:ButtonField>                                                                   
                            </Columns>
                        </asp:GridView>
                        <div class="modal-footer">
                            <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="../Images/cargando2.gif" class="img-responsive center-block"><br />
                            <h5 class="text-center">Espere por Favor..</h5>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
    </div>
    <!-- BUscar Modal Ends here -->
    <!-- Edit Modal Starts here -->
    <div id="editModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Editar Matricula</h5>                                            
                </div>
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                 <asp:HiddenField runat="server" ID="hdfMembresiaID" />
                                <div class="col-xs-2"></div>
                                <div class="col-xs-8">
                                    <div class="text-group input-field">
                                        <asp:TextBox Enabled="false" ID="txtCedulaEdit" runat="server"></asp:TextBox>
                                        <label class="active" for="txtCedulaEdit">Cédula: </label>
                                    </div>
                                    <div class="text-group input-field">
                                        <asp:TextBox Enabled="false" ID="txtNombresEdit" runat="server"></asp:TextBox>
                                        <label class="active" for="txtNombresEdit">Nombres: </label>
                                    </div>
                                    <div class="text-group input-field">
                                        <asp:TextBox ID="txtDocumentoEdit" runat="server"></asp:TextBox>
                                        <label class="active" for="txtDocumentoEdit">Número de Documento: </label>
                                    </div>
                                    <div class="text-group input-field">
                                        <asp:TextBox ID="txtPagoEdit" runat="server"></asp:TextBox>
                                        <label class="active" for="txtPagoEdit">Pago: </label>
                                    </div>
                                    <div class="text-group input-field">
                                        <asp:TextBox ID="txtFechaInicioEdit" runat="server"></asp:TextBox>
                                        <label class="active" for="txtFechaInicioEdit">Fecha Inicial: </label>
                                    </div>
                                    <div class="text-group input-field">
                                        <asp:TextBox ID="txtFechaFinEdit" runat="server"></asp:TextBox>
                                        <label class="active" for="txtFechaFinEdit">Fecha Final: </label>
                                    </div>
                                </div>
                                <div class="col-xs-2"></div>     
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Modificar" CssClass="btn btn-purple" OnClick="btnSave_Click" />
                            <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
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
                        <h5>Eliminar Matricula</h5>
                    </div>
                    <asp:UpdatePanel ID="upDel" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                ¿Seguro desea eliminar este registro?
                                <asp:HiddenField runat="server" ID="hdfMembresiaIDEli" />
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
