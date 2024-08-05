Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Transactions
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Saldo
Imports System.Data
Imports Prosegur.DbHelper

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe responsável por buscar los reportes de certificación configurados en la base de datos
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FormulariosCertificados

#Region "RecuperarFormularios"

        Public Shared Function ObtenerFormulariosCertificados(_peticion As Prosegur.Genesis.Comon.Peticion(Of Clases.ConfiguracionReporte)) As Comon.Respuesta(Of List(Of DataRow))
            Dim _respuesta As New Comon.Respuesta(Of List(Of DataRow))
            Try
                _respuesta.Retorno = AccesoDatos.GenesisSaldos.Certificacion.Formularios.RetornarFormulariosCertificados(_peticion, _respuesta)

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
            Return _respuesta
        End Function

        Public Shared Function ObtenerConfiguracionesReporte(IdentificadorCliente As String,
                                                             IdentificadorTipoCliente As String,
                                                             TipoReporte As List(Of Enumeradores.TipoReporte)) As List(Of Comon.Clases.ConfiguracionReporte)

            Return AccesoDatos.GenesisSaldos.Certificacion.Formularios.RecuperarConfiguracionReporte(IdentificadorCliente, IdentificadorTipoCliente, TipoReporte)

        End Function

        Public Shared Function ObtenerConfiguracionReporte(objIdentificador As String, Optional ByRef ConfigPosibles As ObservableCollection(Of Clases.ConfiguracionReporte) = Nothing) As Clases.ConfiguracionReporte

            If ConfigPosibles IsNot Nothing AndAlso ConfigPosibles.Count > 0 AndAlso ConfigPosibles.FirstOrDefault(Function(x) x.Identificador = objIdentificador) IsNot Nothing Then

                Return ConfigPosibles.FirstOrDefault(Function(x) x.Identificador = objIdentificador)

            Else

                If ConfigPosibles Is Nothing Then
                    ConfigPosibles = New ObservableCollection(Of Clases.ConfiguracionReporte)
                End If

                Dim objIdentificadoresConfigReportes As New List(Of String)
                objIdentificadoresConfigReportes.Add(objIdentificador)

                Dim objConfiguraciones As ObservableCollection(Of Clases.ConfiguracionReporte) = ObtenerConfiguracionReporte(objIdentificadoresConfigReportes)

                If objConfiguraciones IsNot Nothing AndAlso objConfiguraciones.Count > 0 Then
                    ConfigPosibles.Add(objConfiguraciones(0))

                    Return objConfiguraciones(0)
                End If
            End If

            Return Nothing
        End Function

        Public Shared Function ObtenerTipoClientesReporte(Identificador As String) As ObservableCollection(Of Clases.TipoCliente)
            Dim objTiposClientes As New ObservableCollection(Of Clases.TipoCliente)

            Dim tdTiposClientes As DataTable = AccesoDatos.GenesisSaldos.Certificacion.Formularios.ObtenerTipoClientesReporte(Identificador)
            If tdTiposClientes IsNot Nothing AndAlso tdTiposClientes.Rows.Count > 0 Then
                For Each objRow In tdTiposClientes.Rows

                    With objTiposClientes
                        If objRow("OID_TIPO_CLIENTE") IsNot Nothing AndAlso objRow("OID_TIPO_CLIENTE") <> String.Empty Then
                            .Add(New Clases.TipoCliente With {.Identificador = objRow("OID_TIPO_CLIENTE")})
                        End If
                    End With
                Next

            End If

            Return objTiposClientes

        End Function

        Public Shared Function ObtenerClientesReporte(Identificador As String) As ObservableCollection(Of Clases.Cliente)
            Dim objClientes As New ObservableCollection(Of Clases.Cliente)

            Dim tdClientes As DataTable = AccesoDatos.GenesisSaldos.Certificacion.Formularios.ObtenerClientesReporte(Identificador)
            If tdClientes IsNot Nothing AndAlso tdClientes.Rows.Count > 0 Then
                For Each objRow In tdClientes.Rows

                    With objClientes
                        If objRow("OID_CLIENTE") IsNot Nothing AndAlso objRow("OID_CLIENTE") <> String.Empty Then
                            .Add(New Clases.Cliente With {.Identificador = objRow("OID_CLIENTE")})
                        End If
                    End With
                Next

            End If

            Return objClientes

        End Function

        Public Shared Function ObtenerConfiguracionReporte(objIdentificadores As List(Of String)) As ObservableCollection(Of Clases.ConfiguracionReporte)
            Dim tdConfiguracionReporte As DataTable = AccesoDatos.GenesisSaldos.Certificacion.Formularios.ObtenerConfiguracionesReportes(objIdentificadores)
            Return cargarConfigReportes(tdConfiguracionReporte)
        End Function


        Public Shared Function cargarConfigReportes(tdConfigReporte As DataTable) As ObservableCollection(Of Clases.ConfiguracionReporte)
            Dim objConfReporte As New ObservableCollection(Of Clases.ConfiguracionReporte)

            If tdConfigReporte IsNot Nothing AndAlso tdConfigReporte.Rows.Count > 0 Then

                Dim IdentificadorConfiguracionReporte As String = String.Empty
                Dim objConfiguracionReporte As Clases.ConfiguracionReporte = Nothing

                For Each objRow In tdConfigReporte.Rows

                    IdentificadorConfiguracionReporte = Util.AtribuirValorObj(objRow("OID_CONFIG_REPORTE"), GetType(String))

                    objConfiguracionReporte = (From cr In objConfReporte Where cr.Identificador = IdentificadorConfiguracionReporte).FirstOrDefault

                    If objConfiguracionReporte Is Nothing Then

                        Dim confReporte As New Comon.Clases.ConfiguracionReporte

                        With confReporte
                            .Identificador = Util.AtribuirValorObj(objRow("OID_CONFIG_REPORTE"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(objRow("COD_CONFIG_REPORTE"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(objRow("DES_CONFIG_REPORTE"), GetType(String))
                            .TiposClientes = New ObservableCollection(Of Clases.TipoCliente)
                            .Direccion = Util.AtribuirValorObj(objRow("DES_DIRECCION"), GetType(String))
                            .UsuarioCreacion = Util.AtribuirValorObj(objRow("DES_USUARIO_CREACION"), GetType(String))
                            .TipoReporte = Extenciones.RecuperarEnum(Of Comon.Enumeradores.TipoReporte)(Util.AtribuirValorObj(objRow("NEL_TIPO_REPORTE"), GetType(String)))
                            .MascaraNombre = Util.AtribuirValorObj(objRow("COD_MASCARA_NOMBRE"), GetType(String))
                            If objRow("COD_RENDERIZADOR") IsNot DBNull.Value Then
                                .CodigoRedenrizador = [Enum].Parse(GetType(Enumeradores.TipoRenderizador), Util.AtribuirValorObj(objRow("COD_RENDERIZADOR"), GetType(String)))
                            End If
                            .DescripcionExtension = Util.AtribuirValorObj(objRow("DES_EXTENSION"), GetType(String))
                            .DescripcionSeparador = Util.AtribuirValorObj(objRow("DES_SEPARADOR"), GetType(String))
                            .ParametrosReporte = New ObservableCollection(Of Clases.ParametroReporte)
                        End With

                        objConfReporte.Add(confReporte)

                        objConfiguracionReporte = (From cr In objConfReporte Where cr.Identificador = IdentificadorConfiguracionReporte).FirstOrDefault

                    End If

                    objConfiguracionReporte.ParametrosReporte.Add(New Comon.Clases.ParametroReporte With { _
                                                                  .Identificador = Util.AtribuirValorObj(objRow("OID_TIPO_REPORTEXPARAMETRO"), GetType(String)), _
                                                                  .Codigo = Util.AtribuirValorObj(objRow("COD_PARAMETRO"), GetType(String)), _
                                                                  .Descripcion = Util.AtribuirValorObj(objRow("DES_PARAMETRO"), GetType(String))})
                Next

            End If

            Return objConfReporte
        End Function


        Public Shared Function InserirFormulariosCertificado(_peticion As Prosegur.Genesis.Comon.Clases.ConfiguracionReporte) As String
            Dim _respuesta As String = String.Empty
            Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GENESIS)

            Try
                If (_peticion.TiposClientes Is Nothing OrElse _peticion.TiposClientes.Count = 0) AndAlso _
                   (_peticion.Clientes Is Nothing OrElse _peticion.Clientes.Count = 0) Then
                    Throw New Excepcion.NegocioExcepcion("Erro")
                End If

                _respuesta = AccesoDatos.GenesisSaldos.Certificacion.Formularios.InserirFormulariosCertificado(_peticion, objTransacion)

                If _respuesta IsNot Nothing Then
                    _peticion.Identificador = _respuesta
                End If

                If _peticion.TiposClientes IsNot Nothing AndAlso _peticion.TiposClientes.Count > 0 Then
                    'Insere os tipo de cliente da configuração do certificado
                    AccesoDatos.GenesisSaldos.Certificacion.Formularios.InserirTipoClientesCertificados(_peticion, objTransacion)
                End If

                If _peticion.Clientes IsNot Nothing AndAlso _peticion.Clientes.Count > 0 Then
                    'Insere os clientes da configuração do certificado
                    AccesoDatos.GenesisSaldos.Certificacion.Formularios.InserirClientesCertificados(_peticion, objTransacion)
                End If

                If _peticion.ParametrosReporte IsNot Nothing AndAlso _peticion.ParametrosReporte.Count > 0 Then
                    'Insere os ParametrosReporte da configuração do certificado
                    AccesoDatos.GenesisSaldos.ParametroReporte.InserirParametrosConfigCertificado(_peticion.Identificador, _peticion.ParametrosReporte, objTransacion)
                End If

                'Realiza Transação
                objTransacion.RealizarTransacao()

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
            Return _respuesta
        End Function
        Public Shared Function AlterarFormulariosCertificado(_peticion As Prosegur.Genesis.Comon.Clases.ConfiguracionReporte) As String
            Dim _respuesta As String = String.Empty
            Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GENESIS)

            Try
                _respuesta = AccesoDatos.GenesisSaldos.Certificacion.Formularios.AlterarFormulariosCertificado(_peticion, objTransacion)

                'REMOVO TODOS CLIENTES E TIPOCLIENTES E INSERE NOVAMENTE
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirTiposClientesFormulariosCertificado(_respuesta, objTransacion)

                If _respuesta IsNot Nothing Then
                    _peticion.Identificador = _respuesta
                End If

                If _peticion.TiposClientes IsNot Nothing Then
                    'Insere os tipo de cliente da configuração do certificado
                    AccesoDatos.GenesisSaldos.Certificacion.Formularios.InserirTipoClientesCertificados(_peticion, objTransacion)
                End If

                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirClientesFormulariosCertificado(_respuesta, objTransacion)

                If _respuesta IsNot Nothing Then
                    _peticion.Identificador = _respuesta
                End If


                If _peticion.Clientes IsNot Nothing Then

                    'Insere os clientes da configuração do certificado
                    AccesoDatos.GenesisSaldos.Certificacion.Formularios.InserirClientesCertificados(_peticion, objTransacion)

                End If

                If _peticion.ParametrosReporte IsNot Nothing Then

                    AccesoDatos.GenesisSaldos.ParametroReporte.ExcluirParametrosConfigCertificado(_respuesta, objTransacion)

                    'Insere os ParametrosReporte da configuração do certificado
                    AccesoDatos.GenesisSaldos.ParametroReporte.InserirParametrosConfigCertificado(_peticion.Identificador, _peticion.ParametrosReporte, objTransacion)

                End If

                'Realiza Transação
                objTransacion.RealizarTransacao()

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try
            Return _respuesta
        End Function

        Public Shared Sub ExcluirFormulariosCertificado(identificador As String)

            Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GENESIS)

            Try
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirTiposClientesFormulariosCertificado(identificador, objTransacion)
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirClientesFormulariosCertificado(identificador, objTransacion)
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirParametrosReporte(identificador, objTransacion)
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirParametrosResultado(identificador, objTransacion)
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirResultadoReporte(identificador, objTransacion)
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirFormulariosCertificado(identificador, objTransacion)

                'Realiza Transação
                objTransacion.RealizarTransacao()

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Shared Sub ExcluirTiposClientesFormulariosCertificado(_peticion As Prosegur.Genesis.Comon.Clases.ConfiguracionReporte)
            Dim _respuesta As String = String.Empty
            Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GENESIS)

            Try
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirTiposClientesFormulariosCertificado(_peticion.Identificador, objTransacion)
                'Realiza Transação
                objTransacion.RealizarTransacao()

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Shared Sub ExcluirClientesFormulariosCertificado(_peticion As Prosegur.Genesis.Comon.Clases.ConfiguracionReporte)

            Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GENESIS)

            Try
                AccesoDatos.GenesisSaldos.Certificacion.Formularios.ExcluirClientesFormulariosCertificado(_peticion.Identificador, objTransacion)
                'Realiza Transação
                objTransacion.RealizarTransacao()

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub


#End Region

    End Class

End Namespace