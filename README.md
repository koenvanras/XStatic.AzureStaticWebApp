# xStatic - Azure Static Web App

This package is based on the Umbraco xStatic deployer documentation which can be found at https://www.sammullins.co.uk/software/xstatic-for-umbraco/extending-xstatic-2/deployers/.

Current limitations
-------------------

Currently the `zipdeploy` request though documented is not implemented by Microsoft yet.
This package is fully based on the documentation provided via https://learn.microsoft.com/en-us/rest/api/appservice/static-sites/create-zip-deployment-for-static-site?view=rest-appservice-2024-04-01&tabs=dotnet#deploy-a-site-from-a-zipped-package.

While this package can be installed and configured in the xStatic Umbraco backoffice, deployment will not work and throw a 501 not implemented error from the Microsoft API.