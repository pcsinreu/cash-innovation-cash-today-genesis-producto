Namespace Clases

    <Serializable()>
    Public Class Transaccion
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Certificado As Certificado
        Private _Documento As Documento
        Private _EstadoDocumento As Enumeradores.EstadoDocumento
        Private _Divisa As Divisa
        Private _TipoSaldo As Enumeradores.TipoSaldo
        Private _TipoSitio As Enumeradores.TipoSitio
        Private _TipoMovimiento As Enumeradores.TipoMovimiento
        Private _Observaciones As String
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _FechaHoraPlanificacionCertificacion As DateTime

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

        Public Property Certificado As Certificado
            Get
                Return _Certificado
            End Get
            Set(value As Certificado)
                SetProperty(_Certificado, value, "Certificado")
            End Set
        End Property

        Public Property Documento As Documento
            Get
                Return _Documento
            End Get
            Set(value As Documento)
                SetProperty(_Documento, value, "Documento")
            End Set
        End Property

        Public Property EstadoDocumento As Enumeradores.EstadoDocumento
            Get
                Return _EstadoDocumento
            End Get
            Set(value As Enumeradores.EstadoDocumento)
                SetProperty(_EstadoDocumento, value, "EstadoDocumento")
            End Set
        End Property

        Public Property Divisa As Divisa
            Get
                Return _Divisa
            End Get
            Set(value As Divisa)
                SetProperty(_Divisa, value, "Divisa")
            End Set
        End Property

        Public Property TipoSaldo As Enumeradores.TipoSaldo
            Get
                Return _TipoSaldo
            End Get
            Set(value As Enumeradores.TipoSaldo)
                SetProperty(_TipoSaldo, value, "TipoSaldo")
            End Set
        End Property

        Public Property TipoSitio As Enumeradores.TipoSitio
            Get
                Return _TipoSitio
            End Get
            Set(value As Enumeradores.TipoSitio)
                SetProperty(_TipoSitio, value, "TipoSitio")
            End Set
        End Property

        Public Property TipoMovimiento As Enumeradores.TipoMovimiento
            Get
                Return _TipoMovimiento
            End Get
            Set(value As Enumeradores.TipoMovimiento)
                SetProperty(_TipoMovimiento, value, "TipoMovimiento")
            End Set
        End Property

        Public Property Observaciones As String
            Get
                Return _Observaciones
            End Get
            Set(value As String)
                SetProperty(_Observaciones, value, "Observaciones")
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

        Public ReadOnly Property Cuenta() As Clases.Cuenta
            Get
                If Me.Documento IsNot Nothing Then
                    ' Se for tipo sitio origem, então recupera a conta de origem
                    ' senão recupera a conta de destino.
                    If Me.TipoSitio = Enumeradores.TipoSitio.Origen Then
                        Return Me.Documento.CuentaOrigen
                    Else
                        Return Me.Documento.CuentaDestino
                    End If
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public ReadOnly Property CuentaSaldo() As Clases.Cuenta
            Get
                If Me.Documento IsNot Nothing Then
                    ' Se for tipo sitio origem, então recupera a conta de saldo origem
                    ' senão recupera a conta de saldo destino.
                    If Me.TipoSitio = Enumeradores.TipoSitio.Origen Then
                        Return Me.Documento.CuentaSaldoOrigen
                    Else
                        Return Me.Documento.CuentaSaldoDestino
                    End If
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Property FechaHoraPlanificacionCertificacion As DateTime
            Get
                Return _FechaHoraPlanificacionCertificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraPlanificacionCertificacion, value, "FechaHoraPlanificacionCertificacion")
            End Set
        End Property
#End Region

    End Class

End Namespace
