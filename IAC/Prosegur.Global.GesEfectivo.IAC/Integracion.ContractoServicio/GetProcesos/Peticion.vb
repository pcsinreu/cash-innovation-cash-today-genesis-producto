Imports System.Xml.Serialization
Imports System.Xml

Namespace GetProcesos

    <XmlType(Namespace:="urn:GetProcesos")> _
    <XmlRoot(Namespace:="urn:GetProcesos")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _fechaInicial As DateTime
        Private _fechaFinal As DateTime
        Private _vigente As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property FechaInicial() As DateTime
            Get
                Return _fechaInicial
            End Get
            Set(value As DateTime)
                _fechaInicial = value
            End Set
        End Property

        Public Property FechaFinal() As DateTime
            Get
                Return _fechaFinal
            End Get
            Set(value As DateTime)
                _fechaFinal = value
            End Set
        End Property

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _vigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace
