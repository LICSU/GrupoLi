<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgregarReservaAdmin.aspx.cs" Inherits="sistema_AgregarReservaAdmin" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h4>Agregar Reserva</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-6">
                        <input runat="server" id="txtBuscar" class="form-control"  placeholder="dd/mm/yyyy" />
                    </div>
                    <div class="col-lg-2">
                        <asp:Button runat="server" ID="btnBuscar1" Text="Buscar" CssClass="waves-effect waves-light btn" OnClick="btnBuscar_Click1" />
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row form-group">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="true"
                            DataKeyNames="ClaseID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                            onpageindexchanging="GridView1_PageIndexChanging"
                            EmptyDataText="No existen Clases para Reservar.." OnRowCommand="GridView1_RowCommand"
                            PagerStyle-CssClass="pagination" 
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="ClaseID" runat="server" Enabled="false" Text='<%# Eval("ClaseID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clase" >
                                    <ItemTemplate>
                                        <asp:Label ID="ClaseDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fecha" >
                                    <ItemTemplate>
                                        <asp:Label ID="ClasePlantillaFecha" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaFecha") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hora" >
                                    <ItemTemplate>
                                        <asp:Label ID="ClasePlantillaHora" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaHora") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cupo" >
                                    <ItemTemplate>
                                        <asp:Label ID="ClasePlantillaCupo" runat="server" Enabled="false" Text='<%# Eval("ClasePlantillaCupo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unidad" >
                                    <ItemTemplate>
                                        <asp:Label ID="ClienteNombre" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                                        <asp:Label ID="ClienteID" runat="server" Enabled="false" Visible="false" Text='<%# Eval("ClienteID") %>' />
                                        <asp:Label ID="ClasePlantillaID" runat="server" Enabled="false" Visible="false" Text='<%# Eval("ClasePlantillaID") %>' />
                                        <asp:Label ID="ClaseTipo" runat="server" Visible="false" Enabled="false" Text='<%# Eval("ClaseTipo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profesor" >
                                    <ItemTemplate>
                                        <asp:Label ID="ProfesorNombre" runat="server" Enabled="false" Text='<%# Eval("ProfesorNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField CommandName="reservar" ButtonType="Button" Text='Reservar' HeaderText="Reservar">
                                    <ControlStyle CssClass="waves-effect waves-light btn"></ControlStyle>
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
                    <h5>Agregar Reserva</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <asp:HiddenField ID="hdfClaseID" runat="server" />
                            <asp:HiddenField ID="hdfClienteID" runat="server" />
                            <asp:HiddenField ID="hdfClaseTipo" runat="server" />
                            <asp:HiddenField ID="hdfClasePlantillaID" runat="server" />

                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtClaseNombreAdd" runat="server" Enabled="false"></asp:TextBox>
                                        <label class="active" for="txtClaseNombreAdd">Clase: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtUnidadAdd" runat="server" Enabled="false"></asp:TextBox> 
                                        <label class="active" for="txtUnidadAdd">Unidad: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtFechaAdd" runat="server" Enabled="false"></asp:TextBox> 
                                        <label class="active" for="txtFechaAdd">Fecha: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtHoraAdd" runat="server" Enabled="false"></asp:TextBox> 
                                        <label class="active" for="txtHoraAdd">Hora: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:TextBox ID="txtProfesorAdd" runat="server" Enabled="false"></asp:TextBox> 
                                        <label class="active" for="txtProfesorAdd">Profesor: </label>
                                    </div>
                                    <div class="form-group input-field">
                                        <asp:HiddenField ID="txtUsuarioIDAdd" runat="server" />
                                        <asp:TextBox ID="txtUsuarioNombreAdd" runat="server" ></asp:TextBox> 
                                        <label class="active" for="txtUsuarioNombreAdd">Alumno: </label>
                                        <asp:Button ID="btnBuscar" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
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
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Add Modal Ends here -->
    <!-- Buscar Modal Starts here -->
    <div id="bscModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="Button2" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Buscar Usuario</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>                                    
                        <asp:GridView ID="GridView2" runat="server" Width="95%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="true"
                            CssClass="table table-hover table-striped" PageSize="20"
                            OnRowCommand="GridView2_RowCommand" 
                            EmptyDataText="No existen Usuarios..."
                            onpageindexchanging="GridView2_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Alumno" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioNombreB" runat="server" Enabled="false" Text='<%# Eval("Nombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                                            
                                <asp:TemplateField HeaderText="Cedula" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioCedulaB" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioIDB" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField CommandName="selectRecord" ButtonType="Button" Text="Seleccionar" HeaderText="Seleccionar">
                                    <ControlStyle CssClass="waves-effect waves-light btn"></ControlStyle>
                                </asp:ButtonField>
                                                                   
                            </Columns>
                        </asp:GridView>
                        <div class="modal-footer">
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- BUscar Modal Ends here -->
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