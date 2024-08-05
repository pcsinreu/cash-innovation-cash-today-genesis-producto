Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases

Namespace Contractos.Genesis.Bulto.RomperPrecintosSaldosSalidas

    <XmlType(Namespace:="urn:RomperPrecintosSaldosSalidas")> _
    <XmlRoot(Namespace:="urn:RomperPrecintosSaldosSalidas")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Bultos As List(Of Clases.Bulto)
        Public Property ReenvioCambioPrecintoLegado As Boolean
        Public Property ReenvioCambioPrecintoSol As Boolean
        Public Property TrabajaPorBulto As Boolean
        Public Property CodigoPlanta As String
        Public Property CodigoDelegacion As String
        Public Property CodigoUsuario As String

    End Class

End Namespace
