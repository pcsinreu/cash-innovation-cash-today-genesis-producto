Imports System.Xml.Serialization
Imports System.Xml

Namespace IngresoRemesasNuevo

    <Serializable()> _
    Public Class TerminoMedioPago

#Region "[VARIÁVEIS]"

        Private _CodigoTermino As String
        Private _CodigoValorTermino As String
        Private _DescripcionValorTermino As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoTermino() As String
            Get
                Return _CodigoTermino
            End Get
            Set(value As String)
                _CodigoTermino = value
            End Set
        End Property

        Public Property CodigoValorTermino() As String
            Get
                Return _CodigoValorTermino
            End Get
            Set(value As String)
                _CodigoValorTermino = value
            End Set
        End Property

        Public Property DescripcionValorTermino() As String
            Get
                Return _DescripcionValorTermino
            End Get
            Set(value As String)
                _DescripcionValorTermino = value
            End Set
        End Property

#End Region

    End Class

End Namespace