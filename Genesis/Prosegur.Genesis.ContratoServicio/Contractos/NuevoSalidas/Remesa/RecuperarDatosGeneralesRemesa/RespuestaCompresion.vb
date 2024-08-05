Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.RecuperarDatosGeneralesRemesa

    <XmlType(Namespace:="urn:RecuperarDatosGeneralesRemesa")> _
    <XmlRoot(Namespace:="urn:RecuperarDatosGeneralesRemesa")> _
    <Serializable> _
    Public Class RespuestaCompresion
        Inherits BaseRespuesta

        Public Property RemesasCompresion As Byte()

    End Class

End Namespace