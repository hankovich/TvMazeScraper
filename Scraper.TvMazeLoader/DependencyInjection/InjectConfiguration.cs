//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InjectConfiguration.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the InjectConfiguration type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.TvMazeLoader.DependencyInjection
{
    using Autofac;

    using Microsoft.Azure.WebJobs.Host.Config;

    public class InjectConfiguration : IExtensionConfigProvider
    {
        private static readonly object SyncLock = new object();
        private static volatile IContainer container;

        public void Initialize(ExtensionConfigContext context)
        {
            this.InitializeContainer(context);

            context
                .AddBindingRule<InjectAttribute>()
                .BindToInput(i => container.Resolve(i.Type));
        }

        private void InitializeContainer(ExtensionConfigContext context)
        {
            if (container != null)
            {
                return;
            }

            lock (SyncLock)
            {
                if (container != null)
                {
                    return;
                }

                container = ContainerConfig.BuildContainer(context.Config.LoggerFactory);
            }
        }
    }
}