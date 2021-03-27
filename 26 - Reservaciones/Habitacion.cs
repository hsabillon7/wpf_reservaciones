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

        // Métodos
        private string ObtenerEstado(EstadosHabitacion estado)
        {
            switch (estado)
            {
                case EstadosHabitacion.Ocupada:
                    return "OCUPADA";
                case EstadosHabitacion.Disponible:
                    return "DISPONIBLE";
                case EstadosHabitacion.Mantenimiento:
                    return "MANTENIMIENTO";
                case EstadosHabitacion.FueraServicio:
                    return "FUERADESERVICIO";
                default:
                    return "DISPONIBLE";
            }
        }

        public void CrearHabitacion(Habitacion habitacion)
        {
            try
            {
                // Query de inserción
                string query = @"INSERT INTO Habitaciones.Habitacion (descripcion, numero, estado)
                                 VALUES (@descripcion, @numero, @estado)";

                // Establecer la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@descripcion", habitacion.Descripcion);
                sqlCommand.Parameters.AddWithValue("@numero", habitacion.Numero);
                sqlCommand.Parameters.AddWithValue("@estado", ObtenerEstado(habitacion.Estado));

                // Ejecutar el comando de inserción
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }
    }
}
