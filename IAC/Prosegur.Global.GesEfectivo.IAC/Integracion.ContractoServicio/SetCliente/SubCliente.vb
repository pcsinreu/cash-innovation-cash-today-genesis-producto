﻿Namespace SetCliente

    <Serializable()> _
    Public Class SubCliente
#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _vigente As Nullable(Of Boolean)
        Private _puntoServicio As PuntoServicioColeccion
#End Region

#Region "Propriedades"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _vigente = value
            End Set
        End Property

        Public Property PuntoServicio() As PuntoServicioColeccion
            Get
                Return _puntoServicio
            End Get
            Set(value As PuntoServicioColeccion)
                _puntoServicio = value
            End Set
        End Property
#End Region

    End Class

End Namespace