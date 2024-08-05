Imports System.Xml.Serialization

Namespace Clases

    <Serializable> _
    Public Class Objeto
        Inherits BindableBase

#Region "[VARIAVEIS]"

        Private _Identificador As String
        Private _IdentificadorRemesa As String
        Private _IdentificadorMorfologiaComponente As String
        Private _TipoObjeto As Enumeradores.TipoObjeto
        Private _CodigoIdentificador As String
        Private _CodigoUsuario As String
        Private _FechaActualizacion As Date

#End Region

#Region "[PROPRIEDADES]"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property IdentificadorRemesa As String
            Get
                Return _IdentificadorRemesa
            End Get
            Set(value As String)
                SetProperty(_IdentificadorRemesa, value, "IdentificadorRemesa")
            End Set
        End Property

        Public Property IdentificadorMorfologiaComponente As String
            Get
                Return _IdentificadorMorfologiaComponente
            End Get
            Set(value As String)
                SetProperty(_IdentificadorMorfologiaComponente, value, "IdentificadorMorfologiaComponente")
            End Set
        End Property

        Public Property TipoObjeto As Enumeradores.TipoObjeto
            Get
                Return _TipoObjeto
            End Get
            Set(value As Enumeradores.TipoObjeto)
                SetProperty(_TipoObjeto, value, "TipoObjeto")
            End Set
        End Property

        Public Property CodigoIdentificador As String
            Get
                Return _CodigoIdentificador
            End Get
            Set(value As String)
                SetProperty(_CodigoIdentificador, value, "CodigoIdentificador")
            End Set
        End Property

        Public Property CodigoUsuario As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                SetProperty(_CodigoUsuario, value, "CodigoUsuario")
            End Set
        End Property

        Public Property FechaActualizacion As Date
            Get
                Return _FechaActualizacion
            End Get
            Set(value As Date)
                SetProperty(_FechaActualizacion, value, "FechaAtualizacion")
            End Set
        End Property



#End Region

    End Class

End Namespace