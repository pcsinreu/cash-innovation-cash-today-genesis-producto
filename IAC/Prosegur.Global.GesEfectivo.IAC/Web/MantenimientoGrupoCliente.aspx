<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoGrupoCliente.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoGrupoCliente" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Grupo de Cliente</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 417px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidBtnGrabar" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1"  runat="server" >
    <ContentTemplate>
    
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloGrupoCliente" runat="server"></asp:Label>
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
                <table class="tabela_campos"   >
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upCodigo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" AutoPostBack="true" CssClass="Text02"
                                        IdDivExibicao="DivCodigo" Width="130px"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigo"
                                        Text="*"></asp:CustomValidator>
                                    <asp:CustomValidator ID="csvCodigoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigo">*</asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblDescripcion" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="upDescripcion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Width="96%" MaxLength="50" AutoPostBack="true"
                                        CssClass="Text02" IdDivExibicao="DivDescripcion"></asp:TextBox>
                                    <asp:CustomValidator ID="csvDescripcionObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="txtCodigo" Text="*" Width="1px"></asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left">
                            <%--<asp:Button ID="btnDirecion" runat="server" />--%>
                            <pro:Botao ID="btnDireccion" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                Tipo="Consultar" ExibirLabelBtn="true" Titulo="030_btnDireccion">
                            </pro:Botao>
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right" valign="top">
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td valign="top">
                            <asp:CheckBox ID="chkVigente" runat="server" Checked />
                        </td>
                         <td align="right">
                            <asp:Label ID="lblTipoGrupoCliente" runat="server" CssClass="Lbl2"></asp:Label>
                         </td>
                        <td  align="left">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                     <asp:CustomValidator ID="csvTipoGrupoClienteObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="ddlTipoGrupoCliente" Text="*" Width="1px"></asp:CustomValidator>
                                    <asp:DropDownList ID="ddlTipoGrupoCliente" runat="server"></asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right" valign="top">
                            <asp:Label ID="lblCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td valign="top" colspan="3">
                            <asp:UpdatePanel ID="upCliente" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCliente" runat="server" Enabled="False" Width="98.4%"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnRemover" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAnadir" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td valign="top" align="left">
                            <pro:Botao ID="btnBuscaCliente" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                Tipo="Consultar" ExibirLabelBtn="false" Titulo="030_busca_cliente">
                            </pro:Botao>
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right" valign="top">
                            <asp:Label ID="lblSubCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td valign="top" colspan="3">
                            <asp:UpdatePanel ID="upSubCliente" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSubCliente" runat="server" Enabled="False" Width="98.4%"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnRemover" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAnadir" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td valign="top" align="left">
                            <pro:Botao ID="btnBuscaSubCliente" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                Tipo="Consultar" ExibirLabelBtn="false" Titulo="030_busca_subcliente">
                            </pro:Botao>
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right" valign="top">
                            <asp:Label ID="lblPtoServicio" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td valign="top" colspan="3">
                            <asp:UpdatePanel ID="uPtoServicio" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPtoServicio" runat="server" Enabled="False" Width="98.4%"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnRemover" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAnadir" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td valign="top" align="left">
                            <pro:Botao ID="btnBuscaPtoServicio" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                Tipo="Consultar" ExibirLabelBtn="false" Titulo="030_busca_pto_servicio">
                            </pro:Botao>
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right" valign="top">
                            &nbsp;
                        </td>
                        <td valign="top">
                            &nbsp;
                        </td>
                        <td align="right" valign="top">
                            &nbsp;
                        </td>
                        <td valign="top" align="right">
                            <table>
                                <tr>
                                    <td>
                                        <pro:Botao ID="btnAnadir" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                            Tipo="Adicionar"  ExecutaValidacaoCliente="false" Titulo="btnAnadir" ExibirLabelBtn="True">
                                        </pro:Botao>
                                    </td>
                                    <td>
                                        <pro:Botao ID="btnRemover" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                            Tipo="Sair" Titulo="btnCancelar">
                                        </pro:Botao>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="left" valign="top" colspan="5">
                            <asp:Label ID="lblClienteResultado" runat="server" CssClass="Lbl2"></asp:Label>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="left" valign="top" colspan="5">
                           
                                    <pro:ProsegurGridView ID="gdvClientes" runat="server" AllowPaging="True"
                                            AllowSorting="True" ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque"
                                            GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                            NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="True"
                                            Width="95%" Height="100%" ExibirCabecalhoQuandoVazio="False" 
                                            AgruparRadioButtonsPeloName="False" 
                                            ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True" 
                                            HeaderSpanStyle=""
                                        DataKeyNames="OidGrupoClienteDetalle,OidCliente">
                                        <Pager ID="objPager_gdvClientes">
                                            <FirstPageButton Visible="True">
                                                <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                </Image>
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Pgina {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaAlternada" />
                                        <RowStyle CssClass="GridLinhaPadrao" HorizontalAlign="Center" />
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField HeaderText="DesCliente" DataField="DesCliente" SortExpression="DesCliente" />
                                          
                                          
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/Imagenes/cross.PNG" CommandName="Deletar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                            
                                    <asp:Panel ID="pnlSemRegistroCliente" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                            <tr>
                                                <td style="border-width: 0;">
                                                   
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistroCliente" runat="server" CssClass="Lbl2" Text="Label">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="left" valign="top" colspan="5">
                            <asp:Label ID="lblSubClienteResultado" runat="server" CssClass="Lbl2"></asp:Label>
                            &nbsp; &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="left" valign="top" colspan="5">
                          
                                    <pro:ProsegurGridView ID="gdvSubClientes" runat="server" Ajax="True"   AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" EstiloDestaque="GridLinhaDestaque"
                                        ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True" GridPadrao="False"
                                        Height="100%" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0"
                                        PaginacaoAutomatica="True" Width="95%" AgruparRadioButtonsPeloName="False" ConfigurarNumeroRegistrosManualmente="False"
                                        EnableModelValidation="True" HeaderSpanStyle="" 
                                        DataKeyNames="OidGrupoClienteDetalle,OidCliente,oidSubCliente">
                                        <Pager ID="objPager_gdvClientes0">
                                            <FirstPageButton Visible="True">
                                                <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                </Image>
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Pgina {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaAlternada" />
                                        <RowStyle CssClass="GridLinhaPadrao" HorizontalAlign="Center" />
                                        <TextBox ID="objTextoProsegurGridView2" AutoPostBack="True" MaxLength="10" 
                                            Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField HeaderText="DesCliente" DataField="DesCliente" SortExpression="DesCliente" />
                                            <asp:BoundField DataField="codSubCliente" HeaderText="codSubCliente" SortExpression="codSubCliente" />
                                            <asp:BoundField DataField="DesSubCliente" HeaderText="DesSubCliente" SortExpression="DesSubCliente" />
                                              <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/Imagenes/cross.PNG" CommandName="Deletar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                               
                                    <asp:Panel ID="pnlSemRegistroSubCliente" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                            <tr>
                                                <td style="border-width: 0;">
                                                    
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistroSubCliente" runat="server" CssClass="Lbl2" Text="Label">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                               
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="left" valign="top" colspan="5">
                            <asp:Label ID="lblPtoServicioResultado" runat="server" CssClass="Lbl2"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="left" valign="top" colspan="5">
                           
                                    <pro:ProsegurGridView ID="gdvPtoServicio" runat="server" Ajax="True" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" EstiloDestaque="GridLinhaDestaque"
                                        ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True" GridPadrao="False"
                                        Height="100%" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0"
                                        PaginacaoAutomatica="True" Width="95%" AgruparRadioButtonsPeloName="False" ConfigurarNumeroRegistrosManualmente="False"
                                        EnableModelValidation="True" HeaderSpanStyle="" 
                                        
                                        DataKeyNames="OidGrupoClienteDetalle,OidCliente,oidSubCliente,oidPtoServicio">
                                        <Pager ID="objPager_gdvClientes1">
                                            <FirstPageButton Visible="True">
                                                <Image Url="mvwres://Prosegur.Web, Version=3.1.1203.901, Culture=neutral, PublicKeyToken=a8a76e1d318ac1f1/Prosegur.Web.pFirst.gif">
                                                </Image>
                                            </FirstPageButton>
                                            <LastPageButton Visible="True">
                                            </LastPageButton>
                                            <Summary Text="Pgina {0} de {1} ({2} itens)" />
                                            <SummaryStyle>
                                            </SummaryStyle>
                                        </Pager>
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaAlternada" />
                                        <RowStyle CssClass="GridLinhaPadrao" HorizontalAlign="Center" />
                                        <TextBox ID="objTextoProsegurGridView3" AutoPostBack="True" MaxLength="10" 
                                            Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="CodCliente" HeaderText="CodCliente" SortExpression="CodCliente" />
                                            <asp:BoundField HeaderText="DesCliente" DataField="DesCliente" SortExpression="DesCliente" />
                                             <asp:BoundField DataField="codSubCliente" HeaderText="codSubCliente" SortExpression="codSubCliente" />
                                            <asp:BoundField DataField="DesSubCliente" HeaderText="DesSubCliente" SortExpression="DesSubCliente" />
                                            <asp:BoundField DataField="codPtoServicio" HeaderText="codPtoServicio" SortExpression="codPtoServicio" />
                                            <asp:BoundField DataField="DesPtoServicio" HeaderText="DesPtoServicio" SortExpression="DesPtoServicio" />
                                               <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="~/Imagenes/cross.PNG" CommandName="Deletar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                              
                                    <asp:Panel ID="pnlSemRegistroPtoServico" runat="server" Visible="false">
                                        <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                            <tr>
                                                <td style="border-width: 0;">
                                                    
                                                </td>
                                                <td style="border-width: 0;">
                                                    <asp:Label ID="lblSemRegistroPtoServico" runat="server" CssClass="Lbl2" Text="Label">Não existem dados a serem exibidos.</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                              
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td colspan="3">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="center" colspan="4">
                           <%-- <asp:UpdatePanel ID="UpdatePanelAcaoPagina"  ChildrenAsTriggers="false" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
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
                             <%--   </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
 <%--   <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAnadir" EventName="Click" />
        </Triggers>--%>
    </asp:UpdatePanel>
     
</asp:Content>
