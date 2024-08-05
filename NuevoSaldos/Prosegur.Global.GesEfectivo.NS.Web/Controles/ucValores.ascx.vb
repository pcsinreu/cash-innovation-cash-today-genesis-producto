Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HelperDocumento
Imports System.Collections.ObjectModel

Public Class ucValores
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Divisas As New ObservableCollection(Of Clases.Divisa)
    Public Property Modo() As Enumeradores.Modo
    Public Property Titulo As String

    Public Property TipoValor As Comon.Enumeradores.TipoValor

    Private _ValoresDivisas As UCValoresDivisas
    Public ReadOnly Property ValoresDivisas() As UCValoresDivisas
        Get
            If _ValoresDivisas Is Nothing Then
                _ValoresDivisas = LoadControl("~\Controles\UCValoresDivisas.ascx")
                _ValoresDivisas.ID = "ValoresDivisas"
                _ValoresDivisas.Modo = Modo
                _ValoresDivisas.TipoValor = TipoValor
                _ValoresDivisas.ExhibirTitulo = False
                _ValoresDivisas.TrabajarConCalidad = True
                AddHandler _ValoresDivisas.Erro, AddressOf ErroControles
            End If
            Return _ValoresDivisas
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()
        ConfigurarControles()
    End Sub

    ''' <summary>
    ''' Método que valida os campos obrigatorios do controle.
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ValidarControl() As List(Of String)
        Dim retorno As New List(Of String)

        If ValoresDivisas.Divisas Is Nothing OrElse ValoresDivisas.Divisas.Count < 1 Then
            retorno.Add(Traduzir("043_divisa_obrigatorio"))
        End If

        Return retorno
    End Function

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        lblTitulo.Text = Titulo
    End Sub

#End Region

#Region "[EVENTOS]"

#End Region

#Region "[DELEGATE]"

    Public Class DetallarElementoEventArgs
        Public Property Elemento As Clases.Elemento
        Public Property identificadorContenedor As String
        Public Property identificadorRemesa As String
        Public Property identificadorBulto As String
        Public Property identificadorParcial As String
    End Class

    Public Event DetallarElemento(sender As Object, e As DetallarElementoEventArgs)

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

    Private Sub ConfigurarControles()

        Aplicacao.Util.Utilidad.EliminarDatosSinValores(Divisas)

        If Divisas IsNot Nothing Then
            ConfigurarDivisas()
        End If

    End Sub

    Private Sub ConfigurarDivisas()
        Dim objDivisasDenominaciones = Divisas.Where(Function(d) d.Denominaciones IsNot Nothing AndAlso d.Denominaciones.Count > 0)
        Dim objDivisasMediosPago = Divisas.Where(Function(d) d.MediosPago IsNot Nothing AndAlso d.MediosPago.Count > 0)
        Dim objTotalesEfectivo = Divisas.Where(Function(d) d.ValoresTotalesEfectivo IsNot Nothing AndAlso d.ValoresTotalesEfectivo.Count > 0)
        Dim objTotalesTipoMedioPago = Divisas.Where(Function(d) d.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso d.ValoresTotalesTipoMedioPago.Count > 0)
        If Modo <> Enumeradores.Modo.Consulta OrElse (Modo = Enumeradores.Modo.Consulta AndAlso (
                                                      (objDivisasDenominaciones IsNot Nothing AndAlso objDivisasDenominaciones.Count > 0) OrElse
                                                      (objDivisasMediosPago IsNot Nothing AndAlso objDivisasMediosPago.Count > 0) OrElse
                                                      (objTotalesEfectivo IsNot Nothing AndAlso objTotalesEfectivo.Count > 0) OrElse
                                                      (objTotalesTipoMedioPago IsNot Nothing AndAlso objTotalesTipoMedioPago.Count > 0))) Then
            ValoresDivisas.Divisas = Divisas
            phUCValoresDivisas.Controls.Add(ValoresDivisas)
        End If
    End Sub

    Public Function ActualizaDivisas() As ObservableCollection(Of Clases.Divisa)

        Return ValoresDivisas.Divisas

    End Function

#End Region

End Class