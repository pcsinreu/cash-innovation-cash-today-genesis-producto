Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.VerificarCodigoMedioPago

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoMedioPago")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoMedioPago")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Codigo As String
        private _Tipo As String
        private _Divisa As String

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

        Public Property Tipo() As String
            Get
                Return _Tipo
            End Get
            Set(value As String)
                _Tipo = value
            End Set
        End Property

        Public Property Divisa() As String
            Get
                Return _Divisa
            End Get
            Set(value As String)
                _Divisa = value
            End Set
        End Property

#End Region

    End Class

End Namespace