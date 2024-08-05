Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases
    <Serializable()>
    Public Class MensajeDePlanificacion
        Inherits BindableBase
        Implements IEntidadeHelper

        Private _codigo As String
        Private _descripcion As String
        Private _tipo As String
        Private _sinReintentos As Boolean
        Private _tipoPeriodo As String

        Public Property Codigo As String Implements IEntidadeHelper.Codigo
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion As String Implements IEntidadeHelper.Descripcion
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Tipo() As String
            Get
                Return _tipo
            End Get
            Set(ByVal value As String)
                _tipo = value
            End Set
        End Property

        Public Property SinReintentos() As Boolean
            Get
                Return _sinReintentos
            End Get
            Set(ByVal value As Boolean)
                _sinReintentos = value
            End Set
        End Property

        Public Property TipoPeriodo() As String
            Get
                Return _tipoPeriodo
            End Get
            Set(ByVal value As String)
                _tipoPeriodo = value
            End Set
        End Property

        Public Property DesTipoPeriodo As String
    End Class
End Namespace

