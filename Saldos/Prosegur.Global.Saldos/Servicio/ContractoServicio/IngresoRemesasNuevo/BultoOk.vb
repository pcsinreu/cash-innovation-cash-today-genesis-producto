Namespace IngresoRemesasNuevo

    Public Class BultoOk

#Region "[VARIÁVEIS]"

        Private _IdentificadorBultoOriginal As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property IdentificadorBultoOriginal() As String
            Get
                Return _IdentificadorBultoOriginal
            End Get
            Set(value As String)
                _IdentificadorBultoOriginal = value
            End Set
        End Property

#End Region

    End Class

End Namespace