Imports System.Runtime.Serialization

Namespace Login.EjecutarLogin

    ''' <summary>
    ''' AplicacionVersion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [prezende] 11/05/2012 Criado
    ''' </history>
    <Serializable()> _
    Public Class AplicacionVersion

#Region " Variáveis "

        Private _OidAplicacion As String = String.Empty
        Private _CodigoAplicacion As String = String.Empty
        Private _DescripcionAplicacion As String = String.Empty
        Private _CodigoBuild As String = String.Empty
        Private _CodigoVersion As String = String.Empty
        Private _DesURLServicio As String = String.Empty
        Private _DesURLSitio As String = String.Empty

#End Region

#Region "Propriedades"

        Public Property OidAplicacion() As String
            Get
                Return _OidAplicacion
            End Get
            Set(value As String)
                _OidAplicacion = value
            End Set
        End Property

        Public Property CodigoAplicacion() As String
            Get
                Return _CodigoAplicacion
            End Get
            Set(value As String)
                _CodigoAplicacion = value
            End Set
        End Property

        Public Property DescripcionAplicacion() As String
            Get
                Return _DescripcionAplicacion
            End Get
            Set(value As String)
                _DescripcionAplicacion = value
            End Set
        End Property

        Public Property CodigoBuild() As String
            Get
                Return _CodigoBuild
            End Get
            Set(value As String)
                _CodigoBuild = value
            End Set
        End Property

        Public Property CodigoVersion() As String
            Get
                Return _CodigoVersion
            End Get
            Set(value As String)
                _CodigoVersion = value
            End Set
        End Property

        Public Property DesURLServicio() As String
            Get
                Return _DesURLServicio
            End Get
            Set(value As String)
                _DesURLServicio = value
            End Set
        End Property

        Public Property DesURLSitio() As String
            Get
                Return _DesURLSitio
            End Get
            Set(value As String)
                _DesURLSitio = value
            End Set
        End Property

#End Region

    End Class

End Namespace