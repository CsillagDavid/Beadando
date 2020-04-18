Public Module Parser

    'Ellenőrzi az objectként kapott értéket, hogy az dátum-e, majd visszaadja az eredményt.
    Public Function IsDate(parameter As Object)
        Dim result As Date
        If Date.TryParse(parameter, result) Then
            Return result
        Else
            Return DateTime.Now
        End If
    End Function

    'Ellenőrzi, hogy egész számot adtunk-e meg, majd visszaadja az eredményt
    Public Function IsInteger(parameter As Object)
        Dim result As Integer
        If Integer.TryParse(parameter, result) Then
            Return result
        Else
            Return 0
        End If
    End Function

    'Ellenőrzi, hogy decimális számot adtunk-e meg, majd visszaadja az eredményt
    Public Function IsDecimal(parameter As Object)
        Dim result As Decimal
        If Decimal.TryParse(parameter, result) Then
            Return result
        Else
            Return 0
        End If
    End Function

    'Ellenőrzi, hogy a kapott dátum ünnepnapnak vagy hétvégének számít-e
    Public Function IsHolidayOrWeekend(datum As Date, holidays As List(Of Unnepnapok))
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
