<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucDatosDocumento.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucDatosDocumento" %>
<div id="ucDatosDocumento" runat="server">
    <div class="dvUsarFloat">
        <div id="dvimgFormulario" runat="server" class="foto" style="display: none;">
            <asp:Literal ID="litimgFormulario" runat="server"></asp:Literal>
        </div>
        <div id="dvDescFormulario" runat="server" style="display: none;">
            <asp:Label ID="lblDescFormulario" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="txtDescFormulario" runat="server" Text="" CssClass="valor"></asp:Label>
        </div>
        <div id="dvTipoDocumento" runat="server" style="display: none;">
            <asp:Label ID="lblTipoDocumento" runat="server" Text=""></asp:Label><br />
            <asp:Label ID="txtTipoDocumento" runat="server" Text="" CssClass="valor"></asp:Label>
        </div>
        <div id="dvEstado" runat="server" style="display: none;">
            <div style="margin: 0px 5px 0px 0px;">
                <asp:Label ID="lblEstado" runat="server" Text="" /><br />
                <asp:Label ID="txtEstado" runat="server" Text="" CssClass="valor"></asp:Label>
            </div>
            <div id="dvHistEstado" runat="server" style="display: none; margin-top: 8px; height: 32px;">
                <asp:Image ID="imgHistEstado" runat="server" ImageUrl="../Imagenes/Detalle.png" AlternateText="" onclick="ModalHistorico(true)" CssClass="imgLink" />
            </div>
        </div>

        <div id="dvCodComprobante" runat="server" style="display: none;">
            <asp:Label ID="lblCodComprobante" runat="server" Text="" /><br />
            <asp:Label ID="txtCodComprobante" runat="server" Text="" CssClass="valor"></asp:Label>
        </div>
        <div id="dvConsultaSaldos" runat="server" style="display: none;">
            <div style="margin: 0px 5px 0px 0px;">
                <br />
                <input type="button" value="" id="btnConsultaSaldos" class="valor" runat="server" style="border: 0px none; background: none; cursor: pointer; color: #767676;" />
            </div>
            <div id="Div1" runat="server" style="display: block; margin-top: 8px; height: 32px;">
                <asp:Image ID="Image1" runat="server" ImageUrl="../Imagenes/ICO_SALDOCUENTA_16x16.png" AlternateText="" />
            </div>
        </div>

        <div id="dvFechaHoraPlanificacionCertificacion" runat="server" style="display: none;">
            <asp:Label ID="lblFechaHoraPlanificacionCertificacion" runat="server" Text="" /><br />
            <asp:TextBox ID="txtFechaHoraPlanificacionCertificacion" runat="server" Enabled="false"></asp:TextBox>
        </div>
        <div id="dvEstaCertificado" runat="server" style="display: none;">
            <asp:Image ID="imgCertificacion" runat="server" Style="vertical-align: middle;" />
            <asp:Label ID="lblEstaCertificado" runat="server" Text="" />
        </div>
        <div id="dvSaldoSuprimido" runat="server" style="display: none;">
            <asp:Image ID="imgSaldoSuprimido" runat="server" Style="vertical-align: middle;" />
            <asp:Label ID="lblSaldoSuprimido" runat="server" Text="" />
        </div>
        <div id="dvNotificado" runat="server" style="display: none;">
            <asp:Image ID="imgNotificado" runat="server" Style="vertical-align: middle;" />
            <asp:Label ID="lblNotificado" runat="server" Text="" />
        </div>

        <div class="dvclear">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dvNumeroExterno" runat="server" style="display: none;">
                    <asp:Label ID="lblNumeroExterno" runat="server" Text="" /><br />
                    <asp:TextBox ID="txtNumeroExterno" runat="server" MaxLength="50" Enabled="false" Style="width: 410px;" />
                </div>
         
            </ContentTemplate>
        </asp:UpdatePanel>
                <div id="dvFechaHoraGestion" runat="server" style="display: none;">
                    <asp:Label ID="lblFechaHoraGestion" runat="server" Text="" /><br />
                    <asp:TextBox ID="txtFechaHoraGestion" runat="server" Enabled="false"></asp:TextBox>
                </div>
                       
         <asp:UpdatePanel ID="uppNumeroExterno" runat="server">
            <ContentTemplate>
                <div id="dvPrecinto" runat="server" style="display: none;">
                    <asp:Label ID="lblPrecinto" runat="server" Text="Precinto" /><br />
                    <asp:TextBox ID="txtPrecinto" runat="server" MaxLength="12" Enabled="true" Style="width: 100px;" AutoPostBack="True" />
                </div>
                <div id="dvSecuencia" runat="server" style="display: none; margin-left:10px;">
                    <asp:Label ID="lblSecuencia" runat="server" Text="Secuencia" /><br />
                    <asp:TextBox ID="txtSecuencia" runat="server" MaxLength="2" Enabled="true" Style="width: 60px; text-align: right;" onkeypress="return bloqueialetras(event,this);" AutoPostBack="True" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="dvclear">
    </div>
    <div id="dvHistorico" style="display: none;">
        <div class="ui-widget-overlay ui-front"></div>
        <div class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-front ui-draggable" style="height: auto; width: 778px; top: 21.5px; left: 290.5px; display: block;"
            tabindex="-1" role="dialog">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span id="ui-id-53" class="ui-dialog-title" style="color: #5e5e5e">
                    <asp:Literal ID="litTituloHistorico" runat="server"></asp:Literal></span>
                <div style="position: absolute; right: .3em; border-style: none; cursor: pointer;" onclick="ModalHistorico(false)">
                    <span class="ui-button-icon-primary ui-icon ui-icon-closethick"></span>
                </div>
            </div>
            <div id="dvHistoricoConteudo" style="width: auto; min-height: 0px; max-height: none; height: auto;" class="ui-dialog-content ui-widget-content">
                <asp:GridView ID="GridHistorico" runat="server" AllowPaging="False" BorderStyle="None" Width="100%" CssClass="GridDefecto"></asp:GridView>
                <%--<center>
                <input type="button"  name="btnVoltar" value="Cerrar" id="btnVoltar" tabindex="54" class="ui-button ui-button-text-only butondefecto" onclick="visualizarHistorico()" /></center>--%>
            </div>
        </div>

    </div>

    <script type="text/javascript">
        //function ModalHistorico(modal) {
        //    var objHistorico = document.getElementById("dvHistorico");
        //    if (objHistorico != undefined) {
        //        if (modal == true) {
        //            objHistorico.style.display = 'block';
        //        } else {
        //            objHistorico.style.display = 'none';
        //        }
        //    }
        //    //if (objHistorico.style.display == 'block') { objHistorico.style.display = 'none'; } else { objHistorico.style.display = 'block'; }

        //}

        //document.onkeydown = function (e) {
        //    if (e.which === 27) {
        //        ModalHistorico(false);
        //    }
        //};

    </script>
</div>
