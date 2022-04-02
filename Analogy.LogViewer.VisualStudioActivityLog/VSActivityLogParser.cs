using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;
using Analogy.LogViewer.VisualStudioActivityLog.Types;

namespace Analogy.LogViewer.VisualStudioActivityLog
{
    public class VSActivityLogParser
    {

        public Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token,
            ILogMessageCreatedHandler messagesHandler)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(activity));
            List<AnalogyLogMessage> msg = new List<AnalogyLogMessage>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                long count = 0;
                var entries = (activity)serializer.Deserialize(reader);
                reader.Close();
                for (var i = 0; i < entries.entry.Length; i++)
                {
                    activityEntry entry = entries.entry[i];
                    AnalogyLogLevel level = entry.type == "Information"
                        ? AnalogyLogLevel.Information
                        : (entry.type == "Warning" ? AnalogyLogLevel.Warning : AnalogyLogLevel.Error);
                    AnalogyLogMessage m = new AnalogyLogMessage(entry.description, level, AnalogyLogClass.General, "");
                    if (DateTime.TryParse(entry.time, out var time))
                    {
                        m.Date = time;
                    }

                    m.Source = entry.source;
                    m.AdditionalInformation = new Dictionary<string, string>();
                    m.AdditionalInformation.Add("GUID", entry.guid);
                    m.AdditionalInformation.Add("HR", entry.hr.ToString());
                    m.AdditionalInformation.Add("Is Specific", entry.hrSpecified.ToString());
                    m.AdditionalInformation.Add("Path", entry.path);
                    messagesHandler.AppendMessage(m, fileName);
                    messagesHandler.ReportFileReadProgress(new AnalogyFileReadProgress(AnalogyFileReadProgressType.Percentage, 1, i, entries.entry.Length));
                    msg.Add(m);
                }
            }

            return Task.FromResult(msg.AsEnumerable());

        }


        private string TryFix(string data)
        {
            string corrrected = $"<Array>{data}</Array>";
            return corrrected;
        }

        private List<object> ParserInternal(string data)
        {
            XDocument XMLDoc = XDocument.Parse(data);
            return new List<object>();
        }
    }
}
