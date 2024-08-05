<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucElementosSelecionados.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucElementosSelecionados" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucListaElementos.js" />
    </Scripts>
</asp:ScriptManagerProxy>

<script type="text/javascript">
    function limparSeleccionar(id) {
        var txt = document.getElementById(id);
        if (txt != null) {
            txt.value = '';
        }
    }
</script>
<asp:UpdatePanel ID="upElementosSelecionados" runat="server">
    <ContentTemplate>
        <div id="ucListaElementos" runat="server" style="width: 98%; color: #767676; padding: 0px 0px 10px 0px;">

            <div class="ui-panel-titlebar" id="dvTitulo" runat="server">
                <asp:Label ID="lblTitulo" runat="server" Text="" SkinID="filtro-label_titulo" Style="color: #767676 !important;" />
            </div>

            <div style="width: 100%; margin-left: 5px; padding-top: 5px;">
                <asp:Label ID="lblmensajeVacio" runat="server" Text=""></asp:Label>

                <div id="dvSeleccionar" runat="server" class="dvCodigoBarra">
                    <asp:Label ID="lblSeleccionar" runat="server"></asp:Label>
                    <asp:TextBox ID="txtSeleccionar" runat="server" MaxLength="15" CssClass="txtEliminar" onchange="javascript:quitarSelecionado(this.value, this.id);" onkeypress="javascript:if (event.keyCode == 13) quitarSelecionado(this.value, this.id);" />
                    <img src="../Imagenes/BarCodeSeleccionar.png" style="vertical-align: middle" />
                </div>
                <asp:Repeater ID="rptElementos" runat="server">
                    <HeaderTemplate></HeaderTemplate>
                    <ItemTemplate>
                        <div id="dvListaElementos" runat="server" class="dvListaElemento">
                            <div id="dvSaldoCuenta" runat="server" style="padding: 5px 5px 0px 5px; width: 10px; height: auto;">
                                <asp:ImageButton ID="btnSaldoCuenta" runat="server" OnClick="btnSaldoCuenta_Click" ImageUrl="~/Imagenes/ICO_SALDOCUENTA_16x16.png" onfocus="javascript:seleccionarElemento(this);" Style="border: 0px none; background: none;" />
                            </div>
                            <div style="border-right: 1px dashed #CCCCCC; padding: 0px 5px; height: auto; width: 9%;">
                                <asp:Button ID="btnPrecinto" runat="server" OnClick="btnPrecinto_Click" onfocus="javascript:seleccionarElemento(this);" Style="color: #2996e2; text-decoration: underline; border: 0px none; font-size: 10px; background: none; cursor: pointer;" /><br />
                                &nbsp;<asp:Label ID="lblCodigo" runat="server"></asp:Label>
                            </div>
                            <div style="border-right: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; width: 25%;">
                                <asp:Label ID="lblCliente" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="border-right: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; width: 15%;">
                                <asp:Label ID="lblCanal" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="border-right: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; width: 15%;">
                                <asp:Label ID="lblSector" runat="server" Text=""></asp:Label>
                            </div>
                            <div id="dvCuenta" runat="server" style="border-right: 1px dashed #CCCCCC; padding: 0px 5px; height: auto; width: 10%;">
                                <asp:Label ID="lblCuenta" runat="server" Text=""></asp:Label><br />
                                <div id="dvBultos" runat="server" class="bultos">
                                    <asp:Repeater ID="rptObjetos" runat="server" OnItemDataBound="rptObjetos_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Literal ID="litObjetos" runat="server"></asp:Literal>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <asp:Literal ID="litObjetos" runat="server"></asp:Literal>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <div style="padding-left: 7px; height: auto; width: 10%;">
                                <asp:Label ID="lblDivisas" runat="server" Text=""></asp:Label><br />
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
                            <div style="padding-top: 5px; padding-right: 5px; float: right;">
                                <asp:ImageButton ID="imgHistorico" runat="server" OnClick="imgHistorico_Click" ImageUrl="~/Imagenes/history.png" Style="border: 0px none; background: none;" />
                                <asp:ImageButton OnClick="btnQuitar_Click" ID="btnQuitar" runat="server" ImageUrl="~/Imagenes/Quitar.png" Style="border: 0px none; font-size: 10px; background: none;" onfocus="javascript:seleccionarElemento(this);" />
                            </div>
                            <div class="dvclear">
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div style="padding: 5px;">
                            <div style="position: relative; width: 100%;">
                                <div id="dvTotales" runat="server" style="float: left; font-weight: bold; font-size: 13px; margin-right: 5px;"></div>
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
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
