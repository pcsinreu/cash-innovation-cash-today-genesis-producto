Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.Comon

   
    <Serializable()> _
    Public Class Cliente
        Inherits BindableBase

#Region "[PROPRIEDADES]"

        Public Property codCliente As String
        Public Property codSubcliente As String
        Public Property codPuntoServicio As String
        Public Property IdentificadorCliente As String
        Public Property IdentificadorSubCliente As String
        Public Property IdentificadorPuntoServicio As String

#End Region

    End Class

End Namespace
