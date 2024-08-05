<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoOpcionesParametro.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoOpcionesParametro"
    EnableEventValidation="false" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Opciones del Parámetro</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTituloOpcionParametro" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
        <table class="tabela_campos">
            <tr>
                <td class="tamanho_celula">
                    <asp:Label ID="lblCodigoOpcion" runat="server" CssClass="label2"></asp:Label>
                </td>
                <td width="150px">
                    <table class="tabela_campos" style="margin:0px !Important">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelCodigoOpcion" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtCodigoOpcion" runat="server" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
                                            MaxLength="20" Width="130px"></asp:TextBox>
                                        <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ControlToValidate="txtCodigoOpcion"
                                            ErrorMessage="" Text="*"></asp:CustomValidator>
                                        <asp:CustomValidator ID="csvCodigoOpcionExistente" runat="server" ControlToValidate="txtCodigoOpcion"
                                            ErrorMessage="">*</asp:CustomValidator>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="tamanho_celula">
                    <asp:Label ID="lblDescricaoOpcion" runat="server" CssClass="label2"></asp:Label>
                </td>
                <td width="275px">
                    <asp:UpdatePanel ID="UpdatePanelDescricaoOpcion" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtDescricaoOpcion" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="50"
                                AutoPostBack="true" Width="253px"></asp:TextBox>
                            <asp:CustomValidator ID="csvDescricaoOpcion" runat="server" ControlToValidate="txtDescricaoOpcion"
                                ErrorMessage="">*</asp:CustomValidator>
                            <asp:CustomValidator ID="csvDescricaoOpcionExistente" runat="server" ControlToValidate="txtDescricaoOpcion"
                                ErrorMessage="">*</asp:CustomValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="tamanho_celula">
                    <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkVigente" runat="server" />
                </td>
                <td class="tamanho_celula">
                    <asp:Label ID="lblDelegacion" runat="server" CssClass="label2"></asp:Label>
                </td>
                <td width="275px">
                    <asp:UpdatePanel ID="UpdatePanelDelegacion" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlDelegacion" runat="server" Width="208px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                        <ContentTemplate>
                            <table style="margin: 0px !Important">
                                <tr>
                                    <td >
                                        <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="100"/>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

