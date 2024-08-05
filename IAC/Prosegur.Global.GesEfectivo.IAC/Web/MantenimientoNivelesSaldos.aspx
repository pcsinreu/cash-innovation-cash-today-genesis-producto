<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoNivelesSaldos.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoNivelesSaldos" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Erro.ascx" TagName="Erro" TagPrefix="uc1" %>
<%@ Register Src="Controles/Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IAC - Niveles Saldos por SubCanal</title>
     
    <base target="_self" />
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style7
        {
            width: 226px;
        }
        .style10
        {
            width: 121px;
        }
    </style>
</head>
<body class="barraBody" style="width: 893px">
    <div style="width: 900px; margin-left: 2px; margin-right: 2px;">
        <form id="form1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                    <tr>
                        <td>
                            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                            </asp:ScriptManager>
                            <uc3:Cabecalho ID="Cabecalho1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <uc1:Erro ID="ControleErro" runat="server" />
                        </td>
                    </tr>
                    <tr id="trEntidade" runat="server">
                        <td>
                            <table style="width: 888px;" cellspacing="5" cellpadding="3" border="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblCliente" runat="server" Text="lblCliente" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCliente" runat="server" Width="400px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblSubCliente" runat="server" Text="lblSubCliente" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSubCliente" runat="server" Width="400px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblPtoServicio" runat="server" Text="lblPtoServicio" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPtoServicio" runat="server" Width="400px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblDefecto" runat="server" CssClass="Lbl2" Text="lblDefecto"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtDefecto" runat="server" Width="400px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trTituloBusqueda" runat="server">
                        <td class="titulo02">
                            <table cellpadding="0" cellspacing="4" border="0">
                                <tr>
                                    <td>
                                        <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 880px;" cellspacing="5" cellpadding="3" border="0">
                                <tr id="trCamposBusqueda" runat="server">
                                    <td align="right" style="width: 148px">
                                        <asp:Label ID="lblSubCanal" runat="server" CssClass="Lbl2" Text="lblSubCanal"></asp:Label>
                                    </td>
                                    <td width="300px" align="left">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlSubCanal" runat="server" AutoPostBack="True" Width="228px" />
                                                <asp:CustomValidator ID="csvSubCanal" runat="server" ControlToValidate="ddlSubCanal"
                                                    ErrorMessage="" Text="*">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="right" style="width: 100px;">
                                        <asp:Label ID="lblNivelSaldo" runat="server" CssClass="Lbl2" Text="lblNivelSaldo"></asp:Label>
                                    </td>
                                    <td align="left" width="400px">
                                        <asp:TextBox ID="txtNivelSaldo" runat="server" Enabled="False" Width="360px"></asp:TextBox>
                                        <asp:CustomValidator ID="csvNivelSaldo" runat="server" ControlToValidate="txtNivelSaldo"
                                            ErrorMessage="" Text="*">*</asp:CustomValidator>
                                    </td>
                                    <td>
                                        <pro:Botao ID="btnBusquedaNivelSaldo" runat="server" ExibirLabelBtn="false" Habilitado="True"
                                            Tipo="Consultar" Titulo="037_btnBusquedaNivelSaldo">
                                        </pro:Botao>
                                    </td>
                                </tr>
                                <tr id="trBotoesBusqueda" runat="server">
                                    <td colspan="5" align="right">
                                        <table>
                                            <tr>
                                                <td align="right">
                                                    <pro:Botao ID="btnAnadir" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                        Tipo="Adicionar" Titulo="btnAnadir">
                                                    </pro:Botao>
                                                </td>
                                                <td align="right">
                                                    <pro:Botao ID="btnLimpiar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                        Tipo="Sair" Titulo="btnCancelar">
                                                    </pro:Botao>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <table style="width: 878px;"   border="0">
                                <tr>
                                    <td align="center" width="100%">
                                        <pro:ProsegurGridView ID="ProsegurGridViewNivelSaldo" runat="server" AgruparRadioButtonsPeloName="False"
                                            Ajax="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                            ConfigurarNumeroRegistrosManualmente="False" DataKeyNames="OidConfigNivelMov"
                                            EnableModelValidation="True" EstiloDestaque="GridLinhaDestaque" ExibirCabecalhoQuandoVazio="False"
                                            GerenciarControleManualmente="True" GridPadrao="False" HeaderSpanStyle="" Height="100%"
                                            NumeroRegistros="0" OrdenacaoAutomatica="True" PageSize="5" PaginaAtual="0" PaginacaoAutomatica="True"
                                            Width="100%">
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
                                            <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                            </TextBox>
                                            <Columns>
                                                <asp:BoundField DataField="OidConfigNivelMov" HeaderText="OidConfigNivelMov" SortExpression=""
                                                    Visible="false" />
                                                <asp:BoundField DataField="SubCanal" HeaderText="SubCanal" ItemStyle-Width="130px"
                                                    SortExpression="">
                                                    <ItemStyle Width="130px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NivelSaldo" HeaderText="Nível Saldo" ItemStyle-Width="450px"
                                                    SortExpression="">
                                                    <ItemStyle Width="450px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Modificar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imbModificar" runat="server" CommandArgument='<%# Bind("OidConfigNivelMov")%>'
                                                            CommandName="AccionEditar" ImageUrl="Imagenes/pencil.PNG" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imbEliminar" runat="server" CommandArgument='<%# Bind("OidConfigNivelMov") %>'
                                                            CommandName="AccionBorrar" ImageUrl="Imagenes/cross.PNG" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 25%; text-align: center">
                                        <pro:Botao ID="btnGrabar" runat="server" Habilitado="True" Tipo="Salvar" Titulo="btnGrabar"
                                            ExibirLabelBtn="True" ExecutaValidacaoCliente="True">
                                        </pro:Botao>
                                    </td>
                                    <td style="width: 25%; text-align: center">
                                        <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                            Tipo="Sair" Titulo="btnCancelar">
                                        </pro:Botao>
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
                </table>
                </td> </tr> </table>
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
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGrabar" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="AlertDivAll" class="AlertLoading" style="visibility: hidden;">
        </div>
        </form>
    </div>
</body>
</html>
