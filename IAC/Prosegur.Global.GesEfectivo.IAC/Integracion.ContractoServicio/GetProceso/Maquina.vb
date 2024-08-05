Namespace GetProceso

    <Serializable()> _
    Public Class Maquina

#Region "Variáveis"
        Private _descripcion As String
#End Region

#Region "Propriedades"

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

#End Region
    End Class

End Namespace