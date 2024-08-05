Namespace Proceso.SetProceso

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/03/2009 Criado
    ''' </history>    
    <Serializable()> _
    Public Class Cliente

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _subClientes As SubClienteColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property SubClientes() As SubClienteColeccion
            Get
                Return _subClientes
            End Get
            Set(value As SubClienteColeccion)
                _subClientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace