<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoCodigosAjenos.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoCodigosAjenos" EnableEventValidation="false" MasterPageFile="Master/MasterModal.Master" %>
<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Búsqueda de SubClientes</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 160px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulo" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_campos">
                    <tr id="trEntidade" runat="server">
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigoEntidade" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>&nbsp;<asp:TextBox ID="txtCodigoEntidade" runat="server" Enabled="false" Style="width: 150px;" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDesEntidade" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">&nbsp;<asp:TextBox ID="txtDesEntidade" runat="server" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trCamposBusqueda" runat="server">
                        <td class="tamanho_celula">
                            <asp:Label ID="lblIdentificador" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelIdentificador" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table  style="margin:0px !Important">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtIdentificador" runat="server" MaxLength="15"
                                                    Style="width: 150px;" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CustomValidator ID="csvIdentificador" runat="server" ErrorMessage="" ControlToValidate="txtIdentificador" Text="*" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAnadir" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="tamanho_celula">
                            <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="margin:0px !Important">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCodigoAjeno" runat="server" MaxLength="25" Style="width: 150px;" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:CustomValidator ID="csvCodigoAjeno" runat="server" ErrorMessage="" ControlToValidate="txtCodigoAjeno" Text="*" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAnadir" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr id="trDescripcionAjena" runat="server">
                        <td class="tamanho_celula">
                            <asp:Label ID="lblDescripcionAjena" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td >
                            <asp:TextBox ID="txtDescripcionAjena" runat="server" MaxLength="50" Width="150px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                        </td>
                  
                    </tr>
                    <tr id="trBotoesBusqueda" runat="server">
                        <td colspan="4">
                            <table style="margin: 0px !important;">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnAnadir" CssClass="ui-button" Width="100"/>
                                    </td>
                                    <td>
                                         <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100"/>
                                    </td>
                                     <td>
                                         <asp:Button runat="server" ID="btnEliminar" CssClass="ui-button" Width="100"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table class="tabela_campos">
                    <tr>
                        <td>
                            <table width="100%" style="margin: 0px !important;">
                                <tr>
                                    <td>
                                        <pro:ProsegurGridView ID="ProsegurGridViewCodigoAjeno" runat="server" AllowPaging="False"
                                            AllowSorting="True" ColunasSelecao="OidCodigoAjeno" EstiloDestaque="GridLinhaDestaque"
                                            GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                            NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                            Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="False"
                                            AgruparRadioButtonsPeloName="False"
                                            ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True"
                                            HeaderSpanStyle="">
                                            <Pager ID="objPager_ProsegurGridView1">
                                                <FirstPageButton Visible="True">
                                                    <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                    </Image>
                                                </FirstPageButton>
                                                <LastPageButton Visible="True">
                                                </LastPageButton>
                                                <Summary Text="Página {0} de {1} ({2} itens)" />
                                                <SummaryStyle>
                                                </SummaryStyle>
                                            </Pager>
                                            <HeaderStyle Font-Bold="True" />
                                            <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                            <RowStyle CssClass="GridLinhaPadraoImpar" />
                                            <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                            </TextBox>
                                            <Columns>
                                                 <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imbModificar" CommandName="Select" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" CssClass="imgButton" runat="server"></asp:ImageButton>
                                                        <asp:ImageButton ID="imbBorrar" CommandName="Select" ImageUrl="~/App_Themes/Padrao/css/img/button/borrar.png" CssClass="imgButton" runat="server" OnClick="imbBorrar_Click"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Defecto" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbxDefecto" runat="server" OnCheckedChanged="cbxdefecto_CheckedChanged" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CodIdentificador" HeaderText="Identificador"
                                                    SortExpression="" ItemStyle-Width="130px">
                                                    <ItemStyle Width="130px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CodAjeno" HeaderText="Código" SortExpression=""
                                                    ItemStyle-Width="130px">
                                                    <ItemStyle Width="130px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DesAjeno" HeaderText="Descripción" SortExpression=""
                                                    ItemStyle-Width="250px">
                                                    <ItemStyle Width="250px" />
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="OidCodigoAjeno" HeaderText="OidCodigoAjeno" Visible="false" SortExpression=""
                                                    ItemStyle-Width="130px">
                                                    <ItemStyle Width="130px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </pro:ProsegurGridView>
                                    </td>
                                </tr>
                                <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                    <tr>
                                        <td align="center" colspan="4">
                                            <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                                <tr>
                                                    <td style="border-width: 0;">
                                                       
                                                    </td>
                                                    <td style="border-width: 0;">
                                                        <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </asp:Panel>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="margin: 0px !Important;">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="100"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="botaoOcultar">
                    <asp:Button runat="server" ID="btnAlertaSi" CssClass="btn-excluir" />
                </div>
                <script type="text/javascript">
                    //Script necessário para evitar que dê erro ao clicar duas vezes em algum controle que esteja dentro do updatepanel.
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_initializeRequest(initializeRequest);

                    function initializeRequest(sender, args) {
                        if (prm.get_isInAsyncPostBack()) {
                            args.set_cancel(true);
                        }
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
