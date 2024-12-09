using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.IconPacks;

namespace Fitness.ViewModels
{
    public class MuteState
    {
        public bool IsMuted { get; private set; }

        public PackIconMaterialKind GetIcon()
        {
            return IsMuted ? PackIconMaterialKind.BellOffOutline : PackIconMaterialKind.BellOutline;
        }

        public void ToggleMute()
        {
            IsMuted = !IsMuted;
        }
    }
}
