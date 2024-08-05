Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Excepcion

Public Class ucTextBox
    Inherits UcBase

#Region "Propriedades"

#Region "    Entrada    "

    ''' <summary>
    ''' Define el titulo del controle
    ''' </summary>
    ''' <value>String</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Titulo As String
    ''' <summary>
    ''' Define el tamaño (maxlength) del controle
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Tamano As Integer
    ''' <summary>
    ''' Define el tipo de dato que el controle soporta
    ''' </summary>
    ''' <value>Enumeradores.TiposInternos</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TipoInterno As Enumeradores.TiposInternos

    ''' <summary>
    ''' Define un valor padrón para ser insertado en el componente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValorPadron As String

#End Region

#Region "    Salida    "

    ''' <summary>
    ''' Almacena el valor digitado en el controle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Valor As String

#End Region

#End Region

#Region "Overrides"

    Protected Overrides Sub AdicionarScripts()

        If Me.TipoInterno = Enumeradores.TiposInternos.Fecha Then
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}'); ", Me.txtControle.ClientID, "False")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Me.ID, script, True)

        ElseIf Me.TipoInterno = Enumeradores.TiposInternos.FechaHora Then
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}'); ", Me.txtControle.ClientID, "True")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Me.ID, script, True)

        End If

    End Sub

#End Region

#Region "Eventos"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Me.CargarDatos()

        Catch ex As Genesis.Excepcion.NegocioExcepcion
            MyBase.NotificarErro(ex)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Cargar el controle con la definición de los parámetros de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarDatos()

        Dim Mesajes As New StringBuilder
        If Me.Validar(Mesajes) Then

            Me.lbltituloTextBox.Text = Titulo
            Me.txtControle.MaxLength = Tamano

            Select Case Me.TipoInterno
                Case Enumeradores.TiposInternos.Entero
                    If Not String.IsNullOrEmpty(Me.ValorPadron) Then
                        Me.txtControle.Text = Me.ValorPadron
                    End If

                    Me.txtControle.Attributes.Add("onkeypress", "return ValorNumerico(event);")

                Case Enumeradores.TiposInternos.Fraccion
                    If Not String.IsNullOrEmpty(Me.ValorPadron) Then
                        Me.txtControle.Text = Me.ValorPadron
                    End If

                    Me.txtControle.Attributes.Add("onkeypress", "return bloqueialetrasImporte(event,this);")
                    Me.txtControle.Attributes.Add("onpaste", "return false;")
                    Me.txtControle.Attributes.Add("onkeyup", String.Format("moedaIAC(event,this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))

                Case Enumeradores.TiposInternos.Fecha
                    If Not String.IsNullOrEmpty(Me.ValorPadron) Then
                        Me.txtControle.Text = Date.Parse(Me.ValorPadron)

                    End If

                Case Enumeradores.TiposInternos.FechaHora
                    If Not String.IsNullOrEmpty(Me.ValorPadron) Then
                        Me.txtControle.Text = DateTime.Parse(Me.ValorPadron)

                    End If

                Case Enumeradores.TiposInternos.Hora
                    Throw New NotImplementedException

            End Select

        Else
            Throw New Genesis.Excepcion.NegocioExcepcion(Mesajes.ToString)

        End If

    End Sub

    ''' <summary>
    ''' Almacenar el valor selecionado en el controle
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GuardarDatos()
        If Me.txtControle IsNot Nothing Then
            Me.Valor = txtControle.Text
        End If
    End Sub

    ''' <summary>
    ''' Validar el unico parámetro obligatorio del controle
    ''' </summary>
    ''' <param name="Mesajes">Lista de los errores</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Validar(ByRef Mesajes As StringBuilder) As Boolean

        If String.IsNullOrEmpty(Me.Titulo) Then
            Mesajes.AppendLine(String.Format(Traduzir("014_parametro_obrigatorio"), "Titulo"))
        End If

        If IsDBNull(Me.Tamano) Then
            Mesajes.AppendLine(String.Format(Traduzir("014_parametro_obrigatorio"), "Tamano"))
        End If

        If Mesajes.Length > 0 Then
            Return False

        End If

        Return True

    End Function

#End Region

End Class