Public Class Form2

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            If SaveFileDialog1.FileName <> "" Then
                Dim tt As String
                Dim Data As String
                Data = ""
                For i = 0 To DataGridView1.DataSource.Columns.Count - 1
                    Data = Data + DataGridView1.DataSource.Columns(i).ColumnName.ToString
                    If i < DataGridView1.DataSource.Columns.Count - 1 Then
                        Data = Data + "," 'CSV文件逗号分隔
                    End If
                Next
                tt = Data '列名
                For i = 0 To DataGridView1.DataSource.Rows.Count - 1
                    Data = ""
                    For j = 0 To DataGridView1.DataSource.Columns.Count - 1
                        Data = Data + DataGridView1.DataSource.Rows(i)(j).ToString.Trim
                        If j < DataGridView1.DataSource.Columns.Count - 1 Then
                            Data = Data + "," '行内容
                        End If
                    Next
                    tt = tt + vbCrLf + Data
                Next
                System.IO.File.WriteAllText(SaveFileDialog1.FileName, tt)
            End If
        End If
    End Sub
End Class