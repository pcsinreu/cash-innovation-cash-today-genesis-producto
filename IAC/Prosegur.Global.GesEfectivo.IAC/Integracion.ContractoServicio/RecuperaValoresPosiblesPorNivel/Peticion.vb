Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperaValoresPosiblesPorNivel

    <XmlType(Namespace:="urn:RecuperaValoresPosiblesPorNivel")> _
    <XmlRoot(Namespace:="urn:RecuperaValoresPosiblesPorNivel")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property Cliente As String
        Public Property Subcliente As String
        Public Property PuntoServicio As String
        Public Property Terminos As TerminoColeccion

#End Region

    End Class

End Namespace