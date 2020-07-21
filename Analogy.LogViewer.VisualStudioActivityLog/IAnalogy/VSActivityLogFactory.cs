using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.DataProviders.Extensions;
using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using Analogy.LogViewer.VisualStudioActivityLog.Managers;
using Analogy.LogViewer.VisualStudioActivityLog.Properties;

namespace Analogy.LogViewer.VisualStudioActivityLog.IAnalogy
{
    public class VSActivityLogFactory : IAnalogyFactory
    {
        internal static Guid factory = new Guid("a437ad53-0ecc-49a7-9fc3-b2f60ad007e2");
        public Guid FactoryId { get; } = factory;
        public string Title { get; } = "VS Activity Log Parser";
        public IEnumerable<IAnalogyChangeLog> ChangeLog => VisualStudioActivityLog.ChangeLog.GetChangeLog();
        public IEnumerable<string> Contributors { get; } = new List<string> { "Lior Banai" };
        public string About { get; } = "VS Activity Log Parser";
    }

    public class AnalogyXMLDataProviderFactory : IAnalogyDataProvidersFactory
    {
        public Guid FactoryId { get; } = VSActivityLogFactory.factory;
        public string Title { get; } = "VS Activity Log Data Provider";
        public IEnumerable<IAnalogyDataProvider> DataProviders { get; } = new List<IAnalogyDataProvider>()
        {
            new VSActivityLogDataProvider()
        };
    }

    public class VSActivityLogCustomActionFactory : IAnalogyCustomActionsFactory
    {
        public Guid FactoryId { get; } = VSActivityLogFactory.factory;
        public string Title { get; } = "VS Activity Log tools";
        public IEnumerable<IAnalogyCustomAction> Actions { get; } = new List<IAnalogyCustomAction>(0);
    }

    //public class VSActivityLogSettings : IAnalogyDataProviderSettings
    //{

    //    public Guid ID { get; set; } = new Guid("59fe719c-2998-4ead-b3f2-610a99e6df80");
    //    public Guid FactoryId { get; set; } = VSActivityLogFactory.xmlFactory;
    //    public string Title { get; } = "Visual Studio Activity Log Parser Settings";
    //    public UserControl DataProviderSettings { get; } = new XMLUserControlSettings();
    //    public Image SmallImage { get; }
    //    public Image LargeImage { get; } = Resources.xml32x32;

    //    public Task SaveSettingsAsync()
    //    {
    //        UserSettingsManager.UserSettings.Save();
    //        return Task.CompletedTask;
    //    }

    //}
}