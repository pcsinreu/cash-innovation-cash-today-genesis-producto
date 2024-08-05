Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.ValidarRemesasAnuladasSOL

    <XmlType(Namespace:="urn:ValidarRemesasAnuladasSOL")> _
    <XmlRoot(Namespace:="urn:ValidarRemesasAnuladasSOL")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadoresRemesas As List(Of String)
        Public Property IdentificadorOT As String
        Public Property UrlServicioIntegracionSol As String
        Public Property CodigoUsuario As String
        Public Property FechaHoraActualizacion As DateTime
        Public Property PeticionSOL As ContractoServicio.NuevoSalidas.ActualizarEstadoPreparacionRemesa.peticionActualizarEstadoPreparacionRemesa
        Public Property Armador As Boolean
        Public Property PreparadorArmador As Boolean

    End Class

End Namespace