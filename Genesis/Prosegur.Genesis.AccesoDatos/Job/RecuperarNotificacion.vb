Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Genesis.ContractoServicio.Contractos.Job.EnviarNotificacion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Notification.Nilo

Public Class RecuperarNotificaciones
    Public Shared Function Ejecutar(identificadorLlamada As String, peticion As Peticion, identificadorIntegracion As String, log As StringBuilder) As Request
        Try
            If log Is Nothing Then log = New StringBuilder
            Dim TiempoParcial As DateTime = Now

            Dim respuesta As New Request()

            Dim ds As DataSet = Nothing
            Dim spw As SPWrapper = Nothing
            TiempoParcial = Now
            spw = ColectarPeticion(identificadorLlamada, peticion, identificadorIntegracion)
            log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

            TiempoParcial = Now
            ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
            log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

            TiempoParcial = Now
            respuesta = PoblarRespuesta(ds)

            spw = Nothing
            ds.Dispose()

            log.AppendLine("Tiempo de acceso a datos (Poblar objecto de respuesta): " & Now.Subtract(TiempoParcial).ToString() & "; ")

            Return respuesta
        Catch ex As Exception
            Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

            If Not String.IsNullOrEmpty(MsgErroTratado) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                     MsgErroTratado)
            Else
                Throw ex
            End If
        End Try
        Return Nothing
    End Function

    Private Shared Function PoblarRespuesta(ds As DataSet) As Request
        Dim unaNotificacion As Request = Nothing
        If ds IsNot Nothing AndAlso ds.Tables.Contains("datos") AndAlso ds.Tables("datos").Rows.Count > 0 Then
            unaNotificacion = New Request()
            Dim fila = ds.Tables("datos").Rows(0)


            unaNotificacion.IdTran = Util.AtribuirValorObj(fila("idTran"), GetType(String))
            unaNotificacion.Integration = Util.AtribuirValorObj(fila("integracion"), GetType(String))
            unaNotificacion.Source = "genesis-producto"
            unaNotificacion.DateTime = Util.AtribuirValorObj(fila("dateTime"), GetType(DateTime))

            unaNotificacion.Context = New Context()
            unaNotificacion.Context.Country = Util.AtribuirValorObj(fila("country"), GetType(String))
            unaNotificacion.Context.Region = Util.AtribuirValorObj(fila("region"), GetType(String))

            unaNotificacion.Object = New [Object]()
            unaNotificacion.Object.SourceId = Util.AtribuirValorObj(fila("sourceId"), GetType(String))
            unaNotificacion.Object.Operation = Util.AtribuirValorObj(fila("operation"), GetType(String))
            unaNotificacion.Object.Attributes = PoblarAtributos(ds)

        End If

        Return unaNotificacion
    End Function

    Private Shared Function PoblarAtributos(ds As DataSet) As List(Of Attribute)

        Dim atributos As New List(Of Attribute)
        Dim objAtributo As Attribute
        For Each fila As DataRow In ds.Tables("datos").Rows
            objAtributo = New Attribute With {
                .Name = Util.AtribuirValorObj(fila("llave"), GetType(String)),
                .Value = Util.AtribuirValorObj(fila("valor"), GetType(String))
            }

            atributos.Add(objAtributo)
        Next


        Return atributos
    End Function

    Private Shared Function ColectarPeticion(identificadorLlamada As String, peticion As Peticion, identificadorIntegracion As String) As SPWrapper
        Dim SP As String = String.Format("SAPR_PSALDOS_{0}.srecuperar_notificacion", Prosegur.Genesis.Comon.Util.Version)
        Dim spw As New SPWrapper(SP, False)

        spw.AgregarParam("par$oid_llamada", ProsegurDbType.Descricao_Curta, identificadorLlamada)
        spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais)
        spw.AgregarParam("par$oid_integracion", ProsegurDbType.Descricao_Curta, identificadorIntegracion)
        spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno)
        spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, peticion.Configuracion.Usuario)
        spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Curta, Util.GetCultureUser())


        spw.AgregarParam("par$rc_datos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datos")

        Return spw

    End Function
End Class
