Imports System.Xml.Serialization
Imports System.Xml

Namespace Procedencia.VerificarExisteProcedencia
    <XmlType(Namespace:="urn:VerificaExisteProcedencia")> _
    <XmlRoot(Namespace:="urn:VerificaExisteProcedencia")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _existe As Boolean

        Public Property Existe() As Boolean
            Get
                Return _existe
            End Get
            Set(value As Boolean)
                _existe = value
            End Set
        End Property

    End Class

End Namespace

