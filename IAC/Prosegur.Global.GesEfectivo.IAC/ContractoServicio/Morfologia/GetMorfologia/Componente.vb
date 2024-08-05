
Namespace Morfologia.GetMorfologia

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

        Private _oidMorfologiaComponente As String
        Private _codMorfologiaComponente As String
        Private _codTipoContenedor As String
        Private _necFuncionContenedor As String
        Private _bolVigente As Boolean
        Private _orden As Integer

#End Region

#Region "[Propriedades]"

        Public Property OidMorfologiaComponente() As String
            Get
                Return _oidMorfologiaComponente
            End Get
            Set(value As String)
                _oidMorfologiaComponente = value
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

        Public Property CodTipoContenedor() As String
            Get
                Return _codTipoContenedor
            End Get
            Set(value As String)
                _codTipoContenedor = value
            End Set
        End Property

        Public Property necFuncionContenedor() As String
            Get
                Return _necFuncionContenedor
            End Get
            Set(value As String)
                _necFuncionContenedor = value
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

        Public Property Orden As Integer
            Get
                Return _orden
            End Get
            Set(value As Integer)
                _orden = value
            End Set
        End Property

#End Region

    End Class

End Namespace