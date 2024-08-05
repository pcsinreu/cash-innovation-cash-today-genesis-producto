Namespace CorteParcial.GetCortesParciais

    Public Class Observacion

#Region " Variáveis "

        Private _Remesa As String = String.Empty
        Private _Descricion As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property Remesa() As String
            Get
                Return _Remesa
            End Get
            Set(value As String)
                _Remesa = value
            End Set
        End Property

        Public Property Descricion() As String
            Get
                Return _Descricion
            End Get
            Set(value As String)
                _Descricion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
