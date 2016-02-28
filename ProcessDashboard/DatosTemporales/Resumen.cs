namespace ProcessDashboard.DatosTemporales
{
    public class Resumen : BaseTablas
    {
        public float EstLOC { get; set; }
        public float EstMin { get; set; }
        public float EstDef { get; set; }
        public float EstProd { get; set; }
        public float EstYield { get; set; }
        public float ResLOC { get; set; }
        public float ResMin { get; set; }
        public float ResDef { get; set; }
        public float ResProd { get; set; }
        public float ResYield { get; set; }
        public float ResPQI { get; set; }
        public float ResDLDR { get; set; }
        public float ResCR { get; set; }
        public float ResVelRev { get; set; }
        public float ResCPI { get; set; }
        public float ResDensDef { get; set; }
        public float ResDefCyT { get; set; }
        public float ResAFR { get; set; }
        public float ResDHDLDR { get; set; }
        public float ResDHCR { get; set; }
        public float ResDHUT { get; set; }
        public float ResDRLDLDR { get; set; }
        public float ResDRLCR { get; set; }

        public override string ToString() =>
            $@"Programa: {Programa}, EstLOC: {EstLOC}, EstMin: {EstMin}, EstDef: {EstDef}, EstProd: {EstProd}, EstYield: {EstYield}, 
            ResLOC: {ResLOC}, ResMin: {ResMin}, ResDef: {ResDef}, ResProd: {ResProd}, ResYield: {ResYield}, ResPQI: {ResPQI}, 
            ResDLDR: {ResDLDR}, ResCR: {ResCR}, ResVelRev: {ResVelRev}, ResCPI: {ResCPI}, ResDensDef: {ResDensDef}, ResDefCyT: {ResDefCyT}, 
            ResAFR: {ResAFR}, ResDHDLDR: {ResDHDLDR}, ResDHCR: {ResDHCR}, ResDHUT: {ResDHUT} ,ResDRLDLDR: {ResDRLDLDR} ,ResDRLCR: {ResDRLCR}";
    }
}
