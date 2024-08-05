Namespace Login.EjecutarLogin

    Public Enum ResultadoOperacionLoginLocal
        Autenticado = 1
        NoEsValido = 2
        [Error] = 3
        SesionAtiva = 4
        VersionAplicacionNoEncontrada = 5
        DelegacionNoEncontrada = 6
    End Enum

End Namespace