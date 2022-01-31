using Topshelf;

namespace SD.FileSystem.WebClient
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(config =>
            {
                config.Service<ServiceLauncher>(host =>
                {
                    host.ConstructUsing(name => new ServiceLauncher());
                    host.WhenStarted(launcher => launcher.Start());
                    host.WhenStopped(launcher => launcher.Stop());
                });
                config.RunAsLocalSystem();

                config.SetServiceName("SD.FileSystem.WebClient");
                config.SetDisplayName("SD.FileSystem.WebClient");
                config.SetDescription("SD.FileSystem.WebClient");
            });
        }
    }
}
