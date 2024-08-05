<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoTolerancias.aspx.vb" MasterPageFile="Master/MasterModal.Master"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoTolerancias" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IAC - Mantenimiento de Subcanales</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <div class="ui-panel-titlebar">
                <asp:Label ID="lblSubTituloTolerancia" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
                <table style="width: 100%;" >
                    <tr>
                        <td>
                            <table style="margin: 0px !Important; width: 100%;">
                                <tr>
                                    <td align="center" width="100%">
                                        <pro:ProsegurGridView ID="ProsegurGridViewAgrupacion" runat="server" Ajax="True"
                                            AllowSorting="False" AutoGenerateColumns="False" ColunasSelecao="Codigo" ConfigurarNumeroRegistrosManualmente="False"
                                            EstiloDestaque="GridLinhaDestaque" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True"
                                            GridPadrao="False" Height="100%" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                            PaginaAtual="0" PaginacaoAutomatica="False" Width="99%" DataKeyNames="CodigoAgrupacion">
                                            <Columns>
                                                <asp:BoundField DataField="CodigoAgrupacion" HeaderText="CodigoAgrupacion" Visible="false" />
                                                <asp:BoundField DataField="DescripcionAgrupacion" HeaderText="DescripcionAgrupacion" />
                                                <asp:TemplateField HeaderText="ToleranciaParcialMin">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ToleranciaParcialMin") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaParcialMax">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ToleranciaParcialMax") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaBultoMin">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ToleranciaBultoMin") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaBultoMax">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ToleranciaBultoMax") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaRemesaMin">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("ToleranciaRemesaMin") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaRemesaMax">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("ToleranciaRemesaMax") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <Pager ID="objPager_ProsegurGridView2">
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
                                            <TextBox ID="objTextoProsegurGridView2" AutoPostBack="True" MaxLength="10" Width="30px">            
                                            &nbsp;&nbsp;
                                            &nbsp;
                                            </TextBox>
                                        </pro:ProsegurGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="100%">
                                        <pro:ProsegurGridView ID="ProsegurGridViewMedioPago" runat="server" Ajax="True" AllowSorting="False"
                                            AutoGenerateColumns="False" ColunasSelecao="Codigo" ConfigurarNumeroRegistrosManualmente="False"
                                            EstiloDestaque="GridLinhaDestaque" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True"
                                            GridPadrao="False" Height="100%" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                            PaginaAtual="0" PaginacaoAutomatica="False" Width="99%" EnableViewState="true"
                                            DataKeyNames="CodigoDivisa,CodigoTipoMedioPago,CodigoMedioPago">
                                            <Columns>
                                                <asp:BoundField DataField="CodigoDivisa" HeaderText="CodigoDivisa" Visible="false" />
                                                <asp:BoundField DataField="DescripcionDivisa" HeaderText="DescripcionDivisa" />
                                                <asp:BoundField DataField="CodigoTipoMedioPago" HeaderText="CodigoTipoMedioPago"
                                                    Visible="false" />
                                                <asp:BoundField DataField="DescripcionTipoMedioPago" HeaderText="DescripcionTipoMedioPago" />
                                                <asp:BoundField DataField="CodigoMedioPago" HeaderText="CodigoMedioPago" Visible="false" />
                                                <asp:BoundField DataField="DescripcionMedioPago" HeaderText="DescripcionMedioPago" />
                                                <asp:TemplateField HeaderText="ToleranciaParcialMin">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ToleranciaParcialMin") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaParcialMax">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("ToleranciaParcialMax") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaBultoMin">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ToleranciaBultoMin") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaBultoMax">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("ToleranciaBultoMax") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaRemesaMin">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("ToleranciaRemesaMin") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ToleranciaRemesaMax">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("ToleranciaRemesaMax") %>'
                                                            Width="55px" EnableViewState="true" MaxLength="19"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <Pager ID="Pager1">
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
                                            <TextBox ID="TextBox7" AutoPostBack="True" MaxLength="10" Width="30px">            
                                            &nbsp;&nbsp;            
                                            &nbsp;            
                                            </TextBox>
                                        </pro:ProsegurGridView>
                                        <br />
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
                        <td >
                            <table style="margin: 0px !Important;">
                                <tr>
                                    <td >
                                        <asp:Button runat="server" ID="btnAceptar" Width="120px" CssClass="ui-button"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

