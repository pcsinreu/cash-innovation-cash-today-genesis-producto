Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos
    ''' <summary>
    ''' Classe de AccionTransaccion.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 04/09/2013 - Criado
    ''' </history>

    Public Class AccionTransaccion
#Region "[CONSULTAS]"
        ''' <summary>
        ''' Recupera os estados das acciones contables 
        ''' </summary>
        ''' <param name="identificadorAccionContable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [claudioniz.pereira] 04/09/2013 - Criado
        ''' </history>
        Public Shared Function ObtenerAccionesTransacciones(identificadorAccionContable As String) As List(Of Clases.AccionTransaccion)

            Dim listaAcciones As List(Of Clases.AccionTransaccion)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AccionTransaccionRecuperarPorAccionContable)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ACCION_CONTABLE", ProsegurDbType.Objeto_Id, identificadorAccionContable))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            listaAcciones = New List(Of Clases.AccionTransaccion)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows

                    If row("COD_ACCION_ORIGEN_DISPONIBLE") <> "0" Then

                        Dim accion_ORIGEN_DISPONIBLE As New Clases.AccionTransaccion
                        accion_ORIGEN_DISPONIBLE.Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO"))
                        accion_ORIGEN_DISPONIBLE.TipoSaldo = Enumeradores.TipoSaldo.Disponible
                        accion_ORIGEN_DISPONIBLE.TipoSitio = Enumeradores.TipoSitio.Origen
                        If row("COD_ACCION_ORIGEN_DISPONIBLE") = "+" Then
                            accion_ORIGEN_DISPONIBLE.TipoMovimiento = Enumeradores.TipoMovimiento.Ingreso
                        Else
                            accion_ORIGEN_DISPONIBLE.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso
                        End If

                        listaAcciones.Add(accion_ORIGEN_DISPONIBLE)
                    End If

                    If row("COD_ACCION_ORIGEN_NODISP") <> "0" Then

                        Dim accion_ORIGEN_NODISP As New Clases.AccionTransaccion
                        accion_ORIGEN_NODISP.Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO"))
                        accion_ORIGEN_NODISP.TipoSaldo = Enumeradores.TipoSaldo.NoDisponible
                        accion_ORIGEN_NODISP.TipoSitio = Enumeradores.TipoSitio.Origen
                        If row("COD_ACCION_ORIGEN_NODISP") = "+" Then
                            accion_ORIGEN_NODISP.TipoMovimiento = Enumeradores.TipoMovimiento.Ingreso
                        Else
                            accion_ORIGEN_NODISP.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso
                        End If

                        listaAcciones.Add(accion_ORIGEN_NODISP)
                    End If

                    If row("COD_ACCION_DESTINO_DISPONIBLE") <> "0" Then

                        Dim accion_DESTINO_DISPONIBLE As New Clases.AccionTransaccion
                        accion_DESTINO_DISPONIBLE.Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO"))
                        accion_DESTINO_DISPONIBLE.TipoSaldo = Enumeradores.TipoSaldo.Disponible
                        accion_DESTINO_DISPONIBLE.TipoSitio = Enumeradores.TipoSitio.Destino
                        If row("COD_ACCION_DESTINO_DISPONIBLE") = "+" Then
                            accion_DESTINO_DISPONIBLE.TipoMovimiento = Enumeradores.TipoMovimiento.Ingreso
                        Else
                            accion_DESTINO_DISPONIBLE.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso
                        End If

                        listaAcciones.Add(accion_DESTINO_DISPONIBLE)
                    End If

                    If row("COD_ACCION_DESTINO_NODISP") <> "0" Then

                        Dim accion_DESTINO_NODISP As New Clases.AccionTransaccion
                        accion_DESTINO_NODISP.Estado = Extenciones.RecuperarEnum(Of Enumeradores.EstadoDocumento)(row("COD_ESTADO"))
                        accion_DESTINO_NODISP.TipoSaldo = Enumeradores.TipoSaldo.NoDisponible
                        accion_DESTINO_NODISP.TipoSitio = Enumeradores.TipoSitio.Destino
                        If row("COD_ACCION_DESTINO_NODISP") = "+" Then
                            accion_DESTINO_NODISP.TipoMovimiento = Enumeradores.TipoMovimiento.Ingreso
                        Else
                            accion_DESTINO_NODISP.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso
                        End If

                        listaAcciones.Add(accion_DESTINO_NODISP)
                    End If
                Next
            End If

            Return listaAcciones
        End Function
#End Region
    End Class
End Namespace



