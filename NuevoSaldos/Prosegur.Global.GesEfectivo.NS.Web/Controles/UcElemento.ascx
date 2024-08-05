<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UcElemento.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.UcElemento" %>
<div class="ui-panel-titlebar">
    <asp:Label ID="lblTitulo" runat="server" Text="" SkinID="Titulo" />
</div>
<div class="dvUsarFloat" style="margin-left: 5px;">
    <div id="divCodigoExterno" runat="server" style="display: none;">
        <asp:Label ID="lblCodigoExterno" runat="server" Text="" /><br />
        <asp:TextBox ID="txtCodigoExterno" MaxLength="15" Enabled="false" runat="server"
            Style="width: 355px;" />
    </div>
    <div id="divPrecinto" runat="server" style="display: none;">
        <asp:Label ID="lblPrecinto" runat="server" Text="" /><br />
        <asp:TextBox ID="txtPrecinto" runat="server" MaxLength="50" Style="width: 100px;" />
    </div>
    <div id="divTipo" runat="server" style="display: none;">
        <asp:Label ID="lblTipo" runat="server" Text="" /><br />
        <asp:DropDownList runat="server" ID="ddlTipo" SkinID="form-dropdownlist-mandatory">
        </asp:DropDownList>
    </div>
    <div id="divTipoFormato" runat="server" style="display: none;">
        <asp:Label ID="lblTipoFormato" runat="server" Text="" /><br />
        <asp:DropDownList runat="server" ID="ddlTipoFormato" SkinID="form-dropdownlist-mandatory">
        </asp:DropDownList>
    </div>
    <div id="divFechaAlta" runat="server" style="display: none;">
        <asp:Label ID="lblFechaAlta" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblFechaAltaValor" runat="server" Text="" CssClass="valor"></asp:Label>
    </div>
    <div id="divFechaTransporte" runat="server" style="display: none;">
        <asp:Label ID="lblFechaTransporte" runat="server" Text="" /><br />
        <asp:TextBox ID="txtFechaTransporte" runat="server" MaxLength="50" Enabled="false"
            Style="width: 100px;" />
    </div>
    <div id="divCodContenedor" runat="server" style="display: none;">
        <asp:Label ID="lblCodContenedor" runat="server" Text="" /><br />
        <asp:TextBox ID="txtCodContenedor" runat="server" Style="width: 100px;" />
    </div>
    <div id="divCodRuta" runat="server" style="display: none;">
        <asp:Label ID="lblCodRuta" runat="server" Text="" /><br />
        <asp:TextBox ID="txtCodRuta" MaxLength="15" Enabled="false" runat="server" Style="width: 100px;" />
    </div>
    <div id="divPrecintoPadre" runat="server" style="display: none;">
        <asp:Label ID="lblPrecintoPadre" runat="server" Text="" /><br />
        <asp:TextBox ID="txtPrecintoPadre" MaxLength="50" Enabled="false" runat="server"
            Style="width: 100px;" />
    </div>
    <div id="divCodigoBolsa" runat="server" style="display: none;">
        <asp:Label ID="lblCodigoBolsa" runat="server" Text=""></asp:Label><br />
        <asp:TextBox ID="txtCodigoBolsa" MaxLength="50" Enabled="false" runat="server" Style="width: 100px;" />
    </div>
    <div id="divNelParada" runat="server" style="display: none;">
        <asp:Label ID="lblNelParada" runat="server" Text=""></asp:Label><br />
        <asp:TextBox ID="txtNelParada" MaxLength="10" Enabled="false" runat="server" Style="width: 100px;" />
    </div>
    <div id="divCodigoATM" runat="server" style="display: none;">
        <asp:Label ID="lblCodigoATM" runat="server" Text=""></asp:Label><br />
        <asp:TextBox ID="txtCodigoATM" MaxLength="20" Enabled="false" runat="server" Style="width: auto;" />
    </div>
    <div id="dvCantidadParcialesAvancado" runat="server" style="display: none;">
        <asp:Label ID="lblCantidadParcialesAvancado" runat="server"></asp:Label><br />
        <asp:TextBox ID="txtCantidadParcialesAvancado" runat="server" MaxLength="15" onkeypress="return bloqueialetras(event,this);"/>
    </div>
    <div id="dvConfiguracionNivelDetalle" runat="server" style="display: none;">
        <asp:Label ID="lblConfiguracionNivelDetalle" runat="server"></asp:Label><br />
        <asp:TextBox ID="txtConfiguracionNivelDetalle" runat="server" MaxLength="15" Enabled="false" />
    </div>
    <div class="dvclear">
    </div>
</div>
<div id="divListaElementos" runat="server" style="margin-left: 0px; margin-top: 5px;">
    <fieldset style="border: none; margin-left: 0px !important; padding-left: 2px;">
        <legend id="lgsubtitulobar" class="subtitulobar" style="margin-left: 0px !important;">
            <span class="subtitulobar-alternar iconesubtitulo iconesubtitulo-menor"></span>
            <asp:Label ID="lblTituloElemento" runat="server" Text="Lista de Objectos" SkinID="SubTitulo" />
        </legend>

        <div style="margin-left: 10px !important;">
            <div class="subtitulobar-barra">
            </div>

            <div id="ucListaElementos" runat="server" style="width: 98%; color: #767676; padding: 5px 0px 0px 0px;">
                <!-- Inicio: ucAgregarElemento -->
                <div id="dvAgregarElemento" runat="server" class="dvAgregarElemento">
                    <div>
                        <div class="ui-panel-titlebar" style="background-image: none !important; width: 98%; height: auto;">
                            <asp:Label ID="lblTituloAgregar" runat="server" Style="color: #767676 !important; font-size: 9pt;" />
                        </div>
                        <div id="dvPrecinto" runat="server">
                            <asp:Label ID="lblPrecintoAgregar" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtPrecintoAgregar" runat="server" MaxLength="15" />
                        </div>
                        <div id="dvCodigoBolsa" runat="server">
                            <asp:Label ID="lblCodigoBolsaAgregar" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtCodigoBolsaAgregar" runat="server" MaxLength="15" />
                        </div>
                        <div id="dvTipoServicio" runat="server">
                            <asp:Label ID="lblTipoServicio" runat="server" /><br />
                            <asp:DropDownList ID="ddlTipoServicio" runat="server" Style="width: 130px;"></asp:DropDownList>
                        </div>
                        <div id="dvFormato" runat="server">
                            <asp:Label ID="lblFormato" runat="server" /><br />
                            <asp:DropDownList ID="ddlFormato" runat="server" Style="width: 130px;"></asp:DropDownList>
                        </div>
                        <div id="divCantidadParciales" runat="server">
                            <asp:Label ID="lblCantidadParciales" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtCantidadParciales" runat="server" MaxLength="14" onkeypress="return bloqueialetras(event,this);"/>
                        </div>
                        <div id="dvBoton" runat="server" style="float: right;">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" class="ui-button" Width="100" />
                            <asp:Button ID="btnAvanzado" runat="server" Text="Avanzado" class="ui-button" Width="100" />
                        </div>
                        <div class="dvclear">
                        </div>
                    </div>
                    <div>
                        <div id="dvDivisa" runat="server">
                            <asp:Label ID="lblDivisa" runat="server" /><br />
                            <asp:DropDownList ID="ddlDivisa" runat="server" Style="width: 130px;"></asp:DropDownList>
                        </div>
                        <div id="dvTotalEfectivo" runat="server">
                            <asp:Label ID="lblTotalEfectivo" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtTotalEfectivo" runat="server" MaxLength="15" onkeyup="moedaIAC(event,this,',','.');" onkeypress="return bloqueialetrasImporte(event, this);" />
                        </div>
                        <div id="dvTotalCheque" runat="server">
                            <asp:Label ID="lblTotalCheque" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtTotalCheque" runat="server" MaxLength="15" onkeyup="moedaIAC(event,this,',','.');" onkeypress="return bloqueialetrasImporte(event, this);" />
                        </div>
                        <div id="dvTotalTicket" runat="server">
                            <asp:Label ID="lblTotalTicket" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtTotalTicket" runat="server" MaxLength="15" onkeyup="moedaIAC(event,this,',','.');" onkeypress="return bloqueialetrasImporte(event, this);" />
                        </div>
                        <div id="dvTotalTarjeta" runat="server">
                            <asp:Label ID="lblTotalTarjeta" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtTotalTarjeta" runat="server" MaxLength="15" onkeyup="moedaIAC(event,this,',','.');" onkeypress="return bloqueialetrasImporte(event, this);" />
                        </div>
                        <div id="dvTotalOtros" runat="server">
                            <asp:Label ID="lblTotalOtros" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtTotalOtros" runat="server" MaxLength="15" onkeyup="moedaIAC(event,this,',','.');" onkeypress="return bloqueialetrasImporte(event, this);" />
                        </div>
                        <div class="dvclear">
                        </div>
                    </div>
                </div>
                <!-- Fim: ucAgregarElemento -->
                <asp:PlaceHolder runat="server" ID="phControleListaElementos"></asp:PlaceHolder>
            </div>
        </div>
    </fieldset>
    <div style="margin-top: 10px;">
        <asp:PlaceHolder runat="server" ID="phControleTermino"></asp:PlaceHolder>
    </div>
</div>
<div style="margin-left: 0px; margin-top: 5px;">
    <div runat="server" visible="false" id="divValoresDeclarados">
        <asp:PlaceHolder runat="server" ID="phControleEfectivoDeclarado"></asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phControleMedioPagoDeclarado"></asp:PlaceHolder>
    </div>
    <div runat="server" visible="false" id="divValoresContados">
        <asp:PlaceHolder runat="server" ID="phControleEfectivoContado"></asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phControleMedioPagoContado"></asp:PlaceHolder>
    </div>
    <div runat="server" visible="false" id="divDiferencias">
        <asp:PlaceHolder runat="server" ID="phControleEfectivoDiferencias"></asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phControleMedioDiferencias"></asp:PlaceHolder>
    </div>
</div>
<asp:HiddenField ID="hdnCodigoExterno" runat="server" />
<asp:Literal ID="litScriptCodigoExterno" runat="server"></asp:Literal>
