Namespace InformeResultadoContaje.ListarResultadoContaje

    <Serializable()> _
    Public Class Bulto

#Region "[PROPRIEDADES]"

        Public Property IdentificadorBulto As String
        Public Property NumPrecinto As String
        Public Property Declarados As DeclaradoColeccion
        Public Property Diferencias As DiferenciaColeccion
        Public Property Parciales As ParcialColeccion
        Public Property Intervenciones As IntervencionColeccion
        Public Property IACs As IACColeccion
        Public Property FechaFinConteo As DateTime
        Public Property CodUsuario As String
        Public Property Caracteristicas As CaracteristicaColeccion
        Public Property NumeroParcialesDeclarados As Int32

#End Region

    End Class

End Namespace