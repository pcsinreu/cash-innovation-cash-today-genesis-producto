﻿Namespace Canal
    <Serializable()> _
    Public Class Canal

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _vigente As Boolean
        Private _subCanales As SubCanalColeccion



        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(ByVal value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(ByVal value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(ByVal value As String)
                _observaciones = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(ByVal value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property SubCanales() As SubCanalColeccion
            Get
                Return _subCanales
            End Get
            Set(ByVal value As SubCanalColeccion)
                _subCanales = value
            End Set
        End Property


    End Class
End Namespace
