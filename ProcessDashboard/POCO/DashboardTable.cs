using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using static ProcessDashboard.HelperClasses.DataUtil;
using ProcessDashboard.Accessors;
using System.Collections.Generic;
using System;
using ProcessDashboard.HelperClasses;
using ProcessDashboard.GenerationProcess;

namespace ProcessDashboard.POCO {
    /// <summary>
    /// Class that holds the Base Information other Classes must have.
    /// </summary>
    public abstract class DashboardTable : IDataConvertor {
        #region Properties        
        /// <summary>
        /// A unique Identifier for the Information.
        /// </summary>
        public abstract int ID { get; set; }
        /// <summary>
        /// The Student that Information corresponds to.
        /// </summary>
        public string Student { get; set; }
        /// <summary>
        /// The Program that Information of the Student corresponds to.
        /// </summary>
        public Program Program { get; set; }
        /// <summary>
        /// The Version that Information of the Student corresponds to.
        /// </summary>
        public int Version { get; set; }
        #endregion

        #region Main Methods
        /// <summary>
        /// Processes and Sets all the Information needed (populates the Information).
        /// </summary>
        public abstract void populate();
        #endregion

        #region Helper Methods        
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() {
            var result = "";
            var counter = 0;
            var numProperties = GetType().GetProperties().Length;

            //Gets the Base Properties into a String
            PropertyInfo[] baseProperties = GetType().BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in baseProperties) {
                string comma = counter < numProperties - 1 ? ", " : "";
                result += $"{ property.Name }: { property.GetValue(this) }{ comma }";
                counter++;
            }

            //Adds the Derived Properties into the String
            PropertyInfo[] derivedProperties = GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in derivedProperties) {
                string comma = counter < numProperties - 1 ? ", " : "";
                result += $"{ property.Name }: { property.GetValue(this) }{ comma }";
                counter++;
            }
            return result;
        }
        #endregion
    }
}
