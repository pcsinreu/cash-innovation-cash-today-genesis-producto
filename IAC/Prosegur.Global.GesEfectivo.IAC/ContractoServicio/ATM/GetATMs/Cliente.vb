﻿
Namespace GetATMs

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 07/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Cliente

#Region "[Variáveis]"

        Private _codigoCliente As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace