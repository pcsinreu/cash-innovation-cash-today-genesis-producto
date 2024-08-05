<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoProceso.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoProcesos" MasterPageFile="~/Principal.Master"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Procesos</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Div_Titulo">
        <div class="Div_Titulo_icon">
        </div>
        <div class="Div_Titulo_legenda">
            <asp:Label ID="lblTituloProcesos" runat="server"></asp:Label></div>
    </div>
    <table class="tabela_interna">
        <tr>
            <td>
                <table style="width: auto; margin-top: 10px; margin-bottom: 10px;" cellspacing="0"
                    cellpadding="0" border="0">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" style="width: 100%;">
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" border="0px" style="width: 100%">
                                            <tr>
                                                <td align="right" width="150px" style="white-space: nowrap;">
                                                    <asp:Label ID="lblCliente" runat="server" CssClass="Lbl2"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server">
                                                        <ContentTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0px" width="250px">
                                                                <tr>
                                                                    <td width="170px">
                                                                        <asp:TextBox ID="txtCliente" runat="server" Width="170px" MaxLength="15"></asp:TextBox>
                                                                    </td>
                                                                    <td align="left" width="20px">
                                                                        <pro:Botao ID="btnBuscarCliente" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                                            Tipo="Consultar" Titulo="016_btncliente">
                                                                        </pro:Botao>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CustomValidator ID="csvClienteObrigatorio" runat="server" ControlToValidate="txtCliente"
                                                                            ErrorMessage="" Text="*"></asp:CustomValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td width="5px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubCliente" runat="server" CssClass="Lbl2"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" width="170px">
                                                    <asp:TextBox ID="txtSubCliente" runat="server" Width="170px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <pro:Botao ID="btnSubCliente" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                        Tipo="Consultar" Titulo="016_btnsubcliente">
                                                    </pro:Botao>
                                                </td>
                                                <td width="5px">
                                                    &nbsp;
                                                </td>
                                                <td align="right" style="height: 18px;">
                                                    <asp:Label ID="lblPuntoServicio" runat="server" CssClass="Lbl2" Style="white-space: nowrap;"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                </td>
                                                <td align="left" width="170px">
                                                    <asp:TextBox ID="txtPuntoServicio" runat="server" Width="170px"></asp:TextBox>
                                                </td>
                                                <td align="left">
                                                    <pro:Botao ID="btnPuntoServicio" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                        Tipo="Consultar" Titulo="016_btnpuntoservicio">
                                                    </pro:Botao>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="10">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" width="150px" style="white-space: nowrap;">
                                                    <asp:Label ID="lblCanal" runat="server" CssClass="Lbl2"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:ListBox ID="lstCanal" runat="server" AutoPostBack="True" Height="90px" SelectionMode="Multiple"
                                                        Width="200px"></asp:ListBox>
                                                </td>
                                                <td width="5px">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubCanal" runat="server" CssClass="Lbl2"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:UpdatePanel ID="UpdatePanelSubCanal" runat="server">
                                                        <ContentTemplate>
                                                            <table width="100%" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListBox ID="lstSubCanais" runat="server" Height="90px" SelectionMode="Multiple"
                                                                            Width="200px"></asp:ListBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:CustomValidator ID="csvSubCanalObrigatorio" runat="server" ControlToValidate="lstSubCanais"
                                                                            ErrorMessage="">*</asp:CustomValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td width="5px">
                                                    &nbsp;
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblDelegacion" runat="server" CssClass="Lbl2"></asp:Label>
                                                </td>
                                                <td width="1px">
                                                    &nbsp;
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtDelegacion" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table cellspacing="0" cellpadding="0" style="width: 100%;">
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap; width: 150px;">
                                        <asp:Label ID="lblDescricaoProceso" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <table width="100%" cellpadding="0px" cellspacing="0px">
                                            <tr>
                                                <td width="300px">
                                                    <asp:UpdatePanel ID="UpdatePanelDescricao" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtDescricaoProceso" runat="server" Width="272px" MaxLength="50"
                                                                AutoPostBack="true" CssClass="Text02"></asp:TextBox>
                                                            <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                                                ControlToValidate="txtDescricaoProceso" Text="*"></asp:CustomValidator>
                                                            <asp:CustomValidator ID="csvDescripcionExistente" runat="server" ErrorMessage=""
                                                                ControlToValidate="txtDescricaoProceso">*</asp:CustomValidator>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <asp:UpdateProgress ID="UpdateProgressDescricao" runat="server" AssociatedUpdatePanelID="UpdatePanelDescricao">
                                                        <ProgressTemplate>
                                                            <img src="Imagenes/loader1.gif" alt="Loader" />
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr id="vigenteLinha" runat="server">
                                    <td align="right" style="white-space: nowrap; width: 150px;">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap; width: 150px;">
                                        <asp:Label ID="lblProducto" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelProduto" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlProducto" runat="server" Width="400px" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="csvProductoObrigatorio" runat="server" ControlToValidate="ddlProducto"
                                                    ErrorMessage="" Text="*"></asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap; width: 150px;">
                                        <asp:Label ID="lblModalidad" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelModalidad" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlModalidad" runat="server" Width="400px" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="csvModalidadObrigatorio" runat="server" ControlToValidate="ddlModalidad"
                                                    ErrorMessage="" Text="*"></asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap; width: 150px;">
                                        <asp:Label ID="lblIACParcial" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelIACParcial" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlIACParcial" runat="server" Width="400px" Enabled="False"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap; width: 150px;">
                                        <asp:Label ID="lblIACBulto" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelIACBulto" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlIACBulto" runat="server" Width="400px" Enabled="False" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap; width: 150px;">
                                        <asp:Label ID="lblIACRemesa" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelIACRemesa" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlIACRemesa" runat="server" Width="400px" Enabled="False"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="white-space: nowrap; width: 150px;">
                                        <asp:Label ID="lblClienteFaturacion" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td align="left">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table cellpadding="0px" cellspacing="0px" width="430px">
                                                        <tr>
                                                            <td width="273px">
                                                                <asp:TextBox ID="txtClienteFaturacion" runat="server" Width="272px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <pro:Botao ID="btnBuscarClienteFaturacion" runat="server" EstiloIcone="EstiloIcone"
                                                                    Habilitado="True" Tipo="Consultar" Titulo="016_btnClienteFaturacion">
                                                                </pro:Botao>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="Div_Titulo">
        <div class="Div_Titulo_icon">
        </div>
        <div class="Div_Titulo_legenda">
            <asp:UpdatePanel ID="UpdatePanelInformacionDeclarado" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lblSubTitulosInformacionDelDeclarado" runat="server"></asp:Label>
                    <asp:CustomValidator ID="csvInformacioDeclaradoObrigatorio" runat="server" ErrorMessage=""
                        Text="*" ValidationGroup="InformacionDelDeclarado"></asp:CustomValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <table class="tabela_interna">
        <tr>
            <td>
                <table style="width: auto; margin-top: 10px; margin-bottom: 10px;" cellspacing="0"
                    cellpadding="0" border="0">
                    <tr>
                        <td>
                            <table border="0" cellpadding="3" cellspacing="0" class="tabela_interna">
                                <tr>
                                    <td width="140px" style="height: 26px;">
                                        &nbsp;
                                    </td>
                                    <td style="height: 26px;">
                                        <asp:RadioButton ID="rdbPorAgrupaciones" runat="server" AutoPostBack="True" GroupName="InformacionDelDeclarado" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rdbPorMedioPago" runat="server" AutoPostBack="True" GroupName="InformacionDelDeclarado" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlAgrupacao" runat="server">
        <div class="Div_Titulo">
            <div class="Div_Titulo_icon">
            </div>
            <div class="Div_Titulo_legenda">
                <asp:Label ID="lblSubTitulosAgrupaciones" runat="server"></asp:Label>
            </div>
        </div>
        <table class="tabela_interna">
            <tr>
                <td>
                    <table style="width: auto; margin-top: 10px; margin-bottom: 10px;" cellspacing="0"
                        cellpadding="0" border="0">
                        <tr>
                            <td align="left">
                                <table border="0" cellpadding="0" cellspacing="2">
                                    <tr>
                                        <td width="150px">
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="5" style="width: 100%;">
                                                <tr>
                                                    <td id="Td1" runat="server" style="width: 350px; border-width: 1px; vertical-align: top;">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel ID="UpdatePanelAgrupacionesPossibles" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:ListBox ID="lstAgrupacionesPosibles" runat="server" Height="120px" SelectionMode="Multiple"
                                                                                Width="350px"></asp:ListBox>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                                            <asp:AsyncPostBackTrigger ControlID="imgBtnAgrupacionesIncluirTodas" EventName="Click" />
                                                                            <asp:AsyncPostBackTrigger ControlID="imgBtnAgrupacionesIncluir" EventName="Click" />
                                                                            <asp:AsyncPostBackTrigger ControlID="imgBtnAgrupacionesExcluir" EventName="Click" />
                                                                            <asp:AsyncPostBackTrigger ControlID="imgBtnAgrupacionesExcluirTodas" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="center" style="width: 68px; border-width: 0;">
                                                        <asp:ImageButton ID="imgBtnAgrupacionesIncluirTodas" runat="server" ImageUrl="~/Imagenes/pag07.png" /><br />
                                                        <asp:ImageButton ID="imgBtnAgrupacionesIncluir" runat="server" ImageUrl="~/Imagenes/pag05.png" /><br />
                                                        <asp:ImageButton ID="imgBtnAgrupacionesExcluir" runat="server" ImageUrl="~/Imagenes/pag06.png" /><br />
                                                        <asp:ImageButton ID="imgBtnAgrupacionesExcluirTodas" runat="server" ImageUrl="~/Imagenes/pag08.png" />
                                                    </td>
                                                    <td style="width: 350px; border-width: 1px; vertical-align: top;">
                                                        <asp:UpdatePanel ID="UpdatePanelAgrupacionCompoenProceso" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ListBox ID="lstAgrupacionesCompoenProceso" runat="server" Height="120px" SelectionMode="Multiple"
                                                                                Width="350px"></asp:ListBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CustomValidator ID="csvAgrupacionesCompoenProcesoObrigatorio" runat="server"
                                                                                ControlToValidate="" ErrorMessage="" Text="*"></asp:CustomValidator>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="imgBtnAgrupacionesIncluirTodas" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="imgBtnAgrupacionesIncluir" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="imgBtnAgrupacionesExcluir" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="imgBtnAgrupacionesExcluirTodas" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlMediosPago" runat="server">
        <div class="Div_Titulo">
            <div class="Div_Titulo_icon">
            </div>
            <div class="Div_Titulo_legenda">
                <asp:Label ID="lblSubTitulosMediosPago" runat="server"></asp:Label>
            </div>
        </div>
        <table class="tabela_interna">
            <tr>
                <td>
                    <table style="width: auto; margin-top: 10px; margin-bottom: 10px;" cellspacing="0"
                        cellpadding="0" border="0">
                        <tr>
                            <td align="left">
                                <asp:UpdatePanel ID="UpdatePanelTreeView" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="2">
                                            <tr>
                                                <td width="150px">
                                                </td>
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table border="1" cellpadding="0" cellspacing="5" style="width: 100%;">
                                                                    <tr>
                                                                        <td id="TdTreeViewDivisa" runat="server" style="width: 350px; border-width: 1px;
                                                                            vertical-align: top;">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TreeView ID="TrvDivisas" runat="server" CssClass="Lbl2" ShowLines="True">
                                                                                            <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                                                        </asp:TreeView>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="center" style="width: 68px; border-width: 0;">
                                                                            <asp:ImageButton ID="imgBtnIncluirTreeview" runat="server" ImageUrl="~/Imagenes/pag05.png" />
                                                                            <br />
                                                                            <asp:ImageButton ID="imgBtnExcluirTreeview" runat="server" ImageUrl="~/Imagenes/pag06.png" />
                                                                        </td>
                                                                        <td style="width: 350px; border-width: 1px; vertical-align: top;">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TreeView ID="TrvProcesos" runat="server" CssClass="Lbl2" ShowLines="True">
                                                                                            <SelectedNodeStyle CssClass="TreeViewSelecionado" />
                                                                                        </asp:TreeView>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="3">
                                                                <asp:Label ID="lblNota" runat="server" CssClass="Lbl2" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <asp:CustomValidator ID="csvTrvProcesos" runat="server" ControlToValidate="" ErrorMessage=""
                                                        Text="*"></asp:CustomValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div class="Div_Titulo">
        <div class="Div_Titulo_icon">
        </div>
        <div class="Div_Titulo_legenda">
            <asp:Label ID="lblSubTitulosModoContaje" runat="server"></asp:Label>
        </div>
    </div>
    <table class="tabela_interna">
        <tr>
            <td>
                <table style="width: auto; margin-top: 10px; margin-bottom: 10px;" cellspacing="0"
                    cellpadding="0" border="0">
                    <tr>
                        <td>
                            <table border="0" cellpadding="3" cellspacing="0" class="tabela_campos">
                                <tr>
                                    <td width="140px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkContarChequeTotales" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkContarTicketTotales" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkContarOtrosValoresTotales" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:HiddenField ID="chkContarTarjetaTotales" runat="server" />
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table cellpadding="0" cellspacing="2">
                                <tr>
                                    <td align="right" width="150px">
                                        <asp:Label ID="lblObservaciones" runat="server" CssClass="Lbl2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="Text02" Height="96px"
                                            MaxLength="500" TextMode="MultiLine" Width="610px"></asp:TextBox>
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
                            &nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelAcaoPagina" runat="server">
                                <ContentTemplate>
                                    <table border="0" cellpadding="0" cellspacing="5">
                                        <tr>
                                            <td>
                                                <pro:Botao ID="btnTerminoMedioPago" runat="server" ExecutaValidacaoCliente="true"
                                                    ExibirLabelBtn="True" Habilitado="True" Tipo="Grafico" Titulo="016_btnTerminoMedioPago">
                                                </pro:Botao>
                                            </td>
                                            <td>
                                                <pro:Botao ID="btnTolerancia" runat="server" ExecutaValidacaoCliente="true" ExibirLabelBtn="True"
                                                    Habilitado="True" Tipo="Abrir" Titulo="016_btnTolerancia">
                                                </pro:Botao>
                                            </td>
                                            <td>
                                                <pro:Botao ID="btnGrabar" runat="server" ExecutaValidacaoCliente="true" ExibirLabelBtn="True"
                                                    Habilitado="True" Tipo="Salvar" Titulo="btnGrabar">
                                                </pro:Botao>
                                            </td>
                                            <td>
                                                <pro:Botao ID="btnCancelar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                    Tipo="Sair" Titulo="btnCancelar">
                                                </pro:Botao>
                                            </td>
                                            <td>
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
            </td>
        </tr>
    </table>
</asp:Content>
