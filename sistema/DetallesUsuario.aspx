<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetallesUsuario.aspx.cs" Inherits="sistema_DetallesUsuario" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Balance General</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="col-lg-12 text-left">
                    <div class="col-lg-1 text-left"></div>
                    <div class="col-lg-10 text-left">
                        <asp:Label ID="lblUsuario" runat="server" CssClass="text-left" ></asp:Label>  
                    </div> 
                    <div class="col-lg-1text-left"></div> 
                </div>
                <div class="col-lg-12">
                    <div class="col-lg-1 text-left"></div>
                    <div class="col-lg-10 text-left">
                        <asp:Label ID="lblClasesTotal" runat="server" ></asp:Label>
                    </div>
                    <div class="col-lg-1 text-left"></div>
                </div>
                <div class="col-lg-12">
                    <div class="col-lg-1 text-left"></div>
                    <div class="col-lg-10 text-left">
                        <asp:Label ID="lblClasesActivas" runat="server" ></asp:Label>
                    </div>
                    <div class="col-lg-1 text-left"></div>
                </div>
                <div class="col-lg-12">
                    <div class="col-lg-1 text-left"></div>
                    <div class="col-lg-10 text-left">
                        <asp:Label ID="lblClasesDisponibles" runat="server" ></asp:Label>
                    </div>
                    <div class="col-lg-1 text-left"></div>
                </div>
                <div class="col-lg-12">
                    <div class="col-lg-2 text-left"></div>
                    <div class="col-lg-8 text-left">
                        <br />
                    </div>
                    <div class="col-lg-2 text-left"></div>
                </div>
                <div class="text-center row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="UsuarioID" 
                        CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                        <Columns>                            
                            <asp:TemplateField HeaderText="Deuda">
                                <ItemTemplate>
                                    <asp:Label ID="txtDeuda" runat="server" Enabled="false" Text='<%# Eval("SaldoNegativo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plan" >
                                <ItemTemplate>
                                    <asp:Label ID="txtPlan" runat="server" Enabled="false" Text='<%# Eval("PlanNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Plan Costo" >
                                <ItemTemplate>
                                    <asp:Label ID="txtPlanCosto" runat="server" Enabled="false" Text='<%# Eval("PlanCosto") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Total de Clases" >
                                <ItemTemplate>
                                    <asp:Label ID="txtPlanClases" runat="server" Enabled="false" Text='<%# Eval("PlanCantidadClases") %>' />
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
