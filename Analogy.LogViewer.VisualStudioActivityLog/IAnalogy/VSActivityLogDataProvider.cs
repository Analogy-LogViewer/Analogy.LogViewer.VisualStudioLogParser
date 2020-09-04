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
    public class VSActivityLogDataProvider : IAnalogyOfflineDataProvider
    {
        public string OptionalTitle { get; set; } = "Visual Studio Activity Log Parser";

        public Guid Id { get; set; } = new Guid("1ee52030-4866-4c4a-b7bb-75e5cb8a58ef");
        public Image LargeImage { get; set; } = null;
        public Image SmallImage { get; set; } = null;

        public bool CanSaveToLogFile { get; } = false;
        public string FileOpenDialogFilters { get; } = "Visual studio ActivityLog.xml files|ActivityLog.xml";
        public string FileSaveDialogFilters { get; } = string.Empty;
        public IEnumerable<string> SupportFormats { get; } = new[] { "ActivityLog.xml" };
        public bool DisableFilePoolingOption { get; } = false;
        public string InitialFolderFullPath => Environment.CurrentDirectory;
        public VSActivityLogParser VsActivityLogParser { get; set; }

        public bool UseCustomColors { get; set; } = false;
        public IEnumerable<(string originalHeader, string replacementHeader)> GetReplacementHeaders()
            => Array.Empty<(string, string)>();

        public (Color backgroundColor, Color foregroundColor) GetColorForMessage(IAnalogyLogMessage logMessage)
            => (Color.Empty, Color.Empty);
        public VSActivityLogDataProvider()
        {
        }
        public Task InitializeDataProviderAsync(IAnalogyLogger logger)
        {
            LogManager.Instance.SetLogger(logger);
            VsActivityLogParser = new VSActivityLogParser();
            return Task.CompletedTask;
        }

        public void MessageOpened(AnalogyLogMessage message)
        {
            //nop
        }
        public async Task<IEnumerable<AnalogyLogMessage>> Process(string fileName, CancellationToken token, ILogMessageCreatedHandler messagesHandler)
        {
            if (CanOpenFile(fileName))
                return await VsActivityLogParser.Process(fileName, token, messagesHandler);
            return new List<AnalogyLogMessage>(0);

        }

        public IEnumerable<FileInfo> GetSupportedFiles(DirectoryInfo dirInfo, bool recursiveLoad)
            => GetSupportedFilesInternal(dirInfo, recursiveLoad);

        public Task SaveAsync(List<AnalogyLogMessage> messages, string fileName)
        {
            throw new NotSupportedException("Saving is not supported for Plain Text");
        }

        public bool CanOpenFile(string fileName) => CanOpenFileInternal(fileName);

        private bool CanOpenFileInternal(string fileName) => Path.GetFileName(fileName).Equals("ActivityLog.xml", StringComparison.InvariantCultureIgnoreCase);

        public bool CanOpenAllFiles(IEnumerable<string> fileNames) => fileNames.All(CanOpenFile);

        private List<FileInfo> GetSupportedFilesInternal(DirectoryInfo dirInfo, bool recursive)
        {

            List<FileInfo> files = dirInfo.GetFiles("ActivityLog.xml").ToList();
            if (!recursive)
                return files;
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
