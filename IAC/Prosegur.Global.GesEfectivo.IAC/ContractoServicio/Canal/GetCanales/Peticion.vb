Imports System.Xml.Serialization
Imports System.Xml


Namespace Canal.GetCanales

    <XmlType(Namespace:="urn:GetCanales")> _
    <XmlRoot(Namespace:="urn:GetCanales")> _
    <Serializable()> _
    Public Class Peticion

        Private _codigoCanal As List(Of String)
        Private _descripcionCanal As List(Of String)
        Private _bolVigente As Boolean


        Public Property codigoCanal() As List(Of String)
            Get
                Return _codigoCanal
            End Get
            Set(value As List(Of String))
                _codigoCanal = value
            End Set
        End Property


        Public Property descripcionCanal() As List(Of String)
            Get
                Return _descripcionCanal
            End Get
            Set(value As List(Of String))
                _descripcionCanal = value
            End Set
        End Property


        Public Property bolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property


    End Class

End Namespace