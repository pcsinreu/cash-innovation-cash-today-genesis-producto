Imports System.Xml.Serialization
Imports System.Xml


Namespace Contractos.GenesisMovil.ObtenerDescripcionesFiltroExtracion
    <Serializable()> _
    Public Class Respuesta

        Public Property IdentificadorCliente As String = String.Empty
        Public Property DescripcionCliente As String = String.Empty
        Public Property IdentificadorCanal As String = String.Empty
        Public Property DescripcionCanal As String = String.Empty
        Public Property IdentificadorSubCanal As String = String.Empty
        Public Property DescripcionSubCanal As String = String.Empty
        Public Property IdentificadorDivisa As String = String.Empty
        Public Property DescripcionDivisa As String = String.Empty
        Public Property IdentificadorDenominacion As String = String.Empty
        Public Property DescripcionDenominacion As String = String.Empty
        Public Property IdentificadorTipoContenedor As String = String.Empty
        Public Property DescripcionTipoContenedor As String = String.Empty
        Public Property IdentificadorDelegacion As String = String.Empty
        Public Property DescripcionDelegacion As String = String.Empty
        Public Property IdentificadorPlanta As String = String.Empty
        Public Property DescripcionPlanta As String = String.Empty
        Public Property IdentificadorSector As String = String.Empty
        Public Property DescripcionSector As String = String.Empty
        Public Property NumValor As Decimal = 1
        Public Property UnidadMedida As Decimal = 1

    End Class
End Namespace
