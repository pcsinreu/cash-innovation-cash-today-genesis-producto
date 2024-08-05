Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/01/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class Cliente
        Inherits BaseEntidade

#Region "[Variáveis]"

        Private _desCliente As String
        Private _codigoCliente As String
        Private _subclientes As New List(Of Subcliente)

#End Region

#Region "[Propriedades]"

        Public Property DesCliente() As String
            Get
                Return _desCliente
            End Get
            Set(value As String)
                _desCliente = value
            End Set
        End Property

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property Subclientes() As List(Of Subcliente)
            Get
                Return _subclientes
            End Get
            Set(value As List(Of Subcliente))
                _subclientes = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(ClienteUtilidad As ContractoServicio.Utilidad.GetComboClientes.Cliente)

            _codigoCliente = ClienteUtilidad.Codigo
            _desCliente = ClienteUtilidad.Descripcion

        End Sub


#End Region

#Region "[MÉTODOS]"

        ''' <summary>
        ''' Retorna um objeto da operação GetATMs preenchido
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  10/01/2011  criado
        ''' </history>
        Public Function ConvertToGetATMs() As ContractoServicio.GetATMs.Cliente

            Dim objCliente As New ContractoServicio.GetATMs.Cliente

            With objCliente
                .CodigoCliente = _codigoCliente
            End With

            Return objCliente

        End Function

        ''' <summary>
        ''' Retorna um objeto da operação GetATMs preenchido
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  10/01/2011  criado
        ''' </history>
        Public Shared Function ConvertToGetATMs(CodCliente As String) As ContractoServicio.GetATMs.Cliente

            Dim objCliente As New ContractoServicio.GetATMs.Cliente

            With objCliente
                .CodigoCliente = CodCliente
            End With

            Return objCliente

        End Function

        Public Function ConvertToComboCliente() As ContractoServicio.Utilidad.GetComboClientes.Cliente

            Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente

            With objCliente
                .Codigo = _codigoCliente
                .Descripcion = _desCliente
            End With

            Return objCliente

        End Function

#End Region
        
    End Class

End Namespace