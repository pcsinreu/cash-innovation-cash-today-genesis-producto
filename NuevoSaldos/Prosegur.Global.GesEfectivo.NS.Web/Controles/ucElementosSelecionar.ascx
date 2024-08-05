<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucElementosSelecionar.ascx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.ucElementosSelecionar" %>
<asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
    <Scripts>
        <asp:ScriptReference Path="~/js/ucListaElementos.js" />
    </Scripts>
</asp:ScriptManagerProxy>
<asp:UpdatePanel ID="upElementosSelecionar" runat="server">
    <ContentTemplate>
        <div id="ucListaElementos" runat="server" style="width: 98%; color: #767676; padding: 0px 0px 10px 0px;">
            <div style="width: 100%; margin-left: 5px; margin-top: 5px;">

                <asp:Label ID="lblmensajeVacio" runat="server" Text=""></asp:Label>

                <div id="dvSeleccionar" runat="server" class="dvCodigoBarra">
                    <asp:Label ID="lblSeleccionar" runat="server"></asp:Label>
                    <asp:TextBox ID="txtSeleccionar" runat="server" MaxLength="15" onchange="javascript:seleccionarAgregarElemento(this);" onkeypress="javascript:if (event.keyCode == 13) seleccionarAgregarElemento(this);" />
                    <img src="../Imagenes/BarCodeAgregar.png" style="vertical-align: middle" />
                </div>

                <div id="dvSeleccionarTodos" runat="server">
                    <asp:Button ID="btnAgregarTodos" runat="server" Text="Seleccionar todos" CssClass="BotonAgregarTodos" />
                </div>

                <asp:Repeater ID="rptElementos" runat="server">
                    <HeaderTemplate></HeaderTemplate>
                    <ItemTemplate>
                        <div id="dvListaElementos" runat="server" class="dvListaElemento">
                            <div style="padding: 5px 5px 0px 5px; width: 10px; height: auto;">
                                <asp:ImageButton ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" ImageUrl="~/Imagenes/Agregar.png" onfocus="javascript:seleccionarElemento(this);" Style="border: 0px none; background: none;" />
                            </div>
                            <div style="border-right: 1px dashed #CCCCCC; padding: 0px 5px; height: auto; width:9%;">
                                <asp:Button runat="server" ID="btnPrecinto" OnClick="btnPrecinto_Click" onfocus="javascript:seleccionarElemento(this);" Style="color: #2996e2; text-decoration: underline; border: 0px none; font-size: 10px; background: none; cursor: pointer;" /><br />
                                <asp:Literal ID="litPrecintoPadre" runat="server"></asp:Literal>
                                <asp:Button ID="btnPrecintoPadre" runat="server" OnClick="btnPrecintoPadre_Click" onfocus="javascript:seleccionarElemento(this);" Style="color: #2996e2; text-decoration: underline; border: 0px none; font-size: 10px; background: none; cursor: pointer;" /><br />
                            </div>
                            <div style="border-right: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; width:25%;">
                                <asp:Label ID="lblCliente" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="border-right: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; width:15%;">
                                <asp:Label ID="lblCanal" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="border-right: 1px dashed #CCCCCC; padding: 0px 7px; height: auto;  width:15%;">
                                <asp:Label ID="lblSector" runat="server" Text=""></asp:Label>
                            </div>
                            <div id="dvObjetos" runat="server" style="border-right: 1px dashed #CCCCCC; padding: 0px 7px; height: auto; width:10%;">
                                <asp:Label ID="lblObjetos" runat="server" Text=""></asp:Label><br />
                                <div class="bultos">
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
                            <div style="padding-left: 7px; width:10%;">
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
                            <div style="padding-top:5px; padding-right:10px; height: auto; float:right">
                                <asp:ImageButton ID="imgHistorico" runat="server" OnClick="imgHistorico_Click" ImageUrl="~/Imagenes/history.png" Style="border: 0px none; background: none;" />
                            </div>
                            <div class="dvclear"></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
