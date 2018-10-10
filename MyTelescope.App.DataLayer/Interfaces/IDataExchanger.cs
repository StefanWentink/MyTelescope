namespace MyTelescope.App.DataLayer.Interfaces
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IDataExchanger<in T>
        where T : IRequestModel
    {
        Task<byte[]> GetBytes(T requestModel);

        Task<Stream> GetStream(T requestModel);

        Task<string> GetString(T requestModel);
    }
}
