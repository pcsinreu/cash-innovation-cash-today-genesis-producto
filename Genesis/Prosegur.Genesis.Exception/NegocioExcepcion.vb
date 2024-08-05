Imports System.Xml.Serialization
Imports System.Xml

''' <summary>
''' Exceção customizada
''' </summary>
''' <remarks></remarks>
''' <history>
''' [prezende]  11/04/2012  Criado
''' </history>
Public Class NegocioExcepcion
    Inherits System.Exception

    Public Sub New(descricao As String)
        MyBase.New(descricao)
    End Sub

    Public Sub New(codigo As Integer, descricao As String)
        MyBase.New(descricao)
        _Codigo = codigo
    End Sub

    Public Sub New(excepcion As NegocioExcepcion)
        MyBase.New(If(excepcion Is Nothing, String.Empty, excepcion.Descricao))
        If excepcion IsNot Nothing Then
            _Codigo = excepcion.Codigo
        End If
    End Sub

#Region " Variáveis "

    Private _Codigo As Integer = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT

#End Region

#Region " Propriedades "

    Public ReadOnly Property Codigo() As Integer
        Get
            Return _Codigo
        End Get
    End Property

    Public ReadOnly Property Descricao() As String
        Get
            Return MyBase.Message
        End Get
    End Property

#End Region

End Class
