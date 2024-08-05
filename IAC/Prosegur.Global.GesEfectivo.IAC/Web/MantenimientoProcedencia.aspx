<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoProcedencia.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoProcedencia" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Procedencias</title>
    <link href="Css/css.css" rel="stylesheet"type="text/css" />
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 257px;
        }
        .style2
        {
            width: 297px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidBtnGrabar" runat="server" />
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloProcedencia" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <table class="tabela_campos"  >
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTipoSubCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelTipoSubCliente" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <asp:DropDownList ID="ddlTipoSubCliente" runat="server" Width="208px" AutoPostBack="True"></asp:DropDownList>
                                     <asp:CustomValidator ID="csvTipoSubClienteObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoSubCliente" Text="*">
                                     </asp:CustomValidator>                                    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>                           
                        </td>
                        <td class="tamanho_celula" align="right">
                            <asp:Label ID="lblTipoPuntoServicio" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelTipoPuntoServicio" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <asp:DropDownList ID="ddlTipoPuntoServicio" runat="server" Width="208px" AutoPostBack="True"></asp:DropDownList>
                                     <asp:CustomValidator ID="csvTipoPuntoServicioObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoPuntoServicio" Text="*">
                                     </asp:CustomValidator>                                    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td width="20px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTipoProcedencia" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanelTipoProcedencia" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <asp:DropDownList ID="ddlTipoProcedencia" runat="server" Width="208px" AutoPostBack="True"></asp:DropDownList>
                                     <asp:CustomValidator ID="csvTipoProcedenciaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlTipoProcedencia" Text="*">
                                     </asp:CustomValidator>                                    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>                            
                        </td>
                        <td align="right">
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="chkVigente" runat="server" Checked="true" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 14px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
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
</asp:Content>
