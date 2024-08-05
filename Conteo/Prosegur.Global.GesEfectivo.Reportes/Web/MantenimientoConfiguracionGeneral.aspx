<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="MantenimientoConfiguracionGeneral.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.MantenimientoConfiguracionGeneral" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="titulo02">
                        <table cellpadding="0" cellspacing="4">
                            <tr>
                                <td>
                                    <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                </td>
                                <td>
                                    <asp:Label ID="lblTituloPagina" runat="server">Mantenimiento Configuración General Reportes</asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr class="TrImpar">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left" width="150">
                                    <asp:Label runat="server" ID="lblReporte" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlReporte" runat="server" CssClass="Text01">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvReporte" runat="server" InitialValue="0" ErrorMessage=""
                                        ControlToValidate="ddlReporte">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="TrPar">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left" width="150">
                                    <asp:Label runat="server" ID="lblIDReporte" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtIDReporte" CssClass="Text01" MaxLength="15"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvIDReporte" runat="server" ControlToValidate="txtIDReporte">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upFormatoSalida" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr class="TrPar">
                                        <td class="espaco_inicial" align="right">
                                            <img alt="" src="imagenes/MarcadorCampo.gif" />
                                        </td>
                                        <td align="left" width="150">
                                            <asp:Label runat="server" ID="lblFormatoSaida" CssClass="Lbl2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFormatoSaida" runat="server" CssClass="Text01" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvFormatoSaida" runat="server" InitialValue="0"
                                                ErrorMessage="" ControlToValidate="ddlFormatoSaida">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="TrImpar">
                                        <td class="espaco_inicial" align="right">
                                            <img alt="" src="imagenes/MarcadorCampo.gif" />
                                        </td>
                                        <td align="left" width="150">
                                            <asp:Label runat="server" ID="lblExtensaoArquivo" CssClass="Lbl2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtExtensaoArquivo" CssClass="Text01" MaxLength="100"
                                                Width="400px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvExtensaoArquivo" runat="server" ErrorMessage=""
                                                ControlToValidate="txtExtensaoArquivo">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="TrPar">
                                        <td class="espaco_inicial" align="right">
                                            <img alt="" src="imagenes/MarcadorCampo.gif" />
                                        </td>
                                        <td align="left" width="150">
                                            <asp:Label runat="server" ID="lblSeparador" CssClass="Lbl2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtSeparador" CssClass="Text01" MaxLength="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSeparador" runat="server" ErrorMessage="" ControlToValidate="txtSeparador">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlFormatoSaida" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="TrImpar">
                    <td colspan="3">
                        <table align="center" width="30%">
                            <tr>
                                <td class="style1">
                                    <pro:Botao ID="btnSalvar" runat="server" Tipo="Salvar" ExecutaValidacaoCliente="false">
                                    </pro:Botao>
                                </td>
                                <td class="style1">
                                    <pro:Botao ID="btnVoltar" runat="server" Tipo="Voltar">
                                    </pro:Botao>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnVoltar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgressPanelBtnsGrid" runat="server">
        <ProgressTemplate>
            <div id="divLoadingReporte" class="AlertLoading">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
