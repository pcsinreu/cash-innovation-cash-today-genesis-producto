Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports System.Reflection
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Threading.Tasks
Imports Prosegur.AgenteDispositivos.ContractoServicio
Imports System.IO

Namespace Genesis

    Public Class Parametros

        Private Shared _ParametrosIntegracion As GetParametrosDelegacionPais.ParametroRespuestaColeccion
        Public Shared ReadOnly Property ParametrosIntegracion As GetParametrosDelegacionPais.ParametroRespuestaColeccion
            Get
                Return _ParametrosIntegracion
            End Get
        End Property

        Shared Sub Inicializar(parametroRespuestaColeccion As GetParametrosDelegacionPais.ParametroRespuestaColeccion)
            _ParametrosIntegracion = parametroRespuestaColeccion
        End Sub

    End Class

End Namespace