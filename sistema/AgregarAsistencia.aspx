<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgregarAsistencia.aspx.cs" Inherits="sistema_AgregarAsistencia" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h4>Asistencias</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="table-responsive" style="width:96%; margin-left:2%;">
                        <asp:Table CssClass="table" ID="tabUsuarios" runat="server">
                            <asp:TableHeaderRow CssClass="table-bordered">
                                <asp:TableCell>Nombre</asp:TableCell>
                                <asp:TableCell>Apellido</asp:TableCell>
                                <asp:TableCell>Cédula</asp:TableCell>
                                <asp:TableCell>Asistió</asp:TableCell>
                            </asp:TableHeaderRow>                                       
                        </asp:Table>       
                    </div>
                    <asp:Button ID="Button1" runat="server" Text="Agregar" CssClass="btn btn-purple"  OnClick="btnAdd_Click"/>
                </div>
                <div class="row form-group">
                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   <!-- Modal PleaseWait-->
    <div class="modal" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false">
      <div class="modal-dialog modal-sm">
        <div class="modal-content">
            <div class="modal-body">
                <div class="clearfix">
                    <img src="../Images/cargando.gif" class="img-responsive center-block" />
                </div>
            </div>
        </div> 
      </div>
    </div>
    <!-- Modal PleaseWait -->
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
    <div class="modal fade" id="modalVolver" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
            <h4 class="modal-title"><label id="Label1"></label></h4>
          </div>
          <div class="modal-body">
                <div class="row">
                    <div class="col-md-1">
                        <span id="Span1" class="fa fa-check fa-2x text-success"></span>
                    </div>
                    <div class="col-md-11">
                        <label id="Label2">Registros Agregados con Éxito</label>
                    </div>
                </div>
                <div class="clearfix"></div>      </div><!-- /modal-body -->
          <div class="modal-footer">
                <asp:Button ID="btnVolver" runat="server" Text="Aceptar" OnClick="btnVolver_Click" class="btn btn-default" ></asp:Button>
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
    <script type="text/javascript">
        function MostrarVolver(message, title, ccsclas) {;
            $('#modalVolver').modal('show');
            return true;
        }
    </script>
</form>