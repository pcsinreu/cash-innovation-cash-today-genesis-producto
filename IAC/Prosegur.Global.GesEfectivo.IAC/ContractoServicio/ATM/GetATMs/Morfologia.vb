
Namespace GetATMs

    ''' <summary>
    ''' Classe Morfologia
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 07/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Morfologia

#Region "[Variáveis]"

        Private _oidMorfologia As String
        Private _descripcionMorfologia As String
        Private _necModalidadRecogida As Integer

#End Region

#Region "[Propriedades]"

        Public Property OidMorfologia() As String
            Get
                Return _oidMorfologia
            End Get
            Set(value As String)
                _oidMorfologia = value
            End Set
        End Property

        Public Property DescripcionMorfologia() As String
            Get
                Return _descripcionMorfologia
            End Get
            Set(value As String)
                _descripcionMorfologia = value
            End Set
        End Property

        Public Property NecModalidadRecogida() As Integer
            Get
                Return _necModalidadRecogida
            End Get
            Set(value As Integer)
                _necModalidadRecogida = value
            End Set
        End Property

#End Region

    End Class

End Namespace