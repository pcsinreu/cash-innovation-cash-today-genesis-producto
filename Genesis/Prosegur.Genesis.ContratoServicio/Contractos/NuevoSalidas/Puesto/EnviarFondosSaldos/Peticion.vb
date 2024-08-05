Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Puesto.EnviarFondosSaldos

    <XmlType(Namespace:="urn:EnviarFondosSaldos")> _
    <XmlRoot(Namespace:="urn:EnviarFondosSaldos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Documentos As ObservableCollection(Of Clases.Documento)
        Public Property CodigoDelegacion As String
        Public Property TipoEfectivo As Enumeradores.TipoBuscaSaldo

    End Class

End Namespace