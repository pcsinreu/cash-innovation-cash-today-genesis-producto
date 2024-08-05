Imports System.Xml.Serialization

Namespace CargaPreviaEletronica.GetConfiguracion


    <Serializable()> _
    Public Class Iac

#Region "Campos e Propriedades"

        Private _codigoIAC As String
        Private _descripcionIac As String
        Private _formatoFecha As String
        Private _campo As Campo


        Public Property CodigoIAC() As String
            Get
                Return _codigoIAC
            End Get
            Set(value As String)
                _codigoIAC = value
            End Set
        End Property

        Public Property DescripcionIac() As String
            Get
                Return _descripcionIac
            End Get
            Set(value As String)
                _descripcionIac = value
            End Set
        End Property

        Public Property FormatoFecha() As String
            Get
                Return _formatoFecha
            End Get
            Set(value As String)
                _formatoFecha = value
            End Set
        End Property

        Public Property Campo() As Campo
            Get
                Return _campo
            End Get
            Set(value As Campo)
                _campo = value
            End Set
        End Property





#End Region

    End Class

End Namespace