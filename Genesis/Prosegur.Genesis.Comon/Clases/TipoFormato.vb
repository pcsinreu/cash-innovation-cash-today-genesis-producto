Imports System.ComponentModel
Imports System.Collections.ObjectModel

Namespace Clases

    <Serializable()>
    Public Class TipoFormato
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _EsDefecto As Boolean
        Private _Modulo As Modulo
#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property EsDefecto As Boolean
            Get
                Return _EsDefecto
            End Get
            Set(value As Boolean)
                SetProperty(_EsDefecto, value, "EsDefecto")
            End Set
        End Property

        Public Property Modulo As Modulo
            Get
                Return _Modulo
            End Get
            Set(value As Modulo)
                SetProperty(_Modulo, value, "Modulo")
            End Set
        End Property

       


#End Region

    End Class



End Namespace
