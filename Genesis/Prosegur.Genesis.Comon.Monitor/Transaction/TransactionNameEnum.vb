Imports System.Runtime.CompilerServices
Imports System.Reflection

Namespace Transaction
    Public Enum TransactionNameEnum
        <Sampling(1)>
        THREAD_1
        <Sampling(1)>
        THREAD_2
        <Sampling(1)>
        SALDOS_INTEGRACION_CONSULTARSALDOS
        <Sampling(1)>
        SALDOS_INTEGRACION_CREARDOCUMENTOFONDOS
        <Sampling(1)>
        SALDOS_INTEGRACION_RECUPERARTRANSACCIONESFECHAS
        <Sampling(1)>
        SALDOS_INTEGRACION_OBTENERFORMULARIOFONDOS
        <Sampling(5)>
        BUSCAR_RECURSOS_DIRECTOS
        <Sampling(2)>
        FRM_MONITOR_TEST
    End Enum

    Public Module TransactionName
        Private Function ComputeTransactionNameEnum(sampling As TransactionNameEnum) As Integer
            Dim oMemberInfo As MemberInfo = GetType(TransactionNameEnum).GetMember(sampling.ToString())(0)
            Dim oSamplingAttribute As SamplingAttribute = oMemberInfo.GetCustomAttributes(GetType(SamplingAttribute), False).FirstOrDefault
            If oSamplingAttribute IsNot Nothing Then
                Return oSamplingAttribute.Sampling
            Else
                Return 1
            End If
        End Function

        <Extension>
        Public Function getSampling(transactionNameEnum As TransactionNameEnum) As Integer
            Return ComputeTransactionNameEnum(transactionNameEnum)
        End Function
    End Module
End Namespace