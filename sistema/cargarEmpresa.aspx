﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cargarEmpresa.aspx.cs" Inherits="sistema_cargarEmpresa" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Cargar Empleados</h5>
        </div>
        <div class="row"><br /></div>
        <div class="row form-group">
            <asp:PlaceHolder runat="server" ID="phUnidad">
            <div class="form-group">
                <asp:Label ID="lblPlan" runat="server" AssociatedControlID="dplUnidad" CssClass="col-sm-3 control-label">Unidad de Negocio:</asp:Label>     
                <div class="col-sm-5 selectContainer">
                    <asp:DropDownList runat="server" ID="dplUnidad" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            </asp:PlaceHolder>
        </div>
        <div class="row form-group text-center">
            <div class="col-lg-2"></div>
            <div class="col-lg-8">
                <div class="col-lg-8">
                <asp:FileUpload ID="plantillaEmpleados" runat="server"  />   
                    <asp:RegularExpressionValidator id="RegularExpressionValidator1"
                        runat="server" ErrorMessage="Solo Archivos .CSV"
                        ValidationExpression ="^.+(.CSV|.csv)$"
                        ControlToValidate="plantillaEmpleados"> 
                    </asp:RegularExpressionValidator>
                </div>
                <div class="col-lg-4">
                    <asp:Button ID="btnCargarPlantilla" CssClass="waves-effect waves-light btn" runat="server" Text="Cargar Empleados"  OnClick="btnCargarPlantilla_Click" />
                </div>
            </div>
            <div class="col-lg-2"></div>
        </div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group text-center">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <div class="input-field col s8">
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                            <label for="txtSearch">Buscar</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="btn waves-effect waves-light" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row form-group text-center">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                    AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="EmpleadoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                    onpageindexchanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" EmptyDataText="No existen Empleados Cargados">
                    <Columns>
                        <asp:TemplateField HeaderText="Cédula" >
                            <ItemTemplate>
                                <asp:Label Visible="false" runat="server"  ID="EmpleadoNivel1" Text='<%# Eval("EmpleadoNivel1") %>' />
                                <asp:Label Visible="false" runat="server"  ID="EmpleadoNivel2" Text='<%# Eval("EmpleadoNivel2") %>' />
                                <asp:Label Visible="false" runat="server"  ID="EmpleadoNivel3" Text='<%# Eval("EmpleadoNivel3") %>' />
                                <asp:Label Visible="false" runat="server"  ID="EmpleadoEmail" Text='<%# Eval("EmpleadoEmail") %>' />
                                <asp:Label Visible="false" runat="server"  ID="EmpleadoNivel4" Text='<%# Eval("EmpleadoNivel4") %>' />
                                <asp:Label Visible="false" runat="server"  ID="EmpleadoID" Text='<%# Eval("EmpleadoID") %>' />
                                <asp:Label ID="EmpleadoCedula" runat="server" Enabled="false" Text='<%# Eval("EmpleadoCedula") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="EmpleadoNombre" runat="server" Enabled="false" Text='<%# Eval("EmpleadoNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cargo" >
                            <ItemTemplate>
                                <asp:Label ID="EmpleadoCargo" runat="server" Enabled="false" Text='<%# Eval("EmpleadoCargo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha de Ingreso" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="EmpleadoFechaIng" runat="server" Enabled="false" Text='<%# Eval("EmpleadoFechaIng") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empresa">
                            <ItemTemplate>
                                <asp:Label ID="ClienteNombre" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
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
    <!-- Edit Modal Starts here -->
    <div id="editModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h3 id="editModalLabel">Modificar Empleado</h3>
                                            
                </div>
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="hdfEmpleadoIDEdit" runat="server" /> 
                                    <div class="form-group">
                                    <label class="col-xs-4 control-label">Cédula: </label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txtCedulaEdit" runat="server" Enabled="false" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                    </div>
                                    <div class="col-xs-4"></div>
                                </div>
                                <div class="form-group">
                                    <label class="col-xs-4 control-label">Nombre(s): </label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txtNombreEdit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                    </div>
                                    <div class="col-xs-4"></div>
                                </div>     
                                <div class="form-group">
                                    <label class="col-xs-4 control-label">Cargo: </label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txtCargoEdit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                    </div>
                                    <div class="col-xs-4"></div>
                                </div>  
                                <div class="form-group">
                                    <label class="col-xs-4 control-label">Nivel 1: </label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txtNivel1Edit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                    </div>
                                    <div class="col-xs-4"></div>
                                </div>  
                                <div class="form-group">
                                    <label class="col-xs-4 control-label">Nivel 2: </label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txtNivel2Edit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                    </div>
                                    <div class="col-xs-4"></div>
                                </div>  
                                <div class="form-group">
                                    <label class="col-xs-4 control-label">Nivel 3: </label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txtNivel3Edit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                    </div>
                                    <div class="col-xs-4"></div>
                                </div>  
                                <div class="form-group">
                                    <label class="col-xs-4 control-label">NIvel 4: </label>
                                    <div class="col-xs-6">
                                        <asp:TextBox ID="txtNivel4Edit" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>                                                                
                                    </div>
                                    <div class="col-xs-4"></div>
                                </div>    
                                <div class="form-group">
                                    <label class="col-xs-4 control-label">Email: </label>
                                    <div class="col-xs-6">
                                        <asp:TextBox runat="server" ID="txtEmailEdit" ClientIDMode="Static" CssClass="form-control"></asp:TextBox> 
                                    </div>
                                    <div class="col-xs-4"></div>
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
                    <h3 id="delModalLabel">Eliminar Empleado</h3>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField ID="hdfEmpleadoIDDel" runat="server" />
                                                    
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
