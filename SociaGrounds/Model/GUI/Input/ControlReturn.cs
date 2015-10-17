using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGrounds.Model.GUI.Input
{
    public class ControlReturn
    {
        public bool IsReleased { get; }
        public int State { get; }

        public ControlReturn(bool isReleased, int state)
        {
            State = state;
            IsReleased = isReleased;
        }
    }
}
