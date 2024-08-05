<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoValoresPosibles.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoValoresPosibles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Erro.ascx" TagName="Erro" TagPrefix="uc1" %>
<%@ Register Src="Controles/Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IAC - Mantenimiento de Subcanales</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <table style="width: 768px; border-style: none; text-align: left; border-collapse: collapse;
            background-color: #FFFFFF;">
            <tr>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/JS/FuncaoAjax.js" />
                        </Scripts>
                    </asp:ScriptManager>
                    <uc3:Cabecalho ID="Cabecalho1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 768px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <uc1:Erro ID="ControleErro" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="titulo02" style="color: Black;">
                    <table cellpadding="0" cellspacing="4" border="0">
                        <tr>
                            <td>
                                <img src="imagenes/ico01.jpg" alt="icon" width="16" height="18" />
                            </td>
                            <td>
                                <asp:Label ID="lblTituloValoresPosibles" runat="server"></asp:Label>
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
                <td>
                    <table style="width: 100%;" cellpadding="3">
                        <tr>
                            <td class="legendas">
                                <asp:Label ID="lblCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" AutoPostBack="True"></asp:TextBox><asp:CustomValidator
                                                        ID="csvCodigo" runat="server" ControlToValidate="txtCodigo">*</asp:CustomValidator>
                                                    <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigo">*</asp:CustomValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                                <ProgressTemplate>
                                                    <img src="Imagenes/loader1.gif" alt="Loader" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="legendas">
                                <asp:Label ID="lblDescricao" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <table style="width: 100%; border-collapse: collapse;">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDescricao" runat="server" Width="260px" MaxLength="36" AutoPostBack="True"></asp:TextBox>
                                                    <asp:CustomValidator ID="csvDescricao" runat="server" ControlToValidate="txtDescricao">*</asp:CustomValidator>
                                                    <asp:CustomValidator ID="csvDescripcionExistente" runat="server" ErrorMessage=""
                                                        ControlToValidate="txtDescricao">*</asp:CustomValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="legendas">
                                <asp:Label ID="lblValorDefecto" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelValorDefecto" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkValorDefecto" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="legendas">
                                <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkVigente" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                        <ContentTemplate>
                            <table style="width: 50%; border-collapse: collapse;">
                                <tr>
                                    <td style="width: 50%; text-align: center">
                                        <pro:Botao ID="btnGrabar" runat="server" Habilitado="True" Tipo="Confirmar" Titulo="btnAceptar"
                                            ExibirLabelBtn="True" ExecutaValidacaoCliente="True">
                                        </pro:Botao>
                                    </td>
                                    <td style="width: 50%; text-align: center">
                                        <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                            Tipo="Sair" Titulo="btnCancelar">
                                        </pro:Botao>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div id="AlertDivAll" class="AlertLoading" style="visibility: hidden;">
        </div>
    </center>
    </form>
</body>
</html>
