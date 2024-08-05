Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos
    Public Class Inventario
        Public Shared Function InventarioRecuperar(objPeticion As Peticion(Of Clases.Transferencias.FiltroInventario)) _
                                                 As Comon.Respuesta(Of List(Of Clases.Inventario))

            Dim objRespuesta As New Comon.Respuesta(Of List(Of Clases.Inventario))

            Dim listaInventarios As New List(Of Clases.Inventario)

            'Recupera os inventários
            objRespuesta.Retorno = AccesoDatos.GenesisSaldos.Inventario.InventarioRecuperar(objPeticion, objRespuesta)

            Return objRespuesta

        End Function
    End Class
End Namespace

