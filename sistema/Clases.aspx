<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Clases.aspx.cs" Inherits="sistema_Clases" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Listado de Clases</h5>
        </div>
        <div class="row"><br /></div>
        
        <div class="row">
            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                 <ContentTemplate>
                     <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-10">
                            <div class="input-field col s6">
                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                <label for="txtSearch">Clase</label>
                            </div>
                            <div class="col-lg-2 col-xs-4">
                                <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" />                                                                                        
                            </div>
                            <div class="col-lg-2 col-xs-8">&nbsp;</div>
                            <div class="col-lg-1 col-xs-2">
                                <asp:ImageButton ID="Add" runat="server" ImageUrl="~/Images/agregar.png" OnClick="btnAdd_Click" />
                            </div>
                            <div class="col-lg-1 col-xs-2 text-right">                                                
                                <asp:ImageButton ID="ImgbtnArchivo" runat="server" ImageUrl="~/Images/descargar.png" OnClick="btnArchivo_Click" />
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                     <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="ClaseID"  PageSize="20"
                        CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                        <Columns>    
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="IDClase_1" runat="server" Visible="false" Enabled="false" Text='<%# Eval("ClaseID") %>' />
                                    <asp:Label ID="Nombre" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Label ID="Tipo" runat="server" Enabled="false" Text='<%# Eval("Tipo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo de Calificación">
                                <ItemTemplate>
                                    <asp:Label ID="TipoCalificacionID" runat="server" Visible="false" Enabled="false" Text='<%# Eval("TipoCalificacionID") %>' />
                                    <asp:Label ID="TipoCalificacion" runat="server" Enabled="false" Text='<%# Eval("TipoCalificacion") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Unidades">
                                <ItemTemplate>
                                    <asp:Label ID="Unidad" runat="server" Enabled="false" Text='<%# Eval("Unidad") %>' />
                                    <asp:Label ID="Sensor" Visible="false" runat="server" Enabled="false" Text='<%# Eval("Sensor") %>' />
                                    <asp:Label ID="Break" Visible="false" runat="server" Enabled="false" Text='<%# Eval("TiempoCambio") %>' />
                                    <asp:Label ID="Estacion" Visible="false" runat="server" Enabled="false" Text='<%# Eval("Estacion") %>' />
                                    <asp:Label ID="Intervalo" Visible="false" runat="server" Enabled="false" Text='<%# Eval("Intervalo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderText="Activa" Visible="False">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Activa" runat="server" Enabled="false" Checked='<%# Eval("ClaseActiva") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>    
                            <asp:ButtonField CommandName="editRecord"
                                ButtonType="Image" ImageUrl="~/Images/editar.png" HeaderText="Mod">
                                <ControlStyle></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="deleteRecord"
                                ButtonType="Image" ImageUrl="~/Images/eliminar.png" HeaderText="Eli">
                                <ControlStyle></ControlStyle>
                            </asp:ButtonField>                              
                        </Columns>
                    </asp:GridView>
                 </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- Add Modal Starts here -->
        <div id="addModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h5>Agregar Clase</h5>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-10">
                                        <div class="row">
                                            <div class="form-group input-field">  
                                                 <asp:TextBox ID="txtNombreAdd" runat="server" ClientIDMode="Static"></asp:TextBox> 
                                                 <label for="txtNombreAdd">Nombre</label>                                                               
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList ID="dplTipo" runat="server" ClientIDMode="Static" CssClass="browser-default">
                                                    <asp:ListItem Value="" Text="Seleccione el Tipo de Clase"></asp:ListItem>
                                                    <asp:ListItem Value="Complementaria" Text="Complementaria"></asp:ListItem>
                                                    <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList ID="dplUnidad" runat="server" ClientIDMode="Static" CssClass="browser-default">
                                                <asp:ListItem Value="" Text="Seleccione la Unidad"></asp:ListItem>
                                                <asp:ListItem Value="0,25" Text="0.25"></asp:ListItem>
                                                <asp:ListItem Value="0,5" Text="0.5"></asp:ListItem>
                                                <asp:ListItem Value="0,75" Text="0.75"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList runat="server" ID="dplTipoCalificacion" ClientIDMode="Static" CssClass="browser-default">
                                                    <asp:ListItem Value="" Text="Seleccione el Tipo de Calificacion"></asp:ListItem>
                                                </asp:DropDownList>                                               
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList CssClass="browser-default" ID="dplOrden" AutoPostBack="true" runat="server" 
                                                                    ClientIDMode="Static" OnSelectedIndexChanged="dplOrden_SelectedIndexChanged"  >
                                                    <asp:ListItem Text="Esta ordenada por Espacios?" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Si" Value="Si"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>   
                                            </div>
                                        </div>
                                        <asp:PlaceHolder runat="server" ID="plhOrdenAdd" Visible="false">
                                            <div class="row">
                                                <div class="form-group input-field">
                                                    <asp:TextBox ID="txtFilasAdd" runat="server" ClientIDMode="Static"  ></asp:TextBox>
                                                    <label for="txtFilasAdd">Cantidad de Filas</label> 
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group input-field">
                                                    <asp:TextBox ID="txtColumnasAdd" runat="server" ClientIDMode="Static"  ></asp:TextBox> 
                                                    <label for="txtFilasAdd">Cantidad de Columnas</label> 
                                                </div>
                                            </div>
                                        </asp:PlaceHolder>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:CheckBox ID="chkActivoAdd" runat="server" ClientIDMode="Static"  /> 
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-xs-10 control-label text-left">Solo para Sistema Medical Li: </label>                                                            
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList ID="dplIntervaloAdd" CssClass="browser-default" runat="server" ClientIDMode="Static"  >
                                                <asp:ListItem Text="Seleccione un Intervalo" Value=""></asp:ListItem>
                                                <asp:ListItem Text="5 Minutos" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="10 Minutos" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="15 Minutos" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="20 Minutos" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="25 Minutos" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="30 Minutos" Value="30"></asp:ListItem>
                                            </asp:DropDownList>   
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList ID="dplSensoresAdd" CssClass="browser-default" runat="server" ClientIDMode="Static"  >
                                                <asp:ListItem Text="¿Número de Sensores?" Value=""></asp:ListItem>
                                                <asp:ListItem Text="1 Sensor" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2 Sensores" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3 Sensores" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4 Sensores" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5 Sensores" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6 Sensores" Value="6"></asp:ListItem>
                                            </asp:DropDownList>  
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:ListBox  ID="ddlEstaciones" CssClass="browser-default" runat="server" SelectionMode="Multiple" Rows="3">
                                                <asp:ListItem Text="Volumen" Value="Volumen"></asp:ListItem>
                                                <asp:ListItem Text="Peso" Value="Peso"></asp:ListItem>
                                                <asp:ListItem Text="Nivel de Actividad Fisica" Value="Nivel de Actividad Fisica"></asp:ListItem>
                                                <asp:ListItem Text="Porcentaje de grasa" Value="Porcentaje de grasa"></asp:ListItem>
                                                <asp:ListItem Text="Electrocardiograma" Value="Electrocardiograma"></asp:ListItem>
                                                <asp:ListItem Text="Tensiometro" Value="Tensiometro"></asp:ListItem>
                                                <asp:ListItem Text="Oximetro" Value="Oximetro"></asp:ListItem>
                                                <asp:ListItem Text="Prueba de esfuerzo" Value="Prueba de esfuerzo"></asp:ListItem>
                                                <asp:ListItem Text="Metabolismo" Value="Metabolismo"></asp:ListItem>
                                                <asp:ListItem Text="Electroencefalograma" Value="Electroencefalograma"></asp:ListItem>
                                            </asp:ListBox>    
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:TextBox runat="server" ID="txtBreakAdd"></asp:TextBox>
                                                <label for="txtBreakAdd">¿Tiempo de Intercambio por Turno? (Minutos)</label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col s6 text-right">
                                                <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="btn btn-purple"  OnClick="btnAgregar_Click"/>
                                            </div>
                                            <div class="col s6 text-left">
                                                <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-1"></div>
                                </div>                                
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click"  />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <!-- Add Modal Ends here -->
            <!-- Edit Modal Starts here -->
        <div id="editModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h5>Modificar Clase</h5>                                            
                    </div>
                    <asp:UpdatePanel ID="upEdit" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-1"></div>
                                    <div class="col-lg-10">
                                        <div class="row">
                                            <asp:HiddenField ID="hClaseMod" runat="server" />
                                            <div class="form-group input-field">
                                                <asp:TextBox ID="txtNombreEdit" runat="server" ClientIDMode="Static" ></asp:TextBox>
                                                <label for="txtNombreEdit" class="active">Nombre</label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList ID="dplTipoEdit" runat="server" ClientIDMode="Static" CssClass="browser-default">
                                                    <asp:ListItem Value="" Text="Seleccione el Tipo de Clase"></asp:ListItem>
                                                    <asp:ListItem Value="Complementaria" Text="Complementaria"></asp:ListItem>
                                                    <asp:ListItem Value="Regular" Text="Regular"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList ID="dplUnidadEdit" runat="server" ClientIDMode="Static" CssClass="browser-default">
                                                <asp:ListItem Value="" Text="Seleccione la Unidad"></asp:ListItem>
                                                <asp:ListItem Value="0,25" Text="0.25"></asp:ListItem>
                                                <asp:ListItem Value="0,5" Text="0.5"></asp:ListItem>
                                                <asp:ListItem Value="0,75" Text="0.75"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList runat="server" ID="dplTipoCalificacionEdit" ClientIDMode="Static" CssClass="browser-default">
                                                    <asp:ListItem Value="" Text="Seleccione el Tipo de Calificacion"></asp:ListItem>
                                                </asp:DropDownList>                                               
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList CssClass="browser-default" ID="dplOrdenEdit" AutoPostBack="true" runat="server" 
                                                                    ClientIDMode="Static" OnSelectedIndexChanged="dplOrden_SelectedIndexChanged"  >
                                                    <asp:ListItem Text="Esta ordenada por Espacios?" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Si" Value="Si"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:DropDownList>   
                                            </div>
                                        </div>
                                        <asp:PlaceHolder runat="server" ID="plhOrdenEdit" Visible="false">
                                            <div class="row">
                                                <div class="form-group input-field">
                                                    <asp:TextBox ID="txtFilasEdit" runat="server" ClientIDMode="Static"  ></asp:TextBox>
                                                    <label for="txtFilasEdit" class="active">Cantidad de Filas</label> 
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group input-field">
                                                    <asp:TextBox ID="txtColumnasEdit" runat="server" ClientIDMode="Static"  ></asp:TextBox> 
                                                    <label for="txtColumnasEdit" class="active">Cantidad de Columnas</label> 
                                                </div>
                                            </div>
                                        </asp:PlaceHolder>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:CheckBox ID="chkActivaEdit" runat="server" ClientIDMode="Static"  /> 
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-xs-10 control-label text-left">Solo para Sistema Medical Li: </label>                                                            
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList ID="dplIntervaloEdit" CssClass="browser-default" runat="server" ClientIDMode="Static"  >
                                                <asp:ListItem Text="Seleccione un Intervalo" Value=""></asp:ListItem>
                                                <asp:ListItem Text="5 Minutos" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="10 Minutos" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="15 Minutos" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="20 Minutos" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="25 Minutos" Value="25"></asp:ListItem>
                                                <asp:ListItem Text="30 Minutos" Value="30"></asp:ListItem>
                                            </asp:DropDownList>   
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:DropDownList ID="dplSensoresEdit" CssClass="browser-default" runat="server" ClientIDMode="Static"  >
                                                <asp:ListItem Text="¿Número de Sensores?" Value=""></asp:ListItem>
                                                <asp:ListItem Text="1 Sensor" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2 Sensores" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3 Sensores" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4 Sensores" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5 Sensores" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="6 Sensores" Value="6"></asp:ListItem>
                                            </asp:DropDownList>  
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:ListBox  ID="ddlEstacionesEdit" CssClass="browser-default" runat="server" SelectionMode="Multiple" Rows="3">
                                                <asp:ListItem Text="Volumen" Value="Volumen"></asp:ListItem>
                                                <asp:ListItem Text="Peso" Value="Peso"></asp:ListItem>
                                                <asp:ListItem Text="Nivel de Actividad Fisica" Value="Nivel de Actividad Fisica"></asp:ListItem>
                                                <asp:ListItem Text="Porcentaje de grasa" Value="Porcentaje de grasa"></asp:ListItem>
                                                <asp:ListItem Text="Electrocardiograma" Value="Electrocardiograma"></asp:ListItem>
                                                <asp:ListItem Text="Tensiometro" Value="Tensiometro"></asp:ListItem>
                                                <asp:ListItem Text="Oximetro" Value="Oximetro"></asp:ListItem>
                                                <asp:ListItem Text="Prueba de esfuerzo" Value="Prueba de esfuerzo"></asp:ListItem>
                                                <asp:ListItem Text="Metabolismo" Value="Metabolismo"></asp:ListItem>
                                                <asp:ListItem Text="Electroencefalograma" Value="Electroencefalograma"></asp:ListItem>
                                            </asp:ListBox>    
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group input-field">
                                                <asp:TextBox runat="server" ID="txtBreakEdit"></asp:TextBox>
                                                <label for="txtBreakEdit" class="active">¿Tiempo de Intercambio por Turno? (Minutos)</label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col s6 text-right">
                                                <asp:Button ID="btnSave" runat="server" Text="Modificar" CssClass="btn btn-purple" OnClick="btnSave_Click" />
                                            </div>
                                            <div class="col s6 text-left">
                                                 <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-1"></div>        
                                </div>
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
        <!-- Delete Record Modal Starts here-->
        <div id="deleteModal"  class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button id="closeDelete" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h5>Eliminar Clase</h5>
                    </div>
                    <asp:UpdatePanel ID="upDel" runat="server">
                        <ContentTemplate>
                            <div class="modal-body">
                                ¿Seguro desea eliminar este registro?
                                <asp:HiddenField ID="hClaseDel" runat="server" />
                                                    
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CssClass="btn btn-purple" OnClick="btnDelete_Click" />
                                <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cancelar</button>
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
                <h5 class="modal-title"><label id="lblMsjTitle"></label></h5>
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


