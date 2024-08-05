Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Sub Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/01/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class Subcliente
        Inherits BaseEntidade

#Region "[Variáveis]"

        Private _desSubcliente As String
        Private _codigoSubcliente As String
        Private _ptosServicio As New List(Of PuntoServicio)

#End Region

#Region "[Propriedades]"

        Public Property DesSubcliente() As String
            Get
                Return _desSubcliente
            End Get
            Set(value As String)
                _desSubcliente = value
            End Set
        End Property

        Public Property CodigoSubcliente() As String
            Get
                Return _codigoSubcliente
            End Get
            Set(value As String)
                _codigoSubcliente = value
            End Set
        End Property

        Public Property PtosServicio() As List(Of PuntoServicio)
            Get
                Return _ptosServicio
            End Get
            Set(value As List(Of PuntoServicio))
                _ptosServicio = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(Codigo As String, Descricao As String)

            _codigoSubcliente = Codigo
            _desSubcliente = Descricao

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
        Public Function ConvertToGetATMs() As ContractoServicio.GetATMs.Subcliente

            Dim objSubcliente As New ContractoServicio.GetATMs.Subcliente

            With objSubcliente
                .CodigoSubcliente = _codigoSubcliente
            End With

            Return objSubcliente

        End Function

        ''' <summary>
        ''' Retorna uma lista de objetos da operação GetATMs preenchido
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  10/01/2011  criado
        ''' </history>
        Public Function ConvertToGetATMs(Codigos As List(Of String)) As List(Of ContractoServicio.GetATMs.Subcliente)

            Dim lista As New List(Of ContractoServicio.GetATMs.Subcliente)
            Dim objSubcliente As ContractoServicio.GetATMs.Subcliente

            For Each cod In Codigos

                objSubcliente = New ContractoServicio.GetATMs.Subcliente
                objSubcliente.CodigoSubcliente = cod
                lista.Add(objSubcliente)

            Next

            Return lista

        End Function

        ''' <summary>
        ''' Converte uma lista de objetos ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
        ''' para uma lista de Negocio.Subcliente
        ''' </summary>
        ''' <param name="SubClientes"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  26/01/2011  criado
        ''' </history>
        Public Shared Function ConvertToList(SubClientes As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion) As List(Of Subcliente)

            Dim lista As New List(Of Negocio.Subcliente)
            Dim subCliente As Negocio.Subcliente

            For Each subcli As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente In SubClientes

                subCliente = New Negocio.Subcliente

                With subCliente
                    .CodigoSubcliente = subcli.Codigo
                    .DesSubcliente = subcli.Descripcion
                End With

                lista.Add(subCliente)

            Next

            Return lista

        End Function

        ''' <summary>
        ''' Converte uma lista de Negocio.Subcliente
        ''' para uma lista de objetos ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
        ''' </summary>
        ''' <param name="SubClientes"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  26/01/2011  criado
        ''' </history>
        Public Shared Function ConvertToSubClienteColeccion(SubClientes As List(Of Subcliente)) As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion

            Dim lista As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
            Dim objSubCliente As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente

            For Each subcli As Subcliente In SubClientes

                objSubCliente = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente

                With objSubCliente
                    .Codigo = subcli.CodigoSubcliente
                    .Descripcion = subcli.DesSubcliente
                End With

                lista.Add(objSubCliente)

            Next

            Return lista

        End Function

        Public Function ConvertToComboSubCliente() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente

            Dim objSubCliente As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente

            With objSubCliente
                .Codigo = _codigoSubcliente
                .Descripcion = _desSubcliente
            End With

            Return objSubCliente

        End Function

#End Region

    End Class

End Namespace