using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar los namespaces requeridos
using System.Data.SqlClient;
using System.Configuration;

namespace _26___Reservaciones
{
    // Crear una variable que mantenga los valores para los estados de la habitación
    public enum EstadosHabitacion
    {
        Ocupada = 'O',
        Disponible = 'D',
        Mantenimiento = 'M',
        FueraServicio = 'F'
    }

    class Habitacion
    {
        // Variables miembro
        private static string connectionString = ConfigurationManager.ConnectionStrings["_26___Reservaciones.Properties.Settings.ReservacionesConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        // Propiedades
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public int Numero { get; set; }

        public EstadosHabitacion Estado { get; set; }

        // Constructores
        public Habitacion() { }

        public Habitacion(string descripcion, int numero, EstadosHabitacion estado)
        {
            Descripcion = descripcion;
            Numero = numero;
            Estado = estado;
        }
    }
}
