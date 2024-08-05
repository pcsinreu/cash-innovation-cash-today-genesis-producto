Namespace CorteParcial.GetCortesParciais

    Public Class Sobre

#Region " Variáveis "

        Private _ParcialesContados As Decimal = 0
        Private _ParcialesIngresados As Decimal = 0
        Private _ParcialesDeclarados As Decimal = 0
        Private _Remesa As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property ParcialesContados() As Decimal
            Get
                Return _ParcialesContados
            End Get
            Set(value As Decimal)
                _ParcialesContados = value
            End Set
        End Property

        Public Property ParcialesIngresados() As Decimal
            Get
                Return _ParcialesIngresados
            End Get
            Set(value As Decimal)
                _ParcialesIngresados = value
            End Set
        End Property

        Public Property ParcialesDeclarados() As Decimal
            Get
                Return _ParcialesDeclarados
            End Get
            Set(value As Decimal)
                _ParcialesDeclarados = value
            End Set
        End Property

        Public Property Remesa() As String
            Get
                Return _Remesa
            End Get
            Set(value As String)
                _Remesa = value
            End Set
        End Property

#End Region

    End Class

End Namespace
