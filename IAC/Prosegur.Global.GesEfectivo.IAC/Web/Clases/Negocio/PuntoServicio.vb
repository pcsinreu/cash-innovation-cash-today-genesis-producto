Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    ''' <summary>
    ''' Classe de negócio Punto Servicio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/01/2011 criado
    ''' </history>
    <Serializable()> _
    Public Class PuntoServicio
        Inherits BaseEntidade

#Region "[Variáveis]"

        Private _oidPuntoServicio As String
        Private _desPuntoServicio As String
        Private _codigoPuntoServicio As String

#End Region

#Region "[Propriedades]"

        Public Property OidPuntoServicio() As String
            Get
                Return _oidPuntoServicio
            End Get
            Set(value As String)
                _oidPuntoServicio = value
            End Set
        End Property

        Public Property DesPuntoServicio() As String
            Get
                Return _desPuntoServicio
            End Get
            Set(value As String)
                _desPuntoServicio = value
            End Set
        End Property

        Public Property CodigoPuntoServicio() As String
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As String)
                _codigoPuntoServicio = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(OidPtoServicio As String, Codigo As String, Descricao As String)

            _oidPuntoServicio = OidPtoServicio
            _codigoPuntoServicio = Codigo
            _desPuntoServicio = Descricao

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
        Public Function ConvertToGetATMs() As ContractoServicio.GetATMs.PuntoServicio

            Dim objPtoServ As New ContractoServicio.GetATMs.PuntoServicio

            With objPtoServ
                .CodigoPuntoServicio = _codigoPuntoServicio
            End With

            Return objPtoServ

        End Function

        ''' <summary>
        ''' Retorna uma lista de objetosda operação GetATMs preenchido
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  10/01/2011  criado
        ''' </history>
        Public Function ConvertToGetATMs(Codigos As List(Of String)) As List(Of ContractoServicio.GetATMs.PuntoServicio)

            Dim lista As New List(Of ContractoServicio.GetATMs.PuntoServicio)
            Dim objPtoServ As ContractoServicio.GetATMs.PuntoServicio

            For Each cod In Codigos

                objPtoServ = New ContractoServicio.GetATMs.PuntoServicio
                objPtoServ.CodigoPuntoServicio = cod
                lista.Add(objPtoServ)

            Next

            Return lista

        End Function

        ''' <summary>
        ''' converte uma lista ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
        ''' para uma lista de objetos PuntoServicio
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  26/01/2011  criado
        ''' </history>
        Public Shared Function ConvertToList(PtosServicio As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion) _
        As List(Of PuntoServicio)

            Dim lista As New List(Of PuntoServicio)
            Dim pto As PuntoServicio

            For Each ptoServUtilidad In PtosServicio

                pto = New PuntoServicio

                With pto
                    .CodigoPuntoServicio = ptoServUtilidad.Codigo
                    .DesPuntoServicio = ptoServUtilidad.Descripcion
                End With

                lista.Add(pto)

            Next

            Return lista

        End Function

        Public Function ConvertToComboPtosServicio() As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

            Dim obj As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

            With obj
                .Codigo = _codigoPuntoServicio
                .Descripcion = _desPuntoServicio
            End With

            Return obj

        End Function

#End Region

    End Class

End Namespace