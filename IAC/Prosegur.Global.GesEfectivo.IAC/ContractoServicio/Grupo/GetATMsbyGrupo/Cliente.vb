
Namespace Grupo.GetATMsbyGrupo

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 13/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Cliente

#Region "[Variáveis]"

        Private _codigoCliente As String
        Private _descripcionCliente As String
        Private _subClientes As List(Of SubCliente)

#End Region

#Region "[Propriedades]"

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property DescripcionCliente() As String
            Get
                Return _descripcionCliente
            End Get
            Set(value As String)
                _descripcionCliente = value
            End Set
        End Property

        Public Property SubClientes() As List(Of SubCliente)
            Get
                Return _subClientes
            End Get
            Set(value As List(Of SubCliente))
                _subClientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace