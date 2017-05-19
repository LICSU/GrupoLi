<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registrar.aspx.cs" Inherits="registrar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Fitness Li | Registro</title>
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
    
    <div class="container" style="margin-top:100px; min-height:600px;">
        <asp:UpdatePanel runat="server" ID="upRegistro" UpdateMode="Always">
            <ContentTemplate>
                <div class="row text-center">
                    <h3>Registro de Nuevo Alumno</h3>
                </div>
                <div class="row">
                    <div class="col-lg-3"></div>
                    <div class="col-lg-6">
                        <div class="row form-group">
                            <label for="dplUnidad">Seleccione la unidad de negocio</label><br />
                            <asp:DropDownList runat="server" ID="dplUnidad" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dplUnidad_SelectedIndexChanged">
                            </asp:DropDownList>      
                        </div> 
                        <asp:PlaceHolder runat="server" Visible="false" ID="phTipo">
                             <div class="row form-group">
                                <label for="ddlTipoEmpleado">Tipo de Empleado</label><br />
                                <asp:DropDownList runat="server" ID="ddlTipoEmpleado" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoEmpleado_SelectedIndexChanged" >
                                    <asp:ListItem Text="Seleccione el Tipo de Empleado" Value=""  />
                                    <asp:ListItem Text="Empleado" Value="Empleado"  />
                                    <asp:ListItem Text="Contratado/Temporal" Value="Contratado"  />
                                    <asp:ListItem Text="Otro" Value="Otro"  />
                                </asp:DropDownList>
                             </div> 
                         </asp:PlaceHolder>             
                    </div>
                    <div class="col-lg-3"></div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dplUnidad" />
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
    </form>
</body>
</html>

