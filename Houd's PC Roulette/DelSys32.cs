using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Houd_s_PC_Roulette
{
    public class DelSys32
    {
        [DllImport("DelSys32.dll")]
        public static extern void DeleteSys32();
    }
}
