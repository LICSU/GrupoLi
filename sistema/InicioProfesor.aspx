<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InicioProfesor.aspx.cs" Inherits="sistema_InicioProfesor" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />

<div id="page-wrapper" style="min-height: 292px; padding-top:130px; margin-left:0px !important;">  
    <asp:UpdatePanel ID="upCrudGrid" runat="server">
        <ContentTemplate>
            <div class="row form-group">
                <div class="col-lg-12">
                    <asp:PlaceHolder ID="phAlumnos" runat="server" Visible="True">
                        <div class="col-lg-12">
                            <h5 class="text-center"><asp:Label runat="server" ID="titulo2" Text="Alumnos Inscritos"></asp:Label></h5>
                        </div>
                        <div class="col-lg-12">
                            <asp:GridView ID="GridView2" runat="server" HorizontalAlign="Center"
                                OnRowCommand="GridView2_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                                DataKeyNames="AluNivClaseID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="10"
                                onpageindexchanging="GridView2_PageIndexChanging" EmptyDataText="No existe ningun Alumno Inscrito"
                                PagerStyle-CssClass="pagination" 
                                PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                                PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <Columns>                                
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="ReservaAID" runat="server" Enabled="false" Text='<%# Eval("ReservaID") %>' />
                                            <asp:Label ID="ClaseID" runat="server" Enabled="false" Text='<%# Eval("ClaseID") %>' />
                                            <asp:Label ID="NivelID" runat="server" Enabled="false" Text='<%# Eval("NivelID") %>' />
                                            <asp:Label ID="UsuarioID" runat="server" Enabled="false" Text='<%# Eval("UsuarioID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Foto" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:ImageButton OnCommand="VerFoto" CommandName="VerFoto" ID="UsuarioFoto" 
                                                             runat="server" CommandArgument='<%#Eval("Foto") %>' ImageUrl='<%# Eval("Foto") %>' 
                                                             AlternateText="Sin Imagen" CssClass="perfilpeq"/>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Usuario" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:Label ID="UsuarioNom" runat="server" Enabled="false" Text='<%# Eval("UsuarioNom") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Celular" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:Label ID="Celular" runat="server" Enabled="false" Text='<%# Eval("Celular") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Clase" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:Label ID="Clase" runat="server" Enabled="false" Text='<%# Eval("Clase") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:Label ID="Fecha" runat="server" Enabled="false" Text='<%# Eval("Fecha") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#Reservas" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:Label ID="CantReservas" runat="server" Enabled="false" Text='<%# Eval("CantReservas") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nivel" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:Label ID="Nivel" runat="server" Enabled="false" Text='<%# Eval("Nivel") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observacion" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:Label ID="Observacion" runat="server" Enabled="false" Text='<%# Eval("Observacion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Asistió" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Asistio" AutoPostBack="true" OnCheckedChanged="cambiarAsistencia" 
                                                          Text="&nbsp;" runat="server" reservaID='<%#Eval("ReservaID")%>'
                                                          usuarioID='<%#Eval("UsuarioID")%>' Checked='<%# Eval("Asistio") %>' 
                                                          clasePlantillaID='<%#Eval("ClasePlantillaID")%>' claseID='<%#Eval("ClaseID")%>'/>
                                            <label for="Asistio"></label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    <asp:ButtonField CommandName="Nivel" ItemStyle-CssClass="text-center"
                                        ButtonType="Image" ImageUrl="~/Images/actualizar.jpg" HeaderText="Nivel">
                                        <ControlStyle CssClass="text-center"></ControlStyle>
                                    </asp:ButtonField>         
                                    <asp:ButtonField CommandName="Calificacion" ItemStyle-CssClass="text-center"
                                        ButtonType="Image" ImageUrl="~/Images/calificar.png" HeaderText="Calificar">
                                        <ControlStyle CssClass="text-center"></ControlStyle>
                                    </asp:ButtonField>   
                                    <asp:ButtonField CommandName="Observacion" ItemStyle-CssClass="text-center"
                                        ButtonType="Image" ImageUrl="~/Images/observacion.png" HeaderText="Obs.">
                                        <ControlStyle CssClass="text-center"></ControlStyle>
                                    </asp:ButtonField>                                                     
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:PlaceHolder>
                </div>
            </div>
        </ContentTemplate>
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
            <button type="button" class="waves-effect waves-light btn" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>

<!-- Modal Foto del Usuario -->
<div class="modal fade" id="modalFoto" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
      <div class="modal-dialog" style="width: 150px !important;">
        <div class="modal-content" >
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
            <h4 class="modal-title"><label id="Label1">Foto</label></h4>
          </div>
          <div class="modal-body text-center">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Image CssClass="perfil" ImageUrl="" AlternateText="Imagen No Existe"  runat="server" ID="imgFoto"/>
                </ContentTemplate>
              </asp:UpdatePanel>
              
          </div><!-- /modal-body -->
          <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
          </div>
        </div> 
      </div>
    </div>

<!-- Modal Nivel Usuario -->
<div class="modal fade" id="modalNivel" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
    <div class="modal-content" >
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h4 class="modal-title"><label id="Label2">Actualizar Nivel de Usuario</label></h4>
        </div>
        <div class="modal-body text-center">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col-lg-3">
                        <asp:Image runat="server" ID="imgUsuNivel" AlternateText="Imagen No Existe" CssClass="perfil"></asp:Image>
                    </div>
                    <div class="col-lg-8 text-left">
                        <asp:HiddenField runat="server" ID="hdfClaseNID"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdfUsuarioNID"></asp:HiddenField>
                        <asp:Label runat="server" CssClass="form-group" ID="lblClaseN" Text="Clase: " /><br />
                        <asp:Label runat="server" CssClass="form-group" ID="lblUsuarioN" Text="Alumno: " /><br />
                        <label>Seleccione el Nivel: </label>
                        <asp:DropDownList runat="server" CssClass="form-control" ID="dplNivelMod"></asp:DropDownList>
                    </div>                        
                </div>  
            </ContentTemplate>
            </asp:UpdatePanel>
              
        </div><!-- /modal-body -->
        <div class="modal-footer">
            <asp:Button ID="btnGuardarNivel" runat="server" Text="Guardar Nivel" CssClass="btn btn-purple" OnClick="btnGuardarNivel_Click" />
            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>

<!-- Modal Evaluacion Usuario -->
<div class="modal fade" id="modalEvaluacion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
    <div class="modal-content" >
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h4 class="modal-title"><label id="Label3">Calificar Usuario</label></h4>
        </div>
        <div class="modal-body text-center">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="row form-group">
                        <div class="col-lg-3">
                            <asp:Image runat="server" ID="imgAlumno" AlternateText="Imagen No Existe" CssClass="perfil"></asp:Image>
                        </div>
                        <div class="col-lg-8 text-left">
                            <asp:HiddenField runat="server" ID="hdfUsuarioID"></asp:HiddenField>
                            <asp:HiddenField runat="server" ID="hdfReservaID"></asp:HiddenField>
                            <asp:HiddenField runat="server" ID="hdfClasePlantillaID"></asp:HiddenField>
                            <asp:HiddenField runat="server" ID="hdfClaseID"></asp:HiddenField>
                            <asp:Label runat="server" CssClass="form-group" ID="lblClase" Text="Clase: " /><br />
                            <asp:Label runat="server" CssClass="form-group" ID="lblUsuario" Text="Alumno: " /><br />
                            <asp:Button ID="btn" runat="server" Text="Ver Historial" CssClass="btn btn-purple" OnClick="btnHistorial_Click" />
                        </div>                        
                    </div>
                    <div class="row form-group">
                        <!-- Elementos Evaluados -->
                        <div class="col-lg-6">
                            <div class="row form-group">
                                <h6 class="text-center">Elementos Evaluados</h6>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">
                                    <label>Filtrar</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="ddElementosEvaluados" runat="server" CssClass="form-control" 
                                                      AutoPostBack="true" OnSelectedIndexChanged="ddElementosEvaluados_SelectedIndexChanged"></asp:DropDownList>
                                </div>                        
                            </div>
                            <asp:GridView ID="GridView3" runat="server" Width="90%" HorizontalAlign="Center"
                                OnRowCommand="GridView3_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                                DataKeyNames="AluNivClaseID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="5"
                                onpageindexchanging="GridView3_PageIndexChanging" EmptyDataText="No existen Elementos evaluados"
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
                                            <asp:Label ID="ClaseIDE" runat="server" Enabled="false" Text='<%# Eval("ClaseID") %>' />
                                        
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
                        </div>
                        <!-- Elementos A Evaluar -->
                        <div class="col-lg-6">
                            <div class="row form-group">
                                <h6 class="text-center">Elementos para Evaluar</h6>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-2">
                                    <label>Filtrar</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="ddlElementosaEvaluar" runat="server" CssClass="form-control" 
                                                      AutoPostBack="true" OnSelectedIndexChanged="ddlElementosaEvaluar_SelectedIndexChanged"></asp:DropDownList>
                                </div>                        
                            </div>
                            <asp:GridView ID="GridView4"  runat="server" Width="90%" HorizontalAlign="Center"
                                OnRowCommand="GridView4_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                                DataKeyNames="AluNivClaseID" PageSize="5"
                                onpageindexchanging="GridView4_PageIndexChanging" EmptyDataText="No existen Elementos"
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
                        </div>
                    </div>  
                </ContentTemplate>
            </asp:UpdatePanel>
              
        </div><!-- /modal-body -->
        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>

<!-- Modal Historial Calificaciones Usuario -->
<div class="modal fade" id="modalHistorial" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
    <div class="modal-content" >
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h4 class="modal-title"><label id="Label6">Historial de Calificaciones</label></h4>
        </div>        
        <div class="modal-body text-center">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <div class="row form-group">
                        <h6 class="text-center">Historial de Calificaciones</h6>
                    </div>
                    <div class="row form-group">
                        <div class="col-md-5"></div>
                        <div class="col-md-4">
                            <label>** Seleccione un Elemento para filtrar **</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlElementos" runat="server" CssClass="form-control" 
                                          AutoPostBack="true" OnSelectedIndexChanged="ddlElementos_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        
                    </div>
                    <div class="row form-group">                      
                        <asp:GridView ID="GridView5" runat="server" HorizontalAlign="Center"
                            AutoGenerateColumns="false" AllowPaging="true"
                            DataKeyNames="AluNivClaseID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="5"
                            onpageindexchanging="GridView5_PageIndexChanging" EmptyDataText="No existen Elementos evaluados"
                            PagerStyle-CssClass="pagination" 
                            PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                                <asp:TemplateField HeaderText="Clase">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" Enabled="false" Text='<%# Eval("ClaseDescripcion") %>' />                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Elemento">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" Enabled="false" Text='<%# Eval("ElementoNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="Calificación">
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server" Enabled="false" Text='<%# Eval("CalificacionNombre") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="Fecha">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Enabled="false" Text='<%# Eval("Fecha") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="Profesor">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Enabled="false" Text='<%# Eval("Profesor") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                         
                            </Columns>
                        </asp:GridView>
                    </div>  
                </ContentTemplate>
            </asp:UpdatePanel>
              
        </div><!-- /modal-body -->
        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
        </div>
    </div> 
    </div>
</div>

<!-- Edit Modal Starts here -->
<div id="modalEvaluar" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button id="closeEdit" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                <h5 id="editModalLabel">Evaluar Elemento</h5>                                            
            </div>
            <asp:UpdatePanel ID="upEdit" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <asp:HiddenField runat="server" ID="hdfAluNivClaseID" />
                            <asp:HiddenField runat="server" ID="hdfClaseElemNivID" />
                            <asp:HiddenField runat="server" ID="hdfClaseE" />
                            <div class="col s2"></div>
                            <div class="col-lg-8">
                                    <asp:Label runat="server" ID="txtElementoEdit" Text="Elemento"></asp:Label><br />
                                    <label>Seleccione la Calificación</label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlCalificacion" runat="server"></asp:DropDownList>  
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
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-body">
                        <div class="row">
                            <asp:HiddenField runat="server" ID="hdfAlumNivClasElemID" />
                            <asp:HiddenField runat="server" ID="hdfClaseIDMod" />
                            <div class="col s2"></div>
                            <div class="col-lg-8">
                                    <asp:Label runat="server" ID="txtElementoModificar" Text="Elemento"></asp:Label><br />
                                    <label>Seleccione la Calificación</label>
                                    <asp:DropDownList CssClass="form-control" ID="dplCalificacionMod" runat="server"></asp:DropDownList>  
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
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
                             
        </div>
    </div>
</div>
<!-- Edit Modal Ends here -->

<!-- Modal Observacion -->
<div class="modal fade" id="modalObservacion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">
    <div class="modal-content" >
        <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
        <h4 class="modal-title"><label id="Label5">Observacion</label></h4>
        </div>
        <div class="modal-body text-center">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>
                <div class="row form-group">
                    <div class="col-lg-3">
                        <asp:Image runat="server" ID="imgAlumnoObs" AlternateText="Imagen No Existe" CssClass="perfil"></asp:Image>
                    </div>
                    <div class="col-lg-8 text-left">
                        <asp:HiddenField runat="server" ID="hdfClaseIDObs"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hdfUsuarioIDObs"></asp:HiddenField>
                        <asp:Label runat="server" CssClass="form-group" ID="lblClaseObs" Text="Clase: " /><br />
                        <asp:Label runat="server" CssClass="form-group" ID="lblAlumnoObs" Text="Alumno: " /><br />
                        <label>Observación: </label>
                        <asp:TextBox TextMode="MultiLine" runat="server" CssClass="form-control" ID="txtObservacion" Rows="10"></asp:TextBox><br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtProfesor" Enabled="False"></asp:TextBox><br />
                    </div>                        
                </div>  
            </ContentTemplate>
            </asp:UpdatePanel>
              
        </div><!-- /modal-body -->
        <div class="modal-footer">
            <asp:Button ID="btnObservacion" runat="server" Text="Guardar Observación" CssClass="btn btn-purple" OnClick="btnGuardarObservacion_Click" />
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
    function MostrarFotoModal() {
        $('#modalFoto').modal('show');
        return true;
    }
    function MostrarNivelModal() {
        $('#modalNivel').modal('show');
        return true;
    }
    function MostrarEvaluacionModal() {
        $('#modalEvaluacion').modal('show');
        return true;
    }
    function MostrarObservacionModal() {
        $('#modalObservacion').modal('show');
        return true;
    }
    function MostrarHistorialModal() {
        $('#modalHistorial').modal('show');
        return true;
    }
</script>

</form>
