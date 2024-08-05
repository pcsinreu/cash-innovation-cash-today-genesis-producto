Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class ucData
    Inherits System.Web.UI.UserControl

    Private MaxLength As Integer

#Region "[Propriedades]"

    Public Property DatasObrigatorias As Boolean
        Get
            Return ViewState("DatasObrigatorias")
        End Get
        Set(value As Boolean)
            ViewState("DatasObrigatorias") = value
        End Set
    End Property
    Public Property LabelData As String
        Get
            Return lblData.Text
        End Get
        Set(value As String)
            lblData.Text = value
        End Set
    End Property

    Public Property DataFinalVisivel As Boolean
        Get
            Return ViewState("DataFinalVisivel")
        End Get
        Set(value As Boolean)
            ViewState("DataFinalVisivel") = value
        End Set
    End Property

    Public Property DataComHoras As Boolean
        Get
            Return ViewState("DataComHoras")
        End Get
        Set(value As Boolean)
            ViewState("DataComHoras") = value
        End Set
    End Property

    Public Property DataInicio As String
        Get
            Return txtDataInicio.Text
        End Get
        Set(value As String)
            txtDataInicio.Text = value
        End Set
    End Property

    Public Property DataFin As String
        Get
            Return txtDataFin.Text
        End Get
        Set(value As String)
            txtDataFin.Text = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TraduzirControles()
        ConfigurarControles()
    End Sub

    Public Sub New()
        Me.DatasObrigatorias = False
        Me.DataFinalVisivel = True
        Me.DataComHoras = True
        Me.MaxLength = 16
    End Sub

#Region "METODOS"
    Private Sub ConfigurarControles()

        Dim script As String = String.Empty
      ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()
        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

        If DataComHoras Then
            Me.txtDataFin.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
            Me.txtDataInicio.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
            Me.MaxLength = 16
        Else
            Me.txtDataFin.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/####');")
            Me.txtDataInicio.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/####');")
            Me.MaxLength = 10
        End If

       
        If DataComHoras Then
            script &= String.Format("AbrirCalendario('{0}','{1}',{2});", _
                                 txtDataInicio.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                        txtDataFin.ClientID, "true", 2)

        Else
            script &= String.Format("AbrirCalendario('{0}','{1}',{2});", _
                                  txtDataInicio.ClientID, "false", 0)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                        txtDataFin.ClientID, "false", 0)

        End If

        If DataComHoras Then
            txtDataInicio.Width = 130
            txtDataFin.Width = 130
            txtDataInicio.MaxLength = 19
        Else
            txtDataInicio.Width = 77
            txtDataFin.Width = 77
            txtDataFin.MaxLength = 10
        End If

        If Not DataFinalVisivel Then
            txtDataFin.Visible = False
            lblDataAte.Visible = False
        End If

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), Guid.NewGuid().ToString(), script, True)

    End Sub
    Public Function Validar() As Boolean
        Dim retorno As Boolean = True
        Dim erro As String = Nothing

        'Verifica datas obrigatórias
        If DatasObrigatorias Then
            If String.IsNullOrEmpty(txtDataInicio.Text) Then
                erro = vbCrLf & String.Format(Traduzir("err_campo_obrigatorio"), _
                                      lblData.Text & " " & Traduzir("lbl_desde"))

                retorno = False
            Else
                'Verifica se é uma data válida
                If (Not txtDataInicio.Text.Length >= MaxLength OrElse Not IsDate(txtDataInicio.Text)) Then
                    erro = erro & vbCrLf & String.Format(Traduzir("lbl_campo_datahora_invalida"), _
                                           lblData.Text & " " & Traduzir("lbl_desde"))

                    retorno = False
                End If
            End If

            If DataFinalVisivel Then
                If String.IsNullOrEmpty(txtDataFin.Text) Then
                    erro = erro & vbCrLf & String.Format(Traduzir("err_campo_obrigatorio"), _
                                           lblData.Text & " " & Traduzir("lbl_hasta"))
      
                    retorno = False
                Else
                    'Verifica se é uma data válida
                    If (Not txtDataFin.Text.Length >= MaxLength OrElse Not IsDate(txtDataFin.Text)) Then
                        erro = erro & vbCrLf & String.Format(Traduzir("lbl_campo_datahora_invalida"), _
                                               lblData.Text & " " & Traduzir("lbl_hasta"))
      
                        retorno = False
                    End If
                End If
            End If
        Else
            'Verifica se as data foram preenchidas, se foram verificar se são validas
            ' se a data foi preenchida então verifica se é uma data válida
            If (Not String.IsNullOrEmpty(txtDataInicio.Text) AndAlso (Not txtDataInicio.Text.Length >= MaxLength OrElse Not IsDate(txtDataInicio.Text))) Then
                erro = erro & vbCrLf & String.Format(Traduzir("lbl_campo_datahora_invalida"), _
                                       lblData.Text & " " & Traduzir("lbl_desde"))
                retorno = False
            End If

            If DataFinalVisivel Then
                If (Not String.IsNullOrEmpty(txtDataFin.Text) AndAlso (Not txtDataFin.Text.Length >= MaxLength OrElse Not IsDate(txtDataFin.Text))) Then
                    erro = erro & vbCrLf & String.Format(Traduzir("lbl_campo_datahora_invalida"), _
                                           lblData.Text & " " & Traduzir("lbl_hasta"))
                    retorno = False
                End If
            End If
        End If

        If (erro IsNot Nothing) Then
            retorno = False
            Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, erro.Substring(vbCrLf.Length))
        End If
        '-----------------------------------

        'Verifica se a data final é maior do que a data inicial
        If DataFinalVisivel Then
            If (IsDate(txtDataInicio.Text) AndAlso IsDate(txtDataFin.Text)) AndAlso _
                        Date.Compare(Convert.ToDateTime(txtDataInicio.Text), Convert.ToDateTime(txtDataFin.Text)) > 0 Then
                erro = String.Format(Traduzir("lbl_campo_periodo_invalido"), _
                                       lblData.Text & " " & _
                                       Traduzir("lbl_hasta"), lblData.Text & " " & _
                                       Traduzir("lbl_desde"))
                retorno = False
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, erro)
            End If
        End If

        Return retorno
    End Function

    Private Sub TraduzirControles()

        Me.lblDataDe.Text = Traduzir("lbl_desde")
        Me.lblDataAte.Text = Traduzir("lbl_hasta")

    End Sub

#End Region

    Private Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
 
      

    End Sub
End Class