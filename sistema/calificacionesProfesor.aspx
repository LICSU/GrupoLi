<%@ Page Language="C#" AutoEventWireup="true" CodeFile="calificacionesProfesor.aspx.cs" Inherits="sistema_calificacionesProfesor" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Calificaciones por profesor</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row text-center">
                    <div class="col s2"></div>
                    <div class="col s8">
                        <div class="form-group input-field">
                            <asp:DropDownList ID="dplProfesor" runat="server" CssClass="browser-default" AutoPostBack="true" OnSelectedIndexChanged="ddlProfesor_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="form-group input-field">
                            <asp:DropDownList ID="ddlMeses" runat="server" CssClass="browser-default" AutoPostBack="true" OnSelectedIndexChanged="ddlMeses_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col s2 text-right">
                        <asp:ImageButton ID="btnDescargar" runat="server" ImageUrl="~/Images/descargar.png"   OnClick="btnDescargar_Click"/>   
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="GridView1" runat="server" Width="95%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" AllowPaging="true"
                        CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existen calificaciones de ese profesor."
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:BoundField DataField="Clase" HeaderText="Clase" HeaderStyle-CssClass="text-center" />                                                                                        
                            <asp:BoundField DataField="Alumnos" HeaderText="Alumnos que Reservaron" HeaderStyle-CssClass="text-center" />                                                                                        
                            <asp:BoundField DataField="Promedios" HeaderText="Calificación Promedio (1 al 5)" HeaderStyle-CssClass="text-center" />                                                                                        
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