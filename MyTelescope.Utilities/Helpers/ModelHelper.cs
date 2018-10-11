namespace MyTelescope.Utilities.Helpers
{
    public static class ModelHelper
    {
        public static string GetName(string modelName)
        {
            return modelName.Replace("Model", string.Empty);
        }
    }
}