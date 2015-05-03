using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CVista
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WrapWindow());
        }
    }
}
