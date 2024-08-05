Imports System.IO
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Reflection
Imports System.Text

Public Class Util


    Private Shared _VersionCompleta As String
    Public Shared ReadOnly Property VersionCompleta
        Get
            If String.IsNullOrEmpty(_VersionCompleta) Then
                Dim product As FileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
                _VersionCompleta = product.ProductVersion
            End If

            Return _VersionCompleta
        End Get
    End Property

    Private Shared _Version As String
    Public Shared ReadOnly Property Version
        Get
            If String.IsNullOrEmpty(_Version) Then
                Dim product As FileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
                _Version = FormataVersion(product)
            End If

#If DEBUG Then
            Dim objParametrosDebug As ParametrosDebug = Prosegur.Genesis.Comon.Util.RecuperarParametrosDebug
            If objParametrosDebug IsNot Nothing AndAlso Not String.IsNullOrEmpty(objParametrosDebug.VersaoProcedure) Then
                _Version = objParametrosDebug.VersaoProcedure
            End If
#End If

            Return _Version
        End Get
    End Property

    Private Shared _VersionPunto As String
    Public Shared ReadOnly Property VersionPunto
        Get
            If String.IsNullOrEmpty(_VersionPunto) Then
                Dim product As FileVersionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
                _VersionPunto = FormataVersionPunto(product)
            End If
            Return _VersionPunto
        End Get
    End Property

    Private Shared _VersionPantallas As String
    Public Shared Function VersionPantallas(assembly As Assembly) As String
        If String.IsNullOrEmpty(_VersionPantallas) Then
            Dim product As FileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location)
            _VersionPantallas = FormataVersionMajorMinorBuild(product)
        End If

        Return _VersionPantallas
    End Function

    Private Shared Function FormataVersion(product As FileVersionInfo) As String
        Return product.FileMajorPart.ToString("00") & product.FileMinorPart.ToString("00")
    End Function

    Private Shared Function FormataVersionPunto(product As FileVersionInfo) As String
        Return product.FileMajorPart & "." & product.FileMinorPart
    End Function

    Private Shared Function FormataVersionMajorMinorBuild(product As FileVersionInfo) As String
        Return product.FileMajorPart & "." & product.FileMinorPart ' & "." & product.FileBuildPart
    End Function

    Public Shared Function RetornaInformacionGenesis(listaDLL As List(Of Clases.Assembly), aplicacion As String, usuario As String, codDelegacion As String, Optional codSector As String = Nothing, Optional codPuesto As String = Nothing, Optional host As String = Nothing, Optional url As String = Nothing) As String
        Dim strResultadoGrid As New StringBuilder()

        strResultadoGrid.AppendLine("Aplicacion:" & vbTab & aplicacion)

        If Not String.IsNullOrEmpty(usuario) Then
            strResultadoGrid.AppendLine("Usuario:" & vbTab & usuario)
        End If
        If Not String.IsNullOrEmpty(codDelegacion) Then
            strResultadoGrid.AppendLine("Delegacion:" & vbTab & codDelegacion)
        End If
        If Not String.IsNullOrEmpty(codSector) Then
            strResultadoGrid.AppendLine("Sector:" & vbTab & codSector)
        End If
        If Not String.IsNullOrEmpty(codPuesto) Then
            strResultadoGrid.AppendLine("Puesto:" & vbTab & codPuesto)
        End If
        If Not String.IsNullOrEmpty(host) Then
            strResultadoGrid.AppendLine("Host:" & vbTab & host)
        End If
        If Not String.IsNullOrEmpty(url) Then
            strResultadoGrid.AppendLine("Url:" & vbTab & url)
        End If

        strResultadoGrid.AppendLine("")

        If listaDLL IsNot Nothing AndAlso listaDLL.Count > 0 Then
            Dim colunas As New List(Of String) From {"FileName", "FileVersion", "ProductVersion", "FechaHora"}
            strResultadoGrid.AppendLine(String.Join(vbTab, colunas))

            For Each dll In listaDLL
                strResultadoGrid.AppendLine(dll.Name.Split("\")(dll.Name.Split("\").Count - 1) & vbTab & dll.Version & vbTab & dll.Build & vbTab & dll.FechaHora)
            Next
        End If
        Return strResultadoGrid.ToString()
    End Function

    Public Shared Function RetornaDLLs(caminhoPasta As String) As List(Of FileVersionInfo)
        Dim retorno As New List(Of FileVersionInfo)
        For Each file In Directory.GetFiles(caminhoPasta, "Prosegur.Genesis.*.dll", SearchOption.TopDirectoryOnly)
            retorno.Add(FileVersionInfo.GetVersionInfo(file))
        Next

        For Each file In Directory.GetFiles(caminhoPasta, "Prosegur.Global.*.dll", SearchOption.TopDirectoryOnly)
            retorno.Add(FileVersionInfo.GetVersionInfo(file))
        Next
        Return retorno.OrderBy(Function(a) a.FileName).ToList()
    End Function

    Public Shared Function RetornaDLLsAssembly(pPath As String) As List(Of Clases.Assembly)
        Dim retorno As New List(Of Clases.Assembly)
        Dim unAssemblyInfo As Clases.Assembly
        Dim unArchivoFisico As IO.FileInfo

        For Each fileVersion In RetornaDLLs(pPath)
            unAssemblyInfo = New Clases.Assembly()
            unArchivoFisico = New IO.FileInfo(fileVersion.FileName)

            unAssemblyInfo.Name = fileVersion.FileName
            unAssemblyInfo.Build = fileVersion.ProductVersion
            unAssemblyInfo.Version = fileVersion.FileVersion
            unAssemblyInfo.FechaHora = unArchivoFisico.LastWriteTime


            retorno.Add(unAssemblyInfo)
        Next

        Return retorno
    End Function

    Public Shared Function RecuperarParametrosDebug() As Comon.ParametrosDebug

        Dim objParametrosDebug As Comon.ParametrosDebug = Nothing

        Try

            If System.IO.File.Exists("C:\tmp\ParametrosDebug.xml") Then

                Dim obj As New System.Xml.Serialization.XmlSerializer(GetType(Comon.ParametrosDebug))
                Dim w As New System.IO.StreamReader("C:\tmp\ParametrosDebug.xml")
                objParametrosDebug = CType(obj.Deserialize(w), Comon.ParametrosDebug)

                w.Close()

            End If

        Catch ex As Exception

        End Try

        Return objParametrosDebug
    End Function
    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.seabra] 04/01/2013 Criado
    ''' </history>
    Public Shared Function AtribuirValorObj(Valor As Object, _
                                               TipoCampo As System.Type) As Object

        Dim Campo As New Object

        If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
            If TipoCampo Is Nothing Then
                Campo = Valor
            ElseIf TipoCampo Is GetType(String) Then
                Campo = Convert.ToString(Valor)
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = Convert.ToInt16(Valor)
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = Convert.ToInt32(Valor)
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(DateTime) Then
                Campo = Convert.ToDateTime(Valor)
            End If
        Else

            If TipoCampo Is GetType(Decimal) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = 0
            Else
                Campo = Nothing
            End If

        End If

        Return Campo
    End Function

    Public Shared Function PrepararQuery(sql As String, IdConexao As String) As String

        Return sql.Replace("[]", RetornarPrefixoParametro(IdConexao))

    End Function

    Public Shared Function RetornarPrefixoParametro(IdConexao As String) As String

        ' verificar o banco que está sendo usado
        Select Case AcessoDados.RecuperarProvider(IdConexao)

            Case Provider.MsOracle

                Return ":"

            Case Provider.SqlServer

                Return "@"

        End Select

        Return String.Empty

    End Function

    Public Shared Function AccionTransaccion(TipoMovimiento As String, estado As String, TipoSitio As Enumeradores.TipoSitio, TipoSaldo As Enumeradores.TipoSaldo) As Clases.AccionTransaccion
        Dim objAccionTransaccion As New Clases.AccionTransaccion
        Try

            objAccionTransaccion.Estado = RecuperarEnum(Of Enumeradores.EstadoDocumento)(estado)

            objAccionTransaccion.TipoSitio = TipoSitio
            objAccionTransaccion.TipoSaldo = TipoSaldo

            If TipoMovimiento = Constantes.TipoMovimientoIngreso Then
                objAccionTransaccion.TipoMovimiento = Enumeradores.TipoMovimiento.Ingreso
            ElseIf TipoMovimiento = Constantes.TipoMovimientoEgreso Then
                objAccionTransaccion.TipoMovimiento = Enumeradores.TipoMovimiento.Egreso
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return objAccionTransaccion

    End Function

    ''' <summary>
    ''' Função que retorna a parte inteira de um número decimal.
    ''' Arrendonda para baixo o valor.
    ''' </summary>
    ''' <param name="dNumero"></param>
    ''' <returns></returns>
    ''' <history>[jorge.viana]	07/07/2010	Creado</history>
    ''' <remarks></remarks>
    Public Shared Function RetornaParteInteiraDoNumero(dNumero As Decimal) As Integer
        'Toda variável do tipo decimal quando convertida para string, o ponto (.) vira uma vírgula (,)
        'então, transforma-se a string da variável em um array separando pela vírgula e pega o primeiro
        'item do array que será a parte inteira do valor decimal.
        Return dNumero.ToString.Split(",").GetValue(0)
    End Function

    ''' <summary>
    ''' Remove da lista de divisas todos os itens (Divisas, Denominações, Valores de Denominações, Meios de Pagamentos, 
    ''' Valores de Meios de Pagamentos, Valores Totais Gerais, Valores Totais Efetivos e Valores Totais de Meios de Pagamento)
    ''' que sejam "Nothing" ou que seu valor e quantidade (combinação de ambos) seja igual a zero.
    ''' </summary>
    ''' <param name="divisas">Lista de Divisas que receberá o tratamento de "exclusão" de itens vazios ou sem valor e quantidade.</param>
    ''' <remarks></remarks>
    Public Shared Sub BorrarItemsDivisasSinValoresCantidades(ByRef divisas As ObservableCollection(Of Clases.Divisa), Optional borrarTotales As Boolean = True)

        ' para não ter que gerar cópia de objetos em memória e garantir a integridade deles
        ' a remoção dos valores que se enquadram nas validações é feita na própria coleção (por referência)
        ' por este motivo, para que não haja um erro de alteração na coleção ao se remover um item
        ' as coleções abaixo são lidas utilizando um "for" inverso, ou seja, lendo do maior índice
        ' para o menor, neste caso, mesmo removendo itens da coleção, o índice sempre esta de acordo
        ' com a quantidade final

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            Dim totalDivisas As Integer = divisas.Count() - 1
            For div As Integer = totalDivisas To 0 Step -1

                If divisas(div) Is Nothing Then
                    ' remove a divisa caso seja vazia
                    divisas.RemoveAt(div)
                Else

                    ' denominações
                    If divisas(div).Denominaciones IsNot Nothing AndAlso divisas(div).Denominaciones.Count() > 0 Then
                        Dim totalDenominaciones As Integer = divisas(div).Denominaciones.Count() - 1
                        For den As Integer = totalDenominaciones To 0 Step -1
                            If divisas(div).Denominaciones(den) Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 OrElse Not divisas(div).Denominaciones(den).ValorDenominacion.Exists(Function(v) Not v.Importe = 0 OrElse Not v.Cantidad = 0) Then
                                ' remove a denominação caso seja vazia ou não tenha valores
                                divisas(div).Denominaciones.RemoveAt(den)
                            Else
                                If divisas(div).Denominaciones(den).ValorDenominacion IsNot Nothing AndAlso divisas(div).Denominaciones(den).ValorDenominacion.Count() > 0 Then
                                    Dim totalValoresDenominaciones As Integer = divisas(div).Denominaciones(den).ValorDenominacion.Count() - 1
                                    For valden As Integer = totalValoresDenominaciones To 0 Step -1
                                        If divisas(div).Denominaciones(den).ValorDenominacion(valden) Is Nothing OrElse (divisas(div).Denominaciones(den).ValorDenominacion(valden).Importe = 0 AndAlso divisas(div).Denominaciones(den).ValorDenominacion(valden).Cantidad = 0) Then
                                            ' remove o valor relacionado a denominação
                                            divisas(div).Denominaciones(den).ValorDenominacion.RemoveAt(valden)
                                        End If
                                    Next
                                    If divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 Then
                                        ' remove a denominação (caso não tenha mais valores)
                                        divisas(div).Denominaciones.RemoveAt(den)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    ' medio de pagos
                    If divisas(div).MediosPago IsNot Nothing AndAlso divisas(div).MediosPago.Count() > 0 Then
                        Dim totalMediosPago As Integer = divisas(div).MediosPago.Count() - 1
                        For med As Integer = totalMediosPago To 0 Step -1
                            If divisas(div).MediosPago(med) Is Nothing OrElse divisas(div).MediosPago(med).Valores Is Nothing OrElse divisas(div).MediosPago(med).Valores.Count() = 0 OrElse Not divisas(div).MediosPago(med).Valores.Exists(Function(v) Not v.Importe = 0 OrElse Not v.Cantidad = 0) Then
                                ' remove o meio de pagamento caso seja vazio ou não tenha valores
                                divisas(div).MediosPago.RemoveAt(med)
                            Else
                                If divisas(div).MediosPago(med).Valores IsNot Nothing AndAlso divisas(div).MediosPago(med).Valores.Count() > 0 Then
                                    Dim totalValoresMediosPago As Integer = divisas(div).MediosPago(med).Valores.Count() - 1
                                    For valmed As Integer = totalValoresMediosPago To 0 Step -1
                                        If divisas(div).MediosPago(med).Valores(valmed) Is Nothing OrElse (divisas(div).MediosPago(med).Valores(valmed).Importe = 0 AndAlso divisas(div).MediosPago(med).Valores(valmed).Cantidad = 0) Then
                                            ' remove o valor relacionado ao meio de pagamento
                                            divisas(div).MediosPago(med).Valores.RemoveAt(valmed)
                                        End If
                                    Next
                                    If divisas(div).MediosPago(med).Valores.Count() = 0 Then
                                        ' remove o meio de pagamento (caso não tenha mais valores)
                                        divisas(div).MediosPago.RemoveAt(med)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    If borrarTotales Then

                        ' Valores Totales
                        If divisas(div).ValoresTotales IsNot Nothing AndAlso divisas(div).ValoresTotales.Count() > 0 AndAlso divisas(div).ValoresTotales.Exists(Function(v) v.Importe = 0) Then
                            Dim totalItems As Integer = divisas(div).ValoresTotales.Count() - 1
                            For ger As Integer = totalItems To 0 Step -1
                                If divisas(div).ValoresTotales(ger).Importe = 0 Then
                                    ' remove o valor geral
                                    divisas(div).ValoresTotales.RemoveAt(ger)
                                End If
                            Next
                        End If

                        ' geral
                        If divisas(div).ValoresTotalesDivisa IsNot Nothing AndAlso divisas(div).ValoresTotalesDivisa.Count() > 0 AndAlso divisas(div).ValoresTotalesDivisa.Exists(Function(v) v.Importe = 0) Then
                            Dim totalItems As Integer = divisas(div).ValoresTotalesDivisa.Count() - 1
                            For ger As Integer = totalItems To 0 Step -1
                                If divisas(div).ValoresTotalesDivisa(ger).Importe = 0 Then
                                    ' remove o valor geral
                                    divisas(div).ValoresTotalesDivisa.RemoveAt(ger)
                                End If
                            Next
                        End If

                        ' total efectivo
                        If divisas(div).ValoresTotalesEfectivo IsNot Nothing AndAlso divisas(div).ValoresTotalesEfectivo.Count() > 0 AndAlso divisas(div).ValoresTotalesEfectivo.Exists(Function(v) v.Importe = 0) Then
                            Dim totalItems As Integer = divisas(div).ValoresTotalesEfectivo.Count() - 1
                            For totefe As Integer = totalItems To 0 Step -1
                                If divisas(div).ValoresTotalesEfectivo(totefe).Importe = 0 Then
                                    ' remove o valor total efectivo
                                    divisas(div).ValoresTotalesEfectivo.RemoveAt(totefe)
                                End If
                            Next
                        End If

                        ' total tipo medio de pago
                        If divisas(div).ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divisas(div).ValoresTotalesTipoMedioPago.Count() > 0 AndAlso divisas(div).ValoresTotalesTipoMedioPago.Exists(Function(v) v.Importe = 0 AndAlso v.Cantidad = 0) Then
                            Dim totalItems As Integer = divisas(div).ValoresTotalesTipoMedioPago.Count() - 1
                            For totmed As Integer = totalItems To 0 Step -1
                                If divisas(div).ValoresTotalesTipoMedioPago(totmed).Importe = 0 AndAlso divisas(div).ValoresTotalesTipoMedioPago(totmed).Cantidad = 0 Then
                                    ' remove o valor total medio de pago
                                    divisas(div).ValoresTotalesTipoMedioPago.RemoveAt(totmed)
                                End If
                            Next
                        End If

                    End If

                    ' se após todas as exclusões individuais não restar mais nenhuma propriedade com valores
                    ' deverá excluir a divisa
                    If (divisas(div).Denominaciones Is Nothing OrElse divisas(div).Denominaciones.Count() = 0) AndAlso _
                        (divisas(div).MediosPago Is Nothing OrElse divisas(div).MediosPago.Count() = 0) AndAlso _
                        (divisas(div).ValoresTotalesDivisa Is Nothing OrElse divisas(div).ValoresTotalesDivisa.Count() = 0) AndAlso _
                        (divisas(div).ValoresTotalesEfectivo Is Nothing OrElse divisas(div).ValoresTotalesEfectivo.Count() = 0) AndAlso _
                        (divisas(div).ValoresTotalesTipoMedioPago Is Nothing OrElse divisas(div).ValoresTotalesTipoMedioPago.Count() = 0) Then
                        divisas.RemoveAt(div)
                    End If

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Remove da lista de divisas do documento todos os itens (Divisas, Denominações, Valores de Denominações, Meios de Pagamentos, 
    ''' Valores de Meios de Pagamentos, Valores Totais Gerais, Valores Totais Efetivos e Valores Totais de Meios de Pagamento)
    ''' que sejam "Nothing" ou que seu valor e quantidade (combinação de ambos) seja igual a zero. Replica a remoção para o elemento
    ''' relacionado ao documento (e sua estrutura - remesa, bulto, parcial) caso exista um e caso o parâmetro "borrarItemsDivisaElementoRelacionado"
    ''' não seja modificado.
    ''' </summary>
    ''' <param name="documento">Documento que deverá ter suas divisas (e elementos) recebendo o tratamento de "exclusão" de itens vazios ou sem valor e quantidade.</param>
    ''' <param name="borrarItemsDivisaElementoRelacionado">Informa se o tratamento deverá ou não ser replicado para o elemento relacionado ao documento.</param>
    ''' <remarks></remarks>
    Public Shared Sub BorrarItemsDivisaSinValoresCantidades(ByRef documento As Clases.Documento, Optional borrarItemsDivisaElementoRelacionado As Boolean = True)

        ' remove valores de divisas nulos ou iguais a zero
        Comon.Util.BorrarItemsDivisasSinValoresCantidades(documento.Divisas)

        ' verifica se deverá remover os dados do elemento relacionado ao documento caso exista um
        If borrarItemsDivisaElementoRelacionado AndAlso documento.Elemento IsNot Nothing Then
            Select Case True
                Case TypeOf documento.Elemento Is Clases.Remesa
                    ' remove no nível da remesa
                    Comon.Util.BorrarItemsDivisasSinValoresCantidades(documento.Elemento.Divisas)
                    If DirectCast(documento.Elemento, Clases.Remesa).Bultos IsNot Nothing AndAlso DirectCast(documento.Elemento, Clases.Remesa).Bultos.Count() > 0 Then
                        For Each bulto In DirectCast(documento.Elemento, Clases.Remesa).Bultos
                            ' remove no nível da bulto
                            Comon.Util.BorrarItemsDivisasSinValoresCantidades(bulto.Divisas)
                            If bulto.Parciales IsNot Nothing AndAlso bulto.Parciales.Count() > 0 Then
                                For Each parcial In bulto.Parciales
                                    ' remove no nível da parcial
                                    Comon.Util.BorrarItemsDivisasSinValoresCantidades(parcial.Divisas)
                                Next
                            End If
                        Next
                    End If
                Case Else
                    ' remove no elemento que não pode ser identificado como remesa ou bulto
                    Comon.Util.BorrarItemsDivisasSinValoresCantidades(documento.Elemento.Divisas)
            End Select
        End If

    End Sub

    ''' <summary>
    ''' Remove da lista de divisas todos os itens (Divisas, Denominações, Valores de Denominações, Meios de Pagamentos, 
    ''' Valores de Meios de Pagamentos, Valores Totais Gerais, Valores Totais Efetivos e Valores Totais de Meios de Pagamento)
    ''' que não sejam do mesmo tipo de valor informado.
    ''' </summary>
    ''' <param name="divisas">Lista de Divisas que receberá o tratamento de "exclusão" de itens do tipo de valor diferente do solicitado.</param>
    ''' <param name="tipoValor">Tipo de Valor que será utilizado como referência para manter os dados.</param>
    ''' <remarks></remarks>
    Public Shared Sub BorrarItemsDivisasDiferentesTipoValor(ByRef divisas As ObservableCollection(Of Clases.Divisa), tipoValor As Enumeradores.TipoValor)

        ' para não ter que gerar cópia de objetos em memória e garantir a integridade deles
        ' a remoção dos valores que se enquadram nas validações é feita na própria coleção (por referência)
        ' por este motivo, para que não haja um erro de alteração na coleção ao se remover um item
        ' as coleções abaixo são lidas utilizando um "for" inverso, ou seja, lendo do maior índice
        ' para o menor, neste caso, mesmo removendo itens da coleção, o índice sempre esta de acordo
        ' com a quantidade final

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            Dim totalDivisas As Integer = divisas.Count() - 1
            For div As Integer = totalDivisas To 0 Step -1

                If divisas(div) Is Nothing Then
                    ' remove a divisa caso seja vazia
                    divisas.RemoveAt(div)
                Else

                    ' denominações
                    If divisas(div).Denominaciones IsNot Nothing AndAlso divisas(div).Denominaciones.Count() > 0 Then
                        Dim totalDenominaciones As Integer = divisas(div).Denominaciones.Count() - 1
                        For den As Integer = totalDenominaciones To 0 Step -1
                            If divisas(div).Denominaciones(den) Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 OrElse Not divisas(div).Denominaciones(den).ValorDenominacion.Exists(Function(v) v.TipoValor.Equals(tipoValor)) Then
                                ' remove a denominação caso seja vazia ou não tenha valores
                                divisas(div).Denominaciones.RemoveAt(den)
                            Else
                                If divisas(div).Denominaciones(den).ValorDenominacion IsNot Nothing AndAlso divisas(div).Denominaciones(den).ValorDenominacion.Count() > 0 Then
                                    Dim totalValoresDenominaciones As Integer = divisas(div).Denominaciones(den).ValorDenominacion.Count() - 1
                                    For valden As Integer = totalValoresDenominaciones To 0 Step -1
                                        If divisas(div).Denominaciones(den).ValorDenominacion(valden) Is Nothing OrElse Not divisas(div).Denominaciones(den).ValorDenominacion(valden).TipoValor.Equals(tipoValor) Then
                                            ' remove o valor relacionado a denominação
                                            divisas(div).Denominaciones(den).ValorDenominacion.RemoveAt(valden)
                                        End If
                                    Next
                                    If divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 Then
                                        ' remove a denominação (caso não tenha mais valores)
                                        divisas(div).Denominaciones.RemoveAt(den)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    ' medio de pagos
                    If divisas(div).MediosPago IsNot Nothing AndAlso divisas(div).MediosPago.Count() > 0 Then
                        Dim totalMediosPago As Integer = divisas(div).MediosPago.Count() - 1
                        For med As Integer = totalMediosPago To 0 Step -1
                            If divisas(div).MediosPago(med) Is Nothing OrElse divisas(div).MediosPago(med).Valores Is Nothing OrElse divisas(div).MediosPago(med).Valores.Count() = 0 OrElse Not divisas(div).MediosPago(med).Valores.Exists(Function(v) v.TipoValor.Equals(tipoValor)) Then
                                ' remove o meio de pagamento caso seja vazio ou não tenha valores
                                divisas(div).MediosPago.RemoveAt(med)
                            Else
                                If divisas(div).MediosPago(med).Valores IsNot Nothing AndAlso divisas(div).MediosPago(med).Valores.Count() > 0 Then
                                    Dim totalValoresMediosPago As Integer = divisas(div).MediosPago(med).Valores.Count() - 1
                                    For valmed As Integer = totalValoresMediosPago To 0 Step -1
                                        If divisas(div).MediosPago(med).Valores(valmed) Is Nothing OrElse Not divisas(div).MediosPago(med).Valores(valmed).TipoValor.Equals(tipoValor) Then
                                            ' remove o valor relacionado ao meio de pagamento
                                            divisas(div).MediosPago(med).Valores.RemoveAt(valmed)
                                        End If
                                    Next
                                    If divisas(div).MediosPago(med).Valores.Count() = 0 Then
                                        ' remove o meio de pagamento (caso não tenha mais valores)
                                        divisas(div).MediosPago.RemoveAt(med)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    ' geral
                    If divisas(div).ValoresTotalesDivisa IsNot Nothing AndAlso divisas(div).ValoresTotalesDivisa.Count() > 0 AndAlso divisas(div).ValoresTotalesDivisa.Exists(Function(v) Not v.TipoValor.Equals(tipoValor)) Then
                        Dim totalItems As Integer = divisas(div).ValoresTotalesDivisa.Count() - 1
                        For ger As Integer = totalItems To 0 Step -1
                            If Not divisas(div).ValoresTotalesDivisa(ger).TipoValor.Equals(tipoValor) Then
                                ' remove o valor geral
                                divisas(div).ValoresTotalesDivisa.RemoveAt(ger)
                            End If
                        Next
                    End If

                    ' total efectivo
                    If divisas(div).ValoresTotalesEfectivo IsNot Nothing AndAlso divisas(div).ValoresTotalesEfectivo.Count() > 0 AndAlso divisas(div).ValoresTotalesEfectivo.Exists(Function(v) Not v.TipoValor.Equals(tipoValor)) Then
                        Dim totalItems As Integer = divisas(div).ValoresTotalesEfectivo.Count() - 1
                        For totefe As Integer = totalItems To 0 Step -1
                            If Not divisas(div).ValoresTotalesEfectivo(totefe).TipoValor.Equals(tipoValor) Then
                                ' remove o valor total efectivo
                                divisas(div).ValoresTotalesEfectivo.RemoveAt(totefe)
                            End If
                        Next
                    End If

                    ' total tipo medio de pago
                    If divisas(div).ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divisas(div).ValoresTotalesTipoMedioPago.Count() > 0 AndAlso divisas(div).ValoresTotalesTipoMedioPago.Exists(Function(v) Not v.TipoValor.Equals(tipoValor)) Then
                        Dim totalItems As Integer = divisas(div).ValoresTotalesTipoMedioPago.Count() - 1
                        For totmed As Integer = totalItems To 0 Step -1
                            If Not divisas(div).ValoresTotalesTipoMedioPago(totmed).TipoValor.Equals(tipoValor) Then
                                ' remove o valor total medio de pago
                                divisas(div).ValoresTotalesTipoMedioPago.RemoveAt(totmed)
                            End If
                        Next
                    End If

                    ' se após todas as exclusões individuais não restar mais nenhuma propriedade com valores
                    ' deverá excluir a divisa
                    If (divisas(div).Denominaciones Is Nothing OrElse divisas(div).Denominaciones.Count() = 0) AndAlso _
                        (divisas(div).MediosPago Is Nothing OrElse divisas(div).MediosPago.Count() = 0) AndAlso _
                        (divisas(div).ValoresTotalesDivisa Is Nothing OrElse divisas(div).ValoresTotalesDivisa.Count() = 0) AndAlso _
                        (divisas(div).ValoresTotalesEfectivo Is Nothing OrElse divisas(div).ValoresTotalesEfectivo.Count() = 0) AndAlso _
                        (divisas(div).ValoresTotalesTipoMedioPago Is Nothing OrElse divisas(div).ValoresTotalesTipoMedioPago.Count() = 0) Then
                        divisas.RemoveAt(div)
                    End If

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Remove da lista de divisas todos os itens (Denominações, Valores de Denominações
    ''' Valores Totais Gerais, Valores Totais Efetivos)
    ''' que sejam "Nothing" ou que seu valor e quantidade (combinação de ambos) seja igual a zero.
    ''' </summary>
    ''' <param name="divisas">Lista de Divisas que receberá o tratamento de "exclusão" de itens vazios ou sem valor e quantidade.</param>
    ''' <remarks></remarks>
    Public Shared Sub BorrarItemsDenominacionesMediosPagoSinValoresCantidades(ByRef divisas As ObservableCollection(Of Clases.Divisa))

        ' para não ter que gerar cópia de objetos em memória e garantir a integridade deles
        ' a remoção dos valores que se enquadram nas validações é feita na própria coleção (por referência)
        ' por este motivo, para que não haja um erro de alteração na coleção ao se remover um item
        ' as coleções abaixo são lidas utilizando um "for" inverso, ou seja, lendo do maior índice
        ' para o menor, neste caso, mesmo removendo itens da coleção, o índice sempre esta de acordo
        ' com a quantidade final

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            Dim totalDivisas As Integer = divisas.Count() - 1
            For div As Integer = totalDivisas To 0 Step -1

                If divisas(div) IsNot Nothing Then

                    ' denominações
                    If divisas(div).Denominaciones IsNot Nothing AndAlso divisas(div).Denominaciones.Count() > 0 Then
                        Dim totalDenominaciones As Integer = divisas(div).Denominaciones.Count() - 1
                        For den As Integer = totalDenominaciones To 0 Step -1
                            If divisas(div).Denominaciones(den) Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion Is Nothing OrElse divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 OrElse Not divisas(div).Denominaciones(den).ValorDenominacion.Exists(Function(v) Not v.Importe = 0 OrElse Not v.Cantidad = 0) Then
                                ' remove a denominação caso seja vazia ou não tenha valores
                                divisas(div).Denominaciones.RemoveAt(den)
                            Else
                                If divisas(div).Denominaciones(den).ValorDenominacion IsNot Nothing AndAlso divisas(div).Denominaciones(den).ValorDenominacion.Count() > 0 Then
                                    Dim totalValoresDenominaciones As Integer = divisas(div).Denominaciones(den).ValorDenominacion.Count() - 1
                                    For valden As Integer = totalValoresDenominaciones To 0 Step -1
                                        If divisas(div).Denominaciones(den).ValorDenominacion(valden) Is Nothing OrElse (divisas(div).Denominaciones(den).ValorDenominacion(valden).Importe = 0 AndAlso divisas(div).Denominaciones(den).ValorDenominacion(valden).Cantidad = 0) Then
                                            ' remove o valor relacionado a denominação
                                            divisas(div).Denominaciones(den).ValorDenominacion.RemoveAt(valden)
                                        End If
                                    Next
                                    If divisas(div).Denominaciones(den).ValorDenominacion.Count() = 0 Then
                                        ' remove a denominação (caso não tenha mais valores)
                                        divisas(div).Denominaciones.RemoveAt(den)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    ' geral
                    If divisas(div).ValoresTotalesDivisa IsNot Nothing AndAlso divisas(div).ValoresTotalesDivisa.Count() > 0 AndAlso divisas(div).ValoresTotalesDivisa.Exists(Function(v) v.Importe = 0) Then
                        Dim totalItems As Integer = divisas(div).ValoresTotalesDivisa.Count() - 1
                        For ger As Integer = totalItems To 0 Step -1
                            If divisas(div).ValoresTotalesDivisa(ger).Importe = 0 Then
                                ' remove o valor geral
                                divisas(div).ValoresTotalesDivisa.RemoveAt(ger)
                            End If
                        Next
                    End If

                    ' total efectivo
                    If divisas(div).ValoresTotalesEfectivo IsNot Nothing AndAlso divisas(div).ValoresTotalesEfectivo.Count() > 0 AndAlso divisas(div).ValoresTotalesEfectivo.Exists(Function(v) v.Importe = 0) Then
                        Dim totalItems As Integer = divisas(div).ValoresTotalesEfectivo.Count() - 1
                        For totefe As Integer = totalItems To 0 Step -1
                            If divisas(div).ValoresTotalesEfectivo(totefe).Importe = 0 Then
                                ' remove o valor total efectivo
                                divisas(div).ValoresTotalesEfectivo.RemoveAt(totefe)
                            End If
                        Next
                    End If

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Corrige os valores relacionados a importe e quantidade das denominações relacionadas na coleção. 
    ''' Caso uma denominação tenha valores com importe informado mas sem quantidade, a função irá corrigir a quantidade.
    ''' Caso uma denominação tenha valores com quantidade informada mas sem importe, a função irá corrigir o importe.
    ''' </summary>
    ''' <param name="divisas">Lista de Divisas que receberá o tratamento de "correção".</param>
    ''' <remarks></remarks>
    Public Shared Sub CorrigeImporteCantidadDenominaciones(ByRef divisas As ObservableCollection(Of Clases.Divisa))

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            For Each div In divisas

                If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count() > 0 Then

                    ' seleciona todas denominações que tenham valores zero mas com quantidades diferente de zero
                    For Each den In div.Denominaciones.Where(Function(d) d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Exists(Function(v) (Not v.Cantidad = 0 AndAlso v.Importe = 0) OrElse (v.Cantidad = 0 AndAlso Not v.Importe = 0)))

                        ' seleciona os valores que estão com quantidade mas sem importe
                        For Each valden In den.ValorDenominacion.Where(Function(v) Not v.Cantidad = 0 AndAlso v.Importe = 0)
                            valden.Importe = (valden.Cantidad * den.Valor)
                        Next

                        ' seleciona os valores que estão sem quantidade mas com importe
                        For Each valden In den.ValorDenominacion.Where(Function(v) v.Cantidad = 0 AndAlso Not v.Importe = 0)

                            ' valida se a divisão do valor de importe pelo valor da denominação resultará
                            ' em uma quantidade "inteira", caso não seja, tem que devolver um erro 
                            ' pela icompatibilidade de valor de importe com a denominação
                            Dim valorInteiro As Integer
                            If Integer.TryParse((valden.Importe / den.Valor), valorInteiro) Then
                                valden.Cantidad = valorInteiro
                            Else
                                Throw New Excepciones.ExcepcionLogica(String.Format("El valor de importe '{0}' es incompatible con la denominación '{1}' ({2}) de la divisa '{3}' ({4}). La división del importe por el valor no resulta en una cantidad entera.", valden.Importe, den.Descripcion, den.Valor, div.Descripcion, div.CodigoISO))
                            End If

                        Next

                    Next

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Corrige da lista de divisas do documento todas as denominações que tenham sido mal informadas (quantidade informada
    ''' mas importe não, ou, importe informado mas quantidade não), de forma que fique coerente os valores com as quantidades.
    ''' </summary>
    ''' <param name="documento">Documento que deverá ter suas divisas (e elementos) recebendo o tratamento de "correção".</param>
    ''' <param name="borrarItemsDivisaElementoRelacionado">Informa se o tratamento deverá ou não ser replicado para o elemento relacionado ao documento.</param>
    ''' <remarks></remarks>
    Public Shared Sub CorrigeImporteCantidadDenominaciones(ByRef documento As Clases.Documento, Optional borrarItemsDivisaElementoRelacionado As Boolean = True)

        ' corrige valores de divisas
        Comon.Util.CorrigeImporteCantidadDenominaciones(documento.Divisas)

        ' verifica se deverá remover os dados do elemento relacionado ao documento caso exista um
        If borrarItemsDivisaElementoRelacionado AndAlso documento.Elemento IsNot Nothing Then
            Select Case True
                Case TypeOf documento.Elemento Is Clases.Remesa
                    ' corrige no nível da remesa
                    Comon.Util.CorrigeImporteCantidadDenominaciones(documento.Elemento.Divisas)
                    If DirectCast(documento.Elemento, Clases.Remesa).Bultos IsNot Nothing AndAlso DirectCast(documento.Elemento, Clases.Remesa).Bultos.Count() > 0 Then
                        For Each bulto In DirectCast(documento.Elemento, Clases.Remesa).Bultos
                            ' corrige no nível da bulto
                            Comon.Util.CorrigeImporteCantidadDenominaciones(bulto.Divisas)
                            If bulto.Parciales IsNot Nothing AndAlso bulto.Parciales.Count() > 0 Then
                                For Each parcial In bulto.Parciales
                                    ' corrige no nível da parcial
                                    Comon.Util.CorrigeImporteCantidadDenominaciones(parcial.Divisas)
                                Next
                            End If
                        Next
                    End If
                Case Else
                    ' corrige no elemento que não pode ser identificado como remesa ou bulto
                    Comon.Util.CorrigeImporteCantidadDenominaciones(documento.Elemento.Divisas)
            End Select
        End If

    End Sub

    ''' <summary>
    ''' Unifica os items de divisas de forma a não repetir uma mesma combinação. Exemplos: Se a lista de divisas contém dois itens da mesma divisa,
    ''' a função unifica os valores de ambos para que ao final tenha somente um item de divisa. Se a lista de denominações de uma divisa tem dois (ou mais)
    ''' itens para mesma denominação, unifica-os de modo que fique somente um item (faz o mesmo com seus valores - considerando unidade de medida, 
    ''' informado por, qualidade e tipo de valor como chaves de comparação). Se a lista de meios de pagamentos de uma divisa tem dois (ou mais)
    ''' itens para o mesmo meio de pagamento, unifica-os de modo que fique somente um item (faz o mesmo com o seus valores - considerando unidade de
    ''' medida, informado por e tipo de valor como chaves de comparação). O mesmo se segue para as listas de valores totalizados.
    ''' </summary>
    ''' <param name="divisas">Lista de Divisas que receberá o tratamento de "unificação" de itens.</param>
    ''' <remarks></remarks>
    Public Shared Sub UnificaItemsDivisas(ByRef divisas As ObservableCollection(Of Clases.Divisa),
                                          Optional MesclarValorDenominacion As Boolean = False)

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            Dim divisasUnificadas As New ObservableCollection(Of Clases.Divisa)
            Dim divisa As Clases.Divisa
            Dim denominacion As Clases.Denominacion
            Dim valorDenominacion As Clases.ValorDenominacion
            Dim medioPago As Clases.MedioPago
            Dim valorMedioPago As Clases.ValorMedioPago
            Dim valorTotalDivisa As Clases.ValorDivisa
            Dim valorTotalEfectivo As Clases.ValorEfectivo
            Dim valorTotalTipoMedioPago As Clases.ValorTipoMedioPago
            Dim valorTotal As Clases.ImporteTotal

            For Each div In divisas.Where(Function(d) d IsNot Nothing AndAlso Not String.IsNullOrEmpty(d.Identificador))

                ' divisa
                If Not divisasUnificadas.Exists(Function(d) d.Identificador.Equals(div.Identificador)) Then
                    divisa = New Clases.Divisa With { _
                                          .CodigoAcceso = div.CodigoAcceso, _
                                          .CodigoISO = div.CodigoISO, _
                                          .CodigoSimbolo = div.CodigoSimbolo, _
                                          .CodigoUsuario = div.CodigoUsuario, _
                                          .Color = div.Color, _
                                          .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                          .Descripcion = div.Descripcion, _
                                          .EstaActivo = div.EstaActivo, _
                                          .FechaHoraTransporte = div.FechaHoraTransporte, _
                                          .Icono = div.Icono, _
                                          .Identificador = div.Identificador, _
                                          .MediosPago = New ObservableCollection(Of Clases.MedioPago), _
                                          .ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal), _
                                          .ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa), _
                                          .ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo), _
                                          .ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                                      }
                    divisasUnificadas.Add(divisa)
                Else
                    divisa = divisasUnificadas.Where(Function(d) d.Identificador.Equals(div.Identificador)).First()
                End If

                ' denominações
                If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count() > 0 Then

                    For Each den In div.Denominaciones.Where(Function(d) d IsNot Nothing AndAlso Not String.IsNullOrEmpty(d.Identificador))

                        If Not divisa.Denominaciones.Exists(Function(d) d.Identificador.Equals(den.Identificador)) Then
                            denominacion = New Clases.Denominacion With { _
                                .Codigo = den.Codigo, _
                                .CodigoUsuario = den.CodigoUsuario, _
                                .Descripcion = den.Descripcion, _
                                .EsBillete = den.EsBillete, _
                                .EstaActivo = den.EstaActivo, _
                                .FechaHoraActualizacion = den.FechaHoraActualizacion, _
                                .Identificador = den.Identificador, _
                                .Peso = den.Peso, _
                                .Valor = den.Valor, _
                                .ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion) _
                            }
                            divisa.Denominaciones.Add(denominacion)
                        Else
                            denominacion = divisa.Denominaciones.Where(Function(d) d.Identificador.Equals(den.Identificador)).First()
                        End If

                        ' valores denominação
                        If den.ValorDenominacion IsNot Nothing AndAlso den.ValorDenominacion.Count() > 0 Then

                            For Each valDen In den.ValorDenominacion.Where(Function(vd) vd IsNot Nothing AndAlso (Not vd.Importe = 0 OrElse Not vd.Cantidad = 0))

                                If MesclarValorDenominacion Then

                                    If denominacion.ValorDenominacion Is Nothing OrElse denominacion.ValorDenominacion.Count = 0 Then

                                        valorDenominacion = New Clases.ValorDenominacion With { _
                                            .Calidad = valDen.Calidad, _
                                            .Cantidad = valDen.Cantidad, _
                                            .Importe = valDen.Importe, _
                                            .InformadoPor = valDen.InformadoPor, _
                                            .TipoValor = valDen.TipoValor, _
                                            .UnidadMedida = valDen.UnidadMedida,
                                            .Detallar = valDen.Detallar,
                                            .Color = valDen.Color,
                                            .Diferencia = valDen.Diferencia
                                            }
                                        denominacion.ValorDenominacion.Add(valorDenominacion)

                                    Else

                                        valorDenominacion = denominacion.ValorDenominacion.FirstOrDefault
                                        valorDenominacion.Cantidad += valDen.Cantidad
                                        valorDenominacion.Importe += valDen.Importe

                                    End If

                                Else

                                    If Not denominacion.ValorDenominacion.Exists(Function(vd) vd.InformadoPor.Equals(valDen.InformadoPor) AndAlso vd.TipoValor.Equals(valDen.TipoValor) AndAlso vd.Calidad IsNot Nothing AndAlso valDen.Calidad IsNot Nothing AndAlso vd.Calidad.Identificador.Equals(valDen.Calidad.Identificador) AndAlso vd.UnidadMedida IsNot Nothing AndAlso valDen.UnidadMedida IsNot Nothing AndAlso vd.UnidadMedida.Identificador.Equals(valDen.UnidadMedida.Identificador)) Then

                                        valorDenominacion = New Clases.ValorDenominacion With { _
                                            .Calidad = valDen.Calidad, _
                                            .Cantidad = valDen.Cantidad, _
                                            .Importe = valDen.Importe, _
                                            .InformadoPor = valDen.InformadoPor, _
                                            .TipoValor = valDen.TipoValor, _
                                            .UnidadMedida = valDen.UnidadMedida,
                                            .Detallar = valDen.Detallar,
                                            .Color = valDen.Color,
                                            .Diferencia = valDen.Diferencia
                                            }
                                        denominacion.ValorDenominacion.Add(valorDenominacion)

                                    Else

                                        valorDenominacion = denominacion.ValorDenominacion.Where(Function(vd) vd.InformadoPor.Equals(valDen.InformadoPor) AndAlso vd.TipoValor.Equals(valDen.TipoValor) AndAlso vd.Calidad IsNot Nothing AndAlso valDen.Calidad IsNot Nothing AndAlso vd.Calidad.Identificador.Equals(valDen.Calidad.Identificador) AndAlso vd.UnidadMedida IsNot Nothing AndAlso valDen.UnidadMedida IsNot Nothing AndAlso vd.UnidadMedida.Identificador.Equals(valDen.UnidadMedida.Identificador)).First()
                                        valorDenominacion.Cantidad += valDen.Cantidad
                                        valorDenominacion.Importe += valDen.Importe

                                    End If

                                End If

                            Next

                        End If

                    Next

                End If

                ' meios de pagamento
                If div.MediosPago IsNot Nothing AndAlso div.MediosPago.Count() > 0 Then

                    For Each med In div.MediosPago.Where(Function(mp) mp IsNot Nothing AndAlso Not String.IsNullOrEmpty(mp.Identificador))

                        If Not divisa.MediosPago.Exists(Function(mp) mp.Identificador.Equals(med.Identificador)) Then
                            medioPago = New Clases.MedioPago With { _
                                .Codigo = med.Codigo, _
                                .CodigoUsuario = med.CodigoUsuario, _
                                .Descripcion = med.Descripcion, _
                                .EstaActivo = med.EstaActivo, _
                                .FechaHoraActualizacion = med.FechaHoraActualizacion, _
                                .Identificador = med.Identificador, _
                                .Observacion = med.Observacion, _
                                .Terminos = med.Terminos, _
                                .Tipo = med.Tipo, _
                                .Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                            }
                            divisa.MediosPago.Add(medioPago)
                        Else
                            medioPago = divisa.MediosPago.Where(Function(mp) mp.Identificador.Equals(med.Identificador)).First()
                        End If

                        ' valores de meios de pagamento
                        If med.Valores IsNot Nothing AndAlso med.Valores.Count() > 0 Then

                            For Each valMed In med.Valores.Where(Function(v) v IsNot Nothing AndAlso (Not v.Importe = 0 OrElse Not v.Cantidad = 0))

                                If Not medioPago.Valores.Exists(Function(vmp) vmp.InformadoPor.Equals(valMed.InformadoPor) AndAlso vmp.TipoValor.Equals(valMed.TipoValor) AndAlso vmp.UnidadMedida IsNot Nothing AndAlso valMed.UnidadMedida IsNot Nothing AndAlso vmp.UnidadMedida.Identificador.Equals(valMed.UnidadMedida.Identificador)) Then
                                    valorMedioPago = New Clases.ValorMedioPago With { _
                                        .Cantidad = valMed.Cantidad, _
                                        .Importe = valMed.Importe, _
                                        .InformadoPor = valMed.InformadoPor, _
                                        .TipoValor = valMed.TipoValor, _
                                        .UnidadMedida = valMed.UnidadMedida, _
                                        .Detallar = valMed.Detallar,
                                        .Color = valMed.Color,
                                        .Diferencia = valMed.Diferencia,
                                        .Terminos = If(valMed.Terminos IsNot Nothing AndAlso valMed.Terminos.Count() > 0, valMed.Terminos, New ObservableCollection(Of Clases.Termino))
                                        }
                                    medioPago.Valores.Add(valorMedioPago)
                                Else
                                    valorMedioPago = medioPago.Valores.Where(Function(vmp) vmp.InformadoPor.Equals(valMed.InformadoPor) AndAlso vmp.TipoValor.Equals(valMed.TipoValor) AndAlso vmp.UnidadMedida IsNot Nothing AndAlso valMed.UnidadMedida IsNot Nothing AndAlso vmp.UnidadMedida.Identificador.Equals(valMed.UnidadMedida.Identificador)).First()
                                    valorMedioPago.Cantidad += valMed.Cantidad
                                    valorMedioPago.Importe += valMed.Importe
                                    If valMed.Terminos IsNot Nothing AndAlso valMed.Terminos.Count() > 0 Then
                                        valorMedioPago.Terminos.AddRange(valMed.Terminos.Clonar())
                                    End If
                                End If

                            Next

                        End If

                    Next

                End If

                ' totais divisa (geral)
                If div.ValoresTotalesDivisa IsNot Nothing AndAlso div.ValoresTotalesDivisa.Count() > 0 Then

                    For Each valTotDiv In div.ValoresTotalesDivisa.Where(Function(vtd) vtd IsNot Nothing AndAlso Not vtd.Importe = 0)

                        If Not divisa.ValoresTotalesDivisa.Exists(Function(vtd) vtd.InformadoPor.Equals(valTotDiv.InformadoPor) AndAlso vtd.TipoValor.Equals(valTotDiv.TipoValor)) Then
                            valorTotalDivisa = New Clases.ValorDivisa With { _
                                .Importe = valTotDiv.Importe, _
                                .InformadoPor = valTotDiv.InformadoPor, _
                                .TipoValor = valTotDiv.TipoValor,
                                .Color = valTotDiv.Color,
                                .Detallar = valTotDiv.Detallar,
                                .Diferencia = valTotDiv.Diferencia
                            }
                            divisa.ValoresTotalesDivisa.Add(valorTotalDivisa)
                        Else
                            valorTotalDivisa = divisa.ValoresTotalesDivisa.Where(Function(vtd) vtd.InformadoPor.Equals(valTotDiv.InformadoPor) AndAlso vtd.TipoValor.Equals(valTotDiv.TipoValor)).First()
                            valorTotalDivisa.Importe += valTotDiv.Importe
                        End If

                    Next

                End If

                ' totais efectivo
                If div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count() > 0 Then

                    For Each valTotEfe In div.ValoresTotalesEfectivo.Where(Function(vte) vte IsNot Nothing AndAlso Not vte.Importe = 0)

                        If Not divisa.ValoresTotalesEfectivo.Exists(Function(vte) vte.InformadoPor.Equals(valTotEfe.InformadoPor) AndAlso vte.TipoValor.Equals(valTotEfe.TipoValor) AndAlso vte.TipoDetalleEfectivo.Equals(valTotEfe.TipoDetalleEfectivo)) Then
                            valorTotalEfectivo = New Clases.ValorEfectivo With { _
                                .Importe = valTotEfe.Importe, _
                                .InformadoPor = valTotEfe.InformadoPor, _
                                .TipoDetalleEfectivo = valTotEfe.TipoDetalleEfectivo, _
                                .TipoValor = valTotEfe.TipoValor,
                                .Color = valTotEfe.Color,
                                .Detallar = valTotEfe.Detallar,
                                .Diferencia = valTotEfe.Diferencia
                            }
                            divisa.ValoresTotalesEfectivo.Add(valorTotalEfectivo)
                        Else
                            valorTotalEfectivo = divisa.ValoresTotalesEfectivo.Where(Function(vte) vte.InformadoPor.Equals(valTotEfe.InformadoPor) AndAlso vte.TipoValor.Equals(valTotEfe.TipoValor) AndAlso vte.TipoDetalleEfectivo.Equals(valTotEfe.TipoDetalleEfectivo)).First()
                            valorTotalEfectivo.Importe += valTotEfe.Importe
                        End If

                    Next

                End If

                ' totais tipo medio pago
                If div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso div.ValoresTotalesTipoMedioPago.Count() > 0 Then

                    For Each valTotMed In div.ValoresTotalesTipoMedioPago.Where(Function(vtmp) vtmp IsNot Nothing AndAlso (Not vtmp.Importe = 0 OrElse Not vtmp.Cantidad = 0))

                        If Not divisa.ValoresTotalesTipoMedioPago.Exists(Function(vtmp) vtmp.InformadoPor.Equals(valTotMed.InformadoPor) AndAlso vtmp.TipoValor.Equals(valTotMed.TipoValor) AndAlso vtmp.TipoMedioPago.Equals(valTotMed.TipoMedioPago)) Then
                            valorTotalTipoMedioPago = New Clases.ValorTipoMedioPago With { _
                                .Cantidad = valTotMed.Cantidad, _
                                .Importe = valTotMed.Importe, _
                                .InformadoPor = valTotMed.InformadoPor, _
                                .TipoMedioPago = valTotMed.TipoMedioPago, _
                                .TipoValor = valTotMed.TipoValor,
                                .Color = valTotMed.Color,
                                .Detallar = valTotMed.Detallar,
                                .Diferencia = valTotMed.Diferencia
                            }
                            divisa.ValoresTotalesTipoMedioPago.Add(valorTotalTipoMedioPago)
                        Else
                            valorTotalTipoMedioPago = divisa.ValoresTotalesTipoMedioPago.Where(Function(vtmp) vtmp.InformadoPor.Equals(valTotMed.InformadoPor) AndAlso vtmp.TipoValor.Equals(valTotMed.TipoValor) AndAlso vtmp.TipoMedioPago.Equals(valTotMed.TipoMedioPago)).First()
                            valorTotalTipoMedioPago.Importe += valTotMed.Importe
                            valorTotalTipoMedioPago.Cantidad += valTotMed.Cantidad
                        End If

                    Next

                End If

                ' ValoresTotales
                If div.ValoresTotales IsNot Nothing AndAlso div.ValoresTotales.Count() > 0 Then

                    For Each valTotDiv In div.ValoresTotales.Where(Function(vtd) vtd IsNot Nothing AndAlso Not vtd.Importe = 0)

                        If Not divisa.ValoresTotales.Exists(Function(vtd) vtd.InformadoPor.Equals(valTotDiv.InformadoPor) AndAlso vtd.TipoValor.Equals(valTotDiv.TipoValor)) Then
                            valorTotal = New Clases.ImporteTotal With { _
                                .Importe = valTotDiv.Importe, _
                                .InformadoPor = valTotDiv.InformadoPor, _
                                .TipoValor = valTotDiv.TipoValor,
                                .Color = valTotDiv.Color,
                                .Detallar = valTotDiv.Detallar,
                                .Diferencia = valTotDiv.Diferencia
                            }
                            divisa.ValoresTotales.Add(valorTotal)
                        Else
                            valorTotal = divisa.ValoresTotales.Where(Function(vtd) vtd.InformadoPor.Equals(valTotDiv.InformadoPor) AndAlso vtd.TipoValor.Equals(valTotDiv.TipoValor)).First()
                            valorTotal.Importe += valTotDiv.Importe
                        End If

                    Next

                End If

            Next

            divisas = divisasUnificadas

        End If

    End Sub

    ''' <summary>
    ''' Unifica os items de divisas de forma a não repetir uma mesma combinação. Exemplos: Se a lista de divisas contém dois itens da mesma divisa,
    ''' a função unifica os valores de ambos para que ao final tenha somente um item de divisa. Se a lista de denominações de uma divisa tem dois (ou mais)
    ''' itens para mesma denominação, unifica-os de modo que fique somente um item (faz o mesmo com seus valores - considerando unidade de medida, 
    ''' informado por, qualidade e tipo de valor como chaves de comparação). Se a lista de meios de pagamentos de uma divisa tem dois (ou mais)
    ''' itens para o mesmo meio de pagamento, unifica-os de modo que fique somente um item (faz o mesmo com o seus valores - considerando unidade de
    ''' medida, informado por e tipo de valor como chaves de comparação). O mesmo se segue para as listas de valores totalizados.
    ''' 
    ''' Unifica também valores zerados
    ''' </summary>
    ''' <param name="divisas">Lista de Divisas que receberá o tratamento de "unificação" de itens.</param>
    ''' <remarks></remarks>
    Public Shared Sub UnificaItemsDivisas_v2(ByRef divisas As ObservableCollection(Of Clases.Divisa))

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            Dim divisasUnificadas As New ObservableCollection(Of Clases.Divisa)
            Dim divisa As Clases.Divisa
            Dim denominacion As Clases.Denominacion
            Dim valorDenominacion As Clases.ValorDenominacion
            Dim medioPago As Clases.MedioPago
            Dim valorMedioPago As Clases.ValorMedioPago
            Dim valorTotalDivisa As Clases.ValorDivisa
            Dim valorTotalEfectivo As Clases.ValorEfectivo
            Dim valorTotalTipoMedioPago As Clases.ValorTipoMedioPago
            Dim valorTotal As Clases.ImporteTotal

            For Each div In divisas.Where(Function(d) d IsNot Nothing AndAlso Not String.IsNullOrEmpty(d.Identificador))

                ' divisa
                If Not divisasUnificadas.Exists(Function(d) d.Identificador.Equals(div.Identificador)) Then
                    divisa = New Clases.Divisa With { _
                                          .CodigoAcceso = div.CodigoAcceso, _
                                          .CodigoISO = div.CodigoISO, _
                                          .CodigoSimbolo = div.CodigoSimbolo, _
                                          .CodigoUsuario = div.CodigoUsuario, _
                                          .Color = div.Color, _
                                          .Denominaciones = New ObservableCollection(Of Clases.Denominacion), _
                                          .Descripcion = div.Descripcion, _
                                          .EstaActivo = div.EstaActivo, _
                                          .FechaHoraTransporte = div.FechaHoraTransporte, _
                                          .Icono = div.Icono, _
                                          .Identificador = div.Identificador, _
                                          .MediosPago = New ObservableCollection(Of Clases.MedioPago), _
                                          .ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal), _
                                          .ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa), _
                                          .ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo), _
                                          .ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                                      }
                    divisasUnificadas.Add(divisa)
                Else
                    divisa = divisasUnificadas.Where(Function(d) d.Identificador.Equals(div.Identificador)).First()
                End If

                ' denominações
                If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count() > 0 Then

                    For Each den In div.Denominaciones.Where(Function(d) d IsNot Nothing AndAlso Not String.IsNullOrEmpty(d.Identificador))

                        If Not divisa.Denominaciones.Exists(Function(d) d.Identificador.Equals(den.Identificador)) Then
                            denominacion = New Clases.Denominacion With { _
                                .Codigo = den.Codigo, _
                                .CodigoUsuario = den.CodigoUsuario, _
                                .Descripcion = den.Descripcion, _
                                .EsBillete = den.EsBillete, _
                                .EstaActivo = den.EstaActivo, _
                                .FechaHoraActualizacion = den.FechaHoraActualizacion, _
                                .Identificador = den.Identificador, _
                                .Peso = den.Peso, _
                                .Valor = den.Valor, _
                                .ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion) _
                            }
                            divisa.Denominaciones.Add(denominacion)
                        Else
                            denominacion = divisa.Denominaciones.Where(Function(d) d.Identificador.Equals(den.Identificador)).First()
                        End If

                        ' valores denominação
                        If den.ValorDenominacion IsNot Nothing AndAlso den.ValorDenominacion.Count() > 0 Then

                            For Each valDen In den.ValorDenominacion.Where(Function(vd) vd IsNot Nothing)

                                If Not denominacion.ValorDenominacion.Exists(Function(vd) vd.InformadoPor.Equals(valDen.InformadoPor) AndAlso vd.TipoValor.Equals(valDen.TipoValor) AndAlso vd.Calidad IsNot Nothing AndAlso valDen.Calidad IsNot Nothing AndAlso vd.Calidad.Identificador.Equals(valDen.Calidad.Identificador) AndAlso vd.UnidadMedida IsNot Nothing AndAlso valDen.UnidadMedida IsNot Nothing AndAlso vd.UnidadMedida.Identificador.Equals(valDen.UnidadMedida.Identificador)) Then
                                    valorDenominacion = New Clases.ValorDenominacion With { _
                                        .Calidad = valDen.Calidad, _
                                        .Cantidad = valDen.Cantidad, _
                                        .Importe = valDen.Importe, _
                                        .InformadoPor = valDen.InformadoPor, _
                                        .TipoValor = valDen.TipoValor, _
                                        .UnidadMedida = valDen.UnidadMedida,
                                        .Detallar = valDen.Detallar,
                                        .Color = valDen.Color,
                                        .Diferencia = valDen.Diferencia
                                        }
                                    denominacion.ValorDenominacion.Add(valorDenominacion)
                                Else
                                    valorDenominacion = denominacion.ValorDenominacion.Where(Function(vd) vd.InformadoPor.Equals(valDen.InformadoPor) AndAlso vd.TipoValor.Equals(valDen.TipoValor) AndAlso vd.Calidad IsNot Nothing AndAlso valDen.Calidad IsNot Nothing AndAlso vd.Calidad.Identificador.Equals(valDen.Calidad.Identificador) AndAlso vd.UnidadMedida IsNot Nothing AndAlso valDen.UnidadMedida IsNot Nothing AndAlso vd.UnidadMedida.Identificador.Equals(valDen.UnidadMedida.Identificador)).First()
                                    valorDenominacion.Cantidad += valDen.Cantidad
                                    valorDenominacion.Importe += valDen.Importe
                                End If

                            Next

                        End If

                    Next

                End If

                ' meios de pagamento
                If div.MediosPago IsNot Nothing AndAlso div.MediosPago.Count() > 0 Then

                    For Each med In div.MediosPago.Where(Function(mp) mp IsNot Nothing AndAlso Not String.IsNullOrEmpty(mp.Identificador))

                        If Not divisa.MediosPago.Exists(Function(mp) mp.Identificador.Equals(med.Identificador)) Then
                            medioPago = New Clases.MedioPago With { _
                                .Codigo = med.Codigo, _
                                .CodigoUsuario = med.CodigoUsuario, _
                                .Descripcion = med.Descripcion, _
                                .EstaActivo = med.EstaActivo, _
                                .FechaHoraActualizacion = med.FechaHoraActualizacion, _
                                .Identificador = med.Identificador, _
                                .Observacion = med.Observacion, _
                                .Terminos = med.Terminos, _
                                .Tipo = med.Tipo, _
                                .Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                            }
                            divisa.MediosPago.Add(medioPago)
                        Else
                            medioPago = divisa.MediosPago.Where(Function(mp) mp.Identificador.Equals(med.Identificador)).First()
                        End If

                        ' valores de meios de pagamento
                        If med.Valores IsNot Nothing AndAlso med.Valores.Count() > 0 Then

                            For Each valMed In med.Valores.Where(Function(v) v IsNot Nothing)

                                If Not medioPago.Valores.Exists(Function(vmp) vmp.InformadoPor.Equals(valMed.InformadoPor) AndAlso vmp.TipoValor.Equals(valMed.TipoValor) AndAlso vmp.UnidadMedida IsNot Nothing AndAlso valMed.UnidadMedida IsNot Nothing AndAlso vmp.UnidadMedida.Identificador.Equals(valMed.UnidadMedida.Identificador)) Then
                                    valorMedioPago = New Clases.ValorMedioPago With { _
                                        .Cantidad = valMed.Cantidad, _
                                        .Importe = valMed.Importe, _
                                        .InformadoPor = valMed.InformadoPor, _
                                        .TipoValor = valMed.TipoValor, _
                                        .UnidadMedida = valMed.UnidadMedida, _
                                        .Detallar = valMed.Detallar,
                                        .Color = valMed.Color,
                                        .Diferencia = valMed.Diferencia,
                                        .Terminos = If(valMed.Terminos IsNot Nothing AndAlso valMed.Terminos.Count() > 0, valMed.Terminos, New ObservableCollection(Of Clases.Termino))
                                        }
                                    medioPago.Valores.Add(valorMedioPago)
                                Else
                                    valorMedioPago = medioPago.Valores.Where(Function(vmp) vmp.InformadoPor.Equals(valMed.InformadoPor) AndAlso vmp.TipoValor.Equals(valMed.TipoValor) AndAlso vmp.UnidadMedida IsNot Nothing AndAlso valMed.UnidadMedida IsNot Nothing AndAlso vmp.UnidadMedida.Identificador.Equals(valMed.UnidadMedida.Identificador)).First()
                                    valorMedioPago.Cantidad += valMed.Cantidad
                                    valorMedioPago.Importe += valMed.Importe
                                    If valMed.Terminos IsNot Nothing AndAlso valMed.Terminos.Count() > 0 Then
                                        valorMedioPago.Terminos.AddRange(valMed.Terminos.Clonar())
                                    End If
                                End If

                            Next

                        End If

                    Next

                End If

                ' totais divisa (geral)
                If div.ValoresTotalesDivisa IsNot Nothing AndAlso div.ValoresTotalesDivisa.Count() > 0 Then

                    For Each valTotDiv In div.ValoresTotalesDivisa.Where(Function(vtd) vtd IsNot Nothing)

                        If Not divisa.ValoresTotalesDivisa.Exists(Function(vtd) vtd.InformadoPor.Equals(valTotDiv.InformadoPor) AndAlso vtd.TipoValor.Equals(valTotDiv.TipoValor)) Then
                            valorTotalDivisa = New Clases.ValorDivisa With { _
                                .Importe = valTotDiv.Importe, _
                                .InformadoPor = valTotDiv.InformadoPor, _
                                .TipoValor = valTotDiv.TipoValor,
                                .Color = valTotDiv.Color,
                                .Detallar = valTotDiv.Detallar,
                                .Diferencia = valTotDiv.Diferencia
                            }
                            divisa.ValoresTotalesDivisa.Add(valorTotalDivisa)
                        Else
                            valorTotalDivisa = divisa.ValoresTotalesDivisa.Where(Function(vtd) vtd.InformadoPor.Equals(valTotDiv.InformadoPor) AndAlso vtd.TipoValor.Equals(valTotDiv.TipoValor)).First()
                            valorTotalDivisa.Importe += valTotDiv.Importe
                        End If

                    Next

                End If

                ' totais efectivo
                If div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count() > 0 Then

                    For Each valTotEfe In div.ValoresTotalesEfectivo.Where(Function(vte) vte IsNot Nothing)

                        If Not divisa.ValoresTotalesEfectivo.Exists(Function(vte) vte.InformadoPor.Equals(valTotEfe.InformadoPor) AndAlso vte.TipoValor.Equals(valTotEfe.TipoValor) AndAlso vte.TipoDetalleEfectivo.Equals(valTotEfe.TipoDetalleEfectivo)) Then
                            valorTotalEfectivo = New Clases.ValorEfectivo With { _
                                .Importe = valTotEfe.Importe, _
                                .InformadoPor = valTotEfe.InformadoPor, _
                                .TipoDetalleEfectivo = valTotEfe.TipoDetalleEfectivo, _
                                .TipoValor = valTotEfe.TipoValor,
                                .Color = valTotEfe.Color,
                                .Detallar = valTotEfe.Detallar,
                                .Diferencia = valTotEfe.Diferencia
                            }
                            divisa.ValoresTotalesEfectivo.Add(valorTotalEfectivo)
                        Else
                            valorTotalEfectivo = divisa.ValoresTotalesEfectivo.Where(Function(vte) vte.InformadoPor.Equals(valTotEfe.InformadoPor) AndAlso vte.TipoValor.Equals(valTotEfe.TipoValor) AndAlso vte.TipoDetalleEfectivo.Equals(valTotEfe.TipoDetalleEfectivo)).First()
                            valorTotalEfectivo.Importe += valTotEfe.Importe
                        End If

                    Next

                End If

                ' totais tipo medio pago
                If div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso div.ValoresTotalesTipoMedioPago.Count() > 0 Then

                    For Each valTotMed In div.ValoresTotalesTipoMedioPago.Where(Function(vtmp) vtmp IsNot Nothing)

                        If Not divisa.ValoresTotalesTipoMedioPago.Exists(Function(vtmp) vtmp.InformadoPor.Equals(valTotMed.InformadoPor) AndAlso vtmp.TipoValor.Equals(valTotMed.TipoValor) AndAlso vtmp.TipoMedioPago.Equals(valTotMed.TipoMedioPago)) Then
                            valorTotalTipoMedioPago = New Clases.ValorTipoMedioPago With { _
                                .Cantidad = valTotMed.Cantidad, _
                                .Importe = valTotMed.Importe, _
                                .InformadoPor = valTotMed.InformadoPor, _
                                .TipoMedioPago = valTotMed.TipoMedioPago, _
                                .TipoValor = valTotMed.TipoValor,
                                .Color = valTotMed.Color,
                                .Detallar = valTotMed.Detallar,
                                .Diferencia = valTotMed.Diferencia
                            }
                            divisa.ValoresTotalesTipoMedioPago.Add(valorTotalTipoMedioPago)
                        Else
                            valorTotalTipoMedioPago = divisa.ValoresTotalesTipoMedioPago.Where(Function(vtmp) vtmp.InformadoPor.Equals(valTotMed.InformadoPor) AndAlso vtmp.TipoValor.Equals(valTotMed.TipoValor) AndAlso vtmp.TipoMedioPago.Equals(valTotMed.TipoMedioPago)).First()
                            valorTotalTipoMedioPago.Importe += valTotMed.Importe
                            valorTotalTipoMedioPago.Cantidad += valTotMed.Cantidad
                        End If

                    Next

                End If

                ' ValoresTotales
                If div.ValoresTotales IsNot Nothing AndAlso div.ValoresTotales.Count() > 0 Then

                    For Each valTotDiv In div.ValoresTotales.Where(Function(vtd) vtd IsNot Nothing)

                        If Not divisa.ValoresTotales.Exists(Function(vtd) vtd.InformadoPor.Equals(valTotDiv.InformadoPor) AndAlso vtd.TipoValor.Equals(valTotDiv.TipoValor)) Then
                            valorTotal = New Clases.ImporteTotal With { _
                                .Importe = valTotDiv.Importe, _
                                .InformadoPor = valTotDiv.InformadoPor, _
                                .TipoValor = valTotDiv.TipoValor,
                                .Color = valTotDiv.Color,
                                .Detallar = valTotDiv.Detallar,
                                .Diferencia = valTotDiv.Diferencia
                            }
                            divisa.ValoresTotales.Add(valorTotal)
                        Else
                            valorTotal = divisa.ValoresTotales.Where(Function(vtd) vtd.InformadoPor.Equals(valTotDiv.InformadoPor) AndAlso vtd.TipoValor.Equals(valTotDiv.TipoValor)).First()
                            valorTotal.Importe += valTotDiv.Importe
                        End If

                    Next

                End If

            Next

            divisas = divisasUnificadas

        End If

    End Sub

    ''' <summary>
    ''' Retorna uma lista com os valores somados de importe de uma divisa, assim como o objeto divisa utilizado para chegar a soma.
    ''' Os valores são somados de acordo com o TipoValor informado e o nível de detalhe necessário.
    ''' </summary>
    ''' <param name="divisas">Lista de divisas para se obter o importe somado.</param>
    ''' <param name="tipoValor">Tipo de valor que será utilizado como filtro para a soma de importes.</param>
    ''' <param name="tipoNivelDetalle">Nível de detalhe que deverá ser somado os importes, quando não informado, soma em todos os níveis.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornaTotalImporteDivisas(ByRef divisas As ObservableCollection(Of Clases.Divisa), tipoValor As Enumeradores.TipoValor, Optional tipoNivelDetalle As Nullable(Of Enumeradores.TipoNivelDetalhe) = Nothing) As List(Of Tuple(Of Decimal, Clases.Divisa))

        Dim totalImporteDivisasCalculado As New List(Of Tuple(Of Decimal, Clases.Divisa))

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            ' cria um objeto "clonado" da lista de divisas para que seja
            ' possível aplicar a função de unificação sem atrapalhar a referência
            Dim divisasReferencia As ObservableCollection(Of Clases.Divisa) = divisas.Clonar()

            ' unifica os items de divisas para que o cálculo seja feito
            UnificaItemsDivisas(divisasReferencia)

            ' variavel responsável por manter o total de importe a ser somado na divisa
            Dim totalImporte As Decimal = 0

            For Each div In divisasReferencia.Where(Function(d) d IsNot Nothing)

                totalImporte = 0

                ' se não foi informado um nível de detalhe para somar ou se foi pedido para somar
                ' apenas os valores detalhados
                If tipoNivelDetalle Is Nothing OrElse tipoNivelDetalle.Equals(Enumeradores.TipoNivelDetalhe.Detalhado) Then

                    ' soma os valores detalhados - denominações
                    If div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count() > 0 Then
                        For Each den In div.Denominaciones.Where(Function(d) d IsNot Nothing AndAlso d.ValorDenominacion IsNot Nothing AndAlso d.ValorDenominacion.Count() > 0 AndAlso d.ValorDenominacion.Exists(Function(v) v.TipoValor.Equals(tipoValor)))
                            For Each valDen In den.ValorDenominacion.Where(Function(v) Not v.Importe = 0 AndAlso v.TipoValor.Equals(tipoValor))
                                totalImporte += valDen.Importe
                            Next
                        Next
                    End If

                    ' soma os valores detalhados - meios de pagamento
                    If div.MediosPago IsNot Nothing AndAlso div.MediosPago.Count() > 0 Then
                        For Each med In div.MediosPago.Where(Function(d) d IsNot Nothing AndAlso d.Valores IsNot Nothing AndAlso d.Valores.Count() > 0 AndAlso d.Valores.Exists(Function(v) v.TipoValor.Equals(tipoValor)))
                            For Each valMed In med.Valores.Where(Function(v) Not v.Importe = 0 AndAlso v.TipoValor.Equals(tipoValor))
                                totalImporte += valMed.Importe
                            Next
                        Next
                    End If

                End If

                ' se não foi informado um nível de detalhe para somar ou se foi pedido para somar
                ' apenas os valores totais gerais da divisa
                If tipoNivelDetalle Is Nothing OrElse tipoNivelDetalle.Equals(Enumeradores.TipoNivelDetalhe.TotalGeral) Then

                    ' soma os valores totais gerais da divisa
                    If div.ValoresTotalesDivisa IsNot Nothing AndAlso div.ValoresTotalesDivisa.Count() > 0 Then
                        For Each valTotDiv In div.ValoresTotalesDivisa.Where(Function(vtd) vtd IsNot Nothing AndAlso Not vtd.Importe = 0 AndAlso vtd.TipoValor.Equals(tipoValor))
                            totalImporte += valTotDiv.Importe
                        Next
                    End If

                End If

                ' se não foi informado um nível de detalhe para somar ou se foi pedido para somar
                ' apenas os valores totais da divisa
                If tipoNivelDetalle Is Nothing OrElse tipoNivelDetalle.Equals(Enumeradores.TipoNivelDetalhe.Total) Then

                    ' soma os valores totais da divisa - efectivo
                    If div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count() > 0 Then
                        For Each valTotEfe In div.ValoresTotalesEfectivo.Where(Function(vtd) vtd IsNot Nothing AndAlso Not vtd.Importe = 0 AndAlso vtd.TipoValor.Equals(tipoValor))
                            totalImporte += valTotEfe.Importe
                        Next
                    End If

                    ' soma os valores totais da divisa - tipo medio pago
                    If div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso div.ValoresTotalesTipoMedioPago.Count() > 0 Then
                        For Each valTotMed In div.ValoresTotalesTipoMedioPago.Where(Function(vtd) vtd IsNot Nothing AndAlso Not vtd.Importe = 0 AndAlso vtd.TipoValor.Equals(tipoValor))
                            totalImporte += valTotMed.Importe
                        Next
                    End If

                End If

                totalImporteDivisasCalculado.Add(New Tuple(Of Decimal, Clases.Divisa)(totalImporte, div))

            Next

        End If

        Return totalImporteDivisasCalculado

    End Function

    Public Shared Sub CalcularTotalesItemsDivisas(ByRef divisas As ObservableCollection(Of Clases.Divisa), TipoValor As Enumeradores.TipoValor)

        BorrarItemsDivisasDiferentesTipoValor(divisas, TipoValor)

        If divisas IsNot Nothing AndAlso divisas.Count() > 0 Then

            Dim totalDivisas As Integer = divisas.Count() - 1
            For div As Integer = totalDivisas To 0 Step -1

                divisas(div).ValoresTotales = New ObservableCollection(Of Clases.ImporteTotal)

                Dim Total As Decimal = 0

                ' denominações
                If divisas(div).Denominaciones IsNot Nothing AndAlso divisas(div).Denominaciones.Count() > 0 Then
                    Dim totalDenominaciones As Integer = divisas(div).Denominaciones.Count() - 1
                    For den As Integer = totalDenominaciones To 0 Step -1
                        If divisas(div).Denominaciones(den).ValorDenominacion IsNot Nothing AndAlso divisas(div).Denominaciones(den).ValorDenominacion.Count() > 0 Then
                            Dim totalValoresDenominaciones As Integer = divisas(div).Denominaciones(den).ValorDenominacion.Count() - 1
                            For valden As Integer = totalValoresDenominaciones To 0 Step -1
                                Total += divisas(div).Denominaciones(den).ValorDenominacion(valden).Importe
                            Next
                        End If
                    Next
                End If

                ' medio de pagos
                If divisas(div).MediosPago IsNot Nothing AndAlso divisas(div).MediosPago.Count() > 0 Then
                    Dim totalMediosPago As Integer = divisas(div).MediosPago.Count() - 1
                    For med As Integer = totalMediosPago To 0 Step -1

                        If divisas(div).MediosPago(med).Valores IsNot Nothing AndAlso divisas(div).MediosPago(med).Valores.Count() > 0 Then
                            Dim totalValoresMediosPago As Integer = divisas(div).MediosPago(med).Valores.Count() - 1
                            For valmed As Integer = totalValoresMediosPago To 0 Step -1
                                Total += divisas(div).MediosPago(med).Valores(valmed).Importe
                            Next
                        End If
                    Next
                End If

                ' geral
                If divisas(div).ValoresTotalesDivisa IsNot Nothing AndAlso divisas(div).ValoresTotalesDivisa.Count() > 0 Then
                    Dim totalItems As Integer = divisas(div).ValoresTotalesDivisa.Count() - 1
                    For ger As Integer = totalItems To 0 Step -1
                        Total += divisas(div).ValoresTotalesDivisa(ger).Importe
                    Next
                End If

                ' total efectivo
                If divisas(div).ValoresTotalesEfectivo IsNot Nothing AndAlso divisas(div).ValoresTotalesEfectivo.Count() > 0 Then
                    Dim totalItems As Integer = divisas(div).ValoresTotalesEfectivo.Count() - 1
                    For totefe As Integer = totalItems To 0 Step -1
                        Total += divisas(div).ValoresTotalesEfectivo(totefe).Importe
                    Next
                End If

                ' total tipo medio de pago
                If divisas(div).ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divisas(div).ValoresTotalesTipoMedioPago.Count() > 0 Then
                    Dim totalItems As Integer = divisas(div).ValoresTotalesTipoMedioPago.Count() - 1
                    For totmed As Integer = totalItems To 0 Step -1
                        Total += divisas(div).ValoresTotalesTipoMedioPago(totmed).Importe
                    Next
                End If

                If divisas(div).ValoresTotales.FirstOrDefault(Function(x) x.TipoValor = TipoValor) IsNot Nothing Then
                    divisas(div).ValoresTotales.FirstOrDefault(Function(x) x.TipoValor = TipoValor).Importe += Util.AtribuirValorObj(Total, GetType(Decimal))
                Else
                    Dim objTotal As New Clases.ImporteTotal
                    objTotal.Importe = Util.AtribuirValorObj(Total, GetType(Decimal))
                    objTotal.TipoValor = TipoValor
                    divisas(div).ValoresTotales.Add(objTotal)
                End If

            Next

        End If

    End Sub

    Public Shared Sub ActualizarSectorRemesa(ByRef remesa As Clases.Remesa, controlaSaldoPuesto As Boolean, codigoSector As String, codigoPuesto As String)

        If remesa.Cuenta IsNot Nothing AndAlso remesa.Cuenta.Sector IsNot Nothing Then

            If controlaSaldoPuesto Then
                remesa.Cuenta.Sector.Codigo = codigoPuesto
            Else
                remesa.Cuenta.Sector.Codigo = codigoSector
            End If

        End If

    End Sub

    Public Shared Function AgruparDivisasRemesa(divisasOrigen As ObservableCollection(Of Clases.Divisa), tipoValor As Enumeradores.TipoValor) As ObservableCollection(Of Clases.Divisa)

        Dim divisas As New ObservableCollection(Of Clases.Divisa)
        Dim denominaciones As New ObservableCollection(Of Clases.Denominacion)

        If divisasOrigen IsNot Nothing AndAlso divisasOrigen.Count > 0 Then

            For Each divisa In divisasOrigen

                Dim div = divisas.Where(Function(d) d.Identificador = divisa.Identificador).FirstOrDefault

                If div Is Nothing Then
                    divisas.Add(divisa)
                    div = divisa
                    Continue For
                End If

                If divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then

                    For Each denominacion In divisa.Denominaciones

                        Dim denonominacionActualizada = div.Denominaciones.Where(Function(deno) deno.Codigo = denominacion.Codigo).FirstOrDefault

                        If denominacion.ValorDenominacion IsNot Nothing AndAlso denominacion.ValorDenominacion.Count > 0 Then

                            If denonominacionActualizada IsNot Nothing Then

                                If denonominacionActualizada.ValorDenominacion IsNot Nothing AndAlso denonominacionActualizada.ValorDenominacion.Count > 0 Then
                                    denonominacionActualizada.ValorDenominacion.FirstOrDefault.Cantidad += denominacion.ValorDenominacion.FirstOrDefault.Cantidad
                                    denonominacionActualizada.ValorDenominacion.FirstOrDefault.Importe += denominacion.ValorDenominacion.FirstOrDefault.Importe
                                Else
                                    denonominacionActualizada.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion) From {denominacion.ValorDenominacion.FirstOrDefault}
                                End If

                            End If

                            For Each valor In denonominacionActualizada.ValorDenominacion
                                valor.TipoValor = tipoValor
                            Next valor

                        End If

                    Next denominacion

                End If

            Next divisa

            Return divisas

        End If


        Return Nothing

    End Function

    Public Shared Sub ZerarValoresDivisas(ByRef divisas As ObservableCollection(Of Clases.Divisa), Optional borrarCalidadesYUnidadesMedida As Boolean = False)

        If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

            For Each divisa In divisas

                If divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then

                    For Each denominacion In divisa.Denominaciones

                        If denominacion.ValorDenominacion IsNot Nothing AndAlso denominacion.ValorDenominacion.Count > 0 Then
                            denominacion.ValorDenominacion.Foreach(Sub(vd)
                                                                       vd.Cantidad = 0
                                                                       vd.Importe = 0
                                                                       If borrarCalidadesYUnidadesMedida Then
                                                                           vd.Calidad = Nothing
                                                                           vd.UnidadMedida = Nothing
                                                                       End If
                                                                   End Sub)
                        End If

                    Next denominacion

                End If

                If divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then
                    For Each mp In divisa.MediosPago
                        If mp.Valores IsNot Nothing AndAlso mp.Valores.Count > 0 Then
                            For Each valor In mp.Valores
                                valor.Cantidad = 0
                                valor.Importe = 0
                                If borrarCalidadesYUnidadesMedida Then
                                    valor.UnidadMedida = Nothing
                                End If
                            Next
                        End If
                    Next
                End If

                If divisa.ValoresTotalesDivisa IsNot Nothing AndAlso divisa.ValoresTotalesDivisa.Count > 0 Then
                    For Each valor In divisa.ValoresTotalesDivisa
                        valor.Importe = 0
                    Next
                End If
                If divisa.ValoresTotalesEfectivo IsNot Nothing AndAlso divisa.ValoresTotalesEfectivo.Count > 0 Then
                    For Each valor In divisa.ValoresTotalesEfectivo
                        valor.Importe = 0
                    Next
                End If
                If divisa.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divisa.ValoresTotalesTipoMedioPago.Count > 0 Then
                    For Each valor In divisa.ValoresTotalesTipoMedioPago
                        valor.Cantidad = 0
                        valor.Importe = 0
                    Next
                End If

            Next divisa

        End If

    End Sub

    Public Shared Sub ZerarValoresDivisa(ByRef divisa As Clases.Divisa)

        If divisa IsNot Nothing Then

            If divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then
                For Each denominacion In divisa.Denominaciones
                    If denominacion.ValorDenominacion IsNot Nothing AndAlso denominacion.ValorDenominacion.Count > 0 Then
                        For Each valor In denominacion.ValorDenominacion
                            valor.Cantidad = 0
                            valor.Importe = 0
                        Next
                    End If
                Next denominacion
            End If

            If divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then
                For Each mp In divisa.MediosPago
                    If mp.Valores IsNot Nothing AndAlso mp.Valores.Count > 0 Then
                        For Each valor In mp.Valores
                            valor.Cantidad = 0
                            valor.Importe = 0
                        Next
                    End If
                Next
            End If

            If divisa.ValoresTotalesDivisa IsNot Nothing AndAlso divisa.ValoresTotalesDivisa.Count > 0 Then
                For Each valor In divisa.ValoresTotalesDivisa
                    valor.Importe = 0
                Next
            End If
            If divisa.ValoresTotalesEfectivo IsNot Nothing AndAlso divisa.ValoresTotalesEfectivo.Count > 0 Then
                For Each valor In divisa.ValoresTotalesEfectivo
                    valor.Importe = 0
                Next
            End If
            If divisa.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso divisa.ValoresTotalesTipoMedioPago.Count > 0 Then
                For Each valor In divisa.ValoresTotalesTipoMedioPago
                    valor.Cantidad = 0
                    valor.Importe = 0
                Next
            End If

        End If

    End Sub

    Public Shared Sub ActualizarDivisasRemesa(remesa As Clases.Remesa)

        If remesa IsNot Nothing Then

            If remesa.Bultos IsNot Nothing AndAlso remesa.Bultos.Count > 0 Then

                ' Verifica se a remessa possui divisas
                If remesa.Divisas IsNot Nothing Then
                    ' Limpa os valores existentes nas denominações das divisas das remessas
                    remesa.Divisas.Foreach(Sub(div) If div.Denominaciones IsNot Nothing Then div.Denominaciones.Foreach(Sub(den) If den.ValorDenominacion IsNot Nothing Then den.ValorDenominacion.Clear()))
                End If

                ' Para cada malote existente, limpa as divisas dos malotes
                For Each bulto In remesa.Bultos.Where(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0 AndAlso Not b.EsModulo).ToObservableCollection

                    ' Se não existe divisas na remessa
                    If remesa.Divisas Is Nothing OrElse remesa.Divisas.Count = 0 Then

                        ' A remessa recebe a divida do malote
                        remesa.Divisas = bulto.Divisas.Clonar

                    Else

                        ' Para cada divisa existente no malote
                        For Each div In bulto.Divisas

                            ' Recupera a divisa da remessa de acordo com a divisa do malote
                            Dim divisaRemesa As Clases.Divisa = remesa.Divisas.FirstOrDefault(Function(f) f.CodigoISO = div.CodigoISO)

                            ' Se a remessa não possui a divisa
                            If divisaRemesa Is Nothing Then

                                ' Adiciona a divisa na remessa
                                remesa.Divisas.Add(div.Clonar)

                            Else

                                ' Verifica se existe denominações
                                If div.Denominaciones IsNot Nothing Then

                                    ' Verifica se a divisa da remessa possui denominações
                                    If divisaRemesa.Denominaciones Is Nothing Then

                                        ' Define as denominações da remessa
                                        divisaRemesa.Denominaciones = div.Denominaciones.Clonar

                                    End If

                                    ' Para cada denominação existente
                                    For Each den In div.Denominaciones

                                        ' Verifica se a denominação existe na remessa
                                        Dim denominacionDivRemesa As Clases.Denominacion = divisaRemesa.Denominaciones.FirstOrDefault(Function(f) f.Codigo = den.Codigo)

                                        ' Se a remessa não possui a denominação
                                        If denominacionDivRemesa Is Nothing Then

                                            ' Adiciona a denominação na divisa
                                            divisaRemesa.Denominaciones.Add(den.Clonar)

                                        Else

                                            ' Verifica se a denominação possui valor
                                            If den.ValorDenominacion IsNot Nothing Then

                                                ' Verifica se a denominação da divisa da remessa possui valores
                                                If denominacionDivRemesa.ValorDenominacion Is Nothing Then

                                                    ' Define os valores da denominação da divisa da remessa
                                                    denominacionDivRemesa.ValorDenominacion = den.ValorDenominacion.Clonar

                                                Else

                                                    ' Para cada valor da denominacao existente
                                                    For Each vd In den.ValorDenominacion

                                                        ' Busca a valor da denominacao
                                                        Dim valorDenominacion As Clases.ValorDenominacion = denominacionDivRemesa.ValorDenominacion.FirstOrDefault(Function(f) f.Calidad IsNot Nothing AndAlso vd.Calidad IsNot Nothing AndAlso f.Calidad.Codigo = vd.Calidad.Codigo)

                                                        ' Se nao encontrou o valor
                                                        If valorDenominacion Is Nothing Then
                                                            ' Adiciona o valor da denominacao
                                                            denominacionDivRemesa.ValorDenominacion.Add(vd.Clonar)
                                                        Else
                                                            ' Atualiza os valores da denomincao
                                                            valorDenominacion.Cantidad += vd.Cantidad
                                                            valorDenominacion.Importe += vd.Importe
                                                        End If

                                                    Next vd

                                                End If

                                            End If

                                        End If

                                    Next den

                                End If

                            End If

                        Next div

                    End If

                Next bulto

                Dim objDivisas As ObservableCollection(Of Clases.Divisa) = remesa.Divisas.Clonar

                If objDivisas Is Nothing Then objDivisas = New ObservableCollection(Of Clases.Divisa)

                ' remesa possui somente modulos 
                If (remesa.Bultos.Where(Function(b) b.EsModulo).Count = remesa.Bultos.Count AndAlso (remesa.Divisas Is Nothing OrElse Not (remesa.Divisas.Any(Function(div) div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Any(Function(efect) efect.Importe <> 0))))) Then
                    For Each bulto In remesa.Bultos.FindAll(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0)

                        For Each divisa In bulto.Divisas
                            If Not objDivisas.Exists(Function(div) div.CodigoISO = divisa.CodigoISO) AndAlso _
                               (remesa.Divisas Is Nothing OrElse
                                (remesa.Divisas IsNot Nothing AndAlso Not remesa.Divisas.Exists(Function(div) div.CodigoISO = divisa.CodigoISO))) Then
                                objDivisas.Add(divisa.Clonar)
                            Else
                                Dim divisaTemp = objDivisas.FirstOrDefault(Function(div) div.CodigoISO = divisa.CodigoISO)

                                If divisaTemp IsNot Nothing AndAlso divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then
                                    divisaTemp.Denominaciones = divisa.Denominaciones.Clonar
                                End If

                            End If
                        Next

                    Next

                    Comon.Util.ZerarValoresDivisas(objDivisas)

                    remesa.Divisas = objDivisas
                Else

                    Dim divisasBultos As New ObservableCollection(Of Clases.Divisa)
                    If remesa.Bultos IsNot Nothing AndAlso remesa.Bultos.Count > 0 Then
                        For Each bulto In remesa.Bultos.Where(Function(b) b.EsModulo AndAlso b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0)
                            divisasBultos.AddRange(bulto.Divisas)
                        Next
                    End If

                    Comon.Util.UnificaItemsDivisas(divisasBultos)
                    Comon.Util.ZerarValoresDivisas(divisasBultos)

                    If remesa.Divisas IsNot Nothing AndAlso remesa.Divisas.Count > 0 Then
                        For Each divisaBulto In divisasBultos
                            If Not remesa.Divisas.Exists(Function(e) e.Identificador = divisaBulto.Identificador) Then
                                remesa.Divisas.Add(divisaBulto)
                            End If
                        Next
                    Else
                        remesa.Divisas = divisasBultos
                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Atualiza as denominações da remesa
    ''' </summary>
    ''' <param name="remesas"></param>
    ''' <remarks></remarks>
    Public Shared Sub ActualizarDivisasRemesas(remesas As ObservableCollection(Of Clases.Remesa))

        If remesas IsNot Nothing Then

            Dim RemesasMescladas As New ObservableCollection(Of Clases.Remesa)
            Dim objRemesa As Clases.Remesa = Nothing

            For Each remesa In remesas.Where(Function(r) r.Bultos IsNot Nothing AndAlso r.Bultos.Count > 0).ToObservableCollection.Clonar

                objRemesa = RemesasMescladas.FindAll(Function(r) r.Identificador = remesa.Identificador).FirstOrDefault

                If objRemesa Is Nothing Then
                    RemesasMescladas.Add(remesa)
                Else
                    objRemesa.Bultos.AddRange(remesa.Bultos)
                End If

            Next


            For Each remesa In remesas.Where(Function(r) r.Bultos IsNot Nothing AndAlso r.Bultos.Count > 0).ToObservableCollection

                objRemesa = RemesasMescladas.Find(Function(r) r.Identificador = remesa.Identificador)

                If objRemesa IsNot Nothing Then

                    ' Verifica se a remessa possui divisas
                    If remesa.Divisas IsNot Nothing Then
                        ' Limpa os valores existentes nas denominações das divisas das remessas
                        remesa.Divisas.Foreach(Sub(div) If div.Denominaciones IsNot Nothing Then div.Denominaciones.Foreach(Sub(den) If den.ValorDenominacion IsNot Nothing Then den.ValorDenominacion.Clear()))
                    End If

                    ' Para cada malote existente, limpa as divisas dos malotes
                    For Each bulto In objRemesa.Bultos.Where(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0 AndAlso Not b.EsModulo).ToObservableCollection

                        ' Se não existe divisas na remessa
                        If remesa.Divisas Is Nothing OrElse remesa.Divisas.Count = 0 Then

                            ' A remessa recebe a divida do malote
                            remesa.Divisas = bulto.Divisas.Clonar

                        Else

                            ' Para cada divisa existente no malote
                            For Each div In bulto.Divisas

                                ' Recupera a divisa da remessa de acordo com a divisa do malote
                                Dim divisaRemesa As Clases.Divisa = remesa.Divisas.FirstOrDefault(Function(f) f.CodigoISO = div.CodigoISO)

                                ' Se a remessa não possui a divisa
                                If divisaRemesa Is Nothing Then

                                    ' Adiciona a divisa na remessa
                                    remesa.Divisas.Add(div.Clonar)

                                Else

                                    ' Verifica se existe denominações
                                    If div.Denominaciones IsNot Nothing Then

                                        ' Verifica se a divisa da remessa possui denominações
                                        If divisaRemesa.Denominaciones Is Nothing Then

                                            ' Define as denominações da remessa
                                            divisaRemesa.Denominaciones = div.Denominaciones.Clonar

                                        End If

                                        ' Para cada denominação existente
                                        For Each den In div.Denominaciones

                                            ' Verifica se a denominação existe na remessa
                                            Dim denominacionDivRemesa As Clases.Denominacion = divisaRemesa.Denominaciones.FirstOrDefault(Function(f) f.Codigo = den.Codigo)

                                            ' Se a remessa não possui a denominação
                                            If denominacionDivRemesa Is Nothing Then

                                                ' Adiciona a denominação na divisa
                                                divisaRemesa.Denominaciones.Add(den.Clonar)

                                            Else

                                                ' Verifica se a denominação possui valor
                                                If den.ValorDenominacion IsNot Nothing Then

                                                    ' Verifica se a denominação da divisa da remessa possui valores
                                                    If denominacionDivRemesa.ValorDenominacion Is Nothing Then

                                                        ' Define os valores da denominação da divisa da remessa
                                                        denominacionDivRemesa.ValorDenominacion = den.ValorDenominacion.Clonar

                                                    Else

                                                        ' Para cada valor da denominacao existente
                                                        For Each vd In den.ValorDenominacion

                                                            ' Busca a valor da denominacao
                                                            Dim valorDenominacion As Clases.ValorDenominacion = denominacionDivRemesa.ValorDenominacion.FirstOrDefault(Function(f) f.Calidad IsNot Nothing AndAlso vd.Calidad IsNot Nothing AndAlso f.Calidad.Codigo = vd.Calidad.Codigo)

                                                            ' Se nao encontrou o valor
                                                            If valorDenominacion Is Nothing Then
                                                                ' Adiciona o valor da denominacao
                                                                denominacionDivRemesa.ValorDenominacion.Add(vd.Clonar)
                                                            Else
                                                                ' Atualiza os valores da denomincao
                                                                valorDenominacion.Cantidad += vd.Cantidad
                                                                valorDenominacion.Importe += vd.Importe
                                                            End If

                                                        Next vd

                                                    End If

                                                End If

                                            End If

                                        Next den

                                    End If

                                End If

                            Next div

                        End If

                    Next bulto

                    Dim objDivisas As ObservableCollection(Of Clases.Divisa) = remesa.Divisas.Clonar

                    If objDivisas Is Nothing Then objDivisas = New ObservableCollection(Of Clases.Divisa)

                    ' remesa possui somente modulos
                    If (objRemesa.Bultos.Where(Function(b) b.EsModulo).Count = objRemesa.Bultos.Count) AndAlso (objRemesa.Divisas Is Nothing OrElse Not (objRemesa.Divisas.Any(Function(div) div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Any(Function(efect) efect.Importe <> 0)))) Then

                        For Each bulto In objRemesa.Bultos.FindAll(Function(b) b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0)

                            For Each divisa In bulto.Divisas
                                If Not objDivisas.Exists(Function(div) div.CodigoISO = divisa.CodigoISO) AndAlso _
                                   (remesa.Divisas Is Nothing OrElse
                                    (remesa.Divisas IsNot Nothing AndAlso Not remesa.Divisas.Exists(Function(div) div.CodigoISO = divisa.CodigoISO))) Then
                                    objDivisas.Add(divisa.Clonar)
                                Else
                                    Dim divisaTemp = objDivisas.FirstOrDefault(Function(div) div.CodigoISO = divisa.CodigoISO)

                                    If divisaTemp IsNot Nothing AndAlso divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then
                                        divisaTemp.Denominaciones = divisa.Denominaciones.Clonar
                                    End If

                                End If
                            Next

                        Next

                        Comon.Util.ZerarValoresDivisas(objDivisas)

                        remesa.Divisas = objDivisas
                    Else

                        Dim divisasBultos As New ObservableCollection(Of Clases.Divisa)
                        If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                            For Each bulto In objRemesa.Bultos.Where(Function(b) b.EsModulo AndAlso b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0)
                                divisasBultos.AddRange(bulto.Divisas)
                            Next
                        End If

                        Comon.Util.UnificaItemsDivisas(divisasBultos)
                        Comon.Util.ZerarValoresDivisas(divisasBultos)

                        If remesa.Divisas IsNot Nothing AndAlso remesa.Divisas.Count > 0 Then
                            For Each divisaBulto In divisasBultos
                                If Not remesa.Divisas.Exists(Function(e) e.Identificador = divisaBulto.Identificador) Then
                                    remesa.Divisas.Add(divisaBulto)
                                End If
                            Next
                        Else
                            remesa.Divisas = divisasBultos
                        End If

                    End If

                End If

            Next remesa

        End If

    End Sub

    Public Shared Function DataHoraGMT(fecha As DateTime, delegacion As Clases.Delegacion, esGMTZero As Boolean) As DateTime
        Dim Respuesta As DateTime = fecha
        Dim HusoHorarioEnMinutos As Integer = delegacion.HusoHorarioEnMinutos
        Dim AjusteHorarioVerano As Integer = 0

        ' Verifica se utilia Horario Verano
        If delegacion.AjusteHorarioVerano > 0 AndAlso
            (DateTime.Now >= delegacion.FechaHoraVeranoInicio AndAlso DateTime.Now < delegacion.FechaHoraVeranoFin.AddMinutes(delegacion.HusoHorarioEnMinutos)) Then
            AjusteHorarioVerano = delegacion.AjusteHorarioVerano
        End If

        ' Fuso Horario da Delegação
        If esGMTZero Then
            ' Se quero o GMTZero, retiro o horario de verão e retorno a data UTC
            Respuesta = Respuesta.AddMinutes(AjusteHorarioVerano * (-1))
            Respuesta = Respuesta.AddMinutes(HusoHorarioEnMinutos * (-1))
        Else
            ' Se estou recuperando da base, calculo do fuso horario da delegação e acerto o horario de verão
            Respuesta = Respuesta.AddMinutes(HusoHorarioEnMinutos)
            Respuesta = Respuesta.AddMinutes(AjusteHorarioVerano)
        End If
        Return Respuesta
    End Function

    Public Shared Function ValidarEsGeneracionF22(formulario As Clases.Formulario) As Boolean

        If formulario IsNot Nothing AndAlso
           formulario.Caracteristicas IsNot Nothing AndAlso
           formulario.TipoDocumento IsNot Nothing Then

            If Caracteristicas.Util.VerificarCaracteristicas(formulario.Caracteristicas,
                New Caracteristicas.GrupoCaracteristicas(Caracteristicas.TipoVerificacionCaracteristicas.Xor,
                        Enumeradores.CaracteristicaFormulario.GestiondeRemesas,
                        Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then

                If formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.Altas) AndAlso
                    formulario.TipoDocumento.Codigo = Comon.Constantes.CODIGO_TIPO_DOCUMENTO_REGPROC Then
                    Return True
                End If

            End If
        End If

        Return False

    End Function

    Public Shared Function FormatToSI(valor As Double, simbolo As String) As Double

        Dim incPrefixes As String() = New String() {"da", "h", "k", "M", "G", "T", "P", "E", "Z", "Y"}
        Dim decPrefixes As String() = New String() {"d", "c", "m", "u", "n", "p", "f", "a", "z", "y"}
        Dim dec As Boolean = False
        Dim degree As Decimal = Array.IndexOf(incPrefixes, simbolo)
        If degree = -1 Then
            degree = Array.IndexOf(decPrefixes, simbolo)
            dec = True
        End If

        If degree <= 1 Then
            Select Case degree
                Case 0
                    degree = 1 / 3
                Case 1
                    degree = 2 / 3
            End Select
        Else
            degree -= 1
        End If

        Dim scaled As Double = -1
        Dim numeroSI As Double = -1

        If dec Then
            degree *= -1
            scaled = Decimal.Round(Convert.ToDecimal(Math.Pow(1000, degree)), 2)
            numeroSI = valor * scaled
        Else
            scaled = Math.Round(Math.Pow(1000, degree))
            numeroSI = valor / scaled
        End If


        Return numeroSI

    End Function

    Public Shared Function FormatToSI(valor As Double, simbolo As String, qtdDecimais As Integer) As String

        Return FormatToSI(valor, simbolo).ToString("N" + qtdDecimais.ToString()) + simbolo

    End Function

    Public Shared Function ValidarNumeroDocumento(ByRef numeroDocumento As String, ByRef numeroSerie As String, ByRef numeroDigito As String, CodigoRegla As String) As Boolean

        ' Verifica se o número de documento foi informado
        If Not String.IsNullOrEmpty(numeroDocumento) AndAlso Not String.IsNullOrEmpty(CodigoRegla) Then

            ' Verifica se o número é válido
            If Not Long.TryParse(numeroDocumento, Nothing) Then
                Return False
            End If

            ' Verifica o a regra do codigo do documento
            Select Case CodigoRegla.Trim

                Case Constantes.DigitoVerificadorBrasil

                    ' Recupera o digito verificador
                    Dim digito As String = Right(numeroDocumento, 1)
                    ' Recupera o numero de serie
                    Dim serie As String = Left(Right(numeroDocumento, 2), 1)
                    ' Calcula o digito verificador ('-1' - remove o digito verificador)
                    Dim digitocalculado As String = GerarDigitoVerificador(Left(numeroDocumento, numeroDocumento.Length - 1), CodigoRegla)

                    ' Se o digito calculado é diferente
                    If digito <> digitocalculado Then
                        numeroDocumento = String.Empty
                        numeroSerie = String.Empty
                        numeroDigito = String.Empty
                        Return False
                    Else
                        ' ('-2' - remove o número de serie e o digito verificador)
                        numeroDocumento = Left(numeroDocumento, numeroDocumento.Length - 2)
                        numeroSerie = serie
                        numeroDigito = digito
                        Return True
                    End If

            End Select

        End If

        Return True

    End Function

    Public Shared Function GerarDigitoVerificador(numero As String, codigoRegla As String) As String

        ' Verifica se o número foi informado
        If Not String.IsNullOrEmpty(numero) AndAlso Integer.TryParse(numero, Nothing) Then

            ' Verifica se o parâmetro não foi informado
            If Not String.IsNullOrEmpty(codigoRegla) Then

                ' Verifica o a regra do codigo do documento
                Select Case codigoRegla.Trim

                    Case Comon.Constantes.DigitoVerificadorBrasil

                        ' Retorna o digito verificador
                        Dim sco As New Prosegur.Seguranca.DigitoVerificador.SCO
                        Return sco.GerarDigito(numero)

                End Select

            End If

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Adiciona uma mensagem ao arquivo de Log em disco
    ''' </summary>
    ''' <param name="mensagem"></param>
    ''' <remarks></remarks>
    Public Shared Sub LogMensagemEmDisco(mensagem As String, nomeArquivo As String, Optional tempoInicial As DateTime? = Nothing)

        ' Gera o arquivo
        Dim arquivo As StreamWriter = Nothing

        Try
            Dim pastaLog As String = AppDomain.CurrentDomain.BaseDirectory()
            LogMensagemEmDisco(mensagem, pastaLog, nomeArquivo, tempoInicial)
        Catch ex As Exception
            'Não faz nada se ocorrer algum erro ao serializar um erro que não foi possível gravar no banco.
        Finally

            If (arquivo IsNot Nothing) Then
                arquivo.Dispose()
                arquivo.Close()
            End If

        End Try
    End Sub

    Public Shared Sub LogMensagemEmDisco(mensagem As String, caminhoArquivo As String, nomeArquivo As String, Optional tempoInicial As DateTime? = Nothing)

        ' Gera o arquivo
        Dim arquivo As StreamWriter = Nothing

        Try
            Dim pastaLog As String = caminhoArquivo + "\LOG"

            If Not Directory.Exists(pastaLog) Then
                Directory.CreateDirectory(pastaLog)
            End If

            ' grava o arquivo de log em disco (mesmo nível em que os assemblys se encontram
            arquivo = New StreamWriter(pastaLog + "\" + nomeArquivo, True)

            ' trata a mensagem para que ele tenha uma identação correta no arquivo txt (caso haja quebra de linha)
            mensagem = mensagem.Replace(vbCrLf, vbCrLf + "                    ")

            If (tempoInicial IsNot Nothing) Then
                Dim tempoFinal As TimeSpan = Now.Subtract(tempoInicial)
                mensagem = mensagem + String.Format("- Tempo gasto:{0} ", tempoFinal)
            End If

            ' adiciona a linha no arquivo
            arquivo.WriteLine(Now.ToString("dd-MM-yyyy hh:mm:ss") + " " + mensagem)

        Catch ex As Exception
            'Não faz nada se ocorrer algum erro ao serializar um erro que não foi possível gravar no banco.
        Finally

            If (arquivo IsNot Nothing) Then
                arquivo.Dispose()
                arquivo.Close()
            End If

        End Try



    End Sub

    ''' <summary>
    ''' Generación de un texto (palabra, nombre, etc.) dinámico basado en un padrón previamente configurable
    ''' se hará la traducción de los códigos configurados por el texto descriptivo.
    '''{}	Marcación para fechas.	{dd/mm/yyyy}
    '''@@	Marcación para campos variables.	@CODIGO_DELEGACION@
    '''#Rx#, #Lx# Indica la cantidad de caracteres que será recuperado conforme letra “R-derecha” o “L-izquierda” indicada.	@DESCRIPCION_CANAL@#R4#	
    '''%U%, %L%	Indica si el valor deberá quedarse en caja alta (U) o caja baja (L).	@DESCRIPCION_CANAL@#R4#%U%	LDOS
    ''' @DES_SECTOR@%L%_@DES_DELEGACION@%U%_{MMM}
    ''' </summary>
    ''' <param name="texto">El texto que será formatado</param>
    ''' <param name="referencias">Las referencias a las variables</param>
    ''' <returns></returns>
    Public Shared Function GeneracionDinamicaTexto(texto As String, Optional referencias As Dictionary(Of String, String) = Nothing) As String
        Dim textoGenerado As String = texto

        If Not String.IsNullOrEmpty(textoGenerado) Then
            Dim totalCaracteres As Integer = textoGenerado.Length

            'FECHAS
            If textoGenerado.Contains("{") AndAlso textoGenerado.Contains("}") Then
                Try

                    Dim indexInicio As Integer = textoGenerado.IndexOf("{")
                    For i = indexInicio To totalCaracteres

                        Dim indexFim As Integer = textoGenerado.IndexOf("}", indexInicio)
                        Dim formatoData As String = textoGenerado.Substring(indexInicio, (indexFim - indexInicio) + 1)
                        textoGenerado = textoGenerado.Replace(formatoData, DateTime.Now.ToString(formatoData.Replace("{", "").Replace("}", "")))

                        If textoGenerado.IndexOf("{", indexFim - 1) > 0 Then
                            indexInicio = textoGenerado.IndexOf("{", indexFim - 1)
                            i = indexInicio
                        Else
                            Exit For
                        End If

                    Next

                Catch ex As Exception
                    Throw New Exception("Erro ao gerar data", ex)
                End Try
            End If

            'VARIABLES
            If textoGenerado.Contains("@") Then
                Try
                    If referencias Is Nothing OrElse referencias.Count = 0 Then
                        Throw New Exception("Referências não encontradas")
                    End If

                    Dim indexInicio As Integer = textoGenerado.IndexOf("@")
                    totalCaracteres = textoGenerado.Length
                    For i = indexInicio To totalCaracteres

                        Dim indexFim As Integer = textoGenerado.IndexOf("@", indexInicio + 1)
                        Dim variavel As String = textoGenerado.Substring(indexInicio, (indexFim - indexInicio) + 1)
                        Dim strVariavel As String = variavel.Replace("@", "")

                        'VALIDA VARIÁVEL
                        If Not referencias.ContainsKey(strVariavel) Then
                            Throw New Exception("Referência " + strVariavel + " não encontrada")
                        End If

                        Dim lstSufixo As New List(Of String)
                        If indexFim < (totalCaracteres - 1) Then
                            Dim proxCarac As String = textoGenerado(indexFim + 1)

                            'SUFIJOS
                            If proxCarac = "#" OrElse proxCarac = "%" Then

                                'RECUPERA PRIMEIRO SUFIXO
                                If proxCarac = "#" Then
                                    lstSufixo.Add(textoGenerado.Substring(indexFim + 1, (textoGenerado.IndexOf("#", indexFim + 2) - textoGenerado.IndexOf("#", indexFim + 1)) + 1))
                                Else
                                    lstSufixo.Add(textoGenerado.Substring(indexFim + 1, 3))
                                End If

                                If ((indexFim + lstSufixo(0).Length) + 1) < totalCaracteres Then

                                    'RECUPERA SEGUNDO SUFIXO
                                    Dim segundoSuf As String = textoGenerado.Substring((indexFim + lstSufixo(0).Length) + 1, 1)
                                    Dim indexSegSuf As Integer = (indexFim + lstSufixo(0).Length) + 1

                                    If segundoSuf = "#" OrElse segundoSuf = "%" Then

                                        If segundoSuf = "#" Then
                                            lstSufixo.Add(textoGenerado.Substring(indexSegSuf, (textoGenerado.IndexOf("#", indexSegSuf + 1) - textoGenerado.IndexOf("#", indexSegSuf)) + 1))
                                        Else
                                            lstSufixo.Add(textoGenerado.Substring(indexSegSuf, 3))
                                        End If

                                    End If

                                End If

                            End If

                        End If

                        Dim variavelFormatada As String = referencias(strVariavel)

                        'FORMATA DE ACORDO COM SUFIXO
                        For Each sufixo In lstSufixo

                            indexFim += sufixo.Length

                            Select Case sufixo(0)
                                Case "#"
                                    Dim qtdCarac As Integer = sufixo.Substring(2, sufixo.Length - 3)
                                    If sufixo(1) = "R" Then
                                        variavelFormatada = Strings.Right(variavelFormatada, qtdCarac)
                                    ElseIf sufixo(1) = "L" Then
                                        variavelFormatada = Strings.Left(variavelFormatada, qtdCarac)
                                    End If
                                Case "%"
                                    If sufixo(1) = "U" Then
                                        variavelFormatada = variavelFormatada.ToUpper()
                                    ElseIf sufixo(1) = "L" Then
                                        variavelFormatada = variavelFormatada.ToLower()
                                    End If
                            End Select

                        Next

                        'REPLACE DA VARIAVEL PELO VALOR FORMATADO
                        textoGenerado = textoGenerado.Replace(variavel + String.Join("", lstSufixo), variavelFormatada)

                        'ATUALIZA INDICE FINAL DA STRING FORMATADA
                        indexFim = (indexFim - (variavel + String.Join("", lstSufixo)).Length) + variavelFormatada.Length
                        indexFim = IIf(indexFim > 0, indexFim, 1)

                        If textoGenerado.IndexOf("@", indexFim - 1) > 0 Then
                            indexInicio = textoGenerado.IndexOf("@", indexFim - 1)
                            i = indexInicio
                        Else
                            Exit For
                        End If

                        totalCaracteres = textoGenerado.Length

                    Next

                Catch ex As Exception
                    Throw New Exception("Erro ao gerar variaveis", ex)
                End Try
            End If
        End If

        Return textoGenerado

    End Function


    Public Shared Function ValidarPrecintosDuplicados(documentos As ObservableCollection(Of Clases.Documento),
                                                      remesas As Prosegur.Global.Saldos.ContractoServicio.IngresoRemesasNuevo.Remesas,
                                                      mensajeTraduzida As String) As String

        Dim resultado As New List(Of Tuple(Of String, String))
        Dim precintos As New List(Of String)

        If documentos IsNot Nothing AndAlso documentos.Count > 0 Then
            For Each doc In documentos
                If doc.Elemento IsNot Nothing Then
                    Dim remesa As Clases.Remesa = DirectCast(doc.Elemento, Clases.Remesa)
                    If remesa.Bultos IsNot Nothing AndAlso remesa.Bultos.Count > 0 Then
                        For Each bulto In remesa.Bultos.Where(Function(x) x.Precintos IsNot Nothing AndAlso x.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(x.Precintos.First))

                            ' se não possuir na lista, adiciona
                            If Not precintos.Contains(bulto.Precintos.First) Then
                                precintos.Add(bulto.Precintos.First)
                            Else
                                If Not resultado.Exists(Function(x) x.Item1 = remesa.CodigoExterno AndAlso x.Item2 = bulto.Precintos.First) Then
                                    resultado.Add(New Tuple(Of String, String)(remesa.CodigoExterno, bulto.Precintos.First))
                                End If
                            End If

                        Next
                    End If
                End If
                precintos.Clear()
            Next
        ElseIf remesas IsNot Nothing AndAlso remesas.Count > 0 Then
            For Each remesa In remesas
                If remesa.Bultos IsNot Nothing AndAlso remesa.Bultos.Count > 0 Then
                    For Each bulto In remesa.Bultos.Where(Function(x) Not String.IsNullOrEmpty(x.CodigoPrecinto))

                        ' se não possuir na lista, adiciona
                        If Not precintos.Contains(bulto.CodigoPrecinto) Then
                            precintos.Add(bulto.CodigoPrecinto)
                        Else
                            If Not resultado.Exists(Function(x) x.Item1 = remesa.NumeroExterno AndAlso x.Item2 = bulto.CodigoPrecinto) Then
                                resultado.Add(New Tuple(Of String, String)(remesa.NumeroExterno, bulto.CodigoPrecinto))
                            End If
                        End If

                    Next
                End If
            Next
        End If

        If resultado.Count > 0 Then
            Dim valor As String = String.Empty
            For Each item In resultado
                If valor.Length = 0 Then
                    valor += String.Format(mensajeTraduzida, item.Item1, item.Item2)
                Else
                    valor += vbNewLine & String.Format(mensajeTraduzida, item.Item1, item.Item2)
                End If

            Next
            Return valor
        End If

        Return String.Empty

    End Function


End Class
