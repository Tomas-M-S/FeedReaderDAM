using System;

namespace CDatos.ConnectionWeb
{
    public class ExceptionMissingWeb : Exception
    {
        public ExceptionMissingWeb() {}

        public ExceptionMissingWeb(string message) : base(message) {}

        public ExceptionMissingWeb(string message, Exception inner) : base(message, inner) {}
    }
}
