<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoAsistieron.aspx.cs" Inherits="sistema_NoAsistieron" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Usuarios No Asistieron</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row text-center form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <asp:PlaceHolder runat="server" ID="phClientes">
                            <asp:DropDownList runat="server" ID="dplClientes" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="dplClientes_SelectedIndexChanged"></asp:DropDownList>
                        </asp:PlaceHolder>
                    </div>
                    <div class="col-lg-2 text-right">
                        <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="ImgbtnArchivo_Click" />
                    </div>                
                </div>
                <div class="row text-center form-group">
                    <div class="col-lg-2"></div>
                    <div class="input-field col s3">
                        <asp:TextBox runat="server"  ID="txtFecNac1" ></asp:TextBox>
                        <label for="txtFecNac1">Desde</label>
                    </div>

                    <div class="input-field col s3">
                        <asp:TextBox runat="server"  ID="txtFecNac2"></asp:TextBox>
                        <label for="txtFecNac2">Hasta</label>
                    </div>

                    <div class="col-sm-2">
                        <asp:Button ID="Filtrar" runat="server" Text="Filtrar"  CssClass="btn waves-effect waves-light" OnClick="Filtrar_Click" />
                    </div>
                    <div class="col-lg-2"></div>
                 </div>
                <div class="row text-center form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                    AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="BonoID" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging"
                    EmptyDataText="No existen Usuarios Sin Asistir"
                    CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline"
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <Columns>  
                        <asp:TemplateField HeaderText="Cédula">
                            <ItemTemplate>
                                <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre(s) y Apellido(s)">
                            <ItemTemplate>
                                <asp:Label ID="UsuarioNombres" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Clase">
                            <ItemTemplate>
                                <asp:Label ID="ClaseDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha">
                            <ItemTemplate>
                                <asp:Label ID="ClaseFecha" runat="server" Enabled="false" Text='<%# Eval("ClaseFecha") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cliente">
                            <ItemTemplate>
                                <asp:Label ID="ClienteNombre" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                                                 
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