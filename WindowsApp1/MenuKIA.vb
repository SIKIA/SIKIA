Imports MySql.Data.MySqlClient
Public Class MenuKIA

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem.Click
        Panel2.Visible = True
        Panel1.Visible = False
        Panel3.Visible = False
    End Sub

    Private Sub LaporanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanToolStripMenuItem.Click
        Panel2.Visible = False
        Panel1.Visible = False
        Panel3.Visible = True
    End Sub

    Private Sub ObatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ObatToolStripMenuItem.Click
        Panel2.Visible = False
        Panel1.Visible = True
        Panel3.Visible = False
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
        Call tampilPasien()

    End Sub

    Sub tampilPasien()
        Call koneksi()
        Dim table As New DataTable()
        adapter = New MySqlDataAdapter("select * from pasien", conn)
        adapter.Fill(table)
        tabelPasien.DataSource = table
        'Call aturDGV()
    End Sub

    Sub aturDGV()
        Dim btnBukaFile As New DataGridViewButtonColumn()
        tabelPasien.Columns.Add(btnBukaFile)
        btnBukaFile.Width = 80
        btnBukaFile.HeaderText = "Buka File"
        btnBukaFile.Text = "Buka File"
        btnBukaFile.Name = "btnBukaFile"
        btnBukaFile.UseColumnTextForButtonValue = True
        tabelPasien.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        tabelPasien.MultiSelect = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()
        Dim table As New DataTable()
        If noRM.Text IsNot "" And nama.Text IsNot "" Then
            cmd = New MySqlCommand("select * from pasien join pelayanan where pasien.NO_RM = pelayanan.NO_RM and pasien.NO_RM = '" & noRM.Text & "' and pasien.NAMA_PAS = '" & nama.Text & "' and pelayanan.TGL = @tgl", conn)
            cmd.Parameters.Add("tgl", MySqlDbType.Date).Value = tanggal.Value
            adapter = New MySqlDataAdapter(cmd)
            adapter.Fill(table)
            If table.Rows.Count() <= 0 Then
                MessageBox.Show("Data Tidak ditemuka")
            Else
                adapter.Fill(table)
                tabelPasien.DataSource = table
            End If
        ElseIf nama.Text IsNot "" Then
            cmd = New MySqlCommand("select * from pasien join pelayanan where pasien.NO_RM = pelayanan.NO_RM and pasien.NAMA_PAS = '" & nama.Text & "' and pelayanan.TGL =  @tgl", conn)
            cmd.Parameters.Add("tgl", MySqlDbType.Date).Value = tanggal.Value
            adapter = New MySqlDataAdapter(cmd)
            adapter.Fill(table)
            If table.Rows.Count() <= 0 Then
                MessageBox.Show("Data Tidak ditemukan nama")
            Else
                adapter.Fill(table)
                tabelPasien.DataSource = table
            End If
            'ElseIf nama IsNot Nothing And noRM IsNot Nothing Then
            '   adapter = New MySqlDataAdapter("select * from pasien join pelayanan where pasien.NO_RM = pelayanan.NO_RM and pasien.NAMA_PAS = '" & nama.Text & "' and pasien.NO_RM = '" & noRM.Text & "'", conn)
            '   adapter.Fill(table)
            '   If table.Rows.Count() <= 0 Then
            '   MessageBox.Show("Data Tidak ditemukan")
            'Else
            '   adapter.Fill(table)
            '   tabelPasien.DataSource = table
            'End If
        ElseIf noRM.Text IsNot "" Then
            cmd = New MySqlCommand("select * from pasien join pelayanan where pasien.NO_RM = pelayanan.NO_RM and pasien.NO_RM = '" & noRM.Text & "' and pelayanan.TGL =  @tgl", conn)
            cmd.Parameters.Add("tgl", MySqlDbType.Date).Value = tanggal.Value
            adapter = New MySqlDataAdapter(cmd)
            adapter.Fill(table)
            If table.Rows.Count() <= 0 Then
                MessageBox.Show("Data Tidak ditemukan norm")
            Else
                adapter.Fill(table)
                tabelPasien.DataSource = table
            End If
            'ElseIf nama IsNot Nothing Then
            '   adapter = New MySqlDataAdapter("select * from pasien join pelayanan where pasien.NO_RM = pelayanan.NO_RM and pasien.NAMA_PAS = '" & nama.Text & "'", conn)
            '   adapter.Fill(table)
            'If table.Rows.Count() <= 0 Then
            '   MessageBox.Show("Data Tidak ditemukan")
            'Else
            '   adapter.Fill(table)
            '   tabelPasien.DataSource = table
            'End If
        Else
            cmd = New MySqlCommand("select * from pasien join pelayanan where pasien.NO_RM = pelayanan.NO_RM and pelayanan.TGL =  @tgl", conn)
            cmd.Parameters.Add("tgl", MySqlDbType.Date).Value = tanggal.Value
            adapter = New MySqlDataAdapter(cmd)
            adapter.Fill(table)
            If table.Rows.Count() <= 0 Then
                MessageBox.Show("Data Tidak ditemukan")
            Else
                adapter.Fill(table)
                tabelPasien.DataSource = table
            End If
            'ElseIf noRM IsNot Nothing Then
            '   adapter = New MySqlDataAdapter("select * from pasien join pelayanan where pasien.NO_RM = pelayanan.NO_RM and pasien.NO_RM = '" & noRM.Text & "'", conn)
            '   adapter.Fill(table)
            '   If table.Rows.Count() <= 0 Then
            '   MessageBox.Show("Data Tidak ditemukan")
            'Else
            '    adapter.Fill(table)
            '   tabelPasien.DataSource = table
            'End If
            'Else
            '    MessageBox.Show("Tidak ada data yang dicari")
        End If
    End Sub
End Class