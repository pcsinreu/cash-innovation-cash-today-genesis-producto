<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="MantenimientoPlanta.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoPlanta_aspx"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Plantas</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 115px;
        }

        .style2 {
            width: 23px;
        }

        .style3 {
            width: 228px;
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
                                    <asp:Label ID="lblTituloPlanta" runat="server"></asp:Label>
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
                                <td class="style2">&nbsp;</td>
                                <td align="right" class="style1">
                                    <asp:Label ID="lblDelegacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:UpdatePanel ID="UpdatePanelDelegacion" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlDelegacion" runat="server" Width="225px"
                                                            AutoPostBack="True" />
                                                        <asp:CustomValidator ID="csvDelegacion" runat="server" ErrorMessage=""
                                                            ControlToValidate="ddlDelegacion" Text="*">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" class="tamanho_celula">&nbsp;</td>
                                <td class="style3">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style2">&nbsp;
                                </td>
                                <td align="right" class="style1">
                                    <asp:Label ID="lblCodigoPlanta" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="170px">
                                    <table cellpadding="0px" cellspacing="0px" style="width: 148%">
                                        <tr>
                                            <td width="180px">
                                                <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCodigoPlanta" runat="server" MaxLength="15" AutoPostBack="true"
                                                            CssClass="Text02" Width="150px"></asp:TextBox>&nbsp;<asp:CustomValidator ID="csvCodigoPlanta"
                                                                runat="server" ErrorMessage="" ControlToValidate="txtCodigoPlanta" Text="*">*</asp:CustomValidator>
                                                        <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage=""
                                                            ControlToValidate="txtCodigoPlanta" Text="*">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtCodigoPlanta" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                                    <ProgressTemplate>
                                                        <img src="Imagenes/loader1.gif" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" class="tamanho_celula">&nbsp;</td>
                                <td class="style3"></td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">&nbsp;</td>
                                <td align="right" class="style1">
                                    <asp:Label ID="lblDescricaoPlanta" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDescricaoPlanta" runat="server" Width="225px"
                                                            MaxLength="50" CssClass="Text02" AutoPostBack="True" />
                                                        <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                                            ControlToValidate="txtDescricaoPlanta" Text="*">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                                <td align="right">&nbsp;</td>
                                <td class="style3">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style2">&nbsp;</td>
                                <td align="right" class="style1">
                                    <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" Width="225px"
                                        MaxLength="25" CssClass="Text02" AutoPostBack="True" Enabled="False"
                                        ReadOnly="True" />

                                </td>
                                <td align="right">
                                    <asp:Label ID="lblDesCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:TextBox ID="txtDesCodigoAjeno" runat="server" Width="225px"
                                        MaxLength="50" CssClass="Text02" AutoPostBack="True" Enabled="False"
                                        ReadOnly="True" />
                                </td>
                                <td>
                                    <pro:Botao ID="btnAlta" runat="server" ExecutaValidacaoCliente="True"
                                        Habilitado="True" Tipo="Novo"
                                        EstiloIcone="EstiloIcone" Titulo="btnCodigoAjeno">
                                    </pro:Botao>
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">&nbsp;</td>
                                <td align="right" class="style1">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <asp:CheckBox ID="chkVigente" runat="server" />
                                </td>
                                <td align="right">&nbsp;</td>
                                <td class="style3">&nbsp;</td>
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
                        <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                            <ContentTemplate>
                                <table border="0" cellpadding="0" cellspacing="0" border="1">
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
