using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.DatosTemporales
{
    public class PROBE : BaseTablas
    {
        public float TamProxy { get; set; }
        public string TamMetodo { get; set; }
        public float TamPlan { get; set; }
        public float TamR2 { get; set; }
        public float TamB0 { get; set; }
        public float TamB1 { get; set; }
        public float TamRango { get; set; }
        public float TamUPI { get; set; }
        public float TamLPI { get; set; }
        public string TieMetodo { get; set; }
        public float TiePlan { get; set; }
        public float TieR2 { get; set; }
        public float TieB0 { get; set; }
        public float TieB1 { get; set; }
        public float TieRango { get; set; }
        public float TieUPI { get; set; }
        public float TieLPI { get; set; }

        public override string ToString() =>
            $@"Programa: {Programa}, TamProxy: {TamProxy}, TamMetodo: {TamMetodo}, TamPlan: {TamPlan}, TamR2: {TamR2}, TamB0: {TamB0}, 
            TamB1: {TamB1}, TamRango: {TamRango}, TamUPI: {TamUPI}, TamLPI: {TamLPI}, TieMetodo: {TieMetodo}, TiePlan: {TiePlan}, 
            TieR2: {TieR2}, TieB0: {TieB0}, TieB1: {TieB1}, TieRango: {TieRango}, TieUPI: {TieUPI}, TieLPI: {TieLPI}";
    }
}
