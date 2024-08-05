Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.GrupoCliente
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Genesis

''' <summary>
''' Classe AccionGrupoCliente
''' </summary>
''' <remarks></remarks>
''' <history>
''' [matheus.araujo] 24/10/2009 Criado
''' </history>
Public Class AccionGrupoCliente
    Implements ContractoServicio.IGrupoCliente

    Function GetGruposCliente(Peticion As ContractoServicio.GrupoCliente.GetGruposCliente.Peticion) As ContractoServicio.GrupoCliente.GetGruposCliente.Respuesta _
        Implements ContractoServicio.IGrupoCliente.GetGruposCliente

        ' objeto de resposta
        Dim objRespuesta As New ContractoServicio.GrupoCliente.GetGruposCliente.Respuesta

        Try


            ' obtém os dados do banco
            objRespuesta.GruposCliente = AccesoDatos.GrupoCliente.getGruposCliente(Peticion, objRespuesta.ParametrosPaginacion)

            ' retorna sem erro
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

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

    Function GetGruposClientesDetalle(Peticion As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion) As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta _
        Implements ContractoServicio.IGrupoCliente.GetGruposClientesDetalle

        ' Objeto de resposta
        Dim objRespuesta As New ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta

        Try

            ' Verifica se codCliente nao eh nulo
            If Peticion.Codigo Is Nothing OrElse Peticion.Codigo.Count = 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_codigo_vazio"))
            End If

            ' obtém os dados do banco
            objRespuesta.GrupoCliente = AccesoDatos.GrupoCliente.getGruposClientesDetalle(Peticion)

            ' retorna sem erro
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty


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

    Function SetGrupoCliente(Peticion As ContractoServicio.GrupoCliente.SetGruposCliente.Peticion) As ContractoServicio.GrupoCliente.SetGruposCliente.Respuesta _
        Implements ContractoServicio.IGrupoCliente.SetGrupoCliente

        ' objeto de resposta
        Dim objRespuesta As New ContractoServicio.GrupoCliente.SetGruposCliente.Respuesta

        Try
            ' verifica campos obrigatórios
            If String.IsNullOrEmpty(Peticion.GrupoCliente.Codigo) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_codigo_vazio"))
            End If

            If String.IsNullOrEmpty(Peticion.GrupoCliente.Descripcion) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_desc_vazio"))
            End If

            If Peticion.GrupoCliente.Clientes Is Nothing OrElse Peticion.GrupoCliente.Clientes.Count = 0 Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_cliente_vazio"))
            End If

            If String.IsNullOrEmpty(Peticion.GrupoCliente.CodigoUsuario) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("006_msg_codusuario_vazio"))
            End If

            Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
            Dim OidGrupoCliente As String = String.Empty

            AccesoDatos.GrupoCliente.SetGrupoCliente(Peticion, OidGrupoCliente, objTransacion)


            'Grava as Direcciones
            SetDirecciones(OidGrupoCliente, Peticion.GrupoCliente.Direccion, Peticion.GrupoCliente.CodigoUsuario, objTransacion)


            objTransacion.RealizarTransacao()

            objRespuesta.oidGrupoCliente = OidGrupoCliente
            objRespuesta.CodGrupoCliente = Peticion.GrupoCliente.Codigo
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT

        Catch ex As Excepcion.NegocioExcepcion

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

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
    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IGrupoCliente.Test
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

#Region "DIRECCIONES"

    Private Sub SetDirecciones(oidGrupoCliente As String, objDirecion As Direccion.DireccionBase, codigoUsuario As String, _
                               objTransacion As Transacao)

        If objDirecion IsNot Nothing Then

            With objDirecion

                Dim objDireciones As New ContractoServicio.Direccion.SetDirecciones.Peticion
                Dim codigotablaGenesis As String = String.Empty

                objDireciones.bolBaja = .bolBaja
                objDireciones.codFiscal = .codFiscal
                objDireciones.codPostal = .codPostal
                objDireciones.desCampoAdicional1 = .desCampoAdicional1
                objDireciones.desCampoAdicional2 = .desCampoAdicional2
                objDireciones.desCampoAdicional3 = .desCampoAdicional3
                objDireciones.desCategoriaAdicional1 = .desCategoriaAdicional1
                objDireciones.desCategoriaAdicional2 = .desCategoriaAdicional2
                objDireciones.desCategoriaAdicional3 = .desCategoriaAdicional3
                objDireciones.desCiudad = .desCiudad
                objDireciones.desDireccionLinea1 = .desDireccionLinea1
                objDireciones.desDireccionLinea2 = .desDireccionLinea2
                objDireciones.desEmail = .desEmail
                objDireciones.desNumeroTelefono = .desNumeroTelefono
                objDireciones.desPais = .desPais
                objDireciones.desProvincia = .desProvincia
                objDireciones.oidDireccion = .oidDireccion
                objDireciones.desUsuarioModificacion = codigoUsuario
                objDireciones.gmtModificacion = DateTime.Now
                If String.IsNullOrEmpty(objDireciones.desUsuarioCreacion) Then
                    objDireciones.desUsuarioCreacion = codigoUsuario
                    objDireciones.gmtCreacion = DateTime.Now
                End If

                objDireciones.oidTablaGenesis = oidGrupoCliente
                codigotablaGenesis = ContractoServicio.Constantes.COD_GRUPO_CLIENTE
                objDireciones.codTipoTablaGenesis = (From item In AccesoDatos.Constantes.MapeoEntidadesCodigoAjeno
                                                   Where item.CodTipoTablaGenesis = codigotablaGenesis
                                                   Select item.Entidade).FirstOrDefault()

                IAC.LogicaNegocio.AccionDireccion.EjecutarSetDireccion(objDireciones, String.Empty, String.Empty, objTransacion)


            End With

        End If

    End Sub
#End Region

End Class
