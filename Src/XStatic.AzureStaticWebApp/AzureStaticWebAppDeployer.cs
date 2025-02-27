using Azure;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.AppService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using XStatic.Core;
using XStatic.Core.Deploy;
using XStatic.Core.Helpers;

namespace XStatic.AzureStaticWebApp
{
    public class AzureStaticWebAppDeployer : IDeployer
    {
        public const string DeployerKey = "azurestaticwebapp";
        private readonly string _subscription;
        private readonly string _tenant;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _resourceGroup;
        private readonly string _appName;


        public AzureStaticWebAppDeployer(Dictionary<string, string> parameters)
        {
            _subscription = parameters[AzureStaticWebAppDeployerDefinition.FieldNames.Subscription];
            _tenant = parameters[AzureStaticWebAppDeployerDefinition.FieldNames.Tenant];
            _clientId = parameters[AzureStaticWebAppDeployerDefinition.FieldNames.ClientId];
            _clientSecret = parameters[AzureStaticWebAppDeployerDefinition.FieldNames.ClientSecret];
            _resourceGroup = parameters[AzureStaticWebAppDeployerDefinition.FieldNames.ResourceGroup];
            _appName = parameters[AzureStaticWebAppDeployerDefinition.FieldNames.AppName];
        }

        public Task<XStaticResult> DeployWholeSite(string folderPath)
        {
            return TaskHelper.FromResultOf(() =>
            {
                var result = Deploy(folderPath);

                return result;
            });
        }

        public XStaticResult Deploy(string folderPath)
        {
            var distZipFile = $"{Environment.CurrentDirectory}/dist.zip";

            try
            {
                // Prepare dist
                File.Delete(distZipFile);
                ZipFile.CreateFromDirectory(folderPath, distZipFile);

                // Authenticate with Azure
                var credentail = new ClientSecretCredential(_tenant, _clientId, _clientSecret);
                var client = new ArmClient(credentail);

                // Get Azure Static Web App
                var staticSiteResourceId = StaticSiteResource.CreateResourceIdentifier(_subscription, _resourceGroup, _appName);
                var staticSiteResource = client.GetStaticSiteResource(staticSiteResourceId);

                // Create zip deployment package
                var staticSiteZipDeploymentEnvelope = new StaticSiteZipDeployment()
                {
                    AppZipUri = new Uri(distZipFile),
                    DeploymentTitle = $"xStatic build {DateTime.Now.ToString("yyyy-MM-dd HH:mm")}",
                    Provider = "xStatic Umbraco"
                };

                // Deploy to Azure Static Web App
                var result = staticSiteResource.CreateZipDeploymentForStaticSiteAsync(WaitUntil.Completed, staticSiteZipDeploymentEnvelope).GetAwaiter().GetResult();

                // Cleanup dist
                File.Delete(distZipFile);
            }
            catch (Exception e)
            {
                File.Delete(distZipFile);
                return XStaticResult.Error("Error deploying site to Azure Static Web App.", e);
            }

            return XStaticResult.Success("Site deployed to Azure Static Web App.");
        }
    }
}
