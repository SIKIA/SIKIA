Public Class Detail
    Public session As String


    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        If e.ColumnIndex = 2 Then
            Dim id_ubah As String
            id_ubah = e.RowIndex
            formUbah(id_ubah)
        End If

    End Sub

    Sub formUbah(ByRef id_ubah)
        For i As Integer = id_ubah To vardosis.LongCount() - 1
            If i + 1 = vardosis.LongCount() Then
                varobat.RemoveAt(i)
                vardosis.RemoveAt(i)
            Else
                varobat(i) = varobat(i + 1)
                vardosis(i) = vardosis(i + 1)
            End If
        Next
        DataGridView1.Rows.RemoveAt(id_ubah)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

        For i As Integer = 0 To vardosis.LongCount() - 1
            Dim x As String() = {varobat(i), vardosis(i)}
            DataGridView1.Rows.Add(x)
        Next

        Dim btnEdit As New DataGridViewButtonColumn()
        DataGridView1.Columns.Add(btnEdit)
        btnEdit.HeaderText = "Hapus"
        btnEdit.Text = "Hapus"
        btnEdit.Name = "btnEdit"
        btnEdit.UseColumnTextForButtonValue = True
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
        For i As Integer = 0 To diagicd.LongCount() - 1
            Dim x As String() = {diagicd(i)}
            DataGridView2.Rows.Add(x)
        Next

        Dim btnEdit As New DataGridViewButtonColumn()
        DataGridView2.Columns.Add(btnEdit)
        btnEdit.HeaderText = "Hapus"
        btnEdit.Text = "Hapus"
        btnEdit.Name = "btnEdit"
        btnEdit.UseColumnTextForButtonValue = True
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 1 Then
            Dim id_ubah As String
            id_ubah = e.RowIndex
            formUbahicd(id_ubah)
        End If
    End Sub

    Sub formUbahicd(ByRef id_ubah)
        For i As Integer = id_ubah To diagicd.LongCount() - 1
            If i + 1 = diagicd.LongCount() Then
                diagicd.RemoveAt(i)
            Else
                diagicd(i) = diagicd(i + 1)
            End If
        Next
        DataGridView2.Rows.RemoveAt(id_ubah)
    End Sub

    Private Sub Detail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If session = "obat" Then
            Panel1.Visible = True
            Panel2.Visible = False
        ElseIf session = "icd" Then
            Panel1.Visible = False
            Panel2.Visible = True
        End If
    End Sub
End Class