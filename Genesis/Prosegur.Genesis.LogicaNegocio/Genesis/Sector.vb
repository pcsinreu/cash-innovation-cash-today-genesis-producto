Imports Prosegur.Framework.Dicionario
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.AccesoDatos
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Paginacion
Imports System.Data
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Clase AccionSector
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [henrique.ribeiro] 13/09/2013 - Criado
    ''' </history>
    Public Class Sector

        Public Shared Function ObtenerIdentificadorSector(CodigoSector As String) As String
            Return AccesoDatos.Genesis.Sector.ObtenerIdentificadorSector(CodigoSector)
        End Function

        Public Shared Function ObtenerSectoresHijos(ByRef identificadoresSectores As List(Of String)) As List(Of String)

            Return AccesoDatos.Genesis.Sector.ObteneridentificadoresSectoresHijos(identificadoresSectores)

        End Function


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="codigoDelegacion"></param>
        ''' <param name="codigoPlanta"></param>
        ''' <param name="TipoSectores"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSectores(CodigoDelegacion As String, CodigoPlanta As String, TipoSectores As ObservableCollection(Of Clases.TipoSector), Optional CargarCodigosAjenos As Boolean = False) As ObservableCollection(Of Comon.Clases.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectores(CodigoDelegacion, CodigoPlanta, TipoSectores, CargarCodigosAjenos)
        End Function

        Public Shared Function ObtenerSectores(paises As List(Of Prosegur.Genesis.Comon.Clases.Pais), Optional sinPuesto As Boolean = False) As ObservableCollection(Of Clases.Sector)
            Dim sectores As New ObservableCollection(Of Clases.Sector)

            Try
                Dim dtsectores As List(Of DataTable) = Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectores(paises, sinPuesto)

                If dtsectores IsNot Nothing AndAlso dtsectores.Count > 0 Then

                    For Each dt In dtsectores

                        If dt IsNot Nothing AndAlso dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                            For Each _row In dt.Rows

                                Dim _delegacion As Clases.Delegacion = New Clases.Delegacion With {.Identificador = Util.AtribuirValorObj(_row("OID_DELEGACION"), GetType(String)),
                                                                                                   .Codigo = Util.AtribuirValorObj(_row("COD_DELEGACION"), GetType(String)),
                                                                                                   .Descripcion = Util.AtribuirValorObj(_row("DES_DELEGACION"), GetType(String))}

                                Dim _planta As Clases.Planta = New Clases.Planta With {.Identificador = Util.AtribuirValorObj(_row("OID_PLANTA"), GetType(String)),
                                                                                       .Codigo = Util.AtribuirValorObj(_row("COD_PLANTA"), GetType(String)),
                                                                                       .Descripcion = Util.AtribuirValorObj(_row("DES_PLANTA"), GetType(String))}

                                Dim _tiposector As Clases.TipoSector = New Clases.TipoSector With {.Identificador = Util.AtribuirValorObj(_row("OID_TIPO_SECTOR"), GetType(String)),
                                                                                                   .Codigo = Util.AtribuirValorObj(_row("COD_TIPO_SECTOR"), GetType(String)),
                                                                                                   .Descripcion = Util.AtribuirValorObj(_row("DES_TIPO_SECTOR"), GetType(String))}
                                sectores.Add(New Clases.Sector With {.Identificador = Util.AtribuirValorObj(_row("OID_SECTOR"), GetType(String)),
                                                                     .Codigo = Util.AtribuirValorObj(_row("COD_SECTOR"), GetType(String)),
                                                                     .Descripcion = Util.AtribuirValorObj(_row("DES_SECTOR"), GetType(String)),
                                                                     .Planta = _planta,
                                                                     .Delegacion = _delegacion,
                                                                     .TipoSector = _tiposector})

                            Next

                        End If

                    Next

                End If

            Catch ex As Exception
                Throw
            End Try

            Return sectores
        End Function

        ''' <summary>
        ''' Obtener Sector especifico
        ''' </summary>
        ''' <param name="CodigoDelegacion">Codigo Delegacion</param>
        ''' <param name="CodigoPlanta">Codigo Planta</param>
        ''' <param name="CodigoSector">Codigo Sector</param>
        ''' <returns>Objeto Sector</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSector(CodigoDelegacion As String, CodigoPlanta As String, CodigoSector As String) As Comon.Clases.Sector
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSector(CodigoDelegacion, CodigoPlanta, CodigoSector)
        End Function

        ''' <summary>
        ''' Obtener Sectores por setor pai
        ''' </summary>
        ''' <param name="CodigoDelegacion">Codigo Delegacion</param>
        ''' <param name="CodigoPlanta">Codigo Planta</param>
        ''' <param name="CodigoSectorPadre">Codigo Sector Pai</param>
        ''' <returns>Objeto Sector</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSectoresPorSectorPadre(CodigoDelegacion As String, CodigoPlanta As String, CodigoSectorPadre As String) As List(Of Comon.Clases.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectoresPorSectorPadre(CodigoDelegacion, CodigoPlanta, CodigoSectorPadre)
        End Function
        ''' <summary>
        ''' Obtener Codigos Sectores por setor pai
        ''' </summary>
        ''' <param name="CodigoDelegacion">Codigo Delegacion</param>
        ''' <param name="CodigoPlanta">Codigo Planta</param>
        ''' <param name="CodigoSectorPadre">Codigo Sector Pai</param>
        ''' <returns>List String</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerCodigosSectoresPorSectorPadre(CodigoDelegacion As String, CodigoPlanta As String, CodigoSectorPadre As String) As List(Of String)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerCodigosSectoresPorSectorPadre(CodigoDelegacion, CodigoPlanta, CodigoSectorPadre)
        End Function


        ''' <summary>
        ''' Obtener Sectores por Características
        ''' </summary>
        ''' <param name="CodigoDelegacion">Codigo Delegacion</param>
        ''' <param name="CodigoPlanta">Codigo Planta</param>
        ''' <param name="caracteristicasTipoSector">Características tipo setor</param>
        ''' <returns>Objeto Sector</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSectoresPorCaracteristicas(codigoDelegacion As String, codigoPlanta As String, caracteristicasTipoSector As List(Of Enumeradores.CaracteristicaTipoSector), Optional CargarCodigosAjenos As Boolean = False) As ObservableCollection(Of Comon.Clases.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerPorCaracteristicas(codigoDelegacion, codigoPlanta, caracteristicasTipoSector, CargarCodigosAjenos)
        End Function

        ''' <summary>
        ''' Obtener Sectores por Características
        ''' </summary>
        ''' <param name="CodigoDelegacion">Codigo Delegacion</param>
        ''' <param name="CodigoPlanta">Codigo Planta</param>
        ''' <param name="caracteristicasTipoSector">Características tipo setor</param>
        ''' <returns>Objeto Sector</returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerSectoresPorCaracteristicasSimultaneas(codigoDelegacion As String, codigoPlanta As String, caracteristicasTipoSector As List(Of Enumeradores.CaracteristicaTipoSector)) As ObservableCollection(Of Comon.Clases.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectoresPorCaracteristicasSimultaneas(codigoDelegacion, codigoPlanta, caracteristicasTipoSector)
        End Function

        Public Shared Function ObtenerSectoresTesoro(codigoDelegacion As String, codigoPlanta As String, desLogin As String) As List(Of ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectoresTesoro(codigoDelegacion, codigoPlanta, desLogin)
        End Function

        ''' <summary>
        ''' Método que recupera o setor pelo seu OID.
        ''' </summary>
        ''' <param name="oid">OID a ser pesquisado</param>
        ''' <param name="cargarCodigosAjenos">Carrega os códigos Ajenos</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPorOid(oid As String, Optional cargarCodigosAjenos As Boolean = False, Optional CargarSectorPadre As Boolean = True) As Prosegur.Genesis.Comon.Clases.Sector
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerPorOid(oid, cargarCodigosAjenos, CargarSectorPadre)
        End Function

        Public Shared Function ObtenerSectoresPorDelegacion(identificadorDelegacion As String) As ObservableCollection(Of Comon.Clases.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectoresPorDelegacion(identificadorDelegacion)
        End Function

        Public Shared Function ObtenerSectoresPorCodigoDelegacion(codigoDelegaciones As List(Of String), Optional tiposSectores As List(Of String) = Nothing, Optional solamentePadres As Boolean = False) As ObservableCollection(Of Comon.Clases.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectoresPorCodigoDelegacion(codigoDelegaciones, tiposSectores, solamentePadres)
        End Function

        Public Shared Function ObtenerCodigosSectoresPorCodigoTiposSectores(listaCodigoTiposSectores As List(Of String)) As List(Of String)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerCodigosSectoresPorCodigoTiposSectores(listaCodigoTiposSectores)
        End Function

        Public Shared Function ObtenerDatosResumidos(codDelegacion As String, codPlanta As String) As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerDatosResumidos(codDelegacion, codPlanta)
        End Function

        Public Shared Function ObtenerSectorPadrePrimerNivel(identificadorSector As String) As Prosegur.Genesis.Comon.Clases.Sector
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectorPadrePrimerNivel(identificadorSector)
        End Function
        Public Shared Function ObtenerSectoresPorCodigo(listaCodigo As List(Of String)) As ObservableCollection(Of Clases.Sector)
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectoresPorCodigo(listaCodigo)
        End Function

#Region "Integracion Salidas -> NuevoSaldos"

        Public Shared Function ObtenerSector(Idps As String) As Clases.Sector
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSector(Idps)
        End Function

        Public Shared Function ObtenerPermisionSectorPorIDPS(Idps As String, _
                                                             IdentificadorFormulario As String, _
                                                             OrigenDestino As Char) As List(Of Clases.Sector)

            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerPermisionSectorPorIDPS(Idps, IdentificadorFormulario, OrigenDestino)
        End Function

        Public Shared Function ObtenerSectorPorIDPS(Idps As String) As Comon.Clases.Sector
            Return Prosegur.Genesis.AccesoDatos.Genesis.Sector.ObtenerSectorPorIDPS(Idps)
        End Function

#End Region

        Shared Function ObtenerSectorJSON(codigo As String, descripcion As String, identificadorPadre As String, considerarTodosNiveis As Boolean) As List(Of Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Cuenta.ObtenerCuentas.Valor)
            Return AccesoDatos.Genesis.Sector.ObtenerSectorJSON(codigo, descripcion, identificadorPadre, considerarTodosNiveis)
        End Function


    End Class
End Namespace
