<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucListaElementos.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucListaElementos" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucListaElementos.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upListaElementos" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblmensajeVacio" runat="server" Text=""></asp:Label>
        <div style="width: 100%; margin-left: 0px;">
            <div id="dvSeleccionar" runat="server" class="dvCodigoBarra">
                <asp:Label ID="lblSeleccionar" runat="server"></asp:Label>
                <asp:TextBox ID="txtSeleccionar" runat="server" MaxLength="15" onchange="buscarElemento(this);" />
                <img src="../Imagenes/BarCodeSeleccionar.png" style="vertical-align: middle" />
            </div>
            <asp:Repeater ID="rptElementos" runat="server">
                <HeaderTemplate></HeaderTemplate>
                <ItemTemplate>
                    <div class="dvListaElemento" runat="server" id="dvListaElemento">
                        <div style="padding: 0px 5px; width: 100px; height: 35px; border-right: 1px dashed #CCCCCC;">
                            <asp:Button runat="server" ID="btnPrecinto" OnClick="btnPrecinto_Click" onfocus="javascript:seleccionarElemento(this);" Style="color: #2996e2; text-decoration: underline; border: 0px none; font-size: 10px; background: none; cursor: pointer;" />
                            <br />
                            <asp:Label ID="lblCodigoBolsa" runat="server"></asp:Label>
                        </div>
                        <div id="dvTipoServicio" runat="server" style="padding: 0px 5px; width: 120px; height: 35px; border-right: 1px dashed #CCCCCC;">
                            <asp:Label ID="lblTituloTipoServicio" runat="server"></asp:Label><br />
                            <asp:Label ID="lblTipoServicio" runat="server"></asp:Label>
                        </div>
                        <div style="padding: 0px 5px; width: 120px; height: 35px; border-right: 1px dashed #CCCCCC;">
                            <asp:Label ID="lblTituloFormatoObjeto" runat="server"></asp:Label><br />
                            <asp:Label ID="lblFormatoObjeto" runat="server"></asp:Label>
                        </div>
                        <div id="dvCantidad" runat="server" style="padding: 0px 5px; width: 120px; height: 35px; border-right: 1px dashed #CCCCCC;">
                            <asp:Label ID="lblTituloCantidadObjeto" runat="server"></asp:Label><br />
                            <asp:Label ID="lblCantidad" runat="server"></asp:Label>
                        </div>
                        <div style="padding-left: 7px; width: 425px; height: auto;">
                            <asp:Label ID="lblTituloTipoValor" runat="server"></asp:Label><br />
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
                        <div style="height: auto; float: right;">
                            <asp:ImageButton OnClick="btnQuitar_Click" ID="btnQuitar" runat="server" ImageUrl="~/Imagenes/Quitar.png" Style="border: 0px none; font-size: 10px; background: none;" onfocus="javascript:seleccionarElemento(this);" />
                        </div>
                        <div class="dvclear">
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <div style="padding: 5px;">
                        <div style="position: relative; width: 100%;">
                            <div id="dvTotales" runat="server" style="float: left; font-weight: bold; font-size: 13px; margin-right:5px;"></div>
                            <asp:Repeater ID="rptTotalDivisas" runat="server" OnItemDataBound="rptTotalDivisas_ItemDataBound" OnItemCreated="rptTotalDivisas_ItemCreated">
                                <ItemTemplate>
                                    <div runat="server" id="dvTotalDivisa" style="float: left; margin-right: 5px; font-size: 13px;"></div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div class="dvclear"></div>
                                    <asp:Label ID="lblTituloDivisas" runat="server"></asp:Label><asp:Label ID="lblDivisas" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblTituloCantidad" runat="server"></asp:Label><asp:Label ID="lblCantidad" runat="server"></asp:Label><br />
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
