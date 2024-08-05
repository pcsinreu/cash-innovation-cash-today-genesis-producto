Imports System.Version

Public Class Constantes

    ''' Versão da aplicação
    Public Shared VERSAO As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor

    '''  build da aplicação  
    Public Shared BUILD As String = My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision.ToString("0000")

    Public Shared APP_COD_ATM As String = "ATM"
    Public Shared ATM_DLL_APP As String = "Prosegur.Global.GesEfectivo.ATM.Aplication"
    Public Shared ATM_FRM_CLASS As String = "Prosegur.Global.GesEfectivo.ATM.Aplication.Controler"
    Public Shared ATM_PER_SUPERVISOR As String = "SUPERVISOR"

    Public Shared APP_COD_CONTEO As String = "Conteo"
    Public Shared CONTEO_DLL_APP As String = "Prosegur.Global.GesEfectivo.Conteo.Aplicacion"
    Public Shared CONTEO_FRM_CLASS As String = "Prosegur.Global.GesEfectivo.Conteo.Aplicacion.Controler"
    Public Shared CONTEO_PER_SUPERVISOR As String = String.Empty

    Public Shared APP_COD_EMULADOR As String = "EmuladorConteo"
    Public Shared EMULADOR_DLL_APP As String = "Prosegur.Global.GesEfectivo.Conteo.Emulador"
    Public Shared EMULADOR_FRM_CLASS As String = "Prosegur.GesEfectivo.Conteo.Emulador.Controler"
    Public Shared EMULADOR_PER_SUPERVISOR As String = String.Empty

    Public Shared APP_COD_EMULADORATM As String = "EmuladorATM"
    Public Shared EMULADORATM_DLL_APP As String = "Prosegur.Global.GesEfectivo.ATM.Emulador"
    Public Shared EMULADORATM_FRM_CLASS As String = "Prosegur.GesEfectivo.ATM.Emulador.Controler"
    Public Shared EMULADORATM_PER_SUPERVISOR As String = String.Empty

    Public Shared APP_COD_SALIDAS As String = "Salidas"
    Public Shared SALIDAS_DLL_APP As String = "Prosegur.Global.GesEfectivo.Salidas.Aplicacion"
    Public Shared SALIDAS_FRM_CLASS As String = "Prosegur.Global.GesEfectivo.Salidas.Aplicacion.Controler"
    Public Shared SALIDAS_PER_SUPERVISOR As String = String.Empty

    Public Shared APP_COD_SUPERVISOR_CONTEO As String = "SupervisorConteo"
    Public Shared SUPERVISOR_CONTEO_DLL_APP As String = "Prosegur.Global.GesEfectivo.Conteo.Supervisor"
    Public Shared SUPERVISOR_CONTEO_FRM_CLASS As String = "Prosegur.Global.GesEfectivo.Conteo.Supervisor.Controler"
    Public Shared SUPERVISOR_PER_SUPERVISOR As String = String.Empty

    Public Shared APP_COD_RECEPCION_Y_ENVIO As String = "RecepcionyEnvio"
    Public Shared RECEPCIONYENVIO_DLL_APP As String = "Prosegur.Genesis.RecepcionyEnvio.Aplicacion"
    Public Shared RECEPCIONYENVIO_FRM_CLASS As String = "Prosegur.Genesis.RecepcionyEnvio.Aplicacion.Controler"
    Public Shared RECEPCIONYENVIO_PER_SUPERVISOR As String = String.Empty

    Public Shared APP_COD_PACK_MODULAR As String = "PackModular"
    Public Shared APP_COD_CUSTODIA_PRECINTADA As String = "CustodiaPrecintada"
    Public Shared PACKMODULAR_DLL_APP As String = "Prosegur.Genesis.PackModular.Aplicacion"
    Public Shared PACKMODULAR_FRM_CLASS As String = "Prosegur.Genesis.PackModular.Aplicacion.Controler"
    Public Shared PACKMODULAR_PER_SUPERVISOR As String = String.Empty

    Public Shared APP_COD_GE_SALIDAS As String = "GenesisSalidas"
    Public Shared GE_SALIDAS_DLL_APP As String = "Prosegur.Genesis.Salidas.Aplicacion"
    Public Shared GE_SALIDAS_FRM_CLASS As String = "Prosegur.Genesis.Salidas.Aplicacion.Controler"
    Public Shared GE_SALIDAS_PER_SUPERVISOR As String = String.Empty

    Public Shared APP_COD_EMULADOR_ATM As String = "EmuladorATM"
    Public Shared EMULADOR_ATM_DLL_APP As String = "Prosegur.Global.GesEfectivo.ATM.Emulador"
    Public Shared EMULADOR_ATM_FRM_CLASS As String = "Prosegur.Global.GesEfectivo.ATM.Emulador.Controler"
    Public Shared EMULADOR_ATM_PER_SUPERVISOR As String = "SUPERVISOR"

    Public Shared APP_COD_GENESIS As String = "Genesis"
    Public Shared GENESIS_EXE_APP As String = "Prosegur.Genesis.Aplicacion"

    Public Shared APP_COD_ACTUALIZADOR As String = "Actualizador"
    Public Shared ACTUALIZADOR_EXE_APP As String = "Prosegur.Genesis.Actualizador"

    Public Const NomeArquivoRelacionMaquinaPuesto As String = "MaquinaxPuesto.xml"



End Class
