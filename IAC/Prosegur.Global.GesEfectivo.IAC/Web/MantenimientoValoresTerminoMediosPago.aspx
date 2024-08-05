<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoValoresTerminoMediosPago.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoValoresTerminoMediosPago" MasterPageFile="Master/MasterModal.Master" %>
<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Valores de Termino de Médios de Pago</title>
     
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
                <asp:HiddenField runat="server" ID="hiddenCodigo" />
        <asp:UpdatePanel runat="server" ID="updGeral">
            <ContentTemplate>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblTituloValorTerminoMedioPago" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_campos">
                    <tr>
                        <td>
                            <table class="tabela_campos" style="margin: 0px !Important">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoValorTerminoMedioPago" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <table style="margin: 0px !Important">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelCodigoValorPossivel" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtCodigoValorTerminoMedioPago" Width="130" runat="server" MaxLength="15" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox><asp:CustomValidator
                                                                ID="csvCodigoValorTerminoMedioPago" runat="server" ControlToValidate="txtCodigoValorTerminoMedioPago">*</asp:CustomValidator><asp:CustomValidator
                                                                    ID="csvCodigoValorTerminoMedioPagoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoValorTerminoMedioPago">*</asp:CustomValidator>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescricaoValorTerminoMedioPago" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelDescricaoSubCanal" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDescricaoValorTerminoMedioPago" runat="server" Width="398px"
                                                    MaxLength="36" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox><asp:CustomValidator ID="csvDescricaoValorTerminoMedioPagoObrigatorio"
                                                        runat="server" ControlToValidate="txtDescricaoValorTerminoMedioPago">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:UpdatePanel ID="UpdatePanellblVigente" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanelchkVigente" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chkVigente" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table style="margin:0px !Important">
                                                    <tr>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnSave" CssClass="ui-button" Width="100"/>
                                                            <div class="botaoOcultar">
                                                               <asp:Button runat="server" ID="btnExcluirdoGrid"/>
                                                            </div>
                                                        </td>
                                                        <td >
                                                            <asp:Button runat="server" ID="btnCancelarModiciacion" CssClass="ui-button" Width="100"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="False"
                                                    ColunasSelecao="codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                                    PageSize="10" AutoGenerateColumns="False" Ajax="True" Width="99%" ExibirCabecalhoQuandoVazio="True"
                                                    Height="100%">
                                                    <Pager ID="objPager_ProsegurGridView1">
                                                        <FirstPageButton Visible="True">
                                                        </FirstPageButton>
                                                        <LastPageButton Visible="True">
                                                        </LastPageButton>
                                                        <Summary Text="Página {0} de {1} ({2} itens)" />
                                                        <SummaryStyle>
                                                        </SummaryStyle>
                                                    </Pager>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                                    <RowStyle CssClass="GridLinhaPadraoImpar" />
                                                    <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick"  />
                                                                <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" ValidationGroup="none"  />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="codigo" HeaderText="Código" SortExpression="" />
                                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="" />
                                                        <asp:TemplateField HeaderText="Vigente" SortExpression="">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Image ID="imgVigente" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </pro:ProsegurGridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                                <ContentTemplate>
                                    <table style="margin: 20px 0px 0px 0px !important">
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="100"/>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
