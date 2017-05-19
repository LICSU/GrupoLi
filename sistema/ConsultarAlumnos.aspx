<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultarAlumnos.aspx.cs" Inherits="sistema_ConsultarAlumnos" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h4>Alumnos Inscritos</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col s2"></div>
                    <div class="col s8">
                        <div class="col s8 input-field">
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                            <label for="txtSearch">Nombre</label>
                        </div>
                        <div class="col s4 text-left">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="col s2"></div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center"
                    OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                    DataKeyNames="PlanID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                    onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No tiene Alumnos Inscritos"
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <asp:TemplateField HeaderText="Alumno(a)" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label Visible="false" ID="UsuarioID" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />
                                <asp:Label  ID="PlanNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Celular">
                            <ItemTemplate>
                                <asp:Label  ID="UsuarioCelular" runat="server" Enabled="false" Text='<%# Eval("UsuarioCelular") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="Antecedente">
                            <ItemTemplate>
                                <asp:Label  ID="UsuarioObservacion" runat="server" Enabled="false" Text='<%# Eval("UsuarioObservacion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha de Ingreso">
                            <ItemTemplate>
                                <asp:Label  ID="UsuarioFechaRegistro" runat="server" Enabled="false" Text='<%# Eval("UsuarioFechaRegistro") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Foto">
                            <ItemTemplate>
                                <asp:ImageButton OnCommand="VerFoto" CommandName="VerFoto" ID="UsuarioFoto" runat="server" CommandArgument='<%#Eval("UsuarioFoto") %>' ImageUrl='<%# Eval("UsuarioFoto") %>' CssClass="perfilpeq"/>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Clase">
                            <ItemTemplate>
                                <asp:Label  ID="ClienteID" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Nivel">
                            <ItemTemplate>
                                <asp:Label  ID="Nivel" runat="server" Enabled="false" Text='<%# Eval("Nivel") %>' />
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

    <!-- Msj Modal -->
    <div class="modal fade" id="modalFoto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog" style="width: 150px !important;">
        <div class="modal-content" >
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
            <h4 class="modal-title"><label id="Label1">Foto</label></h4>
          </div>
          <div class="modal-body text-center">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Image CssClass="perfil" ImageUrl="" AlternateText="Imagen No Existe"  runat="server" ID="imgFoto"/>
                </ContentTemplate>
              </asp:UpdatePanel>
              
          </div><!-- /modal-body -->
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
        function MostrarFotoModal() {
            $('#modalFoto').modal('show');
            return true;
        }
    </script>
</form>