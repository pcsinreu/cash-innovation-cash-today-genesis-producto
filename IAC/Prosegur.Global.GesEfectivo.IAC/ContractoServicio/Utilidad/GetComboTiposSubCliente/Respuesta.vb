Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposSubCliente

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboTiposSubCliente")> _
    <XmlRoot(Namespace:="urn:GetComboTiposSubCliente")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[Variáveis]"

        Private _TiposSubCliente As TipoSubClienteColeccion

#End Region

#Region "[Propriedades]"

        Public Property TiposSubCliente() As TipoSubClienteColeccion
            Get
                Return _TiposSubCliente
            End Get
            Set(value As TipoSubClienteColeccion)
                _TiposSubCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace