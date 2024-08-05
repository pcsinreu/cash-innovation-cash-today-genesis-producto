''' <summary>
''' Interface Controler
''' </summary>
''' <remarks></remarks>
Public Interface IControler
    Property Aplicaciones As ContractoServicio.Login.EjecutarLogin.AplicacionVersionColeccion

    Property Usuario As ContractoServicio.Login.EjecutarLogin.Usuario

    Property UrlServicio As String

    Property InformacionUsuarioCompleta As ContractoServicio.Login.EjecutarLogin.Usuario

    Function LogarUsuario(usuario As Prosegur.Genesis.ContractoServicio.Login.EjecutarLogin.Usuario, urlServicio As String) As Prosegur.Genesis.Comon.Clases.Sector

End Interface
