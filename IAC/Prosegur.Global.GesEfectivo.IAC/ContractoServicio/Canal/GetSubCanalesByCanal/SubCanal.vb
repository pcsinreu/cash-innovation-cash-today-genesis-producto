Imports System.Xml.Serialization

Namespace Canal.GetSubCanalesByCanal
    <Serializable()> _
    Public Class SubCanal

        Private _oidSubCanal As String
        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _vigente As Boolean
        Private _codigosAjenosSet As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Private _codigosAjenos As CodigoAjeno.CodigoAjenoColeccionBase


        Public Property OidSubCanal() As String
            Get
                Return _oidSubCanal
            End Get
            Set(value As String)
                _oidSubCanal = value
            End Set
        End Property

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property CodigosAjenos() As CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _codigosAjenos
            End Get
            Set(value As CodigoAjeno.CodigoAjenoColeccionBase)
                _codigosAjenos = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property CodigosAjenosSet() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _codigosAjenosSet
            End Get
            Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
                _codigosAjenosSet = value
            End Set
        End Property

    End Class
End Namespace
