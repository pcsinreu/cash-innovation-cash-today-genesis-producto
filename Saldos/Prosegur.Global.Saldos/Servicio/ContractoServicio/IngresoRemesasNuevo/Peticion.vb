Imports System.Xml.Serialization
Imports System.Xml

Namespace IngresoRemesasNuevo

    ''' <summary>
    ''' Ingreso de Remesas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 27/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:IngresoRemesasNuevo")> _
    <XmlRoot(Namespace:="urn:IngresoRemesasNuevo")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _Remesas As Remesas = Nothing
        Private _CodigoUsuario As String = Nothing
        Private _EsIntegracionRecepcionEnvio As Boolean = False

#End Region

#Region " Propriedades "

        Public Property EsIntegracionRecepcionEnvio() As Boolean
            Get
                Return _EsIntegracionRecepcionEnvio
            End Get
            Set(value As Boolean)
                _EsIntegracionRecepcionEnvio = value
            End Set
        End Property

        Public Property Remesas() As Remesas
            Get
                Return _Remesas
            End Get
            Set(value As Remesas)
                _Remesas = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

#End Region

    End Class

End Namespace