using System;
using System.Collections.Generic;
using XStatic.Core.Deploy;

namespace XStatic.AzureStaticWebApp
{
    public class AzureStaticWebAppAutoInstaller : IDeployerAutoInstaller
    {
        public IDeployerDefinition Definition => new AzureStaticWebAppDeployerDefinition();

        public Func<Dictionary<string, string>, IDeployer> Constructor => (x) => new AzureStaticWebAppDeployer(x);
    }
}
