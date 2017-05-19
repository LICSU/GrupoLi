<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CorreoEmpresa.aspx.cs" Inherits="sistema_CorreoEmpresa" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Correo Empresa</h4>
        </div>
        <div class="row"><br /></div>        
                <div class="row form-group text-center">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <asp:UpdatePanel ID="upCrudGrid" runat="server" UpdateMode="Always" >
                            <ContentTemplate>
                                <div class="input-field from-group text-center">
                                    <label for="dplEmpresa" class="active">Destinatario</label><br />
                                    <asp:DropDownList  ID="dplEmpresa" runat="server" CssClass="browser-default"></asp:DropDownList>  
                                </div>
                                <div class="input-field from-group text-center">
                                    <asp:TextBox ID="txtAsunto" runat="server"></asp:TextBox>       
                                    <label for="txtAsunto">Asunto</label>
                                </div>
                                <div class="input-field from-group text-center">
                                    <asp:TextBox ID="txtMensaje" runat="server" TextMode="MultiLine" Rows="10" CssClass="materialize-textarea"></asp:TextBox>       
                                    <label for="txtMensaje">Mensaje</label>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="input-field from-group text-center">
                            <asp:FileUpload ID="fipAdjuntos" runat="server"   />         
                        </div>
                        <div class="input-field from-group text-center">
                            <asp:Button ID="Button1" runat="server" Text="Enviar" CssClass="waves-effect waves-light btn" OnClick="Button1_Click" />
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
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