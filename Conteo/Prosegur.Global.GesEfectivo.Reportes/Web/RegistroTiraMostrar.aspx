<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegistroTiraMostrar.aspx.vb" MasterPageFile="~/Master/Master.Master"
    Inherits="Prosegur.Global.GesEfectivo.Reportes.Web.RegistroTiraMostrar" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Erro.ascx" TagName="Erro" TagPrefix="uc1" %>
<%@ Register Src="Controles/Cabecalho.ascx" TagName="Cabecalho" TagPrefix="uc3" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
     <title></title>
    <style type="text/css">
        .styleGridSpace
        {
            background-color: transparent;
            background: transparent;
            border: none;
            padding-left: 5px;
        }
        .styleGridCellNoBorder
        {
            background-color: transparent;
            background: transparent;
            border: none;
        }
        .styleGridCellConfere
        {
            background-color: #D0E8E8;
            background: #D0E8E8;
        }
        .styleGridCellNoConfere
        {
            background-color: #FF9999;
            background: #FF9999;
        }
        .styleGridCellConfereDec
        {
            background-color: #80FF80;
            background: #80FF80;
        }
        .styleGridCellNoConfereDec
        {
            background-color: #FF3737;
            background: #FF3737;
        }
        .styleGridSpaceHeader
        {
            background-color: transparent;
            background: transparent;
            border: none;
            padding-left: -5px;
        }
        .styleGridSupHeader
        {
           background-image: url(imagenes/grid_titulo_bg.jpg);
            border-color: #d4d0c8;
            border-style: inset;
            border-top: 1px solid #A5A3A0;
            border-left: 1px solid #A5A3A0;
            border-right: 1px solid #A5A3A0;
            border-collapse: collapse;
            display: block;
            text-indent: 3px;
            font-weight: bold;
            padding: 3pt 0pt 0pt 1pt;
        }
    </style>
    <asp:Literal ID="ltr_js" runat="server"></asp:Literal>
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        var gvDecCtlId = '<%=gdvConteudoDeclarados.ClientID %>';
        var gvDecTotCtlId = '<%=gdvConteudoDeclaradosTotales.ClientID %>';
        var gvContCtlId = '<%=gdvConteudoContado.ClientID %>';
        var gvContTotCtlId = '<%=gdvConteudoContadoTot.ClientID %>';

        window.onload = load_2;
        function load_2() {
            var cellwidth1 = getCellWidth(gvDecCtlId, 0, 0) + getCellWidth(gvDecCtlId, 0, 1) + getCellWidth(gvDecCtlId, 0, 2);
            var cellwidth2 = getCellWidth(gvDecCtlId, 0, 3) + getCellWidth(gvDecCtlId, 0, 4) + getCellWidth(gvDecCtlId, 0, 5) + getCellWidth(gvDecCtlId, 0, 6);
            var cellwidth3 = getCellWidth(gvDecCtlId, 0, 7) + getCellWidth(gvDecCtlId, 0, 8) + getCellWidth(gvDecCtlId, 0, 9) + getCellWidth(gvDecCtlId, 0, 10) + getCellWidth(gvDecCtlId, 0, 11);
            var cellwidth4 = getCellWidth(gvDecTotCtlId, 0, 0) + getCellWidth(gvDecTotCtlId, 0, 1) + getCellWidth(gvDecTotCtlId, 0, 2) + getCellWidth(gvDecTotCtlId, 0, 3) + getCellWidth(gvDecTotCtlId, 0, 4) + 1;
            var cellwidth5 = getCellWidth(gvContCtlId, 0, 0) + getCellWidth(gvContCtlId, 0, 1) + getCellWidth(gvContCtlId, 0, 2) + getCellWidth(gvContCtlId, 0, 3);
            var cellwidth6 = getCellWidth(gvContTotCtlId, 0, 0) + getCellWidth(gvContTotCtlId, 0, 1) - 2;
            var coltdCajetines = document.getElementById('<%=tdCajetines.ClientID%>');
            var coltdCantidad = document.getElementById('<%=tdCantidad.ClientID%>');
            var coltdImporte = document.getElementById('<%=tdImporte.ClientID%>');
            var coltdDivisa = document.getElementById('<%=tdDivisa.ClientID%>');
            var coltdCajetinesConteo = document.getElementById('<%=tdCajetinesConteo.ClientID%>');
            var coltdDivisaConteo = document.getElementById('<%=tdDivisaConteo.ClientID%>');

            if (getGridViewControl(gvDecCtlId) != null) { coltdCajetines.width = cellwidth1; } else { coltdCajetines.style.visibility = "hidden"; }
            if (getGridViewControl(gvDecCtlId) != null) { coltdCantidad.width = cellwidth2; } else { coltdCantidad.style.visibility = "hidden"; }
            if (getGridViewControl(gvDecCtlId) != null) { coltdImporte.width = cellwidth3; } else { coltdImporte.style.visibility = "hidden"; }
            if (getGridViewControl(gvDecTotCtlId) != null) { coltdDivisa.width = cellwidth4; } else { coltdDivisa.style.visibility = "hidden"; }
            if (getGridViewControl(gvContCtlId) != null) { coltdCajetinesConteo.width = cellwidth5; } else { coltdCajetinesConteo.style.visibility = "hidden"; }
            if (getGridViewControl(gvContTotCtlId) != null) { coltdDivisaConteo.width = cellwidth6; } else { coltdDivisaConteo.style.visibility = "hidden"; }
        }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <div class="ui-panel-titlebar">
            <asp:Label ID="lblIdentificacionATM" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
            <table  border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" width="1285px">
                <tr>
                    <td align="left">
                        <table class="tabela_campos" cellspacing="0" cellpadding="3">
                            <tr>
                                <td align="left" class="tamanho_celula">
                                    <asp:Label ID="lblCodigoATM" Text="lblCodigoATM" CssClass="Lbl2" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoATM" Width="130px" MaxLength="20" CssClass="Text01" runat="server"
                                        ReadOnly="True" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="tamanho_celula">
                                    <asp:Label ID="lblCodCliente" Text="lblCodCliente" CssClass="Lbl2" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodCliente" Width="400px" MaxLength="15" CssClass="Text01" runat="server"
                                        ReadOnly="True" />
                                </td>
                            </tr>
                            <tr >
                                <td align="left" class="tamanho_celula">
                                    <asp:Label ID="lblCodSubcliente" Text="lblCodSubcliente" CssClass="Lbl2" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodSubcliente" Width="400px" MaxLength="15" CssClass="Text01"
                                        runat="server" ReadOnly="True" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="tamanho_celula">
                                    <asp:Label ID="lblCodPtoServicio" Text="lblCodPtoServicio" CssClass="Lbl2" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodPtoServicio" Width="400px" MaxLength="15" CssClass="Text01"
                                        runat="server" ReadOnly="True" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
            <div class="ui-panel-titlebar">
                <asp:Label ID="lblHabilitaciones" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
        <table  border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" width="1285px">
                <tr>
                    <td align="left">
                        <table>
                            <tr valign="top">
                                <td align="right">
                                    <pro:ProsegurGridView ID="gdvHabilitaciones" runat="server" AllowPaging="False" AllowSorting="False"
                                        ColunasSelecao="Codigo" EstiloDestaque="GridLinhaPadrao" GridPadrao="False" AutoGenerateColumns="False"
                                        Ajax="false" GerenciarControleManualmente="True" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="false" ExibirCabecalhoQuandoVazio="False"
                                        BorderStyle="None">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                       <Columns>
                                            <asp:BoundField DataField="CodRemesaLegado" HeaderText="colCodRemesaLegado" SortExpression="CodRemesaLegado"
                                                HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FyhHabilitacion" HeaderText="colFyhHabilitacion" SortExpression="FyhHabilitacion"
                                                HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodInventarioContenedor" HeaderText="colCodInventarioContenedor"
                                                SortExpression="CodInventarioContenedor" HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodPrecintoContenedor" HeaderText="colCodPrecintoContenedor"
                                                SortExpression="CodPrecintoContenedor" HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CodMorfologiaComponente" HeaderText="colCodMorfologiaComponente"
                                                SortExpression="CodMorfologiaComponente" HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesDivisa" HeaderText="colDesDivisa" SortExpression="DesDivisa"
                                                HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesDenominacion" HeaderText="colCodDenominacion" SortExpression="DesDenominacion"
                                                HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NelCantidadHabilitada" HeaderText="colNelCantidadHabilitada"
                                                SortExpression="NelCantidadHabilitada" HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteHabilitado" HeaderText="colNumImporteHabilitado"
                                                SortExpression="NumImporteHabilitado" HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderStyle CssClass="styleGridSpaceHeader" />
                                                <ItemStyle CssClass="styleGridSpace" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TotalDivisa" HeaderText="colTotalDivisa" SortExpression="NumImporteHabilitado"
                                                HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Divisa" HeaderText="colDivisa" SortExpression="NumImporteHabilitado"
                                                HeaderStyle-Wrap="false">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td align="right" style="padding-top: 5px;">
                                    <pro:ProsegurGridView ID="gdvHabilitacionesTotales" runat="server" AllowPaging="False"
                                        AllowSorting="False" ColunasSelecao="Codigo" EstiloDestaque="GridLinhaDestaque"
                                        GridPadrao="False" AutoGenerateColumns="False" Ajax="false" GerenciarControleManualmente="True"
                                        NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0" PaginacaoAutomatica="false"
                                        ExibirCabecalhoQuandoVazio="False" BorderStyle="None" ShowHeader="False">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodMorfologiaComponente" SortExpression="CodMorfologiaComponente" />
                                            <asp:BoundField DataField="DesDivisa" SortExpression="DesDivisa" />
                                            <asp:BoundField DataField="DesDenominacion" SortExpression="DesDenominacion" />
                                            <asp:BoundField DataField="NelCantidadHabilitada" SortExpression="NelCantidadHabilitada" />
                                            <asp:BoundField DataField="NumImporteHabilitado" SortExpression="NumImporteHabilitado" />
                                            <asp:TemplateField>
                                                <ItemStyle CssClass="styleGridSpace" />
                                                <HeaderStyle CssClass="styleGridSpace" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TotalDivisa" SortExpression="NumImporteHabilitado" HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="Divisa" SortExpression="NumImporteHabilitado" HeaderStyle-Wrap="false" />
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <div style="text-align: center; vertical-align: top">
                                        <asp:Label runat="server" ID="Msg001" Text="Msg001" Visible="false" Font-Size="Large" />
                                    </div>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
             <div class="ui-panel-titlebar">
                <asp:Label ID="lblTiraRegistrada" CssClass="ui-panel-title" runat="server"></asp:Label>
            </div>
        <table  border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF" width="1285px">
                <tr>
                    <td align="left">
                        <table border="0" cellpadding="0" cellspacing="4" style="width: 100%">
                            <tr>
                                <td style="width: 8%">
                                    <asp:Label ID="lblPeriodoContable" runat="server" Text="lblPeriodoContable" Visible="false"/>
                                </td>
                                <td rowspan="2">
                                    <asp:TextBox ID="txtPeriodoContable" runat="server" ReadOnly="true" TextMode="MultiLine" Rows="3" CssClass="Text01" Width="100%" Visible="false"/>
                                </td>
                                <td style="width: 5%; text-align: right">
                                    <asp:Label ID="lblFecha" runat="server" Text="lblFecha" Visible="false"/>
                                </td>
                                <td style="width: 5%">
                                    <asp:TextBox ID="txtFecha" runat="server" ReadOnly="true" CssClass="Text01" Visible="false"/>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <table>
                            <tr align="left" valign="top">
                                <td align="right">
                                    <table cellspacing="0px" style="padding: 1px">
                                        <tr>
                                            <td id="tdDeclaradoDispensadores" runat="server" colspan="3" align="left" style="font-weight: bold">
                                                tdDeclaradoDispensadores
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdCajetines" runat="server" align="center" class="styleGridSupHeader">
                                                tdCajetines
                                            </td>
                                            <td id="tdCantidad" runat="server" align="center" class="styleGridSupHeader">
                                                tdCantidad
                                            </td>
                                            <td id="tdImporte" runat="server" align="center" class="styleGridSupHeader">
                                                tdImporte
                                            </td>
                                        </tr>
                                    </table>
                                    <pro:ProsegurGridView ID="gdvConteudoDeclarados" runat="server" Ajax="false" AllowPaging="False"
                                        AllowSorting="False" AutoGenerateColumns="False" BorderStyle="None" ColunasSelecao="Codigo"
                                        EstiloDestaque="GridLinhaPadrao" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True"
                                        GridPadrao="False" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0"
                                        PaginacaoAutomatica="false">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                          <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                       <Columns>
                                            <asp:BoundField DataField="CodMorfologiaComponente" HeaderStyle-Wrap="false" HeaderText="colCodMorfologiaComponente"
                                                SortExpression="CodMorfologiaComponente">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesDivisa" HeaderStyle-Wrap="false" HeaderText="colDesDivisa"
                                                SortExpression="DesDivisa">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesDenominacion" HeaderStyle-Wrap="false" HeaderText="colDesDenominacion"
                                                SortExpression="DesDenominacion">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NelCantidadIntroducida" HeaderStyle-Wrap="false" HeaderText="colNelCantidadIntroducida"
                                                SortExpression="NelCantidadIntroducida">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NelCantidadDispensada" HeaderStyle-Wrap="false" HeaderText="colNelCantidadDispensada"
                                                SortExpression="NelCantidadDispensada">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NelCantidadRechazada" HeaderStyle-Wrap="false" HeaderText="colNelCantidadRechazada"
                                                SortExpression="NelCantidadRechazada">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NelCantidadRemanente" HeaderStyle-Wrap="false" HeaderText="colNelCantidadRemanente"
                                                SortExpression="NelCantidadRemanente">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteIntroducido" HeaderStyle-Wrap="false" HeaderText="colNumImporteIntroducido"
                                                SortExpression="NumImporteIntroducido">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteDispensado" HeaderStyle-Wrap="false" HeaderText="colNumImporteDispensado"
                                                SortExpression="NumImporteDispensado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteRechazado" HeaderStyle-Wrap="false" HeaderText="colNumImporteRechazado"
                                                SortExpression="NumImporteRechazado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteRemanente" HeaderStyle-Wrap="false" HeaderText="colNumImporteRemanente"
                                                SortExpression="NumImporteRemanente">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="numImporteDeclarado" HeaderStyle-Wrap="false" HeaderText="colnumImporteDeclarado"
                                                SortExpression="numImporteDeclarado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <div style="text-align: center; vertical-align: top">
                                        <asp:Label runat="server" ID="Msg002" Text="Msg002" Visible="false" Font-Size="Large" />
                                    </div>
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    <table cellspacing="0px" style="padding: 1px">
                                        <tr>
                                            <td id="tdConteoDispensadores" runat="server" colspan="3" align="left" style="font-weight: bold">
                                                tdConteoDispensadores
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="tdCajetinesConteo" runat="server" align="center" class="styleGridSupHeader">
                                                tdCajetinesConteo
                                            </td>
                                        </tr>
                                    </table>
                                    <pro:ProsegurGridView ID="gdvConteudoContado" runat="server" Ajax="false" AllowPaging="False"
                                        AllowSorting="False" AutoGenerateColumns="False" BorderStyle="None" ColunasSelecao="Codigo"
                                        EstiloDestaque="GridLinhaPadrao" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True"
                                        GridPadrao="False" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0"
                                        PaginacaoAutomatica="false">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                          <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                      <Columns>
                                            <asp:BoundField DataField="CodRemesaConteo" HeaderStyle-Wrap="false" HeaderText="colCodRemesaConteo"
                                                SortExpression="CodRemesaConteo">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesDivisa" HeaderStyle-Wrap="false" HeaderText="colDesDivisa"
                                                SortExpression="DesDivisa">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesDenominacion" HeaderStyle-Wrap="false" HeaderText="colDesDenominacion"
                                                SortExpression="DesDenominacion">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteContado" HeaderStyle-Wrap="false" HeaderText="colNumImporteContado"
                                                SortExpression="NumImporteContado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <div style="text-align: center; vertical-align: top">
                                        <asp:Label runat="server" ID="Msg003" Text="Msg003" Visible="false" Font-Size="Large" />
                                    </div>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="right" style="padding-top: 5px">
                                    <table cellspacing="0px" style="padding: 1px;">
                                        <tr>
                                            <td id="tdDivisa" runat="server" align="center" class="styleGridSupHeader">
                                                tdDivisa
                                            </td>
                                        </tr>
                                    </table>
                                    <pro:ProsegurGridView ID="gdvConteudoDeclaradosTotales" runat="server" Ajax="false"
                                        AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False" BorderStyle="None"
                                        ColunasSelecao="Codigo" EstiloDestaque="GridLinhaPadrao" ExibirCabecalhoQuandoVazio="False"
                                        GerenciarControleManualmente="True" GridPadrao="False" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="false">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                         <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                       <Columns>
                                            <asp:BoundField DataField="DesDivisa" HeaderStyle-Wrap="false" HeaderText="colDesDivisa"
                                                SortExpression="DesDivisa">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteIntroducido" HeaderStyle-Wrap="false" HeaderText="colNumImporteIntroducido"
                                                SortExpression="NumImporteIntroducido">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteRechazado" HeaderStyle-Wrap="false" HeaderText="colNumImporteRechazado"
                                                SortExpression="NumImporteRechazado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteRemanente" HeaderStyle-Wrap="false" HeaderText="colNumImporteRemanente"
                                                SortExpression="NumImporteRemanente">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="numImporteDeclarado" HeaderStyle-Wrap="false" HeaderText="colnumImporteDeclarado"
                                                SortExpression="numImporteDeclarado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </td>
                                <td>
                                </td>
                                <td align="left" style="padding-top: 6px">
                                    <table cellspacing="0px" style="padding: 0px">
                                        <tr>
                                            <td id="tdDivisaConteo" runat="server" align="center" class="styleGridSupHeader">
                                                tdDivisaConteo
                                            </td>
                                        </tr>
                                    </table>
                                    <pro:ProsegurGridView ID="gdvConteudoContadoTot" runat="server" Ajax="false" AllowPaging="False"
                                        AllowSorting="False" AutoGenerateColumns="False" BorderStyle="None" ColunasSelecao="Codigo"
                                        EstiloDestaque="GridLinhaPadrao" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True"
                                        GridPadrao="False" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0"
                                        PaginacaoAutomatica="false">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoImpar" />
                                        <RowStyle CssClass="GridLinhaPadraoPar" />
                                       <Columns>
                                            <asp:BoundField DataField="DesDivisa" HeaderStyle-Wrap="false" HeaderText="colDesDivisa"
                                                SortExpression="DesDivisa">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteContado" HeaderStyle-Wrap="false" HeaderText="colNumImporteContado"
                                                SortExpression="NumImporteContado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr id="trIngesadorGrid" runat="server">
                    <td align="left" colspan="2">
                        <table>
                            <tr valign="top">
                                <td align="left" >
                                    <table cellspacing="0px" style="padding: 1px;">
                                        <tr>
                                            <td id="tdIngresadoresDec" runat="server" colspan="3" align="left" style="font-weight: bold">
                                                tdIngresadoresDec
                                            </td>
                                        </tr>
                                    </table>
                                    <pro:ProsegurGridView ID="gdvDeclaradosIngresadores" runat="server" Ajax="false"
                                        AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False" BorderStyle="None"
                                        ColunasSelecao="Codigo" EstiloDestaque="GridLinhaPadrao" ExibirCabecalhoQuandoVazio="False"
                                        GerenciarControleManualmente="True" GridPadrao="False" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="false">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                         <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                       <Columns>
                                            <asp:BoundField DataField="DesDivisa" HeaderStyle-Wrap="false" HeaderText="colDesDivisa"
                                                SortExpression="DesDivisa">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesTipoMedioPago" HeaderStyle-Wrap="false" HeaderText="colDesTipoMedioPago"
                                                SortExpression="DesTipoMedioPago">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteIngresado" HeaderStyle-Wrap="false" HeaderText="colNumImporteIngresado"
                                                SortExpression="NumImporteIngresado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <div style="text-align: center; width: 300px; position:relative; top: 30%">
                                        <asp:Label runat="server" ID="Msg006" Text="Msg006" Visible="false" Font-Size="Large" />
                                    </div>
                                </td>
                                <td align="center">
                                </td>
                                <td align="left">
                                    <table cellspacing="0px" style="padding: 1px;">
                                        <tr>
                                            <td id="tdIngresadoresCont" runat="server" colspan="3" align="left" style="font-weight: bold">
                                                tdIngresadoresCont
                                            </td>
                                        </tr>
                                    </table>
                                    <pro:ProsegurGridView ID="gdvContadosIngresadores"  runat="server" Ajax="false" AllowPaging="False"
                                        AllowSorting="False" AutoGenerateColumns="False" BorderStyle="None" ColunasSelecao="Codigo"
                                        EstiloDestaque="GridLinhaPadrao" ExibirCabecalhoQuandoVazio="False" GerenciarControleManualmente="True"
                                        GridPadrao="False" NumeroRegistros="0" OrdenacaoAutomatica="True" PaginaAtual="0"
                                        PaginacaoAutomatica="false">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                      <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                       <Columns>
                                            <asp:BoundField DataField="CodRemesaConteo" HeaderStyle-Wrap="false" HeaderText="colCodRemesaConteo"
                                                SortExpression="CodRemesaConteo">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesDivisa" HeaderStyle-Wrap="false" HeaderText="colDesDivisa"
                                                SortExpression="DesDivisa">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesTipoMedioPago" HeaderStyle-Wrap="false" HeaderText="colDesTipoMedioPago"
                                                SortExpression="DesTipoMedioPago">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteIngresado" HeaderStyle-Wrap="false" HeaderText="colNumImporteIngresado"
                                                SortExpression="NumImporteIngresado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <div style="text-align: center; position:relative; top: 35%">
                                        <asp:Label runat="server" ID="Msg004" Text="Msg004" Visible="false" Font-Size="Large" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr id="trDepositoGrid" runat="server">
                    <td align="left" colspan="2">
                        <table>
                            <tr valign="top">
                                <td align="left">
                                    <table cellspacing="0px" style="padding: 1px;">
                                        <tr>
                                            <td id="tdDepositoDec" runat="server" colspan="3" align="left" style="font-weight: bold">
                                                tdDepositoDec
                                            </td>
                                        </tr>
                                    </table>
                                    <pro:ProsegurGridView ID="gdvDeclaradosDeposito"  runat="server"
                                        Ajax="false" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False"
                                        BorderStyle="None" ColunasSelecao="Codigo" EstiloDestaque="GridLinhaPadrao" ExibirCabecalhoQuandoVazio="False"
                                        GerenciarControleManualmente="True" GridPadrao="False" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="false">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                       <Columns>
                                            <asp:BoundField DataField="DesDivisa" HeaderStyle-Wrap="false" HeaderText="colDesDivisa"
                                                SortExpression="DesDivisa">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesTipoMedioPago" HeaderStyle-Wrap="false" HeaderText="colDesTipoMedioPago"
                                                SortExpression="DesTipoMedioPago">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteIngresado" HeaderStyle-Wrap="false" HeaderText="colNumImporteIngresado"
                                                SortExpression="NumImporteIngresado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <div style="text-align: center; width: 300px; position:relative; top: 30%">
                                        <asp:Label runat="server" ID="Msg007" Text="Msg007" Visible="false" Font-Size="Large" />
                                    </div>
                                </td>
                                <td align="center">
                                </td>
                                <td align="left">
                                    <table cellspacing="0px" style="padding: 1px;">
                                        <tr>
                                            <td id="tdDepositoCont" runat="server" colspan="3" align="left" style="font-weight: bold">
                                                tdDepositoCont
                                            </td>
                                        </tr>
                                    </table>
                                    <pro:ProsegurGridView ID="gdvContadosDeposito" runat="server"
                                        Ajax="false" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False"
                                        BorderStyle="None" ColunasSelecao="Codigo" EstiloDestaque="GridLinhaPadrao" ExibirCabecalhoQuandoVazio="False"
                                        GerenciarControleManualmente="True" GridPadrao="False" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="false">
                                        <HeaderStyle CssClass="GridTitulo" Font-Bold="True" HorizontalAlign="Center" />
                                         <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                       <Columns>
                                            <asp:BoundField DataField="CodRemesaConteo" HeaderStyle-Wrap="false" HeaderText="colCodRemesaConteo"
                                                SortExpression="CodRemesaConteo">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesDivisa" HeaderStyle-Wrap="false" HeaderText="colDesDivisa"
                                                SortExpression="DesDivisa">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesTipoMedioPago" HeaderStyle-Wrap="false" HeaderText="colDesTipoMedioPago"
                                                SortExpression="DesTipoMedioPago">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumImporteIngresado" HeaderStyle-Wrap="false" HeaderText="colNumImporteIngresado"
                                                SortExpression="NumImporteIngresado">
                                                <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <div style="text-align: center; position:relative; top: 35%">
                                        <asp:Label runat="server" ID="Msg005" Text="Msg005" Visible="false" Font-Size="Large" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <caption>
                    </caption>
                </tr>
            </table>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    
</asp:Content>