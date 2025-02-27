using System.Collections.Generic;
using XStatic.Core.Deploy;

namespace XStatic.AzureStaticWebApp
{
    public class AzureStaticWebAppDeployerDefinition : IDeployerDefinition
    {
        public class FieldNames
        {

            public const string Subscription = "AzureStaticWebApp.Subscription";
            public const string Tenant = "AzureStaticWebApp.Tenant";
            public const string ClientId = "AzureStaticWebApp.ClientId";
            public const string ClientSecret = "AzureStaticWebApp.ClientSecret";
            public const string ResourceGroup = "AzureStaticWebApp.ResourceGroup";
            public const string AppName = "AzureStaticWebApp.AppName";
        }

        public string Id => AzureStaticWebAppDeployer.DeployerKey;

        public string Name => "Azure Static Web App";

        public string Help => "First create an Azure Static Web App. This deployer will publish the static application on each deploy.";

        public IEnumerable<DeployerField> Fields => new[]
        {
            new DeployerField { Alias=FieldNames.Subscription, Name = "Subscription", EditorUiAlias = UIEditors.Text},
            new DeployerField { Alias=FieldNames.Tenant, Name = "Tenant", EditorUiAlias = UIEditors.Text},
            new DeployerField { Alias=FieldNames.ClientId, Name = "Client ID", EditorUiAlias = UIEditors.Text},
            new DeployerField { Alias=FieldNames.ClientSecret, Name = "Client Secret", EditorUiAlias = UIEditors.Password},
            new DeployerField { Alias=FieldNames.ResourceGroup, Name = "Resource Group", EditorUiAlias = UIEditors.Text},
            new DeployerField { Alias=FieldNames.AppName, Name = "App Name", EditorUiAlias = UIEditors.Text},
        };
    }
}
