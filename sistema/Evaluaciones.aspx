<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Evaluaciones.aspx.cs" Inherits="sistema_Evaluaciones" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h4>Evaluaciones</h4>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row form-group text-center">
                    <label>Seleccione la Clase</label>
                </div>
                <div class="row form-group text-center">
                    <div class="col-lg-4 col-xs-12"></div>
                    <div class="col-lg-4 col-xs-12">
                        <!--
                        <div class="form-group row input-field">
                            <asp:TextBox  ID="txtClasesAuto"  AutoPostBack="true"  OnTextChanged="txtClasesAuto_TextChanged" runat="server"></asp:TextBox>
                            <label for="txtClasesAuto">Nombre de la Clase</label>
                        </div>
                        -->
                        <div class="form-group row input-field">
                            <asp:DropDownList OnSelectedIndexChanged="dplClases_SelectedIndexChanged" CssClass="browser-default" runat="server" ID="dplClases" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="row form-group text-center">
                            <label>Seleccione el Alumno</label>
                        </div>
                        <div class="row form-group text-center">
                            <asp:DropDownList Enabled="false" OnSelectedIndexChanged="dplAlumnos_SelectedIndexChanged" CssClass="browser-default" runat="server" ID="dplAlumnos" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 col-xs-12"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <div class="row form-group text-center">
                    <asp:Label  runat="server" ID="fechaClase" CssClass="label-info" Text=""></asp:Label>
                </div>
                <div class="row form-group">
                    <div class="col-lg-6 col-xs-12">
                        <!-- Panel Derecho (Elementos Evaluados)-->
                        <h5 class="text-center">Elementos Evaluados</h5>
                        <asp:GridView ID="GridView2" runat="server" Width="90%" HorizontalAlign="Center"
                            OnRowCommand="GridView2_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                            DataKeyNames="AluNivClaseID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                            onpageindexchanging="GridView2_PageIndexChanging" EmptyDataText="No existen Elementos evaluados"
                            PagerStyle-CssClass="pagination" 
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                                <asp:ButtonField CommandName="Modificar"
                                    ButtonType="Button" Text="Modificar" HeaderText="Moddificar">
                                    <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                                </asp:ButtonField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="AlumNivClasElemID" runat="server" Enabled="false" Text='<%# Eval("AlumNivClasElemID") %>' />
                                        <asp:Label ID="CalificacionID" runat="server" Enabled="false" Text='<%# Eval("CalificacionID") %>' />
                                        <asp:Label ID="ClaseID" runat="server" Enabled="false" Text='<%# Eval("ClaseID") %>' />
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clase">
                                    <ItemTemplate>
                                        <asp:Label ID="ClaseDescripcion" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Elemento">
                                    <ItemTemplate>
                                        <asp:Label ID="ElementoNombre" runat="server" Enabled="false" Text='<%# Eval("ElementoNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Calificación">
                                    <ItemTemplate>
                                        <asp:Label ID="CalificacionNombre" runat="server" Enabled="false" Text='<%# Eval("CalificacionNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                           
                            </Columns>
                        </asp:GridView>
                        <!-- Fin Panel Derecho (Elementos Evaluados)-->
                    </div>
                    <div class="col-lg-6 col-xs-12">
                        <!-- Panel Izquierdo (Elementos No Evaluados)-->
                        <h5 class="text-center">Elementos para Evaluar</h5>
                        <asp:GridView ID="GridView1"  runat="server" Width="90%" HorizontalAlign="Center"
                            OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                            DataKeyNames="AluNivClaseID" PageSize="20"
                            onpageindexchanging="GridView1_PageIndexChanging" EmptyDataText="No existen Elementos"
                            CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline"
                            PagerStyle-CssClass="pagination" 
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                                <asp:ButtonField CommandName="Evaluar"
                                    ButtonType="Button" Text="Evaluar" HeaderText="Evaluar">
                                    <ControlStyle CssClass="btn btn-purple"></ControlStyle>
                                </asp:ButtonField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="AluNivClaseID" runat="server" Enabled="false" Text='<%# Eval("AluNivClaseID") %>' />
                                        <asp:Label ID="ClaseElemNivID" runat="server" Enabled="false" Text='<%# Eval("ClaseElemNivID") %>' />
                                        <asp:Label ID="ClaseID1" runat="server" Enabled="false" Text='<%# Eval("ClaseID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clase">
                                    <ItemTemplate>
                                        <asp:Label ID="ClaseDescripcion1" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Elemento">
                                    <ItemTemplate>
                                        <asp:Label ID="ElementoNombre1" runat="server" Enabled="false" Text='<%# Eval("ElementoNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                           
                            </Columns>
                        </asp:GridView>
                        <!-- Fin Panel Izquierdo (Elementos NO Evaluados)-->
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- Edit Modal Starts here -->
    <div id="modalEvaluar" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5 id="editModalLabel">Evaluar Elemento</h5>                                            
                </div>
                <asp:UpdatePanel ID="upEdit" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hdfAluNivClaseID" />
                                <asp:HiddenField runat="server" ID="hdfClaseElemNivID" />
                                <asp:HiddenField runat="server" ID="hdfClaseID" />
                                <div class="col s2"></div>
                                <div class="col s8">
                                    <div class="form-group row text-center input-field">
                                        <asp:TextBox ID="txtClaseEdit" runat="server" Enabled="false"></asp:TextBox> 
                                        <label for="txtClaseEdit" class="active">Clase</label>
                                    </div>
                                    <div class="form-group row text-center input-field">
                                        <asp:TextBox ID="txtElementoEdit" runat="server" Enabled="false"></asp:TextBox> 
                                        <label for="txtElementoEdit" class="active">Elemento</label>
                                    </div>
                                    <div class="form-group row text-center input-field">
                                        <asp:DropDownList CssClass="browser-default" ID="dplCalificacion" runat="server"></asp:DropDownList>  
                                    </div>
                                </div>
                                <div class="col s2"></div>               
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Evaluar" CssClass="btn btn-purple" OnClick="btnSave_Click" />
                            <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
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

    <!-- Edit Modal Starts here -->
    <div id="modalModificar" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeEdit1" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Modificar Evaluación</h5>                                            
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hdfAlumNivClasElemID" />
                                <asp:HiddenField runat="server" ID="hdfClaseIDMod" />
                                <div class="col s2"></div>
                                <div class="col s8">
                                    <div class="form-group row text-center input-field">
                                        <asp:TextBox ID="txtClaseModificar" runat="server" Enabled="false"></asp:TextBox> 
                                        <label for="txtClaseModificar" class="active">Clase: </label>
                                    </div>
                                    <div class="form-group row text-center input-field">
                                        <asp:TextBox ID="txtElementoModificar" runat="server" Enabled="false"></asp:TextBox>  
                                        <label for="txtElementoModificar" class="active">Elemento: </label>
                                    </div>
                                    <div class="form-group row text-center input-field">
                                        <asp:DropDownList CssClass="browser-default" ID="dplCalificacionMod" runat="server" ClientIDMode="Static"  ></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col s2"></div>                                           
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label4" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-purple" OnClick="btnModificar_Click" />
                            <button class="btn btn-purple" data-dismiss="modal" aria-hidden="true">Cerrar</button>
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