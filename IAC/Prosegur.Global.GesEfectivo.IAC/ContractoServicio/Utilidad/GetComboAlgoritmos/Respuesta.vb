Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboAlgoritmos

    ''' <summary>
    ''' Classe respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetComboAlgoritmos")> _
    <XmlRoot(Namespace:="urn:GetComboAlgoritmos")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _Algoritmos As AlgoritmoColeccion

        Public Property Algoritmos() As AlgoritmoColeccion
            Get
                Return _Algoritmos
            End Get
            Set(value As AlgoritmoColeccion)
                _Algoritmos = value
            End Set
        End Property

    End Class

End Namespace