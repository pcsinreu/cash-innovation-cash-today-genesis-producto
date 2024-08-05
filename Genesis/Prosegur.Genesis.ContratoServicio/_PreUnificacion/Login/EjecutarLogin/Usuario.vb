Namespace Login.EjecutarLogin

    ''' <summary>
    ''' Usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    Public Class Usuario

#Region " Variáveis "

        Private _Identificador As String
        Private _Nombre As String
        Private _Apellido As String
        Private _Contrasena As String
        Private _Idioma As String
        Private _Continentes As New List(Of Continente)
        Private _IDSession As String
        Private _CodPrecintoBulto As String
        Private _OidBulto As String
        Private _OidRemesa As String
        Private _OidParcial As String
        Private _CantidadParciales As Integer
        Private _SaldoEnCurso As Boolean
        Private _TipoTrabajoPendiente As String
        Private _EstadoBulto As String
        Private _Login As String
        Private _Password As String
        Private _CodigoPuesto As String
        Private _SectorLogado As Prosegur.Genesis.Comon.Clases.Sector
        Private _SectorDefecto As Prosegur.Genesis.Comon.Clases.Sector
        Private _SectoresSalidas As List(Of Prosegur.Genesis.Comon.Clases.Sector)
        Private _OidUsuario As String

#End Region

#Region "Propriedades"

        Public Property OidUsuario() As String
            Get
                Return _OidUsuario
            End Get
            Set(value As String)
                _OidUsuario = value
            End Set
        End Property

        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Apellido() As String
            Get
                Return _Apellido
            End Get
            Set(value As String)
                _Apellido = value
            End Set
        End Property

        Public Property Contrasena() As String
            Get
                Return _Contrasena
            End Get
            Set(value As String)
                _Contrasena = value
            End Set
        End Property

        Public Property Idioma() As String
            Get
                Return _Idioma
            End Get
            Set(value As String)
                _Idioma = value
            End Set
        End Property

        Public Property Continentes() As List(Of Continente)
            Get
                Return _Continentes
            End Get
            Set(value As List(Of Continente))
                _Continentes = value
            End Set
        End Property

        Public Property IDSession() As String
            Get
                Return _IDSession
            End Get
            Set(value As String)
                _IDSession = value
            End Set
        End Property

        Public Property CodPrecintoBulto() As String
            Get
                Return _CodPrecintoBulto
            End Get
            Set(value As String)
                _CodPrecintoBulto = value
            End Set
        End Property

        Public Property OidBulto() As String
            Get
                Return _OidBulto
            End Get
            Set(value As String)
                _OidBulto = value
            End Set
        End Property

        Public Property OidRemesa() As String
            Get
                Return _OidRemesa
            End Get
            Set(value As String)
                _OidRemesa = value
            End Set
        End Property

        Public Property OidParcial() As String
            Get
                Return _OidParcial
            End Get
            Set(value As String)
                _OidParcial = value
            End Set
        End Property

        Public Property CantidadParciales() As Integer
            Get
                Return _CantidadParciales
            End Get
            Set(value As Integer)
                _CantidadParciales = value
            End Set
        End Property

        Public Property SaldoEnCurso() As Boolean
            Get
                Return _SaldoEnCurso
            End Get
            Set(value As Boolean)
                _SaldoEnCurso = value
            End Set
        End Property

        Public Property TipoTrabajoPendiente() As String
            Get
                Return _TipoTrabajoPendiente
            End Get
            Set(value As String)
                _TipoTrabajoPendiente = value
            End Set
        End Property

        Public Property EstadoBulto() As String
            Get
                Return _EstadoBulto
            End Get
            Set(value As String)
                _EstadoBulto = value
            End Set
        End Property

        Public Property Login() As String
            Get
                Return _Login
            End Get
            Set(value As String)
                _Login = value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(value As String)
                _Password = value
            End Set
        End Property

        Public ReadOnly Property CodigoPais As String
            Get
                ' Se a delegação está preenchida
                If _Continentes IsNot Nothing AndAlso _Continentes.Count > 0 AndAlso _
                    _Continentes.First.Paises IsNot Nothing AndAlso _Continentes.First.Paises.Count > 0 Then

                    Return _Continentes.First.Paises.First.Codigo

                End If

                Return String.Empty

            End Get
        End Property

        Public ReadOnly Property CodigoDelegacion As String
            Get
                ' Se a delegação está preenchida
                If _Continentes IsNot Nothing AndAlso _Continentes.Count > 0 AndAlso _
                    _Continentes.First.Paises IsNot Nothing AndAlso _Continentes.First.Paises.Count > 0 AndAlso _
                    _Continentes.First.Paises.First.Delegaciones IsNot Nothing AndAlso _Continentes.First.Paises.First.Delegaciones.Count > 0 Then

                    Return _Continentes.First.Paises.First.Delegaciones.First.Codigo

                End If

                Return String.Empty

            End Get
        End Property

        Public ReadOnly Property CodigoPlanta As String
            Get
                ' Se a delegação está preenchida
                If _Continentes IsNot Nothing AndAlso _Continentes.Count > 0 AndAlso _
                    _Continentes.First.Paises IsNot Nothing AndAlso _Continentes.First.Paises.Count > 0 AndAlso _
                    _Continentes.First.Paises.First.Delegaciones IsNot Nothing AndAlso _Continentes.First.Paises.First.Delegaciones.Count > 0 AndAlso _
                    TypeOf _Continentes.First.Paises.First.Delegaciones.First Is ContractoServicio.Login.EjecutarLogin.DelegacionPlanta AndAlso _
                    DirectCast(_Continentes.First.Paises.First.Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas IsNot Nothing AndAlso DirectCast(_Continentes.First.Paises.First.Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.Count > 0 Then

                    Return DirectCast(_Continentes.First.Paises.First.Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.First.CodigoPlanta

                End If

                Return String.Empty

            End Get
        End Property

        Public ReadOnly Property DesPlanta As String
            Get
                ' Se a delegação está preenchida
                If _Continentes IsNot Nothing AndAlso _Continentes.Count > 0 AndAlso _
                    _Continentes.First.Paises IsNot Nothing AndAlso _Continentes.First.Paises.Count > 0 AndAlso _
                    _Continentes.First.Paises.First.Delegaciones IsNot Nothing AndAlso _Continentes.First.Paises.First.Delegaciones.Count > 0 AndAlso _
                    TypeOf _Continentes.First.Paises.First.Delegaciones.First Is ContractoServicio.Login.EjecutarLogin.DelegacionPlanta AndAlso _
                    DirectCast(_Continentes.First.Paises.First.Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas IsNot Nothing AndAlso DirectCast(_Continentes.First.Paises.First.Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.Count > 0 Then

                    Return DirectCast(_Continentes.First.Paises.First.Delegaciones.First, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas.First.DesPlanta

                End If

                Return String.Empty

            End Get
        End Property

        Public ReadOnly Property DesDelegacion As String
            Get
                ' Se a delegação está preenchida
                If _Continentes IsNot Nothing AndAlso _Continentes.Count > 0 AndAlso _
                    _Continentes.First.Paises IsNot Nothing AndAlso _Continentes.First.Paises.Count > 0 AndAlso _
                    _Continentes.First.Paises.First.Delegaciones IsNot Nothing AndAlso _Continentes.First.Paises.First.Delegaciones.Count > 0 Then

                    Return _Continentes.First.Paises.First.Delegaciones.First.Nombre

                End If

                Return String.Empty

            End Get
        End Property

        Public ReadOnly Property Delegacion As Delegacion
            Get
                ' Se a delegação está preenchida
                If _Continentes IsNot Nothing AndAlso _Continentes.Count > 0 AndAlso _
                    _Continentes.First.Paises IsNot Nothing AndAlso _Continentes.First.Paises.Count > 0 AndAlso _
                    _Continentes.First.Paises.First.Delegaciones IsNot Nothing AndAlso _Continentes.First.Paises.First.Delegaciones.Count > 0 Then

                    Return _Continentes.First.Paises.First.Delegaciones.First

                End If

                Return Nothing

            End Get
        End Property

        Public Property CodigoPuesto() As String
            Get
                Return _CodigoPuesto
            End Get
            Set(value As String)
                _CodigoPuesto = value
            End Set
        End Property

        Public Property SectorLogado() As Prosegur.Genesis.Comon.Clases.Sector
            Get
                Return _SectorLogado
            End Get
            Set(value As Prosegur.Genesis.Comon.Clases.Sector)
                _SectorLogado = value
            End Set
        End Property

        Public Property SectorDefecto() As Prosegur.Genesis.Comon.Clases.Sector
            Get
                Return _SectorDefecto
            End Get
            Set(value As Prosegur.Genesis.Comon.Clases.Sector)
                _SectorDefecto = value
            End Set
        End Property

        Public Property SectoresSalidas() As List(Of Prosegur.Genesis.Comon.Clases.Sector)
            Get
                Return _SectoresSalidas
            End Get
            Set(value As List(Of Prosegur.Genesis.Comon.Clases.Sector))
                _SectoresSalidas = value
            End Set
        End Property

        Property IdentificadorDelegacionDefecto As String

        Property IdentificadorUsuarioAD As String

        Property PasswordSupervisor As String

#End Region

#Region "[Métodos]"

        Public Function RecuperarTipoSectores(codigoDelegacion As String, codigoPlanta As String) As List(Of TipoSector)

            ' Se a delegação está preenchida
            If Me._Continentes IsNot Nothing AndAlso Me._Continentes.Count > 0 AndAlso _
                Me._Continentes.First.Paises IsNot Nothing AndAlso Me._Continentes.First.Paises.Count > 0 AndAlso _
                Me._Continentes.First.Paises.First.Delegaciones IsNot Nothing AndAlso Me._Continentes.First.Paises.First.Delegaciones.Exists(Function(e) e.Codigo = codigoDelegacion AndAlso DirectCast(e, ContractoServicio.Login.EjecutarLogin.DelegacionPlanta).Plantas IsNot Nothing) Then

                Return Me._Continentes.First.Paises.First.Delegaciones.Cast(Of DelegacionPlanta)().First(Function(f) f.Codigo = codigoDelegacion).Plantas.First(Function(f) f.CodigoPlanta = codigoPlanta).TiposSectores

            End If

            Return Nothing

        End Function

#End Region

    End Class

End Namespace