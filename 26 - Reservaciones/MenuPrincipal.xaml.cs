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
    /// Interaction logic for MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal(string usuario)
        {
            InitializeComponent();

            // Mostrar el nombre en el formulario
            lbUsuario.Content = string.Format("¡Hola {0}! ¿Qué deseas realizar?", usuario);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            // Retornar el usuario al formulario de inicio de sesión
            IniciarSesion iniciarSesion = new IniciarSesion();
            iniciarSesion.Show();
            Close();
        }
    }
}
