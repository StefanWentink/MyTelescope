// some .cs file included in your project
using System.Resources;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("MyTelescope.Integration")]
[assembly: InternalsVisibleTo("MyTelescope.App.Integration")]
[assembly: InternalsVisibleTo("MyTelescope.Api.Integration")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: NeutralResourcesLanguage("nl")]