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
        adapter = New MySqlDataAdapter("select a.NO_RM as 'NO. RM', a.NAMA_PAS as NAMA, b.TGL as 'TGL KUNJUNGAN', a.JK as 'JENIS KELAMIN', TIMESTAMPDIFF(YEAR, a.TANGGAL_LAHIR, CURRENT_DATE) AS USIA, a.ALAMAT from pasien a, pelayanan b where a.NO_RM = b.NO_RM and b.POLI = 'Poli KIA'", conn)
        adapter.Fill(table)
        tabelPasien.DataSource = table
    End Sub
    Sub tampilObat()
        Call koneksi()
        Dim table As New DataTable()
        adapter = New MySqlDataAdapter("select id_obat as 'ID OBAT', nama_obat as 'NAMA OBAT' from obat", conn)
        adapter.Fill(table)
        tabelObat.DataSource = table
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()
        Dim table As New DataTable()
        adapter = New MySqlDataAdapter("select a.NO_RM as 'NO. RM', a.NAMA_PAS as NAMA, b.TGL as 'TGL KUNJUNGAN', a.JK as 'JENIS KELAMIN', TIMESTAMPDIFF(YEAR, a.TANGGAL_LAHIR, CURRENT_DATE) AS USIA, a.ALAMAT from pasien a, pelayanan b where a.NO_RM = b.NO_RM and b.POLI = 'Poli KIA'", conn)
        adapter.Fill(table)
        Dim DV As New DataView(table)
        DV.RowFilter = String.Format("`no. rm` like '%{0}%' and nama like '%{1}%' and `tgl kunjungan` = '" + tanggal.Value.Date + "' ", noRM.Text, nama.Text)
        tabelPasien.DataSource = DV
    End Sub

    Private Sub tabelPasien_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles tabelPasien.CellContentDoubleClick
        session_NO_RM = tabelPasien.CurrentRow.Cells(0).Value.ToString()
        Dim session_TGL1 As Date = Convert.ToDateTime(tabelPasien.CurrentRow.Cells(2).Value.ToString())
        session_TGL = Format(session_TGL1, "yyyy-MM-dd")

        FormulirRekamMedis.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call koneksi()
        Dim table As New DataTable()
        adapter = New MySqlDataAdapter("select id_obat as 'ID OBAT', nama_obat as 'NAMA OBAT' from obat", conn)
        adapter.Fill(table)
        Dim DV As New DataView(table)
        DV.RowFilter = String.Format("`id obat` like '%{0}%' and `nama obat` like '%{1}%'", idObat.Text, namaObat.Text)
        tabelObat.DataSource = DV
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
        Call tampilObat()
    End Sub

    Private Sub KeluarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles KeluarToolStripMenuItem1.Click
        session_user = ""
        session_level = ""
        session_poli = ""
        Login.Show()
        Me.Close()
    End Sub
End Class