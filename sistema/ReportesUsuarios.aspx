<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportesUsuarios.aspx.cs" Inherits="sistema_ReportesUsuarios" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Reporte de Usuarios</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col s6 input-field">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <label for="txtSearch" class="active">Buscar por Cedula</label>
                    </div>
                    <div class="col s2 input-field">
                        <asp:Button ID="Buscar" runat="server" Text="Filtrar"  CssClass="waves-effect waves-light btn"  OnClick="Buscar_Click"/>                                                
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col s6">
                        <div class="col s6 input-field">
                            <asp:TextBox ID="txtFechaRegistroDesde" runat="server" CssClass="form-control" placeholder="dd/mm/yyyy"></asp:TextBox>
                            <label for="txtFechaRegistroDesde" class="active">Fecha desde (dd/mm/yyyy)</label>
                        </div> 
                        <div class="col s6 input-field">
                            <asp:TextBox ID="txtFechaRegistroHasta" runat="server" CssClass="form-control" placeholder="dd/mm/yyyy"></asp:TextBox>
                            <label for="txtFechaRegistroHasta" class="active">Fecha hasta (dd/mm/yyyy)</label>
                        </div> 
                    </div>                    
                    <div class="col s2 input-field">
                        <asp:Button ID="btnFiltroFecha" runat="server" Text="Filtrar"  CssClass="btn btn-purple"  OnClick="btnFiltroFecha_Click"/> 
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col s6 input-field">
                        <asp:DropDownList ID="dplUnidad" runat="server" CssClass="browser-default"></asp:DropDownList>
                    </div>
                    <div class="col s2 input-field">
                        <asp:Button ID="btnUnidad" runat="server" Text="Filtrar"  CssClass="waves-effect waves-light btn"  OnClick="btnUnidad_Click"/> 
                    </div>
                    <div class="col-lg-2 text-right input-field">
                        <asp:ImageButton ID="btnDescargar" runat="server" ImageUrl="~/Images/descargar.png"   OnClick="btnDescargar_Click"/>  
                    </div>
                </div>
                <div class="row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="ArchivoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existen usuarios activos."
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="Nombre" runat="server" Enabled="false" Text='<%# Eval("Nombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cédula">
                                <ItemTemplate>
                                    <asp:Label ID="Cedula" runat="server" Enabled="false" Text='<%# Eval("Cedula") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clave">
                                <ItemTemplate>
                                    <asp:Label ID="Clave" runat="server" Enabled="false" Text='<%# Eval("Clave") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Correo">
                                <ItemTemplate>
                                    <asp:Label ID="Correo" runat="server" Enabled="false" Text='<%# Eval("Correo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Teléfono">
                                <ItemTemplate>
                                    <asp:Label ID="Telefono" runat="server" Enabled="false" Text='<%# Eval("Telefono") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>      
                            <asp:TemplateField HeaderText="Celular">
                                <ItemTemplate>
                                    <asp:Label ID="Celular" runat="server" Enabled="false" Text='<%# Eval("Celular") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Unidad">
                                <ItemTemplate>
                                    <asp:Label ID="Unidad" runat="server" Enabled="false" Text='<%# Eval("Unidad") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Plan">
                                <ItemTemplate>
                                    <asp:Label ID="Plan" runat="server" Enabled="false" Text='<%# Eval("PlanNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Fecha Fin">
                                <ItemTemplate>
                                    <asp:Label ID="FechaFin" runat="server" Enabled="false" Text='<%# Eval("FechaFin") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Clases Activas">
                                <ItemTemplate>
                                    <asp:Label ID="ClasesActivas" runat="server" Enabled="false" Text='<%# Eval("ClasesActivas") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Fecha Registro">
                                <ItemTemplate>
                                    <asp:Label ID="FechaRegistro" runat="server" Enabled="false" Text='<%# Eval("FechaRegistro") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                                    
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDescargar" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
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