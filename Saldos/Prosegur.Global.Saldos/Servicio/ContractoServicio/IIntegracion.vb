Public Interface IIntegracion

    Function Test() As Test.Respuesta

    Function ManipularDocumentos(Peticion As ManipularDocumentos.Peticion) As ManipularDocumentos.Respuesta

End Interface