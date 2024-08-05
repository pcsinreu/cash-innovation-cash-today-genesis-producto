Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboPais

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/02/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboPais")> _
    <XmlRoot(Namespace:="urn:GetComboPais")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _Pais As PaisColeccion

        Public Property Pais() As PaisColeccion
            Get
                Return _Pais
            End Get
            Set(value As PaisColeccion)
                _Pais = value
            End Set
        End Property

    End Class

End Namespace

