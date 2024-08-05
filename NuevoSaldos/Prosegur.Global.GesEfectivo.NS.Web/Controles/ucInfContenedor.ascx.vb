Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon

Public Class ucInfContenedor
    Inherits UcBase


#Region "[PROPRIEDADES]"

    Private _Contenedor As Clases.Contenedor
    Public Property Contenedor() As Clases.Contenedor
        Get
            Return _Contenedor
        End Get
        Set(value As Clases.Contenedor)
            _Contenedor = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Inicializa controles na tela.
    ''' </summary>
    Protected Overrides Sub Inicializar()
        Try
            Me.ConfigurarControles()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configura foco nos UserControls 
    ''' </summary>
    Public Overrides Sub Focus()
        MyBase.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

#End Region

#Region "[METODOS]"


    ''' <summary>
    ''' Carrega Canal e SubCanal utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        If Contenedor IsNot Nothing AndAlso Not String.IsNullOrEmpty(Contenedor.Codigo) Then

            ucListaContenedor.Style.Item("display") = "block"
            lblTituloContenedor.Text = Traduzir("033_datos_contenedor_Titulo")

            dvCodigo.Style.Item("display") = "block"
            lblCodigo.Text = Traduzir("033_datos_contenedor_Codigo")
            txtCodigo.Text = Contenedor.Codigo

            If Contenedor.Precintos IsNot Nothing AndAlso Contenedor.Precintos.Count > 0 Then
                dvPrecintos.Style.Item("display") = "block"
                lblPrecintos.Text = Traduzir("033_datos_contenedor_Precintos")
                litPrecintos.Text = ""
                For Each precinto In Contenedor.Precintos
                    If Not String.IsNullOrEmpty(precinto) Then
                        litPrecintos.Text &= "<div id='item_" & precinto & "'>" & precinto & "</div>"
                    End If
                Next
            End If

        End If

    End Sub

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

#End Region

End Class