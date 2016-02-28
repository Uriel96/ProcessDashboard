using System;

namespace ProcessDashboard.DatosTemporales
{
    public class BitacoraTiempo : BaseTablas
    {
        public string Fase { get; set; }
        public DateTime? Inicio { get; set; } 
        public DateTime? Fin { get; set; }
        public float Interrupcion { get; set; }      
        public float Delta { get; set; }
        public string Comentario { get; set; }

        public override string ToString() => 
            $@"Programa: {Programa}, Fase: {Fase}, Inicio: {Inicio}, Fin: {Fin},
            Interrupcion: {Interrupcion}, Delta: {Delta}, Comentario: {Comentario}";
    }
}
