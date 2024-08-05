Imports System.Xml.Serialization
Imports System.Xml

Namespace Delegacion.GetDelegacion

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 06/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDelegacion")> _
    <XmlRoot(Namespace:="urn:GetDelegacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

#Region "Variáveis"

        Private _Delegacion As DelegacionColeccion
        Private _Resultado As String

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

