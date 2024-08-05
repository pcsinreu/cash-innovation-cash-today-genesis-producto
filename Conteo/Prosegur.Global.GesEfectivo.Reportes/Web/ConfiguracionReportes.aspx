<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="ConfiguracionReportes.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.ConfiguracionReportes" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reportes - Configuración de Reportes</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="titulo02">
                        <table cellpadding="0" cellspacing="4">
                            <tr>
                                <td>
                                    <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                </td>
                                <td>
                                    <asp:Label ID="lblTituloConfiguracionReportes" runat="server"></asp:Label>
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
                        <table class="tabela_campos" cellspacing="0" cellpadding="3" runat="server" id="tabelaCampos">
                            <tr class="TrSinCor">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left" width="150">
                                    <asp:Label ID="lblCodConfiguracion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodConfiguracion" runat="server" MaxLength="15" CssClass="Text01"
                                                Width="100px"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodConfiguracion" runat="server" ErrorMessage="" ControlToValidate="txtCodConfiguracion">*</asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left" width="150">
                                    <asp:Label ID="lblDesConfiguracion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelConfiguracion" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtDesConfiguracion" runat="server" MaxLength="50" CssClass="Text01"
                                                Width="350px"></asp:TextBox>
                                            <asp:CustomValidator ID="csvDesConfiguracion" runat="server" ErrorMessage="" ControlToValidate="txtDesConfiguracion">*</asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblReportes" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelReportes" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlReportes" runat="server" Width="240px" CssClass="Text01"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvReportes" runat="server" ErrorMessage="" ControlToValidate="ddlReportes">*</asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnDelegacion">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblDelegacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelDelegacion" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlDelegacion" runat="server" Width="240px" AutoPostBack="True"
                                                CssClass="Text01">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvDelegacion" runat="server" ErrorMessage="" ControlToValidate="ddlDelegacion">*</asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnFechaConteo">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left" runat="server" id="CollblFechaConteo">
                                    <asp:Label ID="lblFechaConteo" runat="server" CssClass="Lbl2" Width="110px"></asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td runat="server" id="collblFechaConteoDesde" align="right">
                                                <asp:Label ID="lblFechaConteoDesde" runat="server" CssClass="Lbl2"></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td runat="server" id="coltxtFechaConteoDesde">
                                                <asp:UpdatePanel ID="UpdatePanelFechaConteoDesde" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaConteoDesde" runat="server" MaxLength="16" Width="90px"
                                                            CssClass="Text01"></asp:TextBox>
                                                        <asp:ImageButton ID="imbFechaConteoDesde" ImageUrl="~/Imagenes/calendar.gif" runat="server" />
                                                        <asp:CustomValidator ID="csvFechaConteoDesde" runat="server" ErrorMessage="" ControlToValidate="txtFechaConteoDesde">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="right" runat="server" id="CollblFechaConteoHasta">
                                                <asp:Label ID="lblFechaConteoHasta" runat="server" CssClass="Lbl2" Width="50px"></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td runat="server" id="ColtxtFechaConteoHasta">
                                                <asp:UpdatePanel ID="UpdatePanelFechaConteoHasta" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaConteoHasta" runat="server" MaxLength="16" Width="90px"
                                                            CssClass="Text01"></asp:TextBox>
                                                        <asp:ImageButton ID="imbFechaConteoHasta" ImageUrl="~/Imagenes/calendar.gif" runat="server" />
                                                        <asp:CustomValidator ID="csvFechaConteoHasta" runat="server" ErrorMessage="" ControlToValidate="txtFechaConteoHasta">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnFechaTransporte">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left" runat="server" id="CollblFechaTransporte">
                                    <asp:Label ID="lblFechaTransporte" runat="server" CssClass="Lbl2" Width="110px"></asp:Label>
                                </td>
                                <td align="left" colspan="3">
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td runat="server" id="collblFechaTransporteDesde" align="right">
                                                <asp:Label ID="lblFechaTransporteDesde" runat="server" CssClass="Lbl2"></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td runat="server" id="ColtxtFechaTransporteDesde">
                                                <asp:UpdatePanel ID="UpdatePanelFecTransporteDesde" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaTransporteDesde" runat="server" MaxLength="12" Width="90px"
                                                            CssClass="Text01"></asp:TextBox>
                                                        <asp:ImageButton ID="imbFechaTransporteDesde" ImageUrl="~/Imagenes/calendar.gif"
                                                            runat="server" />
                                                        <asp:CustomValidator ID="csvFechaTransporteDesde" runat="server" ErrorMessage=""
                                                            ControlToValidate="txtFechaTransporteDesde">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="right" runat="server" id="CollblFechaTransporteHasta">
                                                <asp:Label ID="lblFechaTransporteHasta" runat="server" CssClass="Lbl2" Width="50px"></asp:Label>
                                                &nbsp;
                                            </td>
                                            <td runat="server" id="ColtxtFechaTransporteHasta">
                                                <asp:UpdatePanel ID="UpdatePanelFecTransporteHasta" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFechaTransporteHasta" runat="server" MaxLength="12" Width="90px"
                                                            CssClass="Text01"></asp:TextBox>
                                                        <asp:ImageButton ID="imbFechaTransporteHasta" ImageUrl="~/Imagenes/calendar.gif"
                                                            runat="server" />
                                                        <asp:CustomValidator ID="csvFechaTransporteHasta" runat="server" ErrorMessage=""
                                                            ControlToValidate="txtFechaTransporteHasta">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnCliente">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblCliente" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanellstCliente" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCliente" runat="server" CssClass="TextDesable" Width="350" ReadOnly="True"
                                                            TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvTxtCliente" runat="server" ErrorMessage="" ControlToValidate="txtCliente">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelBtnCliente" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <pro:Botao ID="btnCliente" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Localizar" Titulo="024_btnCliente">
                                                        </pro:Botao>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lstGrupoCliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="chkAgrupadoGrupoCliente" EventName="CheckedChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnSubCliente">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblSubCliente" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelSubCliente" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtSubCliente" runat="server" CssClass="TextDesable" Width="350"
                                                            ReadOnly="True" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvTxtSubCliente" runat="server" ErrorMessage="" ControlToValidate="txtSubCliente">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelBtnSubCliente" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <pro:Botao ID="btnSubCliente" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Localizar" Titulo="024_btnSubCliente">
                                                        </pro:Botao>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lstGrupoCliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="chkAgrupadoGrupoCliente" EventName="CheckedChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnPuntoServicio">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblPuntoServicio" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelPuntoServicio" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtPuntoServicio" runat="server" CssClass="TextDesable" Width="350"
                                                            ReadOnly="True" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvTxtPuntoServicio" runat="server" ErrorMessage="" ControlToValidate="txtPuntoServicio">*</asp:CustomValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelBtnPtoServicio" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <pro:Botao ID="btnPuntoServicio" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Localizar" Titulo="024_btnPuntoServicio">
                                                        </pro:Botao>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="lstGrupoCliente" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="chkAgrupadoGrupoCliente" EventName="CheckedChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnGrupoCliente">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblGrupCliente" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanellstGrupoCliente" runat="server">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstGrupoCliente" runat="server" Width="245px" AutoPostBack="True"
                                                SelectionMode="Multiple" CssClass="Text01"></asp:ListBox>
                                            <asp:CustomValidator ID="csvGrupoCliente" runat="server" ErrorMessage="" ControlToValidate="lstGrupoCliente">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnCanal">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left" runat="server" id="CollblCanal">
                                    <asp:Label ID="lblCanal" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td runat="server" id="CollstCanal">
                                    <asp:UpdatePanel ID="UpdatePanellstCanal" runat="server">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstCanal" runat="server" Width="245px" AutoPostBack="True" SelectionMode="Multiple"
                                                CssClass="Text01"></asp:ListBox>
                                            <asp:CustomValidator ID="csvCanal" runat="server" ErrorMessage="" ControlToValidate="lstCanal">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="right" runat="server" id="CollblSubCanal">
                                    <asp:Label ID="lblSubCanal" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td runat="server" id="CollstSubCanal">
                                    <asp:UpdatePanel ID="UpdatePantelSubCanal" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstSubCanal" runat="server" Width="245px" SelectionMode="Multiple"
                                                CssClass="Text01"></asp:ListBox>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lstCanal" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:CustomValidator ID="csvSubCanal" runat="server" ErrorMessage="" ControlToValidate="lstSubCanal">*</asp:CustomValidator>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnDivisaPuesto">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left" runat="server" id="CollblDivisa">
                                    <asp:Label ID="lblDivisa" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td runat="server" id="CollstDivisa">
                                    <asp:UpdatePanel ID="UpdatePanellstDivisa" runat="server">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstDivisa" runat="server" Width="245px" SelectionMode="Multiple"
                                                CssClass="Text01"></asp:ListBox>
                                            <asp:CustomValidator ID="csvDivisa" runat="server" ErrorMessage="" ControlToValidate="lstDivisa">*</asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="right" runat="server" id="CollblPuesto">
                                    <asp:Label ID="lblPuesto" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td runat="server" id="CollstPuesto">
                                    <asp:UpdatePanel ID="UpatePanelPuesto" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstPuesto" runat="server" Width="245px" SelectionMode="Multiple"
                                                CssClass="Text01"></asp:ListBox>
                                            <asp:CustomValidator ID="csvPuesto" runat="server" ErrorMessage="" ControlToValidate="lstPuesto">*</asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDelegacion" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnContador">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblContador" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdantePanelContador" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:ListBox ID="lstContador" runat="server" Width="245px" SelectionMode="Multiple"
                                                CssClass="Text01"></asp:ListBox>
                                            <asp:CustomValidator ID="csvContador" runat="server" ErrorMessage="" ControlToValidate="lstContador">*</asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDelegacion" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblRuta" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRuta" runat="server" MaxLength="100" Width="235px" CssClass="Text01"></asp:TextBox>
                                            <asp:CustomValidator ID="csvRuta" runat="server" ErrorMessage="" ControlToValidate="txtRuta">*</asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2">
                                    <pro:Botao ID="btnRuta" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                        Tipo="Localizar" Titulo="btnBuscar" Visible="false">
                                    </pro:Botao>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnAgrupadoCliente" visible="false">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelChkCliente" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkAgrupadocliente" runat="server" AutoPostBack="True" Checked="True" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkAgrupadoGrupoCliente" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lstGrupoCliente" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnAgrupadoSubCliente" visible="false">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelChkSubCliente" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkAgrupadoSubCliente" runat="server" AutoPostBack="True" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkAgrupadoGrupoCliente" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lstGrupoCliente" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkAgrupadocliente" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnAgrupadoPServicio" visible="false">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelChkPuntoServicio" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkAgrupadoPuntoServicio" runat="server" AutoPostBack="True" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkAgrupadoSubCliente" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkAgrupadoGrupoCliente" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="lstGrupoCliente" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkAgrupadocliente" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor" runat="server" id="lnAgrupadoGrupoCliente" visible="false">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="right">
                                    &nbsp;
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkAgrupadoGrupoCliente" runat="server" AutoPostBack="True" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr class="TrSinCor">
                                <td class="espaco_inicial" align="right">
                                    <img alt="" src="imagenes/MarcadorCampo.gif" />
                                </td>
                                <td align="left">
                                    <asp:Label ID="lblFormatoSalida" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:UpdatePanel ID="UpdatePanelFormatoSalida" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:RadioButtonList ID="rblFormatoSalida" runat="server" RepeatDirection="Horizontal"
                                                CssClass="Text01">
                                            </asp:RadioButtonList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:UpdatePanel ID="UpdatePanelBtnsGrid" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <table class="tabela_campos" cellspacing="0" cellpadding="3">
                                                <tr>
                                                    <td align="center">
                                                        <pro:Botao ID="btnGenerarInforme" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Confirmar" Titulo="024_btnGenerarInforme">
                                                        </pro:Botao>
                                                    </td>
                                                    <td align="center">
                                                        <pro:Botao ID="btnLimpiar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Limpar" Titulo="btnLimpiar">
                                                        </pro:Botao>
                                                    </td>
                                                    <td align="center">
                                                        <pro:Botao ID="btnGrabar" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Salvar" Titulo="btnGrabar">
                                                        </pro:Botao>
                                                    </td>
                                                    <td align="center">
                                                        <pro:Botao ID="btnVolver" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                                            Tipo="Sair" Titulo="btnVolver">
                                                        </pro:Botao>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgressPanelBtnsGrid" runat="server" AssociatedUpdatePanelID="UpdatePanelBtnsGrid">
                                        <ProgressTemplate>
                                            <div id="divLoadingReporte" class="AlertLoading">
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGenerarInforme" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlReportes" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
