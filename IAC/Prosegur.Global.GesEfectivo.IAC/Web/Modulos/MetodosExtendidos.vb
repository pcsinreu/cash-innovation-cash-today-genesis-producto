Imports System.Runtime.CompilerServices

Module MetodosExtendidos

#Region "[String]"

    ''' <summary>
    ''' Formata mensagem através dos parâmetros
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="param"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 26/01/2009 Criado
    ''' </history>
    <Extension()> _
    Public Function SetFormat(str As String, param As List(Of String)) As String

        ' inicializar retorno
        Dim retorno As String = String.Empty

        ' se o valor traduzido pelo dicionário não for nothing ou vazio
        If str IsNot Nothing _
            AndAlso str <> String.Empty Then

            ' inicializar controle de chave
            Dim chave As Integer = 0

            ' retorno recebe valor do parametro
            retorno = str

            ' percorrer array
            For Each valor As String In param

                ' se o parametro não for nothing ou vazio
                If valor IsNot Nothing _
                    AndAlso valor <> String.Empty Then

                    ' substituir chave pelo valor do array
                    retorno = retorno.Replace("{" & chave & "}", valor)

                    ' incrementar chave
                    chave += 1

                End If

            Next

        End If

        ' retornar mensagem formatada
        Return retorno

    End Function

    ''' <summary>
    ''' Formata mensagem através do valor 1
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="valor1"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <Extension()> _
    Public Function SetFormat(str As String, valor1 As String) As String

        ' criar listof
        Dim chavesDic As New List(Of String)
        chavesDic.Add(valor1)

        ' retornar mensagem preparada
        Return SetFormat(str, chavesDic)

    End Function

    ''' <summary>
    ''' Formata mensagem através do valor 1
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="valor1"></param>
    ''' <param name="valor2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <Extension()> _
    Public Function SetFormat(str As String, _
                              valor1 As String, _
                              valor2 As String) As String

        ' criar listof
        Dim chavesDic As New List(Of String)
        chavesDic.Add(valor1)
        chavesDic.Add(valor2)

        ' retornar mensagem preparada
        Return SetFormat(str, chavesDic)

    End Function

#End Region

#Region "[DropDownList]"

    ''' <summary>
    ''' Ordena os itens do dropdownlist
    ''' </summary>
    ''' <param name="Controle"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/01/2011 criado
    ''' </history>
    <Extension()> _
    Public Sub OrdenarPorValor(Controle As DropDownList, Optional OrdemDescendente As Boolean = False)

        Dim itemsOrdenados As ListItem()

        If OrdemDescendente Then
            itemsOrdenados = (From i As ListItem In Controle.Items Order By i.Value Descending).ToArray()
        Else
            itemsOrdenados = (From i As ListItem In Controle.Items Order By i.Value Ascending).ToArray()
        End If

        Controle.Items.Clear()

        Controle.Items.AddRange(itemsOrdenados)

    End Sub

    ''' <summary>
    ''' Ordena os itens do dropdownlist
    ''' </summary>
    ''' <param name="Controle"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/01/2011 criado
    ''' </history>
    <Extension()> _
    Public Sub OrdenarPorDesc(Controle As DropDownList, Optional OrdemDescendente As Boolean = False)

        Dim itemsOrdenados As ListItem()

        If OrdemDescendente Then
            itemsOrdenados = (From i As ListItem In Controle.Items Order By i.Text Descending).ToArray()
        Else
            itemsOrdenados = (From i As ListItem In Controle.Items Order By i.Text Ascending).ToArray()
        End If

        Controle.Items.Clear()

        Controle.Items.AddRange(itemsOrdenados)

    End Sub

#End Region

End Module