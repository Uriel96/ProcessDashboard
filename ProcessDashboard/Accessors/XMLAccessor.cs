using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessDashboard.Accessors {

    /// <summary>
    /// Class that Access an XML File.
    /// </summary>
    public class XMLAccessor : Accessor {
        #region Properties
        /// <summary>
        /// The Type of Extension that the File must be (.xml).
        /// </summary>
        public override string extension { get; protected set; } = ".xml";
        /// <summary>
        /// The List of Data Read from the File.
        /// </summary>
        public List<Data> data { get; private set; } = new List<Data>();
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="XMLAccessor"/> class.
        /// </summary>
        /// <param name="file">The File to Access.</param>
        /// <param name="checarExtension">if set to <c>true</c> checks the extension of the File to be .xml.</param>
        public XMLAccessor(string file, bool checarExtension = false) : base(file, checarExtension) { }
        #endregion

        #region Read        
        /// <summary>
        /// Reads the XML File and sets Data based on the tag.
        /// </summary>
        /// <param name="tag">The tag where Data will be Retrieved.</param>
        /// <param name="showAttributes">The attributes that will be using as the Columns of the Data.</param>
        /// <exception cref="IOException">Thrown when the data could not be found in the XML file.</exception>
        public void Read(string tag, params string[] showAttributes) {
            var xml = new XmlDocument();
            try {
                xml.Load(file);
            } catch {
                throw new IOException($"Could not read the XML file: {file}");
            }
            var nodeList = xml.GetElementsByTagName(tag);
            foreach (XmlNode node in nodeList) {
                Data data = new Data();
                foreach (string attribute in showAttributes) {
                    string newData = node.Attributes?[attribute]?.Value ?? null;
                    data[attribute] = newData;
                }
                this.data.Add(data);
            }
        }

        /// <summary>
        /// Reads the XML File and sets Data based on the tag and the condition.
        /// </summary>
        /// <param name="tag">The tag where Data will be Retrieved.</param>
        /// <param name="condition">The Condition it needs to Retrieve the Data from the tag.</param>
        /// <param name="showAttributes">The attributes that will be using as the Columns of the Data.</param>
        /// <exception cref="IOException">Thrown when the data could not be found in the XML file.</exception>
        public void Read(string tag, Predicate<XmlNode> condition, params string[] showAttributes) {
            var xml = new XmlDocument();
            try {
                xml.Load(file);
            } catch {
                throw new IOException($"Could not read the XML file: {file}");
            }
            var nodeList = xml.GetElementsByTagName(tag);
            XmlNode firstNode = null;
            foreach (XmlNode node in nodeList) {
                if (!condition(node)) {
                    continue;
                }
                firstNode = node.ParentNode;
                break;

            }
            if (firstNode == null) {
                throw new IOException("Could not find the first Node inside the XML file.");
            }
            var newNodeList = firstNode.ChildNodes;
            foreach (XmlNode node in newNodeList) {
                var data = new Data();
                foreach (string attribute in showAttributes) {
                    string newData = node.Attributes?[attribute]?.Value ?? null;
                    data[attribute] = newData;
                }
                this.data.Add(data);
            }
        }
        #endregion
    }
}
