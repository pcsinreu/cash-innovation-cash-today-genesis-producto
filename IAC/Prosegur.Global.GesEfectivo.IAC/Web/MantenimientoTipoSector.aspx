<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoTipoSector.aspx.vb" 
Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoTipoSector" MasterPageFile="~/Principal.Master"
EnableEventValidation="false"%>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento Tipos Sectores</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 546px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="titulo02">
                        <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                            <tr>
                                <td>
                                    <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                </td>
                                <td>
                                    <asp:Label ID="lblTituloTiposSetor" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="100%">
                &nbsp;
            </td>
                </tr>
                <tr>
                    <td width="100%">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <table width="800px"  >
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblCodigoTiposSetor" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td width="300px" valign="middle">
                                        <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoTipoSetor" runat="server" AutoPostBack="true" CssClass="Text02"
                                                    MaxLength="15" Width="160px"></asp:TextBox>
                                                &nbsp;<asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" 
                                                    ControlToValidate="txtCodigoTipoSetor" Text="*"></asp:CustomValidator>
                                                <asp:CustomValidator ID="csvCodigoExistente" runat="server" 
                                                    ControlToValidate="txtCodigoTipoSetor" Text="*"></asp:CustomValidator>
                                            </ContentTemplate> 
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtCodigoTipoSetor" 
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
                                    <td align="right" valign="middle" class="style1">
                                        &nbsp;</td>
                                    <td align="right" valign="middle" width="100px">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="right" width="70px" valign="middle">
                                        <asp:Label ID="lblDescricaoTiposSetor" runat="server" CssClass="Lbl2" 
                                            Width="134px"></asp:Label>
                                    </td>
                                    <td width="300px" valign="middle" colspan="4" style="width: 400px">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                           <ContentTemplate>
                                                <asp:TextBox ID="txtDescricaoTiposSetor" runat="server" AutoPostBack="true" CssClass="Text02"
                                                    MaxLength="50" Width="250px"></asp:TextBox>
                                                <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricaoTiposSetor"
                                                    ErrorMessage="019_msg_tipoSetordescripcionobligatorio" Text="*"></asp:CustomValidator>
                                                </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtDescricaoTiposSetor" 
                                                    EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" valign="middle" width="70px">
                                        <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2" Width="134px"></asp:Label>
                                    </td>
                                    <td valign="middle" width="300px">
                                        <asp:TextBox ID="txtCodigoAjeno" runat="server" AutoPostBack="true" 
                                            CssClass="Text02" MaxLength="25" Width="160px" Enabled="False" 
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td align="right" valign="middle" width="100px">
                                        <asp:Label ID="lblDescricaoCodeA" runat="server" CssClass="Lbl2" Width="134px"></asp:Label>
                                    </td>
                                    <td align="left" class="style1" valign="middle">
                                        <asp:TextBox ID="txtDescricaoCodigoAjeno" runat="server" AutoPostBack="true" 
                                            CssClass="Text02" MaxLength="50" Width="100%" Enabled="False" 
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="middle" width="400px">
                                        <pro:Botao ID="btnAlta" runat="server" ExecutaValidacaoCliente="True" 
                                            Habilitado="True" Tipo="Novo" Titulo="btnCodigoAjeno">
                                        </pro:Botao>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2" Text="lblVigente"></asp:Label>
                                    </td>
                                    <td width="300px" valign="middle">
                                        <asp:CheckBox ID="chkVigente" runat="server" CssClass="Lbl2" AutoPostBack="false" />
                                    </td>
                                    <td align="right" width="100px" valign="middle">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" class="style1">
                                        &nbsp;</td>
                                    <td align="left" valign="middle" width="400px">
                                        &nbsp;</td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="titulo02" colspan="2" width="100%">
                            <table cellpadding="0" cellspacing="4" cellpadding="0" border="0" width="100%">
                                <tr>
                                    <td width="1%">
                                        <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                    </td>
                                    <td width="100%">
                                        <asp:Label ID="lblTituloCaracteristicas" runat="server"></asp:Label>
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                               <ContentTemplate>
                                    <table class="tabela_campos"  >
                                <tr>
                                    <td class="espaco_inicial">
                                        &nbsp;
                                    </td>
                                    <td width="55px">
                                        &nbsp;
                                        <asp:Label ID="lblCaracteristicas" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td align="right" width="420px">
                                        <asp:ListBox ID="lstCaracteristicasDisponiveis" runat="server" Height="120px" SelectionMode="Multiple"
                                            Width="400px"></asp:ListBox>
                                    </td>
                                    <td align="center" width="68px">
                                        <asp:ImageButton ID="imbAdicionarTodasCaracteristicas" runat="server" ImageUrl="~/Imagenes/pag07.png"
                                            Style="height: 25px" /><br />
                                        <asp:ImageButton ID="imbAdicionarCaracteristicasSelecionadas" runat="server" ImageUrl="~/Imagenes/pag05.png" /><br />
                                        <asp:ImageButton ID="imbRemoverCaracteristicasSelecionadas" runat="server" ImageUrl="~/Imagenes/pag06.png" /><br />
                                        <asp:ImageButton ID="imbRemoverTodasCaracteristicas" runat="server" ImageUrl="~/Imagenes/pag08.png" />
                                    </td>
                                    <td align="left" width="420px">
                                        <asp:ListBox ID="lstCaracteristicasSelecionadas" runat="server" Height="120px" SelectionMode="Multiple"
                                            Width="400px"></asp:ListBox>
                                            
                                    </td>
                                    <td>
                                        <asp:CustomValidator ID="csvlstCaracteristicas" runat="server" ControlToValidate="lstCaracteristicasSelecionadas"
                                                        ErrorMessage="019_msg_CaracteristicaObrigatoria" Text="*"></asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lstCaracteristicasSelecionadas" 
                                        EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table class="tabela_campo"  >
                                 <tr>
                                            <td class="style11" style="text-align: center">
                                                <pro:Botao ID="btnGrabar" runat="server" ExecutaValidacaoCliente="true" ExibirLabelBtn="True"
                                                    Habilitado="True" Tipo="Salvar" Titulo="btnGrabar">
                                                </pro:Botao>
                                            </td>
                                            <td class="style7" style="text-align: center">
                                                <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                    Tipo="Sair" Titulo="btnCancelar">
                                                </pro:Botao>
                                            </td>
                                            <td class="style10" style="text-align: center">
                                                <pro:Botao ID="btnVolver" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                    Tipo="Voltar" Titulo="btnVolver">
                                                </pro:Botao>
                                            </td>
                                        </tr>
                            <table>
                        </td>
                    </tr>
                </table>
            </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
