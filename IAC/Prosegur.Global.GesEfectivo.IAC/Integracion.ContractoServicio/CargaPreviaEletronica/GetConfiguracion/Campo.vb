Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguracion

    <Serializable()> _
       Public Class Campo

        Private _columna As String
        Private _posicionInicial As Nullable(Of Integer)
        Private _posicionFinal As Nullable(Of Integer)
        Private _relleno As Nullable(Of Char)
        Private _alineacion As Nullable(Of eAlineacion)
        Private _tipoDato As Nullable(Of eTipoDato)


        Public Property PosicionFinal() As Nullable(Of Integer)
            Get
                Return _posicionFinal
            End Get
            Set(value As Nullable(Of Integer))
                _posicionFinal = value
            End Set
        End Property

        Public Property PosicionInicial() As Nullable(Of Integer)
            Get
                Return _posicionInicial
            End Get
            Set(value As Nullable(Of Integer))
                _posicionInicial = value
            End Set
        End Property

        Public Property Alineacion() As Nullable(Of eAlineacion)
            Get
                Return _alineacion
            End Get
            Set(value As Nullable(Of eAlineacion))
                _alineacion = value
            End Set
        End Property

        Public Property Relleno() As Nullable(Of Char)
            Get
                Return _relleno
            End Get
            Set(value As Nullable(Of Char))
                _relleno = value
            End Set
        End Property

        Public Property TipoDato() As Nullable(Of eTipoDato)
            Get
                Return _tipoDato
            End Get
            Set(value As Nullable(Of eTipoDato))
                _tipoDato = value
            End Set
        End Property


        Public Property Columna() As String
            Get
                Return _columna
            End Get
            Set(value As String)
                _columna = value
            End Set
        End Property

    End Class

End Namespace
