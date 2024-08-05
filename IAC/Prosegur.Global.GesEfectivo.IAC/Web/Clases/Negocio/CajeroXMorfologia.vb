Imports Prosegur.Framework.Dicionario.Tradutor

Namespace Negocio

    <Serializable()> _
Public Class CajeroXMorfologia
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidCajeroXMorfologia As String
        Private _oidMorfologia As String
        Private _oidCajero As String
        Private _fecInicio As DateTime
        Private _codUsuario As String
        Private _fyhActualizacion As DateTime

        Private _morfologia As New Morfologia

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidCajeroXMorfologia() As String
            Get
                Return _oidCajeroXMorfologia
            End Get
            Set(value As String)
                _oidCajeroXMorfologia = value
            End Set
        End Property

        Public Property OidMorfologia() As String
            Get
                Return _oidMorfologia
            End Get
            Set(value As String)
                _oidMorfologia = value
            End Set
        End Property

        Public Property OidCajero() As String
            Get
                Return _oidCajero
            End Get
            Set(value As String)
                _oidCajero = value
            End Set
        End Property

        Public Property FecInicio() As DateTime
            Get
                Return _fecInicio
            End Get
            Set(value As DateTime)
                _fecInicio = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

        Public Property FyhActualizacion() As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

        Public Property Morfologia() As Morfologia
            Get
                Return _morfologia
            End Get
            Set(value As Morfologia)
                _morfologia = value
            End Set
        End Property

        Public ReadOnly Property CodDescMorfologia() As String
            Get
                If _morfologia Is Nothing Then
                    Return String.Empty
                Else
                    Return _morfologia.CodMorfologia & " - " & _morfologia.DesMorfologia
                End If
            End Get
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()

        End Sub

        Public Sub New(OidCajeroXMorfologia As String, _
                       OidMorfologia As String, _
                       OidCajero As String, _
                       FechaInicio As DateTime, _
                       CodigoUsuario As String, _
                       FechaActualizacion As DateTime)

            _oidCajeroXMorfologia = OidCajeroXMorfologia
            _oidMorfologia = OidMorfologia
            _oidCajero = OidCajero
            _fecInicio = FechaInicio
            _fyhActualizacion = FechaActualizacion

        End Sub

        Public Sub New(objMorfologia As ContractoServicio.ATM.GetATMDetail.Morfologia, OidCajero As String, _
                       OidCajeroXMorfologia As String)

            _oidCajeroXMorfologia = OidCajeroXMorfologia
            _oidMorfologia = objMorfologia.OidMorfologia
            _oidCajero = OidCajero
            _fecInicio = objMorfologia.FechaInicio
            _fyhActualizacion = objMorfologia.FechaInicio

            ' preenche morfologia
            _morfologia = New Morfologia
            _morfologia.getMorfologia(objMorfologia.OidMorfologia)

        End Sub

        Public Sub New(OidCajeroXMorfologia As String, _
                       OidCajero As String, _
                       FechaInicio As DateTime, _
                       CodigoUsuario As String, _
                       FechaActualizacion As DateTime, _
                       objMorfologia As Morfologia)

            Me.New(OidCajeroXMorfologia, objMorfologia.OidMorfologia, OidCajero, FechaInicio, CodigoUsuario, FechaActualizacion)

            _morfologia = objMorfologia

        End Sub

#End Region

#Region "[MÉTODOS]"

      

#End Region

    End Class

End Namespace