Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ReenvioEntreClientes

    <Serializable()> _
    Public Class Documento

        Public Property CodigoFormulario As String
        Public Property FechaHoraPlanCertificado As DateTime
        Public Property FechaHoraGestion As DateTime
        Public Property Sector As Comon.Sector
        Public Property ClienteDestino As Comon.Cliente
        Public Property Contenedores As List(Of Contenedor)
        Public Property CodigoUsuario As String

    End Class

End Namespace