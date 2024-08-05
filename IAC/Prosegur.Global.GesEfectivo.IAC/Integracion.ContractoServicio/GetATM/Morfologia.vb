Namespace GetATM

    <Serializable()> _
    Public Class Morfologia

        Public Property IdMorfologia As String
        Public Property FechaInicio As Date
        Public Property CodigoMorfologia As String
        Public Property DescripcionMorfologia As String
        Public Property EsVigente As Boolean
        Public Property ModalidadRecogida As Integer
        Public Property Componentes As List(Of GetATM.Componente)

    End Class

End Namespace



