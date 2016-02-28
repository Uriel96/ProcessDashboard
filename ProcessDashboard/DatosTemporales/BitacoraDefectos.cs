using System;

namespace ProcessDashboard.DatosTemporales
{
    public class BitacoraDefectos : BaseTablas
    {
        public int? IDDefecto { get; set; }
        public DateTime? Fecha { get; set; }
        public string FaseInyectado { get; set; }
        public string FaseRemovido { get; set; }
        public string TipoDefecto { get; set; }
        public float FixTime { get; set; }
        public int FixCount { get; set; } = 1;
        public int? FixDefect { get; set; }
        public string Descripcion { get; set; }

        /*public override string ToString() =>
            $@"Programa: {Programa}, IDDefecto: {IDDefecto}, Fecha: {Fecha}, FaseInyectado: {FaseInyectado}, 
            TipoDefecto: {TipoDefecto}, FixTime: {FixTime}, FixCount: {FixCount}, FixDefect: {FixDefect}, Descripcion: {Descripcion}";*/
    }
}
