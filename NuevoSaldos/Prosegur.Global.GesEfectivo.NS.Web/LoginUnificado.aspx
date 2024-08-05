<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginUnificado.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.LoginUnificado" StylesheetTheme="" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Genesis</title>
    <script type="text/javascript" src="./js/jquery.min.js"></script>
    <script type="text/javascript" src="./js/jquery-ui.custom.min.js"></script>
    <script type="text/javascript" src="./js/genesis.js"></script>
    <link rel="stylesheet" href="./App_Themes/Padrao/css/Genesis.css" />
  
    <!--/-->
    <!-- Bootstrap 2 or Bootstrap 3 ?-->
    <!---->
    <link rel="stylesheet" href="./js/Bootstrap/bootstrap-combined.min.css" />
    <!---->
    <!-- Angular 1.1.5 or 1.2.12 ?-->
    <!---->
    <script type="text/javascript" src="./js/Bootstrap/angular.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
        <div id="dvAlert" class="genesisAlert" style="display: none;">
            <div class="fundo"></div>
            <div id="dvAlertPanel" class="painel loading">
                <div id="dvAlertLabel" class="Label">Loading</div>
                <div id="dvAlertClose" class="Close" style="visibility:hidden;">
                    <button type="button" onclick="Javascript: genesisAlertError('','');">x</button>
                </div>
            </div>
            <div id="dvAlertErro" style="display: none;"></div>
        </div>

        

        <script type="text/javascript">

            var msg_loading = 'msg_loading';
            var msg_verificarToken = 'verificarToken';
            var msg_redirecionando = 'redirecionando';
            var msg_producidoError = 'msg_producidoError';
            <asp:Literal ID="litDicionario" runat="server"></asp:Literal>

            function cargarModulo() {

                genesisAlertLoading(msg_loading + msg_verificarToken);
                jQuery.ajax({
                    url: 'LoginUnificado.aspx/cargarModulo',
                    type: "POST",
                    dataType: "json",
                    data: "{token: '" + vtoken + "', UserAgent: '" + vUserAgent + "', UserHostAddress: '" + vUserHostAddress + "'}",
                    contentType: "application/json; charset=utf-8",
                    success: function (data, text) {
                        var json_x = JSON.parse(data.d);
                        if (json_x.CodigoError == "0" && json_x.Respuesta != null) {
                            genesisAlertLoading(msg_loading + msg_redirecionando);
                            window.location.href = urlRedirect;
                        } else {
                            genesisAlertError(json_x.MensajeError, json_x.MensajeErrorDescriptiva);
                        }
                    },
                    error: function (request, status, error) {
                        genesisAlertError(msg_producidoError + error, request.responseText);
                    }
                });
            }
        </script>

        <asp:Literal ID="litVariable" runat="server"></asp:Literal>


    </form>
</body>
</html>
