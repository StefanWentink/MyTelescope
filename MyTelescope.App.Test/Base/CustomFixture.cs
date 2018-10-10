namespace MyTelescope.App.Test.Base
{
    using System;
    using Core.Utilities.Helpers;
    using Core.Utilities.Helpers.Config;
    using Microsoft.Extensions.Logging;
    using MyTelescope.Utilities.Helpers;

    public class CustomFixture : MyTelescope.Test.Base.CustomFixture
    {
        protected override void InitializeConfig()
        {
            base.InitializeConfig();

            if (!App.Utilities.Helpers.ConfigHelper.Initialized)
            {
                App.Utilities.Helpers.ConfigHelper.Initialize(new DummyFileConfiguration());
            }
        }
    }
}
