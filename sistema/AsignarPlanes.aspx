<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AsignarPlanes.aspx.cs" Inherits="sistema_AsignarPlanes" %>
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
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-8 input-field text-center">
                        <asp:DropDownList ID="dplFiltroAsignacion" runat="server" CssClass="browser-default"
                            OnSelectedIndexChanged="dplFiltroAsignacion_SelectedIndexChanged" AutoPostBack="True" >
                            <asp:ListItem Value="" Text="Seleccione un Filtro"></asp:ListItem>
                            <asp:ListItem Value="sinCancelar" Text="Planes Sin Cancelar"></asp:ListItem>
                            <asp:ListItem Value="cancelados" Text="Planes Cancelados"></asp:ListItem>
                            <asp:ListItem Value="abonados" Text="Planes Abonados"></asp:ListItem>
                            <asp:ListItem Value="sinAbonar" Text="Planes Sin Abonar"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-6 input-field text-center">
                        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                        <label for="txtSearch">Nombre o Cédula</label>
                    </div>
                    <div class="col-lg-2 input-field text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="btn btn-purple" OnClick="btnSearch_Click" />                                                
                    </div>
                    <div class="col-lg-2 text-right">
                        <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" PostBackUrl="AsignarPlan.aspx" />
                    </div>
                </div>
                <div class="form-group row text-center">
                   <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="PlanAlumnoID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        EmptyDataText="No existen asignaciones.."
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>
                            <asp:TemplateField HeaderText="Cédula">
                                <ItemTemplate>
                                    <asp:Label Visible="false" ID="UsuarioID" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />
                                    <asp:Label Visible="false" ID="PlanAlumnoID" runat="server" Enabled="false" Text='<%# Eval("PlanAlumnoID") %>' />
                                    <asp:Label Visible="false" ID="PlanAlumnoFechaInicio" runat="server" Enabled="false" Text='<%# Eval("FechaInicio") %>' />
                                    <asp:Label Visible="false" ID="PlanCosto" runat="server" Enabled="false" Text='<%# Eval("PlanCosto") %>' />
                                    <asp:Label Visible="false" ID="PlanAlumnoFechaFin" runat="server" Enabled="false" Text='<%# Eval("FechaFin") %>' />
                                    <asp:Label Visible="false" ID="FacturaNota" runat="server" Enabled="false" Text='<%# Eval("FacturaNota") %>' />
                                    <asp:Label ID="UsuarioCedula" runat="server" Enabled="false" Text='<%# Eval("UsuarioCedula") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre(s)">
                                <ItemTemplate>
                                    <asp:Label ID="UsuarioNombre" runat="server" Enabled="false" Text='<%# Eval("UsuarioNombre") %>' />
                                    <asp:Label Visible="false" ID="UsuarioApellido" runat="server" Enabled="false" Text='<%# Eval("UsuarioApellido") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Plan">
                                <ItemTemplate>
                                    <asp:Label ID="PlanNombre" runat="server" Enabled="false" Text='<%# Eval("PlanNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Factura">
                                <ItemTemplate>
                                    <asp:Label ID="NumeroFactura" runat="server" Enabled="false" Text='<%# Eval("NumeroFactura") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>                                                                            
                            <asp:TemplateField HeaderText="Total Pagado">
                                <ItemTemplate>
                                    <asp:Label ID="SaldoPositivo" runat="server" Enabled="false" Text='<%# Eval("SaldoPositivo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Sin Cancelar">
                                <ItemTemplate>
                                    <asp:Label ID="SaldoNegativo" runat="server" Enabled="false" Text='<%# Eval("SaldoNegativo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Finaliza el:">
                                <ItemTemplate>
                                    <asp:Label Visible="false" ID="ClasesActivas" runat="server" Enabled="false" Text='<%# Eval("ClasesActivas") %>' />
                                    <asp:Label ID="PlanAlumnoFechaFinal" runat="server" Enabled="false" Text='<%# Eval("FechaFinal") %>' />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:ButtonField CommandName="verBalance"
                                ButtonType="Image" ImageUrl="~/Images/ver.png" HeaderText="Bal">
                                <ControlStyle ></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="viewRecord"
                                ButtonType="Image" ImageUrl="~/Images/ver.png" HeaderText="Ver">
                                <ControlStyle ></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="editRecord"
                                ButtonType="Image" ImageUrl="~/Images/editar.png" HeaderText="Mod">
                                <ControlStyle ></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="deleteRecord"
                                ButtonType="Image" ImageUrl="~/Images/eliminar.png" HeaderText="Eli">
                                <ControlStyle ></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="agregarPago"
                                ButtonType="Button" Text="Plan" HeaderText="Pagar">
                                <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView> 
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Realizar Pago Matricula -->
    <div id="addPagoMatricula" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeMat" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Realizar Pago Matricula</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hdfAlumnoID" />
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtAlumnoMatricula" Enabled="false" runat="server"></asp:TextBox>
                                        <label for="txtAlumnoMatricula" class="active">Alumno(a)</label>
                                    </div>
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtPagoMatricula" runat="server"></asp:TextBox>
                                        <label for="txtPagoMatricula" class="active">Pago</label>
                                    </div>
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtFechaInicioMatricula" runat="server" ></asp:TextBox>
                                         <label for="txtFechaInicioMatricula" class="active">Fecha Inicial dd/mm/yyyy</label>
                                    </div>
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtFechaFinalMatricula" runat="server" ></asp:TextBox>
                                         <label for="txtFechaFinalMatricula" class="active">Fecha Final dd/mm/yyyy</label>
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>                                                
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label4" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnPagarMatricula" runat="server" Text="Guardar" CssClass="waves-effect waves-light btn"  OnClick="PagarMatricula_Click"/>
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Realizar Pago Ends here -->
    <!-- Realizar Pago Starts here -->
    <div id="addPago" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeAddPago" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Realizar Pago</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hdfDeuda" />
                                <asp:HiddenField runat="server" ID="hdfPositivo" />
                                <asp:HiddenField runat="server" ID="hdfPlanAlumnoID" />
                                <asp:HiddenField runat="server" ID="hdfUsuarioIDPago" />
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtPlanAlumnoAdd" Enabled="false" runat="server"></asp:TextBox>
                                        <label for="txtPlanAlumnoAdd" class="active">Alumno(a)</label>
                                    </div>
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox Enabled="false" ID="txtFacturaNumeroAdd" runat="server"></asp:TextBox>
                                        <label for="txtFacturaNumeroAdd" class="active">Factura Nro</label>
                                    </div>
                                     <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtPlanAdd" Enabled="false" runat="server"></asp:TextBox>
                                        <label for="txtPlanAdd" class="active">Plan</label>
                                    </div>
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtFechaFacturaAdd2" runat="server" ></asp:TextBox>
                                         <label for="txtFechaFacturaAdd2">Fecha Factura dd/mm/yyyy</label>
                                    </div>
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtDeudaAdd" runat="server" Enabled="false" ></asp:TextBox>
                                         <label for="txtDeudaAdd" class="active">Deuda</label>
                                    </div>
                                    <div class="row text-center form-group input-field">
                                        <asp:TextBox ID="txtMontoAdd" runat="server" ></asp:TextBox>
                                         <label for="txtMontoAdd">Monto</label>
                                    </div>
                                    <div class="form-group row input-field">
                                       <asp:DropDownList ID="ddlDescuento" runat="server" OnSelectedIndexChanged="ddlDescuento_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" CssClass="browser-default">
                                            <asp:ListItem Text="¿Desea otorgar un Descuento?" Value="NO"></asp:ListItem>
                                            <asp:ListItem Text="Si, por porcentaje." Value="PORC"></asp:ListItem>
                                            <asp:ListItem Text="Si, por monto." Value="MONT"></asp:ListItem>
                                        </asp:DropDownList> 
                                    </div>
                                    <asp:PlaceHolder runat="server" ID="phDescuento" Visible="false">
                                        <div class="row text-center form-group input-field">
                                            <asp:TextBox ID="txtDescuento" runat="server"></asp:TextBox>
                                             <label for="txtDescuento">Descuento</label>
                                        </div>
                                    </asp:PlaceHolder>
                                </div>
                                <div class="col-lg-2"></div>                                                
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label2" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="Pagar" runat="server" Text="Guardar" CssClass="waves-effect waves-light btn"  OnClick="Pagar_Click"/>
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Realizar Pago Ends here -->
    <!-- Add Modal Starts here -->
    <div id="addModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Asignar Plan</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
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
                                        <asp:DropDownList ID="dplPlan" runat="server" ClientIDMode="Static" CssClass="browser-default"></asp:DropDownList>
                                    </div>
                                    <div class="form-group text-center row input-field">
                                        <asp:DropDownList ID="dplAcumulado" runat="server" OnSelectedIndexChanged="dplAcumulado_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true" CssClass="browser-default">
                                            <asp:ListItem Text="¿Desea Acumular las clases?" Value=""></asp:ListItem>
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
                                         <div class="form-group text-left row">
                                             <label>Clases Complementarias: </label>
                                             <asp:Label runat="server" ID="lblClasesComplemen"  Text="Clases Acumuladas: " ></asp:Label>  
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
                                        <label for="txtFechaInicio" class="active">Fecha Inicio</label> 
                                    </div>
                                    <div class="form-group text-center row input-field">
                                        <asp:HiddenField ID="hdfFechaFIN" runat="server" />
                                        <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>  
                                        <label for="txtFechaFin" class="active">Fecha Fin</label> 
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
                                <asp:TemplateField HeaderText="Apellido" HeaderStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="UsuarioApellidoB" runat="server" Enabled="false" Text='<%# Eval("UsuarioApellido") %>' />
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

    <!-- View Modal Starts here -->
    <div id="viewModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="Button1" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Ver Asignación</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="form-group row">
                                        <label>Nombre</label>
                                        <asp:Label ID="lblNombre" runat="server" CssClass="form-control" Enabled="false"></asp:Label> 
                                    </div>
                                    <div class="form-group row">
                                        <label>Apellido</label>
                                        <asp:Label ID="lblApellido" runat="server" ClientIDMode="Static" CssClass="form-control" Enabled="false"></asp:Label>   
                                    </div>
                                    <div class="form-group row">
                                        <label>Cédula</label>
                                        <asp:Label ID="lblCedula" runat="server" ClientIDMode="Static" CssClass="form-control" Enabled="false"></asp:Label> 
                                    </div>
                                    <div class="form-group row">
                                        <label>Plan</label>
                                        <asp:Label ID="lblPlan" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label> 
                                    </div>
                                    <div class="form-group row">
                                        <label>Inicio</label>
                                        <asp:Label ID="lblFechaInicio" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>   
                                    </div>
                                    <div class="form-group row">
                                        <label>Fin</label>
                                        <asp:Label ID="lblFechaFin" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label> 
                                    </div>
                                    <div class="form-group row">
                                        <label>Factura</label>
                                        <asp:Label ID="lblFactura" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label> 
                                    </div>
                                    <div class="form-group row">
                                        <label>Nota Factura</label>
                                        <asp:Label ID="lblNotaFactura" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label1" Visible="false" runat="server"></asp:Label>
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
    <!-- View Modal Ends here -->
        <!-- Edit Modal Starts here -->
    <div id="editModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Modificar Asignación</h5>
                                            
                </div>
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="hdfPlanAlumnoIDEdit" runat="server" />
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="text-center row input-field">
                                        <asp:TextBox ID="lblNombreEdit" runat="server" Enabled="false"></asp:TextBox>
                                        <label for="lblNombreEdit" class="active">Nombre: </label>
                                    </div>
                                    <div class="text-center row input-field">
                                        <asp:TextBox ID="lblApellidoEdit" runat="server" Enabled="false"></asp:TextBox>
                                        <label for="lblApellidoEdit" class="active">Apellido: </label>
                                    </div>
                                    <div class="text-center row input-field">
                                        <asp:TextBox ID="lblCedulaEdit" runat="server" Enabled="false"></asp:TextBox>
                                        <label for="lblCedulaEdit" class="active">Cédula: </label>
                                    </div>
                                    <div class="text-center row input-field">
                                        <asp:TextBox ID="lblPlanEdit" runat="server" Enabled="false"></asp:TextBox>
                                        <label for="lblPlanEdit" class="active">Plan: </label>
                                    </div>
                                    <div class="text-center row input-field">
                                        <asp:TextBox ID="lblInicioEdit" runat="server" ></asp:TextBox>
                                        <label for="lblInicioEdit" class="active">Inicio: </label>
                                    </div>
                                    <div class="text-center row input-field">
                                        <asp:TextBox ID="lblFinEdit" runat="server" ></asp:TextBox>
                                        <label for="lblFinEdit" class="active">Fin: </label>
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>   
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label9" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave"  runat="server" Text="Modificar" CssClass="waves-effect waves-light btn" OnClick="btnSave_Click" />
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Edit Modal Ends here -->
    <!-- Delete Record Modal Starts here-->
    <div id="deleteModal"  class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDelete" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Eliminar Asignacion de Plan</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField ID="hPlanAlumnoID" runat="server" />
                                                    
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
            <asp:SqlDataSource runat="server" ID="SqlDataSource1"></asp:SqlDataSource>
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