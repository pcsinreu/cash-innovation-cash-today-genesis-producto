Imports System.Xml.Serialization
Imports System.Xml

Namespace GetParametrosDelegacionPais

    <XmlType(Namespace:="urn:GetParametrosDelegacionPais")> _
    <XmlRoot(Namespace:="urn:GetParametrosDelegacionPais")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property CodigoDelegacion As String
        Public Property CodigoAplicacion As String
        Public Property ValidarParametros As Boolean
        Public Property Parametros As ParametroColeccion

#End Region

    End Class

End Namespace