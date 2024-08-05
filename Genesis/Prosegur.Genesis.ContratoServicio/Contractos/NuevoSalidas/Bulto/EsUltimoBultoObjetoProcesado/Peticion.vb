Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.EsUltimoBultoObjetoProcesado

    <XmlType(Namespace:="urn:EsUltimoBultoObjetoProcesado")> _
    <XmlRoot(Namespace:="urn:EsUltimoBultoObjetoProcesado")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

#Region "[PROPRIEDADES]"

        Public Property IdentificadorRemesa As String
        Public Property EsPreparador As Boolean

#End Region

    End Class

End Namespace