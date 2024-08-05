Imports Prosegur.Genesis.Comon

Namespace Genesis

    ''' <summary>
    ''' Clase EmisorDocumento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] 02/12/2013 - Criado
    ''' </history>
    Public Class Integracion

        ''' <summary>
        ''' Método que busca a integração entre dois modulos pelo Identificador da tabela de integração.
        ''' </summary>
        ''' <param name="integracion">String</param>
        ''' <returns>Clases.Integracion</returns>
        ''' <remarks></remarks>
        Public Shared Function BuscarIntegracion(integracion As Clases.Integracion) As Comon.Clases.Integracion

            ' Retorna a integração entre dois modulos
            Return Prosegur.Genesis.AccesoDatos.Genesis.Integracion.ObtenerIntegracion(integracion)

        End Function

        Public Shared Function BuscarIntegracion(integracion As Clases.Integracion, ByRef transaccion As DataBaseHelper.Transaccion) As Comon.Clases.Integracion

            ' Retorna a integração entre dois modulos
            Return Prosegur.Genesis.AccesoDatos.Genesis.Integracion.ObtenerIntegracion(integracion, transaccion)

        End Function

        ''' <summary>
        ''' Método que insere uma tentativa de integração entre dois modulos.
        ''' </summary>
        ''' <param name="integracion">Comon.Clases.Integracion</param>
        ''' <returns>Comon.Clases.Integracion</returns>
        ''' <remarks></remarks>
        Public Shared Function InserirIntegracion(integracion As Clases.Integracion) As Comon.Clases.Integracion

            ' Grava a uma integração entre dois modulos
            Dim objIntegracion As Clases.Integracion = Prosegur.Genesis.AccesoDatos.Genesis.Integracion.InserirIntegracion(integracion)

            ' Verifica se existem erros
            If objIntegracion.IntegracionErros IsNot Nothing Then
                ' Para cada erro existente
                For Each erro As Clases.IntegracionError In objIntegracion.IntegracionErros
                    ' Grava o erro que aconteceu na integração
                    Prosegur.Genesis.AccesoDatos.Genesis.Integracion.InserirIntegracionError(erro, integracion.Identificador)
                Next

            End If

            ' Retorna a integração gravada
            Return objIntegracion

        End Function

        Public Shared Function InserirIntegracion(integracion As Clases.Integracion, ByRef transaccion As DataBaseHelper.Transaccion) As Comon.Clases.Integracion

            ' Grava a uma integração entre dois modulos
            Dim objIntegracion As Clases.Integracion = Prosegur.Genesis.AccesoDatos.Genesis.Integracion.InserirIntegracion(integracion, transaccion)

            ' Verifica se existem erros
            If objIntegracion.IntegracionErros IsNot Nothing Then
                ' Para cada erro existente
                For Each erro As Clases.IntegracionError In objIntegracion.IntegracionErros
                    ' Grava o erro que aconteceu na integração
                    Prosegur.Genesis.AccesoDatos.Genesis.Integracion.InserirIntegracionError(erro, integracion.Identificador, transaccion)
                Next

            End If

            ' Retorna a integração gravada
            Return objIntegracion

        End Function

        Public Shared Sub GrabarIntegracion(integracion As Clases.Integracion)

            ' Verifica se já existe a integração
            Dim objIntegracion As Clases.Integracion = BuscarIntegracion(integracion)

            ' Verifica se encontrou a integração
            If objIntegracion IsNot Nothing Then
                integracion.Identificador = objIntegracion.Identificador
                integracion.Intentos = objIntegracion.Intentos
                ActualizarIntegracion(integracion)
            Else
                InserirIntegracion(integracion)
            End If

        End Sub

        Public Shared Sub GrabarIntegracion(integracion As Clases.Integracion, ByRef transaccion As DataBaseHelper.Transaccion)

            ' Verifica se já existe a integração
            Dim objIntegracion As Clases.Integracion = BuscarIntegracion(integracion, transaccion)

            ' Verifica se encontrou a integração
            If objIntegracion IsNot Nothing Then
                integracion.Identificador = objIntegracion.Identificador
                integracion.Intentos = objIntegracion.Intentos
                ActualizarIntegracion(integracion, transaccion)
            Else
                InserirIntegracion(integracion, transaccion)
            End If

        End Sub

        ''' <summary>
        ''' Método que atualiza uma tentativa de integração entre dois modulos.
        ''' </summary>
        ''' <param name="integracion">Comon.Clases.Integracion</param>
        ''' <returns>Comon.Clases.Integracion</returns>
        ''' <remarks></remarks>
        Private Shared Function ActualizarIntegracion(integracion As Clases.Integracion) As Comon.Clases.Integracion

            ' Grava a uma integração entre dois modulos
            Prosegur.Genesis.AccesoDatos.Genesis.Integracion.ActualizarIntegracion(integracion)

            ' Verifica se existem erros
            If integracion.IntegracionErros IsNot Nothing Then
                ' Para cada erro existente
                For Each erro As Clases.IntegracionError In integracion.IntegracionErros
                    ' Grava o erro que aconteceu na integração
                    Prosegur.Genesis.AccesoDatos.Genesis.Integracion.InserirIntegracionError(erro, integracion.Identificador)
                Next

            End If

            ' Retorna a integração gravada
            Return integracion

        End Function

        Private Shared Function ActualizarIntegracion(integracion As Clases.Integracion, ByRef transaccion As DataBaseHelper.Transaccion) As Comon.Clases.Integracion

            ' Grava a uma integração entre dois modulos
            Prosegur.Genesis.AccesoDatos.Genesis.Integracion.ActualizarIntegracion(integracion, transaccion)

            ' Verifica se existem erros
            If integracion.IntegracionErros IsNot Nothing Then
                ' Para cada erro existente
                For Each erro As Clases.IntegracionError In integracion.IntegracionErros
                    ' Grava o erro que aconteceu na integração
                    Prosegur.Genesis.AccesoDatos.Genesis.Integracion.InserirIntegracionError(erro, integracion.Identificador, transaccion)
                Next

            End If

            ' Retorna a integração gravada
            Return integracion

        End Function

        ''' <summary>
        ''' Método que insere um erro que aconteceu durante a integração entre dois modulos.
        ''' </summary>
        ''' <param name="objIntegracionError">Comon.Clases.IntegracionError</param>
        ''' <returns>Comon.Clases.Integracion</returns>
        ''' <remarks></remarks>
        Private Shared Function InserirIntegracionError(objIntegracionError As Comon.Clases.IntegracionError, identificadorIntegracion As String) As Comon.Clases.IntegracionError
            ' Grava os erros que aconteceram na integração entre dois modulos
            Return Prosegur.Genesis.AccesoDatos.Genesis.Integracion.InserirIntegracionError(objIntegracionError, identificadorIntegracion)
        End Function

    End Class

End Namespace
