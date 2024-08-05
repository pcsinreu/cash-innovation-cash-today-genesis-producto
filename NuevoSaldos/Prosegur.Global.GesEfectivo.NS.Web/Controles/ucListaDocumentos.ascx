<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucListaDocumentos.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucListaDocumentos" %>

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

                <asp:Repeater ID="rptDocumentos" runat="server">
                    <HeaderTemplate></HeaderTemplate>
                    <ItemTemplate>
                        <div id="dvListaValores" runat="server" class="dvListaElemento" style="width:97%;">
                            <div id="dvIcone" style="padding: 0px 5px; min-height:23px; width:23px;">
                                <asp:ImageButton ID="btnDetalles" runat="server" OnClick="btnDetalles_Click" ImageUrl="~/Imagenes/ICO_VIEW_DOCUMENTO_16x16.gif" onfocus="javascript:seleccionarElemento(this);" Style="border: 0px none; background: none;" />
                            </div>
                            <div id="dvCuentaOrigen" runat="server" style="border-right: 1px dashed #CCCCCC; border-left: 1px dashed #CCCCCC; padding: 0px 7px; height: auto;  min-height:23px; width: 33%;">
                                <asp:Label ID="lblCuentaOrigen" runat="server" Text=""></asp:Label><br />
                            </div>
                            <div id="dvCuentaDestino" runat="server" style="border-right: 1px dashed #CCCCCC; border-left: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; min-height:23px; width: 25%;">
                                <asp:Label ID="lblCuentaDestino" runat="server" Text=""></asp:Label><br />
                            </div>
                            <div id="dvDivisas" style="padding-left: 7px; height: auto; min-height:23px; max-height:20%; width: 35%;">
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
                            <div id="dvQuitar" style="height: auto;  min-height:23px; width: 23px;">
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