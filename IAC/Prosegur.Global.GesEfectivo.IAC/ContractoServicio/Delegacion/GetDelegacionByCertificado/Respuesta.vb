Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetDelegacionByCertificado

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 06/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDelegacionByCertificado")> _
    <XmlRoot(Namespace:="urn:GetDelegacionByCertificado")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Delegaciones As DelegacionColeccion
        Private _Resultado As String

#End Region

#Region "Propriedades"

        Public Property Delegaciones() As DelegacionColeccion
            Get
                Return _Delegaciones
            End Get
            Set(value As DelegacionColeccion)
                _Delegaciones = value
            End Set
        End Property

        Public Property Resultado() As String
            Get
                Return _Resultado
            End Get
            Set(value As String)
                _Resultado = value
            End Set
        End Property
#End Region

    End Class
End Namespace

