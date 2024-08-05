Imports System.Xml.Serialization
Imports System.Xml


Namespace ImportarParametros

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/09/2011 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:ImportarParametros")> _
    <XmlRoot(Namespace:="urn:ImportarParametros")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property DatosPuesto As DatosPuesto

#End Region

    End Class
End Namespace