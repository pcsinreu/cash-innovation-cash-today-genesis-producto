Imports System.Xml.Serialization
Imports System.Xml

Namespace GrupoCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' </history>
    <Serializable()> _
    Public Class GrupoClienteDetalle


#Region "[VARIÁVEIS]"

        Private _oidGrupoCliente As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Vigente As Boolean
        Private _CodigoUsuario As String
        Private _Clientes As ClienteDetalleColeccion
        Private _FyhAtualizacion As Date
        Private _Direccion As Direccion.DireccionBase
        Private _codTipoGrupoCliente As String


#End Region

#Region "[PROPRIEDADES]"

        Public Property oidGrupoCliente() As String
            Get
                Return _oidGrupoCliente
            End Get
            Set(value As String)
                _oidGrupoCliente = value
            End Set
        End Property


        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property Vigente As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

        Public Property CodigoUsuario As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

        Public Property Clientes As ClienteDetalleColeccion
            Get
                Return _Clientes
            End Get
            Set(value As ClienteDetalleColeccion)
                _Clientes = value
            End Set
        End Property

        Public Property FyhAtualizacion As Date
            Get
                Return _FyhAtualizacion
            End Get
            Set(value As Date)
                _FyhAtualizacion = value
            End Set
        End Property

        Public Property Direccion As Direccion.DireccionBase
            Get
                Return _Direccion
            End Get
            Set(value As Direccion.DireccionBase)
                _Direccion = value
            End Set
        End Property

        Public Property CodTipoGrupoCliente() As String
            Get
                Return _codTipoGrupoCliente
            End Get
            Set(value As String)
                _codTipoGrupoCliente = value
            End Set
        End Property



#End Region

    End Class

End Namespace