Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboTiposMedioPago

    ''' <summary>
    ''' Classe TipoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class TipoMedioPago

        Private _Codigo As String
        Private _Descripcion As String

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                If _Descripcion <> value Then
                    _Descripcion = value
                End If
            End Set
        End Property

    End Class

End Namespace