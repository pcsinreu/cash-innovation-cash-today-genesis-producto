Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguracion

    ''' <summary>
    ''' Configuracao
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 25/03/2013 Criado
    ''' </history>
    <Serializable()>
    Public Class Configuracion

#Region "campos e propriedades"


        Private _declarados As DeclaradoColeccion
        Private _iac As IacColeccion
        Private _comentario As String
        Private _descripcionNombreHoja As String
        Private _filaInicial As Integer?
        Private _filaFinal As Integer?
        Private _esFilaTotal As Boolean?

        Public Property Declarados() As DeclaradoColeccion
            Get
                Return _declarados
            End Get
            Set(value As DeclaradoColeccion)
                _declarados = value
            End Set
        End Property

        Public Property IacColeccion() As IacColeccion
            Get
                Return _iac
            End Get
            Set(value As IacColeccion)
                _iac = value
            End Set
        End Property

        Public Property Comentario() As String
            Get
                Return _comentario
            End Get
            Set(value As String)
                _comentario = value
            End Set
        End Property

        Public Property DescripcionNombreHoja() As String
            Get
                Return _descripcionNombreHoja
            End Get
            Set(value As String)
                _descripcionNombreHoja = value
            End Set
        End Property

        Public Property esFilaTotal() As Boolean?
            Get
                Return _esFilaTotal
            End Get
            Set(value As Boolean?)
                _esFilaTotal = value
            End Set
        End Property

        Public Property FilaInicial() As Integer?
            Get
                Return _filaInicial
            End Get
            Set(value As Integer?)
                _filaInicial = value
            End Set
        End Property

        Public Property FilaFinal() As Integer?
            Get
                Return _filaFinal
            End Get
            Set(value As Integer?)
                _filaFinal = value
            End Set
        End Property
#End Region

    End Class

End Namespace