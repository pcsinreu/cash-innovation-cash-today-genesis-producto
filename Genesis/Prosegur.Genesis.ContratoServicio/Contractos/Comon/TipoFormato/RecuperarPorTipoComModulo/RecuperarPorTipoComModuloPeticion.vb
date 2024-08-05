Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace TipoFormato.RecuperarPorTipoComModulo
    
    <Serializable()>
    Public Class RecuperarPorTipoComModuloPeticion
        Inherits Prosegur.Genesis.Comon.BasePeticion
        Public Codigos As List(Of String)
    End Class

End Namespace
