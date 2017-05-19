<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index2.aspx.cs" Inherits="index2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio - SGSST</title>
     <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom2.css" rel="stylesheet" />
    <!--Start of Tawk.to Script-->
	<script type="text/javascript">
	    var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
	    (function () {
	        var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];
	        s1.async = true;
	        s1.src = 'https://embed.tawk.to/58f8ce994ac4446b24a6b4e9/default';
	        s1.charset = 'UTF-8';
	        s1.setAttribute('crossorigin', '*');
	        s0.parentNode.insertBefore(s1, s0);
	    })();
	</script>
<!--End of Tawk.to Script-->
</head>
<body>

    <!-- barra -->
    <nav class="navbar navbar-default">
        <div class="container">
            <!-- Brand and toggle get grouped for better mobile display -->
            <div class="navbar-header page-scroll">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#page-top">
                    <img src="img/logo.png" alt="SGSST" />
                </a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                <ul class="nav navbar-nav navbar-right">
                    <li class="hidden">
                        <a href="#page-top"></a>
                    </li>
                    <li class="first">
                        <a href="#"></a>
                    </li>
                    <li class="page-scroll">
                        <a href="#">Nosotros</a>
                    </li>
                    <li class="page-scroll">
                        <a href="#">Acerca de</a>
                    </li>
                    <li class="page-scroll">
                        <a href="#">Contacto</a>
                    </li>
                    <li class="page-scroll">
                        <a href="#">Preguntas, Respuestas y Glosario</a>
                    </li>
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
        <!-- /.container-fluid -->
    </nav>
    <!-- fin de barra -->
    <div class="cuerpo">
        <!-- Primera Fila -->
        <div class="col-lg-2"></div>
        <div class="col-lg-7 col-xs-12">
            <asp:Table runat="server" CssClass="table table-striped">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell CssClass="text-center superior">Plazos de Cumplimiento</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="text-center" Width="140">&nbsp;</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="text-center izquierdo">Region Central</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="text-center azul">Region SurOccidental</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="text-center azul">Region Oriental</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="text-center azul">Region Caribe</asp:TableHeaderCell>
                    <asp:TableHeaderCell CssClass="text-center derecho">Region Noroccidental</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="text-center">20/10/2017</asp:TableCell>
                    <asp:TableCell CssClass="text-left">Evaluacion Inicial</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="text-center">20/10/2017</asp:TableCell>
                    <asp:TableCell CssClass="text-left">Planear</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="text-center">20/10/2017</asp:TableCell>
                    <asp:TableCell CssClass="text-left">Hacer</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="text-center">20/10/2017</asp:TableCell>
                    <asp:TableCell CssClass="text-left">Verificar</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="text-center">20/10/2017</asp:TableCell>
                    <asp:TableCell CssClass="text-left">Actuar</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                    <asp:TableCell CssClass="text-center">10%</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div class="col-lg-3 col-xs-12 col-sm-6 col-md-6">
            <div class="account-wall">
                <form class="form-signin">
                    <div class="login">
                        <div class="wrap">
                            <div class="form-group">
                                <label for="first-name" class="mat-label">Usuario</label>
                                <input type="text" class="form-control" id="first-name" />
                            </div>
  
                            <div class="form-group">
                                <label for="first-name" class="mat-label">Clave</label>
                                <input type="password" class="form-control" id="last-name" />
                            </div>
                        </div>
                        <button class="btn btn-lg btn-primary btn-block" type="submit">INGRESAR</button>
                    </div>
                </form>
            </div>
        </div>
        <!-- Fin primera fila -->

        <!-- Segunda Fila -->
        <div class="col-lg-12 arriba">
            <div class="col-lg-1"></div>
            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                <div id="captioned-gallery">
                    <figure class="slider">
                        <figure>
                            <img src="img/noticias.png" />
                            <figcaption>Noticia 1</figcaption>
                        </figure>
                        <figure>
                            <img src="img/noticias.png" />
                            <figcaption>Noticia 2</figcaption>
                        </figure>
                        <figure>
                            <img src="img/noticias.png" />
                            <figcaption>Noticia 3</figcaption>
                        </figure>
                    </figure>
                </div>
            </div>
            <div class="col-lg-2"></div>
            <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                <div class="panel panel-success"> 
                    <div class="panel-heading"> 
                        <h3 class="panel-title text-center">Información para analizar</h3> 
                    </div> 
                    <div class="panel-body" style="height:140px;">  

                    </div> 
                </div>
            </div>
        </div>
        
        <!-- fin segunda fila-->
    </div>
<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<!-- Include all compiled plugins (below), or include individual files as needed -->
<script src="js/bootstrap.min.js"></script>
<script type="text/javascript">
    $(".mat-input").focus(function () {
        $(this).parent().addClass("is-active is-completed");
    });

    $(".mat-input").focusout(function () {
        if ($(this).val() === "")
            $(this).parent().removeClass("is-completed");
        $(this).parent().removeClass("is-active");
    })
</script>
</body>
</html>
