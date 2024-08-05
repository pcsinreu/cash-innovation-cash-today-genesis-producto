
Namespace ATM.SetATM

    ''' <summary>
    ''' Classe Proceso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 12/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Proceso

#Region "[Variáveis]"

        Private _peticionProceso As ContractoServicio.Proceso.SetProceso.Peticion

#End Region

#Region "[Propriedades]"

        Public Property PeticionProceso() As ContractoServicio.Proceso.SetProceso.Peticion
            Get
                Return _peticionProceso
            End Get
            Set(value As ContractoServicio.Proceso.SetProceso.Peticion)
                _peticionProceso = value
            End Set
        End Property

#End Region

    End Class

End Namespace