<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Fitness Li</title>
    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/materialize.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/animate.css/3.1.1/animate.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Custom CSS -->
    <link href="css/logo-nav.css" rel="stylesheet" />
    <link href="css/estilos.css" rel="stylesheet" />
    <style>
        .redondo2 {
             margin:0 auto;
             width:260px;
             height:190px;
        }
        .fondo {
            background-color:#fff;
            height:692px;
            width:100%;
        }
        .grande {
            font-size:18px;
            color:#9c27c1;
            text-align:left;
        }
        .grande2 {
            color:#9c27c1;
            font-size:18px;
            text-align:center !important; 
        }
        .fondoTeatro {
            background:url('Images/teatro_fondo.png') no-repeat;
            height:300px;
        }
        .videoCentralClase {
            margin-left:-16px;
            padding-top:59px;
        }
        .page-scroll a {
            color:#9c27c1;
        }
        .inp1 {
            background-color:#000000 !important;
            color: #fff !important;
        }
        body {
            background-image:url("videos/video.webm");
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

    <div id="todo" style="position:relative; min-height:774px;"">
        <div id="idContenedores" runat="server" style="opacity:1; position:absolute;width:100%;">
        <div class="container" style="margin-top: 60px !important;">
            <div class="row">   
                <div class="col-lg-10 text-center" style="margin-top:13%;">
                    <h1 style="color:#8e248c;font-size:100px;">GRUPO LI</h1>
                </div>         
                <asp:PlaceHolder runat="server" ID="ph1" Visible="false">
                    <div class="col-lg-4 text-center">
                        <div runat="server" id="lblAviso" class="control-label grande2" style="opacity:1;">Intro de los programas semana
                            <br /><img src="Images/titulo.png" />
                        </div>

                        <div id="videoCentral" class="fondoTeatro" runat="server" style="opacity:1;">
                            <asp:Panel CssClass="videoCentralClase" runat="server" ID="panelCentral"></asp:Panel>
                        </div>  
                    </div>
                    <div class="col-lg-6">
                        <div class="col-lg-3" style="padding:0 !important; text-align:left; margin-top:55px;">
                            <ul class="list-unstyled">
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video1" CssClass="control-label" Text="Pole Fit" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video2" CssClass="control-label" Text="Pole Dance" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video3" CssClass="control-label" Text="Power (GAP, total)" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video4" CssClass="control-label" Text="Hatha Yoga" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video5" CssClass="control-label" Text="Plasticidad" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video6" CssClass="control-label" Text="Rumba" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video7" CssClass="control-label" Text="Insanity" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video8" CssClass="control-label" Text="Telas" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video9" CssClass="control-label" Text="Acro Yoga" OnClick="video1_Click"></asp:LinkButton></li>
                                <li style="margin-bottom: 12px;"><asp:LinkButton runat="server" ID="video10" CssClass="control-label" Text="Tae-bo" OnClick="video1_Click"></asp:LinkButton></li>
                            </ul>
                        </div>
                        <div class="col-lg-9">
                            <asp:Image runat="server" ID="imagen1" ImageUrl="~/Images/circulo2.png" Visible="false" />
                            <asp:PlaceHolder runat="server" ID="phImagen">
                                <img id="circulo" style="opacity:1;" src="~/Images/circulo2.png" runat="server" />
                            </asp:PlaceHolder>                            
                        </div>        
                    </div>
                </asp:PlaceHolder>
                <div class="col-lg-2 text-center">
                    <div>
                        <img class="profile-img text-center" src="images/logo.png" alt="Usuario" style="margin-bottom:10px;"/>
                        <asp:Label ID="lblValidado" runat="server" Text="" Visible="False"></asp:Label>
                        <span id="reauth-email" class="reauth-email"></span>
                        <div class="input-field">
                            <asp:TextBox ID="Usuario" runat="server" TextMode="SingleLine" ClientIDMode="Static"></asp:TextBox>
                            <label for="Usuario">Usuario</label>
                            <asp:HiddenField runat="server" ID="hfUsuarioActual" />
                        </div>
                        <div class="input-field">
                            <asp:TextBox ID="Clave" runat="server" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
                            <label for="Clave">Clave</label>
                            <asp:HiddenField runat="server" ID="hfClaveActual" />
                        </div>
                        <div class="form-group text-left">
                            <asp:CheckBox runat="server" ID="chkRecordar" />
                            <label for="chkRecordar">Recordar Datos</label>
                        </div>
                        <asp:Literal ID="Msj" runat="server"></asp:Literal>
                        <div class="form-group text-left">
                            <asp:LinkButton runat="server" ID="lbRecuperarClave" PostBackUrl="~/recuperarClave.aspx" Text="¿Olvidó su Contaseña?"></asp:LinkButton>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="bAceptar" runat="server" Text="Ingresar" CssClass="btn btn-lg btn-purple btn-block"  OnClick="bAceptar_Click" />
                        </div>
                        <div class="row" style="text-align:center;"><a href="registrar.aspx" class="text-center text-primary">Eres Nuevo ¡Regístrate!</a></div> 
                        </div>
                        <asp:Label ID="lblError" runat="server" Text="Label" Visible="False"></asp:Label>
                </div>

                
            </div>
        </div>
    </div>
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
    <script src="js/materialize.js"></script>
    <script>

        /*function MostrarImagen() {
            document.getElementById("circulo").style.opacity = "1";
            return true;
        }

        $(document).ready(function () {
            setTimeout(function () {
                $('#fondo').fadeOut('fast');
                document.getElementById("idVideos").style.opacity = "0";
                document.getElementById("idContenedores").style.opacity = "1";
                document.getElementById("todo").style.minHeight = "600px";
                /*$('#fondo').fadeOut('fast');
                document.getElementById("circulo").style.opacity = "1";
                document.getElementById("lblAviso").style.opacity = "1";
                document.getElementById("videoCentral").style.opacity = "1";
            }, 6000);
            //66000
        });*/
    </script>
    </form>
</body>
</html>

