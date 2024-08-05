<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterModal.Master" CodeBehind="BusquedaDatosBancariosPopupComparativo.aspx.vb" Inherits="Prosegur.Global.GesEfectivo.IAC.Web.BusquedaDatosBancariosPopupComparativo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        td {
            padding-bottom: 5px;
        }

        td:first-child {
            padding-right: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 10px;">
        <div style="float: left; padding: 0 20px 0 20px; border-right: 1px solid #ccc;">
            <asp:Label ID="lblDatosBancariosActuales" Style="font-size: small" runat="server"></asp:Label>
            <table style="padding-top: 5px;">
                <tr>
                    <td>
                        <asp:Label ID="lblBanco" runat="server" CssClass=""></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtBanco" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCodigoBancario" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCodigoBancario" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNroDocumento" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtNroDocumento" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAgencia" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtAgencia" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDivisa" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtDivisa" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblObs" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtObs" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDefecto" runat="server" CssClass="label2"></asp:Label></td>
                    <td style="margin:2px 2px 2px 2px !important;">
                        <asp:CheckBox  Enabled="false" ID="chkDefecto" runat="server"></asp:CheckBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTitularidad" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtTitularidad" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCuenta" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCuenta" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipo" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtTipo" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional1" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCampoAdicional1" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional2" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCampoAdicional2" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional3" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCampoAdicional3" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional4" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCampoAdicional4" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional5" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCampoAdicional5" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional6" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCampoAdicional6" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional7" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCampoAdicional7" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional8" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" Style="background-color:gainsboro" ID="txtCampoAdicional8" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div style="float: left; padding: 0 20px 0 20px;">
            <asp:Label ID="lblDatosBancariosPendientes" Style="font-size: small" runat="server"></asp:Label>
            <table style="padding-top: 5px;">
                <tr>
                    <td>
                        <asp:Label ID="lblBancoCambio" runat="server" CssClass=""></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtBancoCambio" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCodigoBancarioCambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCodigoBancarioCambio" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNroDocumentoCambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtNroDocumentoCambio" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAgenciaCambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtAgenciaCambio" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDivisaCambio" runat="server" CssClass="label2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtDivisaCambio" runat="server" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblObsCambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtObsCambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDefectoCambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:CheckBox Enabled="false" ID="chkDefectoCambio" runat="server"></asp:CheckBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTitularidadCambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtTitularidadCambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCuentaCambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCuentaCambio" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoCambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtTipoCambio" runat="server" MaxLength="50" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional1Cambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCampoAdicional1Cambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional2Cambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCampoAdicional2Cambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional3Cambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCampoAdicional3Cambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional4Cambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCampoAdicional4Cambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional5Cambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCampoAdicional5Cambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional6Cambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCampoAdicional6Cambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional7Cambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCampoAdicional7Cambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCampoAdicional8Cambio" runat="server" CssClass="label2"></asp:Label></td>
                    <td>
                        <asp:TextBox ReadOnly="true" ID="txtCampoAdicional8Cambio" runat="server" MaxLength="200" CssClass="ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"></asp:TextBox></td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBotonesRodapie" runat="server">
</asp:Content>
