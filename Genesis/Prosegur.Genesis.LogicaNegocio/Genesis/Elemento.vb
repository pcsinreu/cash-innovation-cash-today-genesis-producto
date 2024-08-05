Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon

Namespace Genesis

    ''' <summary>
    ''' Classe Elemento
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Elemento

#Region "[CONSULTAS]"

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Actualizar Estado Elementos
        ''' </summary>
        ''' <param name="elemento">Elemento base</param>
        ''' <param name="estadoRemesa"></param>
        ''' <param name="estadoBulto"></param>
        ''' <param name="estadoParcial"></param>
        ''' <param name="esGestionRemesa"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarEstado(ByRef elemento As Clases.Elemento,
                                            estadoRemesa As Enumeradores.EstadoRemesa,
                                            estadoBulto As Enumeradores.EstadoBulto,
                                            estadoParcial As Enumeradores.EstadoParcial,
                                            esGestionRemesa As Boolean)

            'Actualiza el elemento y sus hijos
            If elemento IsNot Nothing Then
                If TypeOf elemento Is Clases.Remesa Then

                    Dim objRemesa = CType(elemento, Clases.Remesa)

                    'Verifica se a remesa tem bultos se tiver anular os buultos
                    If objRemesa.Bultos IsNot Nothing Then
                        For Each objBulto In objRemesa.Bultos
                            objBulto.Estado = estadoBulto

                            'verifica se bulto tem parcial e anula os bulto da parcial
                            If objBulto.Parciales IsNot Nothing Then
                                For Each objParcial In objBulto.Parciales
                                    objParcial.Estado = estadoParcial
                                Next
                            End If
                        Next
                    End If

                    'Verifica se é gestão de remesa ou gestão de bulto
                    'Quando for gestão de remesa altera o estado da remesa
                    'Quando for gestão de bulto não altera o estado da remesa porque pode ter bulto nessa remesa que não foi alterado.
                    If esGestionRemesa Then

                        objRemesa.Estado = estadoRemesa
                        LogicaNegocio.Genesis.Remesa.ActualizarEstadoRemesa(objRemesa)

                    Else

                        'Se for gestão de bulto,
                        'Verifica se todos os bultos estão diferente de Cerrado, se estiver então atualiza a remesa para processado.
                        If Not LogicaNegocio.Genesis.Bulto.HayBultoCerradoPorRemesa(objRemesa.Identificador) Then
                            objRemesa.Estado = Enumeradores.EstadoRemesa.Procesada
                            LogicaNegocio.Genesis.Remesa.ActualizarEstadoRemesa(objRemesa)
                        End If

                    End If

                ElseIf TypeOf elemento Is Clases.Bulto Then

                    Dim objBulto = CType(elemento, Clases.Bulto)
                    objBulto.Estado = estadoBulto

                    If objBulto.Parciales IsNot Nothing Then
                        'verifica se bulto tem parcial e anula os bulto da parcial
                        For Each objParcial In objBulto.Parciales
                            objParcial.Estado = estadoParcial
                        Next
                    End If

                    LogicaNegocio.Genesis.Bulto.ActualizarEstadoBulto(objBulto)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Actualizar cuenta de destino del documento en su elemento y sus hijos.
        ''' </summary>
        ''' <param name="elemento"></param>
        ''' <param name="cuentaDestino"></param>
        ''' <remarks></remarks>
        Public Shared Sub ActualizarCuenta(elemento As Clases.Elemento, cuentaDestino As Clases.Cuenta)

            If elemento IsNot Nothing Then
                If TypeOf elemento Is Clases.Remesa Then

                    Dim remesa = CType(elemento, Clases.Remesa)
                    AccesoDatos.Genesis.Remesa.ActualizarCuenta(remesa.Identificador, cuentaDestino.Identificador, remesa.UsuarioModificacion)

                    For Each bulto In remesa.Bultos
                        AccesoDatos.Genesis.Bulto.ActualizarCuenta(bulto.Identificador, cuentaDestino.Identificador, bulto.UsuarioModificacion)
                    Next

                End If
            End If

        End Sub

#End Region

    End Class

End Namespace