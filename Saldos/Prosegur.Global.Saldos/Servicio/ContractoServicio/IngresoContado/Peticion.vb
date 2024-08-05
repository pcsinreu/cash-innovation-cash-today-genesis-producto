Imports System.Xml.Serialization
Imports System.Xml

Namespace IngresoContado

    ''' <summary>
    ''' Ingreso de Remesas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 27/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:IngresoContado")> _
    <XmlRoot(Namespace:="urn:IngresoContado")> _
    <Serializable()> _
   Public Class Peticion

#Region " Variáveis "

        Private _Remesa As Remesa

#End Region

#Region " Propriedades "

        Public Property Remesa() As Remesa
            Get
                Return _Remesa
            End Get
            Set(value As Remesa)
                _Remesa = value
            End Set
        End Property

#End Region

    End Class

End Namespace