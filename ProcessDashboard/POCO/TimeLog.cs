using ProcessDashboard.Accessors;
using ProcessDashboard.HelperClasses;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.POCO {
    /// <summary>
    /// Class that hols the Time Log Information.
    /// </summary>
    public class TimeLog : DashboardTable {
        #region Properties        
        /// <summary>
        /// A unique Identifier for the Time Log Information.
        /// </summary>
        [Key]
        public override int ID { get; set; }
        /// <summary>
        /// The Type of Phase.
        /// </summary>
        public PhaseType Type { get; set; }
        /// <summary>
        /// The Started Date.
        /// </summary>
        public DateTime? Started { get; set; }
        /// <summary>
        /// The Finished Date.
        /// </summary>
        public DateTime? Ended { get; set; }
        /// <summary>
        /// The Space between the Start Date and the End of the previous Time Log.
        /// </summary>
        public float Space { get; set; }
        /// <summary>
        /// The Interruption Time (how much interruption while working).
        /// </summary>
        public float Interrupt { get; set; }
        /// <summary>
        /// The Time took to finish the Log.
        /// </summary>
        public float Delta { get; set; }
        /// <summary>
        /// The Interruption Comment.
        /// </summary>
        public string Comment { get; set; }
        #endregion

        #region Fields        
        /// <summary>
        /// The data that will be taken.
        /// </summary>
        [NotMapped]
        private Data data;
        /// <summary>
        /// The path of the program and phase where the log was created.
        /// </summary>
        [NotMapped]
        private string[] path;
        /// <summary>
        /// The previous time log.
        /// </summary>
        [NotMapped]
        private TimeLog previousTimeLog;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes the Data needed to process the Information..
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="previousTimeLog">The previous time log.</param>
        public void initialize(Data data, TimeLog previousTimeLog) {
            this.data = data;
            this.previousTimeLog = previousTimeLog;
            if (data.ContainsKey("path") && data["path"] != null) {
                //Contains the Program and the Phase
                path = data["path"].Split('/');
            }
            if (path == null)
                return;
            setProgram();
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Processes and Sets all the Information needed (populates the Information).
        /// </summary>
        public override void populate() {
            if (previousTimeLog != null && Program != previousTimeLog.Program) {
                previousTimeLog = null;
            }
            setPhase();
            setInterrupt();
            setDelta();
            setComment();
            setStarted();
            setSpace();
            setEnded();
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Sets the program based on the path.
        /// </summary>
        public void setProgram() {
            Program = path[path.Length - 2].Trim().getProgram();
        }
        /// <summary>
        /// Sets the phase.
        /// </summary>
        public void setPhase() {
            Type = path[path.Length - 1].Trim().getPhase();
        }
        /// <summary>
        /// Sets the interruption time.
        /// </summary>
        public void setInterrupt() {
            this.parse(x => x.Interrupt, data, "interrupt");
        }
        /// <summary>
        /// Sets the delta time.
        /// </summary>
        public void setDelta() {
            this.parse(x => x.Delta, data, "delta");
        }
        /// <summary>
        /// Sets the comment.
        /// </summary>
        public void setComment() {
            this.parse(x => x.Comment, data, "comment");
        }
        /// <summary>
        /// Sets the started date.
        /// </summary>
        public void setStarted() {
            Started = defaultDate.AddMilliseconds(data["start"]?.Trim().Substring(1).parse<double>() ?? 0).ToLocalTime();
        }
        /// <summary>
        /// Sets the ended date.
        /// </summary>
        public void setEnded() {
            Ended = Started?.AddMinutes(Delta + Interrupt);
        }
        /// <summary>
        /// Sets the space between started date and previous ended date.
        /// </summary>
        public void setSpace() {
            if (Started != null && previousTimeLog != null && previousTimeLog.Ended != null) {
                Space = (float)((DateTime)Started).Subtract((DateTime)previousTimeLog.Ended).TotalMinutes;
            }
        }
        #endregion
    }
}
