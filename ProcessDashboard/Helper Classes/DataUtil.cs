using Ionic.Zip;
using ProcessDashboard.POCO;
using ProcessDashboard.Accessors;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ProcessDashboard.POCO.POCO_SET;
using ProcessDashboard.GenerationProcess;

namespace ProcessDashboard.HelperClasses {
    /// <summary>
    /// Class that helps with Utilities the Process needs.
    /// </summary>
    public static class DataUtil {
        #region Fields        
        /// <summary>
        /// The default Date used to make calculations.
        /// </summary>
        public static DateTime defaultDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        /// <summary>
        /// The List of Programs as Strings. Matches with Program Enumeration Order.
        /// </summary>
        private static string[] Programs = new string[] {
            "Student Profile", "Program 1", "Program 2", "Program 3", "Program 4", "Program 5", "Program 6", "Program 7", "Report"
        };

        /// <summary>
        /// The List of Phases as Strings. Matches with Phases Types Enumeration Order.
        /// </summary>
        private static string[] PhaseTypes = new string[] {
            "Planning", "Design", "Design Review", "Code", "Code Review", "Compile", "Test", "Postmortem"
        };
        /// <summary>
        /// The List of SET Types as Strings. Matches with SET Types Enumeration Order.
        /// </summary>
        private static string[] SETTypes = { "Base", "New", "Reused" };
        #endregion

        #region Data        
        /// <summary>
        /// Checks if any of the inputs are Contained in the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="inputs">The inputs.</param>
        /// <returns>Returns true if a inputs is contained in data.</returns>
        public static bool exists(this Data data, params string[] inputs) {
            foreach (var input in inputs) {
                if (!data.ContainsKey(input))
                    continue;
                float value = 0;
                float.TryParse(data[input], out value);
                if (data[input] != "null" && data[input] != "N/A" && value != 0) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Parses the specified outer expression.
        /// </summary>
        /// <typeparam name="T">Type of Information.</typeparam>
        /// <typeparam name="M">Type of Property to be used.</typeparam>
        /// <param name="item">The Information Item.</param>
        /// <param name="outerProperty">The Property that will be used.</param>
        /// <param name="data">The data.</param>
        /// <param name="input">The input for the Column in Data.</param>
        /// <returns></returns>
        public static bool parse<T, M>(this T item, Expression<Func<T, M>> outerProperty, Data data, string input) {
            try {
                var expression = (MemberExpression)outerProperty.Body;
                var property = (PropertyInfo)expression.Member;
                Type type = Nullable.GetUnderlyingType(typeof(M)) ?? typeof(M);
                string value;
                if (!data.TryGetValue(input, out value) || string.IsNullOrEmpty(value) || value.ToLower() == "null" || value.ToLower() == "n/a")
                    return false;
                M newValue;
                if (type == typeof(int))
                    newValue = (M)Convert.ChangeType(Convert.ToDouble(value), type);
                else
                    newValue = (M)Convert.ChangeType(value, type);
                property.SetValue(item, newValue, null);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return true;
        }
        #endregion

        #region Files
        /// <summary>
        /// Gets the File from the Zip File and Creates a copy.
        /// </summary>
        /// <param name="zip">The Zip File from where the File to Access is.</param>
        /// <param name="directory">The directory where the Copied File is created.</param>
        /// <param name="fileName">The Name of the File to be Accessed.</param>
        /// <returns>Returns the path of the new File (copied File).</returns>
        /// <exception cref="FileNotFoundException">Thrown when file name cannot be found inside the zip File.</exception>
        public static string getFile(this ZipFile zip, string directory, string fileName) {
            //Checks if File Exists
            if (fileName != null && zip.ContainsEntry(fileName)) {
                zip[fileName].Extract(directory, ExtractExistingFileAction.OverwriteSilently);
                return $@"{directory}\{fileName}";
            } else {
                throw new FileNotFoundException($"Cannot find the file ({fileName}) in the directory ({directory}).");
            }
        }

        /// <summary>
        /// Determines whether the File is an Excel File.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        ///   <c>true</c> if the File is an Excel File; otherwise, <c>false</c>.
        /// </returns>
        public static bool isAnExcelFile(this string file) {
            string extension = Path.GetExtension(file).ToLower();
            return extension.Equals(".xlsx") || extension.Equals(".xls");
        }

        /// <summary>
        /// Determines whether the File has a specified prefix.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>
        ///     <c>true</c> if the File start with the prefix; otherwise, <c>false</c>.
        /// </returns>
        public static bool startsWithPrefix(this string file, string prefix) {
            return Path.GetFileName(file).ToLower().StartsWith(prefix.ToLower());
        }

        /// <summary>
        /// Gets the full path of a File within the project.
        /// </summary>
        /// <param name="relativeFile">The file Name.</param>
        /// <returns>Returns the complete path within the Project.</returns>
        public static string getProjectFile(this string relativeFile) {
            var fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(fullPath, relativeFile);
        }
        #endregion

        #region General
        /// <summary>
        /// Changes the String Value to a given Value Type.
        /// </summary>
        /// <typeparam name="T">The Type to convert.</typeparam>
        /// <param name="value">The value to be changed.</param>
        /// <returns>Returns the new Value.</returns>
        public static T parse<T>(this string value) {
            T newValue = default(T);
            try {
                newValue = (T)Convert.ChangeType(value, newValue.GetType());
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return newValue;
        }

        /// <summary>
        /// Divides to numbers and if the denominator is cero, then it will be replaced with another number.
        /// </summary>
        /// <param name="num">The numerator.</param>
        /// <param name="den">The denominator.</param>
        /// <param name="replacement">The replacement number.</param>
        /// <param name="rounding">The rounding precision to check.</param>
        /// <returns>Returns the result of the division.</returns>
        public static float div(float num, float den, float replacement = 0, int rounding = 0) {
            return (rounding != 0 ? (Math.Round(den, rounding) == 0) : (den == 0)) ? replacement : (num / den);
        }

        /// <summary>
        /// Divides to numbers and if the denominator or the numerator is cero, then it will be replaced with another number.
        /// </summary>
        /// <param name="num">The numerator.</param>
        /// <param name="den">The denominator.</param>
        /// <param name="replacementNum">The replacement for numerator when cero.</param>
        /// <param name="replacementDen">The replacement for denominator when cero.</param>
        /// <returns>Returns the result of the division.</returns>
        public static float divNum(float num, float den, float replacementNum = 0, float replacementDen = 0) {
            return (num == 0) ? replacementNum : ((den == 0) ? replacementDen : (num / den));
        }

        /// <summary>
        /// Returns an empty Enumerable if the source has no data.
        /// </summary>
        /// <typeparam name="T">Type of Data.</typeparam>
        /// <param name="source">The list of Data.</param>
        /// <returns>Returns the data or an empty enumerable.</returns>
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source) {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Gets the value from the Data and if not there gets default value.
        /// </summary>
        /// <typeparam name="T">the Type of Value to return.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key to retrieve from.</param>
        /// <returns>Returns the value or the default value.</returns>
        public static T getOrDefualt<T>(this Dictionary<string, T> data, string key) {
            return data.ContainsKey(key) ? data[key] : default(T);
        }
        #endregion

        #region Order        
        /// <summary>
        /// Orders Defect Logs by Program and Date.
        /// </summary>
        /// <param name="list">The List of Defect Logs.</param>
        /// <returns>Returns the New Ordered List of Defect Logs.</returns>
        public static IEnumerable<DefectLog> order(this IEnumerable<DefectLog> list) =>
            list.OrderBy(x => x.Program).ThenBy(x => x.Date);

        /// <summary>
        /// Orders Time Logs by Program and Type.
        /// </summary>
        /// <param name="list">The List of Time Logs.</param>
        /// <returns>Returns the New Ordered Time of Defect Logs.</returns>
        public static IEnumerable<TimeLog> order(this IEnumerable<TimeLog> list) =>
            list.OrderBy(x => x.Program).ThenBy(x => x.Type);

        /// <summary>
        /// Orders SETs by Program, Type and Name.
        /// </summary>
        /// <param name="list">The List of SETs.</param>
        /// <returns>Returns the New Ordered List of SETs.</returns>
        public static IEnumerable<SET> order(this IEnumerable<SET> list) =>
            list.OrderBy(x => x.Program).ThenBy(x => x.Type).ThenBy(x => x.Name);

        /// <summary>
        /// Orders PROBEs by Program.
        /// </summary>
        /// <param name="list">The List of PROBEs.</param>
        /// <returns>Returns the New Ordered List of PROBEs.</returns>
        public static IEnumerable<PROBE> order(this IEnumerable<PROBE> list) =>
            list.OrderBy(x => x.Program);

        /// <summary>
        /// Orders Phases by Program and Type.
        /// </summary>
        /// <param name="list">The List of Phases.</param>
        /// <returns>Returns the New Ordered List of Phases.</returns>
        public static IEnumerable<Phase> order(this IEnumerable<Phase> list) =>
            list.OrderBy(x => x.Program).ThenBy(x => x.Type);

        /// <summary>
        /// Orders Summaries by Program.
        /// </summary>
        /// <param name="list">The List of Summaries.</param>
        /// <returns>Returns the New Ordered List of Summaries.</returns>
        public static IEnumerable<Summary> order(this IEnumerable<Summary> list) =>
            list.OrderBy(x => x.Program);
        #endregion

        #region Process Related
        /// <summary>
        /// Determines whether the Program is valid (based on Ignored Programs).
        /// </summary>
        /// <param name="program">The program.</param>
        /// <returns>
        ///   <c>true</c> if is a valid program; otherwise, <c>false</c>.
        /// </returns>
        public static bool isValidProgram(this Program program) {
            return
                (int)program != -1 &&
                !ProcessDashboard.ignoredPrograms.Contains(program) &&
                (ProcessDashboard.isAutomaticProgram || (int)ProcessDashboard.lastProgram == -1 || program <= ProcessDashboard.lastProgram);
        }

        /// <summary>
        /// Gets the Program as String.
        /// </summary>
        /// <returns>Returns the Program Name.</returns>
        public static string getProgram(this Program program) {
            return Programs[(int)program];
        }

        /// <summary>
        /// Gets the Program from the String.
        /// </summary>
        /// <returns>Returns the Program.</returns>
        public static Program getProgram(this string program) {
            return (Program)(Array.FindIndex(Programs, x => x == program));
        }

        /// <summary>
        /// Gets the Type of the Phase as String.
        /// </summary>
        /// <returns>Returns the Phase Name.</returns>
        public static string getPhase(this PhaseType Type) {
            return PhaseTypes[(int)Type];
        }

        /// <summary>
        /// Gets the Type of the Phase.
        /// </summary>
        /// <returns>Returns the Phase.</returns>
        public static PhaseType getPhase(this string Type) {
            return (PhaseType)Array.FindIndex(PhaseTypes, x => x == Type);
        }

        /// <summary>
        /// Gets the Type of the SET as String.
        /// </summary>
        /// <returns>Returns the SET Name Part.</returns>
        public static string getSET(this SETType Type) {
            return SETTypes[(int)Type];
        }

        /// <summary>
        /// Gets the Type of the SET.
        /// </summary>
        /// <returns>Returns the SET Part.</returns>
        public static SETType getSET(this string Type) {
            return (SETType)Array.FindIndex(SETTypes, x => x == Type);
        }
        #endregion
    }
}