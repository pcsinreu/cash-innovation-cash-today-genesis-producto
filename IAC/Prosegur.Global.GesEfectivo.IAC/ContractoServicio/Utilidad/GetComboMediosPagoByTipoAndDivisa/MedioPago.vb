Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.GetComboMediosPagoByTipoAndDivisa

    ''' <summary>
    ''' Classe MedioPago
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class MedioPago

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
                _Descripcion = value
            End Set
        End Property

    End Class

End Namespace