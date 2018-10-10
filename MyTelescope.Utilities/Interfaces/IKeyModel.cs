namespace MyTelescope.Utilities.Interfaces
{
    using System;

    public interface IKeyModel<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IKeyModel : IKeyModel<Guid>
    {
    }
}
