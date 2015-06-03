using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociaGroundsEngine
{
    public class Key
    {
        public static event EnterPressed OnEnterPressed;

        public delegate void EnterPressed(string text);

        public void Enter()
        {
            if (OnEnterPressed != null)
            {
                OnEnterPressed("sadasdasd");
            }
        }
    }

   public class Keyser
   {
       public Keyser()
       {
           Key.OnEnterPressed += KeyOnOnEnterPressed;
       }

       private void KeyOnOnEnterPressed(string text)
       {
           
       }
   }
}
