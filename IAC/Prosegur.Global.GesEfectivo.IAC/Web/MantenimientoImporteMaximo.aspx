<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoImporteMaximo.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoImporteMaximo" MasterPageFile="Master/MasterModal.Master" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento Importe Máximo</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <base target="_self" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="margin-top: 15px;">
                    <table class="tabela_campos">
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblCodigoImporte" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCodImporte" runat="server" Width="160px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblDescricaoImporte" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescImporte" runat="server" Width="190px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel runat="server" ID="pnHelper" Enabled="True">
                        <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                            <ContentTemplate>
                                <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    <table class="tabela_campos" >
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblCanal" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:UpdatePanel ID="updatePanelCanal" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlCanal" runat="server" Width="163px" AutoPostBack="True" OnSelectedIndexChanged="ddlCanal_SelectedIndexChanged" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                                        <asp:CustomValidator ID="csvCanalObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlCanal" Text="*"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblSubCanal" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td align="left" colspan="3">
                                <asp:UpdatePanel ID="updatePanelSubCanal" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSubCanal" runat="server" Width="163px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                                        <asp:CustomValidator ID="csvSubCanalObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlSubCanal" Text="*"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblDivisa" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updatePanelDivisa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlDivisa" runat="server" Width="163px" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:DropDownList>
                                        <asp:CustomValidator ID="csvDivisaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlDivisa" Text="*"></asp:CustomValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblValorMaximo" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanelValor" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtValorMaximo" runat="server" Width="160px" MaxLength="19" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                        <asp:CustomValidator ID="csvValorMaximoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtValorMaximo" Text="*"></asp:CustomValidator>
                                    </ContentTemplate>
                                   <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" /> 
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanho_celula">
                                <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upChkVigenteTela" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkVigenteTela" runat="server"></asp:CheckBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 25%; text-align: center">
                                <asp:UpdatePanel ID="upBotaoAceptar" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="btnAceptar" CssClass="ui-button" Width="130"/>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                 <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="130"/>
                            </td>
                        </tr>
                    </table>
                    <div style="margin-top: 20px;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="center" style="width: 100%;">
                                    <asp:UpdatePanel ID="upGridImporteMAximo" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <pro:ProsegurGridView ID="ProsegurGridViewImporteMaximo" runat="server" AllowPaging="True"
                                                AllowSorting="False" ColunasSelecao="oidImporte" EstiloDestaque="GridLinhaDestaque"
                                                GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                                NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                                Width="99%" Height="100%" ExibirCabecalhoQuandoVazio="true"
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
                                                    <asp:BoundField DataField="oidimportemaximo" HeaderText="oidImporte" Visible="false"
                                                        SortExpression="" ItemStyle-Width="130px">
                                                        <ItemStyle Width="130px" />
                                                    </asp:BoundField>
                                                     <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imbModificar" CommandName="Select" ImageUrl="App_Themes/Padrao/css/img/grid/edit.png" runat="server" CssClass="imgButton"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="CodigoImporte" HeaderText="Codigo"
                                                        SortExpression="" ItemStyle-Width="130px">
                                                        <ItemStyle Width="130px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression=""
                                                        ItemStyle-Width="130px">
                                                        <ItemStyle Width="130px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Divisa" HeaderText="Divisa" SortExpression=""
                                                        ItemStyle-Width="250px">
                                                        <ItemStyle Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ValorMaximo" HeaderText="ValorMaximo" SortExpression=""
                                                        ItemStyle-Width="250px">
                                                        <ItemStyle Width="250px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Vigente" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkVigenteGrid" runat="server" Enabled="false" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </asp:TemplateField>
                                                     <asp:BoundField DataField="Canal" HeaderText="Canal" SortExpression=""
                                                        ItemStyle-Width="100px">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="SubCanal" HeaderText="SubCanal" SortExpression=""
                                                        ItemStyle-Width="100px">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </pro:ProsegurGridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                                                    <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="label2">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>
                    </div>
                    <div style="margin-top: 20px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnGrabar" CssClass="ui-button" Width="130"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
