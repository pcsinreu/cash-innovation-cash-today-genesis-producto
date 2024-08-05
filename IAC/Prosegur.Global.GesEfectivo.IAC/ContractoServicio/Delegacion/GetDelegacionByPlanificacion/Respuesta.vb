Imports System.Xml.Serialization
Imports System.Xml
Imports Prosegur.Genesis

Namespace Delegacion.GetDelegacionByPlanificacion

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 06/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetDelegacionByPlanificacion")>
    <XmlRoot(Namespace:="urn:GetDelegacionByPlanificacion")>
    <Serializable()>
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "Variáveis"

        Private _Delegacion As Comon.Clases.Delegacion
        Private _Resultado As String

#End Region

#Region "Propriedades"

        Public Property Delegacion() As Comon.Clases.Delegacion
            Get
                Return _Delegacion
            End Get
            Set(value As Comon.Clases.Delegacion)
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

