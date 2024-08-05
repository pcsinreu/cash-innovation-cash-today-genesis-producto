
Namespace Delegacion.GetDelegacionByCertificado
    Public Class Delegacion

#Region "[VARIAVEIS]"

        Private _oidDelegacion As String
        Private _CodDelegacion As String
        Private _DesDelegacion As String
        Private _oidPlanta As String
        Private _CodPlanta As String
        Private _DesPlanta As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidDelegacion() As String
            Get
                Return _oidDelegacion
            End Get
            Set(value As String)
                _oidDelegacion = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

        Public Property DesDelegacion() As String
            Get
                Return _DesDelegacion
            End Get
            Set(value As String)
                _DesDelegacion = value
            End Set
        End Property

        Public Property OidPlanta() As String
            Get
                Return _oidPlanta
            End Get
            Set(value As String)
                _oidPlanta = value
            End Set
        End Property

        Public Property CodPlanta() As String
            Get
                Return _CodPlanta
            End Get
            Set(value As String)
                _CodPlanta = value
            End Set
        End Property

        Public Property DesPlanta() As String
            Get
                Return _DesPlanta
            End Get
            Set(value As String)
                _DesPlanta = value
            End Set
        End Property

#End Region

    End Class
End Namespace
