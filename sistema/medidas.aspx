<%@ Page Language="C#" AutoEventWireup="true" CodeFile="medidas.aspx.cs" Inherits="sistema_medical_medidas" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h5>Medidas</h5>
        </div>
        <div class="row"><br /></div>
        <div class="row form-group">
            <p>Vamos a capturar de manera regular tus medidas y vas a poder llevar el registro del impacto de tu actividad en las medidas de tu cuerpo.</p>
        </div>
        <div class="row">
            <div class="col-lg-3"></div>
            <div class="col-lg-2 text-center">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/FigMedidas.png" /> 
            </div>
            <div class="col-lg-4">
                <div class="row form-group">
                    <div class="col-lg-8 text-right">
                        <asp:Label runat="server" ID="Label3" CssClass="text-1" Text="Talla"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblTalla" CssClass="text-primary"></asp:Label>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-8 text-right">
                        <asp:Label runat="server" ID="Label1" CssClass="text-1" Text="Circunferencia de Cuello"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblCirCuello" CssClass="text-primary"></asp:Label>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-8 text-right">
                        <asp:Label runat="server" ID="Label2" CssClass="text-1" Text="Circunferencia de Pecho"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblCirPecho" CssClass="text-primary"></asp:Label>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-8 text-right">
                        <asp:Label runat="server" ID="Label4" CssClass="text-1" Text="Circunferencia de Cintura"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblCirCintura" CssClass="text-primary"></asp:Label>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-8 text-right">
                        <asp:Label runat="server" ID="Label5" CssClass="text-1" Text="Índice Cintura Cadera"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblIndCad" CssClass="text-primary"></asp:Label>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-8 text-right">
                        <asp:Label runat="server" ID="Label6" CssClass="text-1" Text="Circunferencia de la cabeza"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblCirCabeza" CssClass="text-primary"></asp:Label>
                    </div>
                </div>
                <div class="row form-group">
                    <div class="col-lg-8 text-right">
                        <asp:Label runat="server" ID="Label7" CssClass="text-1" Text="Circunferencia de Cadera"></asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblCirCade" CssClass="text-primary"></asp:Label>
                    </div>
                </div>
            </div>    
            <div class="col-lg-3"></div>    
        </div>
        <div class="row form-group">            
            <ul class="collapsible" data-collapsible="accordion">
                <li>
                    <div class="collapsible-header"><i class="material-icons">filter_drama</i>Historico de Medidas</div>
                    <div class="collapsible-body">
                        <table>
                        <thead>
                          <tr>
                              <th>Fecha</th>
                              <th>Talla</th>
                              <th>Cir. Cuello</th>
                              <th>Cir. Pecho</th>
                              <th>Cir. Cintura</th>
                              <th>Cir. Cintura-Cadera</th>
                              <th>Cir. Cabeza</th>
                              <th>Cir. Cadera</th>
                          </tr>
                        </thead>
                        <tbody>
                          <tr>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                          </tr>
                          <tr>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                            <td> - </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                </li>
            </ul>        
        </div>
    </div>
     <uc3:ucFooter runat="server" ID="ucFooter" />

    <script type="text/javascript">

    </script>
</form>
