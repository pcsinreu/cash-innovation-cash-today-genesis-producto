Namespace Clases

    ' ***********************************************************************
    '  Modulo:  ValorDenominacion.vb
    '  Descripción: Clase definición ValorDenominacion
    ' ***********************************************************************
    <Serializable()>
    Public Class ValorDenominacion
        Inherits Valor

#Region "Variaveis"

        Private _Cantidad As Int64
        Private _UnidadMedida As UnidadMedida
        Private _Calidad As Calidad

#End Region

#Region "Propriedades"

        Public Property Cantidad As Int64
            Get
                Return _Cantidad
            End Get
            Set(value As Int64)
                SetProperty(_Cantidad, value, "Cantidad")
            End Set
        End Property

        Public Property UnidadMedida As UnidadMedida
            Get
                Return _UnidadMedida
            End Get
            Set(value As UnidadMedida)
                SetProperty(_UnidadMedida, value, "UnidadMedida")
            End Set
        End Property

        Public Property Calidad As Calidad
            Get
                Return _Calidad
            End Get
            Set(value As Calidad)
                SetProperty(_Calidad, value, "Calidad")
            End Set
        End Property

#End Region

    End Class

End Namespace

