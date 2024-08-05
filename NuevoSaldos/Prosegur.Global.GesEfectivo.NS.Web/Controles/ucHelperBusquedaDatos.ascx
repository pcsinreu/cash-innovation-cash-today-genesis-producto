<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucHelperBusquedaDatos.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucHelperBusquedaDatos" %>
        <div id="ucHelper" runat="server" class="classHelper">
            <div class="dvUsarFloat" style="margin: 0px;">
                <div id="dvValor" runat="server" style="display: none; margin-right: 10px;">
                    <asp:Label ID="lblTituloValor" runat="server" /><br />
                    <asp:Label ID="lblValor" runat="server" CssClass="valor" />
                </div>
                <div id="dvCodigo" runat="server" style="display: none;">
                    <asp:Label ID="lblTitulo" runat="server" /><br />
                    <asp:TextBox ID="txtCodigo" autocomplete="txtCodigo" runat="server" onkeydown="javascript: return event.keyCode != 13"
                        AutoPostBack="true" MaxLength="100" Style="width: 100px;" />
                </div>
                <div id="dvButtonBusqueda" runat="server" style="display: none; margin: 12px 2px 0px 2px; height: auto;">
                    <asp:ImageButton ID="imgButtonBusqueda" runat="server" ImageUrl="~/Imagenes/Lupa.png"
                        CssClass="butondefectoPequeno" />
                </div>
                <div id="dvDescripcion" runat="server" style="display: none;">
                    <br />
                    <asp:TextBox ID="txtDescripcion" runat="server" onkeydown="javascript: return event.keyCode != 13"
                        AutoPostBack="true" MaxLength="100" Style="width: 215px;" />
                </div>
                <div id="dvButtonLimpaCampo" runat="server" style="display: none; margin: 12px 0px 0px 2px; height: auto;">
                    <asp:ImageButton ID="imgButtonLimpaCampo" runat="server" ImageUrl="~/Imagenes/Quitar.png"
                        CssClass="butondefectoPequeno" />
                </div>
                <div id="dvBoxBusqueda" runat="server" style="display: none; height: auto;">
                    <asp:ListBox ID="lstBoxBusqueda" runat="server" SelectionMode="Multiple" Width="347px" />
                </div>
                <div id="dvButtonRemove" runat="server" style="display: none; margin-left: 2px;">
                    <asp:ImageButton ID="imgButtonRemove" runat="server" ImageUrl="~/App_Themes/Padrao/css/img/iconos/icon_close.png"
                        CssClass="butondefectoPequeno" />
                </div>
                <div class="dvclear">
                </div>
            </div>
        </div>
        <asp:PlaceHolder ID="phUcPopUp" runat="server"></asp:PlaceHolder>
