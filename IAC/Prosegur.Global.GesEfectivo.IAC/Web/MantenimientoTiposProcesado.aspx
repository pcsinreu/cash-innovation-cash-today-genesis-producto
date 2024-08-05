<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoTiposProcesado.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoTiposProcesado" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento Tipos Procesado</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidTxtObservacaoValBanco" runat="server" />
    <asp:HiddenField ID="hidTxtCodigoBanco" runat="server" />
    <asp:HiddenField ID="hidTxtDescricaoBanco" runat="server" />
    <asp:HiddenField ID="hidChkVigente" runat="server" />
    <asp:HiddenField ID="hidChkVigenteTemp" runat="server" />
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloTiposProcesado" runat="server"></asp:Label>
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
                                    <td class="espaco_inicial">
                                        &nbsp;
                                    </td>
                                    <td align="right" width="70px" valign="middle">
                                        <asp:Label ID="lblCodigoTiposProcesado" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td width="300px" valign="middle">
                                        <table cellpadding="0px" cellspacing="0px" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtCodigoTiposProcesado" runat="server" AutoPostBack="true" CssClass="Text02"
                                                                MaxLength="15" Width="150px"></asp:TextBox>
                                                            <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ControlToValidate="txtCodigoTiposProcesado"
                                                                ErrorMessage="001_msg_canalcodigoobrigatorio" Text="*"></asp:CustomValidator>
                                                            <asp:CustomValidator ID="csvCodigoExistente" runat="server" ControlToValidate="txtCodigoTiposProcesado"
                                                                ErrorMessage="001_msg_canalcodigoobrigatorio" Text="*"></asp:CustomValidator>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
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
                                    <td align="right" width="100px" valign="middle">
                                        <asp:Label ID="lblDescricaoTiposProcesado" runat="server" CssClass="Lbl2" Width="134px"></asp:Label>
                                    </td>
                                    <td align="left" width="400px" valign="middle">
                                        <table cellpadding="0px" cellspacing="0px" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtDescricaoTiposProcesado" runat="server" AutoPostBack="true" CssClass="Text02"
                                                                MaxLength="50" Width="255px"></asp:TextBox>
                                                            <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ControlToValidate="txtDescricaoTiposProcesado"
                                                                ErrorMessage="001_msg_canaldescripcionobrigatorio" Text="*"></asp:CustomValidator>
                                                            <asp:CustomValidator ID="csvDescricaoExistente" runat="server" ControlToValidate="txtCodigoTiposProcesado"
                                                                ErrorMessage="001_msg_canalcodigoobrigatorio" Text="*"></asp:CustomValidator>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <asp:UpdateProgress ID="UpdateProgressDescricao" runat="server" AssociatedUpdatePanelID="UpdatePanelDescricao">
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
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblObservaciones" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" AutoPostBack="false"
                                            CssClass="Text02" Height="96px" Width="580px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2" Text="lblVigente"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 201px;">
                                        <asp:CheckBox ID="chkVigente" runat="server" CssClass="Lbl2" AutoPostBack="false" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="width: 276px;">
                                        &nbsp;
                                    </td>
                                    <td>
                                    </td>
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
                            <table class="tabela_campos"  >
                                <tr>
                                    <td class="espaco_inicial">
                                        &nbsp;
                                    </td>
                                    <td width="55px">
                                        &nbsp;
                                    </td>
                                    <td align="right" width="420px">
                                        <asp:ListBox ID="lstCaracteristicasDisponiveis" runat="server" Height="120px" SelectionMode="Multiple"
                                            Width="420px"></asp:ListBox>
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
                                            Width="420px"></asp:ListBox>
                                    </td>
                                    <td>
                                        &nbsp;
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
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                                <ContentTemplate>
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
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
