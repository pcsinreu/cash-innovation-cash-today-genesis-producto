Imports System.Drawing

Public Class BusquedaDatosBancariosPopupComparativo
    Inherits Base

#Region "[Propriedades]"
    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal")
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal")
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar")
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.APROBACION_CUENTAS_BANCARIAS
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False
    End Sub

    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then
                Dim identificador = Request.QueryString("identificador")
                Dim respuesta = New LogicaNegocio.AccionDatoBancario().GetDatoBancarioComparativo(identificador)

                'Veo si se trata de un datobancario existente en la base
                If respuesta.DatoBancarioOriginal IsNot Nothing Then
                    txtBanco.Text = respuesta.DatoBancarioOriginal.Banco.Descripcion
                    txtCodigoBancario.Text = respuesta.DatoBancarioOriginal.Banco.Codigo
                    txtNroDocumento.Text = respuesta.DatoBancarioOriginal.CodigoDocumento
                    txtAgencia.Text = respuesta.DatoBancarioOriginal.CodigoAgencia
                    txtDivisa.Text = respuesta.DatoBancarioOriginal.Divisa.Descripcion
                    txtObs.Text = respuesta.DatoBancarioOriginal.DescripcionObs
                    chkDefecto.Checked = IIf(respuesta.DatoBancarioOriginal.bolDefecto IsNot Nothing, respuesta.DatoBancarioOriginal.bolDefecto, False)
                    txtTitularidad.Text = respuesta.DatoBancarioOriginal.DescripcionTitularidad
                    txtCuenta.Text = respuesta.DatoBancarioOriginal.CodigoCuentaBancaria
                    txtTipo.Text = respuesta.DatoBancarioOriginal.CodigoTipoCuentaBancaria
                    txtCampoAdicional1.Text = respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo1
                    txtCampoAdicional2.Text = respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo2
                    txtCampoAdicional3.Text = respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo3
                    txtCampoAdicional4.Text = respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo4
                    txtCampoAdicional5.Text = respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo5
                    txtCampoAdicional6.Text = respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo6
                    txtCampoAdicional7.Text = respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo7
                    txtCampoAdicional8.Text = respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo8

                    'Se trata de un dato bancario en base de datos con cambios pendientes en base de datos
                    If respuesta.DatoBancarioCambio IsNot Nothing Then
                        AsignarTexto(txtBancoCambio, respuesta.DatoBancarioOriginal.Banco.Descripcion, respuesta.DatoBancarioCambio.Banco.Descripcion)
                        AsignarTexto(txtCodigoBancarioCambio, respuesta.DatoBancarioOriginal.Banco.Codigo, respuesta.DatoBancarioCambio.Banco.Codigo)
                        AsignarTexto(txtNroDocumentoCambio, respuesta.DatoBancarioOriginal.CodigoDocumento, respuesta.DatoBancarioCambio.CodigoDocumento)
                        AsignarTexto(txtAgenciaCambio, respuesta.DatoBancarioOriginal.CodigoAgencia, respuesta.DatoBancarioCambio.CodigoAgencia)
                        AsignarTexto(txtDivisaCambio, respuesta.DatoBancarioOriginal.Divisa.Descripcion, respuesta.DatoBancarioCambio.Divisa.Descripcion)
                        AsignarTexto(txtObsCambio, respuesta.DatoBancarioOriginal.DescripcionObs, respuesta.DatoBancarioCambio.DescripcionObs)
                        AsignarTexto(txtTitularidadCambio, respuesta.DatoBancarioOriginal.DescripcionTitularidad, respuesta.DatoBancarioCambio.DescripcionTitularidad)
                        AsignarTexto(txtCuentaCambio, respuesta.DatoBancarioOriginal.CodigoCuentaBancaria, respuesta.DatoBancarioCambio.CodigoCuentaBancaria)
                        AsignarTexto(txtTipoCambio, respuesta.DatoBancarioOriginal.CodigoTipoCuentaBancaria, respuesta.DatoBancarioCambio.CodigoTipoCuentaBancaria)
                        AsignarTexto(txtCampoAdicional1Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo1, respuesta.DatoBancarioCambio.DescripcionAdicionalCampo1)
                        AsignarTexto(txtCampoAdicional2Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo2, respuesta.DatoBancarioCambio.DescripcionAdicionalCampo2)
                        AsignarTexto(txtCampoAdicional3Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo3, respuesta.DatoBancarioCambio.DescripcionAdicionalCampo3)
                        AsignarTexto(txtCampoAdicional4Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo4, respuesta.DatoBancarioCambio.DescripcionAdicionalCampo4)
                        AsignarTexto(txtCampoAdicional5Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo5, respuesta.DatoBancarioCambio.DescripcionAdicionalCampo5)
                        AsignarTexto(txtCampoAdicional6Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo6, respuesta.DatoBancarioCambio.DescripcionAdicionalCampo6)
                        AsignarTexto(txtCampoAdicional7Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo7, respuesta.DatoBancarioCambio.DescripcionAdicionalCampo7)
                        AsignarTexto(txtCampoAdicional8Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo8, respuesta.DatoBancarioCambio.DescripcionAdicionalCampo8)
                        AsignarChecked(chkDefectoCambio, respuesta.DatoBancarioOriginal.bolDefecto, respuesta.DatoBancarioCambio.bolDefecto)
                    Else
                        'Se trata de un dato bancario en base de datos sin cambios pendientes en base de datos
                        AsignarTexto(txtBancoCambio, respuesta.DatoBancarioOriginal.Banco.Descripcion, Session("Dto_Banc_Com_Banco"))
                        AsignarTexto(txtCodigoBancarioCambio, respuesta.DatoBancarioOriginal.Banco.Codigo, Session("Dto_Banc_Com_CodigoBancario"))
                        AsignarTexto(txtNroDocumentoCambio, respuesta.DatoBancarioOriginal.CodigoDocumento, Session("Dto_Banc_Com_NroDocumento"))
                        AsignarTexto(txtAgenciaCambio, respuesta.DatoBancarioOriginal.CodigoAgencia, Session("Dto_Banc_Com_Agencia"))
                        AsignarTexto(txtDivisaCambio, respuesta.DatoBancarioOriginal.Divisa.Descripcion, Session("Dto_Banc_Com_Divisa"))
                        AsignarTexto(txtObsCambio, respuesta.DatoBancarioOriginal.DescripcionObs, Session("Dto_Banc_Com_Obs"))
                        AsignarTexto(txtTitularidadCambio, respuesta.DatoBancarioOriginal.DescripcionTitularidad, Session("Dto_Banc_Com_Titularidad"))
                        AsignarTexto(txtCuentaCambio, respuesta.DatoBancarioOriginal.CodigoCuentaBancaria, Session("Dto_Banc_Com_Cuenta"))
                        AsignarTexto(txtTipoCambio, respuesta.DatoBancarioOriginal.CodigoTipoCuentaBancaria, Session("Dto_Banc_Com_Tipo"))
                        AsignarTexto(txtCampoAdicional1Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo1, Session("Dto_Banc_Com_CampoAdicional1"))
                        AsignarTexto(txtCampoAdicional2Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo2, Session("Dto_Banc_Com_CampoAdicional2"))
                        AsignarTexto(txtCampoAdicional3Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo3, Session("Dto_Banc_Com_CampoAdicional3"))
                        AsignarTexto(txtCampoAdicional4Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo4, Session("Dto_Banc_Com_CampoAdicional4"))
                        AsignarTexto(txtCampoAdicional5Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo5, Session("Dto_Banc_Com_CampoAdicional5"))
                        AsignarTexto(txtCampoAdicional6Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo6, Session("Dto_Banc_Com_CampoAdicional6"))
                        AsignarTexto(txtCampoAdicional7Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo7, Session("Dto_Banc_Com_CampoAdicional7"))
                        AsignarTexto(txtCampoAdicional8Cambio, respuesta.DatoBancarioOriginal.DescripcionAdicionalCampo8, Session("Dto_Banc_Com_CampoAdicional8"))
                        AsignarChecked(chkDefectoCambio, respuesta.DatoBancarioOriginal.bolDefecto, Session("Dto_Banc_Com_Defecto"))
                    End If

                Else
                    'Se trata de un dato bancario nuevo que no se encuentra en la base de datos
                    AsignarTexto(txtBancoCambio, String.Empty, Session("Dto_Banc_Com_Banco"))
                    AsignarTexto(txtCodigoBancarioCambio, String.Empty, Session("Dto_Banc_Com_CodigoBancario"))
                    AsignarTexto(txtNroDocumentoCambio, String.Empty, Session("Dto_Banc_Com_NroDocumento"))
                    AsignarTexto(txtAgenciaCambio, String.Empty, Session("Dto_Banc_Com_Agencia"))
                    AsignarTexto(txtDivisaCambio, String.Empty, Session("Dto_Banc_Com_Divisa"))
                    AsignarTexto(txtObsCambio, String.Empty, Session("Dto_Banc_Com_Obs"))
                    AsignarTexto(txtTitularidadCambio, String.Empty, Session("Dto_Banc_Com_Titularidad"))
                    AsignarTexto(txtCuentaCambio, String.Empty, Session("Dto_Banc_Com_Cuenta"))
                    AsignarTexto(txtTipoCambio, String.Empty, Session("Dto_Banc_Com_Tipo"))
                    AsignarTexto(txtCampoAdicional1Cambio, String.Empty, Session("Dto_Banc_Com_CampoAdicional1"))
                    AsignarTexto(txtCampoAdicional2Cambio, String.Empty, Session("Dto_Banc_Com_CampoAdicional2"))
                    AsignarTexto(txtCampoAdicional3Cambio, String.Empty, Session("Dto_Banc_Com_CampoAdicional3"))
                    AsignarTexto(txtCampoAdicional4Cambio, String.Empty, Session("Dto_Banc_Com_CampoAdicional4"))
                    AsignarTexto(txtCampoAdicional5Cambio, String.Empty, Session("Dto_Banc_Com_CampoAdicional5"))
                    AsignarTexto(txtCampoAdicional6Cambio, String.Empty, Session("Dto_Banc_Com_CampoAdicional6"))
                    AsignarTexto(txtCampoAdicional7Cambio, String.Empty, Session("Dto_Banc_Com_CampoAdicional7"))
                    AsignarTexto(txtCampoAdicional8Cambio, String.Empty, Session("Dto_Banc_Com_CampoAdicional8"))
                    AsignarChecked(chkDefectoCambio, Nothing, Session("Dto_Banc_Com_Defecto"))

                End If
            End If

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub


    Protected Overrides Sub TraduzirControles()
        MyBase.CodFuncionalidad = "UCDATOSBANCARIOS"
        MyBase.CarregaDicinario()
        Me.Page.Title = MyBase.RecuperarValorDic("087_lbl_titulo")
        lblDatosBancariosActuales.Text = MyBase.RecuperarValorDic("lblDatosBancariosActuales")
        lblBanco.Text = MyBase.RecuperarValorDic("lblBanco")
        lblCodigoBancario.Text = MyBase.RecuperarValorDic("lbl_codigo_bancario")
        lblDefecto.Text = MyBase.RecuperarValorDic("lbl_patron")
        lblNroDocumento.Text = MyBase.RecuperarValorDic("lbl_numero_documento")
        lblTitularidad.Text = MyBase.RecuperarValorDic("lbl_titularidad")
        lblAgencia.Text = MyBase.RecuperarValorDic("lbl_agencia")
        lblCuenta.Text = MyBase.RecuperarValorDic("lbl_numero_cuenta")
        lblDivisa.Text = MyBase.RecuperarValorDic("lbl_divisa")
        lblTipo.Text = MyBase.RecuperarValorDic("lbl_tipo")
        lblObs.Text = MyBase.RecuperarValorDic("lbl_observaciones")
        lblCampoAdicional1.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_1")
        lblCampoAdicional2.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_2")
        lblCampoAdicional3.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_3")
        lblCampoAdicional4.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_4")
        lblCampoAdicional5.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_5")
        lblCampoAdicional6.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_6")
        lblCampoAdicional7.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_7")
        lblCampoAdicional8.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_8")


        lblDatosBancariosPendientes.Text = MyBase.RecuperarValorDic("lblDatosBancariosPendientes")
        lblBancoCambio.Text = MyBase.RecuperarValorDic("lblBanco")
        lblCodigoBancarioCambio.Text = MyBase.RecuperarValorDic("lbl_codigo_bancario")
        lblDefectoCambio.Text = MyBase.RecuperarValorDic("lbl_patron")
        lblNroDocumentoCambio.Text = MyBase.RecuperarValorDic("lbl_numero_documento")
        lblTitularidadCambio.Text = MyBase.RecuperarValorDic("lbl_titularidad")
        lblAgenciaCambio.Text = MyBase.RecuperarValorDic("lbl_agencia")
        lblCuentaCambio.Text = MyBase.RecuperarValorDic("lbl_numero_cuenta")
        lblDivisaCambio.Text = MyBase.RecuperarValorDic("lbl_divisa")
        lblTipoCambio.Text = MyBase.RecuperarValorDic("lbl_tipo")
        lblObsCambio.Text = MyBase.RecuperarValorDic("lbl_observaciones")
        lblCampoAdicional1Cambio.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_1")
        lblCampoAdicional2Cambio.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_2")
        lblCampoAdicional3Cambio.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_3")
        lblCampoAdicional4Cambio.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_4")
        lblCampoAdicional5Cambio.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_5")
        lblCampoAdicional6Cambio.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_6")
        lblCampoAdicional7Cambio.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_7")
        lblCampoAdicional8Cambio.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_8")

    End Sub

#End Region
    Private Sub AsignarTexto(texto As TextBox, valorOriginal As String, valorModificado As String)
        If valorOriginal <> valorModificado And valorModificado IsNot Nothing Then
            texto.Text = valorModificado
            texto.BackColor = Color.LightPink
        Else
            texto.Text = valorOriginal
            texto.BackColor = Color.Gainsboro
        End If
    End Sub
    Private Sub AsignarChecked(check As CheckBox, valorOriginal As Boolean?, valorModificado As Boolean?)
        If (Not valorOriginal.HasValue AndAlso valorModificado.HasValue) OrElse (valorOriginal <> valorModificado And valorModificado.HasValue) Then
            check.Checked = valorModificado
            check.BackColor = Color.LightPink
        ElseIf valorOriginal.HasValue Then
            check.Checked = valorOriginal
            check.BackColor = Color.Gainsboro
        End If
    End Sub
End Class