Namespace Clases
    <Serializable()>
    Public Class Certificado
        Inherits BaseClase
        ' TODO : Pendente a definição das propriedades.
        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Estado As Enumeradores.EstadoCertificado
        Public Property CodigoExterno As String
        Public Property FechaHoraCertificado As DateTime
        Public Property EsTodosSectores As Boolean
        Public Property EsTodosCanales As Boolean
        Public Property EsTodasDelegaciones As Boolean
        Public Property FechaHoraCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaHoraModificacion As DateTime
        Public Property UsuarioModificacion As String
    End Class
End Namespace


