namespace Scoreoid
{
    using System;
 
    [Serializable]
    public class ScoreoidException : Exception
    {
        public ScoreoidException() { }
        public ScoreoidException(string message) : base(message) { }
        public ScoreoidException(string message, Exception inner) : base(message, inner) { }
        protected ScoreoidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
