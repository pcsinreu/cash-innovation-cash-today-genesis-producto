Imports System.Collections.ObjectModel

Namespace Clases

    <Serializable()>
    Public NotInheritable Class Documento
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Formulario As Formulario
        Private _CuentaOrigen As Cuenta
        Private _CuentaDestino As Cuenta
        Private _CuentaSaldoOrigen As Cuenta
        Private _CuentaSaldoDestino As Cuenta
        Private _DocumentoPadre As Documento
        Private _IdentificadorSustituto As String
        Private _FechaHoraPlanificacionCertificacion As DateTime
        Private _FechaHoraGestion As DateTime?
        Private _EstaCertificado As Boolean
        Private _CodigoComprobante As String
        Private _NumeroExterno As String
        Private _Estado As Enumeradores.EstadoDocumento
        Private _Elemento As Elemento
        Private _Divisas As New ObservableCollection(Of Divisa)
        Private _DivisasSaldoAnterior As New ObservableCollection(Of Divisa)
        Private _EstadosPosibles As New ObservableCollection(Of Enumeradores.EstadoDocumento)
        Private _GrupoTerminosIAC As GrupoTerminosIAC
        Private _Historico As ObservableCollection(Of HistoricoMovimientoDocumento)
        Private _DocumentosRelacionados As New ObservableCollection(Of Documento)
        Private _IdentificadorGrupo As String
        Private _IdentificadorMovimentacionFondo As String
        Private _TipoDocumento As TipoDocumento
        Private _ExportadoSol As Boolean
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _NelIntentoEnvio As Integer
        Private _IdentificadorIntegracion As String
        Private _CodigoCertificacionCuentas As String
        Private _CodigoATM As String
        Private _MensajeExterno As String
        Private _Rowver As Nullable(Of Int64)
        Private _SaldoSuprimido As Boolean
        Private _Notificado As Boolean

#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Formulario As Formulario
            Get
                Return _Formulario
            End Get
            Set(value As Formulario)
                SetProperty(_Formulario, value, "Formulario")
            End Set
        End Property

        Public Property CuentaOrigen As Cuenta
            Get
                Return _CuentaOrigen
            End Get
            Set(value As Cuenta)
                SetProperty(_CuentaOrigen, value, "CuentaOrigen")
            End Set
        End Property

        Public Property CuentaDestino As Cuenta
            Get
                Return _CuentaDestino
            End Get
            Set(value As Cuenta)
                SetProperty(_CuentaDestino, value, "CuentaDestino")
            End Set
        End Property

        Public Property CuentaSaldoOrigen As Cuenta
            Get
                Return _CuentaSaldoOrigen
            End Get
            Set(value As Cuenta)
                SetProperty(_CuentaSaldoOrigen, value, "CuentaSaldoOrigen")
            End Set
        End Property

        Public Property CuentaSaldoDestino As Cuenta
            Get
                Return _CuentaSaldoDestino
            End Get
            Set(value As Cuenta)
                SetProperty(_CuentaSaldoDestino, value, "CuentaSaldoDestino")
            End Set
        End Property

        Public Property DocumentoPadre As Documento
            Get
                Return _DocumentoPadre
            End Get
            Set(value As Documento)
                SetProperty(_DocumentoPadre, value, "DocumentoPadre")
            End Set
        End Property

        Public Property IdentificadorSustituto As String
            Get
                Return _IdentificadorSustituto
            End Get
            Set(value As String)
                SetProperty(_IdentificadorSustituto, value, "IdentificadorSustituto")
            End Set
        End Property

        Public Property FechaHoraPlanificacionCertificacion As DateTime
            Get
                Return _FechaHoraPlanificacionCertificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraPlanificacionCertificacion, value, "FechaHoraPlanificacionCertificacion")
            End Set
        End Property

        ''' <summary>
        ''' Armazena a data e hora em que o serviço relacionado foi executado no cliente. Exemplo: no momento em que o “Malote” foi retirado do cliente.
        ''' Permite valor núlo.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property FechaHoraGestion As DateTime?
            Get
                Return _FechaHoraGestion
            End Get
            Set(value As DateTime?)
                SetProperty(_FechaHoraGestion, value, "FechaHoraGestion")
            End Set
        End Property

        Public Property EstaCertificado As Boolean
            Get
                Return _EstaCertificado
            End Get
            Set(value As Boolean)
                SetProperty(_EstaCertificado, value, "EstaCertificado")
            End Set
        End Property

        Public Property CodigoComprobante As String
            Get
                Return _CodigoComprobante
            End Get
            Set(value As String)
                SetProperty(_CodigoComprobante, value, "CodigoComprobante")
            End Set
        End Property

        Public Property NumeroExterno As String
            Get
                Return _NumeroExterno
            End Get
            Set(value As String)
                SetProperty(_NumeroExterno, value, "NumeroExterno")
            End Set
        End Property

        Public Property Estado As Enumeradores.EstadoDocumento
            Get
                Return _Estado
            End Get
            Set(value As Enumeradores.EstadoDocumento)
                SetProperty(_Estado, value, "Estado")
            End Set
        End Property

        Public Property Elemento As Elemento
            Get
                Return _Elemento
            End Get
            Set(value As Elemento)
                SetProperty(_Elemento, value, "Elemento")
            End Set
        End Property

        Public Property Divisas As ObservableCollection(Of Divisa)
            Get
                If _Divisas Is Nothing Then
                    _Divisas = New ObservableCollection(Of Divisa)
                End If
                Return _Divisas
            End Get
            Set(value As ObservableCollection(Of Divisa))
                SetProperty(_Divisas, value, "Divisas")
            End Set
        End Property

        Public Property DivisasSaldoAnterior As ObservableCollection(Of Divisa)
            Get
                If _DivisasSaldoAnterior Is Nothing Then
                    _DivisasSaldoAnterior = New ObservableCollection(Of Divisa)
                End If
                Return _DivisasSaldoAnterior
            End Get
            Set(value As ObservableCollection(Of Divisa))
                SetProperty(_DivisasSaldoAnterior, value, "DivisasSaldoAnterior")
            End Set
        End Property

        Public Property EstadosPosibles As ObservableCollection(Of Enumeradores.EstadoDocumento)
            Get
                If _EstadosPosibles Is Nothing Then
                    _EstadosPosibles = New ObservableCollection(Of Enumeradores.EstadoDocumento)
                End If
                Return _EstadosPosibles
            End Get
            Set(value As ObservableCollection(Of Enumeradores.EstadoDocumento))
                SetProperty(_EstadosPosibles, value, "EstadosPosibles")
            End Set
        End Property

        Public Property GrupoTerminosIAC As GrupoTerminosIAC
            Get
                Return _GrupoTerminosIAC
            End Get
            Set(value As GrupoTerminosIAC)
                SetProperty(_GrupoTerminosIAC, value, "Identificador")
            End Set
        End Property

        Public Property Historico As ObservableCollection(Of HistoricoMovimientoDocumento)
            Get
                Return _Historico
            End Get
            Set(value As ObservableCollection(Of HistoricoMovimientoDocumento))
                SetProperty(_Historico, value, "Historico")
            End Set
        End Property

        Public Property DocumentosRelacionados As ObservableCollection(Of Documento)
            Get
                If _DocumentosRelacionados Is Nothing Then
                    DocumentosRelacionados = New ObservableCollection(Of Documento)
                End If
                Return _DocumentosRelacionados
            End Get
            Set(value As ObservableCollection(Of Documento))
                SetProperty(_DocumentosRelacionados, value, "DocumentosRelacionados")
            End Set
        End Property

        Public Property IdentificadorGrupo As String
            Get
                Return _IdentificadorGrupo
            End Get
            Set(value As String)
                SetProperty(_IdentificadorGrupo, value, "IdentificadorGrupo")
            End Set
        End Property

        Public Property IdentificadorMovimentacionFondo As String
            Get
                Return _IdentificadorMovimentacionFondo
            End Get
            Set(value As String)
                SetProperty(_IdentificadorMovimentacionFondo, value, "IdentificadorMovimentacionFondo")
            End Set
        End Property

        'Propriedades que estavam faltando..
        'Porem são compos que não permite nulo no banco de dados.
        Public Property TipoDocumento As TipoDocumento
            Get
                Return _TipoDocumento
            End Get
            Set(value As TipoDocumento)
                SetProperty(_TipoDocumento, value, "TipoDocumento")
            End Set
        End Property

        Public Property ExportadoSol As Boolean
            Get
                Return _ExportadoSol
            End Get
            Set(value As Boolean)
                SetProperty(_ExportadoSol, value, "ExportadoSol")
            End Set
        End Property

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
            End Set
        End Property

        Public Property UsuarioCreacion As String
            Get
                Return _UsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioCreacion, value, "UsuarioCreacion")
            End Set
        End Property

        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraModificacion, value, "FechaHoraModificacion")
            End Set
        End Property

        Public Property UsuarioModificacion As String
            Get
                Return _UsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioModificacion, value, "UsuarioModificacion")
            End Set
        End Property

        Public ReadOnly Property SectorOrigen() As Sector
            Get
                Return If(CuentaOrigen IsNot Nothing, CuentaOrigen.Sector, Nothing)
            End Get
        End Property

        Public ReadOnly Property SectorDestino() As Sector
            Get
                Return If(CuentaDestino IsNot Nothing, CuentaDestino.Sector, Nothing)
            End Get
        End Property

        Public Property NelIntentoEnvio As Integer
            Get
                Return _NelIntentoEnvio
            End Get
            Set(value As Integer)
                SetProperty(_NelIntentoEnvio, value, "NelIntentoEnvio")
            End Set
        End Property

        Public Property IdentificadorIntegracion As String
            Get
                Return _IdentificadorIntegracion
            End Get
            Set(value As String)
                SetProperty(_IdentificadorIntegracion, value, "IdentificadorIntegracion")
            End Set
        End Property

        ''' <summary>
        '''N – No Certificadas
        '''O – Cuenta Origen Certificada
        '''D – Cuenta Destino Certificada
        '''A – Ambas Cuentas Certificadas
        ''' </summary>
        Public Property CodigoCertificacionCuentas As String
            Get
                Return _CodigoCertificacionCuentas
            End Get
            Set(value As String)
                SetProperty(_CodigoCertificacionCuentas, value, "CodigoCertificacionCuentas")
            End Set
        End Property

        Public Property CodigoATM As String
            Get
                Return _CodigoATM
            End Get
            Set(value As String)
                SetProperty(_CodigoATM, value, "CodigoATM")
            End Set
        End Property

        Public Property MensajeExterno As String
            Get
                Return _MensajeExterno
            End Get
            Set(value As String)
                SetProperty(_MensajeExterno, value, "MensajeExterno")
            End Set
        End Property

        Public Property Rowver As Nullable(Of Int64)
            Get
                Return _Rowver
            End Get
            Set(value As Nullable(Of Int64))
                SetProperty(_Rowver, value, "Rowver")
            End Set
        End Property

        Public Property SaldoSuprimido As Boolean
            Get
                Return _SaldoSuprimido
            End Get
            Set(value As Boolean)
                SetProperty(_SaldoSuprimido, value, "SaldoSuprimido")
            End Set
        End Property

        Public Property Notificado As Boolean
            Get
                Return _Notificado
            End Get
            Set(value As Boolean)
                SetProperty(_Notificado, value, "Notificado")
            End Set
        End Property

#End Region

    End Class

End Namespace



