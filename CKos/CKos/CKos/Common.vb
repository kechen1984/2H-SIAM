Module Common
    Public Class Node
        Public num As Int16
        Public ten As Double
    End Class
    Public Function func(num As Double, total As Int32) As String
        Dim lst As New List(Of Node)
        Dim tmp As String = num.ToString()
        Dim p_index As Int16 = tmp.IndexOf(".")
        If p_index = -1 Then p_index = tmp.Length
        For i As Int16 = 0 To tmp.Length - 1
            Dim c As String = tmp.Substring(i, 1)
            If c <> "." Then
                Dim node1 As New Node
                lst.Add(node1)
                node1.num = c
                If i > p_index Then
                    node1.ten = 10 ^ (p_index - i)
                Else
                    node1.ten = 10 ^ (p_index - i - 1)
                End If
            End If
        Next
        Dim y As Double = 0
        Dim count = 0
        Dim started As Boolean = False
        For i As Int16 = 0 To lst.Count - 1
            If lst(i).num <> 0 And started = False Then
                started = True
            End If
            If started Then
                y += lst(i).num * lst(i).ten
                count += 1
                If count >= total Then
                    If i + 1 <= lst.Count - 1 Then
                        If lst(i + 1).num >= 5 Then
                            lst(i).num += 1
                        End If
                    End If
                    Exit For
                End If
            End If
        Next
        Dim content As String = y
        count = content.Length
        If content.IndexOf(".") <> -1 Then
            count -= 1
        End If
        Do While count < total
            content += "0"
            count += 1
        Loop
        Return content
    End Function
End Module
