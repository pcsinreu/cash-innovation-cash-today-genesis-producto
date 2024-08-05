Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetDelegacionDetail
    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDelegacionDetail")> _
    <XmlRoot(Namespace:="urn:GetDelegacionDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Delegacion As DelegacionColeccion
#End Region

#Region "Propriedades"

        Public Property Delegacion() As DelegacionColeccion
            Get
                Return _Delegacion
            End Get
            Set(value As DelegacionColeccion)
                _Delegacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace

