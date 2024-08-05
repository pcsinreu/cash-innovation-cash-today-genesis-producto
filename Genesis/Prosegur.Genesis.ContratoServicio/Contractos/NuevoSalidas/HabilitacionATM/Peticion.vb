Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Remesa.CerrarHabilitacionATM

    <XmlType(Namespace:="urn:CerrarHabilitacionATM")> _
    <XmlRoot(Namespace:="urn:CerrarHabilitacionATM")> _
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
        Public Property EsRomperPrecinto As Boolean
        Public Property UrlServicioIntegracionSol As String
        Public Property PeticionSOL As ContractoServicio.NuevoSalidas.ActualizarEstadoPreparacionRemesa.peticionActualizarEstadoPreparacionRemesa
        Public Property ReenvioCambioPrecintoLegado As Boolean
        Public Property ReenvioCambioPrecintoSol As Boolean
        Public Property PantallaOrigen As Enumeradores.Salidas.Pantalla

    End Class

End Namespace