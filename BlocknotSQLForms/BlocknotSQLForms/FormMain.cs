using BlocknotSQLForms;
using BlocknotSQLForms.Enitites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlocknotSQLForms
{
    public partial class FormMain : Form
    {
        ICollection<Record> records;
        public FormMain()
        {
            InitializeComponent();
            records = new BindingList<Record>();
        }

        private void tsbCountries_Click(object sender, EventArgs e)
        {
            FormCountries countries = new FormCountries();
            countries.ShowDialog();
        }

        private void tsbCities_Click(object sender, EventArgs e)
        {
            FormCities cities = new FormCities();
           
            cities.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Context<Record> city = new Context<Record>();
            dgvRecord.DataSource = city.LoadFromDB(); 
        }
    }
}
