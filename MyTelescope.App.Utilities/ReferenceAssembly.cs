// some .cs file included in your project
using System.Resources;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MyTelescope.Test")]
[assembly: InternalsVisibleTo("MyTelescope.Test.Integration")]
[assembly: InternalsVisibleTo("MyTelescope.App.Test")]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
[assembly: NeutralResourcesLanguage("nl")]