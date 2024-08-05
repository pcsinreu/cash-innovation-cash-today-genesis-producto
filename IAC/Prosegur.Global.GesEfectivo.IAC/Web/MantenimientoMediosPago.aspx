<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoMediosPago.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoMediosPago" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de MediosPago</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0" border="0px">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloMediosPago" runat="server"></asp:Label>
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
                <table class="tabela_campos"   border="0px">
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblTipoMedioPago" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelTipoMedioPago" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTipoMedioPago" runat="server" Width="208px" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="csvTipoMedioPagoObrigatorio" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlTipoMedioPago" Text="*"></asp:CustomValidator><asp:CustomValidator
                                                        ID="csvTipoMedioPagoExistente" runat="server" ErrorMessage="" ControlToValidate="ddlTipoMedioPago">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressTipoMedioPago" runat="server" AssociatedUpdatePanelID="UpdatePanelTipoMedioPago">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tamanho_celula" align="right">
                            <asp:Label ID="lblDivisa" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td width="260px">
                                        <asp:UpdatePanel ID="UpdatePanelDivisa" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlDivisa" runat="server" Width="240px" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="csvDivisaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlDivisa"
                                                    Text="*"></asp:CustomValidator><asp:CustomValidator ID="csvDivisaMedioPagoExistente"
                                                        runat="server" ErrorMessage="" ControlToValidate="ddlDivisa">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressDivisa" runat="server" AssociatedUpdatePanelID="UpdatePanelDivisa">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCodigoMedioPago" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td width="215px">
                                        <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoMedioPago" runat="server" MaxLength="15" AutoPostBack="true"
                                                    CssClass="Text02" Width="200px"></asp:TextBox><asp:CustomValidator ID="csvCodigoObrigatorio"
                                                        runat="server" ErrorMessage="" ControlToValidate="txtCodigoMedioPago" Text="*"></asp:CustomValidator><asp:CustomValidator
                                                            ID="csvCodigoMedioPagoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoMedioPago">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressCodigo" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigo">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblDescricaoMedioPago" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDescricaoMedioPago" runat="server" Width="235px" MaxLength="50"
                                        CssClass="Text02" AutoPostBack="True"></asp:TextBox><asp:CustomValidator ID="csvDescricaoObrigatorio"
                                            runat="server" ErrorMessage="001_msg_MedioPagodescripcionobrigatorio" ControlToValidate="txtDescricaoMedioPago"
                                            Text="*"></asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCodigoAcceso" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                <tr>
                                    <td width="40px">
                                        <asp:UpdatePanel ID="UpdatePanelCodigoAcceso" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtCodigoAcceso" runat="server" MaxLength="1" AutoPostBack="true"
                                                    CssClass="Text02" Width="20px"></asp:TextBox><asp:CustomValidator ID="csvCodigoAccesoObligatorio"
                                                        runat="server" ErrorMessage="" ControlToValidate="txtCodigoAcceso" Text="*"></asp:CustomValidator><asp:CustomValidator
                                                            ID="csvCodigoAccesoExistente" runat="server" ErrorMessage="" ControlToValidate="txtCodigoAcceso">*</asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdateProgress ID="UpdateProgressCodigoAcceso" runat="server" AssociatedUpdatePanelID="UpdatePanelCodigoAcceso">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblObservaciones" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="4000" CssClass="Text02"
                                Height="96px" Width="640px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblMercancia" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:UpdatePanel ID="updMercancia" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chkMercancia" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoMedioPago" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkVigente" runat="server" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblSubTitulosMediosPago" runat="server"></asp:Label>
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
            <td align="center">
                <table border="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style="">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="ProsegurGridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="codigo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" Width="95%" ExibirCabecalhoQuandoVazio="false"
                                        Height="100%">
                                        <Pager ID="objPager_ProsegurGridView1">
                                            <FirstPageButton Visible="True">
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
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="codigo" HeaderText="Código" SortExpression="" />
                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="" />
                                            <asp:BoundField DataField="DescripcionFormato" HeaderText="Formato" SortExpression="" />
                                            <asp:TemplateField HeaderText="Vigente" SortExpression="">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OrdenTermino" HeaderText="OrdenTermino" SortExpression=""
                                                Visible="false" />
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAlta" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnBaja" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnModificacion" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnConsulta" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAcima" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAbaixo" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="left" style="width: 50px">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnAcima" runat="server" Height="23px" ImageUrl="~/Imagenes/pag03.png"
                                                    Width="25px" />&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnAbaixo" runat="server" ImageUrl="~/Imagenes/pag02.png" />&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                    <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                        <tr>
                            <td style="border-width: 0;">
                                
                            </td>
                            <td style="border-width: 0;">
                                <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="Lbl2">Não existem dados a serem exibidos.</asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanelBtnsGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnAlta" runat="server" Habilitado="True" Tipo="Novo" Titulo="btnAlta"
                                        ExibirLabelBtn="True">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnBaja" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Excluir" Titulo="btnBaja">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnModificacion" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Editar" Titulo="btnModificacion">
                                    </pro:Botao>
                                </td>
                                <td style="width: 25%; text-align: center">
                                    <pro:Botao ID="btnConsulta" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Localizar" Titulo="btnConsulta">
                                    </pro:Botao>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgressBtns" runat="server" AssociatedUpdatePanelID="UpdatePanelBtnsGrid">
                    <ProgressTemplate>
                        <div id="divLoading" class="AlertLoading" style="visibility: hidden;">
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                    <ContentTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
