Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio objecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    <Serializable()> _
    Public Class Objecto
        Inherits BaseEntidade

#Region "[Variáveis]"

        Private _oidComponenteObjeto As String
        Private _codIsoDivisa As String
        Private _desDivisa As String
        Private _bolEfectivo As Boolean
        Private _tiposMedioPago As List(Of TipoMedioPago)
        Private _denominacion As Denominacion
        Private _ordenDivisa As Integer
        Private _OrdenEfectivo As Integer

#End Region

#Region "[Propriedades]"

        Public Property OidComponenteObjeto() As String
            Get
                Return _oidComponenteObjeto
            End Get
            Set(value As String)
                _oidComponenteObjeto = value
            End Set
        End Property

        Public Property TiposMedioPago() As List(Of TipoMedioPago)
            Get
                If _tiposMedioPago Is Nothing Then
                    _tiposMedioPago = New List(Of TipoMedioPago)
                End If
                Return _tiposMedioPago
            End Get
            Set(value As List(Of TipoMedioPago))
                _tiposMedioPago = value
            End Set
        End Property

        Public Property CodIsoDivisa() As String
            Get
                Return _codIsoDivisa
            End Get
            Set(value As String)
                _codIsoDivisa = value
            End Set
        End Property

        Public Property DesDivisa() As String
            Get
                Return _desDivisa
            End Get
            Set(value As String)
                _desDivisa = value
            End Set
        End Property

        Public ReadOnly Property CodDenominacion() As String
            Get
                If _denominacion Is Nothing Then
                    Return String.Empty
                Else
                    Return _denominacion.CodDenominacion
                End If
            End Get
        End Property

        Public Property BolEfectivo() As Boolean
            Get
                Return _bolEfectivo
            End Get
            Set(value As Boolean)
                _bolEfectivo = value
            End Set
        End Property

        Public Property Denominacion As Denominacion
            Get
                Return _denominacion
            End Get
            Set(value As Denominacion)
                _denominacion = value
            End Set
        End Property

        Public Property OrdenDivisa As Integer
            Get
                Return _ordenDivisa
            End Get
            Set(value As Integer)
                _ordenDivisa = value
            End Set
        End Property

        Public Property OrdenEfectivo As Integer
            Get
                Return _OrdenEfectivo
            End Get
            Set(value As Integer)
                _OrdenEfectivo = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(Obj As Integracion.ContractoServicio.GetMorfologiaDetail.Objecto)

            _oidComponenteObjeto = Obj.OidComponenteObjeto
            _codIsoDivisa = Obj.CodIsoDivisa
            _desDivisa = Obj.DesDivisa
            _bolEfectivo = Obj.BolEfectivo
            _ordenDivisa = Obj.NecOrdenDivisa
            _tiposMedioPago = New List(Of TipoMedioPago)

            If Obj.BolEfectivo Then
                _OrdenEfectivo = Obj.NecOrdenTipoMedPago
            End If

            ' preenche denominação
            _denominacion = New Denominacion(Obj.Denominacion)

            ' preenche tipos de médios pago
            Dim tmp As TipoMedioPago
            For Each item In Obj.TiposMedioPago
                tmp = New TipoMedioPago(item, Obj.NecOrdenTipoMedPago)
                _tiposMedioPago.Add(tmp)
            Next

        End Sub

#End Region

    End Class

End Namespace