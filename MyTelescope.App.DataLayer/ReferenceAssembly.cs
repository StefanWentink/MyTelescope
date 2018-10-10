// some .cs file included in your project
using System.Resources;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("MyTelescope.Test")]
[assembly: InternalsVisibleTo("MyTelescope.App.Integration")]
[assembly: InternalsVisibleTo("MyTelescope.InfrastructureConfiguration.Integration")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: NeutralResourcesLanguage("nl")]