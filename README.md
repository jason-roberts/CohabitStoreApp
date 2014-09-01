#CohabitStoreApp

Enables running development version of Windows Store app alongside production version installed from store.

Pre build event appends "LOCALDEV" to MainPackageIdentityName element in Package.StoreAssociation.xml and DisplayName element in Package.appxmanifest when built in DEBUG configuration.

Also changes background color to red for local dev version.

Building in RELEASE removes "LOCALDEV" and reverts back to normal name for publishing to store.

As long as you **don't** deploy/run RELEASE mode (which will overwrite the installed store version) this will enable you to keep the dev instance separate.

Check out the included ExampleUsage solution. Notice the pre-build event: "$(SolutionDir)BuildTools\CohabitStoreApp.exe $(ConfigurationName) ExampleUsage $(ProjectDir) 0000FF"

You should replace "ExampleUsage" with the name of your app and "0000FF" with the real background color of your app.

The utility assumes you have a Package.StoreAssociation.xml, you'll see an error if you don't.

This is a quick fix/hack. I have used it with real Store app install and local dev version without problem, it works on my machine, your mileage may vary, use at own risk. Check out the LICENSE.txt for more information.
