using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ssl_aim_trainer.Classes
{
    class GotAddressValueEventArgs : EventArgs
    {
        public readonly object Value;

        public GotAddressValueEventArgs(object value)
        {
            Value = value;
        }
    }
}
