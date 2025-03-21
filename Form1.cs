using MySql.Data.MySqlClient;
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
            
        }



    }
}
