Imports Prosegur.Genesis.ContractoServicio
Imports System.ServiceModel


Namespace Interfaces
    <ServiceContract(Namespace:="http://Prosegur.GenesisMobile.WS", Name:="ICarga")>
    Public Interface IMobile
        <OperationContract>
        Function ObtenerDocumento(Peticion As ContractoServicio.Documento.Mobile.ObtenerDocumento.Peticion) As ContractoServicio.Documento.Mobile.ObtenerDocumento.Respuesta

    End Interface

End Namespace
