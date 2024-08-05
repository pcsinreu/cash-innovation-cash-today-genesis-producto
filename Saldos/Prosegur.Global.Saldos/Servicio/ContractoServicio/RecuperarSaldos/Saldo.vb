Namespace RecuperarSaldos

    ''' <summary>
    ''' Clase Saldo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [maoliveira] - 07/07/2011 - Creado
    ''' </history>
    Public Class Saldo

#Region "[VARIÁVEIS]"

        Private _Sectores As New Sectores

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Datos de los Sectores
        ''' </summary>
        ''' <value>RecuperarSaldos.Sectores</value>
        ''' <returns>RecuperarSaldos.Sectores</returns>
        ''' <history>[maoliveira] - 07/07/2011 - Creado</history>
        ''' <remarks></remarks>
        Public Property Sectores() As Sectores
            Get
                Return _Sectores
            End Get
            Set(value As Sectores)
                _Sectores = value
            End Set
        End Property

#End Region

    End Class

End Namespace
