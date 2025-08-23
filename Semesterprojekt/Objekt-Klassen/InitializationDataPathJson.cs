using System;
using System.IO;

namespace Semesterprojekt
{
    internal class InitializationDataPathJson
    {
        // Initialisierung Dateipfad für JSONs
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
        private static readonly string dataPath = Path.Combine(projectRoot, "data");

        // Zusammenstellung Dateipfad für JSONs
        internal static string DataPath(string fileName)
        {
            return Path.Combine(dataPath, $"{fileName}.json");
        }
    }
}