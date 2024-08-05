Imports Prosegur.Genesis.ContractoServicio.Contractos

Namespace Interfaces
    Public Interface INuevoSalidasIntegracion

        Function Test() As Test.Respuesta

        Function RecuperarRemesasPorIdentificadorCodigoExternos(Peticion As Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Peticion) As Contractos.Genesis.Remesa.RecuperarRemesasPorIdentificadorCodigoExternos.Respuesta

    End Interface

End Namespace
