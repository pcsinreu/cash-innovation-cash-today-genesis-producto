Namespace IngresoContado

    ''' <summary>
    ''' Classe DeclaradoAgrupacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class DeclaradoAgrupacion

        Private _declaradoCodigoAgrupacion As String
        Private _declaradoDescripcionAgrupacion As String
        Private _declaradoImporteAgrupacion As Decimal

        Public Property DeclaradoCodigoAgrupacion() As String
            Get
                Return _declaradoCodigoAgrupacion
            End Get
            Set(value As String)
                _declaradoCodigoAgrupacion = value
            End Set
        End Property

        Public Property DeclaradoDescripcionAgrupacion() As String
            Get
                Return _declaradoDescripcionAgrupacion
            End Get
            Set(value As String)
                _declaradoDescripcionAgrupacion = value
            End Set
        End Property

        Public Property DeclaradoImporteAgrupacion() As Decimal
            Get
                Return _declaradoImporteAgrupacion
            End Get
            Set(value As Decimal)
                _declaradoImporteAgrupacion = value
            End Set
        End Property

    End Class

End Namespace