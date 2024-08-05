<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MantenimientoATM.aspx.vb"
    Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoATM" MasterPageFile="~/Principal.Master" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>
<%@ Register Src="Controles/Helper.ascx" TagName="Helper" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Divisas</title>
     
    <script src="JS/Funcoes.js" type="text/javascript"></script>
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
            } }



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
    <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblTituloATM" runat="server"></asp:Label>
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
            <td>
                <table class="tabela_campos"  >
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <uc1:Helper ID="hlpCliente" runat="server" TituloBotao="023_btncliente" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="hlpCliente" EventName="btnHelperClick" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right" valign="middle" nowrap>
                            <asp:Label ID="lblSubcliente" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <uc1:Helper ID="hlpSubcliente" runat="server" TituloBotao="023_btnsubcliente" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="hlpSubcliente" EventName="btnHelperClick" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right" valign="middle" nowrap>
                            <asp:Label ID="lblPuntoServicio" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <uc1:Helper ID="hlpPuntoServicio" runat="server" TituloBotao="023_btnpuntoservicio" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="hlpPuntoServicio" EventName="btnHelperClick" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblGrupo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelGrupo" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlGrupo" runat="server" Width="190px" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnAddGrupo" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnAddMorfologia" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnAddProceso" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlRedes" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlModelo" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="chkRegistroTira" EventName="CheckedChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="hidOidProcesoSelecionado" EventName="ValueChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="hidOidSelecionado" EventName="ValueChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <pro:Botao ID="btnAddGrupo" runat="server" EstiloIcone="EstiloIcone" Habilitado="True"
                                            Tipo="Novo" ExibirLabelBtn="false" Titulo="btnAnadir">
                                        </pro:Botao>
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgressDescricao" runat="server" AssociatedUpdatePanelID="UpdatePanelGrupo">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" alt="carregando" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" valign="middle">
                            <asp:Label ID="lblCodigo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtCodigo" runat="server" MaxLength="20" CssClass="Text02"></asp:TextBox>
                                    <asp:CustomValidator ID="csvCodigoObrigatorio" runat="server" ErrorMessage="" ControlToValidate="txtCodigo"
                                        Text="*"></asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td align="right" valign="middle" nowrap>
                            <asp:Label ID="lblRed" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td nowrap="nowrap">
                                        <asp:UpdatePanel ID="UpdatePanelRedes" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlRedes" runat="server" Width="190px" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="csvRedesObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlRedes"
                                                    Text="*"></asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlRedes" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelRedes">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="espaco_inicial">
                            &nbsp;
                        </td>
                        <td align="right">
                            <asp:Label ID="lblModelo" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td>
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelModelo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlModelo" runat="server" Width="190px" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="csvModeloObrigatorio" runat="server" ErrorMessage="" ControlToValidate="ddlModelo"
                                                    Text="*"></asp:CustomValidator>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlModelo" EventName="SelectedIndexChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanelModelo">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" valign="middle" nowrap>
                            <asp:Label ID="lblRegistroTira" runat="server" Visible="false" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left">
                            <table cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanelRegistroTira" runat="server" UpdateMode="Conditional"
                                            ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chkRegistroTira" runat="server" Visible="false" AutoPostBack="true" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 20px;">
                                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanelRegistroTira">
                                            <ProgressTemplate>
                                                <img src="Imagenes/loader1.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right" valign="middle" nowrap>
                        </td>
                        <td align="left">
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
                            <asp:Label ID="lblSubTituloMorfologias" runat="server"></asp:Label>
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
                <table border="0" width="65%">
                    <tr>
                        <td align="right" style="width: 70px">
                            <asp:Label ID="lblDesMorfologia" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left" style="width: 215px">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlMorfologia" runat="server" Width="190px" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="csvMorfologiaObrigatorio" runat="server" ErrorMessage=""
                                        ControlToValidate="ddlMorfologia" Text="*" Display="Dynamic"></asp:CustomValidator>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddMorfologia" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="hidOidSelecionado" EventName="ValueChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="hidAcaoMorfologia" EventName="ValueChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlMorfologia" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td align="right" style="width: 70px">
                            <asp:Label ID="lblFecha" runat="server" CssClass="Lbl2"></asp:Label>
                        </td>
                        <td align="left" style="width: 170px">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtFecha" runat="server" MaxLength="10" CssClass="Text02" AutoPostBack="true"></asp:TextBox>
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
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 70px">
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:Botao ID="btnAddMorfologia" runat="server" Habilitado="True" Tipo="Adicionar"
                                        Titulo="btnAnadir" ExibirLabelBtn="True" ExecutaValidacaoCliente="true">
                                    </pro:Botao>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="7">
                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvMorfologias" runat="server" AllowPaging="True" AllowSorting="True"
                                        GridPadrao="False" EstiloDestaque="GridLinhaDestaque" PageSize="10" AutoGenerateColumns="False"
                                        Ajax="True" Width="94%" GerenciarControleManualmente="false" ExibirCabecalhoQuandoVazio="false"
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
                                        <TextBox ID="objTextoProsegurGridView1" AutoPostBack="True" MaxLength="10" Width="30px"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="CodDescMorfologia" HeaderText="" SortExpression="" />
                                            <asp:BoundField DataField="FecInicio" HeaderText="" SortExpression="" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="" HeaderText="" SortExpression="" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbModificar" ImageUrl="Imagenes/pencil.PNG" runat="server">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbBorrar" ImageUrl="Imagenes/cross.PNG" runat="server"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </pro:ProsegurGridView>
                                    <asp:HiddenField ID="hidOidSelecionado" runat="server" />
                                    <asp:HiddenField ID="hidAcaoMorfologia" runat="server" />
                                    <asp:HiddenField ID="HidTemMorfologia" runat="server" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddMorfologia" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
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
            <td class="titulo02">
                <table cellpadding="0" cellspacing="4" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                        </td>
                        <td>
                            <asp:Label ID="lblSubtituloProcesos" runat="server"></asp:Label>
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
                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table border="0" width="65%">
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblProceso" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtProceso" runat="server" MaxLength="50" CssClass="Text02" AutoPostBack="true"
                                                Width="272px"></asp:TextBox>
                                            <asp:CustomValidator ID="csvProcesoObligatorio" runat="server" ErrorMessage="" ControlToValidate="txtProceso"
                                                Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblProduto" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlProduto" runat="server" Width="190px" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvProdutoObligatorio" runat="server" ErrorMessage="" ControlToValidate="ddlProduto"
                                                Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblCanal" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlCanal" runat="server" Width="190px" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvCanalObligatorio" runat="server" ErrorMessage="" ControlToValidate="ddlCanal"
                                                Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblSubcanal" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanelSubCanal" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlSubcanal" runat="server" Width="190px" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvSubcanalObligatorio" runat="server" ErrorMessage="" ControlToValidate="ddlSubcanal"
                                                Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlCanal" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgressSubcanal" runat="server" AssociatedUpdatePanelID="UpdatePanelSubCanal">
                                        <ProgressTemplate>
                                            <img src="Imagenes/loader1.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblModalidad" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlModalidad" runat="server" Width="300px" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:CustomValidator ID="csvModalidadObligatorio" runat="server" ErrorMessage=""
                                                ControlToValidate="ddlModalidad" Text="*"></asp:CustomValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblInfAdicional" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanelInfAdicional" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlInfAdicional" runat="server" Width="190px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlModalidad" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelInfAdicional">
                                        <ProgressTemplate>
                                            <img src="Imagenes/loader1.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap" style="width: 32%">
                                    <asp:Label ID="lblClienteFacturacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <uc1:Helper ID="hlpClienteFacturacion" runat="server" TituloBotao="023_btnclientefacturacion" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="hlpClienteFacturacion" EventName="btnHelperClick" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%">
                                    <asp:Label ID="lblModoContagem" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td align="left">
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
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkContarTarjetasTotales" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <pro:Botao ID="btnAddProceso" runat="server" Habilitado="True" Tipo="Adicionar" Titulo="btnAnadir"
                                        ExibirLabelBtn="True" ExecutaValidacaoCliente="true">
                                    </pro:Botao>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlGrupo" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:UpdatePanel ID="UpdatePanelProcesos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <pro:ProsegurGridView ID="GdvProcesos" runat="server" AllowPaging="True" AllowSorting="true"
                                        ColunasSelecao="OidProceso" EstiloDestaque="GridLinhaDestaque" GridPadrao="False"
                                        PageSize="10" AutoGenerateColumns="False" Ajax="True" GerenciarControleManualmente="false"
                                        ExibirCabecalhoQuandoVazio="false" NumeroRegistros="0" OrdenacaoAutomatica="True"
                                        PaginaAtual="0" PaginacaoAutomatica="True" Width="96%">
                                        <Pager ID="Pager1">
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
                                        <TextBox ID="TextBox1" AutoPostBack="True" MaxLength="10" Width="30px">            
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            
                                        </TextBox>
                                        <Columns>
                                            <asp:BoundField DataField="DesProceso" SortExpression="" />
                                            <asp:BoundField DataField="DesProducto" SortExpression="" />
                                            <asp:BoundField DataField="DesCanal" SortExpression="" />
                                            <asp:BoundField DataField="DesSubCanal" SortExpression="" />
                                            <asp:BoundField DataField="DesModalidad" SortExpression="" />
                                            <asp:BoundField DataField="DesIAC" SortExpression="" />
                                            <asp:BoundField DataField="CodDesClienteFacturacion" SortExpression="" />
                                            <asp:BoundField DataField="DesModoContage" SortExpression="" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbTerminos" ImageUrl="Imagenes/BtnGrafico_old.gif" runat="server">
                                                    </asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbBorrar" ImageUrl="Imagenes/cross.PNG" runat="server"></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
    <asp:UpdateProgress ID="UpdateProgressBtns" runat="server">
        <ProgressTemplate>
            <div id="divLoading" class="AlertLoading" style="visibility: hidden;"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
