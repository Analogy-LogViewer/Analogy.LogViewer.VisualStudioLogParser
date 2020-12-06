using System;
using System.Collections.Generic;
using System.Drawing;
using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using Analogy.LogViewer.VisualStudioActivityLog.Properties;


namespace Analogy.LogViewer.VisualStudioActivityLog.IAnalogy
{
    public class VSActivityLogFactory : Template.PrimaryFactory
    {
        internal static Guid Id = new Guid("a437ad53-0ecc-49a7-9fc3-b2f60ad007e2");
        public override Guid FactoryId { get; set; } = Id;
        public override string Title { get; set; } = "VS Activity Log Parser";
        public override Image? SmallImage { get; set; } = Resources.AnalogyVS16x16;
        public override Image? LargeImage { get; set; } = Resources.AnalogyVS32x32;
        public override IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = VisualStudioActivityLog.ChangeLog.GetChangeLog();
        public override IEnumerable<string> Contributors { get; set; } = new List<string> { "Lior Banai" };
        public override string About { get; set; } = "VS Activity Log Parser";
    }

    public class AnalogyXMLDataProviderFactory : Template.DataProvidersFactory
    {
        public override Guid FactoryId { get; set; } = VSActivityLogFactory.Id;
        public override string Title { get; set; } = "VS Activity Log Data Provider";
        public override IEnumerable<IAnalogyDataProvider> DataProviders { get; set; } = new List<IAnalogyDataProvider>()
        {
            new VSActivityLogDataProvider()
        };
    }

    public class VSActivityLogCustomActionFactory : IAnalogyCustomActionsFactory
    {
        public Guid FactoryId { get; set; } = VSActivityLogFactory.Id;
        public string Title { get; set; } = "VS Activity Log tools";
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