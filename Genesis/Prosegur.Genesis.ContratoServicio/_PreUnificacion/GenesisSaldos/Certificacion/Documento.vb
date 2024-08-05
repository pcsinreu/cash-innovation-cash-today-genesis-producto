Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe Documento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/05/2013 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Documento

#Region "[PROPRIEDADES]"

        Public Property OidDocumento As String
        Public Property OidGrupoDocumento As String
        Public Property CodTipoDocumento As String
        Public Property DesTipoDocumento As String
        Public Property FyhPlanCertificacion As DateTime
        Public Property CodExterno As String
        Public Property CodEstado As String
        Public Property TransaccionesEfectivo As TransaccionEfectivoColeccion
        Public Property TransaccionesMedioPago As TransaccionMedioPagoColeccion

#End Region

    End Class

End Namespace