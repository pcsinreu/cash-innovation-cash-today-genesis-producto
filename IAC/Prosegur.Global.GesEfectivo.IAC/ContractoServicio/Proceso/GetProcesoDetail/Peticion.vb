Imports System.Xml.Serialization
Imports System.Xml

Namespace Proceso.GetProcesoDetail

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 16/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetProcesoDetail")> _
    <XmlRoot(Namespace:="urn:GetProcesoDetail")> _
    <Serializable()> _
Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _peticionProcesos As PeticionProcesoColeccion

#End Region

#Region "[PROPRIEDADES]"

        Public Property PeticionProcesos() As PeticionProcesoColeccion
            Get
                Return _peticionProcesos
            End Get
            Set(value As PeticionProcesoColeccion)
                _peticionProcesos = value
            End Set
        End Property

#End Region

    End Class
End Namespace