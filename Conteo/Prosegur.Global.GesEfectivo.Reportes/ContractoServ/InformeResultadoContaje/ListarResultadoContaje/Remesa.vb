Namespace InformeResultadoContaje.ListarResultadoContaje

    <Serializable()> _
    Public Class Remesa

#Region "[PROPRIEDADES]"

        Public Property IdentificadorRemesa As String
        Public Property Bultos As BultoColeccion
        Public Property Declarados As DeclaradoColeccion
        Public Property InfoCliente As InformacionCliente
        Public Property Diferencias As DiferenciaColeccion
        Public Property IACs As IACColeccion
        Public Property Intervenciones As IntervencionColeccion
        Public Property CodUsuario As String
        Public Property FechaFinConteo As DateTime
        Public Property CodPrecinto As String
        Public Property CodPrecintosBultos As String
        Public Property CodIsoDivisaLocal As String

#End Region

    End Class

End Namespace