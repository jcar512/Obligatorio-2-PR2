namespace Obligatorio2.Utils
    {
    public class Utils
        {
        public static List<DateTime> CalcularFechas(DateTime fechaInicio, DateTime fechaFin)
            {
            List<DateTime> rangoDeFechas = new List<DateTime>();

            for (DateTime fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
                {
                rangoDeFechas.Add(fecha);
                }

            return rangoDeFechas;
            }
        }
    }