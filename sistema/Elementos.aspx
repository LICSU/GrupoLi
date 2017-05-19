<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Elementos.aspx.cs" Inherits="sistema_Elementos" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Lista de Elementos</h5>
        </div>
        <div class="row"><br /></div>
        <div class="row">
            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-10">
                            <div class="row">
                                 <div class="col s6 input-field">
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                     <label for="txtSearch">Elemento</label>
                                </div>
                                <div class="col-lg-2 col-xs-4">
                                    <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="btn btn-purple" OnClick="btnSearch_Click" />                                                                                        
                                </div>
                                <div class="col-lg-2 col-xs-8">&nbsp;</div>
                                <div class="col-lg-1 col-xs-2">
                                    <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="btnAdd_Click" />
                                </div>
                                <div class="col-lg-1 col-xs-2 text-right">                                                
                                    <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="btnArchivo_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                                    DataKeyNames="NivelID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                                    onpageindexchanging="GridView1_PageIndexChanging" AllowSorting="true"
                                    EmptyDataText="No existen Elementos Agregados"
                                    PagerStyle-CssClass="pagination" 
                                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>   
                                        <asp:TemplateField HeaderText="Clase">
                                            <ItemTemplate>
                                                <asp:Label ID="ClaseElemNivID" Visible="false" runat="server" Enabled="false" Text='<%# Eval("ClaseElemNivID") %>' />
                                                <asp:Label ID="IDElemento_1" Visible="false" runat="server" Enabled="false" Text='<%# Eval("ElementoID") %>' />
                                                <asp:Label ID="Clase" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Nivel">
                                            <ItemTemplate>
                                                <asp:Label ID="Nivel" runat="server" Enabled="false" Text='<%# Eval("NivelNombre") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Elemento">
                                            <ItemTemplate>
                                                <asp:Label ID="Nombre" runat="server" Enabled="false" Text='<%# Eval("ElementoNombre") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Activo" Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Activo" runat="server" Enabled="false" Checked='<%# Eval("ElementoActivo") %>' />
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
                           
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ImgbtnArchivo" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- Add Modal Starts here -->
            <div id="addModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h5>Agregar Elemento(s)</h5>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-1"></div>
                                        <div class="col-lg-10">
                                            <div class="row">
                                                <div class="form-group input-field">
                                                    <asp:DropDownList ID="dplClaseAdd" runat="server" ClientIDMode="Static" CssClass="browser-default"></asp:DropDownList> 
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group input-field">
                                                    <asp:DropDownList ID="dplNivelAdd" runat="server" ClientIDMode="Static" CssClass="browser-default"></asp:DropDownList>      
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group input-field">
                                                    <asp:TextBox ID="txtNombreAdd" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                    <label for="txtNombreAdd">Elemento1,Elemento2,Elemento3</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-1"></div>
                                    </div>
                                    <div class="row" style="opacity:0;">
                                        <div class="form-group">
                                            <div class="col-xs-2"></div>
                                            <label class="col-xs-2 control-label">Activo: </label>
                                            <div class="col-xs-6">
                                                <asp:CheckBox ID="ActivoAdd" runat="server" CssClass="checkbox" />                                                             
                                            </div>
                                            <div class="col-xs-4"></div>
                                        </div>                                                                                    
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Label ID="Label3" Visible="false" runat="server"></asp:Label>
                                    <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="btn btn-purple"  OnClick="btnAgregar_Click"/>
                                    <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
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
       
                <!-- Edit Modal Starts here -->
            <div id="editModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h5>Modificar Elemento</h5>                                            
                        </div>
                        <asp:UpdatePanel ID="upEdit" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="row">
                                        <asp:HiddenField ID="hElementoMod" runat="server" />
                                        <div class="col-lg-2 col-xs-12"></div> 
                                        <div class="col-lg-8 col-xs-12">
                                            <div class="form-group input-field">
                                                <asp:TextBox ID="txtNombreEdit" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                <label for="txtNombreEdit" class="active"></label> 
                                            </div>
                                        </div>                                       
                                        <div class="form-group" style="opacity:0;">
                                            <div class="col-xs-2"></div>
                                            <label class="col-xs-2 control-label">Activo: </label>
                                            <div class="col-xs-6">
                                                <asp:CheckBox ID="chkActivoEdit" runat="server" CssClass="checkbox" />                                                             
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
                            <h3 id="delModalLabel">Eliminar Elemento</h3>
                        </div>
                        <asp:UpdatePanel ID="upDel" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    ¿Seguro desea eliminar este registro?
                                    <asp:HiddenField ID="hElementoID" runat="server" />
                                    <asp:HiddenField ID="hClaseElemNivID" runat="server" />
                                                    
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
                <h5 class="modal-title"><label id="lblMsjTitle"></label></h5>
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