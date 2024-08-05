Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.NuevoSalidas.Remesa
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon

Namespace NuevoSalidas

    Public Class Objeto

        ''' <summary>
        ''' Recupera objetos da remesa
        ''' </summary>
        ''' <param name="IdentificadoresRemesas"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarObjetosRemesas(IdentificadoresRemesas As List(Of String)) As Dictionary(Of String, ObservableCollection(Of Clases.Objeto))

            If IdentificadoresRemesas Is Nothing OrElse IdentificadoresRemesas.Count = 0 Then
                Return Nothing
            End If

            Dim objObjetosRemesas As Dictionary(Of String, ObservableCollection(Of Clases.Objeto)) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = My.Resources.Salidas_Objetos_Recuperar_Remesas
            cmd.CommandType = CommandType.Text

            Dim query As New System.Text.StringBuilder
            query.Append(Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresRemesas, "OID_REMESA", cmd, "WHERE", "O"))

            cmd.CommandText = String.Format(cmd.CommandText, query)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objObjetosRemesas = New Dictionary(Of String, ObservableCollection(Of Clases.Objeto))
                Dim objobjetoRemesa As KeyValuePair(Of String, ObservableCollection(Of Clases.Objeto))
                Dim IdentificadorRemesa As String = String.Empty

                For Each dr In dt.Rows

                    IdentificadorRemesa = Util.AtribuirValorObj(dr("OID_REMESA"), GetType(String))

                    objobjetoRemesa = (From objRem In objObjetosRemesas Where objRem.Key = IdentificadorRemesa).FirstOrDefault

                    If String.IsNullOrEmpty(objobjetoRemesa.Key) Then

                        objObjetosRemesas.Add(IdentificadorRemesa, New ObservableCollection(Of Clases.Objeto))
                        objobjetoRemesa = (From objRem In objObjetosRemesas Where objRem.Key = IdentificadorRemesa).FirstOrDefault

                    End If
                    objobjetoRemesa.Value.Add(New Clases.Objeto With {.Identificador = Util.AtribuirValorObj(dr("OID_OBJETO"), GetType(String)), _
                                                           .IdentificadorRemesa = IdentificadorRemesa, _
                                                           .IdentificadorMorfologiaComponente = Util.AtribuirValorObj(dr("OID_MORFOLOGIA_COMPONENTE"), GetType(String)), _
                                                           .TipoObjeto = Extenciones.RecuperarEnum(Of Enumeradores.TipoObjeto)(Util.AtribuirValorObj(dr("TIPO_OBJETO"), GetType(String))), _
                                                           .CodigoIdentificador = Util.AtribuirValorObj(dr("COD_IDENTIFICADOR"), GetType(String)), _
                                                           .FechaActualizacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(Date)), _
                                                           .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))})

                Next

            End If

            Return objObjetosRemesas
        End Function

        ''' <summary>
        ''' Recupera objetos da remesa
        ''' </summary>
        ''' <param name="IdentificadorRemesa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarObjetos(IdentificadorRemesa As String) As ObservableCollection(Of Clases.Objeto)

            Dim objObjetos As ObservableCollection(Of Clases.Objeto) = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Objeto_Recuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, IdentificadorRemesa))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objObjetos = New ObservableCollection(Of Clases.Objeto)()

                For Each dr In dt.Rows

                    objObjetos.Add(New Clases.Objeto With {.Identificador = Util.AtribuirValorObj(dr("OID_OBJETO"), GetType(String)), _
                                                           .IdentificadorRemesa = IdentificadorRemesa, _
                                                           .IdentificadorMorfologiaComponente = Util.AtribuirValorObj(dr("OID_MORFOLOGIA_COMPONENTE"), GetType(String)), _
                                                           .TipoObjeto = Extenciones.RecuperarEnum(Of Enumeradores.TipoObjeto)(Util.AtribuirValorObj(dr("TIPO_OBJETO"), GetType(String))), _
                                                           .CodigoIdentificador = Util.AtribuirValorObj(dr("COD_IDENTIFICADOR"), GetType(String)), _
                                                           .FechaActualizacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(Date)), _
                                                           .CodigoUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String))})

                Next

            End If

            Return objObjetos
        End Function

        Public Shared Sub Insertar(objetos As ObservableCollection(Of Clases.Objeto), Optional ByRef Transaccion As Transacao = Nothing)

            For Each Objeto In objetos

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Objeto_Insertar)
                    cmd.CommandType = CommandType.Text

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_OBJETO", ProsegurDbType.Objeto_Id, Objeto.Identificador))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_REMESA", ProsegurDbType.Objeto_Id, Objeto.IdentificadorRemesa))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "OID_MORFOLOGIA_COMPONENTE", ProsegurDbType.Objeto_Id, Objeto.IdentificadorMorfologiaComponente))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "TIPO_OBJETO", ProsegurDbType.Descricao_Curta, Extenciones.RecuperarValor(Objeto.TipoObjeto)))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Longa, Objeto.CodigoIdentificador))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_USUARIO", ProsegurDbType.Descricao_Curta, Objeto.CodigoUsuario))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, Objeto.FechaActualizacion))

                    If Transaccion IsNot Nothing Then
                        Transaccion.AdicionarItemTransacao(cmd)
                    Else
                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
                    End If

                End Using

            Next Objeto

        End Sub

        Public Shared Sub Borrar(IdentificadoresObjeto As List(Of String), Optional ByRef Transaccion As Transacao = Nothing)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_Objeto_Borrar)
                cmd.CommandType = CommandType.Text

                If IdentificadoresObjeto IsNot Nothing AndAlso IdentificadoresObjeto.Count > 0 Then

                    cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_SALIDAS, IdentificadoresObjeto, "OID_OBJETO", cmd, "WHERE", "O"))

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, cmd.CommandText)

                    If Transaccion IsNot Nothing Then
                        Transaccion.AdicionarItemTransacao(cmd)
                    Else
                        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALIDAS, cmd)
                    End If

                End If

            End Using

        End Sub


    End Class

End Namespace