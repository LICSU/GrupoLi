<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Salones.aspx.cs" Inherits="sistema_Salones" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>


<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;"> 
        <div class="row text-center">
            <h4>Gestión de Salones</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col s6 input-field">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <label for="txtSearch">Salón</label>
                    </div>
                    <div class="col s2 input-field">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />                              
                    </div>
                    <div class="col-lg-2 text-right">
                        <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="Agregar_Click" />  
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="SalonID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existen salones registrados."
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>                            
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label Visible="false" ID="SalonID" runat="server" Enabled="false" Text='<%# Eval("SalonID") %>' />
                                    <asp:Label ID="SalonNombre" runat="server" Enabled="false" Text='<%# Eval("SalonNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción">
                                <ItemTemplate>
                                    <asp:Label ID="SalonDescripcion" runat="server" Enabled="false" Text='<%# Eval("SalonDescripcion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>      
                            <asp:TemplateField HeaderText="Frente">
                                <ItemTemplate>
                                    <asp:Label ID="frenteSalon" runat="server" Enabled="false" Text='<%# Eval("frenteSalon") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Forma">
                                <ItemTemplate>
                                    <asp:Label ID="formaSalon" runat="server" Enabled="false" Text='<%# Eval("formaSalon") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:ButtonField CommandName="deleteRecord"
                                ButtonType="Image" ImageUrl="~/Images/eliminar.png" HeaderText="Eli">
                                <ControlStyle ></ControlStyle>
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
                    <h5>Agregar Salón</h5>
                </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-8">
                                <div class="input-field form-group">
                                    <asp:TextBox ID="txtNombreAdd" runat="server"></asp:TextBox>
                                    <label for="txtNombreAdd">Nombre</label>
                                </div>
                                <div class="input-field form-group">
                                    <asp:TextBox ID="txtDescripcionAdd" runat="server"></asp:TextBox>
                                    <label for="txtDescripcionAdd">Descripción</label>
                                </div>
                                <div class="input-field form-group">
                                    <asp:Image ImageAlign="Middle" runat="server" ImageUrl="~/images/imagenEjemplo.jpg" CssClass="img-responsive"/>
                                </div>
                                <div class="input-field form-group">
                                    <asp:DropDownList runat="server" CssClass="browser-default" ID="dplFrente"  >
                                        <asp:ListItem Text="Seleccione el Frente" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Frente A" Value="frenteA"></asp:ListItem>
                                        <asp:ListItem Text="Frente B" Value="frenteB"></asp:ListItem>
                                        <asp:ListItem Text="Frente C" Value="frenteC"></asp:ListItem>
                                        <asp:ListItem Text="Frente D" Value="frenteD"></asp:ListItem>
                                    </asp:DropDownList> 
                                </div>
                                <div class="input-field form-group">
                                    <asp:DropDownList runat="server" CssClass="browser-default" ID="dplFroma" >
                                        <asp:ListItem Text="Seleccione la Forma" Value=""></asp:ListItem>
                                        <asp:ListItem Text="Rectangular" Value="Rectangular"></asp:ListItem>
                                        <asp:ListItem Text="Cuadrado" Value="Cuadrado"></asp:ListItem>
                                    </asp:DropDownList>   
                                </div>
                            </div>
                            <div class="col-lg-2"></div>  
                        </div>
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
                    <h5>Eliminar Salón</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField ID="hSalonNombreeDel" runat="server" />
                            <asp:HiddenField ID="hSalonDel" runat="server" />
                                                    
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