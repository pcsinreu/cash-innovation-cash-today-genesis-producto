
Namespace ATM.SetATM

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
        Private _fechaInicio As DateTime

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

        Public Property FechaInicio() As DateTime
            Get
                Return _fechaInicio
            End Get
            Set(value As DateTime)
                _fechaInicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace