Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper
Imports System.Xml.Serialization

Namespace Clases

    <Serializable()>
    Public Class ParametroReporte
        Inherits BindableBase


#Region "Variaveis"

        Private _Codigo As String
        Private _Descripcion As String
        Private _DescripcionValor As String
        Private _Identificador As String

#End Region

#Region "Propriedades"

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
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

        Public Property DescripcionValor As String
            Get
                Return _DescripcionValor
            End Get
            Set(value As String)
                SetProperty(_DescripcionValor, value, "DescripcionValor")
            End Set
        End Property

#End Region

    End Class

End Namespace