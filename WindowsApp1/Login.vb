Imports MySql.Data.MySqlClient

Public Class Login
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()
        Dim table As New DataTable()
        cmd = New MySqlCommand("select * from petugas where USERNAME_P = '" & user.Text & "' and PASSWORD_P = '" & pass.Text & "' and level = '" & session_level & "'", conn)
        adapter = New MySqlDataAdapter(cmd)
        adapter.Fill(table)


        If table.Rows.Count() <= 0 Then
            MessageBox.Show("username atau password salah!")
        ElseIf session_poli = "POLI KIA" And session_level = "user" Then
            session_user = user.Text
            MenuKIA.Show()
            Me.Close()
        ElseIf session_poli = "POLI GIGI" And session_level = "user" Then
            session_user = user.Text
            MessageBox.Show("Poli Gigi belum tersedia")
        ElseIf session_poli = "ADMIN" Then
            session_user = user.Text
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

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        session_level = "admin"
        Panel2.Visible = True
        Panel1.Visible = False
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