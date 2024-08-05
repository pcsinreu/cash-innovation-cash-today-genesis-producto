Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoSetor.VerificaTipoSectores

    <XmlType(Namespace:="urn:VerificaTipoSectores")> _
    <XmlRoot(Namespace:="urn:VerificaTipoSectores")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _Codigo As String
#End Region

#Region "[Propriedades]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

#End Region

    End Class
End Namespace
