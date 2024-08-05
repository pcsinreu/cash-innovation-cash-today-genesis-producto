<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoGrupo.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoGrupo" EnableEventValidation="false" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Terminos de Médio de Pago</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <div class="ui-panel-titlebar">
            <asp:Label ID="lblTituloGrupo" CssClass="ui-panel-title" runat="server"></asp:Label>
        </div>
        <table class="tabela_campos">
            <tr>
                <td class="tamanho_celula">
                    <asp:Label ID="lblCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                            <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigo"
                                Text="*"></asp:CustomValidator>
                            <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigo">*</asp:CustomValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="txtCodigo" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="tamanho_celula">
                    <asp:Label ID="lblDescripcion" runat="server" CssClass="Lbl2"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:TextBox ID="txtDescricao" runat="server" Width="272px" MaxLength="50" AutoPostBack="false"
                                CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                            <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                ControlToValidate="txtDescricao" Text="*"></asp:CustomValidator>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td align="center" colspan="3">
                    <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                        <ContentTemplate>
                            <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="130"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
