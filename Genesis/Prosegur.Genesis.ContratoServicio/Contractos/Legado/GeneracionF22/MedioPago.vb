Namespace Legado.GeneracionF22

    <Serializable()> _
    Public Class MedioPago

#Region "Variáveis"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Tipo As Enumeradores.TipoNegocio
        Private _Importe As Double
        Private _Cantidad As Int64
        Private _CodigoISODivisa As String

#End Region

#Region "Propriedades"
        Public Property Tipo As Enumeradores.TipoMedioPago
            Get
                Return _Tipo
            End Get
            Set(value As Enumeradores.TipoMedioPago)
                _Tipo = value
            End Set
        End Property

        Public Property Importe As Double
            Get
                Return _Importe
            End Get
            Set(value As Double)
                _Importe = value
            End Set
        End Property

        Public Property Cantidad As Int64
            Get
                Return _Cantidad
            End Get
            Set(value As Int64)
                _Cantidad = value
            End Set
        End Property


        ''' <summary>
        ''' Propriedad CodigoISODivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoISODivisa() As String
            Get
                Return _CodigoISODivisa
            End Get
            Set(value As String)
                _CodigoISODivisa = value
            End Set
        End Property

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property
#End Region

    End Class

End Namespace