using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
    public delegate void EVENT_HANDLER(object source, EVENT_HANDLER_SUPER e);
   
    public class EVENT_HANDLER_SUPER : EventArgs
    {
        private string EVENT_MESSAGE;
        public EVENT_HANDLER_SUPER()
        {
        }
        public EVENT_HANDLER_SUPER(string Text)
        {
        EVENT_MESSAGE = Text;
        }
        public string GET_EVENT_MESSAGE()
        {
            return EVENT_MESSAGE;
        }
}
 
