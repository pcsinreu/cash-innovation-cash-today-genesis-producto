
Namespace Genesis

    Public Class Cajero
        ''' <summary>
        ''' Verifica se as informação do ATM estão corretas
        ''' </summary>
        ''' <param name="codigoCajero">Código ATM</param>
        ''' <param name="identificadorPtoServicio">Identificador Pto Serviço</param>
        ''' <param name="codigoDelegacion">Código da Delegação</param>
        ''' <returns>informações do ATM corretas</returns>
        Public Shared Function EsDatosATMValidos(codigoCajero As String, identificadorPtoServicio As String, codigoDelegacion As String) As Boolean
            Return AccesoDatos.Genesis.Cajero.EsDatosATMValidos(codigoCajero, identificadorPtoServicio, codigoDelegacion)
        End Function

    End Class

End Namespace

