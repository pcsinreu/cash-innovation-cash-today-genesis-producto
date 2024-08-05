Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.SetMedioPago

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetMedioPago")> _
    <XmlRoot(Namespace:="urn:SetMedioPago")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _MedioPagos As MedioPagoColeccion
        Private _CodigoUsuario As String

#End Region

#Region "[Propriedades]"

        Public Property MedioPagos() As MedioPagoColeccion
            Get
                Return _MedioPagos
            End Get
            Set(value As MedioPagoColeccion)
                _MedioPagos = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace