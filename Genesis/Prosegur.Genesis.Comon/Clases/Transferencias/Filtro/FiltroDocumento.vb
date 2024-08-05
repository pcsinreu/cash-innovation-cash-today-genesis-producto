Imports System.Collections.ObjectModel

Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Remesas
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroDocumento
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _NumeroExterno As String
        Private _CodigoComprovante As String

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

        Public Property NumeroExterno As String
            Get
                Return _NumeroExterno
            End Get
            Set(value As String)
                SetProperty(_NumeroExterno, value, "NumeroExterno")
            End Set

        End Property

        Public Property CodigoComprovante As String
            Get
                Return _CodigoComprovante
            End Get
            Set(value As String)
                SetProperty(_CodigoComprovante, value, "CodigoComprovante")
            End Set
        End Property


#End Region

    End Class

End Namespace
