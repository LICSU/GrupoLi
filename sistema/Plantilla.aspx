<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plantilla.aspx.cs" Inherits="sistema_Plantilla" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Cargar Plantilla de Clases</h4>
        </div>
        <div class="row"><br /></div>
        <div class="row">
            <div class="col-lg-2"></div>
            <div class="col-lg-4 col-xs-5">
                <asp:FileUpload ID="plantillaExcel" runat="server"  />   
                <asp:RegularExpressionValidator id="RegularExpressionValidator1"
                    runat="server" ErrorMessage="Solo Archivos TXT"
                    ValidationExpression ="^.+(.CSV|.csv)$"
                    ControlToValidate="plantillaExcel"> </asp:RegularExpressionValidator>
            </div>
            <div class="col-lg-4 col-xs-4">
                <asp:Button ID="btnCargarPlantilla" CssClass="btn" runat="server" Text="Cargar Plantilla"  OnClick="btnCargarPlantilla_Click" />
            </div>    
            <div class="col-lg-2"></div>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-9">
                        <div class="input-field col s6">
                            <input id="txtBuscar" runat="server" />
                            <label for="txtBuscar">Busqueda por Fecha (dd/mm/yyyy)</label>
                        </div>
                        <div class="col-lg-2 col-xs-4">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="btn btn-purple" OnClick="btnSearch_Click" />                                                                                        
                        </div>
                        <div class="col-lg-2 col-xs-8">&nbsp;</div>
                        <div class="col-lg-2 col-xs-2 text-right"> 
                            <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/images/descargar.png"  OnClick="btnArchivo_Click" />
                        </div>
                      </div>
                    <div class="col-lg-1"></div>
                </div>
                <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                    AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="ClasePlantillaID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                    EmptyDataText="No Existen Clases Cargadas" PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                    <Columns>                        
                        <asp:TemplateField HeaderText="Clase" >
                            <ItemTemplate>
                                <asp:Label runat="server" Visible="false"  ID="ProfesorID" Text='<%# Eval("ProfesorID") %>' />
                                <asp:Label runat="server" Visible="false"  ID="ClasePlantillaID" Text='<%# Eval("ClasePlantillaID") %>' />
                                <asp:Label ID="ClaseDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Día">
                            <ItemTemplate>
                                <asp:Label ID="ClasePlantillaFecha" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaFecha") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hora" >
                            <ItemTemplate>
                                <asp:Label ID="ClasePlantillaHora" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaHora") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cupos" >
                            <ItemTemplate>
                                <asp:Label ID="ClasePlantillaCupo" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaCupo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Profesor" >
                            <ItemTemplate>
                                <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empresa">
                            <ItemTemplate>
                                <asp:Label ID="ClienteDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClienteDescripcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salon">
                            <ItemTemplate>
                                <asp:Label ID="SalonNombre" runat="server" Enabled="false" Text='<%# Eval("SalonNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activa" Visible="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActiva" runat="server" Enabled="false" Checked='<%# Eval("ClasePlantillaActiva") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="editRecord"
                            ButtonType="Image" ImageUrl="~/images/editar.png" HeaderText="Mod">
                            <ControlStyle ></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="deleteRecord"
                            ButtonType="Image" ImageUrl="~/images/eliminar.png" HeaderText="Eli">
                            <ControlStyle ></ControlStyle>
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ImgbtnArchivo" />
            </Triggers>
        </asp:UpdatePanel> 
        </div>
    </div>

    <!-- Edit Modal Starts here -->
    <div id="editModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h3 id="editModalLabel">Modificar Plantilla</h3>                                            
                    </div>
                    <asp:UpdatePanel ID="upEdit" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <div class="row">
                                    <asp:HiddenField ID="hClaseMod" runat="server" /> 
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Nombre: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtNombreEdit" runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Fecha: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtFechaEdit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div> 
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Hora: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtHoraEdit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Cupo: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtCupoEdit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Nombre Profesor: </label>
                                        <div class="col-xs-6">
                                            <asp:DropDownList ID="dplProfesor" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:DropDownList>
                                            <!--<asp:TextBox ID="txtNombrePEdit"  runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>   -->                                                             
                                        </div>
                                    </div>   
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Empresa: </label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtEmpresaEdit" Enabled="false" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                        </div>
                                    </div>  
                                    <div class="form-group">
                                        <label class="col-xs-4 control-label">Activa: </label>
                                        <div class="col-xs-6">
                                            <asp:CheckBox ID="chkActivaEdit" runat="server" ClientIDMode="Static" CssClass="checkbox"></asp:CheckBox>                                                                
                                        </div>
                                    </div>                
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
                    <h3 id="delModalLabel">Eliminar Clase</h3>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField ID="hClaseDel" runat="server" />
                                                    
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