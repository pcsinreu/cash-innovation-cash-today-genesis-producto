Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.VerificarCodigoTerminoMedioPago

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 27/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoTerminoMedioPago")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoTerminoMedioPago")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Codigo As String
        Private _CodigoMedioPago As String

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property CodigoMedioPago() As String
            Get
                Return _CodigoMedioPago
            End Get
            Set(value As String)
                _CodigoMedioPago = value
            End Set
        End Property

#End Region

    End Class

End Namespace