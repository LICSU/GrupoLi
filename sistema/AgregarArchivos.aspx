<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgregarArchivos.aspx.cs" Inherits="sistema_AgregarArchivos" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Agregar Archivos</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="text-right row form-group">
                    <asp:Button ID="Agregar" runat="server" Text="Agregar"  CssClass="waves-effect waves-light btn"  OnClick="Agregar_Click"/> 
                </div>
                <div class="text-right row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="ArchivoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existen archivos cargados."
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="ArchivoID" runat="server" Enabled="false" Text='<%# Eval("ArchivoID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="Nombre" runat="server" Enabled="false" Text='<%# Eval("ArchivoNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Documento">
                                <ItemTemplate>
                                    <asp:Label ID="TipoDocumento" runat="server" Enabled="false" Text='<%# Eval("TipoDocumento") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Fecha de Carga">
                                <ItemTemplate>
                                    <asp:Label ID="FechaArchivo" runat="server" Enabled="false" Text='<%# Eval("FechaRegistro") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField CommandName="deleteRecord"
                                ButtonType="Button" Text="E" HeaderText="Eli">
                                <ControlStyle CssClass="waves-effect waves-light btn"></ControlStyle>
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
                    <h5>Agregar Archivo</h5>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8">
                            <div class="text-center form-group input-field">
                                <asp:TextBox ID="txtNombreAdd" runat="server"></asp:TextBox>  
                                <label for="txtNombreAdd">Nombre</label>
                            </div>
                            <div class="text-center form-group input-field">
                                <asp:DropDownList CssClass="browser-default" ID="dplTipo" runat="server" ClientIDMode="Static" >
                                    <asp:ListItem Text="Tipo de Documento" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Normas y Recomendaciones" Value="Normas"></asp:ListItem>
                                    <asp:ListItem Text="Horario Mensual" Value="Horario"></asp:ListItem>
                                    <asp:ListItem Text="F. Autorización de Descuento Nómina" Value="Nomina"></asp:ListItem>
                                    <asp:ListItem Text="Descripción de Clases" Value="Descripción"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="text-center form-group input-field">
                                <asp:FileUpload ID="flpArchivo" runat="server" /> 
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
            </div>
        </div>
    </div>
    <!-- Add Modal Ends here -->                            
    <!-- Delete Record Modal Starts here-->
    <div id="deleteModal"  class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDelete" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Eliminar Archivo</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField ID="hdArchivoID" runat="server" />
                                                    
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