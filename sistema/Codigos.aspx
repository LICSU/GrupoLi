<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Codigos.aspx.cs" Inherits="sistema_Codigos" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Códigos</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row text-center form-group">
                    <div class="col-lg-2"></div>
                    <div class="col s6 input-field">
                        <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                        <label for="txtSearch">Buscar</label>
                    </div>
                    <div class="col s4 input-field">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar"   CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row text-center form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8 input-field">
                        <asp:DropDownList ID="dpllistaCodigos" CssClass="browser-default" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dpllistaCodigos_SelectedIndexChanged">
                            <asp:ListItem Value="" Text="Seleccione una Opción"></asp:ListItem>
                            <asp:ListItem Value="Clases" Text="Clases"></asp:ListItem>
                            <asp:ListItem Value="Profesores" Text="Profesores"></asp:ListItem>
                            <asp:ListItem Value="Empresas" Text="Empresas"></asp:ListItem>
                            <asp:ListItem Value="Salones" Text="Salones"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row text-center form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="NivelID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="Código">
                                <ItemTemplate>
                                    <asp:Label ID="Codigo" runat="server" Enabled="false" Text='<%# Eval("Codigo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="Nombre" runat="server" Enabled="false" Text='<%# Eval("Nombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                                           
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     <uc3:ucFooter runat="server" ID="ucFooter" />
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