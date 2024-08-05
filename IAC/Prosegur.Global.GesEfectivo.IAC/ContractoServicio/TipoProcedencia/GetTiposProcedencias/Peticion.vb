Imports System.Xml.Serialization
Imports System.Xml

Namespace TipoProcedencia.GetTiposProcedencias

    ''' <summary>
    ''' Classe Peticion de Tipo Procedencia
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 14/05/2013 Criado
    ''' </history>

    <XmlType(Namespace:="urn:GetTiposProcedencia")> _
    <XmlRoot(Namespace:="urn:GetTiposProcedencia")> _
    <Serializable()> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

#Region "[VARIAVEIS]"

        Private _codTipoProcedencia As String

        Private _desTipoProcedencia As String

        Private _bolActivo As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property codTipoProcedencia() As String
            Get
                Return _codTipoProcedencia
            End Get
            Set(value As String)
                _codTipoProcedencia = value
            End Set
        End Property

        Public Property desTipoProcedencia() As String
            Get
                Return _desTipoProcedencia
            End Get
            Set(value As String)
                _desTipoProcedencia = value
            End Set
        End Property

        Public Property bolActivo() As Nullable(Of Boolean)
            Get
                Return _bolActivo
            End Get
            Set(value As Nullable(Of Boolean))
                _bolActivo = value
            End Set
        End Property

#End Region

    End Class
End Namespace

