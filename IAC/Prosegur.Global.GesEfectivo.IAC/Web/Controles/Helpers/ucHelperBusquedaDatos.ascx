<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucHelperBusquedaDatos.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.ucHelperBusquedaDatos" %>

<div id="ucHelper" runat="server" style="position: relative; width:450px; margin-left: 7px; margin-bottom: 7px;">

    <div id="dvTitulo" runat="server" style="width:120px; float:left; margin:5px 2px 0px 2px;">
        <asp:Label runat="server" ID="lblTitulo" CssClass="label2" Visible="False"></asp:Label>
    </div>

    <div id="dvCodigo" runat="server" style="width:90px; float:left;">
        <asp:TextBox ID="txtCodigo" runat="server" onkeydown="javascript: return event.keyCode != 13" Visible="False"
            AutoPostBack="true" MaxLength="15" Style="width: 80px;" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" />
    </div>

    <div id="dvButtonBusqueda" runat="server" style="display: none; width:21px; float:left;" class="helper-div-buttonbusqueda">
        <asp:ImageButton ID="imgButtonBusqueda" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/button/buscar.png" />
    </div>

    <div id="dvDescripcion" runat="server" style="display: none; width:190px; float:left;">
        <asp:TextBox ID="txtDescripcion" runat="server" onkeydown="javascript: return event.keyCode != 13"
            AutoPostBack="true" MaxLength="100" Style="width: 180px;" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" />
    </div>

    <div id="dvButtonLimpaCampo" runat="server" style="display: block; width:21px; float:left; margin:2px 2px 0px 2px; height:auto;">
        <asp:ImageButton ID="imgButtonLimpaCampo" runat="server" ImageUrl="~/Imagenes/Quitar.png"
            CssClass="butondefectoPequeno" />
    </div>
    <div style="clear:both;"></div>
    <div id="dvBoxBusqueda" runat="server" style="display: none; height: auto; width:305px; float:left; margin-left:120px; font-size:8pt;">
        <asp:ListBox ID="lstBoxBusqueda" runat="server" SelectionMode="Multiple" Width="305px" />
    </div>

    <div id="dvButtonRemove" runat="server" style="display: none; margin-left: 2px; width:21px; float:left; ">
        <asp:ImageButton ID="imgButtonRemove" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/iconos/icon_close.png"
            CssClass="butondefectoPequeno" />
    </div>
    <div style="clear:both;"></div>
</div>

<asp:PlaceHolder ID="phUcPopUp" runat="server"></asp:PlaceHolder>
