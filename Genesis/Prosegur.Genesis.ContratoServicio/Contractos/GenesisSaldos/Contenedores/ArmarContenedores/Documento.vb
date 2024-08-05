Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ArmarContenedores

    <Serializable()> _
    Public Class Documento

        Public Property CodigoFormulario As String
        Public Property CodigoUsuario As String
        Public Property FechaPlanCertificacion As DateTime
        Public Property FechaGestion As DateTime
        Public Property IdentificadorSector As String
        Public Property IdentificadorPlanta As String
        Public Property IdentificadorDelegacion As String
        Public Property EstadoDocumento As Prosegur.Genesis.Comon.Enumeradores.EstadoDocumento
        Public Property Contenedores As List(Of Contenedor)
        Public Property VersaoDocumento As Nullable(Of Integer)
        Public Property NumeroExterno As String

    End Class

End Namespace