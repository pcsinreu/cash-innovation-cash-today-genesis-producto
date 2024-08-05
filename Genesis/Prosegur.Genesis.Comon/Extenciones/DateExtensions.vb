Imports System.Runtime.CompilerServices

Namespace Extenciones

    Public Module DateTimeExtensions

        <Extension()>
        Public Function QuieroExibirEstaFechaEnLaPatalla(originalDateTime As DateTime, delegacion As Clases.Delegacion) As DateTime
            Return originalDateTime.DataHoraGMT(delegacion, False)
        End Function

        <Extension()>
        Public Function DataHoraGMT(originalDateTime As DateTime, delegacion As Clases.Delegacion, esGMTZero As Boolean) As DateTime

            If delegacion Is Nothing OrElse originalDateTime = DateTime.MinValue Then
                Return originalDateTime
            End If

            Dim fecha As DateTime = originalDateTime
            Dim HusoHorarioEnMinutos As Integer = delegacion.HusoHorarioEnMinutos
            Dim AjusteHorarioVerano As Integer = 0

            ' Verifica se utilia Horario Verano
            If delegacion.AjusteHorarioVerano > 0 AndAlso
                (originalDateTime >= delegacion.FechaHoraVeranoInicio AndAlso
                 originalDateTime < delegacion.FechaHoraVeranoFin.AddMinutes(delegacion.HusoHorarioEnMinutos)) Then
                AjusteHorarioVerano = delegacion.AjusteHorarioVerano
            End If

            ' Fuso Horario da Delegação
            If esGMTZero Then
                ' Se quero o GMTZero, retiro o horario de verão e retorno a data UTC
                fecha = fecha.AddMinutes(AjusteHorarioVerano * (-1))
                fecha = fecha.AddMinutes(HusoHorarioEnMinutos * (-1))
            Else
                ' Se estou recuperando da base, calculo do fuso horario da delegação e acerto o horario de verão
                fecha = fecha.AddMinutes(HusoHorarioEnMinutos)
                fecha = fecha.AddMinutes(AjusteHorarioVerano)
            End If

            Return fecha
        End Function

        <Extension()>
        Public Function EndOfDay(originalDateTime As DateTime) As DateTime
            Return New DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, 23, 59, 59, 999)
        End Function

        <Extension()>
        Public Function BeginningOfDay(originalDateTime As DateTime) As DateTime
            Return New DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, 0, 0, 0, 0)
        End Function

        <Extension()>
        Public Function Midnight(originalDateTime As DateTime) As DateTime
            Return originalDateTime.BeginningOfDay()
        End Function

        <Extension()>
        Public Function Noon(originalDateTime As DateTime) As DateTime
            Return originalDateTime.SetTime(12, 0, 0, 0)
        End Function

        <Extension()>
        Public Function NextYear(originalDateTime As DateTime) As DateTime
            Dim nxtYear = originalDateTime.Year + 1
            Dim daysInMonth = DateTime.DaysInMonth(nxtYear, originalDateTime.Month)

            If daysInMonth < originalDateTime.Day Then
                Dim dayDiff = originalDateTime.Day - daysInMonth
                Dim dt =
                    New DateTime(nxtYear, originalDateTime.Month, daysInMonth, originalDateTime.Hour, originalDateTime.Minute, originalDateTime.Second, originalDateTime.Millisecond)
                Return dt.AddDays(dayDiff)
            End If

            Return New DateTime(nxtYear, originalDateTime.Month, originalDateTime.Day, originalDateTime.Hour, originalDateTime.Minute, originalDateTime.Second, originalDateTime.Millisecond)
        End Function

        <Extension()>
        Public Function PreviousYear(originalDateTime As DateTime) As DateTime
            Dim prevYear = originalDateTime.Year - 1
            Dim daysInMonth = DateTime.DaysInMonth(prevYear, originalDateTime.Month)

            If daysInMonth < originalDateTime.Day Then
                Dim dayDiff = originalDateTime.Day - daysInMonth
                Dim dt = New DateTime(prevYear, originalDateTime.Month, daysInMonth, originalDateTime.Hour, originalDateTime.Minute, originalDateTime.Second, originalDateTime.Millisecond)
                Return dt.AddDays(dayDiff)
            End If

            Return New DateTime(prevYear, originalDateTime.Month, originalDateTime.Day, originalDateTime.Hour, originalDateTime.Minute, originalDateTime.Second, originalDateTime.Millisecond)
        End Function

        '<Extension()>
        'Public Function NextDay(originalDateTime As DateTime) As DateTime
        '    Return originalDateTime + 1.Days()
        'End Function

        '<Extension()>
        'Public Function PreviousDay(originalDateTime As DateTime) As DateTime
        '    Return originalDateTime - 1.Days()
        'End Function

        '<Extension()>
        'Public Function NextWeek(originalDateTime As DateTime) As DateTime
        '    Return originalDateTime + 1.Weeks()
        'End Function

        '<Extension()>
        'Public Function PreviousWeek(originalDateTime As DateTime) As DateTime
        '    Return originalDateTime - 1.Weeks()
        'End Function

        '<Extension()>
        'Public Function [Next](originalDateTime As DateTime, day As DayOfWeek) As DateTime
        '    Do
        '        originalDateTime = originalDateTime.NextDay()
        '    Loop While originalDateTime.DayOfWeek <> day

        '    Return originalDateTime
        'End Function

        '<Extension()>
        'Public Function Previous(originalDateTime As DateTime, day As DayOfWeek) As DateTime
        '    Do
        '        originalDateTime = originalDateTime.PreviousDay()
        '    Loop While originalDateTime.DayOfWeek <> day

        '    Return originalDateTime
        'End Function

        <Extension()>
        Public Function AddTime(originalDateTime As DateTime, addedTimeSpan As TimeSpan) As DateTime
            Return originalDateTime + addedTimeSpan
        End Function

        <Extension()>
        Public Function SubtractTime(originalDateTime As DateTime, subtractedTimeSpan As TimeSpan) As DateTime
            Return originalDateTime - subtractedTimeSpan
        End Function

        <Extension()>
        Public Function SetTime(originalDateTime As DateTime, newHour As Integer)
            Return SetTime(originalDateTime, newHour, originalDateTime.Minute, originalDateTime.Second, originalDateTime.Millisecond)
        End Function

        <Extension()>
        Public Function SetTime(originalDateTime As DateTime, newHour As Integer, newMinute As Integer)
            Return SetTime(originalDateTime, newHour, newMinute, originalDateTime.Second, originalDateTime.Millisecond)
        End Function

        <Extension()>
        Public Function SetTime(originalDateTime As DateTime, newHour As Integer, newMinute As Integer, newSecond As Integer)
            Return SetTime(originalDateTime, newHour, newMinute, newSecond, originalDateTime.Millisecond)
        End Function

        <Extension()>
        Public Function SetTime(originalDateTime As DateTime, newHour As Integer, newMinute As Integer, newSecond As Integer, newMillisecond As Integer)
            Return New DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, newHour, newMinute, newSecond, newMillisecond)
        End Function

        <Extension()>
        Public Function SetDate(originalDateTime As DateTime, newYear As Integer) As DateTime
            Return SetDate(originalDateTime, newYear, originalDateTime.Month, originalDateTime.Day)
        End Function

        <Extension()>
        Public Function SetDate(originalDateTime As DateTime, newYear As Integer, newMonth As Integer) As DateTime
            Return SetDate(originalDateTime, newYear, newMonth, originalDateTime.Day)
        End Function

        <Extension()>
        Public Function SetDate(originalDateTime As DateTime, newYear As Integer, newMonth As Integer, newDay As Integer) As DateTime
            Return New DateTime(newYear, newMonth, newDay, originalDateTime.Hour, originalDateTime.Minute, originalDateTime.Second, originalDateTime.Millisecond)
        End Function

        <Extension()>
        Public Function SetYear(originalDateTime As DateTime, newYear As Integer) As DateTime
            Return SetDate(originalDateTime, newYear, originalDateTime.Month, originalDateTime.Day)
        End Function

        <Extension()>
        Public Function SetMonth(originalDateTime As DateTime, newMonth As Integer) As DateTime
            Return SetDate(originalDateTime, originalDateTime.Year, newMonth, originalDateTime.Day)
        End Function

        <Extension()>
        Public Function SetDay(originalDateTime As DateTime, newDay As Integer) As DateTime
            Return SetDate(originalDateTime, originalDateTime.Year, originalDateTime.Month, newDay)
        End Function

        <Extension()>
        Public Function SetHour(originalDateTime As DateTime, newHour As Integer) As DateTime
            Return New DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, newHour, originalDateTime.Minute, originalDateTime.Second, originalDateTime.Millisecond)
        End Function

        <Extension()>
        Public Function SetMinute(originalDateTime As DateTime, newMinute As Integer) As DateTime
            Return New DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, originalDateTime.Hour, newMinute, originalDateTime.Second, originalDateTime.Millisecond)
        End Function

        <Extension()>
        Public Function SetSecond(originalDateTime As DateTime, newSecond As Integer) As DateTime
            Return New DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, originalDateTime.Hour, originalDateTime.Minute, newSecond, originalDateTime.Millisecond)
        End Function

        <Extension()>
        Public Function SetMillisecond(originalDateTime As DateTime, newMillisecond As Integer) As DateTime
            Return New DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, originalDateTime.Hour, originalDateTime.Minute, originalDateTime.Second, newMillisecond)
        End Function

        <Extension()>
        Public Function At(originalDateTime As DateTime, newHour As Integer, newMinute As Integer) As DateTime
            Return originalDateTime.SetTime(newHour, newMinute)
        End Function

        <Extension()>
        Public Function At(originalDateTime As DateTime, newHour As Integer, newMinute As Integer, newSecond As Integer) As DateTime
            Return originalDateTime.SetTime(newHour, newMinute, newSecond)
        End Function

        <Extension()>
        Public Function IsBefore(firstDateTime As DateTime, secondDateTime As DateTime) As Boolean
            Return firstDateTime < secondDateTime
        End Function

        <Extension()>
        Public Function IsAfter(firstDateTime As DateTime, secondDateTime As DateTime) As Boolean
            Return Not IsBefore(firstDateTime, secondDateTime)
        End Function

        <Extension()>
        Public Function IsInFuture(originalDateTime As DateTime) As Boolean
            Return originalDateTime > DateTime.Now
        End Function

        <Extension()>
        Public Function IsInPast(originalDateTime As DateTime) As Boolean
            Return Not IsInFuture(originalDateTime)
        End Function

        <Extension()>
        Public Function AddBusinessDays(originalDateTime As DateTime, numberOfDays As Integer) As DateTime
            For i As Integer = 0 To Math.Abs(numberOfDays) - 1
                Do
                    originalDateTime = originalDateTime.AddDays(Math.Sign(numberOfDays))
                Loop While originalDateTime.DayOfWeek = DayOfWeek.Saturday OrElse originalDateTime.DayOfWeek = DayOfWeek.Sunday
            Next

            Return originalDateTime
        End Function

        <Extension()>
        Public Function SubtractBusinessDays(originalDateTime As DateTime, numberOfDays As Integer) As DateTime
            Return AddBusinessDays(originalDateTime, -numberOfDays)
        End Function

        <Extension()>
        Public Function StartOfWeek(originalDateTime As DateTime) As DateTime
            Dim currentCulture = Threading.Thread.CurrentThread.CurrentCulture
            Dim firstDay = currentCulture.DateTimeFormat.FirstDayOfWeek
            Dim offset = If(originalDateTime.DayOfWeek - firstDay, 7, 0)
            Dim daysSinceWeekBeginning = originalDateTime.DayOfWeek + offset - firstDay

            Return originalDateTime.AddDays(-daysSinceWeekBeginning)
        End Function

        <Extension()>
        Public Function QuieroGrabarGMTZeroEnLaBBDD(originalDateTime As DateTime, delegacion As Clases.Delegacion) As DateTime
            Return originalDateTime.DataHoraGMTZero(delegacion)
        End Function

        <Extension()>
        Public Function DataHoraGMTZero(originalDateTime As DateTime, delegacion As Clases.Delegacion) As DateTime

            If delegacion Is Nothing OrElse originalDateTime = DateTime.MinValue Then
                Return originalDateTime
            End If

            Dim fecha As DateTime = originalDateTime
            Dim HusoHorarioEnMinutos As Integer = delegacion.HusoHorarioEnMinutos
            Dim AjusteHorarioVerano As Integer = 0

            ' Verifica se utilia Horario Verano
            If delegacion.AjusteHorarioVerano > 0 AndAlso
                (originalDateTime >= delegacion.FechaHoraVeranoInicio AndAlso
                 originalDateTime < delegacion.FechaHoraVeranoFin.AddMinutes(delegacion.HusoHorarioEnMinutos)) Then
                AjusteHorarioVerano = delegacion.AjusteHorarioVerano
            End If

            ' Fuso Horario da Delegação
            If String.IsNullOrEmpty(fecha.ToString("%K")) Then
                ' Se quero o GMTZero, retiro o horario de verão e retorno a data UTC
                fecha = fecha.AddMinutes(AjusteHorarioVerano * (-1))
                fecha = fecha.AddMinutes(HusoHorarioEnMinutos * (-1))
            Else
                fecha = fecha.ToUniversalTime()
            End If

            Return fecha
        End Function

    End Module

End Namespace