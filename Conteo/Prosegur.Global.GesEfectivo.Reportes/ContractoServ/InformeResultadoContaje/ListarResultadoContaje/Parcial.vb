Namespace InformeResultadoContaje.ListarResultadoContaje

    <Serializable()> _
    Public Class Parcial

#Region "[PROPRIEDADES]"

        Public Property IdentificadorParcial As String
        Public Property NumParcial As String
        Public Property MediosPagos As MedioPagoColeccion
        Public Property Diferencias As DiferenciaColeccion
        Public Property Efectivos As EfectivoColeccion
        Public Property Intervenciones As IntervencionColeccion
        Public Property IACs As IACColeccion
        Public Property CodUsuario As String
        Public Property FechaFinConteo As DateTime
        Public Property CodSupervisor As String
        Public Property Declarados As DeclaradoColeccion

#End Region

    End Class

End Namespace