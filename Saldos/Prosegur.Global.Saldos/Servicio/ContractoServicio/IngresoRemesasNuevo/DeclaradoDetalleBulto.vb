Namespace IngresoRemesasNuevo

    ''' <summary>
    ''' Classe DeclaradoDetalleBulto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama]  27/07/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class DeclaradoDetalleBulto

#Region "[ATRIBUTOS]"

        Private _CodigoIsoDivisa As String
        Private _CodigoDenominacion As String
        Private _Unidades As Nullable(Of Integer)

#End Region

#Region "[METODOS]"

        ''' <summary>
        ''' Propriedade Codigo Divisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoIsoDivisa() As String
            Get
                Return _CodigoIsoDivisa
            End Get
            Set(value As String)
                _CodigoIsoDivisa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade Codigo Denominacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoDenominacion() As String
            Get
                Return _CodigoDenominacion
            End Get
            Set(value As String)
                _CodigoDenominacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade Unidades
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <remarks></remarks>
        Public Property Unidades() As Nullable(Of Integer)
            Get
                Return _Unidades
            End Get
            Set(value As Nullable(Of Integer))
                _Unidades = value
            End Set
        End Property

#End Region

    End Class

End Namespace