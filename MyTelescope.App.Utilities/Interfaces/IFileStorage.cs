namespace MyTelescope.App.Utilities.Interfaces
{
    using System.Threading.Tasks;

    public interface IFileStorage : IFileReader
    {
        /// <summary>
        /// write file as string
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        Task WriteAsString(string fileName, string text);
    }
}