Imports System.Runtime.Serialization
Imports Prosegur.Genesis.Comon

Namespace Login.AutenticarUsuarioAplicacion

    <Serializable()>
    Public NotInheritable Class AutenticarUsuarioAplicacionPeticion
        Inherits Login.EjecutarLogin.Peticion

        Private _IP As String
        Private _Configuraciones As SerializableDictionary(Of String, String)

        Public Property IP() As String
            Get
                Return _IP
            End Get
            Set(value As String)
                _IP = value
            End Set
        End Property

        Public Property Configuraciones() As SerializableDictionary(Of String, String)
            Get
                Return _Configuraciones
            End Get
            Set(value As SerializableDictionary(Of String, String))
                _Configuraciones = value
            End Set
        End Property

    End Class

End Namespace
