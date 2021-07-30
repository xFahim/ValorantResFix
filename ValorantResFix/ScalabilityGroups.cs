using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorantResFix
{
    [Serializable]
    public class ScalabilityGroups
    {
        public int ResolutionQuality { get; set; }
        public int ViewDistanceQuality { get; set; }
        public int AntiAliasingQuality { get; set; }
        public int ShadowQuality { get; set; }
        public int PostProcessQuality { get; set; }
        public int TextureQuality { get; set; }
        public int EffectsQuality { get; set; }
        public int FoliageQuality { get; set; }
        public int ShadingQuality { get; set; }
        public string FilePath { get; set; }


    }
}
