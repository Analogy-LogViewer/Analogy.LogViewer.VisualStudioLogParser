using System;
using System.Drawing;
using Analogy.DataProviders.Extensions;
using Analogy.LogViewer.VisualStudioActivityLog.Properties;

namespace Analogy.LogViewer.VisualStudioActivityLog.IAnalogy
{
    public class VSComponentImages : IAnalogyComponentImages
    {
        public Image GetLargeImage(Guid analogyComponentId)
        {
            if (analogyComponentId == VSActivityLogFactory.factory)
                return Resources.AnalogyVS32x32;
            return null;
        }

        public Image GetSmallImage(Guid analogyComponentId)
        {
            if (analogyComponentId == VSActivityLogFactory.factory)
                return Resources.VS16x16;
            return null;
        }

        public Image GetOnlineConnectedLargeImage(Guid analogyComponentId) => null;

        public Image GetOnlineConnectedSmallImage(Guid analogyComponentId) => null;

        public Image GetOnlineDisconnectedLargeImage(Guid analogyComponentId) => null;

        public Image GetOnlineDisconnectedSmallImage(Guid analogyComponentId) => null;
    }
}
