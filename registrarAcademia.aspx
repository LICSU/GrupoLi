<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registrarAcademia.aspx.cs" Inherits="registrarAcademia" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Fitness Li | Registro Academia</title>
    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/animate.css/3.1.1/animate.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/formValidation/formValidation.min.css" />
    <!-- Custom CSS -->
    <link href="css/logo-nav.css" rel="stylesheet" />
    <link href="css/estilos.css" rel="stylesheet" />
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <style>
        #form1 .selectContainer .form-control-feedback {
            right: 30px;
        }
        .has-feedback label ~ .form-control-feedback {
            right:20px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed"  style="padding-bottom: 36px !important; padding-top: 36px !important;" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">
                    <img src="images/logo.png" alt="Fitness Li" />
                </a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="navbar-collapse collapse" id="bs-example-navbar-collapse-1" aria-expanded="false" style="height: 1px;">
                <ul class="nav navbar-nav" style="float:right !important;">
                    <li>
                        <a href="index.aspx">Inicio</a>
                    </li>
                    <li>
                        <a href="#">Nosotros</a>
                    </li>
                    <li>
                        <a href="#">Clases</a>
                    </li>
                    <li>
                        <a href="#">Inscripciones</a>
                    </li>
                    <li>
                        <a href="#">Clases</a>
                    </li>
                    <li>
                        <a href="#">Contáctanos</a>
                    </li>
                    <li>
                        <a href="registrar.aspx">Registrarse</a>
                    </li>
                    <li>
                        <a href="ingresar.aspx">Ingresar</a>
                    </li>
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container -->
    </nav>
    
    <div class="container" style="margin-top:40px; min-height:600px;">
        <asp:UpdatePanel runat="server" ID="upRegistro" UpdateMode="Always">
            <ContentTemplate>
                <div class="row text-center form-group">
                    <h3>Registro de Nuevo Alumno Academia</h3>
                </div>

                <!-- Formulario Alumno GRUPOLI -->
                
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-10">
                            <div class="row form-group">
                                <div class="col-lg-6">
                                    <label for="txtCedulaFit">Número de Cédula</label> 
                                    <asp:TextBox ID="txtCedulaFit" runat="server" CssClass="form-control" clientIDMode="Static" EnableViewState="true"></asp:TextBox>                                           
                                </div>
                                <div class="col-lg-6">
                                    <label for="txtNombre">Nombre(s)</label>
                                    <asp:TextBox ID="txtNombre" runat="server" clientIDMode="Static" CssClass="form-control" MaxLength="60"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">                                
                                <div class="col-lg-6">
                                    <label for="txtApellido">Apellido(s)</label>
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" clientIDMode="Static"></asp:TextBox>                            
                                </div>                          
                                <div class="col-lg-6">
                                    <label for="txtEmail">Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" MaxLength="50" ClientIDMode="Static" ></asp:TextBox> 
                                </div>                          
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-6">
                                    <label for="txtClaveNuevaFit">Contraseña</label>
                                    <asp:TextBox ID="txtClaveNuevaFit" runat="server" CssClass="form-control" TextMode="Password" MaxLength="20"  ClientIDMode="Static" ></asp:TextBox> 
                                </div>
                                <div class="col-lg-6">
                                    <label for="txtClaveRepetirFit">Confirme la Contraseña</label>
                                    <asp:TextBox ID="txtClaveRepetirFit" runat="server" CssClass="form-control" TextMode="Password" MaxLength="20" ClientIDMode="Static" ></asp:TextBox> 
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-6">
                                    <label for="txtCelularFit">Número de Celular</label>
                                    <asp:TextBox ID="txtCelularFit" runat="server"  TextMode="Number" CssClass="form-control"  MaxLength="20" ClientIDMode="Static" ></asp:TextBox>             
                                </div>
                                <div class="col-lg-6">
                                    <label for="txtCelular2">Otro Número de Celular</label>
                                    <asp:TextBox ID="txtCelular2" runat="server" TextMode="Number" CssClass="form-control"   MaxLength="20"  ClientIDMode="Static" ></asp:TextBox>                                    
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-lg-6">
                                    <label for="txtTelefono">Número de Teléfono</label>
                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"  TextMode="Number" MaxLength="20" ClientIDMode="Static" ></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <label for="txtFechaNacimiento" class="active">Fecha de Nacimiento</label>
                                    <asp:TextBox ID="txtFechaNacimiento" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox> 
                                    <label class="text-info" style="font-style:italic;">En Firefox el formato es 1900-05-20 (Año-Mes-Día)</label>                                   
                                </div>
                            </div>
                            
                            <div class="form-group row">
                                <div class="col-lg-6">
                                     <label for="ddlEstadoCivil">Estado Civil</label>
                                    <asp:DropDownList runat="server" ID="ddlEstadoCivil" CssClass="form-control" >
                                        <asp:ListItem Text="Soltero(a)" Value="S"  />
                                        <asp:ListItem Text="Casado(a)" Value="C"  />
                                        <asp:ListItem Text="Divorciado(a)" Value="D"  />
                                        <asp:ListItem Text="Viudo(a)" Value="V"  />
                                        <asp:ListItem Text="Otro(a)" Value="O"  />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6">
                                    <label for="">Sexo</label><br />
                                    <input  type="radio" runat="server" name="SexoOpcion" id="rdM" value="M" />
                                    <label for="rdM">Masculino</label>
                                    <input type="radio" runat="server" name="SexoOpcion" id="rdF" value="F" /> 
                                    <label for="rdF">Femenino</label>
                                </div>
                            </div>
                            <asp:PlaceHolder runat="server" ID="phRiesgosPensiones" Visible="false">
                                <div class="form-group row">
                                    <div class="col-lg-6">
                                        <label for="txtRiesgos">Riesgos</label>
                                        <asp:TextBox ID="txtRiesgos" runat="server" MaxLength="100" CssClass="form-control"  ClientIDMode="Static" ></asp:TextBox>
                                    </div>
                                    <div class="col-lg-6">
                                        <label for="txtPension">Pension</label>
                                        <asp:TextBox ID="txtPension" runat="server" MaxLength="100" CssClass="form-control"  ClientIDMode="Static" ></asp:TextBox>
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                            <div class="form-group row">
                               <div class="col-lg-6">
                                    <label for="txtEPS">EPS</label>
                                    <asp:TextBox ID="txtEPS" runat="server" CssClass="form-control" MaxLength="100"  ClientIDMode="Static" ></asp:TextBox>
                               </div>
                               <div class="col-lg-6">
                                   <label for="txtObservacionFit">¿Algún antecedente o condición física que debamos conocer?</label>
                                    <asp:TextBox runat="server" MaxLength="250" ID="txtObservacionFit" ClientIDMode="Static" CssClass="form-control"  ></asp:TextBox>
                                    
                               </div>
                            </div>       
                            <hr />
                            <h4 class="text-center text-info">Responder en Caso de Estar Embarazada</h4>                     
                            <div class="form-group row">
                                
                                <div class="col-lg-6">
                                    <label for="ddlConsentimiento">¿Tiene consentimiento medico para las clases?</label>
                                    <asp:DropDownList runat="server" ID="ddlConsentimiento" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione una Opcion" Value=""></asp:ListItem>
                                        <asp:ListItem Text="SI" Value="SI"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-6">
                                </div>
                            </div>
                            <hr />
                            <h4 class="text-center text-info">Responder en Caso de Ser Menor de Edad</h4>  
                            <div class="form-group row">
                                <div class="col-lg-4">
                                    <label for="txtNombreRepresentante">Nombre Completo del Representante</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNombreRepresentante"></asp:TextBox>
                                </div>
                                <div class="col-lg-4">
                                    <label for="txtCedulaRepresentante">Cedula del Representante</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtCedulaRepresentante" TextMode="Number"></asp:TextBox>                                        
                                </div>
                                <div class="col-lg-4">
                                    <label for="txtTelefonoRepresentante">Numero de Telefono del Representante</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefonoRepresentante" TextMode="Number"></asp:TextBox>                                        
                                </div>
                            </div>
                            <br />
                            <div class="form-group row input-field">
                                <div class="col-lg-6 text-right">
                                    <asp:Button ID="btnAddFit" CssClass="waves-effect waves-light btn" ValidationGroup="valAcademia" runat="server" OnClick="btnAddFit_Click" Text="Registrar" />
                                </div>
                                <div class="col-lg-6 text-left">
                                    <asp:Button ID="btnCancelarFit" CssClass="waves-effect waves-light btn" runat="server" PostBackUrl="~/index.aspx" Text="Cancelar" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                <!-- Fin Formulario Alumno Empresa -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <div id="footer" style="background-color:#2d2d2d;color:#d3d3d3">
        <div class="container">
            <div class="row">
            <div class="col-lg-3">
                <h4 class="text-center">Fitness Li</h4>
                <ul class="list-unstyled">
                    <li><a href="#">Nosotros</a></li>
                    <li><a href="#">Clases</a></li>
                    <li><a href="#">Inscripciones</a></li>
                    <li><a href="#">Contáctanos</a></li>
                </ul>
            </div>
            <div class="col-lg-6 text-center">
                <h4 class="text-center">Ubicanos</h4>
                <p>Calle 10a #40-60 Medellín. El Poblado. Teléfono: <a href="tel:43115472">(4) 3115472</a>, Whatsapp: <a href="tel:3052283305">3052283305</a></p>
            </div>
            <div class="col-lg-3">
                <h4 class="text-center">Siguenos</h4>
                <ul class="list-inline text-center">
                    <li><a rel="nofollow" href="#" title="Twitter"><i class="icon-lg ion-social-twitter-outline"></i></a>&nbsp;</li>
                    <li><a rel="nofollow" href="#" title="Facebook"><i class="icon-lg ion-social-facebook-outline"></i></a>&nbsp;</li>
                    <li><a rel="nofollow" href="#" title="Dribble"><i class="icon-lg ion-social-dribbble-outline"></i></a></li>
                </ul>
            </div>
        </div>
        </div>
      <div class="container">
        <p class="text-muted credit text-center">Fitness Li 2016. Todos los derechos reservados &copy;</p>
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
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
          </div>
        </div> 
      </div>
    </div>

    <!-- jQuery -->
    <script src="js/jquery.js"></script>
    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>
    <script src="js/bootstrap-datetimepicker.min.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/moment-with-locales.min.js"></script>
    <script src="js/formValidation/formValidation.min.js"></script>
    <script src="js/formValidation/bootstrap.min.js"></script>
    <script src="js/formValidation/language/es_ES.js"></script>
    
    <script type="text/javascript">
        function isFormValid() {
            var formValidation = $('form1').data('formValidation');
            //formValidation.validate();
            if (formValidation.isValid() != true) {
                return false;
            }
            return true;
        }

        function MostrarMsjModal(message, title, ccsclas) {
            var vIcoModal = document.getElementById("icoModal");
            vIcoModal.className = ccsclas;
            $('#lblMsjTitle').html(title);
            $('#lblMsjModal').html(message);
            $('#Msjmodal').modal('show');
            return true;
        }
        $(document).ready(function () {
            $('#form1')
                .formValidation({
                    framework: 'bootstrap',
                    icon: {
                        valid: 'glyphicon glyphicon-ok',
                        invalid: 'glyphicon glyphicon-remove',
                        validating: 'glyphicon glyphicon-refresh'
                    },
                    fields: {
                        txtCedulaFit: {
                            validators: {
                                notEmpty: {
                                    message: 'La cédula es requerida.'
                                },
                                regexp: {
                                    message: 'Solo puede contener números.',
                                    regexp: /\d/
                                }
                            }
                        },
                        txtCedulaEmp: {
                            validators: {
                                notEmpty: {
                                    message: 'La cédula es requerida.'
                                },
                                regexp: {
                                    message: 'Solo puede contener números.',
                                    regexp: /\d/
                                }
                            }
                        },
                        ddlPlan: {
                            validators: {
                                notEmpty: {
                                    message: 'El plan es requerido.'
                                }
                            }
                        },
                        ddlTipoEmpleado: {
                            validators: {
                                notEmpty: {
                                    message: 'La relación laboral es requerida.'
                                }
                            }
                        },
                        txtRol: {
                            validators: {
                                notEmpty: {
                                    message: 'El Rol es requerido.'
                                }
                            }
                        },
                        txtNombre: {
                            validators: {
                                notEmpty: {
                                    message: 'El nombre es requerido.'
                                }
                            }
                        },
                        txtApellido: {
                            validators: {
                                notEmpty: {
                                    message: 'El apellido es requerido.'
                                }
                            }
                        },
                        txtClaveNuevaFit: {
                            validators: {
                                notEmpty: {
                                    message: 'La clave es requerida.'
                                }
                            }
                        },
                        txtClaveRepetirFit: {
                            validators: {
                                notEmpty: {
                                    message: 'La confirmación es requerida.'
                                },
                                identical: {
                                    field: 'txtClaveNuevaFit',
                                    message: 'La clave y su confirmación deben ser iguales'
                                }
                            }
                        },
                        txtFechaNacimiento: {
                            validators: {
                                date: {
                                    format: 'DD/MM/YYYY',
                                    message: 'El formato de la fecha es invalido.'
                                }
                            }
                        },
                        txtCorreo: {
                            validators: {
                                emailAddress: {
                                    message: 'El formato del correo no es valido.'
                                }
                            }
                        },
                        txtTelefono: {
                            validators: {
                                regexp: {
                                    message: 'Solo puede contener números.',
                                    regexp: /\d/
                                }
                            }
                        },
                        txtCelularFit: {
                            validators: {
                                regexp: {
                                    message: 'Solo puede contener números.',
                                    regexp: /\d/
                                }
                            }
                        },
                        txtCelular2: {
                            validators: {
                                regexp: {
                                    message: 'Solo puede contener números.',
                                    regexp: /\d/
                                }
                            }
                        },
                        txtObservacionFit: {
                            validators: {
                                stringLength: {
                                    max: 250,
                                    message: 'El número máximo de caracteres es de 250.'
                                }
                            }
                        },
                        txtEPS: {
                            validators: {
                                notEmpty: {
                                    message: 'El EPS es requerido.'
                                }
                            }
                        }
                    }
                })
        });
    </script>
    </form>
</body>
</html>

