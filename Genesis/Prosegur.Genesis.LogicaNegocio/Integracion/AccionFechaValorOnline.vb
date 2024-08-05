Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.IntegracionSistemas

Namespace Integracion

    Public Class AccionFechaValorOnline
        Public Shared Sub Ejecutar()
            'Dim objFVO = New IntegracionFechaValorOnline()
            'Dim configuracion = New ContractoServicio.Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion()

            ''buscar Parametros 
            'configuracion.SistemaOrigem = Constantes.CONST_SISTEMA_GENESIS_PRODUCTO
            'configuracion.SistemaDestino = Constantes.CONST_SISTEMA_SWITCH
            'configuracion.CodigoProceso = Constantes.CONST_PROCESO_FV_ONLINE

            'configuracion.Link = "http://10.83.0.47:8305/api/FechaValorOnline"

            'configuracion.CodigoTablaIntegracion = Constantes.CONST_TABLA_INTEGRACION_FV_ONLINE
            'configuracion.Usuario = "BRX0001381"
            'configuracion.IdentificadoresIntegracion = New List(Of String)
            'configuracion.IdentificadoresIntegracion.Add("1")
            'configuracion.IdentificadoresIntegracion.Add("2")
            'configuracion.IdentificadoresIntegracion.Add("3")
            'configuracion.IdentificadoresIntegracion.Add("4")
            'configuracion.IdentificadoresIntegracion.Add("5")
            'configuracion.IdentificadoresIntegracion.Add("6")
            'configuracion.IdentificadoresIntegracion.Add("7")
            'configuracion.IdentificadoresIntegracion.Add("8")
            'configuracion.IdentificadoresIntegracion.Add("9")
            'configuracion.IdentificadoresIntegracion.Add("10")
            'configuracion.IdentificadoresIntegracion.Add("11")
            'configuracion.IdentificadoresIntegracion.Add("12")
            'configuracion.IdentificadoresIntegracion.Add("13")
            'configuracion.IdentificadoresIntegracion.Add("14")

            'configuracion.Mensaje = "PRUEBAAAA"
            'configuracion.CodigoEstadoDetalle = Comon.Enumeradores.EstadoIntegracionDetalle.ReenvioManual


            'Dim resp = objFVO.Ejecutar(configuracion)

        End Sub

        Public Shared Function DevolverPendientes(ByRef peticion As Reintentos.Peticion) As Reintentos.Respuesta
            Return AccesoDatos.GenesisSaldos.Integracion.FechaValorOnline.GetMovimientos(peticion)
        End Function

    End Class

End Namespace
