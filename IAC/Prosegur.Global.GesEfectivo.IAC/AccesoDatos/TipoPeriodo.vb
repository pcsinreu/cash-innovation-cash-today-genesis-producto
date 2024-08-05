Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad.GetComboTiposPeriodo

Public Class TipoPeriodo
    Public Shared Function GetComboTiposPeriodos() As TipoDePeriodoColeccion
        ' Crear objeto colección de tipo de periodos
        Dim objTiposDePeriodos As New ContractoServicio.Utilidad.GetComboTiposPeriodo.TipoDePeriodoColeccion

        ' Crear comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Obtener consulta
        comando.CommandText = Util.PrepararQuery(My.Resources.getComboTiposDePeriodo.ToString)
        comando.CommandType = CommandType.Text

        ' Ejecutar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' Si encontró algún registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            Dim indice As Integer = 0
            'Dim primerElemento As New TipoDeCuenta()
            'primerElemento.Indice = 0
            'primerElemento.Descripcion = "gen_ddl_selecione" 'Indica el primer elemento para que "seleccione" en el combo
            'objTiposDeCuentas.Add(primerElemento)

            'indice = indice + 1
            ' Recorre todos los registros
            For Each dr As DataRow In dtQuery.Rows

                ' Adicionamos un nuevo tipo a la colección
                objTiposDePeriodos.Add(PoblarTiposDePeriodos(dr))

            Next

        End If

        ' retornar colección de Tipos de Cuentas
        Return objTiposDePeriodos

    End Function

    Private Shared Function PoblarTiposDePeriodos(dr As DataRow) As TipoDePeriodo
        Dim objTipoDeCuenta As New TipoDePeriodo

        objTipoDeCuenta.Oid = dr("OID_TIPO_PERIODO").ToString()
        objTipoDeCuenta.Descripcion = dr("DES_TIPO_PERIODO").ToString()

        Return objTipoDeCuenta

    End Function
End Class
