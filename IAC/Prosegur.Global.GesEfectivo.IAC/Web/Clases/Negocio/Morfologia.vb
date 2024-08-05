Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion.ProxyWS.Integracion

Namespace Negocio

    <Serializable()> _
    Public Class Morfologia
        Inherits BaseEntidade

#Region "[VARIÁVEIS]"

        Private _oidMorfologia As String
        Private _codMorfologia As String
        Private _desMorfologia As String
        Private _bolVigente As Boolean
        Private _fyhActualizacion As DateTime
        Private _codUsuario As String
        Private _componentes As New List(Of Componente)
        Private _necModalidadRecogida As Integer

        ' associações da morfologia 
        Private _cajeroXMorfologia As New List(Of CajeroXMorfologia)

#End Region

#Region "[PROPRIEDADES]"

        Public ReadOnly Property FecInicio() As DateTime
            Get
                If _cajeroXMorfologia IsNot Nothing AndAlso _cajeroXMorfologia.Count > 0 Then
                    Return _cajeroXMorfologia(0).FecInicio
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Property NecModalidadRecogida() As Integer
            Get
                Return _necModalidadRecogida
            End Get
            Set(value As Integer)
                _necModalidadRecogida = value
            End Set
        End Property

        Public Property OidMorfologia() As String
            Get
                Return _oidMorfologia
            End Get
            Set(value As String)
                _oidMorfologia = value
            End Set
        End Property

        Public Property CodMorfologia() As String
            Get
                Return _codMorfologia
            End Get
            Set(value As String)
                _codMorfologia = value
            End Set
        End Property

        Public Property DesMorfologia() As String
            Get
                Return _desMorfologia
            End Get
            Set(value As String)
                _desMorfologia = value
            End Set
        End Property

        ''' <summary>
        ''' Retorna codígo e descrição da morfologia no formato: codigo - descrição
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CodDesMorfologia() As String
            Get
                Return _codMorfologia & " - " & _desMorfologia
            End Get
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _bolVigente
            End Get
            Set(value As Boolean)
                _bolVigente = value
            End Set
        End Property

        Public Property FyhActualizacion() As DateTime
            Get
                Return _fyhActualizacion
            End Get
            Set(value As DateTime)
                _fyhActualizacion = value
            End Set
        End Property

        Public ReadOnly Property DescripcionComponentes() As String
            Get
                Return ObtenerDescComponentes()
            End Get
        End Property

        Public Property CodUsuario() As String
            Get
                Return _codUsuario
            End Get
            Set(value As String)
                _codUsuario = value
            End Set
        End Property

        Public Property Componentes() As List(Of Componente)
            Get
                Return _componentes
            End Get
            Set(value As List(Of Componente))
                _componentes = value
            End Set
        End Property

        Public Property CajeroXMorfologia() As List(Of CajeroXMorfologia)
            Get
                Return _cajeroXMorfologia
            End Get
            Set(value As List(Of CajeroXMorfologia))
                _cajeroXMorfologia = value
            End Set
        End Property

#End Region

#Region "[CONSTRUTORES]"

        Public Sub New()



        End Sub

        Public Sub New(Oid As String, Codigo As String, Descripcion As String, Vigente As Boolean, _
                       FechaActualizacion As DateTime, CodigoUsuario As String)

            _oidMorfologia = Oid
            _codMorfologia = Codigo
            _desMorfologia = Descripcion
            _bolVigente = Vigente
            _fyhActualizacion = FechaActualizacion
            _codUsuario = CodigoUsuario

        End Sub

        Public Sub New(Morf As Integracion.ContractoServicio.GetMorfologiaDetail.Morfologia)

            Preencher(Morf)

        End Sub

        Public Sub New(Morf As ContractoServicio.ATM.GetATMDetail.Morfologia)

            _codMorfologia = Morf.CodigoMorfologia
            _desMorfologia = Morf.DescripcionMorfologia
            _oidMorfologia = Morf.OidMorfologia

            ' preenche associação entre cajero e morfologia
            _cajeroXMorfologia.Add(New CajeroXMorfologia(Nothing, Morf.OidMorfologia, Nothing, Morf.FechaInicio, Nothing, Nothing))

            ' inicializa lista de componentes
            Componentes = New List(Of Componente)

        End Sub

#End Region

#Region "[MÉTODOS]"

        Private Sub Preencher(Morf As Integracion.ContractoServicio.GetMorfologiaDetail.Morfologia)

            If Morf Is Nothing Then
                Exit Sub
            End If

            _codMorfologia = Morf.CodMorfologia
            _desMorfologia = Morf.DesMorfologia
            _bolVigente = Morf.BolVigente
            _necModalidadRecogida = Morf.NecModalidadRecogida
            _fyhActualizacion = Morf.FyhActualizacion

            ' preenche lista de componentes
            Componentes = New List(Of Componente)
            For Each c In (From comp In Morf.Componentes Order By comp.Orden Ascending)
                Componentes.Add(New Componente(c))
            Next

            ReconfigurarComponentes()

        End Sub

        Public Function ConvertToSetATM() As ContractoServicio.ATM.SetATM.Morfologia

            Dim Morfologia As New ContractoServicio.ATM.SetATM.Morfologia

            With Morfologia
                .OidMorfologia = _oidMorfologia
                If _cajeroXMorfologia IsNot Nothing Then
                    .FechaInicio = _cajeroXMorfologia(0).FecInicio
                End If
            End With

            Return Morfologia

        End Function

        Public Function ConvertToSetMorfologia(LoginUsuario As String) As ContractoServicio.Morfologia.SetMorfologia.Morfologia

            Dim morfologia As New ContractoServicio.Morfologia.SetMorfologia.Morfologia

            With morfologia
                .BolVigente = BolVigente
                .CodMorfologia = CodMorfologia
                .CodUsuario = LoginUsuario
                .DesMorfologia = DesMorfologia
                .OidMorfologia = OidMorfologia
                .NecModalidadRecogida = NecModalidadRecogida
                .Componentes = New List(Of ContractoServicio.Morfologia.SetMorfologia.Componente)
            End With

            For Each c In Componentes
                morfologia.Componentes.Add(c.ConvertToSetMorfologia())
            Next

            Return morfologia

        End Function

        ''' <summary>
        ''' Insere/Atualiza/exclui morfologia
        ''' </summary>
        ''' <param name="LoginUsuario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Guardar(LoginUsuario As String) As ContractoServicio.Morfologia.SetMorfologia.Respuesta

            Dim objProxy As New Comunicacion.ProxyMorfologia
            Dim objPeticion As New IAC.ContractoServicio.Morfologia.SetMorfologia.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Morfologia.SetMorfologia.Respuesta

            objPeticion.Morfologia = Me.ConvertToSetMorfologia(LoginUsuario)

            Select Case Me.Acao
                Case eAcao.Alta
                    objPeticion.Morfologia.OidMorfologia = String.Empty
                Case eAcao.Baja
                    objPeticion.Morfologia.BolBorrar = True
            End Select

            objRespuesta = objProxy.SetMorfologia(objPeticion)

            Return objRespuesta

        End Function

        ''' <summary>
        ''' Exclui morfologia
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Borrar(OidMorfologia As String) As ContractoServicio.Morfologia.SetMorfologia.Respuesta

            Dim objProxy As New Comunicacion.ProxyMorfologia
            Dim objPeticion As New IAC.ContractoServicio.Morfologia.SetMorfologia.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Morfologia.SetMorfologia.Respuesta

            ' inicializa objeto
            objPeticion.Morfologia = New ContractoServicio.Morfologia.SetMorfologia.Morfologia

            With objPeticion.Morfologia
                .BolBorrar = True
                .OidMorfologia = OidMorfologia
            End With

            objRespuesta = objProxy.SetMorfologia(objPeticion)

            Return objRespuesta

        End Function

        ''' <summary>
        ''' Obtém uma descrição informativa sobre os compontenes da morfologia.
        ''' Ex.: 4 dispensadores/ 1 Ingresador/ 1 Rechazo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  29/12/2010  criado
        ''' </history>
        Private Function ObtenerDescComponentes() As String

            Dim descripcion As New StringBuilder()
            Dim strDesc As String = String.Empty

            Dim numDispensador As Integer = (From c In Me.Componentes _
                                             Where c.NecFuncionContenedor = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR).Count()

            Dim numIngresador As Integer = (From c In Me.Componentes _
                                            Where c.NecFuncionContenedor = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_INGRESADOR).Count()

            Dim numRechazo As Integer = (From c In Me.Componentes _
                                         Where c.NecFuncionContenedor = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_RECHAZO).Count()

            Dim numTarjeta As Integer = (From c In Me.Componentes _
                                         Where c.NecFuncionContenedor = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA).Count()

            Dim numDeposito As Integer = (From c In Me.Componentes _
                                         Where c.NecFuncionContenedor = ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DEPOSITO).Count()

            If numDispensador > 0 Then
                If numDispensador = 1 Then
                    descripcion.Append(numDispensador & " " & Traduzir("021_lbl_dispensador") & "/ ")
                Else
                    descripcion.Append(numDispensador & " " & Traduzir("021_lbl_dispensadores") & "/ ")
                End If
            End If

            If numIngresador > 0 Then
                If numIngresador = 1 Then
                    descripcion.Append(numIngresador & " " & Traduzir("021_lbl_ingresador") & "/ ")
                Else
                    descripcion.Append(numIngresador & " " & Traduzir("021_lbl_ingresadores") & "/ ")
                End If
            End If

            If numRechazo > 0 Then
                If numRechazo = 1 Then
                    descripcion.Append(numRechazo & " " & Traduzir("021_lbl_rechazo") & "/ ")
                Else
                    descripcion.Append(numRechazo & " " & Traduzir("021_lbl_rechazos") & "/ ")
                End If
            End If

            If numTarjeta > 0 Then
                If numTarjeta = 1 Then
                    descripcion.Append(numTarjeta & " " & Traduzir("021_lbl_tarjeta") & "/ ")
                Else
                    descripcion.Append(numTarjeta & " " & Traduzir("021_lbl_tarjetas") & "/ ")
                End If
            End If

            If numDeposito > 0 Then
                If numDeposito = 1 Then
                    descripcion.Append(numDeposito & " " & Traduzir("021_lbl_deposito") & "/ ")
                Else
                    descripcion.Append(numDeposito & " " & Traduzir("021_lbl_depositos") & "/ ")
                End If
            End If

            strDesc = descripcion.ToString()

            If Not String.IsNullOrEmpty(strDesc) Then
                ' remove a última barra
                strDesc = strDesc.Substring(0, strDesc.Length - 2)
            End If

            Return strDesc

        End Function

        ''' <summary>
        ''' verifica se existe uma morfologia com os parámetros informados
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  29/12/2010  criado
        ''' </history>
        Public Shared Function VerificarMorfologia(Codigo As String, Descripcion As String) As IAC.ContractoServicio.Morfologia.VerificarMorfologia.Respuesta

            Dim objProxy As New Comunicacion.ProxyMorfologia
            Dim objPeticion As New IAC.ContractoServicio.Morfologia.VerificarMorfologia.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Morfologia.VerificarMorfologia.Respuesta

            objPeticion.CodigoMorfologia = Codigo
            objPeticion.DescripcionMorfologia = Descripcion

            objRespuesta = objProxy.VerificarMorfologia(objPeticion)

            Return objRespuesta

        End Function

        Public Sub getMorfologia(oidMorfologia As String)

            Dim objProxy As New Comunicacion.ProxyIacIntegracion
            Dim objPeticion As New Integracion.ContractoServicio.GetMorfologiaDetail.Peticion
            Dim objRespuesta As New Integracion.ContractoServicio.GetMorfologiaDetail.Respuesta

            ' preenche petição
            objPeticion.OidMorfologia = oidMorfologia

            ' executa serviço
            objRespuesta = objProxy.GetMorfologiaDetail(objPeticion)

            ' trata erros do serviço
            Dim resp As New ContractoServicio.RespuestaGenerico
            resp.CodigoError = objRespuesta.CodigoError
            resp.MensajeError = objRespuesta.MensajeError

            Me.Respuesta = resp

            If Me.Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' preenche objeto de retorno
                Preencher(objRespuesta.Morfologia)

                ' seta oid
                _oidMorfologia = oidMorfologia

            End If

        End Sub

        ''' <summary>
        ''' Metodo busca as morfologia de acordo com os parametros passados.
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 22/12/2010 - Criado 
        ''' </history>
        Public Shared Function getMorfologias(BolVigente As Boolean, CodMorfologia As String, DesMorfologia As String) As IAC.ContractoServicio.Morfologia.GetMorfologia.Respuesta

            Dim objProxy As New Comunicacion.ProxyMorfologia
            Dim objPeticion As New IAC.ContractoServicio.Morfologia.GetMorfologia.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Morfologia.GetMorfologia.Respuesta

            'Recebe os valores do filtro
            objPeticion.BolVigente = BolVigente
            objPeticion.CodMorfologia = CodMorfologia
            objPeticion.DesMorfologia = DesMorfologia

            objRespuesta = objProxy.GetMorfologia(objPeticion)

            Return objRespuesta

        End Function

        ''' <summary>
        ''' Obtém valores para preencher uma combo de morfologias
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa] 28/12/2010 - Criado 
        ''' </history>
        Public Function ObtenerCombo(Vigente As Boolean) As List(Of Morfologia)

            Dim objProxy As New Comunicacion.ProxyMorfologia
            Dim objPeticion As New IAC.ContractoServicio.Morfologia.GetMorfologia.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Morfologia.GetMorfologia.Respuesta

            'configura petição
            objPeticion.BolVigente = Vigente

            ' obtém morfologias
            objRespuesta = objProxy.GetMorfologia(objPeticion)

            ' seta resposta para que os erros sejam tratados na tela
            MyBase.Respuesta = objRespuesta

            If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then

                ' se não houve erros, retorna morfologias
                Return ConvertToListMorfologia(objRespuesta.Morfologias)

            End If

            ' se houve erros, retorna lista vazia
            Return New List(Of Negocio.Morfologia)

        End Function

        Public Shared Function ConvertToListMorfologia(Morfologias As List(Of ContractoServicio.Morfologia.GetMorfologia.Morfologia)) As List(Of Morfologia)

            Dim novaLista As New List(Of Morfologia)

            For Each item In Morfologias
                novaLista.Add(ConvertToMorfologia(item))
            Next

            Return novaLista

        End Function

        ''' <summary>
        ''' converto objeto do contrato da operação GetMorfologia para a classe de negócio morfologia
        ''' </summary>
        ''' <param name="Morf"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ConvertToMorfologia(Morf As ContractoServicio.Morfologia.GetMorfologia.Morfologia) As Morfologia

            Dim objMorfologia As New Morfologia
            Dim objComponente As Componente

            With objMorfologia
                .BolVigente = Morf.BolVigente
                .CodMorfologia = Morf.CodMorfologia
                .DesMorfologia = Morf.DesMorfologia
                .FyhActualizacion = Morf.FyhActualizacion
                .NecModalidadRecogida = Morf.NecModalidadRecogida
                .OidMorfologia = Morf.OidMorfologia
                .Componentes = New List(Of Componente)
            End With

            For Each c In Morf.Componentes

                objComponente = New Componente

                With objComponente
                    .BolVigente = c.BolVigente
                    .NecFuncionContenedor = c.necFuncionContenedor
                    .OidMorfologiaComponente = c.OidMorfologiaComponente
                    .Objectos = New List(Of Negocio.Objecto)
                End With

                objMorfologia.Componentes.Add(objComponente)

            Next

            Return objMorfologia

        End Function

        ''' <summary>
        ''' Retorna o componente do oid informado
        ''' </summary>
        ''' <param name="OidMorfologiaXComponente"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerComponente(OidMorfologiaXComponente As String) As Negocio.Componente

            Dim comp As Negocio.Componente = Nothing

            If Me.Componentes IsNot Nothing Then

                comp = (From c In Me.Componentes _
                                Where c.OidMorfologiaComponente = OidMorfologiaXComponente).FirstOrDefault()

            End If

            Return comp

        End Function

        ''' <summary>
        ''' Remove o componente informado da lista de componentes
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [blcosta]  03/01/2010  criado
        ''' </history>
        Public Sub EliminarComponente(OidMorfologiaXComponente As String)

            ' obtém componente
            Dim comp = (From c In Me.Componentes Where c.OidMorfologiaComponente = OidMorfologiaXComponente).SingleOrDefault()

            If comp IsNot Nothing Then

                ' remove da coleção
                Me.Componentes.Remove(comp)

            End If

        End Sub

        ''' <summary>
        ''' Move o componente para cima ou para baixo na lista
        ''' </summary>
        ''' <param name="OidMorfologiaXComponente"></param>
        ''' <param name="MoveParaBaixo">
        ''' True = move o componente para baixo na coleção. 
        ''' False = move para cima
        ''' </param>
        ''' <remarks></remarks>
        ''' <returns>
        ''' True = moveu algum item para cima ou para baixo
        ''' False = a lista continua a mesma
        ''' </returns>
        ''' <history>
        ''' [bruno.costa]  30/12/2010  criado
        ''' </history>
        Public Function MoverComponente(OidMorfologiaXComponente As String, MoveParaBaixo As Boolean) As Boolean

            Dim moveuItem As Boolean = False
            Dim comp As Negocio.Componente = Nothing

            If Me.Componentes IsNot Nothing Then

                ' obtém o item da coleção
                comp = (From c In Me.Componentes _
                        Where c.OidMorfologiaComponente = OidMorfologiaXComponente).FirstOrDefault()

                ' obtém seu indice
                Dim indice As Integer = Me.Componentes.IndexOf(comp)

                If MoveParaBaixo Then
                    ' verifica se o item já não é o último da lista
                    If indice < (Me.Componentes.Count - 1) Then
                        ' remove o item da colecao
                        Me.Componentes.RemoveAt(indice)
                        ' se já não for o primeiro item, decrementa
                        indice += 1
                        ' move o item na nova posição
                        Me.Componentes.Insert(indice, comp)
                        moveuItem = True
                    End If
                Else
                    ' verifica se o item já não é o primeiro da lista
                    If indice > 0 Then
                        ' remove o item da colecao
                        Me.Componentes.RemoveAt(indice)
                        ' se já não for o primeiro item, decrementa
                        indice -= 1
                        ' move o item na nova posição
                        Me.Componentes.Insert(indice, comp)
                        moveuItem = True
                    End If
                End If

            End If

            Return moveuItem

        End Function

        Public Function ObtenerDivisaEfetivos() As List(Of Divisa)

            Dim divEfetivos As New List(Of Divisa)
            Dim item As Divisa
            Dim codigo As String

            For Each c In _componentes

                For Each obj In c.Objectos

                    If obj.BolEfectivo Then

                        codigo = obj.CodIsoDivisa

                        ' verifica se já foi adicionado
                        If (From d In divEfetivos Where d.CodigoDivisa = codigo).FirstOrDefault() Is Nothing Then

                            ' se ainda não adicionou, adiciona a lista
                            item = New Divisa
                            item.CodigoDivisa = obj.CodIsoDivisa
                            item.DescripcionDivisa = obj.DesDivisa
                            divEfetivos.Add(item)

                        End If

                    End If

                Next

            Next

            Return divEfetivos

        End Function

        Public Function ObtenerMediosPago() As List(Of MedioPago)

            Dim lista As New List(Of MedioPago)
            Dim codigo As String

            For Each c In _componentes

                For Each obj In c.Objectos

                    For Each tmp In obj.TiposMedioPago

                        For Each mp In tmp.MediosPago

                            codigo = mp.CodMedioPago

                            If (From item In lista Where item.CodMedioPago = codigo).FirstOrDefault() Is Nothing Then

                                mp.Divisa.CodigoDivisa = obj.CodIsoDivisa
                                mp.Divisa.DescripcionDivisa = obj.DesDivisa
                                mp.TipoMedioPago.CodTipoMedioPago = tmp.CodTipoMedioPago
                                mp.TipoMedioPago.DesTipoMedioPago = tmp.DesTipoMedioPago

                                lista.Add(mp)

                            End If

                        Next

                    Next

                Next

            Next

            Return lista

        End Function


        ''' <summary>
        ''' Percorre componentes e reconfigura o código morfologia
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  29/12/2010
        ''' </history>
        Public Sub ReconfigurarComponentes()

            Dim indicesComponentes As New Dictionary(Of String, Integer)

            ' inicializa dicionario de índices dos componentes criados
            indicesComponentes.Add(ContractoServicio.Constantes.C_ORDEN_DEPOSITO, 0)
            indicesComponentes.Add(ContractoServicio.Constantes.C_ORDEN_DISPENSADOR, 0)
            indicesComponentes.Add(ContractoServicio.Constantes.C_ORDEN_INGRESADOR, 0)
            indicesComponentes.Add(ContractoServicio.Constantes.C_ORDEN_RECHAZO, 0)
            indicesComponentes.Add(ContractoServicio.Constantes.C_ORDEN_TARJETA, 0)

            Dim c As Negocio.Componente

            For i As Integer = 0 To Me.Componentes.Count - 1

                c = Me.Componentes(i)

                ' configura código
                c.Orden = i + 1

                ' configura descrição
                ObtenerDescripcionOrden(c, indicesComponentes)

            Next

        End Sub

        ''' <summary>
        ''' Obtém o código da morfologia de acordo com a função do componente.
        ''' Realiza o controle do dicionário de índices
        ''' </summary>
        ''' <remarks></remarks>
        ''' <history>
        ''' [bruno.costa]  29/12/2010  criado
        ''' </history>
        Private Shared Sub ObtenerDescripcionOrden(ByRef Componente As Componente, ByRef Indices As Dictionary(Of String, Integer))

            Dim sigla As String = String.Empty

            Select Case Componente.NecFuncionContenedor

                Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DISPENSADOR

                    sigla = ContractoServicio.Constantes.C_ORDEN_DISPENSADOR

                Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_INGRESADOR

                    sigla = ContractoServicio.Constantes.C_ORDEN_INGRESADOR

                Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_RECHAZO

                    sigla = ContractoServicio.Constantes.C_ORDEN_RECHAZO

                Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_TARJETA

                    sigla = ContractoServicio.Constantes.C_ORDEN_TARJETA

                Case ContractoServicio.Constantes.C_COD_FUNCION_CONTENEDOR_DEPOSITO

                    sigla = ContractoServicio.Constantes.C_ORDEN_DEPOSITO

            End Select

            Indices(sigla) += 1

            Componente.DesOrden = sigla & Indices(sigla)

        End Sub

#End Region

    End Class

End Namespace