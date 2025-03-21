﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDSederhana1
{
    public partial class Form1 : Form 
    {
        static string connectionString = string.Format(
    "Server=127.0.0.1;port=1433; database=Organisasiahasiswa; UID=root; password=admin123");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) //ini berfungsi untuk memeproses data 
        {
            LoadData();
        }

        private void ClearForm()
        {
            txtNIM.Clear();
            txtNama.Clear();
            txtEmail.Clear();
            txtTelepon.Clear();
            txtAlamat.Clear();

            //fokus kembali kenim agar user siap masukkan data baru
            txtNIM.Focus();
        }

        private void LoadData()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "Select NIM, Nama, Email, Telepon, " +
                    "Alamat from Mahasiswa";
                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMahasiswa.AutoGenerateColumns = true;
                dgvMahasiswa.DataSource = dt;

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " +
                    ex.Message, "Kesalahan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                if (txtNIM.Text == "" || txtNama.Text == "" || txtEmail.Text == ""
                    || txtTelepon.Text == "" || txtAlamat.Text == "")
                {
                    MessageBox.Show(
                        "Harap isi semua data!", "Peringatan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                        );
                    return;
                }
                conn.Open();
                string query = "insert into mahasiswa (NIM, Nama, Email, Telepon, " +
                    "Alamat) values (@NIM, @Nama, @Email, @Telepon, " +
                    "@Alamat)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text);
                cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show(
                        "Data berhasil ditambahkan!", "Sukses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(
                        "Data tidak berhasil ditambahkan!", "Kesalahan",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " +
                    ex.Message, "Kesalahan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvMahasiswa.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show(
                    "Yakin ingin menghapus data ini?", "Konfirmasi",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    MySqlConnection conn = new MySqlConnection(connectionString);

                    try
                    {
                        string nim = dgvMahasiswa.SelectedRows[0].Cells["NIM"].Value.ToString();
                        conn.Open();
                        string query = "delete from mahasiswa " +
                            "where NIM = @NIM";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@NIM", nim);
                        int rowAffected = cmd.ExecuteNonQuery();
                        if (rowAffected > 0)
                        {
                            MessageBox.Show(
                                "Data berhasil dihapus!", "Sukses",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Data tidak ditemukan atau gagal dihapus!", "Kesalahan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Error: " +
                            ex.Message, "Kesalahan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();

            //Debugging: cek jumlah kolom dan baris
            MessageBox.Show(
                $"jumlah kolom:{dgvMahasiswa.ColumnCount}\n" +
                $"Jumlah Baris: {dgvMahasiswa.RowCount}",
                "Debugging DataGridView",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void dgvMahasiswa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

    }
}
