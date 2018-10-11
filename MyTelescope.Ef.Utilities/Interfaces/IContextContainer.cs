namespace MyTelescope.Ef.Utilities.Interfaces
{
    using Microsoft.EntityFrameworkCore;

    public interface IContextContainer
    {
        DbContext GetContext { get; }
    }
}