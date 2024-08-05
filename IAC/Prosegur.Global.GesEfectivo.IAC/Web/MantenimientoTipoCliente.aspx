<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master" 
CodeBehind="MantenimientoTipoCliente.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoTipoCliente" 
EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Tipo Clientes</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style2
        {
            width: 153px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server" UpdateMode="Conditional">
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
                        <asp:Label ID="lblTituloTipoCliente" runat="server"></asp:Label>
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
                    <td class="style2" align="right">
                        <asp:Label ID="lblCodTipoCliente" runat="server" CssClass="Lbl2"></asp:Label>
                    </td>
                    <td width="170px">
                        <table cellpadding="0px" cellspacing="0px" style="width: 120%">
                            <tr>
                                <td width="250px">
                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoTipoCliente" runat="server" MaxLength="15" AutoPostBack="true"
                                                CssClass="Text02" Width="148px"></asp:TextBox>&nbsp;<asp:CustomValidator ID="csvCodigoTipoCliente"
                                                    runat="server" ErrorMessage="" 
                                                ControlToValidate="txtCodigoTipoCliente" Text="*"></asp:CustomValidator>
                                                    <asp:CustomValidator ID="csvCodigoExistente"
                                                    runat="server" ErrorMessage="" ControlToValidate="txtCodigoTipoCliente" Text="*">*</asp:CustomValidator>
                                        </ContentTemplate> 
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtCodigoTipoCliente" 
                                                EventName="TextChanged" />
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
                    <td align="right" class="tamanho_celula">
                        &nbsp;</td>
                    <td >
                            
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <asp:Label ID="lblDescripcion" runat="server" CssClass="Lbl2"></asp:Label>
                    </td>
                    <td width="300px">
                        <asp:UpdatePanel ID="UpdatePanelDescripcion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                        <asp:TextBox ID="txtDescricaoTipoCliente" runat="server" Width="225px" 
                        MaxLength="50" CssClass="Text02" AutoPostBack="True"/>
                        <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage="" 
                        ControlToValidate="txtDescricaoTipoCliente" Text="*"></asp:CustomValidator>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                    </td>
                    <td width="300px">
                        <asp:CheckBox ID="chkVigente" runat="server" />
                    </td>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
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
        <td align="center">
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
        </td>
    </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
