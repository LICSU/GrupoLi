<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListarPlanesUsuarios.aspx.cs" Inherits="sistema_ListarPlanesUsuarios" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Usuarios - Planes</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">  
                        <div class="text-center row form-group">
                            <asp:DropDownList ID="dplEmpresas" runat="server" CssClass="browser-default" AutoPostBack="true" OnSelectedIndexChanged="dplEmpresas_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="text-center row form-group">
                            <div class="col-lg-6 input-field">
                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                <label for="txtSearch">Buscar</label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="btnArchivo" runat="server" Text="Generar Archivo"   CssClass="waves-effect waves-light btn" OnClick="btnArchivo_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row text-center">
                    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center"
                    AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="UsuarioID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging" AllowSorting="true"
                    onrowcommand="GridView1_RowCommand"
                    EmptyDataText="No Existen Usuarios."
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <Columns> 
                        <asp:TemplateField HeaderText="Nombres"  >
                            <ItemTemplate>
                                <asp:Label Visible="false" ID="UsuarioID" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' /> 
                                <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>      
                        <asp:TemplateField HeaderText="Cédula"  >
                            <ItemTemplate>
                                <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plan"  >
                            <ItemTemplate>
                                <asp:Label ID="PlanNombre" runat="server" Enabled="false" Text='<%# Eval("PlanNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Clases"  >
                            <ItemTemplate>
                                <asp:Label ID="PlanClases" runat="server" Enabled="false" Text='<%# Eval("TotalR") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Clases"  >
                            <ItemTemplate>
                                <asp:Label ID="PlanClasesC" runat="server" Enabled="false" Text='<%# Eval("TotalC") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activas" >
                            <ItemTemplate>
                                <asp:Label ID="PlanCantidadClases" runat="server" Enabled="false" Text='<%# Eval("PlanCantidadClases") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vence" >
                            <ItemTemplate>
                                <asp:Label ID="PlanAlumnoFechaFin" runat="server" Enabled="false" Text='<%# Eval("PlanAlumnoFechaFin") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Empresa">
                            <ItemTemplate>
                                <asp:Label ID="ClienteNombre" runat="server" Enabled="false"  Text='<%# Eval("ClienteNombre") %>'/>
                                <asp:Label Visible="false" ID="ClienteID" runat="server" Enabled="false"  Text='<%# Eval("ClienteID") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>                      
                    </Columns>
                </asp:GridView>
                </div>
            </ContentTemplate>
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