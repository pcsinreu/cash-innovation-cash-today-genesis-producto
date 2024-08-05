Imports Prosegur.Genesis.Comon.Enumeradores
Namespace IntegracionGenerica
    Public Class Peticion
        Public Property CodigoProceso As String
        Public Property CodigoOrigen As String
        Public Property CodigoDestino As String
        Public Property IdentificadorLlamada As String
        Public Property NombreParametroReintentoMaximo As String
        Public Property CodigoPais As String
        Public Property ListaCodigosEstado As List(Of EstadoIntegracion)

    End Class
End Namespace

