Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class InformacaoAdicionalCliente

        ''' <summary>
        ''' Recupera as informações adicionais ativas e do saldos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerInformacionesAdicionais() As List(Of Clases.InformacaoAdicionalCliente)

            Dim informacoesAdicionais As New List(Of Clases.InformacaoAdicionalCliente)

            Try

                informacoesAdicionais = AccesoDatos.Genesis.InformacaoAdicionalCliente.ObtenerInformacoesAdicionais

            Catch ex As Exception
                Throw
            End Try

            Return informacoesAdicionais
        End Function

        ''' <summary>
        ''' Recupera informação adicional
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerInformacionAdicional(identificador As String) As Clases.InformacaoAdicionalCliente

            Try

                Return AccesoDatos.Genesis.InformacaoAdicionalCliente.ObtenerInformacaoAdicional(identificador)

            Catch ex As Exception
                Throw
            End Try
        End Function

    End Class
End Namespace