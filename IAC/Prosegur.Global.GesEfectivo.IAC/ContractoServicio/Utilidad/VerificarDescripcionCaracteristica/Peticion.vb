Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarDescripcionCaracteristica

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 15/05/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarDescripcionCaracteristica")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionCaracteristica")> _
    <Serializable()> _
Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _descripcion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

#End Region
    End Class
End Namespace