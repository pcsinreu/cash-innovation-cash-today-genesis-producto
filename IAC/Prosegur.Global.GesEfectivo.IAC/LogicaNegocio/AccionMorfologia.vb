Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class AccionMorfologia
    Implements ContractoServicio.IMorfologia

#Region "[MÉTODOS WS]"

    ''' <summary>
    ''' Esta operación es responsable por obtener los datos de las morfologías de acuerdo con los parámetros de entrada.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    Public Function GetMorfologia(Peticion As ContractoServicio.Morfologia.GetMorfologia.Peticion) As ContractoServicio.Morfologia.GetMorfologia.Respuesta Implements ContractoServicio.IMorfologia.GetMorfologia

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Morfologia.GetMorfologia.Respuesta

        Try

            objRespuesta.Morfologias = ObtenerMorfologias(Peticion.CodMorfologia, Peticion.DesMorfologia, Peticion.BolVigente)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function


    ''' <summary>
    ''' Esta operación es responsable por mantener las morfologías de acuerdo con los parámetros de 
    ''' entrada.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/12/2010 Criado
    ''' </history>
    Public Function SetMorfologia(Peticion As ContractoServicio.Morfologia.SetMorfologia.Peticion) As ContractoServicio.Morfologia.SetMorfologia.Respuesta Implements ContractoServicio.IMorfologia.SetMorfologia

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Morfologia.SetMorfologia.Respuesta

        Try

            ValidarPeticion(Peticion)

            If String.IsNullOrEmpty(Peticion.Morfologia.OidMorfologia) Then

                InsertarMorfologia(Peticion.Morfologia)

            ElseIf Peticion.Morfologia.BolBorrar Then

                AccesoDatos.Morfologia.BorrarMorfologia(Peticion.Morfologia.OidMorfologia, Nothing)

            Else

                ActualizarMorfologia(Peticion.Morfologia)

            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Esta operación es responsable por verificar la existencia de una morfología con el
    ''' codigo o descripción informados.
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 29/12/2010 Criado
    ''' </history>
    Public Function VerificarMorfologia(Peticion As ContractoServicio.Morfologia.VerificarMorfologia.Peticion) As ContractoServicio.Morfologia.VerificarMorfologia.Respuesta Implements ContractoServicio.IMorfologia.VerificarMorfologia

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.Morfologia.VerificarMorfologia.Respuesta

        Try

            Dim count As Integer = AccesoDatos.Morfologia.VerificarMorfologia(Peticion.CodigoMorfologia, Peticion.DescripcionMorfologia)

            objRespuesta.BolExiste = Convert.ToBoolean(count)

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception

            Util.TratarErroBugsnag(ex)
            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    ''' <summary>
    ''' Metodo Test
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2013 - Criado
    ''' </history>
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IMorfologia.Test
        Dim objRespuesta As New ContractoServicio.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("021_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

#End Region

    Private Shared Sub ValidarPeticion(Peticion As ContractoServicio.Morfologia.SetMorfologia.Peticion)

        If Peticion.Morfologia Is Nothing Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("022_msg_morfologia"))

        End If

        If Not Peticion.Morfologia.BolBorrar AndAlso Peticion.Morfologia.NecModalidadRecogida = 0 Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("022_msg_codmodalidad"))

        End If

        If Not String.IsNullOrEmpty(Peticion.Morfologia.OidMorfologia) OrElse Peticion.Morfologia.BolBorrar Then

            ' se for exclusão ou atualização, valida oidMorfologia

            If String.IsNullOrEmpty(Peticion.Morfologia.OidMorfologia) Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("022_msg_oidmorfologia"))

            End If

        End If

        ' se for inserção ou atualização, é obrigatório ter pelo menos 1 componente com 1 objeto
        If Not Peticion.Morfologia.BolBorrar Then

            If Peticion.Morfologia.Componentes Is Nothing OrElse Peticion.Morfologia.Componentes.Count = 0 Then

                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("022_msg_componente"))

                If (From c In Peticion.Morfologia.Componentes _
                    Where c.Objectos Is Nothing OrElse c.Objectos.Count = 0).FirstOrDefault() Is Nothing Then

                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("022_msg_objecto"))

                End If

            End If

        End If

    End Sub

    Private Shared Sub ValidarInsertarComponente(Componente As ContractoServicio.Morfologia.SetMorfologia.Componente)

        If String.IsNullOrEmpty(Componente.NecFuncionContenedor) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("022_msg_codfuncioncontenedor"))

        End If

        If String.IsNullOrEmpty(Componente.CodMorfologiaComponente) Then

            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("022_msg_codmorfologia"))

        End If

    End Sub

    Private Shared Sub InsertarMorfologia(Morfologia As ContractoServicio.Morfologia.SetMorfologia.Morfologia)

        Dim transacion As New Prosegur.DbHelper.Transacao(AccesoDatos.Constantes.CONEXAO_GE)
        Dim oidComponente As String
        Dim dataAtual As DateTime = DateTime.Now

        ' cria oid morfologia
        Morfologia.OidMorfologia = Guid.NewGuid().ToString()

        ' insere morfologia
        Morfologia.OidMorfologia = AccesoDatos.Morfologia.InsertMorfologia(Morfologia.CodMorfologia, Morfologia.DesMorfologia, Morfologia.BolVigente, _
                                                Morfologia.CodUsuario, dataAtual, Morfologia.NecModalidadRecogida, transacion)

        For Each comp In Morfologia.Componentes

            ValidarInsertarComponente(comp)

            oidComponente = AccesoDatos.Morfologia.InsertMorfologiaXComp(comp.CodMorfologiaComponente,
                                                                         Morfologia.OidMorfologia,
                                                                         comp.CodTipoContenedor,
                                                                         comp.DesTipoContenedor,
                                                                         comp.NecFuncionContenedor,
                                                                         comp.BolVigente,
                                                                         Morfologia.CodUsuario,
                                                                         dataAtual,
                                                                         comp.Orden,
                                                                         transacion)

            For Each obj In comp.Objectos

                InsertarObj(oidComponente, obj.CodIsoDivisa, obj.CodDenominacion, obj.CodMedioPago, obj.CodTipoMedioPago, obj.NecOrdenDivisa, obj.NecOrdenTipoMedPago, transacion)

            Next

        Next

        transacion.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Insere objeto
    ''' </summary>
    ''' <param name="OidComponente"></param>
    ''' <param name="CodIsoDivisa"></param>
    ''' <param name="CodDenominacion"></param>
    ''' <param name="CodMedioPago"></param>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="transacion"></param>
    ''' <remarks></remarks>
    Private Shared Sub InsertarObj(OidComponente As String, CodIsoDivisa As String, CodDenominacion As String, _
                            CodMedioPago As String, CodTipoMedioPago As String, NecOrdenDivisa As Integer, NecOrdenTipoMedioPago As Integer, _
                            ByRef transacion As DbHelper.Transacao)

        Dim oidDivisa As String = String.Empty
        Dim oidDenominacion As String = String.Empty
        Dim oidMedioPago As String = String.Empty

        If Not String.IsNullOrEmpty(CodIsoDivisa) Then
            ' obtém oidDivisa
            oidDivisa = AccesoDatos.Divisa.ObterOidDivisa(CodIsoDivisa)
        End If

        If Not String.IsNullOrEmpty(CodDenominacion) AndAlso Not String.IsNullOrEmpty(oidDivisa) Then
            ' obtém oidDivisa
            oidDenominacion = AccesoDatos.Denominacion.ObterOidDenominacion(oidDivisa, CodDenominacion)
        End If

        If Not String.IsNullOrEmpty(CodMedioPago) AndAlso Not String.IsNullOrEmpty(CodIsoDivisa) AndAlso _
        Not String.IsNullOrEmpty(CodTipoMedioPago) Then
            ' obtém oidMedioPago
            oidMedioPago = AccesoDatos.MedioPago.ObterOidMedioPago(CodIsoDivisa, CodMedioPago, CodTipoMedioPago)
        End If

        AccesoDatos.Morfologia.InsertComponenteXObj(OidComponente, oidDivisa, oidDenominacion, oidMedioPago, NecOrdenDivisa, NecOrdenTipoMedioPago, transacion)

    End Sub

    Private Shared Sub ActualizarMorfologia(Morfologia As ContractoServicio.Morfologia.SetMorfologia.Morfologia)

        Dim transacion As New Prosegur.DbHelper.Transacao(AccesoDatos.Constantes.CONEXAO_GE)
        Dim oidComponente As String

        ' modifica dados
        AccesoDatos.Morfologia.ActualizarMorfologia(Morfologia.OidMorfologia, Morfologia.CodMorfologia, Morfologia.DesMorfologia, _
                                                    Morfologia.BolVigente, Morfologia.CodUsuario, DateTime.Now, transacion)

        ' deletar componentes da morfologia
        AccesoDatos.Morfologia.BorrarMorfologiaXComp(Morfologia.OidMorfologia, transacion)

        ' insere novamente os componentes
        For Each comp In Morfologia.Componentes

            oidComponente = New Guid().ToString()

            ValidarInsertarComponente(comp)

            oidComponente = AccesoDatos.Morfologia.InsertMorfologiaXComp(comp.CodMorfologiaComponente,
                                                                         Morfologia.OidMorfologia,
                                                                         comp.CodTipoContenedor,
                                                                         comp.DesTipoContenedor,
                                                                         comp.NecFuncionContenedor,
                                                                         comp.BolVigente,
                                                                         Morfologia.CodUsuario,
                                                                         DateTime.Now,
                                                                         comp.Orden,
                                                                         transacion)

            ' insere os objetos do componente
            For Each obj In comp.Objectos

                InsertarObj(oidComponente, obj.CodIsoDivisa, obj.CodDenominacion, obj.CodMedioPago, obj.CodTipoMedioPago, obj.NecOrdenDivisa, obj.NecOrdenTipoMedPago, _
                            transacion)

            Next

        Next

        transacion.RealizarTransacao()

    End Sub


    ''' <summary>
    ''' Obtém morfologias de acordo com os parametros informados
    ''' </summary>
    ''' <param name="CodMorfologia"></param>
    ''' <param name="DesMorfologia"></param>
    ''' <param name="BolVigente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  30/12/2010  criado
    ''' </history>
    Private Shared Function ObtenerMorfologias(CodMorfologia As String, DesMorfologia As String, BolVigente As Boolean) As List(Of ContractoServicio.Morfologia.GetMorfologia.Morfologia)

        Dim morf As ContractoServicio.Morfologia.GetMorfologia.Morfologia = Nothing
        Dim comp As ContractoServicio.Morfologia.GetMorfologia.Componente
        Dim listaMorfologias As New List(Of ContractoServicio.Morfologia.GetMorfologia.Morfologia)
        Dim oidMorfologia As String = String.Empty

        Dim dt As DataTable = Prosegur.Global.GesEfectivo.IAC.AccesoDatos.Morfologia.GetMorfologias(CodMorfologia, DesMorfologia, BolVigente)

        For Each row In dt.Rows

            oidMorfologia = row("OID_MORFOLOGIA")

            ' verifica se a morfologia já foi criada
            If (From m In listaMorfologias Where m.OidMorfologia = oidMorfologia).FirstOrDefault() Is Nothing Then

                ' cria morfologia
                morf = New ContractoServicio.Morfologia.GetMorfologia.Morfologia

                With morf
                    .BolVigente = Convert.ToBoolean(row("BOL_VIGENTE"))
                    .CodMorfologia = row("COD_MORFOLOGIA")
                    .DesMorfologia = row("DES_MORFOLOGIA")
                    .FyhActualizacion = row("FYH_ACTUALIZACION")
                    .OidMorfologia = row("OID_MORFOLOGIA")
                    .Componentes = New List(Of ContractoServicio.Morfologia.GetMorfologia.Componente)
                End With

                listaMorfologias.Add(morf)

            End If

            ' cria e preenche componente
            comp = New ContractoServicio.Morfologia.GetMorfologia.Componente

            If Not IsDBNull(row("NEC_FUNCION_CONTENEDOR")) Then
                comp.necFuncionContenedor = row("NEC_FUNCION_CONTENEDOR")
            End If

            If Not IsDBNull(row("NEC_MODALIDAD_RECOGIDA")) Then
                morf.NecModalidadRecogida = row("NEC_MODALIDAD_RECOGIDA")
            End If

            With comp
                .BolVigente = row("BOL_VIGENTE")
                .OidMorfologiaComponente = Util.VerificarDBNull(row("OID_MORFOLOGIA_COMPONENTE"))
                .CodMorfologiaComponente = Util.VerificarDBNull(row("COD_MORFOLOGIA_COMPONENTE"))
                .CodTipoContenedor = Util.VerificarDBNull(row("COD_TIPO_CONTENEDOR"))
                .Orden = Util.VerificarDBNull(row("NEC_ORDEN"))
            End With

            ' adiciona componente a lista da morfologia corrente
            morf.Componentes.Add(comp)

        Next

        Return listaMorfologias

    End Function

End Class
