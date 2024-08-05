Imports System.Collections.ObjectModel

Namespace Clases
    ' ***********************************************************************
    '  Modulo:  ConfigGrid.vb
    '  Descripción: Clase definición Configuración de las Grid de Salidas
    ' ***********************************************************************
    <Serializable()>
    Public Class ConfigGrid
        Inherits BindableBase

#Region "[VARIAVEIS]"

        Private _CodigoControl As String
        Private _CodigoFuncionalidad As String
        Private _ConfigXML As Byte()

#End Region

#Region "[PROPRIEDADES]"

        Public Property ConfigXML As Byte()
            Get
                Return _ConfigXML
            End Get
            Set(value As Byte())
                SetProperty(_ConfigXML, value, "ConfigXML")
            End Set
        End Property
        Public Property CodigoControl As String
            Get
                Return _CodigoControl
            End Get
            Set(value As String)
                SetProperty(_CodigoControl, value, "CodigoControl")
            End Set
        End Property
        Public Property CodigoFuncionalidad As String
            Get
                Return _CodigoFuncionalidad
            End Get
            Set(value As String)
                SetProperty(_CodigoFuncionalidad, value, "CodigoFuncionalidad")
            End Set
        End Property

#End Region

    End Class
End Namespace

