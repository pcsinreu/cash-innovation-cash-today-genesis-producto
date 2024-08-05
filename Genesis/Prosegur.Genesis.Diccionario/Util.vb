Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Diccionario.genesis

Public Class Util

    Public Shared Function TraduzirEstadoDocumento(estadoDocumento As Enumeradores.EstadoDocumento, Optional noDefinidoEsSeleccionar As Boolean = False) As String

        ' Verifica os estados do documento
        Select Case estadoDocumento

            Case Enumeradores.EstadoDocumento.Aceptado
                Return Tradutor.GenesisSalidas_vmestadodocumento_aceptado
            Case Enumeradores.EstadoDocumento.Anulado
                Return Tradutor.GenesisSalidas_vmestadodocumento_anulado
            Case Enumeradores.EstadoDocumento.Confirmado
                Return Tradutor.GenesisSalidas_vmestadodocumento_confirmado
            Case Enumeradores.EstadoDocumento.Eliminado
                Return Tradutor.GenesisSalidas_vmestadodocumento_eliminado
            Case Enumeradores.EstadoDocumento.EnCurso
                Return Tradutor.GenesisSalidas_vmestadodocumento_encurso
            Case Enumeradores.EstadoDocumento.Nuevo
                Return Tradutor.GenesisSalidas_vmestadodocumento_nuevo
            Case Enumeradores.EstadoDocumento.Rechazado
                Return Tradutor.GenesisSalidas_vmestadodocumento_rechazado
            Case Enumeradores.EstadoDocumento.Sustituido
                Return Tradutor.GenesisSalidas_vmestadodocumento_sustituido
            Case Enumeradores.EstadoDocumento.NoDefinido
                If noDefinidoEsSeleccionar Then
                    Return Tradutor.GenesisSalidas_vmdesglosedivisa_seleccione
                End If
        End Select

        Return String.Empty

    End Function


    Public Shared Function TraduzirEstadoContenedor(estadoDocumento As Enumeradores.EstadoContenedor) As String

        ' Verifica os estados do documento
        Select Case estadoDocumento

            Case Enumeradores.EstadoContenedor.Armado
                Return Tradutor.REyD_ConsultaContenedores_Estado_Armado
            Case Enumeradores.EstadoContenedor.Desarmado
                Return Tradutor.REyD_ConsultaContenedores_Estado_Desarmado
            Case Enumeradores.EstadoContenedor.EnTransito
                Return Tradutor.REyD_ConsultaContenedores_Estado_EnTransito
        End Select

        Return String.Empty

    End Function

    Public Shared Function TraduzirEstadoRemesa(estadoRemesa As Enumeradores.EstadoRemesa) As String

        ' Verifica os estados da remessa
        Select Case estadoRemesa

            Case Enumeradores.EstadoRemesa.Anulado
                Return Tradutor.GenesisSalidas_vmestadoremesaanulada
            Case Enumeradores.EstadoRemesa.Modificada
                Return Tradutor.GenesisSalidas_vmestadoremesamodificada
            Case Enumeradores.EstadoRemesa.Asignada
                Return Tradutor.GenesisSalidas_vmestadoremesaasignada
            Case Enumeradores.EstadoRemesa.EnCurso
                Return Tradutor.GenesisSalidas_vmestadoremesaencurso
            Case Enumeradores.EstadoRemesa.EnviadoLegado
                Return Tradutor.GenesisSalidas_vmestadoremesaenviadalegado
            Case Enumeradores.EstadoRemesa.EnviadoSaldos
                Return Tradutor.GenesisSalidas_vmestadoremesaenviadasaldos
            Case Enumeradores.EstadoRemesa.Pendiente
                Return Tradutor.GenesisSalidas_vmestadoremesapendiente
            Case Enumeradores.EstadoRemesa.Procesada
                Return Tradutor.GenesisSalidas_vmestadoremesaprocesada
        End Select

        Return String.Empty

    End Function

    Public Shared Function TraduzirEstadoBulto(estadoBulto As Enumeradores.EstadoBulto) As String

        ' Verifica os estados do malote
        Select Case estadoBulto

            Case Enumeradores.EstadoBulto.Anulado
                Return Tradutor.GenesisSalidas_vmestadobultoanulado
            Case Enumeradores.EstadoBulto.Modificado
                Return Tradutor.GenesisSalidas_vmestadobultomodificado
            Case Enumeradores.EstadoBulto.Asignado
                Return Tradutor.GenesisSalidas_vmestadobultoasignado
            Case Enumeradores.EstadoBulto.EnCurso
                Return Tradutor.GenesisSalidas_vmestadobultoencurso
            Case Enumeradores.EstadoBulto.Pendiente
                Return Tradutor.GenesisSalidas_vmestadobultopendiente
            Case Enumeradores.EstadoBulto.Procesado
                Return Tradutor.GenesisSalidas_vmestadobultoprocesado

        End Select

        Return String.Empty

    End Function

End Class
