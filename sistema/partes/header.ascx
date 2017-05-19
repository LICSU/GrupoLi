<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="sistema_partes_header" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Fitness Li | Sistema</title>
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="../../css/materialize.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/animate.css/3.1.1/animate.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <link href="http://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="../../css/logo-nav.css" rel="stylesheet" />
    <link href="../../css/estilos.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="../../css/metisMenu.css" rel="stylesheet" />

    <!-- Custom CSS -->
    <link href="../../css/sb-admin-2.css" rel="stylesheet" />

    <!-- Morris Charts CSS -->
    <link href="../../css/morris.css" rel="stylesheet" />

    <!-- Custom Fonts -->
    <link href="../../css/font-awesome.css" rel="stylesheet" type="text/css" />

    <style>
        ul li a img {
            max-width: 50px;
        }
        .perfil {
            max-height: 100px;
            border:dotted 1px #8e248c;
        }
    </style>
</head>
<body onload="nobackbutton();" style="padding-top: 0px;">
    <asp:PlaceHolder runat="server" ID="phNomaster" Visible="false">
        <div id="wrapper">
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phMaster" Visible="false">
        <div id="Div1">
    </asp:PlaceHolder>
        <!-- Navigation -->
        <nav class="navbar navbar-default navbar-static-top" role="navigation" style="margin-bottom: 0; min-height: 98px; position:fixed;">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="../../inicio.aspx"><img src="../../images/logo.png" alt="Fitness Li"/></a>
            </div>
            <!-- /.navbar-header -->            
            <ul class="nav navbar-top-links navbar-right" style="float:right;" > 
                <!-- /.dropdown -->
                <asp:PlaceHolder runat="server" ID="phMenu2" Visible="true">
                    <li id="listMovil2"><a href="/sistema/ReservaAlumnoLicsu.aspx"><i id="i1" class="fa fa-user fa-fw"></i> Reservar</a> </li>
                </asp:PlaceHolder>
                    <li id="listMovil" class="dropdown">                  
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                            <i class="fa fa fa-medkit fa-fw"></i>Opciones &nbsp;<i class="fa fa-caret-down"></i>
                        </a>  
                        <ul class="dropdown-menu dropdown-user">
                            <li><a href="/sistema/medidas.aspx"><i id="icono1" class="fa fa-user fa-fw"></i> Medidas</a> </li>
                            <li class="divider"></li>
                            <li><a href="#"><i class="fa fa-user fa-fw"></i>Estadisticas</a></li>
                            <li class="divider"></li>
                            <li><a href="#"><i class="fa fa-user fa-fw"></i>ElectroCardiograma</a></li>
                            <li class="divider"></li>
                            <li><a href="#"><i class="fa fa-user fa-fw"></i>Glucosa</a></li>
                            <li class="divider"></li>
                            <li><a href="#"><i class="fa fa-user fa-fw"></i>Tigliceridos y Colesterol</a></li>
                            <li class="divider"></li>
                            <li><a href="#"><i class="fa fa-user fa-fw"></i>Tension Arterial y Pulso</a></li>
                            <li class="divider"></li>
                            <li><a href="#"><i class="fa fa-user fa-fw"></i>Obligaciones Adicionales</a></li>
                            <li class="divider"></li>
                            <li><a href="/sistema/Perfil.aspx"><i class="fa fa-user fa-fw"></i>Perfil</a></li>
                            <li class="divider"></li>
                            <li><a href="/sistema/Salir.aspx"><i class="fa fa-sign-out fa-fw"></i> Cerrar Sesión</a></li>
                        </ul>                
                    </li>
                    <ul id="listPC" class="dropdown text-center"  >
                        <asp:PlaceHolder runat="server" ID="phMenu3" Visible="true">
                            <li>
                                <a href="/sistema/ReservaAlumnoLicsu.aspx"><img src="../../images/reservar.png" />
                                    <div class="form-group" style="margin-top:-26px;">Reservar</div>
                                </a>
                            </li>
                        </asp:PlaceHolder>
                        <li>
                            <a href="/sistema/medidas.aspx"><img src="../../images/medidas.png" />
                                <div class="form-group" style="margin-top:-26px;">Medidas</div>
                            </a>
                        </li>
                        <li class="divider"></li>
                        <li><a href="#"><img src="../../images/menu.png" /><div class="form-group" style="margin-top:-26px;">Medidas</div></a></li>
                        <li class="divider"></li>
                        <li><a href="#"><img src="../../images/electro.png" /><div class="form-group" style="margin-top:-26px;">ElectroCardiograma</div></a></li>
                        <li class="divider"></li>
                        <li><a href="#"><img src="../../images/glucosa.png" /><div class="form-group" style="margin-top:-26px;">Glucosa</div></a></li>
                        <li class="divider"></li>
                        <li><a href="#"><img src="../../images/colesterol.png" /><div class="form-group" style="margin-top:-26px;">Colesterol</div></a></li>
                        <li class="divider"></li>
                        <li><a href="#"><img src="../../images/tension.png" /><div class="form-group" style="margin-top:-26px;">Tension</div></a></li>
                        <li class="divider"></li>
                        <li><a href="#"><img src="../../images/obligaciones.png" /><div class="form-group" style="margin-top:-26px;">Obligaciones</div></a></li>
                        <li class="divider"></li>
                        <li class="dropdown">
                            <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                                <img src="../../images/opciones.png" />
                                <div class="form-group" style="margin-top:-26px;">Opciones</div>
                            </a>
                            <ul class="dropdown-menu dropdown-user">
                                <li><a href="/sistema/Perfil.aspx"><i class="fa fa-user fa-fw"></i> Perfil</a></li>
                                <li class="divider"></li>
                                <li><a href="/sistema/Salir.aspx"><i class="fa fa-sign-out fa-fw"></i> Cerrar Sesión</a></li>
                            </ul>
                            <!-- /.dropdown-user -->
                        </li>  
                        <li>
                            <asp:Image CssClass="perfil" ImageUrl="/sistema/fotos/1000084470.png" runat="server" ID="imgPerfil"/>
                        </li>             
                    </ul>
                    
                    
                
                <!-- /.dropdown -->
            </ul>
            <!-- /.navbar-top-links -->
            <asp:PlaceHolder runat="server" ID="phMenu">
                <div class="navbar-default sidebar" role="navigation">
                    <div class="sidebar-nav navbar-collapse" aria-expanded="false" style="height: 1px; width:100%;">
                        <!-- Menu Lateral -->
                        <asp:Repeater ID="rptMenuLateral" runat="server" OnItemDataBound="rptMenuLateral_ItemDataBound">
                            <HeaderTemplate>
                                <ul class="nav" id="side-menu" style="line-height:30px !important;">	            
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li><a href='<%# Eval("MenuURL") %>'><i class="fa fa fa-circle fa-fw "></i> <%# Eval("MenuDescripcion") %></a><asp:Literal ID="ltrlSubMenuLateral" runat="server"></asp:Literal></li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                        <!-- Fin Menu Lateral -->                    
                    </div>
                    <!-- /.sidebar-collapse -->
                </div>
            </asp:PlaceHolder>
            <!-- /.navbar-static-side -->
        </nav>
