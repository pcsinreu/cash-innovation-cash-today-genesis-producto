
Namespace TerminoIac.SetTerminoIac
    ''' <summary>
    ''' Classe TerminoIac
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 12/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class TerminoIac

#Region "[Variáveis]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _Vigente As Boolean

#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(ByVal value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(ByVal value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property Observacion() As String
            Get
                Return _Observacion
            End Get
            Set(ByVal value As String)
                _Observacion = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(ByVal value As Boolean)
                _Vigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace