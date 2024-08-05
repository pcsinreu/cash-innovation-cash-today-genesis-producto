Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace DatoBancario.AlterarCuentaEstandar
    <Serializable()> _
    <XmlType(Namespace:="urn:AlterarCuentaEstandar")> _
    <XmlRoot(Namespace:="urn:AlterarCuentaEstandar")> _
    Public Class Peticion
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BasePeticion

        Public Property Identificador As String
        Public Property BolDefecto As Boolean

    End Class

End Namespace

