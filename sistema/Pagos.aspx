<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pagos.aspx.cs" Inherits="sistema_Pagos" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Listar Pagos</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-3 input-field">
                        <input title="Fecha Inferior" runat="server"  type='text' id="txtFecNac1"  />
                        <label for="txtFecNac1" class="active">Desde(dd/mm/yyyy)</label>
                    </div>
                    <div class="col-lg-3 input-field">
                        <input title="Fecha Inferior" runat="server"  type='text' id="txtFecNac2"  />
                        <label for="txtFecNac2" class="active">Desde(dd/mm/yyyy)</label>
                    </div>
                    <div class="col-lg-2 input-field">
                        <asp:Button ID="Filtrar" runat="server" Text="Filtrar"  CssClass="waves-effect waves-light btn" OnClick="Filtrar_Click" />
                    </div>
                    <div class="col-lg-2"></div>                    
                </div>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-4 input-field">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <label for="txtSearch" class="active">Nombre ,Cédula, Plan</label>
                    </div>
                    <div class="col-lg-4 input-field">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />       
                    </div>
                    <div class="col-lg-2 text-right">
                        <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="btnArchivo_Click" />
                    </div>
                    
                </div>
                <div class="row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="95%" HorizontalAlign="Center"
                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" 
                    DataKeyNames="ClasePagoID" CssClass="footable" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging"
                    EmptyDataText="No existen pagos registrados..."
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                    <Columns>
                        <asp:TemplateField HeaderText="Cédula">
                            <ItemTemplate>
                                <asp:Label Visible="false" ID="ClasePagoID" runat="server" Enabled="false" Text='<%# Eval("ClasePagoID") %>' />
                                <asp:Label Visible="false" ID="UsuarioID" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />
                                <asp:Label OnClick="UsuarioCedula_Click" ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Apellido">
                            <ItemTemplate>
                                <asp:Label ID="UsuarioApellido" runat="server" Enabled="false" Text='<%# Eval("UsuarioApellido") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plan">
                            <ItemTemplate>
                                <asp:Label Visible="false" ID="PlanAlumnoID" runat="server" Enabled="false" Text='<%# Eval("PlanAlumnoID") %>' />
                                <asp:Label ID="PlanNombre" runat="server" Enabled="false" Text='<%# Eval("PlanNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Factura #">
                            <ItemTemplate>
                                <asp:Label ID="FacturaNumero" runat="server" Enabled="false" Text='<%# Eval("FacturaNumero") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Monto">
                            <ItemTemplate>
                                <asp:Label ID="FacturaMonto" runat="server" Enabled="false" Text='<%# Eval("FacturaMonto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="Fecha Factura">
                            <ItemTemplate>
                                <asp:Label ID="FacturaFecha" runat="server" Enabled="false" Text='<%# Eval("FacturaFecha") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:ButtonField CommandName="verBalance"
                            ButtonType="Image" ImageUrl="~/Images/ver.png" HeaderText="Balance">
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
