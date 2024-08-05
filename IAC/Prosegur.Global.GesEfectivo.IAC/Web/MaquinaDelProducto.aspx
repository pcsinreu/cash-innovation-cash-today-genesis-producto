<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaquinaDelProducto.aspx.vb"
    EnableEventValidation="false" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MaquinaDelProducto" MasterPageFile="Master/MasterModal.Master" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>

<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
      <title>IAC - Maquinas Del Producto</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-modal">
                <div>
                    <div class="ui-panel-titlebar">
                        <asp:Label ID="lblTituloMaquinas" CssClass="ui-panel-title" runat="server"></asp:Label>
                    </div>
                </div>
                <table style="width: 100%; background-color: #FFFFFF;">
                    <tr>
                        <td>
                            <table class="tabela_campo">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblProducto" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td >
                                        <asp:Label ID="lblProdutoDescricao" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #FFFFFF">
                            <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="False"
                                ColunasSelecao="codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                Width="100%">
                                <Pager ID="objPager_ProsegurGridView1">
                                    <FirstPageButton Visible="True">
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
                            &nbsp;            
                                </TextBox>
                                <Columns>
                                    <asp:BoundField DataField="DescripcionMaquinas" HeaderText="DescripcionMaquinas"
                                        SortExpression="DescripcionMaquinas" />
                                </Columns>
                            </pro:ProsegurGridView>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                        <tr>
                            <td align="center">
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>
