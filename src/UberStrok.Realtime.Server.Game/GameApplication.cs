﻿using log4net;
using log4net.Config;
using Photon.SocketServer;
using System.IO;

namespace UberStrok.Realtime.Server.Game
{
    public class GameApplication : ApplicationBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GameApplication));

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            Log.Info($"Accepted new connection at {initRequest.RemoteIP}:{initRequest.RemotePort}");

            return new GamePeer(initRequest);
        }

        protected override void Setup()
        {
            // Add a the log path to the properties so can use them in log4net.config.
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationPath, "log");
            // Configure log4net to use log4net.config file.
            var configFile = new FileInfo(Path.Combine(BinaryPath, "log4net.config"));
            if (configFile.Exists)
                XmlConfigurator.ConfigureAndWatch(configFile);

            Log.Info("Started CommServer...");
        }

        protected override void TearDown()
        {
            // Space
        }
    }
}
