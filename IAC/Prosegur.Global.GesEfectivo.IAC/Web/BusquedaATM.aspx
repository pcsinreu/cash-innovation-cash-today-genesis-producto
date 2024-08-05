<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BusquedaATM.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaATM"
    EnableEventValidation="false" MasterPageFile="~/Master/Master.Master" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Helper.ascx" TagName="Helper" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Busqueda de Divisas</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ocultarExibir() {
            $("#DivFiltros").toggleClass("legend-collapse");
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

        function ModificarMorfologia(OidMorfologia, IDHiddenOid, IDHiddenAcao, PodeAlterar, IDGrid) {

            if (PodeAlterar == 1) {
                document.getElementById(IDHiddenAcao).value = 'Modificar';
                document.getElementById(IDHiddenOid).value = OidMorfologia;
            }

        }

        function BorrarMorfologia(OidMorfologia, IDHiddenOid, IDHiddenAcao, MsgConfirmacao, PodeAlterar, IDGrid, MsgModificar) {

            if (PodeAlterar == 0) { return false; }

            // verifica se alguma morfologia está sendo editada
            if (document.getElementById(IDHiddenAcao).value == 'Modificar') {

                alert(MsgModificar);

            }
            else {

                if (confirm(MsgConfirmacao)) {
                    document.getElementById(IDHiddenAcao).value = 'Borrar';
                    document.getElementById(IDHiddenOid).value = OidMorfologia;
                    return true;
                }
                else { return false; }

            }

        }

        function BorrarProceso(Oid, IDHiddenOidProc, IDHiddenAcaoProc, MsgConfirmacao) {

            if (confirm(MsgConfirmacao)) {

                document.getElementById(IDHiddenAcaoProc).value = 'Borrar';
                document.getElementById(IDHiddenOidProc).value = Oid;

                return true;

            }
            else {

                return false;

            }

        }

        function ExibirTerminos(Oid, IDHiddenOid, IDHiddenAcao, IDHiddenTemMorf, Msg) {

            // valida si ya existe al menos una morfología vinculada al ATM
            if (document.getElementById(IDHiddenTemMorf).value == '') {

                // si no existir: el sistema genera el mensaje MSG003. 
                alert(Msg);
                document.getElementById(IDHiddenOid).value = '';
                document.getElementById(IDHiddenAcao).value = '';
                return false;
            }
            else {

                // Caso contrario: accederá la pantalla “Mantenimiento de Términos de Medios de  Pago”.
                document.getElementById(IDHiddenOid).value = Oid;
                document.getElementById(IDHiddenAcao).value = 'Terminos';
                return true;

            }

        }

        // Seleciona morfologia vigente
        function SelecionarLinhaGrid(IDGrid, IDHiddenVigente) {

            var grid = document.getElementById(IDGrid);

            // obtém oid da morfologia vigente
            var OidCajeroXMorfologia = document.getElementById(IDHiddenVigente).value;

            alert(OidCajeroXMorfologia);

            //            onGridViewRowSelected_ctl00_ContentPlaceHolder1_pgvMorfologias(OidCajeroXMorfologia, grid, 'GridLinhaAlternada');
            NovaCorFundo_ctl00_ContentPlaceHolder1_pgvMorfologias(grid);

            //            status_registro(true);

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:HiddenField runat="server" ID="hiddenCodigo" />
        <asp:HiddenField runat="server" ID="hiddenClienteMultiplo" />
        <asp:UpdatePanel ID="UpdatePanelGeral" runat="server" ChildrenAsTriggers="False"
            UpdateMode="Conditional">
            <ContentTemplate>
                <div id="Filtros">
                    <fieldset id="ExpandCollapseDiv" class="ui-fieldset ui-widget ui-widget-content ui-corner-all ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default ui-state-active">
                            <div id="DivFiltros" class="legend-expand" onclick="ocultarExibir();">
                                <asp:Label ID="lblSubTitulosCriteriosBusqueda" CssClass="legent-text" runat="server">Filtrar Clientes</asp:Label>
                            </div>
                        </legend>
                        <div class="accordion" style="display: none;">
                            <asp:UpdatePanel ID="updUcClienteUc" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                    <asp:PlaceHolder runat="server" ID="phCliente"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <table class="tabela_campos">
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblCodigoATM" runat="server" CssClass="label2" nowrap="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCodigo" runat="server" MaxLength="20" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblRed" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                                <asp:DropDownList ID="ddlRed" runat="server" Width="190px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblModelo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>

                                                <asp:DropDownList ID="ddlModelo" runat="server" Width="190px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                </asp:DropDownList>
       
                                    </td>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblGrupo" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td>
                                                <asp:DropDownList ID="ddlGrupo" runat="server" Width="190px" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all">
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tamanho_celula">
                                        <asp:Label ID="lblVigente" runat="server" CssClass="label2"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked />
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button runat="server" ID="btnBuscar" CssClass="ui-button" Width="100px" ClientIDMode="Static"/>
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
                    <asp:Label ID="lblSubTitulosDivisas" CssClass="ui-panel-title" runat="server"></asp:Label>
                </div>
                <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvResultado" runat="server" AllowPaging="True" AllowSorting="True"
                                        ColunasSelecao="OidATM,OidGrupo" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="True"
                                        ExibirCabecalhoQuandoVazio="false" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="99%">
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
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px">            
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            
                                        </TextBox>
                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle HorizontalAlign="Center" Width="130"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/edit.png" ID="imgEditar" CssClass="imgButton" OnClick="imgEditar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/buscar.png" ID="imgConsultar" CssClass="imgButton" OnClick="imgConsultar_OnClick" />
                                                    <asp:ImageButton runat="server" ImageUrl="App_Themes/Padrao/css/img/button/borrar.png" ID="imgExcluir" CssClass="imgButton" OnClick="imgExcluir_OnClick" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DesClienteSubcliPtoServ" HeaderText="DesClienteSubcliPtoServ"
                                                SortExpression="DesClienteSubcliPtoServ" />
                                            <asp:BoundField DataField="CodigoATM" HeaderText="CodigoATM" SortExpression="CodigoATM" />
                                            <asp:BoundField DataField="DesRed" HeaderText="DesRed" SortExpression="DesRed" />
                                            <asp:BoundField DataField="DesModelo" HeaderText="DesModelo" SortExpression="DesModelo" />
                                            <asp:ButtonField DataTextField="DesGrupo" CommandName="MantenerGrupo" ButtonType="Link"
                                                SortExpression="DesGrupo" />
                                            <asp:BoundField DataField="DesMorfologia" HeaderText="DesMorfologia" SortExpression="DesMorfologia" />
                                            <asp:TemplateField HeaderText="vigente" SortExpression="BolVigente">
                                                <ItemStyle HorizontalAlign="Center" Width="100"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Image ID="imgVigente" runat="server" />
                                                    <asp:HiddenField ID="hidGrupo" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                                <td style="border-width: 0;">
                                                   
                                                </td>
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
                <asp:AsyncPostBackTrigger ControlID="btnLimpar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div style="margin-top: 20px;">
            <asp:UpdatePanel runat="server" ID="updForm" UpdateMode="Conditional" ChildrenAsTriggers="False">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnForm" Visible="true">
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblTituloATM" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:Panel runat="server" ID="pnClienteForm" Enabled="True">
                            <asp:UpdatePanel ID="updUcClienteForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                <ContentTemplate>
                                    <asp:PlaceHolder runat="server" ID="phClienteForm"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <table class="tabela_campos">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblGrupoForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" style="margin: 0px !important;">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlGrupoForm" runat="server" Width="190px" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnAddGrupo" CssClass="ui-button" Width="30" Text="+"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblCodigo" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoForm" runat="server" MaxLength="20" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"></asp:TextBox>
                                            <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigoForm"
                                                Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblRedForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanelRedes" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlRedes" runat="server" Width="190px" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvRedesObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlRedes"
                                                Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblModeloForm" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlModeloForm" runat="server" Width="190px" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvModeloObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlModeloForm"
                                                Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblRegistroTira" runat="server" Visible="false" CssClass="label2"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanelRegistroTira" runat="server">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="chkRegistroTira" runat="server" Visible="false" AutoPostBack="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <div class="ui-panel-titlebar">
                            <asp:Label ID="lblSubTituloMorfologias" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <table class="tabela_campos" style="width: 66% !important">
                            <tr>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblDesMorfologia" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="updMorfologia" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlMorfologia" runat="server" Width="190px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvMorfologiaObrigatorio" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlMorfologia" Text="*" Display="Dynamic"></asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnAddMorfologia" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="hidOidSelecionado" EventName="ValueChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="hidAcaoMorfologia" EventName="ValueChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlMorfologia" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlGrupoForm" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="tamanho_celula">
                                    <asp:Label ID="lblFecha" runat="server" CssClass="label2"></asp:Label>
                                </td>
                                <td>
                                    <table style="margin: 0px !important;">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="updTxtFecha" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFecha" runat="server" MaxLength="10" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" Width="140" AutoPostBack="false"></asp:TextBox>
                                                        <asp:CustomValidator ID="csvFechaObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtFecha"
                                                            Text="*" Display="Dynamic"></asp:CustomValidator>
                                                        <asp:CustomValidator ID="csvFechaInvalida" runat="server" ErrorMessage="" ControlToValidate="txtFecha"
                                                            Text="*" Display="Dynamic"></asp:CustomValidator>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAddMorfologia" EventName="Click" />
                                                        <asp:AsyncPostBackTrigger ControlID="hidOidSelecionado" EventName="ValueChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="hidAcaoMorfologia" EventName="ValueChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="txtFecha" EventName="TextChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoForm" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updBtnAddMorfologia" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnAddMorfologia" CssClass="ui-button" Width="130" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlGrupoForm" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
                        </table>
                        <div style="margin-top: 15px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvMorfologias" runat="server" AllowPaging="True" AllowSorting="False"
                                        GridPadrao="False" EstiloDestaque="GridLinhaDestaque" PageSize="10" AutoGenerateColumns="False"
                                        Ajax="True" Width="99%" GerenciarControleManualmente="false" ExibirCabecalhoQuandoVazio="false"
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
                                        <HeaderStyle Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="GridLinhaPadraoPar" />
                                        <RowStyle CssClass="GridLinhaPadraoImpar" />
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbModificar" ImageUrl="Imagenes/pencil.PNG" runat="server"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbBorrar" ImageUrl="Imagenes/cross.PNG" runat="server"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CodDescMorfologia" HeaderText="" SortExpression="" />
                                            <asp:BoundField DataField="FecInicio" HeaderText="" SortExpression="" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="" HeaderText="" SortExpression="" />
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <asp:HiddenField ID="hidOidSelecionado" runat="server" />
                                    <asp:HiddenField ID="hidAcaoMorfologia" runat="server" />
                                    <asp:HiddenField ID="HidTemMorfologia" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddMorfologia" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupoForm" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="ui-panel-titlebar" style="margin-top: 20px !Important;">
                            <asp:Label ID="lblSubtituloProcesos" CssClass="ui-panel-title" runat="server"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tabela_campos">
                                    <tr>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblProceso" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtProceso" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory" AutoPostBack="true"
                                                        Width="272px"></asp:TextBox>
                                                    <asp:CustomValidator ID="csvProcesoObligatorio" runat="server" ErrorMessage="" ControlToValidate="txtProceso"
                                                        Text="*"></asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblProduto" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlProduto" runat="server" Width="190px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="csvProdutoObligatorio" runat="server" ErrorMessage="" ControlToValidate="ddlProduto"
                                                        Text="*"></asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblCanal" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td >
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCanal" runat="server" Width="190px" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="csvCanalObligatorio" runat="server" ErrorMessage="" ControlToValidate="ddlCanal"
                                                        Text="*"></asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblSubcanal" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="updSubCanal" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlSubcanal" runat="server" Width="190px" AutoPostBack="False" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="csvSubcanalObligatorio" runat="server" ErrorMessage="" ControlToValidate="ddlSubcanal"
                                                        Text="*"></asp:CustomValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCanal" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblModalidad" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="updModalidad" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlModalidad" runat="server" Width="300px" AutoPostBack="true" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="csvModalidadObligatorio" runat="server" ErrorMessage=""
                                                        ControlToValidate="ddlModalidad" Text="*"></asp:CustomValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblInfAdicional" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:UpdatePanel ID="UpdatePanelInfAdicional" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlInfAdicional" runat="server" Width="190px" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModalidad" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    </table>
                                <asp:Panel runat="server" ID="pnUcClienteFat" Enabled="True">
                                    <asp:UpdatePanel ID="updUcClienteFat" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
                                        <ContentTemplate>
                                            <asp:PlaceHolder runat="server" ID="phClienteFat"></asp:PlaceHolder>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                                  <table class="tabela_campos" style="margin-top: -5px;">
                                    <tr>
                                        <td class="tamanho_celula">
                                            <asp:Label ID="lblModoContagem" runat="server" CssClass="label2"></asp:Label>
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" style="margin: 0px !important;">
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
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkContarTarjetasTotales" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button runat="server" ID="btnAddProceso" CssClass="ui-button" Width="130"/>
                                        </td>
                                    </tr>
                                </table>
                                <div style="margin-top: 5px;"></div>
                                <asp:UpdatePanel ID="UpdatePanelProcesos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvProcesos" runat="server" AllowPaging="True" AllowSorting="False"
                                        ColunasSelecao="OidProceso" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="false"
                                        ExibirCabecalhoQuandoVazio="false" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="99%" >
                                        <Pager ID="Pager1">
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
                                        <TextBox ID="TextBox1" AutoPostBack="True" MaxLength="10" Width="30px">            
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            
                                        </TextBox>
                                        <Columns>  
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbBorrar" ImageUrl="App_Themes/Padrao/css/img/grid/borrar.png" CssClass="imgButton" runat="server"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbTerminos" ImageUrl="Imagenes/BtnGrafico_old.gif" CssClass="imgButton" runat="server">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DesProceso" SortExpression="" />
                                            <asp:BoundField DataField="DesProducto" SortExpression="" />
                                            <asp:BoundField DataField="DesCanal" SortExpression="" />
                                            <asp:BoundField DataField="DesSubCanal" SortExpression="" />
                                            <asp:BoundField DataField="DesModalidad" SortExpression="" />
                                            <asp:BoundField DataField="DesIAC" SortExpression="" />
                                            <asp:BoundField DataField="CodDesClienteFacturacion" SortExpression="" />
                                            <asp:BoundField DataField="DesModoContage" SortExpression="" />
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <asp:HiddenField ID="hidOidProcesoSelecionado" runat="server" />
                                    <asp:HiddenField ID="hidAcaoProceso" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddProceso" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="GdvProcesos" EventName="RowCreated" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlGrupoForm" EventName="SelectedIndexChanged" />
                            </Triggers>
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
                         <asp:Button runat="server" ID="btnBaja" CssClass="btn-excluir"/>
                        <asp:Button runat="server" ID="btnConsomeCodigoAjeno" CssClass="btn-excluir"/>
                          <asp:Button runat="server" ID="btnConsomeImporteMaximo" CssClass="btn-excluir"/>
                         <asp:Button runat="server" ID="btnConsomeTerminos" />
                         <asp:Button runat="server" ID="btnAtualizaGrupos" />
                    </div>
                </td>
                <td>
                    <asp:Button runat="server" ID="btnCancelar" CssClass="btn_cancelar"/>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
