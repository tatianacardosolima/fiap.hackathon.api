namespace Fiap.Hackathon.Medicos.Domain.Helpers
{
    public static class GeoLocalizacaoHelper
    {
        private const double EarthRadiusKm = 6371.0; // Raio médio da Terra em km

        public static double CalcularDistancia(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = GrausParaRadianos(lat2 - lat1);
            double dLon = GrausParaRadianos(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(GrausParaRadianos(lat1)) * Math.Cos(GrausParaRadianos(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (EarthRadiusKm * c)/1000; // Retorna distância em km
        }

        private static double GrausParaRadianos(double graus) => graus * Math.PI / 180;
    }
}
