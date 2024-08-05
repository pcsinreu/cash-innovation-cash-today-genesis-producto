Namespace InformeResultadoContaje.ListarResultadoContaje

    <Serializable()> _
    Public Class Intervencion

#Region "[PROPRIEDADES]"

        Public Property IdentificadorIntervencion As String
        Public Property Motivos As List(Of String)
        Public Property CodSupervisor As String
        Public Property CodContador As String
        Public Property Comentario As String
        Public Property FechaFinIntervencion As DateTime
        Public Property Falsos As FalsoColeccion

#End Region

    End Class

End Namespace