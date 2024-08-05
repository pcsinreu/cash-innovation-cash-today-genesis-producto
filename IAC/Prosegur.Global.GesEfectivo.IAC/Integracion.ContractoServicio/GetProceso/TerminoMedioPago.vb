﻿Namespace GetProceso

    <Serializable()> _
    Public Class TerminoMedioPago

#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _valorInicial As String
        Private _longitud As Integer
        Private _codigoFormato As String
        Private _descripcionFormato As String
        Private _codigoMascara As String
        Private _descripcionMascara As String
        Private _expresionRegularMascara As String
        Private _codigoAlgoritmo As String
        Private _descripcionAlgoritmo As String
        Private _mostrarCodigo As Boolean
        Private _esObligatorio As Boolean
        Private _orden As Integer
        Private _valoresPosibles As ValorPosibleColeccion

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

        Public Property Observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
            End Set
        End Property

        Public Property ValorInicial() As String
            Get
                Return _valorInicial
            End Get
            Set(value As String)
                _valorInicial = value
            End Set
        End Property

        Public Property Longitud() As Integer
            Get
                Return _longitud
            End Get
            Set(value As Integer)
                _longitud = value
            End Set
        End Property

        Public Property CodigoFormato() As String
            Get
                Return _codigoFormato
            End Get
            Set(value As String)
                _codigoFormato = value
            End Set
        End Property

        Public Property DescripcionFormato() As String
            Get
                Return _descripcionFormato
            End Get
            Set(value As String)
                _descripcionFormato = value
            End Set
        End Property

        Public Property CodigoMascara() As String
            Get
                Return _codigoMascara
            End Get
            Set(value As String)
                _codigoMascara = value
            End Set
        End Property

        Public Property DescripcionMascara() As String
            Get
                Return _descripcionMascara
            End Get
            Set(value As String)
                _descripcionMascara = value
            End Set
        End Property

        Public Property ExpresionRegularMascara() As String
            Get
                Return _expresionRegularMascara
            End Get
            Set(value As String)
                _expresionRegularMascara = value
            End Set
        End Property

        Public Property CodigoAlgoritmo() As String
            Get
                Return _codigoAlgoritmo
            End Get
            Set(value As String)
                _codigoAlgoritmo = value
            End Set
        End Property

        Public Property DescripcionAlgoritmo() As String
            Get
                Return _descripcionAlgoritmo
            End Get
            Set(value As String)
                _descripcionAlgoritmo = value
            End Set
        End Property

        Public Property MostrarCodigo() As Boolean
            Get
                Return _mostrarCodigo
            End Get
            Set(value As Boolean)
                _mostrarCodigo = value
            End Set
        End Property

        Public Property EsObligatorio() As Boolean
            Get
                Return _esObligatorio
            End Get
            Set(value As Boolean)
                _esObligatorio = value
            End Set
        End Property

        Public Property Orden() As Integer
            Get
                Return _orden
            End Get
            Set(value As Integer)
                _orden = value
            End Set
        End Property

        Public Property ValoresPosibles() As ValorPosibleColeccion
            Get
                Return _valoresPosibles
            End Get
            Set(value As ValorPosibleColeccion)
                _valoresPosibles = value
            End Set
        End Property

#End Region

    End Class
End Namespace