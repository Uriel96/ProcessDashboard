using System;

namespace ProcessDashboard.DatosTemporales
{
    public class SET : BaseTablas
    {
        public string Nombre { get; set; }
        public string TipoParte { get; set; }
        public float EstBase { get; set; }
        public float EstDeleted { get; set; }
        public float EstModified { get; set; }
        public float EstAdded { get; set; }
        public float EstSize { get; set; }
        public string EstTipo { get; set; }
        public int EstItems { get; set; }    
        public string EstRelSize { get; set; }
        public int EstNR { get; set; }
        public float ActBase { get; set; }
        public float ActDeleted { get; set; } 
        public float ActModified { get; set; }
        public float ActAdded { get; set; }
        public float ActSize { get; set; } 
        public int ActItems { get; set; }  
        public int ActNR { get; set; }   

        /*public override string ToString() => 
            $@"Programa: {Programa} Nombre: {Nombre} TipoParte: {TipoParte}
            {Environment.NewLine}EstBase: {EstBase} EstDeleted: {EstDeleted} EstModified: {EstModified} EstAdded: {EstAdded}
            EstSize: {EstSize} EstTipo: {EstTipo} EstItems: {EstItems} EstRelSize: {EstRelSize} EstNR: {EstNR}
            {Environment.NewLine}Actbase: {ActBase} ActDeleted: {ActDeleted} ActModified: {ActModified} ActAdded: {ActAdded}
            ActSize: {ActSize} ActItems: {ActItems} ActNR: {ActNR}";*/
    }
}
