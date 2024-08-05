<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaDelegaciones.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaDelegaciones" EnableEventValidation="false"
    MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda de Delegaciones</title>

    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-expand");
            $(".accordion").slideToggle("fast");
        };
        function ManterFiltroAberto() {
            $("#DivFiltros").addClass("legend-expand");
            $(".accordion").show();
        };
    </script>
    <script type="text/javascript" language="javascript">
        function ApenasData(a) {
            Aux = "|ZXCVBNM,<.>;:/?ASDFGHJKLÇ^~}]QWERTYUIOP`´{['!@#$%¨&*()_-+=ªº";
            myVal = a.value.toUpperCase();
            var keycode = (window.event) ? event.keyCode : evt.which;
            if ((keycode < 37 || keycode > 40) && (keycode != 8 && keycode != 46)) {
                for (i = 0; i <= Aux.length; i += 1) {
                    myVal = myVal.replace(Aux.charAt(i), '');
                    if (myVal.indexOf(Aux.charAt(i)) >= 0) {
                        myVal = myVal.replace(Aux.charAt(i), '');
                    }
                }

                if (myVal.length > 8) { MAX = 8; } else { MAX = myVal.length; }
                Resp = "";
                for (i = 0; i < MAX; i = i + 1) {
                    Resp += myVal.charAt(i);
                    if (i == 1 || i == 3) { Resp += "/"; }
                }
                a.value = Resp;
            }
        }

        var evento = window.Event ? true : false;

        // -----------------------------------------------------------------------------
        // <summary>
        // Para permitir a digitação apenas de números no campo
        // </summary>
        // <param name=""evt"">Recupera o evento da pagina</param>
        // <history>
        // 	[rafael.gans]	30/03/2006	Created
        // </history>
        // -----------------------------------------------------------------------------
        function ValorNumerico(evt) {
            var tecla = evento ? evt.which : evt.keyCode;
            return (tecla <= 13 || (tecla >= 48 && tecla <= 57));
        }

        // -----------------------------------------------------------------------------
        // <summary>
        // Para permitir a digitação apenas de números no campo
        // </summary>
        // <param name=""evt"">Recupera o evento da pagina</param>
        // <history>
        // 	[rafael.gans]	30/03/2006	Created
        // </history>
        // -----------------------------------------------------------------------------
        function ValorNumericoGmt(evt) {
            var tecla = evento ? evt.which : evt.keyCode;
            return (tecla <= 13 || (tecla >= 48 && tecla <= 57) || tecla == 45);
        }


        // -----------------------------------------------------------------------------
        // <summary>
        // Retorna o maior dia do mes/ano informado
        // </summary>
        // <param name="month">Mês a ser analisado</param>
        // <param name="year">Ano a ser analisado</param>
        // <history>
        // 	[rafael.gans]	09/05/2006	Created
        // </history>
        // -----------------------------------------------------------------------------
        function MaiorDiaMes(month, year) {
            monthdays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            if (month != 1)
                return monthdays[month];
            else
                return ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0 ? 29 : 28);
        }

        // -----------------------------------------------------------------------------
        // <summary>
        // Verifica se um valor é numérico
        // </summary>
        // <param name="valor">Valor a ser analisado</param>
        // <history>
        // 	[rafael.gans]	09/05/2006	Created
        // </history>
        // -----------------------------------------------------------------------------
        function IsNumeric(valor) {
            var num = parseInt(valor, 10);

            if ((num == valor) || (('0' + num) == valor)) return true;
            else return false;
        }

        // -----------------------------------------------------------------------------
        // <summary>
        // Formata a data no formato informado
        // </summary>
        // <param name="campo">Campo onde é digitado a data</param>
        // <param name="evt">Evento do campo</param>
        // <param name="formato">Formato da data (Atualmente só suporta dd/mm/yyyy ou mm/dd/yyyy)</param>
        // <param name="sep">Separador usado na data</param>
        // <history>
        // 	[rafael.gans]	09/05/2006	Created
        // </history>
        // -----------------------------------------------------------------------------
        function formatarData(campo, evt, formato, sep) {
            var tecla = evento ? evt.which : evt.keyCode;
            var valor = parseInt(tecla) - 48;
            var tam = campo.value.length + 1;
            var eDia = formato.substr(tam - 1, 1) == 'd' ? true : false
            var error = true;

            try {
                //Se é valor numérico
                if (ValorNumerico(evt)) {

                    //Para não aceitar 00
                    if (tam == 2 || tam == 5)
                        if (campo.value.substr(tam - 2, 1) == 0 && valor == 0) error = false;

                    if (eDia) {
                        //Se é o primeiro digito do dia
                        if (tam == 1 || tam == 4) {
                            if (valor > 3) error = false;
                        } else if (tam == 2 || tam == 5) {
                            if (campo.value.substr(tam - 2, 1) == 3 && valor > 1) error = false;
                        }
                    } else {
                        //Se é o primeiro digito do mês
                        if (tam == 1 || tam == 4) {
                            if (valor > 1) error = false;
                        } else if (tam == 2 || tam == 5) {
                            if (campo.value.substr(tam - 2, 1) == 1 && valor > 2) error = false;
                        }
                    }
                } else { error = false; }

                //Se passou em todas validações e é a posição 2 ou 5 adiciona o separador
                if (error) {
                    if (tam == 2 || tam == 5) {
                        campo.value += String(valor) + String(sep);
                        error = false;
                    }
                }
            } catch (ex) { error = false; }

            return error;
        }

        // -----------------------------------------------------------------------------
        // <summary>
        // Valida se a data informada é válida para o formato informado
        // </summary>
        // <param name="campo">Campo onde é digitado a data</param>
        // <param name="formato">Formato da data (Atualmente só suporta dd/mm/yyyy ou mm/dd/yyyy)</param>
        // <param name="msgdatainvalida">Mensagem a ser exibida quando a data for inválida</param>
        // <history>
        // 	[rafael.gans]	09/05/2006	Created
        // </history>
        // -----------------------------------------------------------------------------
        function validarData(campo, formato, msgdatainvalida) {
            var dia, mes, ano;
            var vr = campo.value;

            if (vr == '') return true;

            try {
                // Recupera o dia, mês e ano para testarmos
                dia = vr.substr(formato.indexOf('d'), 2);
                mes = vr.substr(formato.indexOf('M'), 2);
                ano = vr.substr(formato.indexOf('y'), 4);

                var error = false;

                // Verifica se o dia, mês e ano são números
                // Depois testa se o ano é válido (entre 1850 e 3000), se o mês é válido e se o dia
                // existe no mês/ano informado
                if (IsNumeric(dia) == false) error = true;
                else if (IsNumeric(mes) == false) error = true;
                else if (IsNumeric(ano) == false) error = true;
                else if ((ano < 1850) || (ano > 3000)) error = true;
                else if (parseInt(mes, 10) < 1 || parseInt(mes, 10) > 12) error = true;
                else if (parseInt(dia, 10) == 0 || parseInt(dia, 10) > MaiorDiaMes(parseInt(mes, 10) - 1, parseInt(ano, 10))) error = true;

            } catch (e) { error = true; }

            // Caso tenha ocorrido erro, exibe mensagem e volta o foco para o campo
            if (error == true) {
                alert(msgdatainvalida);
                campo.value = '';
                campo.focus();
                return false
            }
            else return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:HiddenField runat="server" ID="hiddenCodigo" />
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
            <ContentTemplate>
                <div id="Filtros" style="display: block;">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-collapse" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: block;">
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodDelegaciones" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodDelegaciones" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            MaxLength="20" Width="198px"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblDesDelegaciones" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDesDelegaciones" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
                                            MaxLength="50" Width="260px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoPais" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPais" runat="server" Width="208px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                        </asp:DropDownList>
                                        <asp:CustomValidator ID="csvPais" runat="server" ControlToValidate="ddlPais">*</asp:CustomValidator>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked="" />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnLimpar" CssClass="ui-button" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </fieldset>
                </div>
                <div class="ui-panel-titlebar">
                    <asp:Label ID="lblSubTitulosDelegaciones" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="coddelegacion" EstiloDestaque="GridLinhaDestaque"
                                        GridPadrao="False" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        ExibirCabecalhoQuandoVazio="False" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="99%"
                                        AgruparRadioButtonsPeloName="False"
                                        ConfigurarNumeroRegistrosManualmente="False" EnableModelValidation="True"
                                        HeaderSpanStyle="">
                                        <Pager ID="objPager_ProsegurGridView1">
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
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">&nbsp;&nbsp;</TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="coddelegacion" HeaderText="codigo" SortExpression="coddelegacion" />
                                            <asp:BoundField DataField="DesDelegacion" HeaderText="descripcion" SortExpression="DesDelegacion" />
                                            <asp:TemplateField HeaderText="BolVigente">
                                                <ItemStyle HorizontalAlign="Center" Width="150"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OidDelegacion" HeaderText="oid" Visible="false" />
                                        </Columns>
                                    </pro:ProsegurGridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGridSemRegistro" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="False">
                                <ContentTemplate>
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
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="margin-top: 20px;">
            <asp:UpdatePanel runat="server" ID="updForm">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnForm" Visible="False">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloDelegacione" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos" border="0">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblPais" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="1">
                                    <asp:DropDownList ID="ddlPaisForm" runat="server" Width="225px" AutoPostBack="True" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" />
                                    <asp:CustomValidator ID="csvPaisForm" runat="server" ErrorMessage=""
                                        ControlToValidate="ddlPaisForm" Text="*" Display="Dynamic"></asp:CustomValidator>
                                </td>

                                <td class="tamanho_celula">
                                    <asp:Label ID="lblVigenteForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="1">
                                    <asp:CheckBox ID="chkVigenteForm" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoDelegacion" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoDelegacion" runat="server" MaxLength="20" AutoPostBack="False"
                                        CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="140px"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodigoObrigatorio"
                                        runat="server" ErrorMessage="" ControlToValidate="txtCodigoDelegacion" Text="*" Display="Dynamic" />
                                    <asp:CustomValidator ID="csvCodigoExistente" Display="Dynamic"
                                        runat="server" ErrorMessage="" ControlToValidate="txtCodigoDelegacion" Text="*" />
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDescricaoDelegaciones" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtDescricaoDelegaciones" runat="server" Width="225px" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="False"></asp:TextBox>
                                    <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="txtDescricaoDelegaciones" Text="*" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" MaxLength="25"
                                        AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Width="225px" Enabled="False"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDesCodigoAjeno" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <table style="margin: 0px !Important">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDesCodigoAjeno" runat="server" AutoPostBack="False"
                                                    CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" Enabled="False" MaxLength="50" ReadOnly="True"
                                                    Width="225px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnAltaAjeno" CssClass="ui-button" Width="130px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                        </table>
                        <div class="ui-panel-titlebar">

                            <asp:Label ID="lblConfiguracionesRegionales" CssClass="ui-panel-title" runat="server">Configuraciones regionales</asp:Label>
                        </div>
                        <table class="tabela_campos" border="0">

                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblGmtMinutos" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtGmtMinutos" runat="server" AutoPostBack="False"
                                        Width="225px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="5"></asp:TextBox>
                                    <asp:CustomValidator ID="csvGmtMinutosObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="txtGmtMinutos" Text="*" Display="Dynamic" />
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCantidadMinAjuste" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="2">

                                    <asp:TextBox ID="txtCantidadMinAjuste" runat="server" MaxLength="5"
                                        AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="225px"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCantiAjuste" runat="server" ErrorMessage=""
                                        ControlToValidate="txtCantidadMinAjuste" Text="*" Display="Dynamic" />

                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblFechaVeranoInicio" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaVeranoInicio" runat="server" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="10" Width="225px"></asp:TextBox>
                                    <asp:CustomValidator ID="csvFechaVeranoInicio" runat="server" ControlToValidate="txtFechaVeranoInicio" Display="Dynamic" ErrorMessage="" Text="*" />
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblFechaVeranoFim" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtFechaVeranoFim" runat="server" Width="225px"
                                        AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" MaxLength="10"></asp:TextBox>
                                    <asp:CustomValidator ID="csvFechaVeranoFim" runat="server" ErrorMessage=""
                                        ControlToValidate="txtFechaVeranoFim" Text="*" Display="Dynamic" />
                                    <asp:CustomValidator ID="csvFechaVeranoInicioInvalida" runat="server" ErrorMessage=""
                                        ControlToValidate="txtFechaVeranoFim" Text="*" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblZona" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtZona" runat="server" MaxLength="50" AutoPostBack="False" Width="225px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                    <asp:CustomValidator ID="csvZona" runat="server" ErrorMessage=""
                                        ControlToValidate="txtZona" Text="*" Display="Dynamic" />
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblTotasDelegaciones" runat="server" CssClass="label2">Aplicar Configuraciones?</asp:Label>
                                </td>
                                <td colspan="1">
                                    <asp:CheckBox ID="chkTodasDelegaciones" runat="server" AutoPostBack="True" />
                                </td>
                            </tr>
                        </table>

                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblFaturacion" CssClass="ui-panel-title" runat="server">Faturacion</asp:Label>
                        </div>

                        <div id="divAddPtoServ" runat="server">

                            <table>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updUcClientesPtoServ" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                            <ContentTemplate>
                                                <asp:PlaceHolder runat="server" ID="phClientesPtoServ"></asp:PlaceHolder>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnAddPtoServ" CssClass="ui-button" Width="100px" />
                                        <asp:CustomValidator ID="csvAddPtoServ" runat="server"
                                            Display="Dynamic" ErrorMessage="" Text="*"></asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: center;">
                            <dx:ASPxGridView ID="grid" runat="server"
                                KeyFieldName="OID_PTO_SERVICIO" Width="99%">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="BANCO_CAPITAL" VisibleIndex="0" />
                                    <dx:GridViewDataTextColumn FieldName="BANCO_TESORERIA" VisibleIndex="1" />
                                    <dx:GridViewDataTextColumn FieldName="CUENTA_TESORERIA" VisibleIndex="2" />
                                    <dx:GridViewDataColumn VisibleIndex="3" CellStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/iconos/institution.png" ID="imgDatosBancariosForm" CssClass="imgButton" OnCommand="imgDatosBancariosForm_OnClick" CommandArgument='<%# Container.KeyValue %>' />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn VisibleIndex="4" CellStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluirForm" CssClass="imgButton" OnCommand="imgExcluirForm_OnClick" CommandArgument='<%# Container.KeyValue %>' />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                                <SettingsPager PageSize="20">

                                    <PageSizeItemSettings Visible="true" />
                                </SettingsPager>
                            </dx:ASPxGridView>
                        </div>
                        <asp:UpdatePanel ID="upDatosBanc" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phDatosBanc" runat="server"></asp:PlaceHolder>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="cphBotonesRodapie">
    <center>
        <table>
            <tr align="center">
                <td>
                    <asp:Button runat="server" ID="btnNovo" CssClass="btn-novo"/>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnGrabar" CssClass="btn-salvar"/>
                </td>
                <td >
                     <asp:Button runat="server" ID="btnBajaConfirm"  CssClass="btn-excluir"/>
                    <div class="botaoOcultar">
                        
                        <asp:Button runat="server" ID="btnCamposExtras" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnConsomeTotalizador" CssClass="btn-excluir"/>
                         <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnConsomeCodigoAjeno" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnAlertaNao" CssClass="btn-excluir"/>
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
