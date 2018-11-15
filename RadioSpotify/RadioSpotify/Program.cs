using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using System.Reflection;
using log4net;
using log4net.Config;

namespace RadioSpotify
{
    static class Program
    {
        public static ILog Log { get { return _log; } }
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().Name);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuForm());   
        }
        
    }
}
