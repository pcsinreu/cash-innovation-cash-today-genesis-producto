Imports System.Xml.Serialization
Imports System.Xml

Namespace Procedencia.GetProcedencias

    <XmlType(Namespace:="urn:GetProcedencias")> _
    <XmlRoot(Namespace:="urn:GetProcedencias")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Private _Procedencias As ProcedenciaColeccion

        Public Property Procedencias() As ProcedenciaColeccion
            Get
                Return _Procedencias
            End Get
            Set(value As ProcedenciaColeccion)
                _Procedencias = value
            End Set
        End Property


    End Class

End Namespace