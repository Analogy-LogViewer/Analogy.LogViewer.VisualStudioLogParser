using System;
using System.Collections.Generic;
using Analogy.Interfaces;

namespace Analogy.LogViewer.VisualStudioActivityLog
{
    public static class ChangeLog
    {
        public static IEnumerable<AnalogyChangeLog> GetChangeLog()
        {
            yield return new AnalogyChangeLog("Initial Project)", AnalogChangeLogType.Improvement, "Lior Banai", new DateTime(2020, 07, 21));
        }
    }
}
