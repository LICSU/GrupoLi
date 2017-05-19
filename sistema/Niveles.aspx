<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Niveles.aspx.cs" Inherits="sistema_Niveles" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Listar Niveles</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel ID="upCrudGrid" runat="server">
            <ContentTemplate>
                <div class="form-group row text-center">
                    <div class="col-lg-3 col-xs-12"></div>
                    <div class="col-lg-6 col-xs-12">
                        <div class="col-lg-6 col-xs-12 input-field">
                            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                            <label for="txtSearch">Buscar</label>
                        </div>
                        <div class="col-lg-6 col-xs-12">
                            <asp:Button ID="btnSearch" runat="server" Text="Buscar"  CssClass="waves-effect waves-light btn" OnClick="btnSearch_Click" /> 
                        </div>
                    </div>
                    <div class="col-lg-1"></div>
                    <div class="col-lg-1">
                        <asp:ImageButton ID="Add" runat="server" ImageUrl="~/images/agregar.png" OnClick="btnAdd_Click" />
                    </div>
                    <div class="col-lg-1">
                        <asp:ImageButton  ID="ImgbtnArchivo" runat="server" ImageUrl="~/images/descargar.png" OnClick="btnArchivo_Click" />
                    </div>
                </div>
                <div class="form-group row text-center">
                    <asp:GridView ID="GridView1" runat="server" Width="90%" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="NivelID" CssClass="table table-striped table-bordered table-hover dataTable no-footer dtr-inline" PageSize="20"
                        onpageindexchanging="GridView1_PageIndexChanging"
                        PagerStyle-CssClass="pagination" 
                        PagerSettings-PreviousPageText="Anterior" PagerSettings-NextPageText="Proxima" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <Columns>  
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="IDNivel_1" Visible="false" runat="server" Enabled="false" Text='<%# Eval("NivelID") %>' />
                                    <asp:Label ID="Nombre" runat="server" Enabled="false" Text='<%# Eval("NivelNombre") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>   
                            <asp:ButtonField CommandName="editRecord"
                                ButtonType="Image" ImageUrl="~/images/editar.png" HeaderText="Mod">
                                <ControlStyle></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="deleteRecord"
                                ButtonType="Image" ImageUrl="~/images/eliminar.png" HeaderText="Eli">
                                <ControlStyle ></ControlStyle>
                            </asp:ButtonField>                                                        
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="ImgbtnArchivo" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!-- Add Modal Starts here -->
    <div id="addModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeAdd" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Agregar Nivel</h5>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row text-center">
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="row form-group input-field">
                                        <asp:TextBox ID="txtNombreAdd" runat="server"></asp:TextBox>  
                                        <label for="txtNombreAdd">Nombre</label>
                                    </div>
                                    <div class="row form-group input-field text-left">
                                        <asp:CheckBox ID="chkActivoAdd" runat="server" ClientIDMode="Static"  /> 
                                        <label for="chkActivoAdd">Activo</label>
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>  
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="Label3" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnAdd" runat="server" Text="Agregar" CssClass="waves-effect waves-light btn"  OnClick="btnAgregar_Click"/>
                            <button class="waves-effect waves-light btn" data-dismiss="modal" aria-hidden="true">Cerrar</button>
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
                    <h5>Modificar Nivel</h5>
                                            
                </div>
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <asp:HiddenField ID="hNivelMod" runat="server" /> 
                                <div class="col-lg-2"></div>
                                <div class="col-lg-8">
                                    <div class="row form-group text-center">
                                        <asp:TextBox ID="txtNombreEdit" runat="server"></asp:TextBox>  
                                        <label class="active" for="txtNombreEdit">Nombre</label>
                                    </div>
                                    <div class="row form-group text-left">
                                        <asp:CheckBox ID="chkActivaEdit" runat="server" ClientIDMode="Static" />
                                        <label for="chkActivaEdit">Activo</label>
                                    </div>
                                </div>
                                <div class="col-lg-2"></div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                            <asp:Button ID="btnSave" runat="server" Text="Modificar" CssClass="btn btn-purple" OnClick="btnSave_Click" />
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
    <!-- Delete Record Modal Starts here-->
    <div id="deleteModal"  class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button id="closeDelete" type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5>Eliminar Nivel</h5>
                </div>
                <asp:UpdatePanel ID="upDel" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            ¿Seguro desea eliminar este registro?
                            <asp:HiddenField ID="hNivelDel" runat="server" />
                                                    
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
     <uc3:ucFooter runat="server" ID="ucFooter" />

    <script type="text/javascript">

    </script>
</form>