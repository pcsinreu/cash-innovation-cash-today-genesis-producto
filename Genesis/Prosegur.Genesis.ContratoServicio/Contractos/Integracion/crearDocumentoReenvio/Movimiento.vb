Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon.Enumeradores

Namespace Contractos.Integracion.crearDocumentoReenvio

    <Serializable()>
    Public Class Movimiento

        Public Property remesas As List(Of Remesa)
        Public Property gestionaPorBulto As Boolean?
        Public Property accion As Comon.Enumeradores.Accion
        Public Property tipoIntegracion As TipoIntegracion
        Public Property usuario As String

    End Class


End Namespace