Public Module Parser

    Public Function isDate(parameter As Object)
        Dim result As Date
        If Date.TryParse(parameter, result) Then
            Return result
        Else
            Return DateTime.Now
        End If
    End Function

    Public Function isInteger(parameter As Object)
        Dim result As Integer
        If Integer.TryParse(parameter, result) Then
            Return result
        Else
            Return 0
        End If
    End Function

    Public Function isDecimal(parameter As Object)
        Dim result As Decimal
        If Decimal.TryParse(parameter, result) Then
            Return result
        Else
            Return 0
        End If
    End Function

    Public Function isHolidayOrWeekend(datum As Date, holidays As List(Of Unnepnapok))
        If datum.DayOfWeek = DayOfWeek.Sunday Then
            Return False
        ElseIf datum.DayOfWeek() = DayOfWeek.Saturday Then
            Return False
        End If
        For Each item In holidays
            If item.Datum = datum Then
                If item.Tipus = 0 Then
                    Return False
                ElseIf item.Tipus = 1 Then
                    Return False
                Else
                    Return True
                End If
            End If
        Next
        Return True
    End Function
End Module
