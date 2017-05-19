<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registrar.aspx.cs" Inherits="registrar" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Registro de Nuevo Alumno / Profesor</h5>
        </div>
        <div class="row"><br /></div>
        <asp:UpdatePanel runat="server" ID="upRegistro">
            <ContentTemplate>
                <div class="row for-group">
                    <div class="col s3"></div>
                    <div class="form-group input-field col s6">
                        <div class="row">
                            <asp:DropDownList runat="server" ID="dplUnidad" CssClass="browser-default" AutoPostBack="True" 
                                                OnSelectedIndexChanged="dplUnidad_SelectedIndexChanged">
                            </asp:DropDownList>      
                        </div> 
                                       
                    </div>
                    <div class="col s3"></div>
                </div>
                <div class="form-group row">
                    <div class="col s3"></div>
                    <div class="form-group input-field col s6">
                        <asp:DropDownList runat="server" ID="txtRol" CssClass="browser-default" AutoPostBack="True" 
                                                OnSelectedIndexChanged="dplRol_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col s3"></div>
                </div> 
                <!-- Formulario Alumno Empresa -->
                <asp:PlaceHolder runat="server" ID="phEmpresa" Visible="false">
                    <div class="row">
                        <div class="col s3"></div>
                        <div class="col s6">
                             <div class="form-group row">
                                <asp:DropDownList runat="server" ID="ddlTipoEmpleado" CssClass="browser-default" AutoPostBack="true" 
                                                  OnSelectedIndexChanged ="ddlTipoEmpleado_SelectedIndexChanged" >
                                    <asp:ListItem Text="Seleccione el Tipo de Empleado" Value=""  />
                                    <asp:ListItem Text="Empleado" Value="Empleado"  />
                                    <asp:ListItem Text="Contratado/Temporal" Value="Contratado"  />
                                    <asp:ListItem Text="Otro" Value="Otro"  />
                                </asp:DropDownList>
                            </div>
                            <!--
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtBono" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <label for="txtBono">Código del bono</label>
                            </div>-->
                            <asp:PlaceHolder runat="server" ID="phTipoEmp" Visible="false">
                                <div class="form-group row input-field">
                                    <asp:TextBox ID="txtNombreEmp" runat="server" clientIDMode="Static"></asp:TextBox>
                                    <label for="txtNombreEmp">Nombre(s)</label>
                                </div>
                                <div class="form-group row input-field">
                                    <asp:TextBox ID="txtEmailEmp" runat="server"  CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                    <label for="txtEmailEmp">Email</label>
                                </div>
                            </asp:PlaceHolder>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtCedulaEmp" runat="server" clientIDMode="Static"></asp:TextBox>
                                <label for="txtCedulaEmp">Número de Cédula</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtClaveNuevaEmp" runat="server" TextMode="Password" CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtClaveNuevaEmp">Contraseña</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtClaveRepetirEmp" runat="server" TextMode="Password" CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtClaveRepetirEmp">Confirme la Contraseña</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtCelularEmp" runat="server" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtCelularEmp">Número de Celular</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtObservacion" Rows="4" ClientIDMode="Static" CssClass="materialize-textarea"  ></asp:TextBox>
                                <label for="txtObservacion">¿Algún antecedente o condición física que debamos conocer?</label>
                            </div>
                            <div class="form-group row">
                                <asp:DropDownList runat="server"  ID="dplActivarPlan" CssClass="browser-default">
                                    <asp:ListItem Text="¿Desea Activar el Plan?" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="Si"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group row input-field">
                                <div class="col s6 text-center">
                                    <asp:Button ID="btnRegistrarEmmpresa" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnRegistrarEmmpresa_Click" Text="Registrar" />
                                </div>
                                <div class="col s6 text-center">
                                    <asp:Button ID="btnCancelar" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnCancelFit_Click" Text="Cancelar" />
                                </div>
                            </div>
                        </div>                   
                        <div class="col s3"></div>
                    </div>
                </asp:PlaceHolder>
                <!-- Fin Formulario Alumno Empresa -->

                <!-- Formulario Alumno Empresa -->
                <asp:PlaceHolder runat="server" ID="phFitness" Visible="false">
                    <div class="row">
                        <div class="col s3"></div>
                        <div class="col s6">
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtCedulaFit" runat="server" clientIDMode="Static"></asp:TextBox>
                                <label for="txtCedulaFit">Número de Cédula</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtNombre" runat="server" clientIDMode="Static"></asp:TextBox>
                                <label for="txtNombre">Nombre(s)</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtApellido" runat="server" clientIDMode="Static"></asp:TextBox>
                                <label for="txtApellido">Apellido(s)</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtClaveNuevaFit" runat="server" TextMode="Password" CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtClaveNuevaFit">Contraseña</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtClaveRepetirFit" runat="server" TextMode="Password" CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtClaveRepetirFit">Confirme la Contraseña</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtEmail" runat="server"  CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtEmail">Email</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtCelularFit" runat="server"  CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtCelular">Número de Celular</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtCelular2" runat="server"  CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtCelular2">Otro Número de Celular</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtTelefono" runat="server"  CssClass="col-sm-4 form-control" ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtTelefono">Número de Teléfono</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtFechaNacimiento" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group row">
                                <asp:DropDownList runat="server" ID="ddlEstadoCivil" CssClass="browser-default" >
                                    <asp:ListItem Text="Soltero(a)" Value="S"  />
                                    <asp:ListItem Text="Casado(a)" Value="C"  />
                                    <asp:ListItem Text="Divorciado(a)" Value="D"  />
                                    <asp:ListItem Text="Viudo(a)" Value="V"  />
                                    <asp:ListItem Text="Otro(a)" Value="O"  />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group row input-field">
                                <input  type="radio" runat="server" name="SexoOpcion" id="rdM" value="M" />
                                <label for="rdM">Masculino</label>
                                <input type="radio" runat="server" name="SexoOpcion" id="rdF" value="F" /> 
                                <label for="rdF">Femenino</label>
                            </div>
                            <asp:PlaceHolder runat="server" ID="phPension" Visible ="false">
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtRiesgos" runat="server" TextMode="SingleLine"  ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtRiesgos">Riesgos</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtPension" runat="server" TextMode="SingleLine"  ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtPension">Pension</label>
                            </div>
                            </asp:PlaceHolder>
                            <div class="form-group row input-field">
                                <asp:TextBox ID="txtEPS" runat="server" TextMode="SingleLine"  ClientIDMode="Static" ></asp:TextBox>
                                <label for="txtEPS">EPS</label>
                            </div>
                            <div class="form-group row input-field">
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtObservacionFit" Rows="4" ClientIDMode="Static" CssClass="materialize-textarea"  ></asp:TextBox>
                                <label for="txtObservacionFit">¿Algún antecedente o condición física que debamos conocer?</label>
                            </div>
                            <div class="form-group row input-field">
                                <div class="col s6 text-center">
                                    <asp:Button ID="btnAddFit" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnAddFit_Click" Text="Registrar" />
                                </div>
                                <div class="col s6 text-center">
                                    <asp:Button ID="btnCancelarFit" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnCancelFit_Click" Text="Cancelar" />
                                </div>
                            </div>
                        </div>
                        <div class="col s3"></div>
                    </div>
                </asp:PlaceHolder>
                <!-- Fin Formulario Alumno Empresa -->
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