<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="BusquedaMAEPopup.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaMAEPopup" %>

<%@ MasterType VirtualPath="~/Master/MasterModal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content runat="server" ContentPlaceHolderID="head">

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <style type="text/css">
        .style1 {
            width: 160px;
        }
    </style>
    <script type="text/javascript">
        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=GdvResultado.ClientID%>');

            for (var i = 1; i < gv.rows.length; i++) {
                var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

                // Check if the id not same
                if (radioBtn[0].id != id.id) {
                    radioBtn[0].checked = false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>

                <div style="margin-top: 15px">
               <%--     <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Always">
                        <ContentTemplate>--%>

                              <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                            <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <table class="tabela_campos">
                    <tr>
                        <td>
                            <table class="tabela_campos" style="margin: 0px !important">
                                <tr>

                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDeviceID" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDeviceID" runat="server" Width="160px" MaxLength="100" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDescripcion" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDescricao" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="163px"
                                            MaxLength="25"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblEstado" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstado" runat="server" Width="160px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                            <asp:ListItem Value="" Selected="True">Todos</asp:ListItem>
                                            <asp:ListItem Value="1">Vigente</asp:ListItem>
                                            <asp:ListItem Value="0">No Vigente</asp:ListItem>
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblMarca" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="160px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblModelo" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlModelo" runat="server" AutoPostBack="False" Enabled="false" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            Width="163px">
                                        </asp:DropDownList>
                                    </td>

                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="margin: 0px !Important">
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="130" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="130" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table class="tabela_interna" style="margin-top: 10px;">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel runat="server"><ContentTemplate>
                            <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                ColunasSelecao="oidMaquina" EstiloDestaque="GridLinhaDestaque"
                                GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                ExibirCabecalhoQuandoVazio="False" NumeroRegistros="0" OrdenacaoAutomatica="True" PageSize="5"
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
                                            <asp:CheckBox runat="server" ID="rdbSelecionado" GroupName="Programacion" />
                                            <asp:Label runat="server" ID="lblOidMaquina" Visible="false" Text='<%#Eval("oidMaquina")%>' />
                                            <asp:Label runat="server" ID="lblOidPunto" Visible="false" Text='<%#Eval("OidPtoServicio")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="cliente" HeaderText="Cliente" SortExpression="cliente" />
                                     <asp:BoundField DataField="subCliente" HeaderText="SubCliente" SortExpression="subCliente" />
                                    <asp:BoundField DataField="ptoServicio" HeaderText="Pto Serv." SortExpression="ptoServicio" />
                                    <asp:BoundField DataField="deviceID" HeaderText="Codigo" SortExpression="deviceID" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descricao" SortExpression="descripcion" />                                                           
                                    <asp:BoundField DataField="desFabricante" HeaderText="Marca" SortExpression="desFabricante" />
                                    <asp:BoundField DataField="desModelo" HeaderText="Modelo" SortExpression="desModelo" />     
                                    <asp:TemplateField HeaderText="Vigente">
                                        <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Image ID="imgVigente" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                      
                                    <asp:BoundField HeaderText="oidMaquina" Visible="False" />
                                   
                                </Columns>
                            </pro:ProsegurGridView>
                            </ContentTemplate></asp:UpdatePanel>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
    <div style="position: absolute; bottom: 0; width: 100%;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
            <ContentTemplate>
        <table class="tabela_campos">
            <tr>
                <td style="text-align: center">
                    
                    <asp:Button runat="server" ID="btnSeleccionar" CssClass="ui-button" Width="150" Enabled="false" />
                    <asp:Button runat="server" ID="btnAceptar" CssClass="ui-button" Width="100" Enabled="false" />
                    <asp:Button runat="server" ID="btnCancelar" CssClass="ui-button" Width="100" />
                     <div class="botaoOcultar">
                         <asp:Button runat="server" ID="btnConsomeSelecionar"  />
                         </div>
                </td>
            </tr>
        </table>
                </ContentTemplate>
            </asp:UpdatePanel>
    </div>
</asp:Content>
