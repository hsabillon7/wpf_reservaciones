using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _26___Reservaciones
{
    /// <summary>
    /// Interaction logic for Habitaciones.xaml
    /// </summary>
    public partial class Habitaciones : Window
    {
        // Variables miembro
        private Habitacion habitacion = new Habitacion();
        private List<Habitacion> habitaciones;

        public Habitaciones()
        {
            InitializeComponent();

            // Llenar el combobox de estado de la habitación
            cmbEstado.ItemsSource = Enum.GetValues(typeof(EstadosHabitacion));

            // Llenar el listbox de habitaciones
            ObtenerHabitaciones();
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Text = string.Empty;
            txtNumeroHabitacion.Text = string.Empty;
            cmbEstado.SelectedValue = null;
        }

        private void ObtenerValoresFormulario()
        {
            habitacion.Descripcion = txtDescripcion.Text;
            habitacion.Numero = Convert.ToInt32(txtNumeroHabitacion.Text);
            habitacion.Estado = (EstadosHabitacion)cmbEstado.SelectedValue;
            habitacion.Id = Convert.ToInt32(lbHabitaciones.SelectedValue);
        }

        private void ObtenerHabitaciones()
        {
            habitaciones = habitacion.MostrarHabitaciones();
            lbHabitaciones.DisplayMemberPath = "Descripcion";
            lbHabitaciones.SelectedValuePath = "Id";
            lbHabitaciones.ItemsSource = habitaciones;
        }

        private void ValoresFormularioDesdeObjeto()
        {
            txtDescripcion.Text = habitacion.Descripcion;
            txtNumeroHabitacion.Text = habitacion.Numero.ToString();
            cmbEstado.SelectedValue = habitacion.Estado;
        }

        private void OcultarBotonesOperaciones(Visibility ocultar)
        {
            btnAgregar.Visibility = ocultar;
            btnModificar.Visibility = ocultar;
            btnEliminar.Visibility = ocultar;
            btnRegresar.Visibility = ocultar;
        }

        private bool VerificarValores()
        {
            if (txtDescripcion.Text == string.Empty || txtNumeroHabitacion.Text == string.Empty)
            {
                MessageBox.Show("Por favor ingresa todos los valores en las cajas de texto");
                return false;
            }
            else if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Por favor selecciona el estado de la habitación");
                return false;
            }

            return true;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que se ingresaron los valores requeridos
            if (VerificarValores())
            {
                try
                {
                    // Obtener los valores para la habitación
                    ObtenerValoresFormulario();

                    // Insertar los datos de la habitación
                    habitacion.CrearHabitacion(habitacion);

                    // Mensaje de inserción exitosa
                    MessageBox.Show("¡Datos insertados correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de insertar la habitación...");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    LimpiarFormulario();
                    ObtenerHabitaciones();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (lbHabitaciones.SelectedValue == null)
                MessageBox.Show("Por favor selecciona una habitación desde el listado");
            else
            {
                try
                {
                    // Obtener la información de la habitación
                    habitacion = habitacion.BuscarHabitacion(Convert.ToInt32(lbHabitaciones.SelectedValue));

                    // Llenar los valores del formulario
                    ValoresFormularioDesdeObjeto();

                    // Ocultar los botones de operaciones CRUD
                    OcultarBotonesOperaciones(Visibility.Hidden);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al momento de modificar la habitación...");
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Mostrar los botones de operaciones CRUD
            OcultarBotonesOperaciones(Visibility.Visible);

            LimpiarFormulario();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarValores())
            {
                try
                {
                    // Obtener los valores para la habitación desde el formulario
                    ObtenerValoresFormulario();

                    // Actualizar los valores en la base de datos
                    habitacion.ModificarHabitacion(habitacion);

                    // Mensaje de actualización realizada
                    MessageBox.Show("¡Habitación modificada correctamente!");

                    // Mostrar los botones de operaciones CRUD
                    OcultarBotonesOperaciones(Visibility.Visible);

                    // Limpiar el formulario
                    LimpiarFormulario();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al momento de actualizar la habitación...");
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Actualizar el listbox de habitaciones
                    ObtenerHabitaciones();
                }
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar el formulario
            this.Close();
        }
    }
}
