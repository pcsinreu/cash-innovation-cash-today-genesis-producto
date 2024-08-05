Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel

Public Class Periodo

#Region "[CONSULTAS]"

#End Region

#Region "[INSERIR]"

#End Region

#Region "[UPDATE]"

    Public Shared Function BorrarPeriodos(oidMaquina As String, oidPlanificacion As String, codigosEstadosPeriodos As List(Of String), objTransacao As Transacao, Optional quitarVigente As Boolean = True) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim qry = Util.PrepararQuery(My.Resources.BorrarPeriodos)
        Dim where As String = String.Empty
        comando.CommandType = CommandType.Text
        Dim whereCodigosEstadosPeriodos As String = String.Empty

        If codigosEstadosPeriodos IsNot Nothing AndAlso codigosEstadosPeriodos.Count > 0 Then
            Dim i As Integer = 0
            Dim largo As Integer = codigosEstadosPeriodos.Count - 1
            whereCodigosEstadosPeriodos = String.Format("{0}{1}", whereCodigosEstadosPeriodos, " ( ")
            For Each codigoEstadoPeriodo As String In codigosEstadosPeriodos
                If i = largo Then
                    whereCodigosEstadosPeriodos = String.Format("{0} '{1}'", whereCodigosEstadosPeriodos, codigoEstadoPeriodo)
                Else

                    whereCodigosEstadosPeriodos = String.Format("{0} '{1}',", whereCodigosEstadosPeriodos, codigoEstadoPeriodo)
                End If
                i += 1
            Next

            whereCodigosEstadosPeriodos = String.Format("{0}{1}", whereCodigosEstadosPeriodos, " )")
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificacion))

        If Not quitarVigente Then
            where = " and fyh_inicio > sysdate "
        End If

        comando.CommandText = String.Format(qry, whereCodigosEstadosPeriodos, where)
        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function

    Public Shared Function BorrarPeriodosAFuturo(oidMaquina As String, oidPlanificacion As String, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim qry = Util.PrepararQuery(My.Resources.BorrarPeriodosAFuturo)

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificacion))



        comando.CommandText = String.Format(qry)
        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function
    Public Shared Function ActualizarPeriodosFV_Confirmacion(oidMaquina As String, oidPlanificacion As String, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim qry = Util.PrepararQuery(My.Resources.ActualizarPeriodosFV_Confirmacion)

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificacion))



        comando.CommandText = String.Format(qry)
        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function

    Public Shared Function BorrarCalculoMedioPagoAFuturo(oidMaquina As String, oidPlanificacion As String, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim qry = Util.PrepararQuery(My.Resources.BorrarCalculoMedioPagoAFuturo)

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificacion))



        comando.CommandText = String.Format(qry)
        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function

    Public Shared Function BorrarPeriodosPorDocumentoAFuturo(oidMaquina As String, oidPlanificacion As String, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim qry = Util.PrepararQuery(My.Resources.BorrarPeriodosPorDocumentoAFuturo)

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificacion))



        comando.CommandText = String.Format(qry)
        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function

    Public Shared Function BorrarSaldosAFuturo(oidMaquina As String, oidPlanificacion As String, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim qry = Util.PrepararQuery(My.Resources.BorrarSaldosAFuturo)

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificacion))



        comando.CommandText = String.Format(qry)
        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function

    Public Shared Function BorrarPeriodoRelacionAFuturo(oidMaquina As String, oidPlanificacion As String, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim qry = Util.PrepararQuery(My.Resources.BorrarPeriodoRelacionAFuturo)

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificacion))



        comando.CommandText = String.Format(qry)
        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function

    Public Shared Function BorrarPeriodoRelacionadoAFuturo(oidMaquina As String, oidPlanificacion As String, objTransacao As Transacao) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        Dim qry = Util.PrepararQuery(My.Resources.BorrarPeriodoRelacionadoAFuturo)

        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MAQUINA", ProsegurDbType.Objeto_Id, oidMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PLANIFICACION", ProsegurDbType.Objeto_Id, oidPlanificacion))



        comando.CommandText = String.Format(qry)
        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)
        End If

        Return oidMaquina

    End Function

#End Region

#Region "[DELETE]"

#End Region

#Region "[DEMAIS]"

#End Region

End Class
