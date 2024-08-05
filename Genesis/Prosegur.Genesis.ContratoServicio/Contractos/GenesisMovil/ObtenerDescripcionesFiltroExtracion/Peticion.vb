Imports Prosegur.Genesis.Comon
Imports System.Xml
Imports System.Xml.Serialization

Namespace Contractos.GenesisMovil.ObtenerDescripcionesFiltroExtracion
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"
        Public Property CodigoCliente As String
        Public Property CodigoCanal As String
        Public Property CodigoSubCanal As String
        Public Property CodigoDivisa As String
        Public Property CodigoDenominacion As String
        Public Property CodigoTipoContenedor As String
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoSector As String

#End Region

    End Class

End Namespace