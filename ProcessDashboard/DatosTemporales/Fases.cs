namespace ProcessDashboard.DatosTemporales
{
    public class Fases : BaseTablas
    {
        public string Fase { get; set; }
        public float EstMin { get; set; }
        public float EstDefIny { get; set; }
        public float EstDefRem { get; set; }
        public float EstYield { get; set; }
        public float ResMin { get; set; }
        public float ResDefIny { get; set; }
        public float ResDefRem { get; set; }
        public float ResYield { get; set; }

        public override string ToString() =>
            $@"Programa: {Programa}, Fase: {Fase}, EstMin: {EstMin}, EstDefIny: {EstDefIny}, EstDefRem: {EstDefRem}, EstYield: {EstYield}
            ResMin: {ResMin}, ResDefIny: {ResDefIny}, ResDefRem: {ResDefRem}, ResYield: {ResYield}";
    }
}
