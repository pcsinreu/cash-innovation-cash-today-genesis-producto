<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoComponente.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoComponente" EnableEventValidation="false" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Terminos de Médio de Pago</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTituloComponente" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="updatePanelFuncion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tabela_campos">
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigo" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" CssClass="Text02" AutoPostBack="False"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigo"
                                        Text="*"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvCodigoUnico" runat="server" ErrorMessage="" ControlToValidate="txtCodigo"
                                        Text="*"></asp:CustomValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblFuncion" runat="server" CssClass="label2"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFuncion" runat="server" Width="190px" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="csvFuncionObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlFuncion"
                                Text="*"></asp:CustomValidator>
                        </td>
                    </tr>
                    <asp:UpdatePanel ID="updatePanelTipo" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblTipo" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTipo" runat="server" Width="190px" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="csvTipoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipo"
                                        Text="*"></asp:CustomValidator>
                                </td>
                            </tr>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="updatePanelDivisa" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlDivisa" runat="server">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDivisa" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDivisa" AutoPostBack="true" runat="server" Width="190px">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvDivisaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlDivisa"
                                            Text="*"></asp:CustomValidator>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDivisa" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:Panel ID="pnlDenominacion" runat="server">
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblDenominacion" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td >
                                <asp:UpdatePanel ID="updatePanelDenominacion" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDenominacion" runat="server" Width="190px" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvDenominacionObrigatorio" runat="server" ErrorMessage=""
                                            ControlToValidate="ddlDenominacion" Text="*"></asp:CustomValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDenominacion" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlMediosPago" runat="server">
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblMediosPago" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelTreeView" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table style="margin: 0px !Important;">
                                            <tr>
                                                <td align="left">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <table style="margin: 0px !Important;">
                                                                    <tr>
                                                                        <td id="TdTreeViewDivisa" runat="server" style="width: 350px; border-width: 1px; vertical-align: top;">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TreeView ID="TrvDivisas" runat="server" CssClass="tree" ShowLines="True">
                                                                                            <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                                                        </asp:TreeView>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="center" style="width: 68px; border-width: 0;">
                                                                            <asp:ImageButton ID="imgBtnIncluirTreeview" runat="server" ImageUrl="~/Imagenes/pag05.png" />
                                                                            <br />
                                                                            <asp:ImageButton ID="imgBtnExcluirTreeview" runat="server" ImageUrl="~/Imagenes/pag06.png" />
                                                                        </td>
                                                                        <td style="width: 350px; border-width: 1px; vertical-align: top;">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TreeView ID="TrvMediosPagosSelecionados" runat="server" CssClass="tree" ShowLines="True">
                                                                                            <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                                                        </asp:TreeView>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <asp:CustomValidator ID="csvTrvMediosPagosSelecionados" runat="server" ControlToValidate=""
                                                        ErrorMessage="" Text="*"></asp:CustomValidator>
                                                    <asp:CustomValidator ID="csvTrvMedioPagoUnico" runat="server" ControlToValidate=""
                                                        ErrorMessage="" Text="*"></asp:CustomValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlFuncion" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td >
                            <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="130"/>
                        </td>
                        <td >
                             <asp:Button runat="server" ID="btnLimpiar" CssClass="ui-button" Width="130"/>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
