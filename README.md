# DNN.Vendors
The Vendors module project allows admins to manage Vendor relationships and add Advertising banners to their site.

## To build this module
- Clone this repository to the DesktopModules folder of a local Dnn website.
- Open the solution file (.sln) in visual studio
- In the build/debug dropdown, pick `package` and click run
- An installable package will be created in the `artifacts` directory
- Use DNN extensions to install that extendion

## To rebuild/debug the module
- Changes to static files like .ascx or .js file need nothing to be done
- If compiled code is updated, simply pick `deploy` in the debug dropdown and start debugging
- Attach to the website process to be able to hit a breakpoint
