using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analogy.LogViewer.VisualStudioActivityLog.Types
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class activity
    {

        private activityEntry[] entryField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("entry")]
        public activityEntry[] entry
        {
            get => entryField;
            set => entryField = value;
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class activityEntry
    {

        private ushort recordField;

        private string timeField;

        private string typeField;

        private string sourceField;

        private string descriptionField;

        private ushort hrField;

        private bool hrFieldSpecified;

        private string pathField;

        private string guidField;

        /// <remarks/>
        public ushort record
        {
            get => recordField;
            set => recordField = value;
        }

        /// <remarks/>
        public string time
        {
            get => timeField;
            set => timeField = value;
        }

        /// <remarks/>
        public string type
        {
            get => typeField;
            set => typeField = value;
        }

        /// <remarks/>
        public string source
        {
            get => sourceField;
            set => sourceField = value;
        }

        /// <remarks/>
        public string description
        {
            get => descriptionField;
            set => descriptionField = value;
        }

        /// <remarks/>
        public ushort hr
        {
            get => hrField;
            set => hrField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hrSpecified
        {
            get => hrFieldSpecified;
            set => hrFieldSpecified = value;
        }

        /// <remarks/>
        public string path
        {
            get => pathField;
            set => pathField = value;
        }

        /// <remarks/>
        public string guid
        {
            get => guidField;
            set => guidField = value;
        }
    }


}
