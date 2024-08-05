Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguracion

    <Serializable()> _
    Public Class Declarado

        Private _decimales As Integer?
        Private _campoImporte As Campo
        Private _campoDetalhe As Campo
        Private _mapeoDeclaradoxFila As MapeoDeclaradoxFilaColeccion
        Private _simbologia As Nullable(Of eSimbologia)

        Public Property CampoDetalhe() As Campo
            Get
                Return _campoDetalhe
            End Get
            Set(value As Campo)
                _campoDetalhe = value
            End Set
        End Property

        Public Property Simbologia() As Nullable(Of eSimbologia)

            Get
                Return _simbologia
            End Get
            Set(value As Nullable(Of eSimbologia))
                _simbologia = value
            End Set
        End Property



        Public Property MapeoDeclaradoxFila() As MapeoDeclaradoxFilaColeccion
            Get
                Return _mapeoDeclaradoxFila
            End Get
            Set(value As MapeoDeclaradoxFilaColeccion)
                _mapeoDeclaradoxFila = value
            End Set
        End Property

        Public Property CampoImporte() As Campo
            Get
                Return _campoImporte
            End Get
            Set(value As Campo)
                _campoImporte = value
            End Set
        End Property

        Public Property Decimales() As Integer?
            Get
                Return _decimales
            End Get
            Set(value As Integer?)
                _decimales = value
            End Set
        End Property

    End Class

End Namespace
