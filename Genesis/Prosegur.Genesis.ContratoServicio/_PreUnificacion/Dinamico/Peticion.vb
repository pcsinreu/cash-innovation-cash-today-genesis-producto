Imports System.Xml.Serialization

Namespace Dinamico
    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/05/2013 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:Dinamico")> _
    <XmlRoot(Namespace:="urn:Dinamico")> _
    <Serializable()> _
    Public Class Peticion
#Region "[PROPRIEDADES]"
        Public Property StoredProcedure As Boolean
        Public Property CommandText As String
        Public Property Conexao As String
        Public Property ValueField As String
        Public Property LabelField As String
        Public Property Parametros As List(Of Parametro)
#End Region
    End Class

End Namespace
