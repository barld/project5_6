using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.Importers.DataContainers
{
    public static class Platforms
    {
        public enum PlatformId
        {
            PS4 = 146, XBO = 145, WIU = 139, PC = 94
        }

        static List<PlatformId> desiredPlatforms = new List<PlatformId>();
        public static void addDesiredPlatform(PlatformId platform)
        {
            desiredPlatforms.Add(platform);
        }

        public static List<int> getPlatformIds()
        {
            var platformIds = new List<int>();
            foreach (PlatformId id in desiredPlatforms)
            {
                platformIds.Add((int)id);
            }
            return platformIds;
        }
    }
}
