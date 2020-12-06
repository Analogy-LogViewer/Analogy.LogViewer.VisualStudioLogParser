using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Analogy.Interfaces;
using Analogy.LogViewer.VisualStudioActivityLog.Managers;

namespace Analogy.LogViewer.VisualStudioActivityLog.IAnalogy
{
    public class VSActivityLogDataProvider : Template.OfflineDataProvider
    {
        public override string OptionalTitle { get; set; } = "Visual Studio Activity Log Parser";

        public override Guid Id { get; set; } = new Guid("1ee52030-4866-4c4a-b7bb-75e5cb8a58ef");
        public override Image? LargeImage { get; set; } = null;
        public override Image? SmallImage { get; set; } = null;

        public override bool CanSaveToLogFile { get; set; } = false;
        public override string FileOpenDialogFilters { get; set; } = "Visual studio ActivityLog.xml files|ActivityLog.xml";
        public override string FileSaveDialogFilters { get; set; } = string.Empty;
        public override IEnumerable<string> SupportFormats { get; set; } = new[] { "ActivityLog.xml" };
        public override bool DisableFilePoolingOption { get; set; } = false;
        public override string InitialFolderFullPath => Environment.CurrentDirectory;
        public VSActivityLogParser VsActivityLogParser { get; set; }

        public override bool UseCustomColors { get; set; } = false;
        public override IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders()
            => Array.Empty<(string, string)>();

        public override (Color backgroundColor, Color foregroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => (Color.Empty, Color.Empty);
        public VSActivityLogDataProvider()
        {
        }
        public override Task InitializeDataProviderAsync(IAnalogyLogger logger)
        {
            LogManager.Instance.SetLogger(logger);
            VsActivityLogParser = new VSActivityLogParser();
            return base.InitializeDataProviderAsync(logger);
        }


        public override async Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            if (CanOpenFile(fileName))
            {
                return await VsActivityLogParser.Process(fileName, token, messagesHandler);
            }

            return new List<AnalogyLogMessage>(0);

        }



        public override bool CanOpenFile(string fileName) => CanOpenFileInternal(fileName);

        private bool CanOpenFileInternal(string fileName) => Path.GetFileName(fileName).Equals("ActivityLog.xml", StringComparison.InvariantCultureIgnoreCase);

        public override bool CanOpenAllFiles(IEnumerable<string> fileNames) => fileNames.All(CanOpenFile);

        protected override List<FileInfo> GetSupportedFilesInternal(DirectoryInfo dirInfo, bool recursive)
        {

            List<FileInfo> files = dirInfo.GetFiles("ActivityLog.xml").ToList();
            if (!recursive)
            {
                return files;
            }

            try
            {
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    files.AddRange(GetSupportedFilesInternal(dir, true));
                }
            }
            catch (Exception)
            {
                return files;
            }

            return files;
        }
    }
}
