namespace preciosaludable.Tools.Geolocation
{
    using System;
    public class Location
    {
        public const int _RADIO_TIERRA = 6371;
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public Location(double latitud, double longitud){
            Latitud = latitud;
            Longitud = longitud;
        }

        public int GetDistancia(Location location){
            double _distancia = 0;
 
            try
            {
                double tmpLatitud = (location.Latitud - this.Latitud) * (Math.PI / 180);
                double tmpLongitud = (location.Longitud - this.Longitud) * (Math.PI / 180);
                double _a = Math.Sin(tmpLatitud / 2) * Math.Sin(tmpLatitud / 2) + Math.Cos(this.Latitud * (Math.PI / 180)) * Math.Cos(location.Latitud * (Math.PI / 180)) * Math.Sin(tmpLongitud / 2) * Math.Sin(tmpLongitud / 2);
                double _c = 2 * Math.Atan2(Math.Sqrt(_a), Math.Sqrt(1 - _a));
 
                _distancia = (_RADIO_TIERRA * _c) * 1000;
            }
            catch (Exception)
            {
                _distancia = -1;
            }
 
            return (int)Math.Round(_distancia, MidpointRounding.AwayFromZero);
        }

    }
}