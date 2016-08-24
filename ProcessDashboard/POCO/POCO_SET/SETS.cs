using ProcessDashboard.Accessors;
using ProcessDashboard.HelperClasses;
using ProcessDashboard.POCO.POCO_SET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.POCO.POCO_SET {
    /// <summary>
    /// Class that generates and holds all the Size Estimated Templates.
    /// </summary>
    public class SETS : List<SET> {
        #region Properties        
        /// <summary>
        /// The Total Actual Base Lines of Code on a Program.
        /// </summary>
        public float TotalActBase { get; set; }
        /// <summary>
        /// The Total Actual Deleted Lines of Code on a Program.
        /// </summary>
        public float TotalActDeleted { get; set; }
        /// <summary>
        /// The Total Actual Deleted Lines of Code on a Program.
        /// </summary>
        public float TotalActModified { get; set; }
        /// <summary>
        /// The Total Actual Added Lines of Code on a Program.
        /// </summary>
        public float TotalActAdded { get; set; }
        /// <summary>
        /// The Total Actual Reused Lines of Code on a Program.
        /// </summary>
        public float TotalActReused { get; set; }
        /// <summary>
        /// The Total Actual Added and Modified Lines of Code on a Program.
        /// </summary>
        public float TotalActAM { get; set; }
        /// <summary>
        /// The Total Actual Lines of Code on a Program.
        /// </summary>
        public float TotalAct { get; set; }
        #endregion

        #region Fields        
        /// <summary>
        /// The Data that will be taken.
        /// </summary>
        private Data data;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The Data.</param>
        public SETS(Data data) {
            this.data = data;
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Populates the Information based on data.
        /// </summary>
        /// <param name="initializationAction">The action that initializes set base properties.</param>
        public void populate(Action<SET> initializationAction) {
            // Gets Base, New and Reused SETs and Joins them
            var baseSET = populate<BaseSET>("Base_Parts_List", initializationAction);
            var newSET = populate<NewSET>("New_Objects_List", initializationAction);
            var reusedSET = populate<ReusedSET>("Reused_Objects_List", initializationAction);
            AddRange(baseSET.Concat(newSET).Concat(reusedSET));

            this.parse(sets => sets.TotalAct, data, $"Total LOC");
            TotalActAdded = TotalAct - TotalActBase - TotalActReused + TotalActDeleted;
            TotalActAM = TotalActAdded + TotalActModified;
        }

        /// <summary>
        /// Populates a Type of SET Information based on data.
        /// </summary>
        /// <typeparam name="T">The Type of SET (Base, New, Reused).</typeparam>
        /// <param name="name">The name of the Data representing the Type of SET.</param>
        /// <param name="initilizationAction">The action that initializes set base properties.</param>
        /// <returns>Returns all the Collection of SETs Populated.</returns>
        public IEnumerable<SET> populate<T>(string name, Action<SET> initilizationAction) where T : SET, new() {
            //Checks if it the Name Exists in Data
            if (!data.ContainsKey(name))
                yield break;

            //Get Values from the Name
            string[] indexes = data[name].ToString().Split('');
            foreach (var strIndex in indexes) {
                int index;
                if (!int.TryParse(strIndex, out index))
                    continue;
                //Creates a new SET
                T set = new T();
                set.initialize(data, index);
                initilizationAction.Invoke(set);
                //Populates Data in SET
                if (set.pass()) {
                    set.populate();
                    if (set.Type == SETType.Base) {
                        TotalActBase += set.ActBase;
                        TotalActDeleted += set.ActDeleted;
                        TotalActModified += set.ActModified;
                    }
                    if (set.Type == SETType.Reused) {
                        TotalActReused += set.ActBase;
                    }
                    yield return set;
                }
            }
        }
        #endregion
    }
}
