Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Interfaces
Imports Prosegur.Genesis
Imports System.ServiceModel
Imports System.ServiceModel.Web

Namespace Interfaces
    <ServiceContract()>
    Public Interface IGenesisMovil

        <OperationContract> _
    <WebGet(UriTemplate:="/ObtenerDelegaciones/{NombreContinente}/{CodigoPais}/{NombreZona}/",
    ResponseFormat:=WebMessageFormat.Json,
    RequestFormat:=WebMessageFormat.Json)> _
        Function ObtenerDelegaciones(NombreContinente As String, CodigoPais As String, NombreZona As String) As Login.ObtenerDelegaciones.Respuesta

        <OperationContract> _
        <WebGet(UriTemplate:="/ObtenerSectoresTesoro/{CodigoDelegacion}/{CodigoPlanta}/{DesLogin}/",
        ResponseFormat:=WebMessageFormat.Json,
        RequestFormat:=WebMessageFormat.Json)> _
        Function ObtenerSectoresTesoro(CodigoDelegacion As String, CodigoPlanta As String, DesLogin As String) As Genesis.ContractoServicio.Contractos.GenesisMovil.ObtenerSectoresTesoro.Respuesta

        <OperationContract> _
        <WebGet(UriTemplate:="/EjecutarLogin/{Login}/{Password}/{CodigoDelegacion}/{CodigoPlanta}/",
        ResponseFormat:=WebMessageFormat.Json,
        RequestFormat:=WebMessageFormat.Json)> _
        Function EjecutarLogin(Login As String, Password As String, CodigoDelegacion As String,
                               CodigoPlanta As String) As Genesis.ContractoServicio.Contractos.GenesisMovil.EjecutarLogin.Respuesta

        <OperationContract> _
        <WebInvoke(UriTemplate:="/ObtenerDescripcionesFiltroExtracion",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json, Method:="POST")> _
        Function ObtenerDescripcionesFiltroExtracion(Peticion As Contractos.GenesisMovil.ObtenerDescripcionesFiltroExtracion.Peticion) As ContractoServicio.Contractos.GenesisMovil.ObtenerDescripcionesFiltroExtracion.Respuesta

        <OperationContract> _
        <WebInvoke(UriTemplate:="/ConsultarContenedorxPosicion",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json, Method:="POST")> _
        Function ConsultarContenedorxPosicion(Peticion As ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Peticion) As ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorxPosicion.Respuesta

        <OperationContract> _
        <WebInvoke(UriTemplate:="/ConsultarContenedoresPorFIFO",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json, Method:="POST")> _
        Function ConsultarContenedoresPorFIFO(Peticion As ContractoServicio.Contractos.GenesisMovil.ConsultarContenedorFIFO.Peticion) As ContractoServicio.GenesisSaldos.Contenedores.ConsultarContenedorFIFO.Respuesta

        <OperationContract> _
        <WebInvoke(UriTemplate:="/ReenvioEntreSectores",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json, Method:="POST")> _
        Function ReenvioEntreSectores(Peticion As ContractoServicio.Contractos.GenesisMovil.ReenvioEntreSectores.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.ReenvioEntreSectores.Respuesta

        <OperationContract> _
        <WebInvoke(UriTemplate:="/GrabarInventarioContenedor",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json, Method:="POST")> _
        Function GrabarInventarioContenedor(Peticion As ContractoServicio.Contractos.GenesisMovil.GrabarInventarioContenedor.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.GrabarInventarioContenedor.Respuesta

        <OperationContract> _
        <WebInvoke(UriTemplate:="/ObtenerPosicionesSector",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json, Method:="POST")> _
        Function ObtenerPosicionesSector(Peticion As ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.Peticion) As ContractoServicio.Contractos.GenesisMovil.ObtenerPosicionesSector.Respuesta

        <OperationContract> _
        <WebInvoke(UriTemplate:="/DefinirCambiarExtraerPosicionContenedor",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json, Method:="POST")> _
        Function DefinirCambiarExtraerPosicionContenedor(Peticion As ContractoServicio.GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Peticion) As Genesis.ContractoServicio.GenesisSaldos.Contenedores.DefinirCambiarExtraerPosicionContenedor.Respuesta

        <OperationContract> _
        <WebInvoke(UriTemplate:="/ObtenerCargaDropDown",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json, Method:="POST")> _
        Function ObtenerCargaDropDown(Peticion As Contractos.GenesisMovil.ObtenerValoresDropDown.Peticion) As Contractos.GenesisMovil.ObtenerValoresDropDown.Respuesta

        <OperationContract> _
        <WebGet(UriTemplate:="/Teste/{arg1}/",
            ResponseFormat:=WebMessageFormat.Json,
            RequestFormat:=WebMessageFormat.Json)> _
        Function Teste(arg1 As String) As DateTime

    End Interface
End Namespace