Imports System.ComponentModel
Imports MySql.Data.MySqlClient
Public Class FormulirRekamMedis
    Public noreg As Integer
    Public id As String
    Private Sub BATAL_Click(sender As Object, e As EventArgs) Handles BATAL.Click
        Me.Close()
    End Sub

    Private Sub FormulirRekamMedis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call isi_obat()
        Call koneksi()
        Dim table As New DataTable()
        cmd = New MySqlCommand("select * from pasien a join pelayanan b join poli_kia c where a.NO_RM = b.NO_RM and a.NO_RM = @norm and b.TGL = '" & session_TGL & "' and b.NOREG = c.NOREG", conn)
        cmd.Parameters.Add("norm", MySqlDbType.VarChar).Value = session_NO_RM
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            id = rd.Item("id_KIA")

            noreg = rd.Item("NOREG")
            NO_RM.Text = rd.Item("NO_RM")
            NAMA.Text = rd.Item("NAMA_PAS")
            NAMA_KK.Text = rd.Item("NAMA_KK")
            NIK.Text = rd.Item("NIK")
            JK.Text = rd.Item("JK")
            PEKERJAAN.Text = rd.Item("PEKERJAAN")
            TGL_LAHIR.Text = rd.Item("TANGGAL_LAHIR")
            AGAMA.Text = rd.Item("AGAMA")
            ALAMAT.Text = rd.Item("ALAMAT")
            BIAYA.Text = rd.Item("KEPERSERTAAN")
            NO_BPJS.Text = rd.Item("BPJS")
            ANAMNESIS.Text = rd.Item("anamnesis")
            TDARAH.Text = rd.Item("tekanan_darah")
            NADI.Text = rd.Item("nadi")
            BB.Text = rd.Item("berat")
            TB.Text = rd.Item("tinggi")
            SB.Text = rd.Item("suhu")
            ALERGI.Text = rd.Item("alergi")
            ICD.ReadOnly = True
            D_BIDAN.Text = rd.Item("diag_bidan")
            DOSIS.ReadOnly = True
            KIE.Text = rd.Item("KIE")
            GIZI.Text = rd.Item("asupan_gizi")
            TINDAKAN.Text = rd.Item("tindakan")
            KASUS.Text = rd.Item("jenis_kasus")
            RUJUK.Text = rd.Item("rujuk")
            RS.Text = rd.Item("rumah_sakit")
            TDARAH.ReadOnly = True
            NADI.ReadOnly = True
            BB.ReadOnly = True
            TB.ReadOnly = True
            SB.ReadOnly = True
            rd.Close()

            Dim table1 As New DataTable()
            adapter = New MySqlDataAdapter("select a.obat, a.dosis from detail_obatkia a join poli_kia b where a.id_KIA = '" & id & "' and b.id_KIA = a.id_KIA", conn)
            adapter.Fill(table1)
            For Each row As DataRow In table1.Rows
                varobat.Add(row.Item("obat"))
                vardosis.Add(row.Item("dosis"))
            Next
            adapter.Dispose()
            Dim table2 As New DataTable()
            adapter = New MySqlDataAdapter("select a.diag_idc from detail_idc a join poli_kia b where a.id_KIA = '" & id & "' and b.id_KIA = a.id_KIA", conn)
            adapter.Fill(table2)
            For Each row As DataRow In table1.Rows
                diagicd.Add(row.Item("obat"))

            Next
            adapter.Dispose()

            SIMPAN.Enabled = False
            EDIT.Enabled = True
            Button1.Enabled = False
            Button2.Enabled = False
        Else
            rd.Close()
            cmd = New MySqlCommand("select * from pasien a join pelayanan b where a.NO_RM = b.NO_RM and a.NO_RM = @norm and b.TGL = '" & session_TGL & "'", conn)
            cmd.Parameters.Add("norm", MySqlDbType.VarChar).Value = session_NO_RM
            rd = cmd.ExecuteReader
            rd.Read()

            noreg = rd.Item("NOREG")
            NO_RM.Text = rd.Item("NO_RM")
            NAMA.Text = rd.Item("NAMA_PAS")
            NAMA_KK.Text = rd.Item("NAMA_KK")
            NIK.Text = rd.Item("NIK")
            JK.Text = rd.Item("JK")
            PEKERJAAN.Text = rd.Item("PEKERJAAN")
            TGL_LAHIR.Text = rd.Item("TANGGAL_LAHIR")
            AGAMA.Text = rd.Item("AGAMA")
            ALAMAT.Text = rd.Item("ALAMAT")
            BIAYA.Text = rd.Item("KEPERSERTAAN")
            NO_BPJS.Text = rd.Item("BPJS")

            SIMPAN.Enabled = True
            EDIT.Enabled = False
            rd.Close()

        End If
    End Sub

    Sub isi_obat()
        Call koneksi()
        cmd = New MySqlCommand("Select * From obat", conn)
        adapter = New MySqlDataAdapter(cmd)
        ds = New DataSet()
        adapter.Fill(ds)
        OBAT.DataSource = ds.Tables(0)
        OBAT.DisplayMember = "nama_obat"
    End Sub

    Private Sub SIMPAN_Click(sender As Object, e As EventArgs) Handles SIMPAN.Click
        rd.Close()
        Try
            Call newID()
            cmd = New MySqlCommand("insert into poli_kia values ('" & id_kia & "', '" & noreg & "','" & ANAMNESIS.Text & "', '" & TDARAH.Text & "','" & NADI.Text & "', '" & BB.Text & "','" & TB.Text & "', '" & SB.Text & "','" & ALERGI.Text & "', '" & D_BIDAN.Text & "', '" & KIE.Text & "','" & GIZI.Text & "', '" & TINDAKAN.Text & "','" & KASUS.Text & "', '" & RUJUK.Text & "','" & RS.Text & "')", conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            For i As Integer = 0 To vardosis.LongCount() - 1
                cmd = New MySqlCommand("insert into detail_obatkia values ('" & id_kia & "', '" & varobat(i) & "', '" & vardosis(i) & "')", conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Next
            For i As Integer = 0 To diagicd.LongCount() - 1
                cmd = New MySqlCommand("insert into detail_idc values ('" & id_kia & "', '" & diagicd(i) & "')", conn)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            Next
            MessageBox.Show("Data Berhasil Disimpan")
                SIMPAN.Enabled = False
            EDIT.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub newID()
        Call koneksi()
        cmd = New MySqlCommand("select * from poli_kia order by id_KIA desc", conn)
        Dim baca As MySqlDataReader
        baca = cmd.ExecuteReader
        baca.Read()
        If Not baca.HasRows Then
            id_kia = "KIA" + "0001"
        Else
            id_kia = Val(Microsoft.VisualBasic.Mid(baca.Item("id_KIA").ToString, 5, 4)) + 1
            If Len(id_kia) = 1 Then
                id_kia = "KIA000" & id_kia & ""
            ElseIf Len(id_kia) = 2 Then
                id_kia = "KIA00" & id_kia & ""
            ElseIf Len(id_kia) = 3 Then
                id_kia = "KIA0" & id_kia & ""
            End If
        End If
        baca.Close()
    End Sub

    Private Sub EDIT_Click(sender As Object, e As EventArgs) Handles EDIT.Click
        rd.Close()
        Try
            cmd = New MySqlCommand("UPDATE `pasien` SET `BPJS`='" & NO_BPJS.Text & "',`NAMA_KK`='" & NAMA_KK.Text & "',`NIK`='" & NIK.Text & "',`NAMA_PAS`='" & NAMA.Text & "',`ALAMAT`='" & ALAMAT.Text & "',`PEKERJAAN`='" & PEKERJAAN.Text & "', `AGAMA`='" & AGAMA.Text & "',`JK`= '" & JK.Text & "',`KEPERSERTAAN`= '" & BIAYA.Text & "' WHERE `NO_RM`= '" & NO_RM.Text & "'", conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cmd = New MySqlCommand("UPDATE `poli_kia` SET anamnesis = '" & ANAMNESIS.Text & "', tekanan_darah = '" & TDARAH.Text & "', nadi = '" & NADI.Text & "', berat = '" & BB.Text & "', tinggi = '" & TB.Text & "', suhu = '" & SB.Text & "', alergi = '" & ALERGI.Text & "', diag_bidan = '" & D_BIDAN.Text & "', KIE = '" & KIE.Text & "', asupan_gizi = '" & GIZI.Text & "', tindakan = '" & TINDAKAN.Text & "', jenis_kasus = '" & KASUS.Text & "', rujuk = '" & RUJUK.Text & "', rumah_sakit = '" & RS.Text & "' WHERE `NO_RM`= '" & NO_RM.Text & "'", conn)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            MessageBox.Show("Data Berhasil Disimpan")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ICD.Text = "" Then
            MessageBox.Show("Diagnosa ICD 10 tidak boleh kosong")
        Else
            diagicd.Add(ICD.Text)
            ICD.Clear()
        End If

    End Sub

    Private Sub FormulirRekamMedis_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        diagicd.Clear()
        varobat.Clear()
        vardosis.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If OBAT.Text = "" Or DOSIS.Text = "" Then
            MessageBox.Show("Obat dan Dosis tidak boleh kosong")
        Else
            varobat.Add(OBAT.Text)
            vardosis.Add(DOSIS.Text)
            OBAT.Text = String.Empty
            DOSIS.Clear()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim L As New Detail()
        L.session = "obat"
        L.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim L As New Detail()
        L.session = "icd"
        L.Show()
    End Sub

    Private Sub CETAK_Click(sender As Object, e As EventArgs) Handles CETAK.Click
        Dim pr As New printRM()
        pr.id = id
        pr.Show()
    End Sub

    Private Sub TAMBAH_Click(sender As Object, e As EventArgs) Handles TAMBAH.Click
        Rkp.Text = id
    End Sub
End Class