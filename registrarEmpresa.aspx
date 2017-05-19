<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registrarEmpresa.aspx.cs" Inherits="registrarEmpresa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Fitness Li | Registro Empresa</title>
    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/animate.css/3.1.1/animate.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/formValidation/formValidation.min.css" />
    <!-- Custom CSS -->
    <link href="css/logo-nav.css" rel="stylesheet" />
    <link href="css/estilos.css" rel="stylesheet" />
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <style>
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
        <asp:UpdatePanel runat="server" ID="upRegistro">
            <ContentTemplate>
                <div class="row text-center form-group">
                    <h3>Registro de Nuevo Alumno Empresa</h3>
                </div>
                <!-- Formulario Alumno Empresa -->
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-10">
                            <!-- Columna 1-->
                            <div class="row form-group">
                                <div class="col-lg-6">
                                    <label for="txtCedulaEmp">Número de Cédula</label>
                                    <asp:TextBox ID="txtCedulaEmp" runat="server" TextMode="Number" clientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <label for="txtCelular">Número de Celular</label>
                                    <asp:TextBox ID="txtCelular" runat="server" ClientIDMode="Static" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <asp:PlaceHolder runat="server" ID="phTipoEmp" Visible="false">
                                    <div class="col-lg-6">
                                        <label for="txtNombre">Nombre(s)</label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" clientIDMode="Static"></asp:TextBox> 
                                    </div>
                                    <div class="col-lg-6">
                                        <label for="txtEmail">Email</label>
                                        <asp:TextBox ID="txtEmail" runat="server"  CssClass="form-control" ClientIDMode="Static" ></asp:TextBox> 
                                    </div>
                                </asp:PlaceHolder>
                            </div>
                            <div class="row form-group">
                                <div class="col-lg-6">
                                    <label for="txtClaveNueva">Contraseña</label>
                                    <asp:TextBox ID="txtClaveNuevaFit" runat="server" TextMode="Password" CssClass="form-control" ClientIDMode="Static" ></asp:TextBox>         
                                </div>
                                <div class="col-lg-6">
                                    <label for="txtClaveRepetir">Confirme la Contraseña</label>
                                    <asp:TextBox ID="txtClaveRepetirFit" runat="server" TextMode="Password" CssClass="form-control" ClientIDMode="Static" ></asp:TextBox>    
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-lg-12">
                                    <label for="txtObservacion">¿Algún antecedente o condición física que debamos conocer?</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtObservacion" Rows="4" ClientIDMode="Static" CssClass="form-control"  ></asp:TextBox>                                
                                </div>
                            </div>                            
                            <div class="row form-group">
                                <div class="col-lg-6 text-right">
                                    <asp:Button ID="btnRegistrarEmmpresa" CssClass="waves-effect waves-light btn" runat="server" OnClick="btnRegistrarEmmpresa_Click" Text="Registrar" />
                                </div>
                                <div class="col-lg-6 text-left">
                                    <asp:Button ID="btnCancelar" CssClass="waves-effect waves-light btn" runat="server" PostBackUrl="~/index.aspx" Text="Cancelar" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-1"></div>
                    </div>
                <!-- Fin Formulario Alumno Empresa -->
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRegistrarEmmpresa" />
            </Triggers>
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
                        txtNombre: {
                            validators: {
                                notEmpty: {
                                    message: 'El nombre es requerido.'
                                }
                            }
                        },
                        txtCelular: {
                            validators: {
                                notEmpty: {
                                    message: 'El teléfono es requerido.'
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
                        txtEmail: {
                            validators: {
                                emailAddress: {
                                    message: 'El formato del correo no es valido.'
                                }
                            }
                        },
                        txtObservacion: {
                            validators: {
                                stringLength: {
                                    max: 250,
                                    message: 'El número máximo de caracteres es de 250.'
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

