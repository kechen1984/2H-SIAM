Public Class Form1
    Dim csvtable As String(,)
    Dim mydt As New DataTable
    Dim an As Integer
    Dim bn As Integer
    Dim ia As Integer
    Dim ib As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox3.SelectedIndex() = 0
        ComboBox5.SelectedIndex() = 2
        ComboBox7.SelectedIndex() = 2
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '检查输入是否均可转为数值
        If CheckBox1.Checked Then
            If IsNumeric(ComboBox1.Text) And
                IsNumeric(ComboBox2.Text) And
                IsNumeric(ComboBox3.Text) And
                IsNumeric(ComboBox4.Text) And
                IsNumeric(ComboBox5.Text) And
                IsNumeric(ComboBox6.Text) And
                IsNumeric(ComboBox7.Text) And
                IsNumeric(ComboBox8.Text) And
                IsNumeric(ComboBox9.Text) And
                IsNumeric(TextBox2.Text) And
                IsNumeric(TextBox3.Text) And
                IsNumeric(TextBox4.Text) And
                IsNumeric(TextBox5.Text) And
                IsNumeric(TextBox6.Text) And
                IsNumeric(TextBox7.Text) And
                IsNumeric(TextBox8.Text) And
                IsNumeric(TextBox9.Text) And
                IsNumeric(TextBox10.Text) Then
            Else
                MsgBox("Error message: All parameters must be numeric!")
                Exit Sub
            End If
        Else
            If IsNumeric(ComboBox1.Text) And
                IsNumeric(ComboBox2.Text) And
                IsNumeric(ComboBox3.Text) And
                IsNumeric(ComboBox4.Text) And
                IsNumeric(ComboBox5.Text) And
                IsNumeric(ComboBox8.Text) And
                IsNumeric(ComboBox9.Text) And
                IsNumeric(TextBox2.Text) And
                IsNumeric(TextBox3.Text) And
                IsNumeric(TextBox4.Text) And
                IsNumeric(TextBox5.Text) And
                IsNumeric(TextBox6.Text) Then
            Else
                MsgBox("Error message: All parameters must be numeric!")
                Exit Sub
            End If
        End If
        '检查列设置
        If CheckBox1.Checked Then
            If ComboBox1.Text = ComboBox2.Text Or
                (ComboBox1.Text >= ComboBox4.Text And ComboBox1.Text <= ComboBox4.Text + ComboBox5.Text) Or
                (ComboBox2.Text >= ComboBox4.Text And ComboBox2.Text <= ComboBox4.Text + ComboBox5.Text) Or
                (ComboBox1.Text >= ComboBox6.Text And ComboBox1.Text <= ComboBox6.Text + ComboBox7.Text) Or
                (ComboBox2.Text >= ComboBox6.Text And ComboBox2.Text <= ComboBox6.Text + ComboBox7.Text) Then
                MsgBox("Error message: There is a incorrect sequence number of column!")
                Exit Sub
            End If
        Else
            If ComboBox1.Text = ComboBox2.Text Or
                (ComboBox1.Text >= ComboBox4.Text And ComboBox1.Text <= ComboBox4.Text + ComboBox5.Text) Or
                (ComboBox2.Text >= ComboBox4.Text And ComboBox2.Text <= ComboBox4.Text + ComboBox5.Text) Then
                MsgBox("Error message: There is a incorrect sequence number of column!")
                Exit Sub
            End If
        End If
        '开始读取文件
        Dim fpathname As DialogResult
        fpathname = Me.OpenFileDialog1.ShowDialog()
        If fpathname = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog1.FileName
            TextBox1.Enabled = False
            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(TextBox1.Text)
                MyReader.SetDelimiters(",")
                Dim currentRow As String()
                an = ComboBox4.Text - 2
                Dim tw As Integer
                Dim th As Integer
                tw = 0
                th = 0
                Erase csvtable
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        tw = currentRow.Count
                        '计算均值重写数组
                        If CheckBox1.Checked Then
                            ReDim Preserve csvtable(tw + 1, th)
                            Dim i As Integer
                            Dim suma As Double
                            Dim sumb As Double
                            For i = 0 To tw - 1
                                csvtable(i, th) = currentRow(i)
                            Next
                            bn = ComboBox6.Text - 2
                            suma = 0
                            sumb = 0
                            For Me.ia = 1 To ComboBox5.Text
                                If IsNumeric(currentRow(an + ia)) Then
                                    suma = suma + currentRow(an + ia)
                                End If
                            Next
                            csvtable(tw, th) = suma / ComboBox5.Text
                            For Me.ib = 1 To ComboBox7.Text
                                If IsNumeric(currentRow(bn + ib)) Then
                                    sumb = sumb + currentRow(bn + ib)
                                End If
                            Next
                            csvtable(tw + 1, th) = sumb / ComboBox7.Text
                        Else
                            ReDim Preserve csvtable(tw, th)
                            Dim i As Integer
                            Dim suma As Double
                            For i = 0 To tw - 1
                                csvtable(i, th) = currentRow(i)
                            Next
                            suma = 0
                            For Me.ia = 1 To ComboBox5.Text
                                If IsNumeric(currentRow(an + ia)) Then
                                    suma = suma + currentRow(an + ia)
                                End If
                            Next
                            csvtable(tw, th) = suma / ComboBox5.Text
                        End If
                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                    End Try
                    th = th + 1
                End While
                '开始排序
                For i As Int32 = 1 To csvtable.GetLength(1) - 1
                    For j As Int32 = i + 1 To csvtable.GetLength(1) - 1
                        If Val(csvtable(0, i)) > Val(csvtable(0, j)) Then
                            Dim tmp_num(csvtable.GetLength(0) - 1) As String
                            For k As Int32 = 0 To tmp_num.Length - 1
                                tmp_num(k) = csvtable(k, i)
                            Next
                            For k As Int32 = 0 To tmp_num.Length - 1
                                csvtable(k, i) = csvtable(k, j)
                            Next
                            For k As Int32 = 0 To tmp_num.Length - 1
                                csvtable(k, j) = tmp_num(k)
                            Next
                        End If
                    Next
                Next
                MsgBox("The file was imported successfully. " & tw & "columns," & th & "rows.")
                Button2.Enabled = True
            End Using
        End If
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ComboBox6.Enabled = CheckBox1.Checked
        ComboBox7.Enabled = CheckBox1.Checked
        TextBox7.Enabled = CheckBox1.Checked
        TextBox8.Enabled = CheckBox1.Checked
        TextBox9.Enabled = CheckBox1.Checked
        TextBox10.Enabled = CheckBox1.Checked
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '检查输入是否均可转为数值
        If CheckBox1.Checked Then
            If IsNumeric(ComboBox1.Text) And
                IsNumeric(ComboBox2.Text) And
                IsNumeric(ComboBox3.Text) And
                IsNumeric(ComboBox4.Text) And
                IsNumeric(ComboBox5.Text) And
                IsNumeric(ComboBox6.Text) And
                IsNumeric(ComboBox7.Text) And
                IsNumeric(ComboBox8.Text) And
                IsNumeric(ComboBox9.Text) And
                IsNumeric(TextBox2.Text) And
                IsNumeric(TextBox3.Text) And
                IsNumeric(TextBox4.Text) And
                IsNumeric(TextBox5.Text) And
                IsNumeric(TextBox6.Text) And
                IsNumeric(TextBox7.Text) And
                IsNumeric(TextBox8.Text) And
                IsNumeric(TextBox9.Text) And
                IsNumeric(TextBox10.Text) Then
            Else
                MsgBox("Error message: All parameters must be numeric!")
                Exit Sub
            End If
        Else
            If IsNumeric(ComboBox1.Text) And
                IsNumeric(ComboBox2.Text) And
                IsNumeric(ComboBox3.Text) And
                IsNumeric(ComboBox4.Text) And
                IsNumeric(ComboBox5.Text) And
                IsNumeric(ComboBox6.Text) And
                IsNumeric(ComboBox7.Text) And
                IsNumeric(ComboBox8.Text) And
                IsNumeric(ComboBox9.Text) And
                IsNumeric(TextBox2.Text) And
                IsNumeric(TextBox3.Text) And
                IsNumeric(TextBox4.Text) And
                IsNumeric(TextBox5.Text) And
                IsNumeric(TextBox6.Text) Then
            Else
                MsgBox("Error message: All parameters must be numeric!")
                Exit Sub
            End If
        End If
        '检查列设置
        If CheckBox1.Checked Then
            If ComboBox1.Text = ComboBox2.Text Or
                (ComboBox1.Text >= ComboBox4.Text And ComboBox1.Text <= ComboBox4.Text + ComboBox5.Text) Or
                (ComboBox2.Text >= ComboBox4.Text And ComboBox2.Text <= ComboBox4.Text + ComboBox5.Text) Or
                (ComboBox1.Text >= ComboBox6.Text And ComboBox1.Text <= ComboBox6.Text + ComboBox7.Text) Or
                (ComboBox2.Text >= ComboBox6.Text And ComboBox2.Text <= ComboBox6.Text + ComboBox7.Text) Then
                MsgBox("Error message: There is a incorrect sequence number of column!")
                Exit Sub
            End If
        Else
            If ComboBox1.Text = ComboBox2.Text Or
                (ComboBox1.Text >= ComboBox4.Text And ComboBox1.Text <= ComboBox4.Text + ComboBox5.Text) Or
                (ComboBox2.Text >= ComboBox4.Text And ComboBox2.Text <= ComboBox4.Text + ComboBox5.Text) Then
                MsgBox("Error message: There is a incorrect sequence number of column!")
                Exit Sub
            End If
        End If
        '检查表头设置
        If Not IsNumeric(csvtable(ComboBox1.Text - 1, ComboBox3.Text)) Then
            MsgBox("Error message: The rows number of table header is incorrect!" & csvtable(ComboBox1.Text - 1, ComboBox3.Text))
            Exit Sub
        End If

        '正式处理数据，并捕获任何异常
        'Try
        Dim maxgap As Double = (ComboBox9.Text + 1) * TextBox4.Text
        Dim tub As Integer = UBound(csvtable, 2)
        Dim trub As Integer = UBound(csvtable, 1)
        Dim sn As Integer = ComboBox1.Text - 1
        Dim tn As Integer = ComboBox2.Text - 1
        Dim r As Integer
        Dim i As Integer
        Dim ci As Integer
        Dim n As Integer
        Dim t As String
        Dim st As DateTime
        Dim nr As DataRow
        Dim f1t As String
        Dim f2t As String
        Dim f3t As String
        Dim ra As Double
        Dim rb As Double
        Dim rab As Double
        an = ComboBox4.Text - 2
        st = Now
        '开始建立表头
        mydt.Clear()
        mydt.Columns.Clear()
        mydt.Columns.Add("m/z")
        mydt.Columns.Add("Retention Time")
        For Me.ia = 1 To ComboBox5.Text
            mydt.Columns.Add("A" & ia)
        Next
        If CheckBox1.Checked Then
            bn = ComboBox6.Text - 2
            For Me.ib = 1 To ComboBox7.Text
                mydt.Columns.Add("B" & ib)
            Next
        End If
        mydt.Columns.Add("Mean of F1")
        If CheckBox1.Checked = True Then
            mydt.Columns.Add("Mean of F2")
        End If
        mydt.Columns.Add("Tentative match")
        mydt.Columns.Add("Filter 1")
        If CheckBox1.Checked Then
            mydt.Columns.Add("Filter 2")
            mydt.Columns.Add("Filter 3")
        End If
        '表头建立完毕，开始写入表格数据，不含最后一行
        For r = ComboBox3.Text To tub - 1
            t = ""
            f1t = ""
            f2t = ""
            f3t = ""
            nr = mydt.NewRow
            nr("m/z") = csvtable(sn, r)
            nr("Retention Time") = csvtable(tn, r)
            For Me.ia = 1 To ComboBox5.Text
                nr("A" & ia) = csvtable(an + ia, r)
            Next
            If CheckBox1.Checked Then
                For Me.ib = 1 To ComboBox7.Text
                    nr("B" & ib) = csvtable(bn + ib, r)
                Next
                nr("Mean of F1") = func(csvtable(trub - 1, r), 3)
                nr("Mean of F2") = func(csvtable(trub, r), 3)
            Else
                nr("Mean of F1") = func(csvtable(trub, r), 3)
            End If
            For i = ComboBox8.Text To ComboBox9.Text
                For n = 1 To tub - r
                    If csvtable(sn, r + n) - csvtable(sn, r) > maxgap Then
                        Exit For
                    Else
                        If Math.Abs(csvtable(sn, r) + TextBox4.Text * i - csvtable(sn, r + n)) < 0.000001 * TextBox2.Text * csvtable(sn, r) And Math.Abs(csvtable(tn, r) - csvtable(tn, r + n)) < TextBox3.Text Then
                            t = t & i & "L @ " & Math.Round(Val(csvtable(sn, r + n)), 5) & " @ " & func(csvtable(sn + 1, r + n), 4) & " ; "
                            If CheckBox1.Checked Then
                                ra = csvtable(trub - 1, r) / csvtable(trub - 1, r + n)
                                rb = csvtable(trub, r) / csvtable(trub, r + n)
                                rab = csvtable(trub - 1, r + n) / csvtable(trub, r + n)
                                If ra >= TextBox5.Text * TextBox6.Text And ra <= TextBox5.Text / TextBox6.Text Then
                                    f1t = f1t & i & "L : " & func(ra, 3) & " " & func(csvtable(sn + 1, r + n), 4) & "; "
                                    If rb >= TextBox7.Text * TextBox8.Text And rb <= TextBox7.Text / TextBox8.Text Then
                                        f2t = f2t & i & "L : " & func(rb, 3) & " " & func(csvtable(sn + 1, r + n), 4) & "; "
                                        If rab >= TextBox9.Text * TextBox10.Text And rab <= TextBox9.Text / TextBox10.Text Then
                                            f3t = f3t & i & "L : " & func(rab, 3) & " " & func(csvtable(sn + 1, r + n), 4) & "; "
                                        End If
                                    End If
                                End If
                            Else
                                ra = csvtable(trub, r) / csvtable(trub, r + n)
                                If ra >= TextBox5.Text * TextBox6.Text And ra <= TextBox5.Text / TextBox6.Text Then
                                    f1t = f1t & i & "L : " & func(ra, 3) & " " & func(csvtable(sn + 1, r + n), 4) & "; "
                                End If
                            End If
                        End If
                    End If
                Next
            Next
            nr("Tentative match") = t
            nr("Filter 1") = f1t
            If CheckBox1.Checked Then
                nr("Filter 2") = f2t
                nr("Filter 3") = f3t
            End If
            mydt.Rows.Add(nr)
            If r Mod 10 = 0 Then
                Me.Text = "Processed " & r & " rows."
            End If
        Next
        '写入最后一行数据
        nr = mydt.NewRow
        nr("m/z") = csvtable(sn, tub)
        nr("Retention Time") = csvtable(tn, tub)
        For Me.ia = 1 To ComboBox5.Text
            nr("A" & ia) = csvtable(an + ia, tub)
        Next
        If CheckBox1.Checked Then
            For Me.ib = 1 To ComboBox7.Text
                nr("B" & ib) = csvtable(bn + ib, tub)
            Next
            nr("Mean of F1") = func(csvtable(trub - 1, tub), 3)
            nr("Mean of F2") = func(csvtable(trub, tub), 3)
        Else
            nr("Mean of F1") = func(csvtable(trub, tub), 3)
        End If
        mydt.Rows.Add(nr)
        MsgBox("Data processing  has consumed " & Now.Subtract(st).TotalSeconds.ToString & " seconds." & vbCrLf & vbCrLf & "You can double click on the table area to export your data file.")
        Form2.DataGridView1.DataSource = mydt
        For ci = 1 To Form2.DataGridView1.Columns.Count
            Form2.DataGridView1.Columns(ci - 1).SortMode = DataGridViewColumnSortMode.NotSortable '禁用排序使表头完全居中
        Next
        Form2.Show()
        'Catch ex As Exception
        '    MsgBox("There was an error in the processing. Please check the parameter settings." & vbCrLf & "Error message: " & vbCrLf & ex.Message)
        '    Me.Close()
        'End Try
        Me.Text = "Experimental Data Analysis V1.0"
    End Sub
    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If IsNumeric(TextBox5.Text) And IsNumeric(TextBox6.Text) Then
            If TextBox5.Text <= 0 Or TextBox6.Text <= 0 Then
                Label19.Text = "Please enter a value greater than 0."
            Else
                Label19.Text = "Range: " & Math.Round(TextBox5.Text * TextBox6.Text, 4) & " - " & Math.Round(TextBox5.Text / TextBox6.Text, 4)
            End If
        Else
            Label19.Text = "Please enter numeric digits only."
        End If
    End Sub
    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If IsNumeric(TextBox5.Text) And IsNumeric(TextBox6.Text) Then
            If TextBox5.Text <= 0 Or TextBox6.Text <= 0 Then
                Label19.Text = "Please enter a value greater than 0."
            Else
                If TextBox6.Text >= 1 Then
                    Label19.Text = "Tolerance value should be less than 1."
                Else
                    Label19.Text = "Range: " & Math.Round(TextBox5.Text * TextBox6.Text, 4) & " - " & Math.Round(TextBox5.Text / TextBox6.Text, 4)
                End If
            End If
        Else
            Label19.Text = "Please enter numeric digits only."
        End If
    End Sub
    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If IsNumeric(TextBox7.Text) And IsNumeric(TextBox8.Text) Then
            If TextBox7.Text <= 0 Or TextBox8.Text <= 0 Then
                Label20.Text = "Please enter a value greater than 0."
            Else
                Label20.Text = "Range: " & Math.Round(TextBox7.Text * TextBox8.Text, 4) & " - " & Math.Round(TextBox7.Text / TextBox8.Text, 4)
            End If
        Else
            Label20.Text = "Please enter numeric digits only."
        End If
    End Sub
    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If IsNumeric(TextBox7.Text) And IsNumeric(TextBox8.Text) Then
            If TextBox7.Text <= 0 Or TextBox8.Text <= 0 Then
                Label20.Text = "Please enter a value greater than 0."
            Else
                If TextBox8.Text >= 1 Then
                    Label20.Text = "Tolerance value should be less than 1."
                Else
                    Label20.Text = "Range: " & Math.Round(TextBox7.Text * TextBox8.Text, 4) & " - " & Math.Round(TextBox7.Text / TextBox8.Text, 4)
                End If
            End If
        Else
            Label20.Text = "Please enter numeric digits only."
        End If
    End Sub
    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox10.Text) Then
            If TextBox9.Text <= 0 Or TextBox10.Text <= 0 Then
                Label21.Text = "Please enter a value greater than 0."
            Else
                Label21.Text = "Range: " & Math.Round(TextBox9.Text * TextBox10.Text, 4) & " - " & Math.Round(TextBox9.Text / TextBox10.Text, 4)
            End If
        Else
            Label21.Text = "Please enter numeric digits only."
        End If
    End Sub
    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        If IsNumeric(TextBox9.Text) And IsNumeric(TextBox10.Text) Then
            If TextBox9.Text <= 0 Or TextBox10.Text <= 0 Then
                Label21.Text = "Please enter a value greater than 0."
            Else
                If TextBox10.Text >= 1 Then
                    Label21.Text = "Tolerance value should be less than 1."
                Else
                    Label21.Text = "Range: " & Math.Round(TextBox9.Text * TextBox10.Text, 4) & " - " & Math.Round(TextBox9.Text / TextBox10.Text, 4)
                End If
            End If
        Else
            Label21.Text = "Please enter numeric digits only."
        End If
    End Sub
End Class
