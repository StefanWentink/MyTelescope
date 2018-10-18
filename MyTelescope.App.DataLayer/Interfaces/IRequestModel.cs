namespace MyTelescope.App.DataLayer.Interfaces
{
    using SWE.Http.Enums;

    public interface IRequestModel
    {
        string ApiRouteName { get; set; }

        string ApiActionName { get; set; }

        string Content { get; set; }

        HttpVerb Verb { get; set; }
    }
}