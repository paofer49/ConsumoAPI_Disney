using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsumoAPI_Disney.Controller;
using ConsumoAPI_Disney.Model;

namespace ConsumoAPI_Disney
{
    public partial class Form1 : Form
    {
        ControllerPersonajes control = new ControllerPersonajes();
        public Form1()
        {
            InitializeComponent();
            ConfigureDataGridView();
            CargarPersonajes();
        }

        private async void CargarPersonajes()
        {
            try
            {
                var characters = await control.MostrarPersonajes();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = characters;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar personajes: {ex.Message}");
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "_id",
                HeaderText = "ID"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "name",
                HeaderText = "Nombre"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FilmsAsString",
                HeaderText = "Películas"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TvShowsAsString",
                HeaderText = "Programas de TV"
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "imageUrl",
                HeaderText = "URL de Imagen"
            });
        }

        private async void BtnBuscar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(TxtIdBuscar.Text, out var id))
            {
                MessageBox.Show("Por favor, ingresa un ID válido.");
                return;
            }

            try
            {
                var character = await control.GetCharacterById(id);
                if (character != null)
                {
                    // Mostrar la imagen en el PictureBox
                    pictureBox1.Load(character.imageUrl);
                    // Crear una lista temporal para mostrar en el DataGridView
                    var characterList = new List<Personajes> { character };
                    // Establecer la lista como fuente de datos del DataGridView
                    dataGridView1.DataSource = characterList;
                }
                else
                {
                    MessageBox.Show("Personaje no encontrado.");
                    dataGridView1.DataSource = null;
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar personaje: {ex.Message}");
            }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            CargarPersonajes();
        }
    }
}
