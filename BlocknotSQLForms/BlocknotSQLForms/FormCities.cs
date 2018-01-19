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
    public partial class FormCities : Form
    {
        BindingList<City> cities = new BindingList<City>();
        public FormCities()
        {
            InitializeComponent();

            this.Load += FormCities_Load;

        }

        private void FormCities_Load(object sender, EventArgs e)
        {
            Context<City> allCities = new Context<City>();
            cities = (BindingList<City>)allCities.LoadFromDB();
            this.dgvCities.DataSource = cities;
        }

        private void button_add_city_Click(object sender, EventArgs e)
        {
            cities.Add(new City()
            {
                Name = txtNewCity.Text,
                CountryID = 1

            });
                        
            Context<City> city = new Context<City>();

            city.InsertToDB(new City()
            {
                Name = txtNewCity.Text,
                CountryID = 1

            });
        }
    }
}
