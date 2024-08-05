<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucReloj.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.ucReloj" %>
<div style="float:right; width:auto; font-size: 12px; color: #666 !important; margin-top: 4px; margin-right: 8px;"><img src="<%=Page.ResolveUrl("~/App_Themes/Padrao/css/images/ico_reloj.png")%>" alt="Reloj" /></div>
<div style="float:right; width:auto; font-size: 12px; color: #666 !important; margin-top: 6px; margin-right: 5px;">
    <span id="lblReloj">teste</span>
</div>

<asp:Literal ID="litReloj" runat="server"></asp:Literal>
<script>

    function moveRelogio() {
       
        if (_fechaGMTDelegacion != null) {
            _fechaGMTDelegacion.setSeconds(_fechaGMTDelegacion.getSeconds() + 1);
            document.getElementById('lblReloj').innerHTML = addZero(_fechaGMTDelegacion.getHours()) + ":" + addZero(_fechaGMTDelegacion.getMinutes()) + ":" + addZero(_fechaGMTDelegacion.getSeconds());
            setTimeout("moveRelogio()", 1000);
        }
    }

    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    }

    moveRelogio();

</script>