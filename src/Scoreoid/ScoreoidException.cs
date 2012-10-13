using System;

namespace Scoreoid
{
    public class ScoreoidException : Exception
    {
        public ScoreoidException()
        {
        }

        public ScoreoidException(string message)
            : base(message)
        {
        }

        public ScoreoidException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}