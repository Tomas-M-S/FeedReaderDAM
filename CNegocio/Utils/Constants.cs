using System;

namespace CNegocio.Utils
{
    /// <summary>
    /// Clase con diversas constantes usadas en el programa
    /// </summary>
    public class Constants
    {
        // Constantes para la Ventana Modificar/Guardar Feed
        public static int MODIFY_FEED = 0;
        public static int CREATE_FEED = 1;

        // Constantes del estado del Feed
        public static int FEED_UPDATED = 1;
        public static int FEED_NO_CHANGES = 0;
        public static int FEED_OFFLINE = -1;

        // Constantes para el mensaje del estado del Feed
        public static string MSG_F_UPDATED = "Feed actualizado";
        public static string MSG_F_NO_CHANGES = "Feed sin cambios";
        public static string MSG_F_OFFLINE = "No se pudo contactar con el feed";
    }
}
