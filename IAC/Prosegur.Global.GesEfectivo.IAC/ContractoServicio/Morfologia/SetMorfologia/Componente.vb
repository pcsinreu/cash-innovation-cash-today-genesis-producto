
Namespace Morfologia.SetMorfologia

    ''' <summary>
    ''' Classe Componente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 20/12/2010 Criado
    ''' </history>
    <Serializable()> _
    Public Class Componente

#Region "[Variáveis]"

        Private _codTipoContenedor As String
        Private _desTipoContenedor As String
        Private _necFuncionContenedor As Integer
        Private _codMorfologiaComponente As String
        Private _bolVigente As Boolean
        Private _objectos As List(Of Objecto)
        Private _orden As Integer

#End Region

#Region "[Propriedades]"

        Public Property Orden As Integer
            Get
                Return _orden
            End Get
            Set(value As Integer)
                _orden = value
            End Set
        End Property

        Public Property CodTipoContenedor() As String
            Get
                Return _codTipoContenedor
            End Get
            Set(value As String)
                _codTipoContenedor = value
            End Set
        End Property

        Public Property DesTipoContenedor() As String
            Get
                Return _desTipoContenedor
            End Get
            Set(value As String)
                _desTipoContenedor = value
            End Set
        End Property


        Public Property NecFuncionContenedor() As String
            Get
                Return _necFuncionContenedor
            End Get
            Set(value As String)
                _necFuncionContenedor = value
            End Set
        End Property

        Public Property CodMorfologiaComponente() As String
            Get
                Return _codMorfologiaComponente
            End Get
            Set(value As String)
                _codMorfologiaComponente = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property Objectos() As List(Of Objecto)
            Get
                Return _objectos
            End Get
            Set(value As List(Of Objecto))
                _objectos = value
            End Set
        End Property

#End Region

    End Class

End Namespace