
Namespace ATM.GetATMDetail

    ''' <summary>
    ''' Classe Morfologia
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 07/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Proceso

#Region "[Variáveis]"

        Private _respuestaProcesoDetail As ContractoServicio.Proceso.GetProcesoDetail.Respuesta
        Private _oidProceso As String

#End Region

#Region "[Propriedades]"

        Public Property RespuestaProcesoDetail() As ContractoServicio.Proceso.GetProcesoDetail.Respuesta
            Get
                Return _respuestaProcesoDetail
            End Get
            Set(value As ContractoServicio.Proceso.GetProcesoDetail.Respuesta)
                _respuestaProcesoDetail = value
            End Set
        End Property

        Public Property OidProceso As String
            Get
                Return _oidProceso
            End Get
            Set(value As String)
                _oidProceso = value
            End Set
        End Property

#End Region

    End Class

End Namespace