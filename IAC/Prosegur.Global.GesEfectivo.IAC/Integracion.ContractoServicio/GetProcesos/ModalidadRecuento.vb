Namespace GetProcesos

    <Serializable()> _
    Public Class ModalidadRecuento

#Region "[VARIÁVEIS]"

        Private _codigoTipoProcesado As String
        Private _descripcionTipoProcesado As String
        Private _vigenteModalidadeRecuento As Boolean
        Private _Caracteristicas As CaracteristicaColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoTipoProcesado() As String
            Get
                Return _codigoTipoProcesado
            End Get
            Set(value As String)
                _codigoTipoProcesado = value
            End Set
        End Property

        Public Property DescripcionTipoProcesado() As String
            Get
                Return _descripcionTipoProcesado
            End Get
            Set(value As String)
                _descripcionTipoProcesado = value
            End Set
        End Property

        Public Property VigenteModalidadRecuento() As Boolean
            Get
                Return _vigenteModalidadeRecuento
            End Get
            Set(value As Boolean)
                _vigenteModalidadeRecuento = value
            End Set
        End Property

        Public Property Caracteristicas() As CaracteristicaColeccion
            Get
                Return _Caracteristicas
            End Get
            Set(value As CaracteristicaColeccion)
                _Caracteristicas = value
            End Set
        End Property

#End Region

    End Class

End Namespace
