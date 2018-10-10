namespace MyTelescope.App.Utilities.Interfaces
{
    using System.Threading.Tasks;

    public interface IFileReader
    {
        /// <summary>
        /// Read file as string
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<string> ReadAsString(string fileName);
    }
}
