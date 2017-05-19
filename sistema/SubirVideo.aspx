<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubirVideo.aspx.cs" Inherits="SubirVideo" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="~/sistema/partes/header.ascx" TagPrefix="uc1" TagName="ucHeader" %>
<%@ Register Src="~/sistema/partes/footer.ascx" TagPrefix="uc3" TagName="ucFooter" %>

<uc1:ucHeader runat="server" ID="ucHeader" />
<form id="form2" runat="server">
    <asp:ScriptManager runat="server" ID="sm1" />
    <div id="page-wrapper" style="min-height: 292px; padding-top:130px;">  
        <div class="row text-center">
            <h4>Subir Videos</h4>
        </div>
        <div class="row"><br /></div>
        <div class="row form-group">
            <div class="col s2"></div>
            <div class="col s8">
                <div class="row text-center form-group">
                    <div class="col s6">
                        <asp:Label runat="server" ID="lblUltimo" CssClass="control-label"  Text="Último Video Subido: "></asp:Label>
                    </div>
                    <div class="col s6">
                        <asp:Label runat="server" CssClass="control-label text-info" Text="Prueba..." ID="lblFechaUltimo"></asp:Label>
                    </div>
                </div>
                <div class="row text-center form-group">
                    <div class="col s6">
                        <asp:Label runat="server" ID="Label1" CssClass="control-label"  Text="Seleccione la Clase"></asp:Label>
                    </div>
                    <div class="col s6">
                        <asp:DropDownList runat="server" CssClass="browser-default col-s6" ID="dplClases"></asp:DropDownList>
                    </div>                    
                </div>
                <div class="row text-center form-group">
                    <div class="col s6">
                        <asp:FileUpload runat="server" ID="flpVideos" CssClass="" />
                    </div>
                    <div class="col s6">
                        <asp:Button runat="server" CssClass="waves-effect waves-light btn" ID="btnSubir" Text="Subir" OnClick="btnSubir_Click" />
                    </div>
                </div>
            </div>
            <div class="col s2"></div>
        </div>
    </div>
     <uc3:ucFooter runat="server" ID="ucFooter" />

    <script type="text/javascript">

    </script>
</form> 