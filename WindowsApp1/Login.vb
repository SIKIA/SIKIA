Imports MySql.Data.MySqlClient

Public Class Login
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()
        Dim table As New DataTable()
        Dim usersess As String
        If session_level = "user" Then
            cmd = New MySqlCommand("select * from petugas where USERNAME_P = '" & user.Text & "' and PASSWORD_P = '" & pass.Text & "' and level = 'user' and JENIS_PETUGAS = 'POLI KIA' ", conn)
        ElseIf session_level = "admin" Then
            cmd = New MySqlCommand("select * from petugas where USERNAME_P = '" & user.Text & "' and PASSWORD_P = '" & pass.Text & "' and level = 'admin' ", conn)
        End If
        adapter = New MySqlDataAdapter(cmd)
        adapter.Fill(table)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            usersess = rd.Item("ID_PETUGAS")
            conn.Close()
            adapter.Dispose()
        End If
        If table.Rows.Count() <= 0 Then
            If session_poli = "POLI GIGI" Then
                MessageBox.Show("Menu GIGI belum tersedia!")
            Else
                MessageBox.Show("username atau password salah!")
            End If
        ElseIf session_poli = "admin" Then
            session_user = usersess
            MenuKIA.Show()
            Me.Close()
        Else
            session_user = usersess
            MenuKIA.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        user.Clear()
        pass.Clear()
        Panel2.Visible = True
        Panel1.Visible = False
        Panel3.Visible = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        session_level = "admin"
        Panel2.Visible = False
        Panel1.Visible = True
        Panel3.Visible = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        session_level = "user"
        Panel2.Visible = True
        Panel1.Visible = False
        Panel3.Visible = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        session_poli = "POLI KIA"
        Panel2.Visible = False
        Panel1.Visible = True
        Panel3.Visible = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        session_poli = "POLI GIGI"
        Panel2.Visible = False
        Panel1.Visible = True
        Panel3.Visible = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Panel2.Visible = False
        Panel1.Visible = False
        Panel3.Visible = True
    End Sub
End Class