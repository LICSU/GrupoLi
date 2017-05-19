<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Perfil.aspx.cs" Inherits="sistema_Perfil" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h4>Editar Perfil</h4>
        </div>
        <div class="row"><br /></div>
        <div class="row">
                <div class="col-lg-1"></div>
                <div class="col-lg-10">
                    <!-- FORMULARIO -->
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="input-field">
                                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                                <label for="txtNombre" class="active">Nombres</label>
                            </div>                                   
                        </div>
                        <div class="col-lg-6">
                            <div class="input-field">
                                <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
                                <label for="txtApellido" class="active">Apellidos</label>
                            </div>                                   
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="input-field">
                                <asp:TextBox ID="txtCedula" runat="server"></asp:TextBox>
                                <label for="txtCedula" class="active">Cedula</label>
                            </div>    
                        </div>
                        <div class="col-lg-6">
                            <div class="input-field">                               
                                <asp:TextBox ID="txtFechaNacimiento" runat="server"></asp:TextBox>
                                <label for="txtFechaNacimiento" class="active">Fecha de Nacimiento (dd/mm/yyyy)</label>
                             </div>
                        </div>
                    </div>    
                    <div class="row">
                        <div class="col-lg-6">
                            <div>
                                <input  type="radio" runat="server" name="SexoOpcion" id="rdM" value="M" checked="true"/>
                                <label for="rdM">Masculino</label>
                                <input type="radio" runat="server" name="SexoOpcion" id="rdF" value="F" /> 
                                <label for="rdF">Femenino</label>
                            </div>         
                        </div>
                        <div class="col-lg-6">                                                     
                            <div class="input-field">
                                <asp:TextBox ID="txtCelular" runat="server"></asp:TextBox>
                                <label for="txtCelular" class="active">Celular</label>
                            </div>
                       </div>
                    </div> 
                    <div class="row">
                        <div class="col-lg-6"> 
                            <div class="input-field">
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                <label for="txtEmail" class="active">Email</label>
                            </div>                                   
                        </div>
                        <div class="col-lg-6">    
                            <div class="input-field">
                                <asp:DropDownList runat="server" ID="ddlEstadoCivil" CssClass="browser-default" >
                                    <asp:ListItem Text="Estado Civil" Value=""  />
                                    <asp:ListItem Text="Soltero(a)" Value="S"  />
                                    <asp:ListItem Text="Casado(a)" Value="C"  />
                                    <asp:ListItem Text="Divorciado(a)" Value="D"  />
                                    <asp:ListItem Text="Viudo(a)" Value="V"  />
                                    <asp:ListItem Text="Otro(a)" Value="O"  />
                                </asp:DropDownList>
                             </div>
                        </div>
                    </div> 
                    <div class="row">
                        <div class="col-lg-6">                                         
                            <div class="input-field">
                                <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
                                <label for="txtTelefono" class="active">Teléfono</label>
                            </div>
                        </div>
                        <div class="col-lg-6">                                         
                            <div class="input-field">
                            <asp:TextBox ID="txtCelular2" runat="server"></asp:TextBox>
                            <label for="txtCelular2" class="active">Celular 2</label>
                            </div>
                        </div>   
                     </div>
                    <asp:PlaceHolder runat="server" ID="phProfesor" Visible="false">   
                        <div class="row">      
                            <div class="col-lg-6">
                                <div class="input-field">
                                    <asp:TextBox ID="txtRiesgos" runat="server"></asp:TextBox>
                                    <label for="txtRiesgos" class="active">Riesgos</label>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="input-field">
                                    <asp:TextBox ID="txtPension" runat="server"></asp:TextBox>
                                    <label for="txtPension" class="active">Pensión</label>
                                </div>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                    <div class="row"> 
                        <div class="col-lg-12"> 
                            <div class="input-field">                               
                                <asp:TextBox ID="txtEPS" runat="server"></asp:TextBox>
                                <label for="txtEPS" class="active">EPS</label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12"> 
                            <div class="input-field">
                                <asp:TextBox runat="server" CssClass="materialize-textarea" TextMode="MultiLine" ID="txtObservacion" Rows="4" />
                                <label for="txtObservacion" class="active">¿Algún antecedente o condición física que debamos conocer?</label>
                            </div>
                        </div>
                    </div>
                    <div class="row">  
                        <div class="col-lg-2">
                            <asp:Image runat="server" ID="imgPerfil" CssClass="perfil"></asp:Image>
                        </div>
                        <div class="col-lg-10 file-field input-field">
                            <div class="btn">
                                <span>Subir Foto de Perfil</span>
                                <asp:FileUpload runat="server" ID="fuPerfil" />
                            </div>
                            <div class="file-path-wrapper">
                                <input class="file-path validate" type="text" />
                            </div>
                        </div> 
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-12" >
                            <asp:CheckBox runat="server" ID="chkCambioClave" OnCheckedChanged="chkCambioClave_CheckedChanged" AutoPostBack="true" />
                            <label for="chkCambioClave" >¿Desea actualizar la contraseña?</label>      
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">                                                    
                            <div class="input-field">
                                <asp:TextBox ID="txtClaveNueva" runat="server" TextMode="Password" Enabled="false" ></asp:TextBox>
                                <label for="txtClaveNueva" class="active">Clave Nueva</label>
                            </div>
                        </div>
                        <div class="col-lg-6">                                                     
                            <div class="input-field">
                                <asp:TextBox ID="txtClaveRepetir" runat="server" TextMode="Password" Enabled="false" ></asp:TextBox>
                                <label for="txtClaveRepetir" class="active">Repetir Clave</label>
                            </div>
                        </div>
                    </div>                    
                    <div class="row">
                        <div class="col-lg-6 text-right">
                            <asp:Button ID="btnActualizar" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnActualizar_Click" Text="Actualizar" />
                        </div>
                        <div class="col-lg-6 text-left">
                            <asp:Button ID="btnCancelar" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" />
                        </div>
                    </div>
                    <!-- FIN FORMULARIO-->                    
                 </div>
                <div class="col-lg-1"></div>
            </div>
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

     <uc3:ucFooter runat="server" ID="ucFooter" />

    <script type="text/javascript">
        $(function () {
            $('#<%= txtFechaNacimiento.ClientID %>').datetimepicker({
                locale: 'es',
                format: 'DD/MM/YYYY'
            });


            $('#<%= chkCambioClave.ClientID %>').change(function () {

                if ($(this).prop("checked") == true)
                    $('#pnlClave').show();
                else
                    $('#pnlClave').hide();


            });


            $('#form1')
                .formValidation({
                    framework: 'bootstrap',
                    icon: {
                        valid: 'fa fa-check',
                        invalid: 'fa fa-times',
                        validating: 'fa fa-refresh'
                    },
                    fields: {
                        txtNombre: {

                            validators: {
                                notEmpty: {
                                    message: 'El nombre es requerido'
                                }
                            }
                        },
                        txtApellido: {

                            validators: {
                                notEmpty: {
                                    message: 'El apellido es requerido'
                                }
                            }
                        },
                        txtCedula: {
                            validators: {
                                notEmpty: {
                                    message: 'La cédula es requerido'
                                }
                            }
                        },

                        txtFechaNacimiento: {
                            validators: {
                                date: {
                                    format: 'DD/MM/YYYY',
                                    message: 'El formato de la fecha es invalido'
                                }
                            }
                        },

                        txtEmail: {
                            validators: {

                                emailAddress: {
                                    message: 'El formato del E-mail no es valido'
                                }
                            }
                        },
                        txtTelefono: {
                            validators: {

                                regexp: {
                                    message: 'Solo puede contener dígitos',
                                    regexp: /\d/
                                }
                            }
                        },
                        txtCelular: {
                            validators: {

                                regexp: {
                                    message: 'Solo puede contener dígitos',
                                    regexp: /\d/
                                }
                            }
                        },
                        txtCelular2: {
                            validators: {

                                regexp: {
                                    message: 'Solo puede contener dígitos',
                                    regexp: /\d/
                                }
                            }
                        }
                        ,
                        txtClaveNueva: {
                            validators: {
                                notEmpty: {
                                    message: 'La clave es requerida.'
                                }
                            }
                        },
                        txtClaveRepetir: {
                            validators: {
                                notEmpty: {
                                    message: 'La confirmación es requerida.'
                                },
                                identical: {
                                    field: 'txtClaveNueva',
                                    message: 'La clave y su confirmación deben ser iguales'
                                }
                            }
                        }
                    }
                })

        });

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