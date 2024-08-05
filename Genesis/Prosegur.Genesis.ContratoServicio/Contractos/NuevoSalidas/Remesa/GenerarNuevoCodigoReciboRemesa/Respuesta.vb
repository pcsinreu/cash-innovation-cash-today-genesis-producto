Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.NuevoSalidas.Remesa.GenerarNuevoCodigoReciboRemesa

    <XmlType(Namespace:="urn:GenerarNuevoCodigoReciboRemesa")> _
    <XmlRoot(Namespace:="urn:GenerarNuevoCodigoReciboRemesa")> _
    <Serializable()> _
    Public NotInheritable Class Respuesta
        Inherits BaseRespuesta

#Region "[PROPRIEDADES]"

        Public Property CodigoReciboRemesa As String
        Public Property FechaHoraActualizacion As DateTime
#End Region

    End Class

End Namespace