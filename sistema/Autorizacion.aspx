<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Autorizacion.aspx.cs" Inherits="sistema_Autorizacion" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Usuarios Bloqueados</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
            <div class="row form-group">
                <div class="col-lg-3"></div>
               <div class="col-lg-6">
                   <div class="row text-center input-field">
                       <div class="col-lg-6">
                           <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                           <label for="txtSearch">Nombre o Cédula</label>
                       </div>
                       <div class="col-lg-6">
                           <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />  
                       </div>                       
                   </div>
               </div>
               <div class="col-lg-3"></div>
            </div>               
            <div class="row form-group">
                <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="AutorizacionID" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging"
                    CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline"
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <Columns>                        
                        <asp:TemplateField HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                <asp:Label ID="AutorizacionID" runat="server" Visible="false"  Enabled="false" Text='<%# Eval("AutorizacionID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cédula">
                            <ItemTemplate>
                                <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bloqueado">
                            <ItemTemplate>
                                <asp:CheckBox ID="AutorizacionActivo" Visible="false" runat="server" Enabled="false" Checked='<%# Eval("AutorizacionActivo") %>' />
                                <asp:Label ID="AutorizacionActivoV" runat="server" Enabled="false" Text='<%# Eval("AutorizacionActivoV") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:ButtonField CommandName="editRecord"
                            ButtonType="Image" ImageUrl="~/Images/editar.png" HeaderText="Mod">
                            <ControlStyle ></ControlStyle>
                        </asp:ButtonField>                              
                    </Columns>
                </asp:GridView>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Edit Modal Starts here -->
    <div id="editModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Modificar Usuario</h5>                                            
                </div>
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="hAutorizacionMod" runat="server" /> 
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="row form-group text-center input-field">
                                        <asp:TextBox Enabled="false" ID="txtNombreEdit" runat="server"></asp:TextBox>
                                        <label for="txtNombreEdit" class="active">Nombre</label>
                                    </div>
                                    <div class="row form-group text-center input-field">
                                        <asp:TextBox Enabled="false" ID="txtCedulaEdit" runat="server"></asp:TextBox>
                                        <label for="txtCedulaEdit" class="active">Cédula</label>
                                    </div>
                                    <div class="row form-group text-left input-field">
                                        <asp:CheckBox ID="chkBloquearEdit" runat="server" ClientIDMode="Static"></asp:CheckBox>  
                                        <label for="chkBloquearEdit">Activar</label>
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Modificar" CssClass="waves-effect waves-light btn" OnClick="btnSave_Click" />
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Edit Modal Ends here -->
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