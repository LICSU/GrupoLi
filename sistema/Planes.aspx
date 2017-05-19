<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Planes.aspx.cs" Inherits="sistema_Planes" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Listar Planes</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row text-center">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <div class="text-center form-group">
                            <div class="col-lg-8 input-field">
                                <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                                <label for="txtSearch">Buscar</label>
                            </div>
                            <div class="col-xs-4">
                                <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />                                                                                        
                            </div>
                        </div>
                        <div class="text-center form-group">
                            <div class="col s10"></div>
                            <div class="col s1">
                                <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="btnAdd_Click" />
                            </div>
                            <div class="col s1">
                                <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="btnArchivo_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="form-group text-center row">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="PlanID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging"
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <asp:TemplateField HeaderText="Nombre" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label  ID="PlanNombre" runat="server" Enabled="false" Text='<%# Eval("PlanNombre") %>' />
                                <asp:Label ID="PlanID" Visible="false" runat="server" Enabled="false" Text='<%# Eval("PlanID") %>' />
                                <asp:Label  ID="PlanDescripcion" Visible="false" runat="server" Enabled="false" Text='<%# Eval("PlanDescripcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha de Fin"  Visible="false">
                            <ItemTemplate>
                                <asp:Label  ID="PlanFechaFin" runat="server" Enabled="false" Text='<%# Eval("PlanFechaFin") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha de Inicio"  Visible="false">
                            <ItemTemplate>
                                <asp:Label  ID="PlanFechaInicio" runat="server" Enabled="false" Text='<%# Eval("PlanFechaInicio") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Complementarias" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="clasesComplemen" runat="server" Enabled="false" Text='<%# Eval("clasesComplemen") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Regulares" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="clasesRegulares" runat="server" Enabled="false" Text='<%# Eval("clasesRegulares") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Costo" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="PlanCosto" runat="server" Enabled="false" Text='<%# Eval("PlanCosto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Duracion (dias)" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="PlanDias" runat="server" CssClass="text-center" Enabled="false" Text='<%# Eval("PlanDias") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField CommandName="editRecord"
                            ButtonType="Image" ImageUrl="~/Images/editar.png" HeaderText="Modificar" HeaderStyle-CssClass="text-center">
                            <ControlStyle></ControlStyle>
                        </asp:ButtonField> 
                        <asp:ButtonField CommandName="deleteRecord" 
                            ButtonType="Image" ImageUrl="~/Images/eliminar.png" HeaderText="Eliminar" HeaderStyle-CssClass="text-center">
                            <ControlStyle></ControlStyle>
                        </asp:ButtonField>  
                        <asp:ButtonField CommandName="Desactivar" 
                            ButtonType="Button" Text="Desactivar" ControlStyle-CssClass="waves-effect waves-light btn" HeaderText="Desactivar" HeaderStyle-CssClass="text-center">
                            <ControlStyle></ControlStyle>
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
                    <h5>Agregar Plan</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtNombreAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="txtNombreAdd">Nombre</label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtDescripcionAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="txtDescripcionAdd">Descripción</label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtFechaIniAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="txtFechaIniAdd">Fecha Inicio</label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtFechaFinAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="txtFechaFinAdd">Fecha Fin</label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="clasesComplemenAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="clasesComplemenAdd">Clases Complementarias</label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="clasesRegularesAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="clasesRegularesAdd">Clases Regulares</label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtCostoAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="txtCostoAdd">Costo</label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtDuracionAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="txtDuracionAdd">Duracion (dias)</label>
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
                    <h5>Editar Plan</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="hPlanIDEdit" runat="server" /> 
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
                                        <asp:TextBox ID="txtFechaIniEdit" runat="server"></asp:TextBox>  
                                        <label for="txtFechaIniEdit" class="active">Fecha Inicio</label> 
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtFechaFinEdit" runat="server"></asp:TextBox>  
                                        <label for="txtFechaFinEdit" class="active">Fecha Fin</label> 
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="clasesComplemenEdit" runat="server"></asp:TextBox>  
                                        <label for="clasesComplemenEdit" class="active">Clases Complementarias</label> 
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="clasesRegularesEdit" runat="server"></asp:TextBox>  
                                        <label for="clasesRegularesEdit" class="active">Clases Regulares</label> 
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtCostoEdit" runat="server"></asp:TextBox>  
                                        <label for="txtCostoEdit" class="active">Costo</label> 
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtDuracionEdit" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                        <label for="txtDuracionEdit" class="active">Duracion (dias)</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="Editar" runat="server" Text="Guardar" CssClass="waves-effect waves-light btn"  OnClick="Editar_Click"/>
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
                    <h5>Eliminar Plan</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField ID="hPlanDel" runat="server" />
                                                    
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
    <!-- Delete Record Modal Starts here-->
    <div id="desactivarModal"  class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDesact" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Inactivar Plan</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea desactivar este Plan?
                            <asp:HiddenField ID="hPlanDes" runat="server" />
                                                    
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDesactivar" runat="server" Text="Eliminar" CssClass="waves-effect waves-light btn" OnClick="btnDesactivar_Click" />
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