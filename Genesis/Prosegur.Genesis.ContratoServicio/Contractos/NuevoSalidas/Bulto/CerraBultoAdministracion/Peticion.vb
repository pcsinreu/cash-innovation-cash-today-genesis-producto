Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace NuevoSalidas.Bulto.CerrarBultoAdministracion

    <XmlType(Namespace:="urn:CerrarBultoAdministracion")> _
    <XmlRoot(Namespace:="urn:CerrarBultoAdministracion")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorDelegacion As String = String.Empty
        Public Property CodigoDelegacion As String = String.Empty
        Public Property IdentificadorPlanta As String = String.Empty
        Public Property CodigoPlanta As String = String.Empty
        Public Property CodigoUsuario As String = String.Empty
        Public Property CodigoPuesto As String = String.Empty
        Public Property EsCuentaCero As Boolean
        Public Property Remesas As ObservableCollection(Of Clases.Remesa)
        Public Property UrlServicioIntegracionSol As String
        Public Property PeticionSOL As ContractoServicio.NuevoSalidas.ActualizarEstadoPreparacionRemesa.peticionActualizarEstadoPreparacionRemesa
        Public Property GestionaSaldoPorPuesto As Boolean

    End Class

End Namespace