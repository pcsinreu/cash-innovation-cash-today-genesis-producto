Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Clases

    <Serializable> _
    Public Class ATM
        Inherits BindableBase

#Region "[VARIAVEIS]"

        Private _DescripcionModeloCajero As String
        Private _DescripcionRed As String
        Private _CodigoCajero As String
        Private _ModalidadRecogida As Enumeradores.ModalidadRecogida

#End Region

#Region "[PROPRIEDADES]"

        Public Property DescripcionModeloCajero As String
            Get
                Return _DescripcionModeloCajero
            End Get
            Set(value As String)
                SetProperty(_DescripcionModeloCajero, value, "DescripcionModeloCajero")
            End Set
        End Property

        Public Property DescripcionRed As String
            Get
                Return _DescripcionRed
            End Get
            Set(value As String)
                SetProperty(_DescripcionRed, value, "DescripcionRed")
            End Set
        End Property

        Public Property CodigoCajero As String
            Get
                Return _CodigoCajero
            End Get
            Set(value As String)
                SetProperty(_CodigoCajero, value, "CodigoCajero")
            End Set
        End Property

        Public Property ModalidadRecogida As Enumeradores.ModalidadRecogida
            Get
                Return _ModalidadRecogida
            End Get
            Set(value As Enumeradores.ModalidadRecogida)
                SetProperty(_ModalidadRecogida, value, "ModalidadRecogida")
            End Set
        End Property

#End Region

    End Class

End Namespace