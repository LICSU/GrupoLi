<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bonos.aspx.cs" Inherits="sistema_Bonos" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Listar Bonos</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row text-center form-group">
                    <div class="col-lg-2"></div>
                     <div class="col-lg-6 input-field">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <label for="txtSearch">Bono</label>
                    </div>
                    <div class="col-xs-2 input-field">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />                                                                                        
                    </div>
                    <div class="col-lg-1 text-right">
                        <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="Add_Click" />
                    </div>
                    <div class="col-lg-1 text-right">
                        <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="ImgbtnArchivo_Click" />
                    </div>
                </div>
                <div class="row text-center form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="BonoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        EmptyDataText="No existen bonos creados"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>   
                            <asp:TemplateField HeaderText="Número">
                                <ItemTemplate>
                                    <asp:Label ID="ClienteID" Visible="false" runat="server" Enabled="false" Text='<%# Eval("ClienteID") %>' />
                                    <asp:Label ID="BonoID" Visible="false" runat="server" Enabled="false" Text='<%# Eval("BonoID") %>' />
                                    <asp:Label Visible="false" ID="BonoUsuarioID" runat="server" Enabled="false" Text='<%# Eval("BonoUsuarioID") %>' />
                                    <asp:Label ID="BonoNumero" runat="server" Enabled="false" Text='<%# Eval("BonoNumero") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Empresa">
                                <ItemTemplate>
                                    <asp:Label ID="BonoEmpresa" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <asp:CheckBox ID="BonoEstado" runat="server" Enabled="false" Checked='<%# Eval("BonoEstado") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Fecha Inicio">
                                <ItemTemplate>
                                    <asp:Label ID="FechaInicio" runat="server" Enabled="false" Text='<%# Eval("FechaInicio") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Fin">
                                <ItemTemplate>
                                    <asp:Label ID="FechaFin" runat="server" Enabled="false" Text='<%# Eval("FechaFin") %>' />
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
                    <h5>Agregar Bono</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox  ID="txtNumeroAdd" runat="server" ></asp:TextBox>
                                        <label for="txtNumeroAdd">Número</label> 
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:DropDownList AutoPostBack="true" ID="dplCantidad" runat="server" 
                                            OnSelectedIndexChanged="dplCantidad_SelectedIndexChanged" CssClass="browser-default">
                                            <asp:ListItem Text="Seleccione Cantidad de Bonos" Value=""></asp:ListItem>
                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:DropDownList ID="dplCliente" CssClass="browser-default" runat="server" ClientIDMode="Static"  ></asp:DropDownList>  
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtFechaInicio" runat="server" ></asp:TextBox>
                                        <label for="txtFechaInicio">Fecha Inicio(dd/mm/yyyy)</label> 
                                    </div>
                                    <div class="form-group text-center input-field">
                                        <asp:TextBox ID="txtFechaFinal" runat="server" ></asp:TextBox>
                                        <label for="txtFechaFinal">Fecha Final(dd/mm/yyyy)</label> 
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
    <!-- Add Modal Ends here -->
    <!-- Edit Modal Starts here -->
<div id="editModal" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h5>Modificar Bono</h5>                                            
            </div>
            <asp:UpdatePanel ID="upEdit" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-xs-2"></div>
                                <asp:HiddenField runat="server" ID="hdfBonoID" />
                                <div class="col-lg-8">
                                    <div class="text-center input-field form-group">
                                        <asp:TextBox Enabled="false" ID="txtNumeroEdit" runat="server" ></asp:TextBox>
                                        <label for="txtNumeroEdit" class="active">Número: </label>
                                    </div>
                                    <div class="text-center input-field form-group">
                                        <asp:DropDownList ID="dplEmpresaEdit" CssClass="browser-default" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="text-center input-field form-group">
                                        <asp:TextBox ID="txtFechaInicioEdit" CssClass="form-control" runat="server"></asp:TextBox> 
                                        <label for="txtFechaInicioEdit" class="active">Fecha de Inicio (dd/mm/yyyy): </label>
                                    </div>
                                    <div class="text-center input-field form-group">
                                        <asp:TextBox ID="txtFechaFinEdit" CssClass="form-control" runat="server"></asp:TextBox> 
                                        <label for="txtFechaFinEdit" class="active">Fecha de Fin (dd/mm/yyyy): </label>
                                    </div>
                                </div>
                                <div class="col-xs-2"></div>
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
                    <h5>Eliminar Bono</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField runat="server" ID="hdfBonoIDEli" />
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
            <h5<label id="lblMsjTitle"></label></h5>
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