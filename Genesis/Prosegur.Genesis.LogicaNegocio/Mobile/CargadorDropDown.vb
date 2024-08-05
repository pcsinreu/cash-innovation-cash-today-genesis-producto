Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Mobile
    Public Class CargadorDropDown
        Public Shared Function ObtenerValoresDropDown(Peticion As Contractos.GenesisMovil.ObtenerValoresDropDown.Peticion) As Contractos.GenesisMovil.ObtenerValoresDropDown.Respuesta
            ValidaPeticion(Peticion)
            Return AccesoDatos.Genesis.CargadorDropDown.ObtenerValoresDropDown(Peticion)
        End Function

        Private Shared Sub ValidaPeticion(Peticion As Contractos.GenesisMovil.ObtenerValoresDropDown.Peticion)
            If Peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Peticion")))
            ElseIf Peticion.CargarSubCanal > 0 AndAlso (Peticion.IdentificadorCanal Is Nothing OrElse Peticion.IdentificadorCanal.Length <= 0) Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Codigo Canal")))
            ElseIf Peticion.CargarDenominacion > 0 AndAlso (Peticion.IdentificadorDivisa Is Nothing OrElse Peticion.IdentificadorDivisa.Length <= 0) Then
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("msg_gen_Parametro_Obrigatorio"), Traduzir("Codigo Divisa")))
            End If
        End Sub

    End Class
End Namespace

