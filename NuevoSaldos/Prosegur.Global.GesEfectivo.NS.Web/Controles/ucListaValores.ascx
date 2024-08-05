<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucListaValores.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucListaValores" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucListaElementos.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upListaValores" runat="server">
    <ContentTemplate>
        <div id="ucListaValores" runat="server" style="width: 98%; color: #767676; padding: 0px 0px 10px 0px;">
            <div class="ui-panel-titlebar" id="dvTitulo" runat="server">
                <asp:Label ID="lblTituloValores" runat="server" Text="Lista de Valores" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>
            <div style="width: 100%; margin-left: 5px; margin-top: 5px;">

                <asp:Label ID="lblmensajeVacio" runat="server" Text=""></asp:Label>

                <div id="dvSeleccionarTodos" runat="server">
                    <asp:Button ID="btnAgregarTodos" runat="server" Text="Seleccionar todos" CssClass="BotonAgregarTodos" />
                </div>

                <asp:Repeater ID="rptValores" runat="server">
                    <HeaderTemplate></HeaderTemplate>
                    <ItemTemplate>
                        <div id="dvListaValores" runat="server" class="dvListaElemento">
                            <div style="padding: 0px 5px; height: auto; max-width:30%">
                                <asp:Repeater ID="rptComprobante" runat="server" OnItemDataBound="rptComprobante_ItemDataBound">
                                    <ItemTemplate>
                                        <div style="padding-bottom: 5px; border: 1px solid; border-radius: 5px; margin-right: 2px; margin-bottom:4px">
                                            <asp:LinkButton Font-Overline="false" runat="server" ID="linkComprobante" Font-Bold="true" OnCommand="linkComprobante_Command"></asp:LinkButton>
                                        </div>
                                       
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div id="dvIcone" style="padding: 0px 5px; width: 70px; height: auto;">
                                <asp:ImageButton ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" ImageUrl="~/Imagenes/Agregar.png" onfocus="javascript:seleccionarElemento(this);" Style="border: 0px none; background: none;" />
                                <asp:ImageButton ID="btnDetalles" runat="server" OnClick="btnDetalles_Click" ImageUrl="~/Imagenes/ICO_VIEW_DOCUMENTO_16x16.gif" onfocus="javascript:seleccionarElemento(this);" Style="border: 0px none; background: none;" />
                                <asp:ImageButton ID="btnSaldoCuenta" runat="server" OnClick="btnSaldoCuenta_Click" ImageUrl="~/Imagenes/ICO_SALDOCUENTA_16x16.png" onfocus="javascript:seleccionarElemento(this);" Style="border: 0px none; background: none;" />
                                <asp:ImageButton ID="btnSaldoHijos" runat="server" OnClick="btnSaldoHijos_Click" ImageUrl="~/Imagenes/ICO_SALDOCUENTA_16x16.png" onfocus="javascript:seleccionarElemento(this);" Style="border: 0px none; background: none;" Visible="false" />
                            </div>
                            <div id="dvCuenta" runat="server" style="border-right: 1px dashed #CCCCCC; border-left: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; max-width: 50%;">
                                <asp:Label ID="lblCuenta" runat="server" Text=""></asp:Label><br />
                            </div>
                            <div id="dvDivisas" style="padding-left: 7px; height: auto; max-height:20%;">
                                <div class="divisas">
                                    <asp:Repeater ID="rptDivisas" runat="server" OnItemDataBound="rptDivisas_ItemDataBound">
                                        <ItemTemplate>
                                            <div id="dvDivisa" runat="server"></div>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <div id="dvDivisa" runat="server"></div>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div id="dvQuitar" style="height: auto; float: right;">
                                <asp:ImageButton OnClick="btnQuitar_Click" ID="btnQuitar" runat="server" ImageUrl="~/Imagenes/Quitar.png" Style="border: 0px none; font-size: 10px; background: none;" onfocus="javascript:seleccionarElemento(this);" />
                            </div>
                            <div class="dvclear">
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div style="padding: 5px;">
                            <div style="font-size: 13px; position: relative; width: 100%;">
                                <div id="dvTotales" runat="server" style="float: left; margin-right: 5px;"></div>
                                <asp:Repeater ID="rptTotalDivisas" runat="server" OnItemDataBound="rptTotalDivisas_ItemDataBound" OnItemCreated="rptTotalDivisas_ItemCreated">
                                    <ItemTemplate>
                                        <div runat="server" id="dvTotalDivisa" style="float: left; margin-right: 5px;"></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div class="dvclear"></div>
                                        <asp:Label ID="lblTituloDivisas" runat="server"></asp:Label><asp:Label ID="lblDivisas" runat="server"></asp:Label><br />
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
