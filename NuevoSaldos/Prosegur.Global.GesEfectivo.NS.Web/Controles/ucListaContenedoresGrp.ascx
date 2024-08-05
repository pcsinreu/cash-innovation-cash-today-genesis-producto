<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucListaContenedoresGrp.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucListaContenedoresGrp" %>


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
                            <div id="dvCodigo" style="padding: 0px 5px; min-height:23px; width:110px;">
                                <asp:Button runat="server" ID="btnDetalles" OnClick="btnDetalles_Click" onfocus="javascript:seleccionarElemento(this);" Style="color: #2996e2; text-decoration: underline; border: 0px none; font-size: 10px; background: none; cursor: pointer;" />
                            </div>
                            <div id="dvCliente" runat="server" style="border-left: 1px dashed #CCCCCC; padding: 0px 7px; height: auto;  min-height:42px; width: 20%;">
                                <asp:Label ID="lblTituloCliente" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCliente" runat="server" Text=""></asp:Label>
                            </div>
                            <div id="dvCanal" runat="server" style="border-left: 1px dashed #CCCCCC;  padding: 0px 7px; height: auto; min-height:42px; width: 11%;">
                                <asp:Label ID="lblTituloCanal" runat="server"></asp:Label><br />
                                <asp:Label ID="lblCanal" runat="server" Text=""></asp:Label>
                            </div>
                            <div id="dvSector" runat="server" style="border-left: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; min-height:42px; width: 14%;">
                                <asp:Label ID="lblTituloSector" runat="server"></asp:Label><br />
                                <asp:Label ID="lblSector" runat="server" Text=""></asp:Label>
                            </div>
                            <div id="dvObjetos" runat="server" style="border-right: 1px dashed #CCCCCC; border-left: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; min-height:42px; width: 21%;">
                                <asp:Label ID="lblTituloPrecintos" runat="server"></asp:Label><br />
                                <div id="lstItensPrecintos" runat="server" class="BuscarPorvalores" style="height:auto !important; width:auto !important;">
                                    <asp:Literal ID="litPrecintos" runat="server"></asp:Literal>
                                </div>
                            </div>
                            <div id="dvDivisas" style="padding-left: 7px; height: auto; min-height:42px; width: 22%;">
                                <asp:Label ID="lblDivisas" runat="server"></asp:Label><br />
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