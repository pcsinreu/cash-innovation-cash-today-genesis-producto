<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="PantallaVisualizar.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Abono.PantallaVisualizar" %>

<%@ Import Namespace="Prosegur.Framework.Dicionario" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%=Page.ResolveUrl("~/js/Abono/Busqueda.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content" data-bind='with: Abono'>
        <div>
            <div class="pnl_title">
                <span><%= Tradutor.Traduzir("071_Abono_Titulo_TituloDefinicionProceso")%></span>
                <input type="button" class="butonFiltroBusqueda" disabled="disabled" />
            </div>
            <div class="dvclear"></div>
            <div class="dvForm">
                <div>
                    <span><%= Tradutor.Traduzir("071_Comon_Campo_Banco")%></span><br>
                    <asp:Label ID="lblBanco" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
                <div>
                    <span><%= Tradutor.Traduzir("071_Comon_Campo_TipoAbono")%></span><br>
                    <asp:Label ID="lblTipoAbono" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
                <div id="dvValores" runat="server">
                    <span><%= Tradutor.Traduzir("071_Comon_Campo_Valores")%></span><br>
                    <asp:Label ID="lblValores" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
                <div>
                    <span><%= Tradutor.Traduzir("071_Comon_Campo_Estado")%></span><br>
                    <asp:Label ID="lblEstado" runat="server" Text="" CssClass="valor"></asp:Label>
                </div>
            </div>
            <div class="dvclear"></div>
        </div>
        <div>
            <div class="pnl_title">
                <asp:Label ID="lblTituloAbonos" runat="server" Text=""></asp:Label>
                <input type="button" class="butonFiltroBusqueda" disabled="disabled" />
            </div>
            <div class="dvclear"></div>
            <div id="dvAbonos" runat="server" class="gridAbono" style="display:none; margin:7px; width: 97.5% !important">
                <table>
                    <thead>
                        <asp:Literal ID="litAbonosHead" runat="server"></asp:Literal>
                    </thead>
                    <tbody>
                        <asp:Literal ID="litAbonosBody" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <div>
                <div id='numberCountdown' style="margin-left:20px; color: #9c9c9c;"></div>
            </div>
            <div>
                <div id='dvReporteError' style="margin-left:20px; color: #d46464;">
                    <asp:Literal ID="litReporteError" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <asp:Literal ID="litDicionario" runat="server"></asp:Literal>
    </div>

    <script type="text/javascript">

        var g_iCount = new Number();
        var g_iCount = 30;

        function startCountdown() {
            if ((g_iCount - 1) >= 0) {
                g_iCount = g_iCount - 1;
                numberCountdown.innerHTML = _Diccionario.msg_actualizaAutomatico + ' <span style="color: #444444;">' + g_iCount + ' ' + _Diccionario.msg_segundos + '</span>';
                setTimeout('startCountdown()', 1000);
            } else {
                location.reload();
            }
        }

    </script>
    <asp:Literal ID="litScript" runat="server"></asp:Literal>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div class="dvForm dvFormNO">
        <div style="margin: 0px; width: 100%;">
            <center>
                <asp:Literal ID="litBotones" runat="server"></asp:Literal>
            </center>
        </div>
        <div class="dvclear"></div>
    </div>
</asp:Content>


