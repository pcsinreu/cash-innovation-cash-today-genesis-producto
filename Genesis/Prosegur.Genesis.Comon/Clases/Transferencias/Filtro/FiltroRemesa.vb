Namespace Clases.Transferencias

    ''' <summary>
    ''' Classe que representa o Filtro de Remesas
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class FiltroRemesa
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _CodigoExterno As String
        Private _CodigoRuta As String
        Private _FechaAltaDesde As Nullable(Of DateTime)
        Private _FechaAltaHasta As Nullable(Of DateTime)

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

        Public Property CodigoExterno As String
            Get
                Return _CodigoExterno
            End Get
            Set(value As String)
                SetProperty(_CodigoExterno, value, "CodigoExterno")
            End Set
        End Property

        Public Property CodigoRuta As String
            Get
                Return _CodigoRuta
            End Get
            Set(value As String)
                SetProperty(_CodigoRuta, value, "CodigoRuta")
            End Set
        End Property

        Public Property FechaAltaDesde As Nullable(Of DateTime)
            Get
                Return _FechaAltaDesde
            End Get
            Set(value As Nullable(Of DateTime))
                SetProperty(_FechaAltaDesde, value, "FechaAltaDesde")
            End Set
        End Property

        Public Property FechaAltaHasta As Nullable(Of DateTime)
            Get
                Return _FechaAltaHasta
            End Get
            Set(value As Nullable(Of DateTime))
                SetProperty(_FechaAltaHasta, value, "FechaAltaHasta")
            End Set
        End Property


#End Region

    End Class

End Namespace