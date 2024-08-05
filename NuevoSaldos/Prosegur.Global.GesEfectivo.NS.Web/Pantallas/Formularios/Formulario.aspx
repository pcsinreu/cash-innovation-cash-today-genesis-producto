<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Master.Master" CodeBehind="Formulario.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Formulario" %>

<%@ MasterType VirtualPath="~/Master/Master.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../App_Themes/Padrao/css/js_color_picker_v2.css" media="screen" />
    <script src="../../js/color_functions.js" type="text/javascript"></script>
    <script src="../../js/js_color_picker_v2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function DesabilitaAccionesContables(controle1, controle2, controle3, Estado, somenteAceptado) {

            if (Estado == true) {
                document.getElementById(controle1).disabled = false;
                document.getElementById(controle1).focus();
                document.getElementById(controle1).options[0].selected = true;
                document.getElementById(controle2).disabled = true;
                document.getElementById(controle2).value = "";

                var nodes = document.getElementById(controle3).getElementsByTagName("select");
                for (var i = 0; i < nodes.length; i++) {
                    nodes[i].disabled = true;
                    nodes[i].options[0].selected = true;
                }
            }
            else {
                document.getElementById(controle1).disabled = false;
                document.getElementById(controle1).focus();
                document.getElementById(controle1).value = "";
                document.getElementById(controle2).disabled = true;
                document.getElementById(controle2).options[0].selected = true;
                var nodes = document.getElementById(controle3).getElementsByTagName("select");
                for (var i = 0; i < nodes.length; i++) {
                    if (somenteAceptado) {
                        nodes[i].disabled = (nodes[i].name.indexOf("Aceptado") < 0);
                    }
                    else {
                        nodes[i].disabled = false;
                    }
                    nodes[i].options[0].selected = true;
                }
            }
        }

        function checkFileExtension(elem, msg) {
            var filePath = elem.value;


            if (filePath.indexOf('.') == -1)
                return false;


            var validExtensions = new Array();
            var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();


            validExtensions[0] = 'jpg';
            validExtensions[1] = 'jpeg';
            validExtensions[2] = 'bmp';
            validExtensions[3] = 'png';
            validExtensions[4] = 'gif';
            validExtensions[5] = 'ico';

            for (var i = 0; i < validExtensions.length; i++) {
                if (ext == validExtensions[i])
                    return true;
            }

            elem.value = "";
            alert(String.format(msg, ext.toUpperCase()));
            return false;
        }

    </script>
    <style>
        .buttonNav {
            width: 100px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
        <asp:HiddenField ID="hidIdendificador" runat="server" />
        <asp:HiddenField ID="hidAccionContable" runat="server" />
        <asp:Wizard ID="wMain" runat="server" CancelButtonText="Cancelar"
            DisplayCancelButton="True" FinishCompleteButtonText="Cerrar"
            FinishPreviousButtonText="Anterior" StartNextButtonText="Seguinte"
            StepNextButtonText="Seguinte" StepPreviousButtonText="Anterior"
            ActiveStepIndex="0" Height="100%" Width="100%" DisplaySideBar="False">
            <HeaderStyle Font-Bold="True" Font-Size="Small" HorizontalAlign="Left" />
            <NavigationStyle HorizontalAlign="Left" />
            <NavigationButtonStyle CssClass="ui-button buttonNav" />
            <WizardSteps>
                <asp:WizardStep ID="ws1" runat="server" Title="Paso 1" StepType="Start">
                    <fieldset class="ui-fieldset ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                            <asp:Label ID="lblPaso1" Text="Paso 1" runat="server" />
                        </legend>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50%;">
                                    <div>
                                        <asp:Label ID="lblCodigo" SkinID="padrao-label" runat="server" Text="Código"></asp:Label><br />
                                        <br />
                                        <asp:TextBox ID="txtCodigo" runat="server" MaxLength="15" SkinID="padrao-textbox-parentWidth"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="labelErro" runat="server" ID="rfvtxtCodigo"
                                            Display="Static" ControlToValidate="txtCodigo" EnableClientScript="true" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div style="background-color: #f7f7f7;">
                                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" SkinID="padrao-label"></asp:Label><br />
                                        <br />
                                        <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="50" SkinID="padrao-textbox-parentWidth"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="labelErro" runat="server" ID="rfvtxtDescripcion" Display="Static" ControlToValidate="txtDescripcion" EnableClientScript="true" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </div>
                                    <asp:Label ID="lblColor" runat="server" SkinID="padrao-label" Text="Color que se presenta en la cabecera"></asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="txtColor" runat="server" SkinID="padrao-textbox-parentWidth"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="labelErro" runat="server" ID="rfvtxtColor" Display="Static" ControlToValidate="txtColor" EnableClientScript="true" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    <div style="background-color: #f7f7f7;">
                                        <asp:Label ID="lblTipoDocumento" runat="server" SkinID="padrao-label" Text="Tipo de Documento (Agrupación por Tipo)"></asp:Label><br />
                                        <br />
                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" SkinID="padrao-dropdownlist-parentWidth"></asp:DropDownList>
                                        <asp:RequiredFieldValidator CssClass="labelErro" runat="server" ID="rfvtxtTipoDocumento" Display="Static" ControlToValidate="ddlTipoDocumento" EnableClientScript="true" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                                    </div>
                                    <br />
                                    <asp:Label ID="lblIcono" runat="server" Text="Icono"></asp:Label><br />
                                    <table class="ui-fieldset" style="height: auto;">
                                        <tr>
                                            <td>
                                                <asp:FileUpload ID="fupImagem" runat="server" />
                                            </td>
                                            <td style="padding-right: 10px; padding-left: 10px;">
                                                <asp:Button ID="btnUpload" runat="server" CssClass="ui-button" CausesValidation="false" Text="Cargar" />
                                            </td>
                                            <td>
                                                <fieldset class="ui-fieldset" style="width: 40px; height: 40px;">
                                                    <asp:Image ID="imgPhoto" runat="server" Height="40px" Width="40px" Visible="false"
                                                        Style="margin-left: 0px" />
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                                <td style="width: 50%; float: right; background-color: #f7f7f7;">
                                    <div>
                                        <asp:CheckBox ID="chkDocumentoIndividual" runat="server" /><br />
                                        <br />
                                        <asp:CheckBox ID="chkDocumentoGrupo" runat="server" /><br />
                                        <br />
                                        <asp:CheckBox ID="chkActivo" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:WizardStep>
                <asp:WizardStep ID="ws2" runat="server" Title="Paso 2" StepType="Step">
                    <fieldset class="ui-fieldset ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                            <asp:Label ID="lblPaso2" Text="Paso 2" runat="server" />
                        </legend>
                        <div style="width: auto; height: auto; margin: 5px;">
                            <asp:RadioButton ID="rbCaracteristicaPrincipalGestionRemesa" runat="server"
                                Checked="True" GroupName="CaracteristicaPrincipal" TabIndex="12" Text="Gestión de Remesas" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rbCaracteristicaPrincipalGestionBultos" runat="server"
                                GroupName="CaracteristicaPrincipal" TabIndex="13" Text="Gestión de Bultos" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rbCaracteristicaPrincipalGestionContenedores" runat="server"
                                GroupName="CaracteristicaPrincipal" TabIndex="14" Text="Gestión de Contenedores" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rbCaracteristicaPrincipalGestionFondos" runat="server"
                                GroupName="CaracteristicaPrincipal" TabIndex="15" Text="Gestión de Fondos" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rbCaracteristicaPrincipalCierres" runat="server"
                                GroupName="CaracteristicaPrincipal" TabIndex="16" Text="Cierres" />
                            <br />
                            <br />
                            <asp:RadioButton ID="rbCaracteristicaPrincipalOtrosMovimientos" runat="server"
                                GroupName="CaracteristicaPrincipal" TabIndex="17" Text="Otros Movimientos" />
                        </div>
                    </fieldset>
                </asp:WizardStep>
                <asp:WizardStep ID="ws3" runat="server" StepType="Step" Title="Paso 3">
                    <fieldset class="ui-fieldset ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                            <asp:Label ID="lblPaso3" Text="Paso 2" runat="server" />
                        </legend>
                        <asp:Panel ID="pnlWs3GestionRemesas" runat="server" Visible="false">

                            <div style="width: auto; height: auto; margin-top: 5px;">
                                <asp:RadioButton ID="rbGestionRemesasAltas" runat="server" GroupName="GestionRemesas" Checked="True" TabIndex="18" Text="Altas"/>
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionRemesasBajas" runat="server" GroupName="GestionRemesas" TabIndex="19" Text="Bajas"/>
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionRemesasReenvios" runat="server" GroupName="GestionRemesas" TabIndex="20" Text="Reenvíos"/>
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionRemesasActas" runat="server" GroupName="GestionRemesas" TabIndex="21" Text="Actas"/>
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionRemesasSustitucion" runat="server" GroupName="GestionRemesas" TabIndex="21" Text="Sustitución"/>
                            </div>
                            <br />
                            <div style="width: auto; height: auto; background-color: #f7f7f7;">
                                <asp:Label ID="lblGestionRemesasTipoBulto" runat="server" TabIndex="24" Text="Tipos de Bulto a tratar"></asp:Label>
                                <asp:CheckBoxList ID="chlGestionRemesasTipoBulto" runat="server" TabIndex="25"></asp:CheckBoxList>
                            </div>
                            <br />
                            <div style="background-color: #f7f7f7;">
                                <asp:CheckBox ID="chkGestionRemesasPermiteLlegarSaldoNegativo" runat="server"
                                    Text="¿El movimiento permite llegar a saldo negativo?" TabIndex="26" />
                            </div>
                            <div id="dvGestionRemesasExcluirSectoresHijos" runat="server" style="background-color: #f7f7f7; margin-bottom: 5px; padding-top: 5px; display: none;">
                                <asp:CheckBox ID="chkGestionRemesasExcluirSectoresHijos" runat="server"
                                    Text="Excluir Sectores Hijos" TabIndex="26" />
                            </div>
                            <script type="text/javascript">

                                function exibeRemesasExcluirSectoresHijos(controle) {
                                    var chkGestionRemesasExcluirSectoresHijos = document.getElementById('<%= chkGestionRemesasExcluirSectoresHijos.ClientID%>');
                                    var dvGestionRemesasExcluirSectoresHijos = document.getElementById('<%= dvGestionRemesasExcluirSectoresHijos.ClientID%>');
                                    var rbGestionRemesasAltas = document.getElementById('<%= rbGestionRemesasAltas.ClientID %>');
                                    if (controle.id != rbGestionRemesasAltas.id && controle.checked) {
                                        dvGestionRemesasExcluirSectoresHijos.style.display = 'block';
                                    } else {
                                        dvGestionRemesasExcluirSectoresHijos.style.display = 'none';
                                        chkGestionRemesasExcluirSectoresHijos.checked = false;
                                    }                                    
                                }

                            </script>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs3GestionBultos" runat="server" Visible="false">
                            <div style="width: auto; height: auto; margin-top: 5px;">
                                <asp:RadioButton ID="rbGestionBultosAltas" runat="server" GroupName="GestionBultos" Checked="True" Text="Altas" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionBultosBajas" runat="server" GroupName="GestionBultos" Text="Bajas" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionBultosReenvios" runat="server" GroupName="GestionBultos" Text="Reenvíos" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionBultosActas" runat="server" GroupName="GestionBultos" Text="Actas" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionBultosSustitucion" runat="server" GroupName="GestionBultos" Text="Sustitución" />
                            </div>
                            <br />
                            <div style="width: auto; height: auto; background-color: #f7f7f7;">
                                <asp:Label ID="lblGestionBultosTipoBulto" runat="server" Text="Tipos de Bulto a tratar"></asp:Label>
                                <asp:CheckBoxList ID="chlGestionBultosTipoBulto" runat="server"></asp:CheckBoxList>
                            </div>
                            <br />
                            <div style="width: auto; height: auto; background-color: #f7f7f7;">
                                <asp:CheckBox ID="chkGestionBultosPermiteLlegarSaldoNegativo" runat="server"
                                    Text="¿El movimiento permite llegar a saldo negativo?" />
                            </div>
                            <div id="dvGestionBultosExcluirSectoresHijos" runat="server" style="background-color: #f7f7f7; margin-bottom: 5px; padding-top: 5px; display: none;">
                                <asp:CheckBox ID="chkGestionBultosExcluirSectoresHijos" runat="server"
                                    Text="Excluir Sectores Hijos" TabIndex="26" />
                            </div>
                            <script type="text/javascript">

                                function exibeBultosExcluirSectoresHijos(controle) {
                                    var chkGestionBultosExcluirSectoresHijos = document.getElementById('<%= chkGestionBultosExcluirSectoresHijos.ClientID%>');
                                    var dvGestionBultosExcluirSectoresHijos = document.getElementById('<%= dvGestionBultosExcluirSectoresHijos.ClientID%>');
                                    var rbGestionBultosAltas = document.getElementById('<%= rbGestionBultosAltas.ClientID %>');
                                    if (controle.id != rbGestionBultosAltas.id && controle.checked) {
                                        dvGestionBultosExcluirSectoresHijos.style.display = 'block';
                                    } else {
                                        dvGestionBultosExcluirSectoresHijos.style.display = 'none';
                                        chkGestionBultosExcluirSectoresHijos.checked = false;
                                    }
                                }

                            </script>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs3GestionContenedores" runat="server" Visible="false">
                            <div style="width: auto; height: auto; margin-top: 5px;">
                                <asp:RadioButton ID="rbGestionContenedoresAltas" runat="server" AutoPostBack="true" GroupName="GestionContenedores" Checked="True" Text="Altas" OnCheckedChanged="rbGestionContenedores_CheckedChanged" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionContenedoresBajas" runat="server" AutoPostBack="true" GroupName="GestionContenedores" Text="Bajas" OnCheckedChanged="rbGestionContenedores_CheckedChanged" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionContenedoresReenvios" runat="server" AutoPostBack="true" GroupName="GestionContenedores" Text="Reenvíos" OnCheckedChanged="rbGestionContenedores_CheckedChanged" />
                            </div>
                            <br />
                            <br />
                            <div style="background-color: #f7f7f7;">
                                <asp:CheckBox ID="chkGestionContenedoresPermiteLlegarSaldoNegativo" runat="server"
                                    Text="¿El movimiento permite llegar a saldo negativo?" />
                            </div>
                            <br />
                            <div id="dvGestionContenedoresPackModular" runat="server" style="background-color: #f7f7f7; display: block;">
                                <asp:CheckBox ID="chkGestionContenedoresPackModular" runat="server"
                                    Text="Pack Modular" />
                            </div>
                            <br />
                            <div id="dvGestionContenedoresExcluirSectoresHijos" runat="server" style="background-color: #f7f7f7; display: none;">
                                <asp:CheckBox ID="chkGestionContenedoresExcluirSectoresHijos" runat="server"
                                    Text="Excluir Sectores Hijos" />
                            </div>
                            
                        </asp:Panel>
                        <asp:Panel ID="pnlWs3GestionFondos" runat="server" Visible="false">
                            <div style="width: auto; margin-top: 5px; height: auto;">
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondos" runat="server" GroupName="GestionFondos" Checked="True" Text="Movimiento de Fondos" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionFondosAjustes" runat="server" GroupName="GestionFondos" Text="Ajustes" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionFondosSustitucion" runat="server" GroupName="GestionFondos" Text="Sustitución" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionFondosSolicitacion" runat="server" GroupName="GestionFondos" Text="Solicitación" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionFondosClasificacion" runat="server" GroupName="GestionFondos" Text="Clasificación" />                               
                            </div>
                            <br />
                            <div style="width: auto; margin-bottom: 5px; height: auto; background-color: #f7f7f7;">
                                <asp:CheckBox ID="chkGestionFondosPermiteLlegarSaldoNegativo" runat="server"
                                    Text="¿El movimiento permite llegar a saldo negativo?" />
                            </div>
                            <div id="dvTipoAjustes" style="width: auto; margin-top: 5px; height: auto; visibility: hidden;">
                                <fieldset class="ui-fieldset ui-fieldset-toggleable">
                                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                                        <asp:Label ID="lblTipoAjustes" Text="Tipos de Ajustes" runat="server" />
                                    </legend>
                                    <asp:RadioButton ID="rbTipoAjustesCualquier" runat="server" GroupName="TiposAjustes" Text="Permitir cualquieras tipo de ajustes" Checked="true" />
                                    <br />
                                    <br />
                                    <asp:RadioButton ID="rbTipoAjustesSoloPositivos" runat="server" GroupName="TiposAjustes" Text="Sólo permitir ajustes Positivos" />
                                    <br />
                                    <br />
                                    <asp:RadioButton ID="rbTipoAjustesSoloNegativos" runat="server" GroupName="TiposAjustes" Text="Sólo permitir ajustes Negativos" />
                                </fieldset>
                            </div>
                            <script type="text/javascript">
                                function exibeTipoAjustes() {
                                    var rbGestionFondosAjustes = document.getElementById('<%= rbGestionFondosAjustes.ClientID%>');
                                    var dvTipoAjustes = document.getElementById('dvTipoAjustes');
                                    if (rbGestionFondosAjustes.checked == true) {
                                        dvTipoAjustes.style.visibility = 'visible';
                                    } else {
                                        dvTipoAjustes.style.visibility = 'hidden';
                                    }
                                }
                                window.onload = exibeTipoAjustes;
                            </script>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs3Cierres" runat="server" Visible="false">
                            <div style="width: auto; margin-top: 5px; margin-bottom: 5px; height: auto;">
                                <asp:RadioButton ID="rbCierresTesoro" runat="server" GroupName="Cierres" Checked="True" Text="Cierre de Tesoro" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbCierresCaja" runat="server" GroupName="Cierres" Text="Cierre de Caja" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbCierresCuadreFisico" runat="server" GroupName="Cierres" Text="Cuadre Físico" />
                            </div>
                            <script type="text/javascript">

                                function displayOpcionesCierreCaja() {
                                    var rbCierresCaja = document.getElementById('<%= rbCierresCaja.ClientID%>');
                                    if (rbCierresCaja.checked == true) {
                                        document.getElementById('dvOpcionesCierreCaja').style.display = 'block';
                                    } else {
                                        document.getElementById('dvOpcionesCierreCaja').style.display = 'none';
                                        document.getElementById('<%= chkCierreCajaPermiteLlegarSaldoNegativo.ClientID%>').checked = false;
                                    }

                                }

                            </script>
                            <div id="dvOpcionesCierreCaja" style="width: auto; margin: 10px; height: auto; display: none;">
                                <fieldset class="ui-fieldset ui-fieldset-toggleable">
                                    <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                                        <asp:Label ID="lblOpcionesCierreCaja" Text="" runat="server" />
                                    </legend>
                                    <asp:CheckBox ID="chkCierreCajaPermiteLlegarSaldoNegativo" runat="server" Text="" />
                                </fieldset>
                            </div>
                            <script>displayOpcionesCierreCaja();</script>
                            <div id="dvGestionCierreExcluirSectoresHijos" runat="server" style="margin-bottom: 5px; margin-top: 10px; display: none;">
                                <asp:CheckBox ID="chkGestionCierreExcluirSectoresHijos" runat="server"
                                    Text="Excluir Sectores Hijos" TabIndex="26" />
                            </div>                         
                            <script type="text/javascript">

                                function exibeCierreExcluirSectoresHijos(controle) {
                                    var chkGestionCierreExcluirSectoresHijos = document.getElementById('<%= chkGestionCierreExcluirSectoresHijos.ClientID%>');
                                    var dvGestionCierreExcluirSectoresHijos = document.getElementById('<%= dvGestionCierreExcluirSectoresHijos.ClientID%>');
                                    var rbCierresCuadreFisico = document.getElementById('<%= rbCierresCuadreFisico.ClientID%>');
                                    if (controle.id != rbCierresCuadreFisico.id && controle.checked) {
                                        dvGestionCierreExcluirSectoresHijos.style.display = 'block';
                                    } else {
                                        dvGestionCierreExcluirSectoresHijos.style.display = 'none';
                                        chkGestionCierreExcluirSectoresHijos.checked = false;
                                    }
                                }

                            </script>      
                        </asp:Panel>
                    </fieldset>
                </asp:WizardStep>
                <asp:WizardStep ID="ws4" runat="server" StepType="Step" Title="Paso 4">
                    <fieldset class="ui-fieldset ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                            <asp:Label ID="lblPaso4" Text="Paso 2" runat="server" />
                        </legend>
                        <asp:Panel ID="pnlWs4GestionRemesasBajas" runat="server" Visible="false">
                            <div style="width: auto; margin-top: 5px; height: auto;">
                                <asp:RadioButton ID="rbGestionRemesasBajasBajaElementos" runat="server" GroupName="GestionRemesasBajas" Checked="True" Text="Baja de una Remesa en el Sistema" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionRemesasBajasSalidasRecorrido" runat="server" GroupName="GestionRemesasBajas" Text="Salida de una Remesa en una Ruta" />
                            </div>
                            <br />
                            <div style="width: auto; margin-bottom: 5px; height: auto; background-color: #f7f7f7;">
                                <asp:Label ID="lblGestionRemesasBajasFiltro" runat="server" Text="Basado en reporte (filtro)"></asp:Label><br />
                                <asp:DropDownList ID="ddlGestionRemesasBajasFiltro" runat="server" SkinID="padrao-dropdownlist"></asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionRemesasReenvios" runat="server" Visible="false">
                            <div style="width: auto; margin-top: 5px; height: auto;">
                                <asp:RadioButton ID="rbGestionRemesasReenviosEntreSectores" runat="server" GroupName="GestionRemesasReenvios" Checked="True" Text="Entre Sectores" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionRemesasReenviosEntrePlantas" runat="server" GroupName="GestionRemesasReenvios" Text="Entre Plantas" />
                            </div>
                            <br />
                            <div style="width: auto; height: auto; background-color: #f7f7f7;">
                                <asp:CheckBox ID="chkGestionRemesasReenviosReenvioAutomatico" runat="server" GroupName="GestionRemesasReenvios" Checked="False" Text="¿Utilizar este formulario para Documentos de Reenvío Automatico?" />
                            </div>
                            <br />
                            <div style="width: auto; margin-bottom: 5px; height: auto;">
                                <asp:Label ID="lblGestionRemesasReenviosFiltro" runat="server" Text="Basado en reporte (filtro)"></asp:Label><br />
                                <asp:DropDownList ID="ddlGestionRemesasReenviosFiltro" runat="server" SkinID="padrao-dropdownlist"></asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionRemesasActas" runat="server" Visible="false">
                            <div style="width: auto; margin-top: 5px; height: auto;">
                                <asp:RadioButton ID="rbGestionRemesasActasActaRecuento" runat="server" GroupName="GestionRemesasActas" Checked="True" Text="Acta de Recuento" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionRemesasActasActaClasificado" runat="server" GroupName="GestionRemesasActas" Text="Acta de Clasificado" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionRemesasActasActaEmbolsado" runat="server" GroupName="GestionRemesasActas" Text="Acta de Embolsado" />
                                <asp:Panel ID="pnlGestionRemesasActasActaDesembolsado" runat="server">
                                    <br />
                                    <asp:RadioButton ID="rbGestionRemesasActasActaDesembolsado" runat="server" GroupName="GestionRemesasActas" Text="Acta de Desembolsado" />
                                </asp:Panel>
                            </div>
                            <br />
                            <div style="width: auto; margin-bottom: 5px; height: auto; background-color: #f7f7f7;">
                                <asp:Label ID="lblGestionRemesasActasFiltro" runat="server" Text="Basado en reporte (filtro)"></asp:Label><br />
                                <asp:DropDownList ID="ddlGestionRemesasActasFiltro" runat="server" SkinID="padrao-dropdownlist"></asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionBultosBajas" runat="server" Visible="false">
                            <div style="width: auto; margin-top: 5px; height: auto;">
                                <asp:RadioButton ID="rbGestionBultosBajasBajaElementos" runat="server" GroupName="GestionBultosBajas" Checked="True" Text="Baja de una Remesa en el Sistema" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionBultosBajasSalidasRecorrido" runat="server" GroupName="GestionBultosBajas" Text="Salida de una Remesa en una Ruta" />
                            </div>
                            <br />
                            <div style="width: auto; margin-bottom: 5px; height: auto; background-color: #f7f7f7;">
                                <asp:Label ID="lblGestionBultosBajasFiltro" runat="server" Text="Basado en reporte (filtro)"></asp:Label><br />
                                <asp:DropDownList ID="ddlGestionBultosBajasFiltro" runat="server" SkinID="padrao-dropdownlist">
                                </asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionBultosReenvios" runat="server" Visible="false">
                            <div style="width: auto; margin-top: 5px; height: auto;">
                                <asp:RadioButton ID="rbGestionBultosReenviosEntreSectores" runat="server" GroupName="GestionBultosReenvios" Checked="True" Text="Entre Sectores" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionBultosReenviosEntrePlantas" runat="server" GroupName="GestionBultosReenvios" Text="Entre Plantas" />
                            </div>
                            <br />
                            <div style="width: auto; height: auto; background-color: #f7f7f7;">
                                <asp:CheckBox ID="chkGestionBultosReenviosReenvioAutomatico" runat="server" GroupName="GestionBultosReenvios" Checked="False" Text="¿Utilizar este formulario para Documentos de Reenvío Automatico?" />
                            </div>
                            <br />
                            <div style="width: auto; margin-bottom: 5px; height: auto;">
                                <asp:Label ID="lblGestionBultosReenviosFiltro" runat="server" Text="Basado en reporte (filtro)"></asp:Label><br />
                                <asp:DropDownList ID="ddlGestionBultosReenviosFiltro" runat="server" SkinID="padrao-dropdownlist"></asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionBultosActas" runat="server" Visible="false">
                            <div style="width: auto; margin-top: 5px; height: auto;">
                                <asp:RadioButton ID="rbGestionBultosActasActaRecuento" runat="server" GroupName="GestionBultosActas" Checked="True" Text="Acta de Recuento" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionBultosActasActaClasificado" runat="server" GroupName="GestionBultosActas" Text="Acta de Clasificado" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionBultosActasActaEmbolsado" runat="server" GroupName="GestionBultosActas" Text="Acta de Embolsado" />
                                <asp:Panel ID="pnlGestionBultosActasActaDesembolsado" runat="server">
                                    <br />
                                    <asp:RadioButton ID="rbGestionBultosActasActaDesembolsado" runat="server" GroupName="GestionBultosActas" Text="Acta de Desembolsado" />
                                </asp:Panel>
                            </div>
                            <br />
                            <div style="width: auto; margin-bottom: 5px; height: auto; background-color: #f7f7f7;">
                                <asp:Label ID="lblGestionBultosActasFiltro" runat="server" Text="Basado en reporte (filtro)"></asp:Label><br />
                                <asp:DropDownList ID="ddlGestionBultosActasFiltro" runat="server" SkinID="padrao-dropdownlist"></asp:DropDownList>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionFondosMovimientoFondos" runat="server" Visible="false">
                            <div style="width: auto; margin-left: 10px; height: auto;">
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondosEntreSectores" runat="server" GroupName="GestionFondosMovimientoFondos" Checked="True" Text="Entre Sectores" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondosEntreCanales" runat="server" GroupName="GestionFondosMovimientoFondos" Text="Entre Canales" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondosEntrePlantas" runat="server" GroupName="GestionFondosMovimientoFondos" Text="Entre Plantas" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionFondosMovimientoFondosEntreClientes" runat="server" GroupName="GestionFondosMovimientoFondos" Text="Entre Clientes" />
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionCierre" runat="server" Visible="false">
                            <div style="width: auto; margin: 10px; height: auto;">                                
                                <asp:RadioButton ID="rbGestionCierreEntreSectores" runat="server" GroupName="GestionCierre" Text="Entre Sectores" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionCierreEntrePlantas" runat="server" GroupName="GestionCierre" Text="Entre Plantas" />
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionContenedoresReenvios" runat="server" Visible="false">
                            <div style="width: auto; margin-left: 10px; height: auto;">
                                <asp:RadioButton ID="rbGestionContenedoresReenviosEntreSectores" runat="server" GroupName="GestionContenedoresReenvios" Checked="True" Text="Entre Sectores" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionContenedoresReenviosEntrePlantas" runat="server" GroupName="GestionContenedoresReenvios" Text="Entre Plantas" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionContenedoresReenviosEntreClientes" runat="server" GroupName="GestionContenedoresReenvios" Text="Entre Clientes" />
                            </div>
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlWs4GestionContenedoresBajas" runat="server" Visible="false">
                            <div style="width: auto; margin-left: 10px; height: auto;">
                                <asp:RadioButton ID="rbGestionContenedoresBajasDesarmar" runat="server" GroupName="GestionContenedoresBajas" Checked="True" Text="Desarmar el Contenedor" />
                                <br />
                                <br />
                                <asp:RadioButton ID="rbGestionContenedoresBajasSalida" runat="server" GroupName="GestionContenedoresBajas" Text="Salida del Contenedor en una Ruta " />                                
                            </div>
                            <br />
                        </asp:Panel>
                    </fieldset>
                </asp:WizardStep>
                <asp:WizardStep ID="ws5" runat="server" StepType="Step" Title="Paso 5">
                    <fieldset class="ui-fieldset ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                            <asp:Label ID="lblPaso5" Text="Paso 5" runat="server" />
                        </legend>
                        <asp:Panel ID="pnlWs5TiposSectoresOrigen" runat="server" Width="100%" Visible="false" Style="margin-top: 5px;">
                            <asp:Label ID="lblTiposSectoresOrigen" runat="server" Text="Tipos de Sectores de Origen"></asp:Label>
                            <table border="1" style="margin-bottom: 5px;">
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstTiposSectoresOrigenReferencia" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all filterInput"
                                            SelectionMode="Multiple" Rows="9" Width="320px"></asp:ListBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnTiposSectoresOrigenQuitaTodos" runat="server" CausesValidation="false"
                                            Text="<<" SkinID="button_navegate_remove_all" />
                                        <br />
                                        <br />
                                        <asp:ImageButton ID="imgBtnTiposSectoresOrigenQuitaUno" runat="server" Text="<" CausesValidation="false"
                                            SkinID="button_navegate_remove" />
                                        <br />
                                        <br />
                                        <asp:ImageButton ID="imgBtnTiposSectoresOrigenSeleccionaUno" runat="server" CausesValidation="false"
                                            Text=">" SkinID="button_navegate_add" />
                                        <br />
                                        <br />
                                        <asp:ImageButton ID="imgBtnTiposSectoresOrigenSeleccionaTodos" runat="server" CausesValidation="false"
                                            Text=">>" SkinID="button_navegate_add_all" />
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstTiposSectoresOrigenSeleccionados" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all filterInput"
                                            SelectionMode="Multiple" Rows="9" Width="320px"></asp:ListBox>
                                    </td>
                                    <td>
                                        <asp:CustomValidator ID="cvTiposSectoresOrigenSeleccionados" runat="server" ControlToValidate="lstTiposSectoresOrigenSeleccionados" ValidateEmptyText="true" ErrorMessage="*" OnServerValidate="ValidasTipoSectorOrigen"></asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="pnlWs5TiposSectoresDestino" runat="server" Width="100%" Visible="false">
                            <asp:Label ID="lblTiposSectoresDestino" runat="server" Text="Tipos de Sectores de Destino"></asp:Label>
                            <table border="1" style="margin-bottom: 5px;">
                                <tr>
                                    <td>
                                        <asp:ListBox ID="lstTiposSectoresDestinoReferencia" runat="server"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all filterInput"
                                            SelectionMode="Multiple" Rows="9" Width="320px"></asp:ListBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnTiposSectoresDestinoQuitaTodos" runat="server" CausesValidation="false"
                                            Enabled="true" SkinID="button_navegate_remove_all" />
                                        <br />
                                        <br />
                                        <asp:ImageButton ID="imgBtnTiposSectoresDestinoQuitaUno" runat="server" CausesValidation="false"
                                            SkinID="button_navegate_remove" />
                                        <br />
                                        <br />
                                        <asp:ImageButton ID="imgBtnTiposSectoresDestinoSeleccionaUno" runat="server" CausesValidation="false"
                                            Text=">" SkinID="button_navegate_add" />
                                        <br />
                                        <br />
                                        <asp:ImageButton ID="imgBtnTiposSectoresDestinoSeleccionaTodos" runat="server" CausesValidation="false"
                                            Text=">>" SkinID="button_navegate_add_all" />
                                    </td>
                                    <td>
                                        <asp:ListBox ID="lstTiposSectoresDestinoSeleccionados" runat="server"
                                            CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all filterInput" SelectionMode="Multiple" Rows="9" Width="320px"></asp:ListBox>
                                    </td>
                                    <td>
                                        <asp:CustomValidator ID="cvTiposSectoresDestinoSeleccionados" runat="server" ControlToValidate="lstTiposSectoresDestinoSeleccionados" ValidateEmptyText="true" ErrorMessage="*" OnServerValidate="ValidasTipoSectorDestino"></asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </fieldset>
                </asp:WizardStep>
                <asp:WizardStep ID="ws6" runat="server" StepType="Step" Title="Paso 6">
                    <fieldset class="ui-fieldset ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                            <asp:Label ID="lblPaso6" Text="Paso 6" runat="server" />
                        </legend>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 40%">
                                    <asp:Panel ID="pnlWs6AccionContable" runat="server"  Visible="false">
                                        <div runat="server" style="width: auto; height: auto; margin-left: 5px;" class="ui-fieldset">
                                            <asp:Label ID="lblAccionContable" runat="server" Text="Accion Contable"></asp:Label><br />
                                            <br />
                                            <asp:RadioButton ID="rbAccionContableSeleccionar" runat="server"
                                                Checked="True" GroupName="AccionContable" TabIndex="12" Text="" />
                                            <asp:DropDownList ID="ddlAccionContableSeleccionar" runat="server" AutoPostBack="true" SkinID="padrao-dropdownlist"></asp:DropDownList><br />
                                            <br />
                                            <asp:RadioButton ID="rbAccionContableNuevo" runat="server"
                                                Checked="False" GroupName="AccionContable" TabIndex="12" Text="" />
                                            <asp:TextBox ID="txtAccionContableNuevo" runat="server" MaxLength="150" Enabled="False" SkinID="padrao-textbox"></asp:TextBox>
                                            <div id="dvEstadosAccionesContables" runat="server" class="ui-fieldset ui-fieldset-toggleable" style="width: auto; margin-top: 13px; margin-left: 5px;">
                                                <asp:Label ID="lblAccionContableAccionesPosibles" runat="server" align="center" Text="Estados Posibles del Documento"></asp:Label>
                                                <table border="1">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAccionContableEstadosPosibles" runat="server"
                                                                Text="Estados Posibles del Documento"></asp:Label>
                                                        </td>

                                                        <td style='padding: 10px 10px 10px 10px'>
                                                            <asp:Label ID="lblAccionContableDisponibleOrigen" runat="server" Text="Disponible en Origen"></asp:Label>
                                                        </td>
                                                        <td style='padding: 10px 10px 10px 10px'>
                                                            <asp:Label ID="lblAccionContableNoDisponibleOrigen" runat="server" Text="No Disponible en Origen"></asp:Label>
                                                        </td>
                                                          <td style='padding: 10px 10px 10px 10px'>
                                                            <asp:Label ID="lblAccionContableDisponibleBloqueadoOrigen" runat="server" Text="Disponible Bloqueado en Origen"></asp:Label>
                                                        </td>
                                                        <td style='padding: 10px 10px 10px 10px'>
                                                            <asp:Label ID="lblAccionContableDisponibleDestino" runat="server" Text="Disponible en Destino"></asp:Label>
                                                        </td>
                                                        <td style='padding: 10px 10px 10px 10px'>
                                                            <asp:Label ID="lblAccionContableNoDisponibleDestino" runat="server" Text="No Disponible en Destino"></asp:Label>
                                                        </td>
                                                        <td style='padding: 10px 10px 10px 10px'>
                                                            <asp:Label ID="lblAccionContableDisponibleBloqueadoDestino" runat="server" Text="Disponible Bloqueado en Destino"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAccionContableEstadoConfirmado" runat="server" Text="Confirmado"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoConfirmadoDisponibleOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoConfirmadoNoDisponibleOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoConfirmadoDisponibleBloqueadoOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoConfirmadoDisponibleDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoConfirmadoNoDisponibleDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                      
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoConfirmadoDisponibleBloqueadoDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAccionContableEstadoAceptado" runat="server" Text="Aceptado"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoAceptadoDisponibleOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoAceptadoNoDisponibleOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoAceptadoDisponibleBloqueadoOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoAceptadoDisponibleDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoAceptadoNoDisponibleDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoAceptadoDisponibleBloqueadoDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblAccionContableEstadoRechazado" runat="server" Text="Rechazado"></asp:Label>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoRechazadoDisponibleOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoRechazadoNoDisponibleOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoRechazadoDisponibleBloqueadoOrigen" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoRechazadoDisponibleDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoRechazadoNoDisponibleDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="ddlAccionContableEstadoRechazadoDisponibleBloqueadoDestino" SkinID="form-dropdownlist-mandatory"
                                                                runat="server" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div style="width: auto; margin-left: 5px; height: auto; background-color: #f7f7f7;">
                                        <asp:CheckBox ID="chkImprime" runat="server" Text="¿Se imprime?" AutoPostBack="True" />
                                        <br />
                                        <fieldset class="ui-fieldset ui-fieldset-toggleable" style="width: auto;">
                                            <asp:Label ID="lblImprimeCopias" runat="server" Text="Copias"></asp:Label>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtImprimeDestinoCopia" MaxLength="36" SkinID="filter-textbox" runat="server"
                                                            Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnImprimeDestinoAnadir" runat="server" SkinID="helper_button_Add"
                                                            Text="Añadir" Enabled="False" />
                                                    </td>
                                                    <td>
                                                        <asp:ListBox ID="lstImprimeDestinos" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all filterInput"
                                                            Enabled="False" Width="150px"></asp:ListBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgBtnImprimeDestinoQuitar" runat="server" SkinID="helper_button_Close"
                                                            Text="Quitar" Enabled="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                </td>
                                <td style="float: right; margin-top: 5px; width:30%  margin-right: 5px;">
                                    <div style="width: auto; height: auto;">
                                        <asp:CheckBox ID="chkCodigoExternoObligatorio" runat="server" Text="¿El Código Externo deberá ser obligatório al crear el Documento?" />
                                    </div>
                                    <div id="dvIntegracionSalidas" runat="server" visible="true" style="width: auto; height: auto; background-color: #f7f7f7;">
                                        <br />
                                        <asp:CheckBox ID="chkIntegracionSalidas" runat="server" Checked="false" Text="¿Utilizar este formulario para la integración con el módulo Salidas?"
                                            TabIndex="26" />
                                    </div>
                                    <div id="dvIntegracionRecepcionEnvio" runat="server" visible="true" style="width: auto; height: auto;">
                                        <br />
                                        <asp:CheckBox ID="chkIntegracionRecepcionEnvio" runat="server" Checked="false" Text="¿Utilizar este formulario para la integración con el módulo Recepción y Envío?"
                                            TabIndex="26" />
                                    </div>
                                    <div id="dvIntegracionLegado" runat="server" visible="true" style="width: auto; height: auto; background-color: #f7f7f7;">
                                        <br />
                                        <asp:CheckBox ID="chkIntegracionLegado" runat="server" Checked="false" Text="¿Utilizar este formulario para la integración con el Legado?"
                                            TabIndex="26" />
                                    </div>
                                    <div id="dvIntegracionConteo" runat="server" visible="true" style="width: auto; height: auto;">
                                        <br />
                                        <asp:CheckBox ID="chkIntegracionConteo" runat="server" Checked="false" Text="¿Utilizar este formulario para la integración con Módulo Conteo?"
                                            TabIndex="26" />
                                    </div>
                                    <div id="dvSolicitacionFondos" runat="server" visible="false" style="width: auto; height: auto; background-color: #f7f7f7;">
                                        <br />
                                        <asp:CheckBox ID="chkSolicitacionFondos" runat="server" AutoPostBack="true" Checked="false" Text="¿Utilizar ese formulario para Solicitación de Fondos?" TabIndex="26" />
                                    </div>
                                    <div id="dvContestarSolicitacionFondos" runat="server" visible="true" style="width: auto; height: auto;">
                                        <br />
                                        <asp:CheckBox ID="chkContestarSolicitacionFondos" runat="server" AutoPostBack="true" Checked="false" Text="¿Utilizar ese formulario para Contestar Solicitación de Fondos?" TabIndex="26" />
                                    </div>
                                    <div id="dvMovimientoAceptacionAutomatica" runat="server" visible="true" style="width: auto; height: auto;">
                                        <br />
                                        <asp:CheckBox ID="chkMovimientoAceptacionAutomatica" runat="server" Checked="false" Text="¿Este formulário es aceptado automáticamente en el sector de destino?" TabIndex="26" />
                                    </div>
                                    <div id="dvModificarTermino" runat="server" visible="true" style="width: auto; height: auto;">
                                        <br />
                                        <asp:CheckBox ID="chkModificarTermino" runat="server" Checked="false" Text="Modificar Términos de documentos válidos?" TabIndex="26" />
                                    </div>
                                    <div style="width: auto;">
                                        <br />
                                        <asp:Label ID="lblIAC" runat="server" SkinID="padrao-label" Text="IAC en el Documento"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:DropDownList ID="ddlIAC" runat="server" SkinID="padrao-dropdownlist">
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblIACGrupo" runat="server" SkinID="padrao-label" Text="IAC en el Documento"></asp:Label>
                                        <br />
                                        <br />
                                        <asp:DropDownList ID="ddlIACGrupo" runat="server" SkinID="padrao-dropdownlist">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </asp:WizardStep>
                <asp:WizardStep ID="ws7" runat="server" StepType="Finish" Title="Resumen de Visión">
                    <fieldset class="ui-fieldset ui-fieldset-toggleable">
                        <legend class="ui-fieldset-legend ui-corner-all ui-state-default">
                            <asp:Label ID="lblPaso7" Text="Paso 7" runat="server" />
                        </legend>
                        <h1>
                            <asp:Label ID="lblResumenIdentificacion" runat="server" Text=""></asp:Label></h1>
                        <asp:Label ID="lblResumenCodigo" runat="server" SkinID="padrao-label" Text=""></asp:Label>
                        <b>
                            <asp:Label ID="lblResumenCodigoValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblResumenDescripcion" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblResumenDescripcionValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblResumenColor" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblResumenColorValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblResumenTipoDocumento" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblResumenTipoDocumentoValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblResumenIndividual" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblResumenIndividualValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Panel ID="pnlWs7Grupo" runat="server" Visible="false">
                            <asp:Label ID="lblResumenGrupo" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenGrupoValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                            <br />
                        </asp:Panel>
                        <asp:Label ID="lblResumenActivo" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblResumenActivoValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblCodigoExternoObligatorio" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblCodigoExternoObligatorioValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                        <br />
                        <hr />
                        <h1>
                            <asp:Label ID="lblResumenObjectivo" runat="server" Text=""></asp:Label></h1>
                        <asp:Label ID="lblResumenCaracteristicas" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblResumenCaracteristicasValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                        <br />
                        <div id="fsResumenTiposBultos" runat="server">
                            <asp:Label ID="lblResumenTiposBultos" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblResumenTiposBultosValor" runat="server" SkinID="padrao-label" Text=""></asp:Label></b><br />
                            <br />
                        </div>
                        <hr />
                        <h1>
                            <asp:Label ID="lblResumenVisibilidadUso" runat="server" Text=""></asp:Label></h1>
                        <br />
                        <asp:Label ID="lblResumenTiposSectoresOrigen" runat="server" Text=""></asp:Label><br />
                        <asp:ListBox ID="lstReumenTiposSectoresOrigen" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all filterInput"
                            SelectionMode="Multiple" Rows="7" Width="530px"></asp:ListBox><br />
                        <div id="fsReumenTiposSectoresDestino" runat="server">
                            <br />
                            <asp:Label ID="lblResumenTiposSectoresDestino" runat="server" Text=""></asp:Label><br />
                            <asp:ListBox ID="lstReumenTiposSectoresDestino" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all filterInput"
                                SelectionMode="Multiple" Rows="7" Width="530px"></asp:ListBox>
                        </div>
                        <br />
                        <hr />
                        <h1>
                            <asp:Label ID="lblResumenOperacionesContablesOtrosDatos" runat="server" Text=""></asp:Label></h1>
                        <asp:Panel ID="pnlWs7AccionContable" runat="server" Width="100%" Visible="false">
                            <br />
                            <asp:Label ID="lblResumenAccionContable" runat="server" Text="Accion Contable"></asp:Label><b><asp:Label ID="lblResumenAccionContableValor" runat="server" Text=""></asp:Label></b><br />
                            <fieldset class="ui-fieldset ui-fieldset-toggleable" style="max-width: 510px;">
                                <table>
                                    <tr align="center">
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableEstado" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableDisponibleOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableNoDisponibleOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableDisponibleBloqueadoOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableDisponibleDestino" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableNoDisponibleDestino" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableDisponibleBloqueadoDestino" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr align="center" style="background-color: Silver;">

                                        <td style='padding: 10px 10px 10px 10px'>
                                            <asp:Label ID="lblResumenAccionContableConfirmado" runat="server" Text="Confirmado"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableConfirmadoDisponibleOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableConfirmadoNoDisponibleOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableConfirmadoDisponibleBloqueadoOrigen" runat="server" Text=""></asp:Label></td>
                                         <td>
                                            <asp:Label ID="lblResumenAccionContableConfirmadoDisponibleDestino" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableConfirmadoNoDisponibleDestino" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableConfirmadoDisponibleBloqueadoDestino" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr align="center">

                                        <td style='padding: 10px 10px 10px 10px'>
                                            <asp:Label ID="lblResumenAccionContableAceptado" runat="server" Text="Aceptado"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableAceptadoDisponibleOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableAceptadoNoDisponibleOrigen" runat="server" Text=""></asp:Label></td>
                                         <td>
                                           <asp:Label ID="lblResumenAccionContableAceptadoDisponibleBloqueadoOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableAceptadoDisponibleDestino" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableAceptadoNoDisponibleDestino" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableAceptadoDisponibleBloqueadoDestino" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr align="center" style="background-color: Silver;">

                                        <td style='padding: 10px 10px 10px 10px'>
                                            <asp:Label ID="lblResumenAccionContableRechazado" runat="server" Text="Rechazado"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableRechazadoDisponibleOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableRechazadoNoDisponibleOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                           <asp:Label ID="lblResumenAccionContableRechazadoDisponibleBloqueadoOrigen" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableRechazadoDisponibleDestino" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableRechazadoNoDisponibleDestino" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblResumenAccionContableRechazadoDisponibleBloqueadoDestino" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                        <div id="dvResumenPermiteLlegarSaldoNegativo" runat="server">
                            <asp:Label ID="lblResumenPermiteLlegarSaldoNegativo" SkinID="padrao-label" runat="server" Text=""></asp:Label>
                            <b>
                                <asp:Label ID="lblResumenPermiteLlegarSaldoNegativoValor" SkinID="padrao-label" runat="server" Text=""></asp:Label>

                            </b>
                            <br />
                            <br />
                        </div>
                        <div id="dvResumenExcluirSectoresHijos" runat="server">
                            <asp:Label ID="lblResumenExcluirSectoresHijos" SkinID="padrao-label" runat="server" Text=""></asp:Label>
                            <b>
                                <asp:Label ID="lblResumenExcluirSectoresHijosValor" SkinID="padrao-label" runat="server" Text=""></asp:Label>

                            </b>
                            <br />
                            <br />
                        </div>
                        <div id="dvResumenPackModular" runat="server">
                            <asp:Label ID="lblResumenPackModular" SkinID="padrao-label" runat="server" Text=""></asp:Label>
                            <b>
                                <asp:Label ID="lblResumenPackModularValor" SkinID="padrao-label" runat="server" Text=""></asp:Label>

                            </b>
                            <br />
                            <br />
                        </div>
                        <asp:Label ID="lblResumenImprime" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenImprimeValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                        <br />
                        <div id="dvResumenImprimeCopias" runat="server">
                            <asp:Label ID="lblResumenImprimeCopias" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenImprimeCopiasValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                            <br />
                        </div>
                        <asp:Label ID="lblResumenIAC" runat="server" SkinID="padrao-label" Text=""></asp:Label><b><asp:Label ID="lblResumenIACValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Panel ID="pnlWs7IACGrupo" runat="server" Visible="false">
                            <asp:Label ID="lblResumenIACGrupo" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenIACGrupoValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                            <br />
                        </asp:Panel>
                        <asp:Label ID="lblResumenIntegracionSalidas" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenIntegracionSalidasValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblResumenIntegracionRecepcion" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenIntegracionRecepcionValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblResumenIntegracionLegado" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenIntegracionLegadoValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblResumenIntegracionConteo" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenIntegracionConteoValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                        <br />
                        <asp:Label ID="lblResumenModificarTermino" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenModificarTerminoValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />                        
                        <br />
                        <asp:Panel ID="pnlWs7SolicitacionFondos" runat="server">
                            <asp:Label ID="lblResumenSolicitacionFondos" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenSolicitacionFondosValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                            <br />
                        </asp:Panel>
                        <asp:Panel ID="pnlWs7ContestarSolicitacionFondos" runat="server">
                            <asp:Label ID="lblResumenContestarSolicitacionFondos" SkinID="padrao-label" runat="server" Text=""></asp:Label><b><asp:Label ID="lblResumenContestarSolicitacionFondosValor" SkinID="padrao-label" runat="server" Text=""></asp:Label></b><br />
                        </asp:Panel>
                </asp:WizardStep>
            </WizardSteps>
        </asp:Wizard>
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>
