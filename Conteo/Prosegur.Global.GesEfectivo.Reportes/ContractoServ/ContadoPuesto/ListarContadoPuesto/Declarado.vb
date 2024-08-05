Namespace ContadoPuesto.ListarContadoPuesto

    Public Class Declarado

#Region " Variáveis "

        ' "P" – parcial, "B" – bulto e "R" - remesa
        Private _TipoDeclarados As String
        Private _DesDivisa As String
        Private _NumImporteTotal As Decimal

#End Region

#Region " Propriedades "

        Public Property TipoDeclarados() As String
            Get
                Return _TipoDeclarados
            End Get
            Set(value As String)
                _TipoDeclarados = value
            End Set
        End Property

        Public Property DesDivisa() As String
            Get
                Return _DesDivisa
            End Get
            Set(value As String)
                _DesDivisa = value
            End Set
        End Property

        Public Property NumImporteTotal() As Decimal
            Get
                Return _NumImporteTotal
            End Get
            Set(value As Decimal)
                _NumImporteTotal = value
            End Set
        End Property

#End Region

    End Class

End Namespace
