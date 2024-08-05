<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucInventario.ascx.vb"
    Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ucInventario" %>
<%@ Register Src="ucData.ascx" TagName="ucData" TagPrefix="uc2" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<style type="text/css">
    ul, li
    {
        margin: 0;
        padding: 0;
    }
    
    .tabs
    {
        position: relative;
        top: 1px;
        z-index: 2;
    }
    
    .tab
    {
        border: 1px solid black;
        background-image: url(Imagenes/tab.png);
        background-repeat: repeat-x;
        color: White;
        padding: 2px 10px;
    }
    
    .selectedtab
    {
        background: none;
        background-repeat: repeat-x;
        color: black;
    }
    
    
    .tabcontents
    {
        border: 1px solid black;
        padding: 10px;
        width: 600px;
        height: 230px;
        background-color: white;
    }
    .ordenacao
    {
        border: 1px solid black;
        border-top: 0;
        padding: 10px;
        width: 600px;
        height: 15px;
        background-color: white;
    }
</style>
<asp:Menu ID="MenuTab" Width="216px" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
    RenderingMode="Table" OnMenuItemClick="tabMenu_MenuItemClick" StaticMenuItemStyle-CssClass="tab"
    StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs">
    <Items>
        <asp:MenuItem Value="0" Text=""></asp:MenuItem>
        <asp:MenuItem Value="1" Text=""></asp:MenuItem>
    </Items>
</asp:Menu>
<div class="tabcontents">
    <asp:MultiView ID="mvTab" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewNovo" runat="server">
            <table width="578" cellpadding="0" cellspacing="0" style="padding-left: 15px;">
                <tr>
                    <td align="left" style="width: 8px;">
                        <img alt="" src="Imagenes/marcadorcampo.gif" />
                    </td>
                    <td align="left" style="width: 100px;">
                        <asp:Label ID="lblSectorNovo" runat="server" Text="Setor" CssClass="Lbl2"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList runat="server" ID="ddlSetorNovo" CssClass="Text01">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:RequiredFieldValidator runat="server" ID="rfvddlSetorNovo" ControlToValidate="ddlSetorNovo"
                            InitialValue="0">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="ViewHistorico" runat="server">
            <div>
                <uc2:ucData ID="dataHistorico" runat="server" LabelData="Invetário" DatasObrigatorias="true" />
            </div>
            <table width="578" cellpadding="0" cellspacing="0" style="padding-left: 14px;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left" style="width: 8px;">
                                    <img alt="" src="Imagenes/marcadorcampo.gif" />
                                </td>
                                <td align="left" style="width: 139px;">
                                    <asp:Label ID="lblSectorHistorico" runat="server" Text="Setor" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="ddlSetorHistorico" CssClass="Text01">
                                    </asp:DropDownList>
                                </td>
                                <td align="left">
                                    <asp:RequiredFieldValidator runat="server" ID="rfvddlSetorHistorico" ControlToValidate="ddlSetorHistorico"
                                        InitialValue="0">*</asp:RequiredFieldValidator>
                                </td>
                                <td align="left">
                                    <pro:Botao ID="btnInventario" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Consultar" Titulo="btnBuscar">
                                    </pro:Botao>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="left" style="width: 8px;">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblInventario" runat="server" Text="Setor" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="center">
                                    <pro:ProsegurGridView ID="gdvInventario" runat="server" AgruparRadioButtonsPeloName="False"
                                        Ajax="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                        ColunasSelecao="Identificador" ConfigurarNumeroRegistrosManualmente="False" DataKeyNames="Identificador"
                                        EnableModelValidation="True" EstiloDestaque="GridLinhaDestaque" ExibirCabecalhoQuandoVazio="False"
                                        GerenciarControleManualmente="True" GridPadrao="False" HeaderSpanStyle="" Height="100%"
                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PageSize="5" PaginaAtual="0" PaginacaoAutomatica="True"
                                        Style="margin-right: 0px">
                                        <Pager ID="objPager_ProsegurGridView1" ItemsPerPage="5">
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
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaAlternada" />
                                        <RowStyle CssClass="GridLinhaPadrao" />
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">&nbsp;&nbsp;</TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="col_selecionar">
                                                <ItemTemplate>
                                                    <asp:RadioButton ID="rdbSelecionado" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Codigo" HeaderStyle-HorizontalAlign="Center" HeaderText="col_inventario"
                                                SortExpression="Codigo">
                                                <ItemStyle Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FechaCreacion" HeaderStyle-HorizontalAlign="Center"
                                                HeaderText="col_data" SortExpression="FechaCreacion">
                                                <ItemStyle Width="150px" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <asp:UpdatePanel ID="UpdatePanelGridSemRegistro" runat="server" ChildrenAsTriggers="False"
                                        UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                                <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                                    <tr>
                                                        <td style="border-width: 0;">
                                                            <asp:Image ID="imgErro" runat="server" src="Imagenes/info.jpg" />
                                                        </td>
                                                        <td style="border-width: 0;">
                                                            <asp:Label ID="lblSemRegistro" runat="server" CssClass="Lbl2" Text="Label">[lblSemRegistro]</asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <br />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</div>
<div class="ordenacao">
    <table cellpadding="0" cellspacing="0" style="padding-left: 15px;">
        <tr>
            <td align="left" style="width: 8px;">
                <img alt="" src="Imagenes/marcadorcampo.gif" />
            </td>
            <td align="left" style="width: 100px;">
                <asp:Label ID="lblOrdenacao" runat="server" Text="Ordenado por" CssClass="Lbl2"></asp:Label>
            </td>
            <td align="left">
                <asp:RadioButton runat="server" ID="rbCliente" CssClass="Text01" Text="Cliente-Canal"
                    GroupName="Ordenacao" Checked="true" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="rbDocumento" CssClass="Text01" Text="Documento"
                    GroupName="Ordenacao" />
            </td>
        </tr>
    </table>
</div>
