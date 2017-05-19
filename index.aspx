<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>
<!DOCTYPE html>
<html>
  <head>
    <title>Grupo LICSU</title>
    <meta name="keywords" content="" />
	<meta name="description" content="" />
    <!--
    Powerful Template
    http://www.templatemo.com/tm-390-powerful
    -->
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Bootstrap -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto+Slab" rel="stylesheet">
    <link href="css/circle.css" rel="stylesheet">
    <link rel="stylesheet" href="css/nivo-slider.css">
    <script src="js/modernizr.custom.js"></script>
    <script src="js/jquery-1.10.2.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.cycle2.min.js"></script>
    <script src="js/jquery.cycle2.carousel.min.js"></script>
    <script src="js/jquery.nivo.slider.pack.js"></script>
    <script>$.fn.cycle.defaults.autoSelector = '.slideshow';</script>
    <script src="js/jquery.cookie.js"></script>
    <script src="js/functions.js"></script>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
      <style>
          .cargo {
            font-size:12px;
          }
          .trab {
            font-weight:bold;
          }
          .footer {
            padding-top:100px;
          }
      </style>
  </head>
  <body id="cuerpo-principal">
    <header>


      <div id="templatemo_top" class="mainTop mWrapper menu_bar_lg">
          <div class="container">
            <div class="row">
              <div class="col-sm-4 col-md-4">
                <div class="logo">
                  <center>
                  <a href="index.aspx"><img class="logo-size" src="images/logo.jpg" alt="Powerful Theme" style="width: 60%"></a>
                  </center>
                </div>
              </div>
              <div class="col-sm-8 col-md-8">
                <nav class="mainMenu">
                  <ul class="nav">
                    <li><a href="#templatemo_top">Inicio</a></li>
                    <li><a href="#templatemo_services">Productos</a></li>
                    <li><a href="#templatemo_about">Nosotros</a></li>
                    <li><a href="#templatemo_contact">Contacto</a></li>
                  </ul>
                </nav>
              </div>
            </div>
          </div>
      </div> <!-- e/o mainTop -->

      <div class="col-sm-12 col-ms-12 home-end">
        <div class ="row">
      <div class="col-sm-12 col-md-12 col-lg-9 text-center" style="padding-left: 4%; padding-right: 4%;">
      	    <div id="slider" class="nivoSlider">
                <a class="boton" href="#togg"><img src="images/slider/img_2.jpg" alt="slide 2" /></a>
                <a class="boton1" href="#togg"><img src="images/slider/img_3.jpg" alt="slide 3" /></a>
                <a class="boton2" href="#togg"><img src="images/slider/img_4.jpg" alt="slide 4" /></a>
                <a class="boton3" href="#togg"><img src="images/slider/img_5.jpg" alt="slide 5" /></a>
                <a class="boton4" href="#togg"><img src="images/slider/img_6.jpg" alt="slide 6" /></a>
    	     </div>
    </div>
    <div class="col-sm-12 col-md-12 col-lg-3">

      <center>
      <div class="login" style="width: 100%; height: 415px; background-color: #fff; box-shadow: 2px 3px 6px #aaa; max-width: 415px; margin-top: 15px;">

        <form role="form" id="form1" runat="server">
          <p style="padding-top: 30px; color: #00ACE9; text-align: center; font-size: 18px;">Iniciar Sesión</p>
          <div class="form-group">
            <p style="padding-top: 20px; color: #00ACE9; text-align: center">Nombre de usuario</p>
            <asp:TextBox ID="Usuario" CssClass="usuario" runat="server" ClientIDMode="Static"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hfUsuarioActual" />
          </div>
          <div class="form-group">
            <p style="color: #00ACE9; text-align: center">Contraseña</p>
            <asp:TextBox ID="Clave" runat="server" CssClass="clave" TextMode="Password" ClientIDMode="Static"></asp:TextBox>
            <asp:HiddenField runat="server" ID="hfClaveActual" />
          </div>
          <a href="#"><p class="formulario-login" style="color: #00ACE9; text-align: center; padding-top: 15px; padding-bottom: 10px;">Recuperar contraseña</p></a>
          <a href="#"><p class="formulario-login" style="color: #00ACE9; text-align: center; padding-bottom: 15px;">Registrate</p></a>
          <div>
              <asp:Button ID="bAceptar" runat="server" Text="Entrar" CssClass="btn btn-primary"  OnClick="bAceptar_Click" />
           </div>
        </form>

      </div>

      </center>
    </div>
  </div>
    </div>
  </header>

    <div class="section5">
      <div class="container">
        <div class="row">
          <div id="product-list" class="col-md-12">
            <div class="row form-group">
              <div class="col-xs-6 col-sm-6 col-md-2">
                <div class="blok text-center">
                  <div class="hexagon-a" id="templatemo_services">
                    <a class="hlinktop" href="#">
                      <div class="hexa-a">
                        <div class="hcontainer-a">
                          <div class="vertical-align-a">
                            <img src="images/products/Logo_MedicLi_On.png" style="position: absolute; display:block;margin:0 auto 0 auto;"></img>
                            <img class="fade" src="images/products/Logo_MedicLi.png" style="opacity: 1; position: absolute; display:block;margin:0 auto 0 auto;"></img>
                          </div>
                        </div>
                      </div>
                    </a>
                  </div>
                  <div class="hexagon">
                    <a class="hlinkbott" href="#">
                        <div class="hcontainer">
                          <div class="vertical-align">
                            <span class="texts"></span>
                          </div>
                        </div>
                    </a>
                  </div>
                  <h4 style ="color: #139F78;">MEDIC LI</h4>
                  <p class="text-justify" style="min-height: 120px;">Sistema inteligente que trabaja con sensores para evaluar condiciones biomédicas.</p>
                  <div class="list-group">
                      <a href="#producto_desp" class="list-group-item boton">Somos Uno</a>
                      <a href="#producto_desp11" class="list-group-item boton11">Heart + Somos Uno</a>
                      <a href="#producto_desp12" class="list-group-item boton12">Heart</a>
                      <a href="#producto_desp13" class="list-group-item boton13">Heart + Somos Uno + Metabolismo</a>
                      <a href="#producto_desp14" class="list-group-item boton14">Metabolismo</a>
                      <a href="#producto_desp15" class="list-group-item boton15">Estress como riesgo Psicosocial</a>
                  </div>
                </div>
              </div>
              <div class="col-xs-6 col-sm-6 col-md-2">
                <div class="blok text-center">
                  <div class="hexagon-a">
                    <a class="hlinktop" href="#">
                      <div class="hexa-a">
                        <div class="hcontainer-a">
                          <div class="vertical-align-a">
                            <img src="images/products/Logo_SGSSTC_On.png" style="position: absolute; display:block;margin:0 auto 0 auto;"></img>
                            <img class="fade" src="images/products/Logo_SGSSTC.png" style="opacity: 1; position: absolute; display:block;margin:0 auto 0 auto;"></img>
                          </div>
                        </div>
                      </div>
                    </a>
                  </div>
                  <div class="hexagon">
                    <a class="hlinkbott" href="#">
                        <div class="hcontainer">
                          <div class="vertical-align">
                            <span class="texts"></span>
                          </div>
                        </div>
                    </a>
                  </div>
                  <h4 style ="color: #D65A0E;">SGSSTC</h4>
                  <p class="text-justify" style="min-height: 120px;">Modelo integrado que usa software y soporte humano para gestionar las necesidades de seguridad y salud en el trabajo.</p>
                    <div class="list-group">
                        <a href="#producto_desp1" class="list-group-item boton1">SGSSTC</a>
                    </div>
                    <!--
                    <ul class="text-left">
                      <li><a class="boton1" href="#togg">SGSSTC</a></li>
                  </ul>-->
                </div>
              </div>
            <div class="col-xs-6 col-sm-6 col-md-2">
              <div class="blok text-center">

                <div class="hexagon-a">
                  <a class="hlinktop">
                    <div class="hexa-a">
                      <div class="hcontainer-a">
                        <div class="vertical-align-a">
                          <img src="images/products/Logos_Recognition_On.png" style="position: absolute; display:block;margin:0 auto 0 auto;"></img>
                          <img class="fade" src="images/products/Logos_Recognition.png" style="opacity: 1; position: absolute; display:block;margin:0 auto 0 auto;"></img>
                        </div>
                      </div>
                    </div>
                  </a>
                </div>
                <div class="hexagon">
                  <a class="hlinkbott" href="#">
                      <div class="hcontainer">
                        <div class="vertical-align">
                          <span class="texts"></span>
                        </div>
                      </div>
                  </a>
                </div>

                <h4 style ="color: #1273C9;">RECOGNITION</h4>
                <p class="text-justify" style="min-height: 120px;">Motor de reconocimiento facial aplicado a múltiples modelos de negocios.</p>
                <div class="list-group">
                    <a href="#producto_desp2" class="list-group-item boton2">Recognition Hotel</a>
                    <a href="#producto_desp21" class="list-group-item boton21">Transporte público individual</a>
                    <a href="#producto_desp22" class="list-group-item boton22">Transporte público masivo</a>
                    <a href="#producto_desp23" class="list-group-item boton23">Big-bro</a>
                    <a href="#producto_desp24" class="list-group-item boton24">Ciudad Segura</a>
                    <a href="#producto_desp25" class="list-group-item boton25">Real Li</a>
                </div>
                  <!--
                <ul class="text-left">
                    <li><a class="boton2" href="#togg">Recognition Hotel</a></li>
                    <li>Transporte Seguro
                        <ul>
                            <li><a class="boton21" href="#togg">Transporte público individual</a></li>
                            <li><a class="boton22" href="#togg">Transporte público masivo</a></li>
                        </ul>
                    </li>
                    <li><a class="boton23" href="#togg">Big-bro</a></li>
                    <li><a class="boton24" href="#togg">Ciudad Segura</a></li>
                    <li><a class="boton25" href="#togg">Real Li</a></li>
                </ul>-->
              </div>
            </div>
            <div class="col-xs-6 col-sm-6 col-md-2">
              <div class="blok text-center">

                <div class="hexagon-a">
                  <a class="hlinktop" href="#">
                    <div class="hexa-a">
                      <div class="hcontainer-a">
                        <div class="vertical-align-a">
                          <img src="images/products/Logo_Fitness_On.png" style="position: absolute; display:block;margin:0 auto 0 auto;"></img>
                          <img class="fade" src="images/products/Logo_Fitness.png" style="opacity: 1; position: absolute; display:block;margin:0 auto 0 auto;"></img>
                        </div>
                      </div>
                    </div>
                  </a>
                </div>
                <div class="hexagon">
                  <a class="hlinkbott" href="#">
                      <div class="hcontainer">
                        <div class="vertical-align">
                          <span class="texts"></span>
                        </div>
                      </div>
                  </a>
                </div>

                <h4 style ="color: #D144F1;">FITNESS LI</h4>
                <p class="text-justify" style="min-height: 120px;">Sevicio para gestionar la participación de personas en programas de actividad física.</p>
                  <div class="list-group">
                      <a href="#producto_desp3" class="list-group-item boton3">Empresas</a>
                      <a href="#" class="list-group-item boton31">Academias</a>
                  </div>
              </div>
            </div>

            <div class="col-xs-6 col-sm-6 col-md-2">
              <div class="blok text-center">
                <div class="hexagon-a">
                  <a class="hlinktop" href="#">
                    <div class="hexa-a">
                      <div class="hcontainer-a">
                        <div class="vertical-align-a">
                          <img src="images/products/Logo_Power_On.png" style="position: absolute; display:block;margin:0 auto 0 auto;"></img>
                          <img class="fade" src="images/products/Logo_Power.png" style="opacity: 1; position: absolute; display:block;margin:0 auto 0 auto;"></img>
                        </div>
                      </div>
                    </div>
                  </a>
                </div>
                <div class="hexagon">
                  <a class="hlinkbott" href="#">
                      <div class="hcontainer">
                        <div class="vertical-align">
                          <span class="texts"></span>
                        </div>
                      </div>
                  </a>
                </div>
                <h4 style ="color: #750505">POWER LI</h4>
                <p class="text-justify" style="min-height: 120px;">Sistema de última generación en entrenamiento físico, que integra la realidad y ambientes virtuales para crear escenarios que activan todos los sentidos.</p>
                  <div class="list-group">
                      <a href="#producto_desp4" class="list-group-item boton4">Power Li</a>
                  </div>
                  <!--
                  <ul class="text-left">
                    <li><a class="boton4" href="#togg">Power Li</a></li>
                </ul>-->
              </div>
            </div>

          </div>
          </div>
        </div>
      </div>
    </div> 
    
    <!-- e/o section1 -->

    <div class="footer" id="producto_desp">
      <div class="container">
        <div class="row">            
          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/1.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
                <div class="col-lg-12 text-center">
                    **Para leer la información completa por favor da clic sobre cada titulo.**
                </div>
                <div class="col-md-12">
                    <div class="info2">
                      <a class="buton" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                      <div class="desplegable">
                      <p>Sistema diseñado para consolidar en un solo indicador de manera cuantitativa y visual (AVATAR) el riesgo de enfermedad de la organización.
    Esta herramienta se orienta a la cobertura de todo el recurso humano de la empresa, permitiendo abordar el riesgo de manera individual (con los datos de cada persona) en primera instancia, hasta el abordaje global de la misma (por departamentos, gerencias, etc).
    .</p>
                      </div>
                    </div>
                </div>

                <div class="col-md-12">
                  <div class="info3">
                    <a class="buton1" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona?</h4></a>
                    <div class="desplegable1">
                    <p>Los sujetos se detienen por 1'20" segundos ante un grupo de sensores que recrean el volumen corporal, adquiriendo su peso, talla, porcentaje de grasa corporal, con especial atención a 4 áreas (Cuello, Pecho, Cintura y Cadera).</p>
                    </div>
                  </div>
                </div>

                <div class="col-md-12">
                  <div class="info3">
                    <a class="buton2" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque "Somos Uno"?</h4></a>
                    <div class="desplegable2">
                        <ul>
                            <li>Usa un modelo visual (Avatar), lo cual lo hace atractivo y entretenido, con facilidad de participación.</li>
                            <li>Permite la elaboración de un “personaje” unificado a través de la agregación de las medidas de todos los participantes, creando una referencia única del grupo que se podría entender como la condición general de la empresa, lo cual facilitará además la comprensión del riesgo individual </li>
                            <li>Este “personaje” unificado,  aunque habla de todos, no personaliza la condición en ningún individuo, facilitando la comunicación entre empresa y recurso humano trabajador, en la búsqueda, de esquemas para atender los resultados.</li>
                            <li>Identifica en cada persona los factores de riesgo básicos, conocidos a nivel mundial por la comunidad científica, como predictores y/o desencadenantes de enfermedades crónicas no transmisibles</li>
                            <li>Es un recurso No invasivo (sin hacer contacto con las personas).</li>
                            <li>Realidad aumentada, combina elementos del entorno físico o realidad con desarrollos de análisis médico y estadístico en una forma que no es presuntuosa y en cambio, en tiempo real entrega respuestas para la salud de todos.</li>
                            <li>Esquema del servicio 100% agendable, facilitando la separación de tiempo justa y aplicando no más de 5 minutos del tiempo de trabajo de cada empleado.</li>
                            <li>Metodología de ejecución divertida y retadora. La versión usada para este servicio se enmarca en competencia entre géneros.</li>
                        </ul>
                    </div>
                  </div>
                </div>

                <div class="col-md-12">
                  <div class="info3">
                    <a class="buton3" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué recibe la empresa?</h4></a>
                    <div class="desplegable3">
                    <ul>
                        <li>Resultado Global de Estado de Salud y riesgo de enfermar de sus empleados</li>
                        <li>Resultados según el esquema organizativo de la empresa, permitiendo la visualización de los departamentos con mayores riesgos de enfermar.</li>
                        <li>Listado de empleados ordenado según nivel de riesgo</li>
                        <li>Acceso al servicio online
                            <ol>
                               <li>Descargar listados y resultados</li> 
                               <li>Conocer el nivel de confianza de sus datos en razón del # de empleados que efectivamente asistieron.</li> 
                            </ol>
                        </li>
                        <li>Row Data sobre antropometría promedio, permitiendo visualizar prioridad de rango de tallas en uniformes. </li>
                        <li>Row Data por individuo sobre antropometría completa. </li>
                    </ul>
                    </div>
                  </div>
                </div>

                <div class="col-md-12">
                  <div class="info3">
                    <a class="buton4" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">- ¿Qué reciben los empleados?</h4></a>
                    <div class="desplegable4">
                        <p>Una visualización de su estado general de salud.</p>
                        <ul>
                            <li>Análisis de peso, talla e IMC en relación con marcadores deseables</li>
                            <li>Circunferencia de Cuello y su posición como indicador de riesgo.</li>
                            <li>Análisis de Cintura/Cadera y su posición como indicador de riesgo</li>
                            <li>Análisis de circunferencia de Cintura como indicador de riesgo.</li>
                            <li>Avatar</li>
                        </ul>
                    </div>
                  </div>
                </div>

                <div class="col-md-12">
                  <div class="info3">
                    <a class="buton5" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">- Costos </h4></a>
                    <div class="desplegable5">
                    <p>El servicio se desarrolla con una tarifa x hora. El # de personas que se pueden evaluar por hora es de 20.<br />
                    El valor incluye el acceso permanente de la administración y los empleados a sus cuentas para seguimiento y control.<br />
                    <strong>Nota</strong> Las empresas que requieran más de 5 horas deben verificar tabla para reconocer el descuento.
                    </p>
                    <table class="table">
                            <tr>
                                <th colspan="4" class="text-center">Tarifas/Vistas<br /># de empleados a ser atendidos.</th>
                            </tr>
                            <tr>
                                <th>1-100</th>
                                <th>101-160</th>
                                <th>161-320</th>
                                <th>+320</th>
                            </tr>
                            <tr>
                                <th>70.000/Hora</th>
                                <th>60.000/Hora</th>
                                <th>50.000/Hora</th>
                                <th>45.000/Hora</th>
                            </tr>
                            <tr>
                                <th colspan="4" class="text-center">Descripción de los valores/empleado según el rango</th>
                            </tr>
                            <tr>
                                <th>3.500</th>
                                <th>3.000</th>
                                <th>2.500</th>
                                <th>2.250</th>
                            </tr>
                        </table>
                    </div>                    
                  </div>
                </div>

              <div class="col-md-12 text-center">
                  <a class="btn btn-product-1 cerrar" href="#product-list" style="text-align:center;">Cerrar</a>
              </div>

          </div>

        </div>
      </div>
    </div>
    
    <div class="footer" id="producto_desp11">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/1.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
            <div class="col-lg-12 text-center">
                **Para leer la información completa por favor da clic sobre cada titulo.**
            </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                        <p>
                            Sistema que se enfoca de manera más detallada en el músculo cardíaco, manteniendo las lecturas de la secuencia de volumen corporal (“Somos Uno”).
                            Sin pretender reemplazar la visita al Médico especialista, permite una evaluación preventiva destinada a detectar  
                            factores de riesgo de enfermedades crónicas no transmisibles, por ejemplo patologías cardiovasculares asintomáticas en su inicio  
                            (ej. hipertensión arterial, etc.). Cuando esta secuencia la unimos a los valores de la secuencia “Somos Uno” permite perfilar 
                            de manera más certera el riesgo de enfermedades metabólicas (ej. exceso de peso, cintura aumentada de tamaño, etc.).
                            El propósito de esta secuencia conjunta (Heart + “Somos Uno”) es disminuir el riesgo de enfermedad, y prevenir el desarrollo de eventos, 
                            tales como: infarto al miocardio, infarto o hemorragia cerebral, etc.
                            El conocimiento de la existencia de riesgos actuales, facilita la implementación, participación y ejecución de planes preventivos, 
                            elaborados y establecidos en la empresa.
                        </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona?</h4></a>
                <div class="desplegable1">
                    <p>
                        Los sujetos se detienen ante un grupo de sensores que recrean el volumen corporal, miden pulso y ritmo cardíaco
                    </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque "Heart" + "Somos Uno"?</h4></a>
                <div class="desplegable2">
                    <p>
                        Logra una visualización ampliada del estado general de salud de la población trabajadora de la empresa                        
                    </p>
                    <ul>
                        <li>Siendo las Enfermedades Cardiovasculares la primera causa de muerte a nivel mundial, provee no solo información de  los factores de riesgo básicos, 
                            pero además evalúa el ritmo y la función cardiaca a través de un registro de la actividad eléctrica del corazón ofreciendo una visión básica del 
                            estado de su sistema cardiovascular</li>
                        <li>Hace uso de la identificación de los factores de riesgo básicos logrados en el proceso de (“Somos Uno”) y permite desarrollar planes individuales y 
                            generales de intervención sobre los factores de riesgo modificables.</li>
                        <li>Permite la participación activa de empleados y empleadores en la reducción de riesgos., 
                            con la implementación de planes de prevención mejor orientados a las necesidades organizativas.</li>
                        <li>Facilita la comprensión de la importancia de establecer y mantener estilos de vida saludables, 
                            actividad física y bienestar psicosocial.</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué recibe la empresa?</h4></a>
                <div class="desplegable3">
                    <ul>
                        <li>Resultado Global de Estado de Salud y riesgo básico de enfermar de sus empleados</li>
                        <li>Resultado Global de la valoración y despistaje de patologías básicas cardíacas de sus empleados</li>
                        <li>Resultados según el esquema organizativo de la empresa, permitiendo la visualización de los departamentos con mayores riesgos de enfermar.</li>
                        <li>Listado de empleados ordenado según nivel de riesgo</li>
                        <li>Acceso al servicio online
                            <ol>
                                <li>Descargar listados y resultados</li>
                                <li>Conocer el nivel de confianza de sus datos en razón del # de empleados que efectivamente asistieron. </li>
                            </ol>
                        </li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué reciben los empleados?</h4></a>
                <div class="desplegable4">
                    <p>Una visualización de su estado general de salud y riesgo cardiovascular básico:</p>
                    <ul>
                        <li>Análisis de peso, talla e IMC en relación con marcadores deseables.</li>
                        <li>Circunferencia de Cuello y su posición como indicador de riesgo.</li>
                        <li>Análisis de Cintura/Cadera y su posición como indicador de riesgo</li>
                        <li>Análisis de circunferencia de Cintura como indicador de riesgo.</li>
                        <li>Valor de su tensión arterial e interpretación de riesgo</li>
                        <li>Estado general de su funcionamiento cardíaco e interpretación de riesgo (probabilidad de afectación si toma medicamentos, ataque cardiaco presente o pasado, etc.)</li>
                        <li>Avatar</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos </h4></a>
                <div class="desplegable5">
                <p>
                    *** Por definir de acuerdo a las condiciones de ejecución y convenio con la empresa.
                </p>

                </div>
              </div>
            </div>

              <div class="col-md-12 text-center">
                  <a class="btn btn-product-1 cerrar11" href="#product-list" style="text-align:center;">Cerrar</a>
              </div>

          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp12">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/1.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
             <div class="col-lg-12 text-center">
                **Para leer la información completa por favor da clic sobre cada titulo.**
            </div>

            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                        <p>
                            Sistema enfocado más detalladamente en el músculo cardíaco, con especial utilidad en aquellas personas con antecedentes familiares de hipertensión 
                            arterial y enfermedades cardiovasculares, o que presenten algunas de estas enfermedades.  El objetivo principal es descartar problemas y patologías 
                            básicas de corazón, logrando generar un conocimiento más profundo de la existencia de riesgos actuales y capacidad de esfuerzo, incluyendo la participación en actividades deportivas.
                            Busca ser un medio preventivo, e informativo de la actividad cardíaca y no reemplaza la visita al Médico especialista.</p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona?</h4></a>
                <div class="desplegable1">
                    <p>
                        Se adhieren los electrodos al sujeto de análisis y este debe interactuar con el sistema que ha sido diseñado para este efecto por un período que puede oscilar entre 2 y 4 minutos.
                    </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque "Heart"?</h4></a>
                <div class="desplegable2">
                    <p>
                        Los trastornos cardiovasculares son la primera causa de mortalidad a nivel mundial,  los cuales son asintomáticos hasta que ocurre una eventualidad, este sistema permite                      
                    </p>
                    <ul>
                        <li>Identificar algún trastorno como hipertensión, arritmias, alteración del tamaño del corazón, etc.</li>
                        <li>Identificar en forma personalizada trastornos básicos cardiovasculares.</li>
                        <li>Desarrollar un plan individual de intervención de ser necesario, o prevención,  sobre aquellos factores de riesgo modificables</li>
                        <li>Reducir el riesgo de presentar un evento cardiovascular.</li>
                        <li>Promover estilos de vida saludables, como alimentación saludable, actividad física y bienestar psicosocial.</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué recibe la empresa?</h4></a>
                <div class="desplegable3">
                    <ul>
                        <li>Resultado Global del Estado cardiovascular básico de sus empleados</li>
                        <li>Resultados según el esquema organizativo de la empresa, permitiendo la visualización de los departamentos con mayores problemas o riesgos.</li>
                        <li>Listado de empleados ordenado según grado de afectación o de riesgo.</li>
                        <li>Acceso al servicio online
                            <ol>
                                <li>Descargar listados y resultados</li>
                                <li>Conocer el nivel de confianza de sus datos en razón del # de empleados que efectivamente asistieron. </li>
                            </ol>
                        </li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué reciben los empleados?</h4></a>
                <div class="desplegable4">
                    <ul>
                        <li>Información de forma,  ritmo, y frecuencia cardíaca.</li>
                        <li>Conocer si  presenta algún trastorno como hipertensión, arritmias, alteración del tamaño del corazón, etc.</li>
                        <li>Un plan personalizado de intervención preventiva o curativa según amerite, sobre aquellos factores de riesgo modificables.</li>
                        <li>Reducir el riesgo de presentar un evento cardiovascular.</li>
                        <li>Una visión personalizada, de las medidas a tomar para establecer un estilo de vida saludable, actividad física y bienestar psicosocial.</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos </h4></a>
                <div class="desplegable5">
                <p>
                    *** Por definir de acuerdo a las condiciones de ejecución y convenio con la empresa.
                </p>
                </div>
                
              </div>
            </div>
            <div class="col-md-12 text-center">
                <a class="btn btn-product-1 cerrar12" href="#product-list" style="text-align:center;">Cerrar</a>
            </div>
          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp13">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/1.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 **Para leer la información completa por favor da clic sobre cada titulo.**
                </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                        <p>
                            Siendo la Diabetes una de las Enfermedades crónicas no transmisibles que afecta gran cantidad de personas a nivel mundial, 
                            capaz de agravar problemas pre existentes cardíacos, y ser origen de discapacidades importantes (Ej. Ceguera, etc.), y la alteración de las grasas 
                            un predisponente y desencadenante de patologías cardíacas, este sistema ha sido diseñado para integrar e inter relacionar toda esta información 
                            enfocado en profundizar en el estado de salud integral de la persona, relacionando su estado físico, metabólico y funcionamiento cardíaco.<br />
                            Esta secuencia permite perfilar de manera más certera el riesgo o presencia de enfermedades, prevenir el desarrollo de eventos, tales como: 
                            infarto al miocardio, infarto o hemorragia cerebral, ceguera secundaria, insuficiencia renal, etc.
                            Aporta una evaluación amplia y completa del estado de salud, procurando abarcar todas las áreas (Física, Cardiovascular y Metabólica), 
                            logrando un conocimiento claro de estado general de salud,  presencia o no de factores de riesgo de enfermedades crónicas no transmisibles, 
                            niveles de riesgos actuales y capacidad de esfuerzo.
                        </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona?</h4></a>
                <div class="desplegable1">
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque usar "Heart" + "Somos Uno" + "Metabolismo"?</h4></a>
                <div class="desplegable2">
                    <p>
                        Las enfermedades crónicas no transmisibles son la primera causa a nivel mundial de morbilidad y mortalidad según la organización mundial de la salud, 
                        con este  abordaje integral se logra:
                    </p>
                    <ul>
                        <li>Identifica y detecta la presencia Enfermedades Crónicas No Transmisibles (Diabetes, Dislipidemias, etc.).</li>
                        <li>Identifica en cada persona los factores de riesgo, permitiendo establecer con escalas reconocidas por la comunidad científica, el nivel de riesgo para el desarrollo de un evento cardiovascular.</li>
                        <li>Recurso rápido confiable mínimamente invasivo</li>
                        <li>Identificar y detectar el nivel de riesgo individual en el desarrollo de algunas enfermedades.</li>
                        <li>Elaborar y establecer programas preventivos enmarcados en la necesidad de la comunidad trabajadora.</li>
                        <li>Facilitar la colaboración en la implementación y mantenimiento de planes preventivos</li>
                        <li>Promoción de salud de manera global e individual en la empresa</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué recibe la empresa?</h4></a>
                <div class="desplegable3">
                    <p >Una visualización de la salud de todos sus trabajadores, con riesgos específicos, permitiendo detectar problemas de salud antes de manifestarse</p>
                    <ul>
                        <li>Resultado Global de Estado de Salud y riesgo básico de enfermar de sus empleados </li>
                        <li>Resultado Global de la valoración y despistaje de patologías básicas cardíacas de sus empleados</li>
                        <li>Resultado Global de la presencia y riesgo de desarrollar Enfermedades Crónicas no transmisibles más comunes (diabetes y patologías cardiovasculares) </li>
                        <li>Resultados según el esquema organizativo de la empresa, permitiendo la visualización de los departamentos con mayores problemas o riesgos.</li>
                        <li>Análisis de peso, talla e IMC en relación con marcadores deseables.</li>
                        <li>Circunferencia de Cuello y su posición como indicador de riesgo.</li>
                        <li>Análisis de Cintura/Cadera y su posición como indicador de riesgo</li>
                        <li>Análisis de circunferencia de Cintura como indicador de riesgo.</li>
                        <li>Valor de su tensión arterial e interpretación de riesgo</li>
                        <li>Presencia de Diabetes e interpretación de riesgo</li>
                        <li>Presencia de alteración de los lípidos sanguíneos e interpretación de riesgo</li>
                        <li>Estado general e interpretación de riesgos específicos en la población trabajadora</li>
                        <li>Detección de riesgo temprano de enfermedades asintomáticas y con posibilidad de ser incapacitantes a largo plazo</li>
                        <li>Avatar</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué reciben los empleados?</h4></a>
                <div class="desplegable4">
                    <ul>
                        <li>Análisis de peso, talla e IMC en relación con marcadores deseables.</li>
                        <li>Circunferencia de Cuello y su posición como indicador de riesgo.</li>
                        <li>Análisis de Cintura/Cadera y su posición como indicador de riesgo</li>
                        <li>Análisis de circunferencia de Cintura como indicador de riesgo.</li>
                        <li>Valor de su tensión arterial e interpretación de riesgo</li>
                        <li>Estado general de su funcionamiento cardíaco e interpretación de riesgo (probabilidad de afectación si toma medicamentos, ataque cardiaco presente o pasado, etc.)</li>
                        <li>Detección de diabetes e interpretación de riesgo</li>
                        <li>Detección de alteración de lípidos sanguíneos e interpretación de riesgo</li>
                        <li>Detección de temprana de riesgo de desarrollar enfermedades Crónicas no Transmisibles</li>
                        <li>Recomendaciones y orientaciones acordes a su caso particular, dirigiendo hacia el especialista especifico de ser necesario</li>
                        <li>Avatar</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">- Costos </h4></a>
                <div class="desplegable5">
                <p>
                    *** Por definir de acuerdo a las condiciones de ejecución y convenio con la empresa.
                </p>

                </div>
                
              </div>
            </div>

              <div class="col-md-12"><a class="btn btn-product-1 cerrar13" href="#product-list" style="text-align:center;">Cerrar</a></div>

          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp14">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/1.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
                <div class="col-lg-12 text-center">
                 **Para leer la información completa por favor da clic sobre cada titulo.**
                </div>
                <div class="col-md-12">
                    <div class="info2">
                      <a class="buton" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                      <div class="desplegable">
                            <p>
                                Sistema de apoyo en la gestión de salud preventiva, mide los grandes marcadores sanguíneos Azúcar y Grasas en sangre.
                                En pocos minutos puede obtener valores que orientan hacia la presencia o no de Diabetes y Dislipidemias.
                            </p>
                      </div>
                    </div>
                </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona?</h4></a>
                <div class="desplegable1">
                    <p>
                        Es un procedimiento rápido mínimamente invasivo, donde se obtiene con una gota de sangre toda la información necesaria.
                    </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">- ¿Porque usar "Metabolismo"</h4></a>
                <div class="desplegable2">
                    <p>
                       Útil para Identificar y detectar trastornos en el manejo de azúcar y  lípidos sanguíneos punto de origen de muchas enfermedades, las cuales son asintomáticas hasta producir un evento, en algunos casos potencialmente mortales como el infarto.<br />
                       Valioso para aquellas personas que requieren un monitoreo constante de su azúcar en la sangre (pre diabéticos, diabéticos, etc.), y de los lípidos sanguíneos (cardiópatas hipertensos, Dislipidemias familiares, etc.) 
                    </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué recibe la empresa?</h4></a>
                <div class="desplegable3">
                    <p>Una valoración sanguínea de sus empleados: </p>
                    <ul>
                        <li>Resultado Global de los valores del metabolismo de los azucares y lípidos de la población evaluada</li>
                        <li>Resultados según el esquema organizativo de la empresa, permitiendo la visualización de los departamentos con mayores problemas o riesgos.</li>
                        <li>Análisis de glicemia (azúcar en sangre) en relación con marcadores deseables.</li>
                        <li>Análisis de lípidos (grasas en sangre) en relación con marcadores deseables.</li>
                        <li>Estado general e interpretación de riesgos específicos en la población trabajadora</li>
                        <li>Detección de riesgo temprano de enfermedades metabólicas (Diabetes, Dislipidemias) asintomáticas y con posibilidad de ser incapacitantes a largo plazo  </li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style=height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué reciben los empleados?</h4></a>
                <div class="desplegable4">
                    <ul>
                        <li>Análisis e interpretación del valor de glicemia encontrado</li>
                        <li>Análisis e interpretación de los valores de lípidos encontrados</li>
                        <li>Recomendaciones y orientaciones acordes a su caso particular, dirigiendo hacia el especialista especifico de ser necesario</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos </h4></a>
                <div class="desplegable5">
                <p>
                    *** Por definir de acuerdo a las condiciones de ejecución y convenio con la empresa.
                </p>

                </div>
              </div>
            </div>

              <div class="col-md-12 text-center"><a class="btn btn-product-1 cerrar14" href="#product-list" style="text-align:center;">Cerrar</a></div>

          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp15">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/1.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 **Para leer la información completa por favor da clic sobre cada titulo.**
                </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                        <p>
                            Este sistema ha sido diseñado para la medición de riesgos psicosociales en cualquier tipo de trabajo, utilizando cuestionarios estandarizados, 
                            método epidemiológico y método físico.
                        </p>
                        <p >
                            El estrés laboral es considerado un agente generador no sólo de “riesgos” sino también de “daños” para la salud de los trabajadores, 
                            forma parte de los riesgos psicosociales, que son características de las condiciones de trabajo y, concretamente, de la organización del 
                            trabajo que produce condiciones nocivas para la salud. Conociendo su presencia en una empresa, se puede intervenir técnicamente para evitar o 
                            reducir los factores causantes de estrés laboral, creando un espacio, sino “libre” por completo si “adecuado”, para afrontar eficazmente la 
                            aparición de estas situaciones en las empresas.
                        </p>
                        <p>
                            El estrés, es el más común entre las manifestaciones debidas a la exposición a riesgos psicosociales en el trabajo. En todo plan de prevención 
                            laboral debe existir la evaluación y medición de los riesgos psicosociales,  de ahí que este programa busca realizar una medición física 
                            (midiendo la actividad cerebral)  y una medición psicológica con encuestas validadas a nivel mundial, facilitando el cumplimiento de evaluación de 
                            riesgos empresariales.
                        </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona?</h4></a>
                <div class="desplegable1">
                    <p>
                        Se utilizan cuestionarios estandarizados, con bases epidemiológicas y se miden la respuesta cerebral ante los estresores.
                    </p>
                    <p >
                        En las encuestas, combina técnicas cuantitativas (análisis epidemiológico de información obtenida mediante cuestionarios estandarizados y anónimos) y 
                        cualitativas en varias fases y de forma altamente participativa (grupo de trabajo tripartito para la organización de la evaluación y la interpretación de
                        los datos; y círculos de prevención para la concreción de las propuestas preventivas). Esto permite triangular los resultados, mejorando su objetividad y 
                        el conocimiento menos sesgado de la realidad, y facilita la consecución de acuerdos entre todos los agentes (directivos, técnicos y trabajadores) para la 
                        puesta en marcha de las medidas preventivas propuestas.
                    </p>
                    <p>
                        La medición física se hace mediante sensores de ondas cerebrales, las cuales establecen cambios suficientemente estudiados, 
                        con un 89% de certeza ante el estrés, esta combinación 
                        permitirá evaluar con un alto porcentaje de seguridad la presencia o no de riesgos psicosociales en su empresa y en sus trabajadores
                    </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque usar "Estrés como riesgo psicosocial"</h4></a>
                <div class="desplegable2">
                    <p>
                       Es imprescindible la medición de riesgo psicosocial en cualquier empresa, este programa facilita la realización del proceso, con un alto porcentaje de certeza, y confiabilidad.
                    </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué recibe la empresa?</h4></a>
                <div class="desplegable3">
                    <ul>
                        <li>Resultado Global del grado de riesgo psicosocial en la empresa</li>
                        <li>Resultados según el esquema organizativo de la empresa, permitiendo la visualización de los departamentos con mayor problema o riesgo.</li>
                        <li>Detección de riesgo tempranamente permitiendo la elaboración e implementación de planes preventivos o curativos según sea el caso.</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Qué reciben los empleados?</h4></a>
                <div class="desplegable4">
                    <ul>
                        <li>Valoración del grado de estrés individual</li>
                        <li>Análisis e interpretación de los valores encontrados</li>
                        <li>Recomendaciones y orientaciones acordes a su caso particular, dirigiendo hacia el especialista especifico de ser necesario</li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style="height: 30px; background: #139F78; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos </h4></a>
                <div class="desplegable5">
                <p>
                    *** Por definir de acuerdo a las condiciones de ejecución y convenio con la empresa.
                </p>
                </div>
              </div>
            </div>

              <div class="col-md-12 text-center"><a class="btn btn-product-1 cerrar15" href="#product-list" style="text-align:center;">Cerrar</a></div>

          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp1">
      <div class="container">
        <div class="row">

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/2.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 **Para leer la información completa por favor da clic sobre cada titulo.**
                </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style="height: 30px; background: #D65A0E; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                  <p>
                      Sistema de gestión de salud y seguridad en el trabajo Colombia (sgsstc.org), es un software robusto y estable  desarrollado para dar cumplimiento eficaz
                      a la resolución 1111 del 2017 del Ministerio del trabajo que a su vez da cumplimiento al Decreto 171 del 1ero de Febrero del 2016 del mismo ministerio; 
                      este articulado exige a todas las empresas sin importar su tamaño o rama de actividad, desarrollar y mantener de manera estricta su plan de gestión bajo 
                      la metodología PHVA (planear, hacer, verificar, actuar).<br />
                      El sistema actuará como un apoyo experto en el análisis de las matrices (legales y de riesgos), en la definición de los sistemas de gestión, 
                      en la administración de las responsabilidades que de allí se derivan y en el seguimiento y comunicación a los agentes internos y externos que 
                      requieran participar o ser comunicados de los procesos.
                  </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style=" height: 30px; background: #D65A0E; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona?</h4></a>
                <div class="desplegable1">
                    <ul>
                        <li>
                            Pasos Iniciales
                            <ul>
                                <li>Registro en el sistema: Se puede solicitar la activación de la empresa y el usuario administrador, a través del correo electrónico activaciones@sgsstc.org o 
                                    a través del soporte en línea (Chat) de la página www.sgsstc.org.<br />
                                    La información que le será solicitada es: NIT, razón social, código CIIU, nombre del administrador del sistema, documento de identificación del administrador, 
                                    correo electrónico del administrador y si posee, código especial de activación.
                                </li>
                                <li>Descargar la consola de carga masiva para hacer de manera práctica (la consola permite usar archivos Excel) la alimentación del sistema con 
                                    respecto a trabajadores. Si la empresa ya posee un trabajo anterior sobre definición de riesgos también puede proceder a hacer su carga. 
                                    Leer y seguir los pasos que indica la guía (Instructivo-de-carga-Masiva-del-SGSST.doc) </li>
                                <li>Leer y proceder a las configuraciones básicas por parte del administrador y seguir los pasos que indica la guía de implementación. 
                                    (Pasos_para_Implementar_el_SGSST.doc)</li>
                                <li>Proceder a diligenciar los ítems que se indican para conseguir el cumplimiento y documento final del primer paso denominado. Evaluación Inicial.
                                    <img class="text-center" src="images/12.png" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style="height: 30px; background: #D65A0E; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque usar SGSSTC?</h4></a>
                <div class="desplegable2">
                <ul>
                    <li>
                        Matriz legal: son el grupo de 92 leyes, normas y/o decretos que han sido impartidas por todos los ámbitos de gobierno que indican lo que debe cumplir 
                        la organización, normalmente para esto se solicita asesoría jurídica que realice el compendio normativo. <br />
                        El sistema realiza el proceso de reconocimiento para la empresa, genera de manera automática la lista normativa y establece la estructura necesaria 
                        para que los responsables indiquen la condición de cumplimiento  (de las obligaciones).
                    </li>
                    <li>
                        Matriz de Riesgos: Son el grupo de patologías o enfermedades, que han sido observadas en los trabajadores por exposición a diversas condiciones mecánicas,
                        físicas, químicas, biológicas y/o psicosociales. Normalmente se requiere experiencia de médicos ocupacionales e Ingenieros industriales 
                        para relacionar a la empresa con las diversas enfermedades sobre las que habría que realizar especial atención.<br />
                        El sistema realiza el proceso de reconocimiento para la empresa, genera de manera automática la lista de condiciones por tipo de riesgo y establece 
                        la estructura necesaria para que los responsables indiquen las medidas de control y/o mitigación.
                    </li>
                    <li>
                        Normalmente se requiere gran experiencia en Seguridad Industrial para poder realizar y 
                        gestionar la lista de tareas que son de obligatorio cumplimiento en observancia de la seguridad en la empresa.  <br /> 
                        El sistema posee todos los módulos necesarios para el cumplimiento de la normativa vigente y están diseñados, de tal forma, 
                        que auxilian la realización y no hacen necesaria la participación de un sujeto con gran experiencia para poder dar cumplimiento.
                    </li>
                </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style="height: 30px; background: #D65A0E; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que recibe la Empresa?</h4></a>
                <div class="desplegable3">
                    <p>Reducción de Costos.</p>
                    <ul>
                        <li>
                            Descuentos en el valor promedio de gasto en aplicaciones que auxilian la gestión, los ahorros van desde el 50% hasta el 90%.
                        </li>
                        <li>La empresa recibe beneficios por la vía de la reducción directa de costos una vez las empresas aplican una menor cantidad de personas y labores para mantener la documentación y los procesos al día, 
                            conforme la normatividad de salud y seguridad en el trabajo.</li>
                    </ul>
                    <p>Soporte inmediato.</p>
                    <ul>
                        <li>El soporte es principalmente en línea a través de chat multiusuario. Cada contacto es atendido por profesionales que de manera inmediata auxilian en la situación o convocan a otros profesionales 
                            para que en el mismo contacto la empresa encuentre las respuestas y herramientas que requiere para cada inquietud. </li>
                        <li>Cada contacto queda registrado y una vez finalizada la atención la transcripción es enviada al correo de la empresa para su posterior revisión.</li>
                    </ul>
                    <p>Cumplimiento estricto de las Etapas (Documentos_de_Soporte_del_SGSST.doc).</p>
                    <ul>
                        <li>
                            Cada una de las etapas del Sistema de Gestión de Seguridad y Salud en el Trabajo requiere soportes y documentos que pueden ser solicitados por 
                            las autoridades ante cualquier auditoría. En SGSSTC cada etapa proporciona los soportes. En total se generan 66 Documentos en el ciclo completo 
                            del PHVA.
                        </li>
                        <li>
                            <ul>
                                <li>Evaluación Inicial (9 documentos)</li>
                                <li>Planear (18 Documentos)</li>
                                <li>Hacer (24 Documentos)</li>
                                <li>Verificar (3 Documentos)</li>
                                <li>Actuar (2 documentos)</li>
                            </ul>
                        </li>
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style="height: 30px; background: #D65A0E; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">- Costos</h4></a>
                <div class="desplegable4">
                    <p>Los costos de uso de la aplicación dependen del tamaño de la organización.</p>
                    <table class="table">
                        <tr>
                            <th>Valores Anuales</th>
                            <th>Micro</th>
                            <th>Pequeña</th>
                            <th>Mediana</th>
                            <th>Gran Empresa</th>
                        </tr>
                        <tr>
                            <th>GRUPO LI</th>
                            <th>1.400.000</th>
                            <th>1.950.000</th>
                            <th>4.800.000</th>
                            <th>9.600.000</th>
                        </tr>
                    </table>
                </div>                
              </div>
            </div>
              <div class="col-md-12 text-center"><a class="btn btn-product-2 cerrar1" href="#product-list" style="text-align:center;">Cerrar</a></div>
          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp2">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/3.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 **Para leer la información completa por favor da clic sobre cada titulo.**
               </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style="height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                  <p>
                      Sistema de reconocimiento facial que permite asignar de manera confiable atribuciones de uso a una identidad facial, misma que será distintiva con un 100% 
                      de confiabilidad. Estas atribuciones de uso pueden ser aplicables no solo a la habitación sino al acceso a cualquier espacio que el hotel quiera administrar.
                  </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style="height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Como Funciona?</h4></a>
                <div class="desplegable1">
                <p >
                    Se registra la imagen facial del huésped y de cualquier acompañante al que se le requiera dar atribuciones. 
                    Este proceso no toma más de un par de segundos y logra 3 imágenes de cada persona. <br />
                    Todos los huéspedes activos y el personal del hotel (si el uso que se implica no es solo para habitaciones sino para diversas áreas) 
                    forman el grupo completo sobre el que se realizan las operaciones de reconocimiento de pertenencia y atribuciones.
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style="height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque es un producto pertinente para el mercado hotelero?</h4></a>
                <div class="desplegable2">
                <p>
                    Este servicio se ha diseñado como la herramienta que maximiza la seguridad de acceso y verificación de identidad, 
                    al tiempo que minimiza los factores asociados al costo del proceso (gestión, uso, implantación y mantenimiento), todo esto en un entorno de cero incomodidad al cliente. 
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style="height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que recibe el Hotel?</h4></a>
                <div class="desplegable3">
                <ul>
                    <li>
                        Verificación de antecedentes para evitar que personas buscadas por autoridades en un contexto nacional e internacional se hospeden en las instalaciones. 
                        La idea es aumentar el blindaje para cuidar la integridad del establecimiento.
                    </li>
                    <li>
                        Eliminación de la manipulación de llaves en su forma convencional o tarjetas de acceso. 
                        Todas ellas en su forma convencional implican generación física, entrega, recolección, reposición, anulación, cambio, mantenimiento por uso.
                    </li>
                    <li>
                        Validación para las habitaciones y espacios sobre los que se quiere administrar su uso. El acto de asegurar que quien use el espacio sea el autorizado, 
                        es un accionar de continuo y amplio desgaste para el personal de seguridad y servicio al cliente. Con esta herramienta los administradores contarán 
                        con información en tiempo real para decidir si permitir, cobrar de manera adicional o impedir el accionar indebido sobre los espacios.
                    </li>
                    <li>
                        Medio de prueba eficaz, es la clave para alejar los conflictos que pueden rondar las decisiones que sobre asuntos de uso y normalmente con 
                        implicaciones económicas se generan. El mundo se mueve en la dirección de las imágenes como factores de prueba básica y aceptación incontrovertible.
                    </li>
                </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style="height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que reciben los usuarios o clientes?</h4></a>
                <div class="desplegable4">
                <ul>
                    <li>
                        Comodidad sin comparación en el uso y la seguridad de acceso. Se elimina la necesidad de llevar nada que permita el ingreso, mismo elemento que 
                        siempre es engorroso por la facilidad de su extravío. 
                    </li>
                    <li>
                        Control eficaz sobre las personas que ingresan a las habitaciones del personal de servicio y mantenimiento del hotel, 
                        generando mayor nivel de confianza en la seguridad de las instalaciones.
                    </li>
                </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style="height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos</h4></a>
                <div class="desplegable5">
                <p>*** Por definir</p>
                </div>                
              </div>
            </div>

              <div class="col-md-12"><a class="btn btn-product-3 cerrar2" href="#product-list" style="text-align:center;">Cerrar</a></div>

          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp21">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/3.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 **Para leer la información completa por favor da clic sobre cada titulo.**
               </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                      <p >
                          Sistema de reconocimiento facial que permite aumentar la confianza en conductor y pasajero sobre la identidad de cada parte en el servicio y la consulta 
                          sobre la existencia de reseñas de comportamientos que caractericen al conductor y pasajero, permitiendo que ambas partes valoren y decidan el nivel de 
                          riesgo económico y/o físico tolerable.<br />
                          El sistema funciona de manera independiente a las aplicaciones de solicitud de servicio, cuando estas no lo tienen incluido dentro de sus opciones de 
                          seguridad.
                      </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Como Funciona para los usuarios clientes?</h4></a>
                <div class="desplegable1">
                <p >
                    Cuando funciona de manera independiente de las aplicaciones de solicitud del servicio de transporte:<br />
                    Al Iniciar la aplicación, el sistema se queda en forma de latencia esperando la imagen del conductor a verificar.<br /> 
                    Una vez el conductor llega y solo bajo su autorización se toma una foto de su rostro y el sistema en 10 segundos arroja la validación de coincidencia 
                    con el conductor registrado para el servicio, sobre la condición de su licencia de conducción, estado de infracciones. <br />
                    Cuando funciona integrado con aplicaciones: una vez se solicita y es asignado el servicio, la información sobre el conductor es desplegada. 
                    Cuando el conductor llega puedes solicitar tomar foto para validar si es el mismo conductor que está registrado con la información que te fue enviada.
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona para los usuarios conductores?</h4></a>
                <div class="desplegable2">
                <p >
                    Cuando funciona independiente a las aplicaciones de solicitud de servicios: Al Iniciar la aplicación, el sistema se queda en forma de latencia esperando
                    la imagen del pasajero a verificar. 2. El conductor solicita autorización al usuario para registrar su rostro y el sistema en 10 segundos arroja 
                    la validación sobre la condición de riesgo del pasajero que incluye si ha sido reportado por comportamientos indebidos por otros conductores y 
                    si tiene reportes o sanciones por parte de la policía.<br />
                    Cuando funciona integrado con aplicaciones de solicitud de servicio, al conductor se le despliega la información del cliente.  
                    Cuando el conductor llega puede solicitar tomar foto para validar si es el mismo pasajero que está registrado con la información que le fue enviada. 
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque es pertinente el servicio?</h4></a>
                <div class="desplegable3">
                <p >
                    La seguridad física y el riesgo económico de fraudes son riesgos permanentes en esta relación comercial. 
                    El primer paso para procurar reducir el riesgo ha sido la generación de cuentas por parte de usuarios y conductores; 
                    el problema con este primer paso, es que no se habría desarrollado hasta el momento ningún medio para validar la correspondencia 
                    entre la información de las cuentas y la persona que llegaba a solicitar o prestar el servicio.  
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos</h4></a>
                <div class="desplegable5">
                <p >*** Por definir</p>
                </div>
              </div>
            </div>
             <div class="col-md-12 text-center"><a class="btn btn-product-3 cerrar21" href="#product-list" style="text-align:center;">Cerrar</a></div>
          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp22">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/3.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 **Para leer la información completa por favor da clic sobre cada titulo.**
               </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                      <p >
                        Sistema de reconocimiento que permite identificar a los individuos en fila de ingreso, validar el saldo de su cuenta y autorizar su ingreso si 
                        cumple el saldo para el pasaje. Este proceso se puede cumplir en grupos, razón por la cual el ingreso puede aumentar de manera significativa 
                        la capacidad de carga de las entradas.<br />
                        Gracias a que este medio de validación elimina la necesidad del uso de tarjetas, la recarga de la cuenta a través de medios electrónicos y sucursales 
                        virtuales se vuelve una realidad con la que los medios de transporte pueden contar.<br />
                        Este medio es mucho más eficiente para el buen funcionamiento del TPP que el uso de tarjetas ya que elimina la necesidad de las mismas y 
                        permite un manejo lógico de la asociación de los pasajes con las rutas; permitiendo la simplificación de las tarifas por trayectos.<br />
                        El proceso de reconocimiento puede hacer cruces con agencias de seguridad permitiendo disuadir el ingreso de personas con antecedentes 
                        o con cuentas pendientes con las autoridades. 
                      </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Como Funciona para los usuarios?</h4></a>
                <div class="desplegable1">
                <p >
                    Los usuarios contarán con una opción adicional en la cual no necesitarán tarjetas o documentos de identificación diferentes a su rosto. 
                    La persona cargará saldo directamente en las cajas autorizadas para este fin o a través de los medios electrónicos habilitados para este propósito.
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque es pertinente el servicio?</h4></a>
                <div class="desplegable3">
                <p >
                    El servicio aumenta la productividad de los medios de transporte, agiliza el ingreso, eliminando el cuello de botella que significan las entradas, 
                    sobre todo en horas pico. Al eliminar la tarjeta como portador de saldo y entregarle esta tarea al rostro, las cuentas virtuales se vuelven una opción 
                    real como medio de pago reduciendo la carga sobre las cajas en instalaciones física. Aplicar métodos tradicionales de seguridad soportados en la acción 
                    humana como primera capa de validación es inefectivo por el volumen de personas que acuden a estos espacios. El servicio acude para brindar una primera 
                    capa que puede cruzar con agencias de seguridad para reconocer entre los pasantes personas que tienen llamados por autoridades.
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos</h4></a>
                <div class="desplegable5">
                <p >*** Por definir</p>
                </div>
              </div>
            </div>

              <div class="col-md-12"><a class="btn btn-product-3 cerrar22" href="#product-list" style="text-align:center;">Cerrar</a></div>
          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp23">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/3.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 ** Para leer la información completa por favor da clic sobre cada titulo.**
               </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                      <p >
                            Sistema de apoyo a la “administración” y atención proactiva de público asistente a eventos deportivos. 
                            Gestión del proceso desde el ingreso hasta la salida incluyendo el comportamiento durante el evento.
                      </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Como Funciona?</h4></a>
                <div class="desplegable1">
                <p >
                    Los usuarios llegan a las filas o espacios de ingreso de los espectáculos, en este(os) lugar(es) el sistema individualiza a cada sujeto y 
                    revisa si tiene asociada a su imagen alguna característica que restringa su ingreso y si tiene abonada una entrada para el día/hora/evento, 
                    entregando luz verde, roja o amarilla.<br />
                    Durante el espectáculo se puede solicitar que el sistema reconozca en tiempo real a partir de imágenes de individuos, grupos específicos. 
                    Una vez individualizadas las personas de esos grupos, se puede solicitar que se actúe sobre alguno de los participantes. 
                    Las actuaciones son normalmente protocolos de mensajes o comunicaciones que se dirigen a la(s) persona(s) de interés para que 
                    desista de un comportamiento en específico.<br />
                    Si el comportamiento no logra ser disuadido, se puede igualmente solicitar seguimiento coordinado de la(s) persona(s) que debe ser retirado(s). 
                    En paralelo se construye una red de información que sirve para el abordaje del proceso.
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque es pertinente el servicio?</h4></a>
                <div class="desplegable3">
                <p >
                    La atmósfera social o de comportamiento de los escenarios deportivos lo marcan las expresiones emocionales (gritos, vitoreos, etc.) 
                    que evidencian las mayorías, pero se conoce de manera extensa que pequeños grupos y hasta individuos aislados causan o ejecutan acciones 
                    denominadas vandálicas que ponen en tela de juicio la seguridad de los eventos mismos, anulando la percepción de buen comportamiento que 
                    hubiera dejado la mayoría, si esas acciones aisladas se hubieran podido evitar. <br />
                    A la luz del nuevo código de policía son los empresarios los primeros responsables de la seguridad e integridad de los asistentes, así pues,
                    las instituciones están siendo llamadas a desarrollar modelos para cumplir estas nuevas tareas. 
                   
                </p>
                </div>
              </div>
            </div>
            
           <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que recibe la empresa?</h4></a>
                <div class="desplegable4">
                <ul >
                   <li>
                       Sistema para administrar la entrega y uso de las entradas a sus espectáculos, 
                       de esta manera los equipos pueden reducir los costos asociados a la venta de boletería y la supervisión de su uso al ingreso.
                   </li> 
                    <li>
                        La administración integrada de venta y validación de ingreso abre una nueva ventana para que los equipos administren el uso de la boletaría.
                    </li>       
                    <li>
                        Con la individualización del ingreso los equipos van a poder mejorar el mercadeo con sus aficionados permitiendo que desde el ingreso mismo ellos sientan un nivel superior de calidad en su atención
                    </li>   
                    <li>
                        Sistema para identificar de manera inmediata a personas o grupos de personas, de tal forma que se pueda accionar de 
                        manera inmediata respuestas para anular los comportamientos indebidos. 
                    </li>         
                    <li>
                        Una vez identificado(s) la(s) personas que comienzan a presentar comportamientos que de alguna manera se consideran riesgosos el 
                        sistema puede disparar acciones que se conocen como disuasivas, acudiendo al uso de “la puesta en evidencia” que desactiva de manera inmediata en el 95% de los casos los comportamientos no deseados ya que les quita a estas personas el anonimato
                    </li>
                    <li>
                        El sistema puede pasar de la acción disuasiva al reconocimiento para retención. Se trata en este caso de guiar a la(s) 
                        personas encargadas hasta el sujeto(s) a través de la búsqueda y reiterada indicación de la posición.
                    </li>
                </ul>
                </div>
              </div>
            </div>

              <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que reciben los usuarios, aficionados o clientes?</h4></a>
                <div class="desplegable5">
                <ul >
                   <li>
                       Facilitación increíble en la adquisición de boletas y su posterior negociación en caso de imposibilidad de asistencia. 
                       Los aficionados van a encontrar un posible canal oficial para negociar sus entradas..
                   </li> 
                    <li>
                        La validación de ingreso no se realizará con el modelo tradicional de papeles especialmente marcados (boletas) 
                        sino con la identificación única del rostro de cada aficionado. Esta circunstancia agiliza el ingreso reduciendo los tiempos de espera.
                    </li>       
                    <li>
                        Un trato más personalizado desde el momento del ingreso, ante cualquier 
                        eventualidad en el desarrollo de la actividad y hasta en la salida del espectáculo.
                    </li>   
                </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton6" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos</h4></a>
                <div class="desplegable6">
                <p >*** Por definir</p>
                </div>
              </div>
            </div>

            <div class="col-md-12"><a class="btn btn-product-3 cerrar23" href="#product-list" style="text-align:center;">Cerrar</a></div>

          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp24">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/3.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 ** Para leer la información completa por favor da clic sobre cada titulo.**
               </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                      <p >
                            Ciudad segura es un servicio que ayuda a cerrar el anillo que se inicia con servicios como Recognition Hotel, 
                          Trasporte público Individual, Transporte público Masivo y Big-bro, para crear una capa de seguridad en torno a 
                          espacios de uso o afectación pública o que son especialmente sensibles al interés colectivo. 
                          Al aplicar el reconocimiento de identidad de manera no invasiva a grupos, se permite aumentar la eficacia y 
                          eficiencia de la fuerza pública en respuesta a incidentes en los espacios cubiertos y posibilita pasar a los movimientos proactivos.<br />
                            En términos concretos estamos hablando de pasar de tareas que requieren actualmente semanas o meses a minutos.
                      </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Como Funciona?</h4></a>
                <div class="desplegable1">
                <p >
                    Preparación de espacios: Se deben seleccionar de manera estratégica los lugares a ser controlados, ellos deben permitir la captura directa de los rostros. 
                    El medio para la captura pueden ser los equipos tradicionales de video vigilancia y de manera adicional se puede potenciar 
                    el impacto con dispositivos  más pequeños y mucho más flexibles en referencia al tamaño y la facilidad para ubicación en pequeños
                     espacios en puntos poco llamativos que no cambian de manera notoria el amueblamiento de ciudad.<br />
                    Reconocimiento: De manera automática cuando las personas pasan por las áreas marcadas como de interés. 
                    Una vez identificados se pasa la información a la capa lógica para reportar de manera inteligente, según sea el interés de la captura.
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque es pertinente el servicio?</h4></a>
                <div class="desplegable3">
                <p >
                    Costo: A diferencia de los sistemas actuales de captura o video vigilancia que usan las ciudades, el costo por punto o dispositivo es 1/50 parte y
                    a diferencia de sus contrapartes actuales el uso está diseñado para ser desasistido, razón por la cual no solo se ahorra en infraestructura sino 
                    en operatividad reduciendo el costo y aumentando la productividad del servicio. De esta manera se pueden cubrir áreas de manera extensa con el 
                    objetivo de mantener reconocimiento continuo.<br />
                    Aumento significativo de la eficiencia: Los sistemas actuales están enfocados en buscar las historias de lo que ocurre, 
                    ciudad segura apoyará a la parte humana en las tareas de identificar, construir las redes de interés y buscar a los responsables, 
                    permitiendo que aumente la eficacia de la fuerza humana en el análisis, la creación de la lógica de los eventos y la selección de los
                    medios probatorios que corresponden y mejor aplican en cada caso. 
                </p>
                </div>
              </div>
            </div>
            
           <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que recibe la ciudad en materia de información de seguridad?</h4></a>
                <div class="desplegable4">
                <ul >
                   <li>
                       Todos los nodos hacen validación de seguridad, por eso cuando Recognition Hotel, Transporte individual, Transporte Colectivo, transporte Masivo y Big-Bro son usados y generan alguna alerta sobre individuo(s) estos reportes pueden ser pasados al Módulo Ciudad Segura.
                   </li> 
                    <li>
                        Ciudad Segura, por ella misma, genera desde cada punto de captura sus propias recolecciones, identificaciones y validaciones.
                    </li>       
                    <li>
                        Las capturas se dan normalmente sobre grupos de personas y una vez el sistema obtiene el o los positivos (que son las personas que generan por algún motivo alerta en el sistema) busca relación(es) entre los sujetos contruyendo una red de interés. Esta red se puede construir con un marco de temporalidad de hasta 24 hs.
                    </li>   
                    <li>
                        Una vez los sujetos positivos son indicados como de interés para detención el sistema activa una búsqueda en toda la red de reconocimiento que alerta de manera inmediata a o las personas que están activados para ser informados.
                    </li>         
                </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton6" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos</h4></a>
                <div class="desplegable6">
                <p >*** Por definir</p>
                </div>
              </div>
            </div>

              <div class="col-md-12"><a class="btn btn-product-3 cerrar24" href="#product-list" style="text-align:center;">Cerrar</a></div>

          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp25">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/3.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 ** Para leer la información completa por favor da clic sobre cada titulo.**
               </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                      <p >
                          Sistema desarrollado para identificar los patrones o gustos en referencia a prendas de vestir con los que se identifica cada sujeto. 
                          Este modelo se construye a partir de la presencia en redes sociales. 
                      </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Como Funciona?</h4></a>
                <div class="desplegable1">
                <p >
                    Cada vez que aparecemos en fotos en redes sociales, mostramos de manera evidente nuestra voluntad con respecto al vestuario. 
                    Si el análisis cuenta con suficiente información en términos de tiempo y ocasionalidad, es posible perfilar el patrón de color, 
                    tipos de prenda y tipos de tela o materiales que le son más cómodos y que serían los más probables de repetir en siguientes compras.
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque es pertinente el servicio?</h4></a>
                <div class="desplegable3">
                <p >
                    Conocer lo que nos sirve y discriminar entre lo que por talla puede ajustar a las personas ha sido el primer acercamiento de la tecnología para 
                    facilitar las compras virtuales de vestuario a los consumidores. <br />
                    Hasta la aparición de este servicio no se habría desarrollado ninguna herramienta que perfilara más allá de la talla, 
                    el gusto de los consumidores y es allí donde Real Li se hace grande y marca la diferencia, ya que hasta los mismos consumidores se 
                    ven sorprendidos al serles presentados sus patrones de gustos. Desde allí, hay un paso para poder cruzar de manera más eficiente la 
                    oferta de las empresas con los patrones de compras de las personas.
                </p>
                </div>
              </div>
            </div>
            
           <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que ofrece el servicio para las empresas?</h4></a>
                <div class="desplegable4">
                    <p >Las marcas sin tienda física (distribución Online)</p>
                    <ul >
                       <li>
                            Codificar de manera asistida (semi-automática) el inventario en la plataforma on-line.
                           <ul>
                               <li>
                                   Pueden agregar prendas de manera individual (en caso de modelos por fuera del estándar de tallaje de su marca) o asociar 
                                   colecciones por tipo de talla usada.
                                   <ul>
                                       <li>
                                           Deben caracterizar las prendas ingresadas con un patrón mayor (tipo de prenda, ej: camisa, saco, pantalón, etc)
                                       </li>
                                       <li>
                                           Patrones menor (color, tipo de estampado, tipo de tela, etc) pero a mayor caracterización mayor confiabilidad en el match de gustos con los productos.
                                       </li>
                                       <li>
                                           Ocasión
                                       </li>
                                   </ul>
                               </li>
                           </ul>
                       </li>
                       <li>Pueden hacer prueba de mercado con conceptos, antes de lanzar producción masiva.</li>      
                    </ul>
                    <p >Las Marcas con tiendas (distribución o presencia física)</p>
                    <ul >
                       <li>
                            Asesorar a los clientes de manera inmediata, una vez reconocidos por la plataforma, sobre las existencias que la tienda tiene 
                           que cumplen sus gustos tradicionales.
                       </li>
                       <li>Atender a clientes nuevos como si fueran compradores con largo historial ya que se conoce sus gustos.</li>   
                       <li>Permitir hacer pruebas virtuales de clientes sobre las prendas automáticamente seleccionadas.</li>   
                    </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que Ofrece el servicio para los usuarios finales?</h4></a>
                <div class="desplegable5">
                <p >
                    Encuentran una plataforma que se despliega de manera inmediata alimentada por las diferentes marcas asociadas a la estrategia con 
                    los productos que son de gusto y talla de cada comprador. 
                    La tienda se divide visualmente según prendas las cuales se muestran según la ocasión que seleccione el comprador. 
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton6" href="#togg"><h4 style=" height: 30px; background: #1273C9; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos</h4></a>
                <div class="desplegable6">
                <p >*** Por definir</p>
                </div>
                
              </div>
            </div>

              <div class="col-md-12"><a class="btn btn-product-3 cerrar25" href="#product-list" style="text-align:center;">Cerrar</a></div>

          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp3">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="secProduct">
            </div>
          </div>

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/4.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 ** Para leer la información completa por favor da clic sobre cada titulo.**
               </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style=" height: 30px; background: #D144F1; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                  <p >
                      Estar saludable es el estado natural del ser humano cuando cumple la regla de oro de una correcta alimentación, hábitos de vida adecuados y 
                      un ritmo de actividad físico acorde a su edad y características particulares.<br />
                      Los postulados anteriores son terreno común de muchos propósitos y en atención a la verdad, muchas empresas y trabajadores han procurado su búsqueda; 
                      lastimosamente y en presencia de las estadísticas más recientes, el sobrepeso, el estrés, el ausentismo y las enfermedades laborales no mejoran, 
                      aportando resultados poco alentadores.<br />
                      La actividad y el entorno laboral profesional agregan nuevos componentes y exigencias que afectan, en la mayoría de los casos, de manera negativa 
                      la salud de los empleados; es por eso que nuestra propuesta hace caso de la experiencia de más de 6 años en actividad directa al nivel empresarial 
                      para obtener de manera eficiente una mejoría cuantificable de la condición física y psíquica de los empleados y en última instancia de manera indirecta, 
                      unas mejores condiciones de entorno laboral.
                  </p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style=" height: 30px; background: #D144F1; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Cómo Funciona?</h4></a>
                <div class="desplegable1">
                <p >
                    A los alumnos les es cargado un # de clases determinado por el convenio a una cuenta individual desde la que pueden separar el cupo para las clases a las que desean asistir.
                </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style=" height: 30px; background: #D144F1; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque es pertinente el servicio?</h4></a>
                <div class="desplegable2">
                <ul >
                    <li>Los alumnos deben poder realizar actividades físicas que impacten de manera positiva su condición pero que no riñan con su perfil como sujeto.(Una misma clase para  todos no funciona)</li>
                    <li>El modelo de actividades debe ser de mutuo soporte para evitar la monotonía y el    estancamiento que se produce con la práctica de una sola técnica.</li>
                    <li>Los alumnos deben tener elementos que les permitan reconocer como van en su proceso.</li>
                    <li>El proceso y la mecánica de la operación no debe recargar trabajo en los responsables de la gestión humana.</li>
                    <li>La logística debe ser dinámica y proactiva.</li>
                </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style=" height: 30px; background: #D144F1; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que recibe la empresa?</h4></a>
                <div class="desplegable3">
                <ul >
                    <li>Tipos de clases acordes al perfil del grupo de personas</li>
                    <li>Profesores idóneos no solo en el conocimiento de su técnica sino de la enseñanza.</li>
                    <li>Plataforma de administración de alumnos, profesores, clases, reservas y cancelaciones y gestión de archivos para conciliar con nóminas.</li>
                    <li>Metodología de Seguimiento y control.</li>
                </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style=" height: 30px; background: #D144F1; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que reciben los empleados?</h4></a>
                <div class="desplegable4">
                <ul >
                    <li>Los modelos se aplican con 8 o 12 clases al mes por persona</li>
                    <li>Acceso a un sistema de reservar que les permite administrar sus clases para programarlas cuando puedan y de lo que quieran, obviamente sujeto a las opciones horarias y temáticas convenidas con la empresa.</li>
                </ul>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style=" height: 30px; background: #D144F1; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos</h4></a>
                <div class="desplegable5">
                <p >
                    El valor por alumno esta entre los 43.000 y 56.000 pesos; por experiencia, sugerimos que el alumno asuma la mitad y la empresa la mitad. 
                    De esta forma no se crean las condiciones para que existan programas abiertos sin alumnos (cuando todo lo asume la empresa) o que existan programas que 
                    difícilmente lleguen al número mínimo por el costo que deben asumir los Alumnos (en los casos en los que la empresa no apoya económicamente).
                </p>
                </div>
              </div>
            </div>
            <div class="col-md-12 text-center"><a class="btn btn-product-4 cerrar3" href="#product-list" style="text-align:center;">Cerrar</a></div>
          </div>

        </div>
      </div>
    </div>

    <div class="footer" id="producto_desp4">
      <div class="container">
        <div class="row">

          <div class="col-md-4">
              <div class="addr">
                <img src="images/grids/5.gif"></img>
              </div>
          </div>

          <div class="col-md-8">
              <div class="col-lg-12 text-center">
                 ** Para leer la información completa por favor da clic sobre cada titulo.**
               </div>
            <div class="col-md-12">
                <div class="info2">
                  <a class="buton" href="#togg"><h4 style=" height: 30px; background: #750505; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que es?</h4></a>
                  <div class="desplegable">
                  <p >Sistema hibrido construido entre herramientas de realidad virtual, sensores, equipos de entrenamiento físico y ambientación inmersiva para lograr experiencias que permitan activar todos los sentidos. .</p>
                  </div>
                </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton1" href="#togg"><h4 style=" height: 30px; background: #750505; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Como funciona?</h4></a>
                <div class="desplegable1">
                    <p >- El usuario hace una reserva de hora/día, de esta manera el sistema de reservas reconoce la identidad del asistente que va a realizar la experiencia.</p>
                    <p >- El sistema indica </p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton2" href="#togg"><h4 style=" height: 30px; background: #750505; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Porque, “    ”?</h4></a>
                <div class="desplegable2">
                <p >Aqui se puede colocar informacion sobre que es el producto.</p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton3" href="#togg"><h4 style=" height: 30px; background: #750505; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">¿Que recibe la empresa?</h4></a>
                <div class="desplegable3">
                <p >Aqui se puede colocar informacion sobre que es el producto.</p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton4" href="#togg"><h4 style=" height: 30px; background: #750505; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Que reciben los empleados?</h4></a>
                <div class="desplegable4">
                <p >Aqui se puede colocar informacion sobre que es el producto.</p>
                </div>
              </div>
            </div>

            <div class="col-md-12">
              <div class="info3">
                <a class="buton5" href="#togg"><h4 style=" height: 30px; background: #750505; color: white; border-radius: 3px; padding-top: 5px; padding-left: 5px;">Costos </h4></a>
                <div class="desplegable5">
                <p >Aqui se puede colocar informacion sobre que es el producto.</p>
                </div>
              </div>
            </div>

              <div class="col-md-12 text-center"><a class="btn btn-product-5 cerrar4" href="#product-list" style="text-align:center;">Cerrar</a></div>

          </div>

        </div>
      </div>
    </div>

    <div id="templatemo_about" class="section3">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="knobWrap">
              <div class="row form-group">
                <div class="col-sm-6 col-md-6">
                  <div class="knob">
                    <div class="c100 p100 big" style="font-weight: bold;">
                        
                        <span><a href="QuienesSomos.aspx">QUIENES<br>SOMOS</a></span>
                        <div class="slice">
                            <div class="bar"></div>
                            <div class="fill"></div>
                        </div>
                        
                    </div>
                  </div>                    
                </div>
                <div class="col-sm-6 col-md-6">
                  <div class="knob">
                    <div class="c100 p100 big" style="font-weight: bold;">
                      <span><a href="Equipo.aspx">EQUIPO DE<br>TRABAJO</a></span>
                      <div class="slice">
                          <div class="bar"></div>
                          <div class="fill"></div>
                      </div>
                    </div>
                  </div>
                </div>

              </div>
            </div>
            <div class="row">
              <div class="col-md-12">
                <div class="socials sbot">
                  <ul>
                    <li><a href="#"><i class="fa fa-twitter soc"></i></a></li>
                    <li><a href="#"><i class="fa fa-facebook soc"></i></a></li>
                    <li><a href="#"><i class="fa fa-instagram soc"></i></a></li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div> 
      
   <!-- e/o section3 -->

    <div id="templatemo_contact" class="section5">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
          </div>
        </div>
      </div>
      <div class="container">
        <div class="row">
          <div class="col-md-3">
            <form role="form">
              <div class="form-group">
                <input name="fullname" type="text" class="form-control" id="Text1" placeholder="Nombre" maxlength="30">
              </div>
              <div class="form-group">
                <input name="email" type="text" class="form-control" id="Text2" placeholder="Correo" maxlength="30">
              </div>
              <div class="form-group">
                <input name="subject" type="text" class="form-control" id="subject" placeholder="Asunto" maxlength="40">
              </div>
              <div><button type="button" class="btn btn-primary">Enviar</button></div>
            </form>
          </div>
          <div class="col-md-6">
            <div class="txtarea">
              <textarea name="message" rows="10" class="form-control" id="message" placeholder="Mensaje..."></textarea>
            </div>
          </div>
          <div class="col-md-3">
            <div class="addr">
              <p>Datos de Contacto.</p>
              <ul>
                <li><i class="fa fa-map-marker"></i>Calle 10A#40-60 Medellín-Antioquia</li>
                <li><i class="fa fa-mobile-phone"></i>(57) 300-3189349 </li>
                <li><i class="fa fa-whatsapp"></i>(57) 300-3189349</li>
                <li><i class="fa fa-envelope"></i>pqr@grupoli.com</li>
              </ul>
            </div>
          </div>
        </div>
      </div>

    </div> <!-- eo section 5 -->

      <div id="back-top" class="gotop text-center">
        <a href="">Volver al Inicio</a>
      </div>

      <div class="bfWrap text-center">
        <div class="templatemo_footer">Copyright © 2017 Grupo-LI</div>
      </div>

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <!-- <script src="https://code.jquery.com/jquery.js"></script> -->
    <script type="text/javascript">
        $(function () {
            var default_view = 'grid';

            if ($.cookie('view') !== 'undefined') {
                $.cookie('view', default_view, { expires: 7, path: '/' });
            }

            function get_grid() {
                $('.list').removeClass('list-active');
                $('.grid').addClass('grid-active');
                $('.prod-cnt').animate({ opacity: 0 }, function () {
                    $('.prod-cnt').removeClass('dbox-list');
                    $('.prod-cnt').addClass('dbox');
                    $('.prod-cnt').stop().animate({ opacity: 1 });
                });
            }

            function get_list() {
                $('.grid').removeClass('grid-active');
                $('.list').addClass('list-active');
                $('.prod-cnt').animate({ opacity: 0 }, function () {
                    $('.prod-cnt').removeClass('dbox');
                    $('.prod-cnt').addClass('dbox-list');
                    $('.prod-cnt').stop().animate({ opacity: 1 });
                });
            }

            if ($.cookie('view') == 'list') {
                $('.grid').removeClass('grid-active');
                $('.list').addClass('list-active');
                $('.prod-cnt').animate({ opacity: 0 });
                $('.prod-cnt').removeClass('dbox');
                $('.prod-cnt').addClass('dbox-list');
                $('.prod-cnt').stop().animate({ opacity: 1 });
            }

            if ($.cookie('view') == 'grid') {
                $('.list').removeClass('list-active');
                $('.grid').addClass('grid-active');
                $('.prod-cnt').animate({ opacity: 0 });
                $('.prod-cnt').removeClass('dboxlist');
                $('.prod-cnt').addClass('dbox');
                $('.prod-cnt').stop().animate({ opacity: 1 });
            }

            $('#list').click(function () {
                $.cookie('view', 'list');
                get_list()
            });

            $('#grid').click(function () {
                $.cookie('view', 'grid');
                get_grid();
            });

            /* filter */
            $('.menuSwitch ul li').click(function () {
                var CategoryID = $(this).attr('category');
                $('.menuSwitch ul li').removeClass('cat-active');
                $(this).addClass('cat-active');

                $('.prod-cnt').each(function () {
                    if (($(this).hasClass(CategoryID)) == false) {
                        $(this).css({ 'display': 'none' });
                    };
                });

                $('.' + CategoryID).fadeIn();
            });

        });
    </script>

    <script type="text/javascript">
        $(window).load(function () {
            $('#slider').nivoSlider({
                prevText: '',
                nextText: '',
                controlNav: false,
            });
        });
    </script>

    <script>
        $(document).ready(function () {

            // hide #back-top first
            $("#back-top").hide();

            // fade in #back-top
            $(function () {
                $(window).scroll(function () {
                    if ($(this).scrollTop() > 100) {
                        $('#back-top').fadeIn();
                    } else {
                        $('#back-top').fadeOut();
                    }
                });

                // scroll body to 0px on click
                $('#back-top a').click(function () {
                    $('body,html').animate({
                        scrollTop: 0
                    }, 800);
                    return false;
                });
            });

        });
      </script>
      <script type="text/javascript">
      <!--
    function toggle_visibility(id) {
        var e = document.getElementById(id);
        if (e.style.display == 'block') {
            e.style.display = 'none';
            $('#togg').text('show footer');
        }
        else {
            e.style.display = 'block';
            $('#togg').text('hide footer');
        }
    }
    //-->
      </script>

      <script type="text/javascript" src="js/lib/jquery.mousewheel-3.0.6.pack.js"></script>

      <script type="text/javascript">
          $(function () {
              $('a[href*=#]:not([href=#])').click(function () {
                  if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
                      var target = $(this.hash);
                      target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                      if (target.length) {
                          $('html,body').animate({
                              scrollTop: target.offset().top
                          }, 1000);
                          return false;
                      }
                  }
              });
          });
      </script>
      <script src="js/stickUp.min.js" type="text/javascript"></script>
      <script type="text/javascript">
          //initiating jQuery
          jQuery(function ($) {
              $(document).ready(function () {
                  //enabling stickUp on the '.navbar-wrapper' class
                  $('.mWrapper').stickUp();
              });
          });
      </script>
</body>
</html>
