<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AsignarPlan.aspx.cs" Inherits="sistema_AsignarPlan" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Asignar Plan</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
        <div class="row">
            <asp:HiddenField runat="server" ID="hdfUsuarioID" />
            <asp:HiddenField runat="server" ID="hdfSucursalID" />
            <asp:HiddenField runat="server" ID="hdfClienteID" />
            <asp:HiddenField runat="server" ID="hdfSaldoPositivo" />
            <asp:HiddenField runat="server" ID="hdfSaldoNegativo" />
            <asp:HiddenField runat="server" ID="hdfPlanCosto" />
            <asp:HiddenField runat="server" ID="hdfClasesActivas" />
            <div class="col-lg-2"></div>
            <div class="col-lg-8">
                <div class="form-group text-center row ">
                    <div class="col s8 input-field">
                        <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                        <label for="txtUsuario" class="active">Buscar</label>
                    </div>
                    <div class="col s4">
                        <asp:Button ID="BuscarUsr" runat="server" Text="Buscar" CssClass="waves-effect waves-light btn"  OnClick="BuscarUsr_Click"/> 
                    </div>
                </div>
                <div class="form-group text-center row input-field">
                    <asp:DropDownList ID="dplPlan" runat="server" ClientIDMode="Static" CssClass="browser-default" OnSelectedIndexChanged="dplPlan_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </div>
                <div class="form-group text-center row input-field">
                    <asp:DropDownList ID="dplAcumulado" runat="server" OnSelectedIndexChanged="dplAcumulado_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" CssClass="browser-default">
                        <asp:ListItem Text="¿Desea Acumular las clases regulares?" Value=""></asp:ListItem>
                        <asp:ListItem Text="Si, por favor." Value="SI"></asp:ListItem>
                        <asp:ListItem Text="No, gracias." Value="NO"></asp:ListItem>
                    </asp:DropDownList>  
                </div>
                <asp:PlaceHolder runat="server" ID="phAcumulado" Visible="false">
                    <div class="form-group text-left row">
                        <label>Fecha de Vencimiento: </label>
                        <asp:Label runat="server" ID="lblFechaVencimiento" Text="Fecha de Vencimiento: "></asp:Label> 
                    </div> 
                    <div class="form-group text-left row">
                        <label>Clases Regulares: </label>
                        <asp:Label runat="server" ID="lblClasesRegulares" Text="Clases Acumuladas: " ></asp:Label> 
                    </div>
                </asp:PlaceHolder>
                <div class="form-group text-center row input-field">
                    <asp:DropDownList ID="ddlAcumuladoC" runat="server" OnSelectedIndexChanged="dplAcumuladoC_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" CssClass="browser-default">
                        <asp:ListItem Text="¿Desea Acumular las clases complementarias?" Value=""></asp:ListItem>
                        <asp:ListItem Text="Si, por favor." Value="SI"></asp:ListItem>
                        <asp:ListItem Text="No, gracias." Value="NO"></asp:ListItem>
                    </asp:DropDownList>  
                </div>
                <asp:PlaceHolder runat="server" ID="phAcumuladoC" Visible="false">
                    <div class="form-group text-left row">
                        <label>Fecha de Vencimiento: </label>
                        <asp:Label runat="server" ID="lblFechaVencimiento2" Text="Fecha de Vencimiento: "></asp:Label> 
                    </div> 
                    <div class="form-group text-left row">
                        <label>Clases Complementarias: </label>
                        <asp:Label runat="server" ID="lblClasesComplementarias"  Text="Clases Acumuladas: " ></asp:Label>  
                    </div>
                </asp:PlaceHolder>
                <div class="form-group text-center row input-field">
                    <asp:TextBox ID="txtFacturaNumeroAsignar" runat="server"></asp:TextBox>  
                    <label for="txtFacturaNumeroAsignar">Factura Número</label> 
                </div>
                <div class="form-group text-center row input-field">
                    <asp:TextBox ID="txtFacturaNotaAdd" runat="server"></asp:TextBox>  
                    <label for="txtFacturaNotaAdd">Factura Nota</label> 
                </div>
                <div class="form-group text-center row input-field">
                    <asp:HiddenField ID="hdfFechaINI" runat="server" />
                    <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>  
                    <label for="txtFechaInicio" class="active">Fecha Inicio del Plan</label> 
                </div>
                <div class="form-group text-center row input-field">
                    <asp:HiddenField ID="hdfFechaFIN" runat="server" />
                    <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>  
                    <label for="txtFechaFin" class="active">Fecha Fin del Plan</label> 
                </div>
                <div class="form-group text-center row input-field">
                    <asp:TextBox ID="txtFechaVenMatricula" runat="server" Enabled="false"></asp:TextBox>  
                    <label for="txtFechaVenMatricula" class="active">Fecha de Vencimiento de Matricula</label> 
                </div>
                <div class="form-group text-center row input-field">
                    <asp:DropDownList ID="ddlMatricula" runat="server" OnSelectedIndexChanged="ddlMatricula_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" CssClass="browser-default">
                        <asp:ListItem Text="¿Desea Pagar la Matricula?" Value=""></asp:ListItem>
                        <asp:ListItem Text="Si, por favor." Value="SI"></asp:ListItem>
                        <asp:ListItem Text="No, gracias." Value="NO"></asp:ListItem>
                    </asp:DropDownList>  
                </div>
                <asp:PlaceHolder runat="server" ID="phMatricula" Visible="false">                     
                    <div class="form-group text-left row input-field">
                        <label for="txtFechaIniMatricula" class="active">Fecha Inicial(dd/mm/yyyy): </label>
                        <asp:TextBox runat="server" ID="txtFechaIniMatricula"></asp:TextBox> 
                    </div> 
                    <div class="form-group text-left row input-field">
                        <label for="txtFechaFinMatricula" class="active">Fecha Final(dd/mm/yyyy): </label>
                        <asp:TextBox runat="server" ID="txtFechaFinMatricula"></asp:TextBox> 
                    </div>
                    <div class="form-group text-left row input-field">
                        <label for="txtPagoMatricula" class="active">Pago: </label>
                        <asp:TextBox runat="server" ID="txtPagoMatricula" ></asp:TextBox> 
                    </div>
                </asp:PlaceHolder>
                <div class="form-group text-center">
                    <asp:Button ID="btnAdd" runat="server" Text="Asignar Plan" CssClass="waves-effect waves-light btn"  OnClick="btnAdd_Click"/>
                </div>
                <div class="form-group text-center"></div>
            </div>
            <div class="col-lg-2"></div>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
                            CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                            OnRowCommand="GridView2_RowCommand" 
                            EmptyDataText="No existen Usuarios..."
                            onpageindexchanging="GridView2_PageIndexChanging"
                            PagerStyle-CssClass="pagination" 
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                                <asp:TemplateField HeaderText="Nombre" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioNombreB" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cedula" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioCedulaB" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Empresa" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="ClienteNombreB" runat="server" Enabled="false" Text='<%# Eval("ClienteNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioIDB" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />
                                        <asp:Label ID="SucursalIDB" runat="server" Enabled="false" Text='<%# Eval("SucursalID") %>' />
                                        <asp:Label ID="ClienteIDB" runat="server" Enabled="false" Text='<%# Eval("ClienteID") %>' />
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
