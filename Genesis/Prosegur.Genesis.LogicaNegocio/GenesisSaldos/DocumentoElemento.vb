Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports System.Data

Namespace GenesisSaldos
    Public Class DocumentoElemento

        Public Shared Sub Inserir(_Documento As Clases.Documento,
                             Optional ActualizarEstadoElementos As Boolean = True)

            Dim valores As New ObservableCollection(Of Clases.Transferencias.DocumentoElementoInserir)

            If _Documento.Elemento IsNot Nothing Then

                Dim _Remesa = CType(_Documento.Elemento, Clases.Remesa)

                ' DocumentoElemento - Documento X Remesa
                valores.Add(New Clases.Transferencias.DocumentoElementoInserir With {
                                            .identificadorDocumentoElemento = Guid.NewGuid.ToString,
                                            .identificadorDocumento = _Documento.Identificador,
                                            .identificadorRemesa = _Remesa.Identificador,
                                            .identificadorBulto = String.Empty,
                                            .usuarioModificacion = _Documento.UsuarioModificacion
                                         })

                ' DocumentoElemento - Documento X Bulto
                For Each _Bulto In _Remesa.Bultos
                    valores.Add(New Clases.Transferencias.DocumentoElementoInserir With {
                                            .identificadorDocumentoElemento = Guid.NewGuid.ToString,
                                            .identificadorDocumento = _Documento.Identificador,
                                            .identificadorRemesa = _Remesa.Identificador,
                                            .identificadorBulto = _Bulto.Identificador,
                                            .usuarioModificacion = _Documento.UsuarioModificacion
                                         })

                    If ActualizarEstadoElementos Then
                        AccesoDatos.Genesis.Bulto.ActualizarEstadoBulto(_Bulto.Identificador, EstadoBulto.EnTransito, _Bulto.UsuarioModificacion)
                    End If

                Next

                If ActualizarEstadoElementos Then
                    AccesoDatos.Genesis.Remesa.ActualizarEstadoRemesa(_Remesa.Identificador, EstadoRemesa.EnTransito, _Remesa.UsuarioModificacion, _Remesa.Bultos.Count)
                End If

            End If

            If valores IsNot Nothing AndAlso valores.Count > 0 Then
                AccesoDatos.GenesisSaldos.DocumentoElemento.DocumentoElementoInserir(valores)
            End If

        End Sub

        Public Shared Sub ActualizarEstado(_documento As Clases.Documento,
                                           _estadoDocumentoElemento As Enumeradores.EstadoDocumentoElemento,
                                  _estadoRemesa As Enumeradores.EstadoRemesa,
                                  _estadoBulto As Enumeradores.EstadoBulto,
                                  _estadoParcial As Enumeradores.EstadoParcial)

            Try
                If _documento.Elemento IsNot Nothing Then

                    Dim _remesa As New Comon.Clases.Remesa
                    _remesa = TryCast(_documento.Elemento, Comon.Clases.Remesa)

                    If _remesa.Bultos IsNot Nothing Then

                        For Each _bulto In _remesa.Bultos

                            'Actualiza los bultos para Histórico
                            _bulto.IdentificadorDocumento = _documento.Identificador
                            _bulto.EstadoDocumentoElemento = _estadoDocumentoElemento
                            _bulto.UsuarioModificacion = _documento.UsuarioModificacion

                            AccesoDatos.GenesisSaldos.DocumentoElemento.Actualizar(_bulto.EstadoDocumentoElemento,
                                                                                   _bulto.UsuarioModificacion,
                                                                                   _bulto.IdentificadorDocumento,
                                                                                   String.Empty,
                                                                                   0,
                                                                                   _bulto.Identificador)

                            If _estadoRemesa <> EstadoRemesa.NoDefinido Then
                                If (_documento.Estado = EstadoDocumento.Sustituido OrElse _estadoDocumentoElemento = EstadoDocumentoElemento.Concluido OrElse _
                                                                ((_documento.Estado = EstadoDocumento.Anulado OrElse _documento.Estado = EstadoDocumento.Rechazado) AndAlso _remesa.Estado = EstadoRemesa.EnTransito)) Then

                                    If _documento.Estado = EstadoDocumento.Sustituido Then
                                        _bulto.Estado = EstadoBulto.Sustituido
                                    Else
                                        _bulto.Estado = _estadoBulto
                                    End If

                                    AccesoDatos.Genesis.Bulto.ActualizarEstadoBulto(_bulto.Identificador, _bulto.Estado, _bulto.UsuarioModificacion)

                                    If _bulto.Parciales IsNot Nothing AndAlso _bulto.Parciales.Count > 0 Then
                                        For Each _parcial In _bulto.Parciales

                                            _parcial.UsuarioModificacion = _documento.UsuarioModificacion

                                            If _documento.Estado = EstadoDocumento.Sustituido Then
                                                _parcial.Estado = EstadoParcial.Sustituido
                                            Else
                                                _parcial.Estado = _estadoParcial
                                            End If

                                            AccesoDatos.Genesis.Parcial.ActualizarEstadoParcial(_parcial.Identificador, _parcial.Estado, _parcial.UsuarioModificacion)

                                        Next
                                    End If

                                End If
                            End If
                        Next

                        'Actualiza la remesa para histórico.
                        _remesa.IdentificadorDocumento = _documento.Identificador
                        _remesa.EstadoDocumentoElemento = _estadoDocumentoElemento
                        _remesa.UsuarioModificacion = _documento.UsuarioModificacion

                        AccesoDatos.GenesisSaldos.DocumentoElemento.Actualizar(_remesa.EstadoDocumentoElemento,
                                                                               _remesa.UsuarioModificacion,
                                                                               _remesa.IdentificadorDocumento,
                                                                               _remesa.Identificador,
                                                                               _remesa.Bultos.Count)

                        If _estadoRemesa <> EstadoRemesa.NoDefinido Then
                            If (_documento.Estado = EstadoDocumento.Sustituido OrElse _estadoDocumentoElemento = EstadoDocumentoElemento.Concluido OrElse _
                           ((_documento.Estado = EstadoDocumento.Anulado OrElse _documento.Estado = EstadoDocumento.Rechazado) AndAlso _remesa.Estado = EstadoRemesa.EnTransito)) Then

                                If _documento.Estado = EstadoDocumento.Sustituido Then
                                    _remesa.Estado = EstadoRemesa.Sustituido
                                Else
                                    _remesa.Estado = _estadoRemesa
                                End If

                                AccesoDatos.Genesis.Remesa.ActualizarEstadoRemesa(_remesa.Identificador, _remesa.Estado, _remesa.UsuarioModificacion, _remesa.Bultos.Count)
                            End If
                        End If
                    End If
                End If

                ' Estou concluindo o elemento, tenho que alterar meu pai para historico.
                If _estadoDocumentoElemento = EstadoDocumentoElemento.Concluido AndAlso _documento IsNot Nothing AndAlso _
                    _documento.DocumentoPadre IsNot Nothing AndAlso _documento.DocumentoPadre.Elemento IsNot Nothing Then

                    _documento.DocumentoPadre.UsuarioModificacion = _documento.UsuarioModificacion

                    ActualizarEstado(_documento.DocumentoPadre, EstadoDocumentoElemento.Historico, EstadoRemesa.NoDefinido, EstadoBulto.NoDefinido, EstadoParcial.NoDefinido)

                End If

            Catch ex As Excepcion.CampoObrigatorioException
                Throw
            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

        Public Shared Function RecuperarHistorico(esGestionBulto As Boolean, idElemento As String) As DataTable
            Return AccesoDatos.GenesisSaldos.DocumentoElemento.RecuperarHistorico(esGestionBulto, idElemento)
        End Function
    End Class
End Namespace

