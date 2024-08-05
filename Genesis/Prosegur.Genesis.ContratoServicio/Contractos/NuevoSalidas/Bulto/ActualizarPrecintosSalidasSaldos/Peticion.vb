Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.NuevoSalidas.Bulto.ActualizarPrecintosSalidasSaldos

    <XmlType(Namespace:="urn:ActualizarPrecintosSalidasSaldos")> _
    <XmlRoot(Namespace:="urn:ActualizarPrecintosSalidasSaldos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Remesa As Clases.Remesa
        Public Property ReenvioCambioPrecintoLegado As Boolean
        Public Property ReenvioCambioPrecintoSol As Boolean
        Public Property PantallaOrigen As Enumeradores.Salidas.Pantalla
        Public Property CodigoPlanta As String

    End Class

End Namespace
