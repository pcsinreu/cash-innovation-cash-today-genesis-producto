Imports System.Xml.Serialization
Imports System.Xml

Namespace MedioPago.VerificarCodigoMedioPago

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoMedioPago")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoMedioPago")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _Existe As Boolean

#End Region

#Region "[Propriedades]"

        Public Property Existe() As Boolean
            Get
                Return _Existe
            End Get
            Set(value As Boolean)
                _Existe = value
            End Set
        End Property

#End Region

    End Class

End Namespace