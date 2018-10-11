namespace MyTelescope.App.Test.Base
{
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