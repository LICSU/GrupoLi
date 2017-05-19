<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Inicio - SGSST</title>
     <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom.css" rel="stylesheet" />
</head>
<body>
    <!-- Barra de Menu -->
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

    <!-- Primera Columna -->
    <div class="cuerpo">
        <div class="col-lg-2 form-group"></div>
        <div class="col-lg-10 col-md-2 form-group">
            <div class="col-lg-2 col-sm-6 form-group">
                 <div class="dropdown">
                     <button class="dropbtn">Región Central</button>
                      <div class="dropdown-content">
                        <a href="#">Activar</a>
                        <a href="#">Validar</a>
                        <a href="#">Hacer</a>
                        <a href="#">Planear</a>
                        <a href="#">Evaluación Inicial</a>
                        <a href="#">Empresas de la Región</a>
                      </div>
                 </div>
             </div>
            <div class="col-lg-2 col-sm-6 form-group">
                 <div class="dropdown">
                     <button class="dropbtn">Región Caribe</button>
                      <div class="dropdown-content">
                        <a href="#">Activar</a>
                        <a href="#">Validar</a>
                        <a href="#">Hacer</a>
                        <a href="#">Planear</a>
                        <a href="#">Evaluación Inicial</a>
                        <a href="#">Empresas de la Región</a>
                      </div>
                 </div>
             </div>
            <div class="col-lg-2 col-sm-6 form-group">
                 <div class="dropdown">
                     <button class="dropbtn">Región Noroccidental</button>
                      <div class="dropdown-content">
                        <a href="#">Activar</a>
                        <a href="#">Validar</a>
                        <a href="#">Hacer</a>
                        <a href="#">Planear</a>
                        <a href="#">Evaluación Inicial</a>
                        <a href="#">Empresas de la Región</a>
                      </div>
                 </div>
             </div>
            <div class="col-lg-2 col-sm-6 form-group">
                 <div class="dropdown">
                     <button class="dropbtn">Región Oriental</button>
                      <div class="dropdown-content">
                        <a href="#">Activar</a>
                        <a href="#">Validar</a>
                        <a href="#">Hacer</a>
                        <a href="#">Planear</a>
                        <a href="#">Evaluación Inicial</a>
                        <a href="#">Empresas de la Región</a>
                      </div>
                 </div>
             </div>
            <div class="col-lg-2 col-sm-6 form-group">
                 <div class="dropdown">
                     <button class="dropbtn">Región Suroccidental</button>
                      <div class="dropdown-content">
                        <a href="#">Activar</a>
                        <a href="#">Validar</a>
                        <a href="#">Hacer</a>
                        <a href="#">Planear</a>
                        <a href="#">Evaluación Inicial</a>
                        <a href="#">Empresas de la Región</a>
                      </div>
                 </div>
             </div>
            <div class="col-lg-2 col-sm-12 form-group" style="padding:0px !important;">
                <div class="account-wall">
                    <form class="form-signin">
                        <div class="login">
                            <div class="wrap">
                                  <div class="mat-div">
                                    <label for="first-name" class="mat-label">Usuario</label>
                                    <input type="text" class="mat-input" id="first-name" />
                                  </div>
  
                                  <div class="mat-div">
                                    <label for="first-name" class="mat-label">Clave</label>
                                    <input type="password" class="mat-input" id="last-name" />
                                  </div><br />
                            </div>
                            <button class="btn btn-lg btn-primary btn-block" type="submit">INGRESAR</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>

        <!-- Segunda Fila -->
        <div class="col-lg-10 form-group">
            <div class="col-lg-4" style="padding-top:100px;">
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
            <div class="col-lg-4">
                <div class="panel panel-success"> 
                    <div class="panel-heading"> 
                        <h3 class="panel-title text-center">Información para analizar</h3> 
                    </div> 
                    <div class="panel-body" style="height:140px;">  

                    </div> 
                </div>
            </div>
            <div class="col-lg-2"></div>
            <div class="col-lg-4">
                <div class="panel panel-success"> 
                    <div class="panel-heading"> 
                        <h3 class="panel-title text-center">Información para analizar</h3> 
                    </div> 
                    <div class="panel-body" style="height:140px;">  

                    </div> 
                </div>
            </div>
        </div>
        <div class="col-lg-2 form-group">
            <div class="account-wall">
                <form class="form-signin">
                    <div class="login text-center" style="padding-top:15px">
                        <textarea cols="20" rows="8" style="width:95%;"></textarea>
                    </div>
                </form>
            </div>
        </div>

        <!-- Tercera Fila -->
        <div class="col-lg-12 form-group">
            
            
        </div>
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
