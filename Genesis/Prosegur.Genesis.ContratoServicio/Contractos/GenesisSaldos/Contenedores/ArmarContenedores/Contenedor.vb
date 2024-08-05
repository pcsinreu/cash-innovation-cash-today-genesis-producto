Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Contenedores.ArmarContenedores

    <Serializable()> _
    Public Class Contenedor

        Public Property CodigoContenedor As String
        Public Property TipoElemento As Enumeradores.TipoElemento
        Public Property TipoContenedor As Clases.TipoContenedor
        Public Property TipoServicio As Clases.TipoServicio
        Public Property TipoFormato As Clases.TipoFormato
        Public Property CodigoPuesto As String
        Public Property Precintos As List(Of String)
        Public Property Elementos As List(Of Clases.Elemento)
        Public Property Cuenta As Clases.CuentasContenedor
        Public Property PrecintoAutomatico As String

    End Class

End Namespace