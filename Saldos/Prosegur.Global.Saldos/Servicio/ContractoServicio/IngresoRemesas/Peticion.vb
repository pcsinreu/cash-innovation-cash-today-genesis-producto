Imports System.Xml.Serialization
Imports System.Xml

Namespace IngresoRemesas

    ''' <summary>
    ''' Ingreso de Remesas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 27/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:IngresoRemesas")> _
    <XmlRoot(Namespace:="urn:IngresoRemesas")> _
    <Serializable()> _
   Public Class Peticion

#Region " Variáveis "

        Private _Remesas As Remesas

#End Region

#Region " Propriedades "

        Public Property Remesas() As Remesas
            Get
                Return _Remesas
            End Get
            Set(value As Remesas)
                _Remesas = value
            End Set
        End Property

#End Region

    End Class

End Namespace