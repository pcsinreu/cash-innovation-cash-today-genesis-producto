<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="MantenimientoSector.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoSector"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mantenimiento de Sector</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
        }

        .style3 {
        }

        .style10 {
            width: 171px;
        }

        .style11 {
            width: 235px;
        }

        .style12 {
            width: 141px;
        }

        .style13 {
            width: 243px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="titulo02">
                        <table cellpadding="0" cellspacing="4" border="0">
                            <tr>
                                <td>
                                    <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                </td>
                                <td>
                                    <asp:Label ID="lblTituloSectores" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <table class="tabela_campos"  >
                            <tr>
                                <td class="style10" align="right">
                                    <asp:Label ID="lblDelegacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style3" colspan="2">
                                    <asp:DropDownList ID="ddlDelegacion" runat="server" AutoPostBack="True"
                                        Width="225px" />
                                    <asp:CustomValidator ID="csvDelegacion" runat="server"
                                        ControlToValidate="ddlDelegacion" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                </td>
                                <td class="style13">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style10">
                                    <asp:Label ID="lblPlanta" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style3" colspan="2">
                                    <asp:DropDownList ID="ddlPlanta" runat="server" AutoPostBack="True"
                                        Enabled="False" Width="225px" />
                                    <asp:CustomValidator ID="csvPlanta" runat="server"
                                        ControlToValidate="ddlPlanta" Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                </td>
                                <td class="style13">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style10" align="right">
                                    <asp:Label ID="lblTipoSector" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style3" colspan="2">
                                    <asp:DropDownList ID="ddlTipoSector" runat="server" AutoPostBack="True"
                                        Width="225px" Enabled="False" />
                                    <asp:CustomValidator ID="csvTipoSector" Display="Dynamic"
                                        runat="server" ErrorMessage="" ControlToValidate="ddlTipoSector"
                                        Text="*" />
                                </td>
                                <td class="style13"></td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style10">
                                    <asp:Label ID="lblCentroProcesso" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style3" colspan="2">
                                    <asp:CheckBox ID="chkCentroProceso" runat="server" Enabled="False" />
                                </td>
                                <td class="style13">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style10">
                                    <asp:Label ID="lblSectorPadre" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style11" colspan="1">
                                    <asp:TextBox ID="txtSectorPadre" runat="server" AutoPostBack="true"
                                        Width="225px" CssClass="Text02" MaxLength="5" Enabled="False"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="style12">
                                    <asp:Panel ID="pnlBuscaSetor" runat="server" Enabled="False">
                                        <pro:Botao ID="btnBuscarSector" runat="server" EstiloIcone="EstiloIcone"
                                            Habilitado="True" Tipo="Consultar" Titulo="054_msg_BuscaSetorPai" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style10">
                                    <asp:Label ID="lblCodigoSector" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style1" colspan="2">
                                    <table cellpadding="0px" cellspacing="0px" style="width: 85%">
                                        <tr>
                                            <td width="">
                                                <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCodigoSector" runat="server" CssClass="Text02"
                                                            Width="225px" AutoPostBack="true" MaxLength="25" Enabled="False" />
                                                        &nbsp;<asp:CustomValidator ID="csvCodigoSector" runat="server" ErrorMessage=""
                                                            ControlToValidate="txtCodigoSector" Text="*" Display="Dynamic" />
                                                        <asp:CustomValidator ID="csvCodigoExistente" runat="server"
                                                            ControlToValidate="txtCodigoSector" Display="Dynamic" ErrorMessage=""
                                                            Text="*" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtCodigoSector" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td width="80px">
                                                <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                                    <ProgressTemplate>
                                                        <img src="Imagenes/loader1.gif" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style10">
                                    <asp:Label ID="lblDescriptionSector" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style3" colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDescricaoSetor" runat="server" Width="225px"
                                                AutoPostBack="true" CssClass="Text02" MaxLength="50" Enabled="False" />
                                            <asp:CustomValidator ID="csvDescricaoSetor" runat="server" ErrorMessage=""
                                                ControlToValidate="txtDescricaoSetor" Text="*" Display="Dynamic" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="style13">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style10">
                                    <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style11">
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" MaxLength="25"
                                        AutoPostBack="true" CssClass="Text02" Width="225px" Enabled="False"
                                        ReadOnly="True" />
                                </td>
                                <td align="right" class="style12">
                                    <asp:Label ID="lblDescricaoCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style13">
                                    <asp:TextBox ID="txtDescricaoAjeno" runat="server" AutoPostBack="true"
                                        CssClass="Text02" Enabled="False" MaxLength="50" ReadOnly="True"
                                        Width="225px" />
                                </td>
                                <td>
                                    <pro:Botao ID="btnAjeno" runat="server" ExecutaValidacaoCliente="true"
                                        Habilitado="True" Tipo="Novo" Titulo="btnCodigoAjeno">
                                    </pro:Botao>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style10">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style3" colspan="2">
                                    <asp:CheckBox ID="chkVigente" runat="server" Enabled="False" />
                                </td>
                                <td class="style13">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnImporteMaximo" runat="server" Habilitado="True" Tipo="Novo" Titulo="btnImporteMaximo"
                                        ExibirLabelBtn="True" ExecutaValidacaoCliente="true">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnGrabar" runat="server" Habilitado="True" Tipo="Salvar" Titulo="btnGrabar"
                                        ExibirLabelBtn="True" ExecutaValidacaoCliente="true">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Sair" Titulo="btnCancelar">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnVolver" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Voltar" Titulo="btnVolver">
                                    </pro:Botao>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPlanta" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
