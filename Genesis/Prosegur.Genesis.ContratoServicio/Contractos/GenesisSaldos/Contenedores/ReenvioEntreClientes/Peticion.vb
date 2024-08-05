Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ReenvioEntreClientes

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    <XmlType(Namespace:="urn:ReenvioEntreClientes")> _
    <XmlRoot(Namespace:="urn:ReenvioEntreClientes")> _
    <Serializable()> _
    Public Class Peticion

        Public Property Documento As Documento
        Public Property CodigoUsuario As String

    End Class

End Namespace