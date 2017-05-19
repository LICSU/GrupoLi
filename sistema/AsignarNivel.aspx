<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AsignarNivel.aspx.cs" Inherits="sistema_AsignarNivel" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:150px;">  
        <div class="row text-center">
            <h4>Asignar Nivel</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8">
                        <div class="col-lg-10">Da Clic en la imagen de la derecha para seleccionar la clase asignada.</div>
                        <div class="col-lg-2">
                            <asp:ImageButton ID="ImgClases" runat="server" ImageUrl="~/Images/agregar.png" OnClick="btnMostrarClases_Click" />
                        </div>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row form-group">
                    <div class="col s3"></div>
                    <div class="col s4 input-field text-center">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <label for="txtSearch">Cédula o Nombre</label>
                    </div>
                    <div class="col s2 text-center input-field">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />
                    </div>
                </div>
                <div class="row form-group text-center">
                    <asp:Label runat="server" ID="lblClaseSeleccionada" Text="No ha seleccionado ninguna clase."></asp:Label>
                </div>
                <div class="row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                            OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                            DataKeyNames="AluNivClaseID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                            onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existe ninguna Asignación"
                            PagerStyle-CssClass="pagination" 
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <Columns>                                
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioID" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre" HeaderStyle-CssClass="text-center" >
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("Nombres") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cédula" HeaderStyle-CssClass="text-center" >
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Nivel" HeaderStyle-CssClass="text-center" >
                                    <ItemTemplate>
                                        <asp:Label ID="Nivel" runat="server" Enabled="false" Text='<%# Eval("Nivel") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Ultima Actualización" HeaderStyle-CssClass="text-center" >
                                    <ItemTemplate>
                                        <asp:Label ID="FechaRegistro" runat="server" Enabled="false" Text='<%# Eval("FechaRegistro") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:ButtonField CommandName="ModificarNivel" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" 
                                    ButtonType="Button" Text="Modificar" HeaderText="Modificar">
                                    <ControlStyle CssClass="btn btn-purple"></ControlStyle>
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
                    <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5 id="editModalLabel">Modificar Asignación</h5>                                            
                </div>
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hdfUsuarioID" />
                                <div class="col s2"></div>
                                <div class="col s8">
                                    <div class="form-group row input-field">
                                        <asp:TextBox ID="txtAlumnoMod" runat="server" Enabled="false"></asp:TextBox> 
                                        <label for="txtAlumnoMod" class="active">Alumno</label>
                                    </div>
                                    <div class="form-group row input-field">
                                        <asp:TextBox ID="txtClaseMod" runat="server" Enabled="false"></asp:TextBox> 
                                        <label for="txtClaseMod" class="active">Clase</label>
                                    </div>
                                    <div class="form-group row input-field">
                                        <asp:DropDownList CssClass="browser-default" ID="dplNivelMod" runat="server" ClientIDMode="Static" ></asp:DropDownList>  
                                    </div>
                                </div>
                                <div class="col s2"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Modificar" CssClass="waves-effect waves-light btn" OnClick="btnSave_Click" />
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Edit Modal Ends here -->

    <!-- Modal de Clases -->
    <div id="modalClases" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <button id="closeModalClases" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5 id="H1">Clases Asignadas</h5>                                            
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="row form-group">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-10">
                                A continuación seleccione la clase asignada para listar los alumnos inscritos en ella.
                            </div>
                            <div class="col-lg-1"></div>                            
                        </div>
                        <asp:GridView ID="GridView3" runat="server" Width="95%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="true"
                            CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="10"
                            OnRowCommand="GridView3_RowCommand" 
                            EmptyDataText="No tiene Clases Asignadas"
                            DataKeyNames="ClaseID"
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            onpageindexchanging="GridView3_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Clase" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="ClaseID" runat="server" Enabled="false" Text='<%# Eval("ClaseID") %>' Visible="false" />
                                        <asp:Label ID="ClaseNombre" runat="server" Enabled="false" Text='<%# Eval("ClaseNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField CommandName="seleccionarClase" ButtonType="Button" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" Text="Seleccionar" HeaderText="Seleccionar">
                                    <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                                </asp:ButtonField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Fin del modal de Clases -->

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
                <button type="button" class="waves-effect waves-light btn" data-dismiss="modal">Cerrar</button>
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