<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cargarVideosAdmin.aspx.cs" Inherits="sistema_cargarVideosAdmin" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Cargar Videos</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="text-right row form-group">
                    <asp:Button runat="server" CssClass="waves-effect waves-light btn" ID="btnSubir" Text="Agregar" OnClick="btnSubir_Click" />
                </div>
                <div class="text-right row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="PlanID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existen Videos Cargados Aún"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            
                            <asp:TemplateField HeaderText="VideoUrl"  >
                                <ItemTemplate>
                                    <asp:Label ID="VideoID" Visible="false" runat="server" Enabled="false" Text='<%# Eval("VideoID") %>' />
                                    <asp:Label  ID="UrlVideo" runat="server" Enabled="false" Text='<%# Eval("UrlVideo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Inicio"  >
                                <ItemTemplate>
                                    <asp:Label  ID="FechaInicioVideo" runat="server" Enabled="false" Text='<%# Eval("FechaInicioVideo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Fin" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label  ID="FechaFinVideo" runat="server" Enabled="false" Text='<%# Eval("FechaFinalVideo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Video" >
                                <ItemTemplate>
                                    <asp:Label  ID="NumeroVideo" runat="server" Enabled="false" Text='<%# Eval("NumeroVideo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:ButtonField CommandName="deleteRecord"
                                ButtonType="Image" ImageUrl="~/Images/eliminar.png" HeaderText="Eli">
                                <ControlStyle></ControlStyle>
                            </asp:ButtonField>                          
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Add Modal Starts here -->
        <div id="addModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h5>Agregar Video</h5>
                    </div>
                    <div class="modal-body row">
                       
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8">
                            <div class="form-group text-center input-field">
                                <asp:TextBox ID="txtUrlVideo" runat="server"></asp:TextBox> 
                                <label for="txtUrlVideo">Url Video</label>   
                            </div>
                            <div class="form-group text-center input-field">
                                <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox> 
                                <label for="txtUrlVideo">Fecha de Inicio</label>   
                            </div>
                            <div class="form-group text-center input-field">
                                <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox> 
                                <label for="txtFechaFin">Fecha de Fin</label>   
                            </div>
                            <div class="form-group text-center input-field">
                                <asp:DropDownList runat="server" ID="dplVideos" CssClass="browser-default">
                                    <asp:ListItem Text="Seleccione una Opción" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Video 1" Value="video1"></asp:ListItem>
                                    <asp:ListItem Text="Video 2" Value="video2"></asp:ListItem>
                                    <asp:ListItem Text="Video 3" Value="video3"></asp:ListItem>
                                    <asp:ListItem Text="Video 4" Value="video4"></asp:ListItem>
                                    <asp:ListItem Text="Video 5" Value="video5"></asp:ListItem>
                                    <asp:ListItem Text="Video 6" Value="video6"></asp:ListItem>
                                    <asp:ListItem Text="Video 7" Value="video7"></asp:ListItem>
                                    <asp:ListItem Text="Video 8" Value="video8"></asp:ListItem>
                                    <asp:ListItem Text="Video 9" Value="video9"></asp:ListItem>
                                    <asp:ListItem Text="Video 10" Value="video10"></asp:ListItem>
                                </asp:DropDownList> 
                            </div>    
                        </div>
                        <div class="col-lg-2"></div>
                                               
                    </div>
                    <div class="modal-footer">
                        <asp:Label ID="Label3" Visible="false" runat="server"></asp:Label>
                        <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="waves-effect waves-light btn"  OnClick="btnAdd_Click"/>
                        <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Add Modal Ends here -->  
    <!-- Delete Record Modal Starts here-->
    <div id="deleteModal"  class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDelete" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Eliminar Archivo</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField ID="hdArchivoID" runat="server" />                                                    
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="waves-effect waves-light btn" OnClick="btnDelete_Click" />
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!--Delete Record Modal Ends here -->

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
