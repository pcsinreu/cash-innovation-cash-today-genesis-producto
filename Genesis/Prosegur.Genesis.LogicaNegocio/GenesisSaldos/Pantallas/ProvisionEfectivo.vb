Imports System.Data
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarTransacciones

Namespace GenesisSaldos.Pantallas
    Public Class ProvisionEfectivo

        Public Shared Function RecuperarFormulario(codFormulario As String) As Comon.Clases.Formulario

            Dim formulario = New Comon.Clases.Formulario
            formulario.Codigo = codFormulario

            Return AccesoDatos.GenesisSaldos.Formulario.ObtenerFormulario(formulario)


        End Function

    End Class
End Namespace

