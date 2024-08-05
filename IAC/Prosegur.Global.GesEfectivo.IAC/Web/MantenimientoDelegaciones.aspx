<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Principal.Master"
    CodeBehind="MantenimientoDelegaciones.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.MantenimientoDelegaciones"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Principal.Master" %>
<%@ Register Assembly="Prosegur.Web" Namespace="Prosegur.Web" TagPrefix="pro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>IAC - Mantenimiento de Delegaciones</title>
     
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
    <style type="text/css">
        .style2
        {
            width: 198px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="tabela_interna" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="titulo02">
                        <table cellpadding="0" cellspacing="4" border="0">
                            <tr>
                                <td>
                                    <img src="imagenes/ico01.jpg" alt="" width="16" height="18" />
                                </td>
                                <td>
                                    <asp:Label ID="lblTituloDelegacione" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <table class="tabela_campos"   border="0">
                            <tr>
                                <td class="style2" align="right">
                                    <asp:Label ID="lblPais" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="250px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:DropDownList ID="ddlPais" runat="server" Width="225px" AutoPostBack="True" />
                                                <asp:CustomValidator ID="csvPais" runat="server" ErrorMessage=""
                                                    ControlToValidate="ddlPais" Text="*" Display="Dynamic"></asp:CustomValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right" class="tamanho_celula">&nbsp;</td>
                                <td width="250px">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style2" align="right">
                                    <asp:Label ID="lblCodigoDelegacion" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="170px">
                                    <table cellpadding="0px" cellspacing="0px" style="width: 171%">
                                        <tr>
                                            <td width="160px">
                                                <asp:UpdatePanel ID="UpdatePanelCodigo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtCodigoDelegacion" runat="server" MaxLength="20" AutoPostBack="true"
                                                            CssClass="Text02" Width="140px"></asp:TextBox><asp:CustomValidator ID="csvCodigoObrigatorio"
                                                                runat="server" ErrorMessage="" ControlToValidate="txtCodigoDelegacion" Text="*" Display="Dynamic" />
                                                        <asp:CustomValidator ID="csvCodigoExistente" Display="Dynamic"
                                                            runat="server" ErrorMessage="" ControlToValidate="txtCodigoDelegacion" Text="*" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtCodigoDelegacion"
                                                            EventName="TextChanged" />
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
                                <td align="right" class="tamanho_celula">&nbsp;</td>
                                <td></td>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td nowrap="nowrap" >
                                    <asp:TextBox ID="txtCodigoAjeno" runat="server" Width="140px" MaxLength="25" CssClass="Text02" AutoPostBack="True" ReadOnly="True" />
                                    &nbsp;<asp:Label ID="lblDesCodigoAjeno" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td class="tamanho_celula" align="left" Width="520px">
                                    <table border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDesCodigoAjeno" runat="server" Width="225px" MaxLength="25" CssClass="Text02" AutoPostBack="True" ReadOnly="True" />
                                            </td>
                                            <td>
                                                <pro:Botao ID="btnAltaAjeno" runat="server" ExecutaValidacaoCliente="True" Habilitado="True" Tipo="Novo" ExibirLabelBtn="false" EstiloIcone="EstiloIcone" Titulo="btnCodigoAjeno">
                                                </pro:Botao>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="style2">
                                    <asp:Label ID="lblDescricaoDelegaciones" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:TextBox ID="txtDescricaoDelegaciones" runat="server" Width="225px" MaxLength="50" CssClass="Text02" AutoPostBack="True" />
                                                <asp:CustomValidator ID="csvDescricaoObrigatorio" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtDescricaoDelegaciones" Text="*" Display="Dynamic" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style2">
                                    <asp:Label ID="lblGmtMinutos" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:TextBox ID="txtGmtMinutos" runat="server" AutoPostBack="true"
                                                    Width="225px" CssClass="Text02" MaxLength="5"></asp:TextBox>
                                                <asp:CustomValidator ID="csvGmtMinutosObrigatorio" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtGmtMinutos" Text="*" Display="Dynamic" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style2">
                                    <asp:Label ID="lblFechaVeranoInicio" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:TextBox ID="txtFechaVeranoInicio" runat="server" CssClass="Text02"
                                                    Width="225px" AutoPostBack="true" MaxLength="10" />
                                                <asp:CustomValidator ID="csvFechaVeranoInicio" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtFechaVeranoInicio" Text="*" Display="Dynamic" />

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style2">
                                    <asp:Label ID="lblFechaVeranoFim" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:TextBox ID="txtFechaVeranoFim" runat="server" Width="225px"
                                                    AutoPostBack="true" CssClass="Text02" MaxLength="10" />
                                                <asp:CustomValidator ID="csvFechaVeranoFim" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtFechaVeranoFim" Text="*" Display="Dynamic" />
                                                <asp:CustomValidator ID="csvFechaVeranoInicioInvalida" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtFechaVeranoFim" Text="*" Display="Dynamic" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style2">
                                    <asp:Label ID="lblCantidadMinAjuste" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:TextBox ID="txtCantidadMinAjuste" runat="server" MaxLength="5"
                                                    AutoPostBack="true" CssClass="Text02" Width="225px" />
                                                <asp:CustomValidator ID="csvCantiAjuste" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtCantidadMinAjuste" Text="*" Display="Dynamic" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style2">
                                    <asp:Label ID="lblZona" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <table width="100%" cellpadding="0px" cellspacing="0px">
                                        <tr>
                                            <td width="250px">
                                                <asp:TextBox ID="txtZona" runat="server" MaxLength="50" AutoPostBack="true" Width="225px" CssClass="Text02" />
                                                <asp:CustomValidator ID="csvZona" runat="server" ErrorMessage=""
                                                    ControlToValidate="txtZona" Text="*" Display="Dynamic" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="right">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right" class="style2">
                                    <asp:Label ID="lblVigente" runat="server" CssClass="Lbl2"></asp:Label>
                                </td>
                                <td width="300px">
                                    <asp:CheckBox ID="chkVigente" runat="server" />
                                </td>
                                <td align="right">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="upppAcaoPagina" runat="server">
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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPais" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
