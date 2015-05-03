using System;

namespace CDatos.ConnectionWeb
{
    /// <summary>
    /// Clase ExceptionMissingWeb heredera de Exception, se lanza cuando no encuentra el feed o no hay conexión
    /// </summary>
    /// <author>Tomás Martínez Sempere</author>
    /// <date>01/05/2015</date>
    /// <see cref="Exception"/>
    /// <see cref="DataWeb"/>
    public class ExceptionMissingWeb : Exception
    {
        /// <summary>
        /// Constructor vacío
        /// </summary>
        public ExceptionMissingWeb()
        {
        }

        /// <summary>
        /// Constructor con mensaje, inicia el constructor de la clase padre
        /// </summary>
        /// <param name="message">(string) Aviso de la exception</param>
        public ExceptionMissingWeb(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor con mensaje y exception, inicia el constructor de la clase padre
        /// </summary>
        /// <param name="message">(string) Aviso de la exception</param>
        /// <param name="inner">(Exception) Exception ocurrida</param>
        public ExceptionMissingWeb(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
