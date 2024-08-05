Imports System.Xml.Serialization
Imports System.Xml

Namespace IngresoRemesasNuevo

    <Serializable()> _
    Public Class ValorRemesa

#Region "[VARIÁVEIS]"

        Private _CodTerminoIac As String
        Private _DesValorTerminoIac As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodTerminoIac() As String
            Get
                Return _CodTerminoIac
            End Get
            Set(value As String)
                _CodTerminoIac = value
            End Set
        End Property

        Public Property DesValorTerminoIac() As String
            Get
                Return _DesValorTerminoIac
            End Get
            Set(value As String)
                _DesValorTerminoIac = value
            End Set
        End Property

#End Region

    End Class
End Namespace