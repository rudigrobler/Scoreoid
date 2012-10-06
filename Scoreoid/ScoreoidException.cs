using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreoid
{
    public class ScoreoidException : Exception
    {
        public ScoreoidException()
            : base()
        {

        }

        public ScoreoidException(string message)
            : base(message)
        {

        }
    }
}
