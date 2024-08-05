Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Saldo.ActualizarSaldoPuesto

    <XmlType(Namespace:="urn:ActualizarSaldoPuesto")> _
    <XmlRoot(Namespace:="urn:ActualizarSaldoPuesto")> _
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty
        Public Property CodigoPlanta As String = String.Empty
        Public Property CodigoPuesto As String = String.Empty
        Public Property CodigoUsuario As String = String.Empty
        Public Property DiferenciaSaldo As Clases.SaldoCuenta

    End Class
End Namespace
