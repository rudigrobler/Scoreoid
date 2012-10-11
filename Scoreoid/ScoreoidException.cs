namespace Scoreoid
{
    using System;
 
    public class ScoreoidException : Exception
    {
        public ScoreoidException() { }
        public ScoreoidException(string message) : base(message) { }
        public ScoreoidException(string message, Exception innerException) : base(message, innerException) { }
    }
}
