<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="BusquedaPlanificacionPopup.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaPlanificacionPopup" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 160px;
        }
        .custom-tabela-campos{
            margin-bottom: 50px;
        }
    </style>
    <script type="text/javascript">
        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=GdvPlanificacion.ClientID %>');
            var hdn = document.getElementById('<%=hdnSelecionado.ClientID %>');

            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
                else {
                    radioBtn[0].checked = true;
                    hdn.value = radioBtn[0].id;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="hdnSelecionado" />
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulo" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>

                <div style="position: relative;">
                    <div style="float: left; width: 450px; height: 30px;" class="helper-div-margins">
                        <asp:UpdatePanel ID="upBanco" runat="server" style="width: 450px; height: 30px;">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phBanco" runat="server"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="float: left;" class="helper-div-margins">
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblNombre" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDesPlanificacion" runat="server" MaxLength="100" Width="290px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <table class="tabela_interna" style="margin-top: 5px;">
                    <tr>
                        <td align="center">
                            <pro:ProsegurGridView ID="GdvPlanificacion" runat="server" AllowPaging="True" AllowSorting="True"
                                ColunasSelecao="oidPlanificacion" EstiloDestaque="GridLinhaDestaque"
                                GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                ExibirCabecalhoQuandoVazio="False" NumeroRegistros="0" OrdenacaoAutomatica="True" PageSize="7"
                                PaginaAtual="0" PaginacaoAutomatica="True" Width="99%"
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
                                <RowStyle CssClass="GridLinhaPadraoImpar" Height="25px" />
                                <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">            
                                </TextBox>
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle HorizontalAlign="Center" Width="30"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:RadioButton runat="server" ID="rdbSelecionado" GroupName="Programacion" onclick="checkRadioBtn(this);" />
                                            <asp:Label runat="server" ID="lbloidPlanificacion" Visible="false" Text='<%#Eval("oidPlanificacion")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="desPlanificacion" HeaderText="Nombre" SortExpression="desPlanificacion" />
                                    <asp:TemplateField HeaderText="Banco" SortExpression="codBanco">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBanco" Text='<%# Eval("codBanco") + " " + Eval("desBanco") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FyhLunes" HeaderText="L" SortExpression="FyhLunes" />
                                    <asp:BoundField DataField="FyhMartes" HeaderText="M" SortExpression="FyhMartes" />
                                    <asp:BoundField DataField="FyhMiercoles" HeaderText="X" SortExpression="FyhMiercoles" />
                                    <asp:BoundField DataField="FyhJueves" HeaderText="J" SortExpression="FyhJueves" />
                                    <asp:BoundField DataField="FyhViernes" HeaderText="V" SortExpression="FyhViernes" />
                                    <asp:BoundField DataField="FyhSabado" HeaderText="S" SortExpression="FyhSabado" />
                                    <asp:BoundField DataField="FyhDomingo" HeaderText="D" SortExpression="FyhDomingo" />
                                    <asp:BoundField HeaderText="oidPlanificacion" Visible="False" />
                                </Columns>
                            </pro:ProsegurGridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Panel ID="pnlSemRegistro" runat="server" Visible="false">
                                <table border="1" cellpadding="3" cellspacing="0" class="SemRegistro">
                                    <tr>
                                        <td style="border-width: 0;"></td>
                                        <td style="border-width: 0;">
                                            <asp:Label ID="lblSemRegistro" runat="server" Text="Label" CssClass="label2">Não existem dados a serem exibidos.</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server" >
    <div class="custom-tabela-campos">
        <div style="position: fixed; bottom: 0; width: 100%;">
            <table class="tabela_campos">
                <tr>
                    <td style="text-align: center">
                        <asp:Button runat="server" ID="btnSeleccionar" CssClass="ui-button" Width="100" />
                        <asp:Button runat="server" ID="btnCancelar" CssClass="ui-button" Width="100" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
