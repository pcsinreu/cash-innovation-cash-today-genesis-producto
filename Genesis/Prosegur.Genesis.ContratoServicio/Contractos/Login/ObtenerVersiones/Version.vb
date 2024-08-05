Imports System.Runtime.Serialization

Namespace Login.ObtenerVersiones

    <Serializable>
    <DataContract>
    Public Class Version

#Region " Variáveis "
        Private _CodigoVersion As String
#End Region

#Region "Propriedades"

        <DataMember()>
        Public Property CodigoVersion() As String
            Get
                Return _CodigoVersion
            End Get
            Set(value As String)
                _CodigoVersion = value
            End Set
        End Property

#End Region

    End Class

End Namespace