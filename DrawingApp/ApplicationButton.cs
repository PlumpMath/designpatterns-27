using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingApp
{
    class ApplicationButton
    {
        Command theCommand;
        public ApplicationButton(Command newCommand)
        {
            theCommand = newCommand;
        }
        public void press()
        {
            theCommand.execute();
        }
    }
}
