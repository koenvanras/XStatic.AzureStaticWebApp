# xStatic - Azure Static Web App

This package is based on the Umbraco xStatic deployer documentation which can be found at https://www.sammullins.co.uk/software/xstatic-for-umbraco/extending-xstatic-2/deployers/.

## Requirements
1. Umbraco website with xStatic installed
2. Azure Static Web App
3. Azure Enterprise Application with proper (contributor) rights to the Azure Static Web App (for client secret authentication)

## Configuration
1. Install the package
2. Navigate to the xStatic section in Umbraco
3. Open the Deployment Targets tab
4. Create a new Deployment Target
5. Choose Azure Static Web App and fill out the form
6. Select the Deployment Target for your website under the xStatic Sites tab

## Current limitations

Currently the `zipdeploy` request though documented is not implemented by Microsoft yet.
This package is fully based on the documentation provided via https://learn.microsoft.com/en-us/rest/api/appservice/static-sites/create-zip-deployment-for-static-site?view=rest-appservice-2024-04-01&tabs=dotnet#deploy-a-site-from-a-zipped-package.

While this package can be installed and configured in the xStatic Umbraco backoffice, deployment will not work and throw a 501 not implemented error from the Microsoft API.