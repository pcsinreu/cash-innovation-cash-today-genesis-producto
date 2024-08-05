<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="MantenimientoTerminos.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoTerminos" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Terminos</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloTermino" runat="server"></asp:Label>
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
                            <asp:Label ID="lblCodigoTermino" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td width="170px">
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td width="140px">
                                        <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoTermino" runat="server" MaxLength="50" AutoPostBack="true"
                                                    CssClass="Text02" Width="120px"></asp:TextBox><asp:CustomValidator ID="csvCodigoObrigatorio"
                                                        runat="server" ErrorMessage="" ControlToValidate="txtCodigoTermino" Text="*"></asp:CustomValidator>
                                                <asp:CustomValidator ID="csvCodigoTerminoExistente" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtCodigoTermino">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" class="tamanho_celula">
                            <asp:Label ID="lblDescricaoTermino" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td width="300px">
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td width="250px">
                                        <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtDescricaoTermino" runat="server" Width="225px" MaxLength="50"
                                                    CssClass="Text02" AutoPostBack="True"></asp:TextBox><asp:CustomValidator ID="csvDescricaoObrigatorio"
                                                        runat="server" ErrorMessage="" ControlToValidate="txtDescricaoTermino" Text="*"></asp:CustomValidator>
                                                <asp:CustomValidator ID="csvDescripcionExistente" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtDescricaoTermino">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
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
                            <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" CssClass="Text02"
                                Height="96px" Width="650px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMostrarCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkMostrarCodigo" runat="server" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkVigente" runat="server" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblAceptarDigitacion" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkAceptarDigitacion" runat="server" />
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTipoFormato" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelTipoFormato" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTipoFormato" runat="server" Width="208px" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="csvTipoFormatoObrigatorio" runat="server" ControlToValidate="ddlTipoFormato"
                                        ErrorMessage="">*</asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblLongitud" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelLongitud" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtLongitud" runat="server" Width="100px" MaxLength="3" AutoPostBack="True"
                                        CssClass="Text02"></asp:TextBox>
                                    <asp:CustomValidator ID="csvLongitudeObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="txtLongitud">*</asp:CustomValidator>
                                    <asp:CustomValidator ID="csvLongitude" runat="server" ErrorMessage="" ControlToValidate="txtLongitud">*</asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoFormato" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right" valign="top">
                            <asp:Label ID="lblValidacao" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <table border="0" cellpadding="0" cellspacing="10">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbtSinValidacion" runat="server" GroupName="Validacion" AutoPostBack="True" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbtMascara" runat="server" GroupName="Validacion" AutoPostBack="True" />
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelMascara" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlMascara" runat="server" Width="200px" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="csvMascaraObrigatorio" runat="server" ControlToValidate="ddlMascara">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbtListaValores" runat="server" GroupName="Validacion" AutoPostBack="True" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbtFormula" OnCheckedChanged="rbtFormula_CheckedChanged" runat="server"
                                            GroupName="Validacion" AutoPostBack="True" />
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlAlgoritmo" runat="server" Width="300px" OnSelectedIndexChanged="ddlAlgoritmo_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="csvAlgoritmoObrigatorio" runat="server" ControlToValidate="ddlAlgoritmo">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
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
