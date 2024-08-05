Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ReenvioEntreSectores

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ReenvioEntreSectores")> _
    <XmlRoot(Namespace:="urn:ReenvioEntreSectores")> _
    <Serializable()> _
    Public Class Peticion

        Public Property Documento As Documento
        Public Property CodigoUsuario As String

    End Class

End Namespace