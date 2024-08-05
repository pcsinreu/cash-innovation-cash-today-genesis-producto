'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class MantenimientoDenominaciones

    '''<summary>
    '''Control pnGeral.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnGeral As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control lblTituloDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTituloDenominacion As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblCodigoDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCodigoDenominacion As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control UpdatePanelCodigoDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelCodigoDenominacion As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control txtCodigoDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCodigoDenominacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control csvCodigoDenominacionExistente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents csvCodigoDenominacionExistente As Global.System.Web.UI.WebControls.CustomValidator

    '''<summary>
    '''Control csvCodigoDenominacionObrigatorio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents csvCodigoDenominacionObrigatorio As Global.System.Web.UI.WebControls.CustomValidator

    '''<summary>
    '''Control lblDescricaoDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblDescricaoDenominacion As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control UpdatePanelDescricao.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelDescricao As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control txtDescricaoDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDescricaoDenominacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control csvDescripcionDenominacionObrigatorio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents csvDescripcionDenominacionObrigatorio As Global.System.Web.UI.WebControls.CustomValidator

    '''<summary>
    '''Control lblValor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblValor As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control UpdatePanelValor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelValor As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control txtValorDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtValorDenominacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control csvValorDenominacionObrigatorio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents csvValorDenominacionObrigatorio As Global.System.Web.UI.WebControls.CustomValidator

    '''<summary>
    '''Control lblPeso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblPeso As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtPesoDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPesoDenominacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblCodigoAcceso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCodigoAcceso As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control UpdatePanelCodigoAcceso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelCodigoAcceso As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control txtCodigoAcceso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCodigoAcceso As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblIndicadorBilhete.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblIndicadorBilhete As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control chkIndicadorBilhete.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkIndicadorBilhete As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Control lblVigente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblVigente As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control chkVigente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkVigente As Global.System.Web.UI.WebControls.CheckBox

    '''<summary>
    '''Control lblCodigoAjeno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblCodigoAjeno As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtCodigoAjeno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCodigoAjeno As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lblDesCodigoAjeno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblDesCodigoAjeno As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtDesCodigoAjeno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDesCodigoAjeno As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAltaAjenoDenominacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAltaAjenoDenominacion As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control UpdatePanelAcaoPagina.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelAcaoPagina As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control btnGrabar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGrabar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnConsomeCodigoAjeno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnConsomeCodigoAjeno As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Propiedad Master.
    '''</summary>
    '''<remarks>
    '''Propiedad generada automáticamente.
    '''</remarks>
    Public Shadows ReadOnly Property Master() As Prosegur.[Global].GesEfectivo.IAC.Web.Master.MasterModal
        Get
            Return CType(MyBase.Master, Prosegur.[Global].GesEfectivo.IAC.Web.Master.MasterModal)
        End Get
    End Property
End Class
