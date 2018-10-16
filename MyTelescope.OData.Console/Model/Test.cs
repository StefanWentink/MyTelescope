namespace MyTelescope.OData.Console.Model
{
    using SWE.Http.Interfaces;
    using SWE.Http.Interfacess;
    using SWE.OData.Interfaces;
    using SWE.Polly.Interfaces.Policies;
    using System;
    using System.Collections.Generic;
    using SWE.Builder;
    using System.Threading.Tasks;

    public class Test
    {
        private IRepository Repository { get; }

        public Test(IRepository repository)
        {
            Repository = repository;
        }

        public async Task<IEnumerable<T>> Read<T>(IODataBuilder<T, Guid> builder)
        {
            var content = builder.Build();
            return await Repository.ReadAsync<T>(content).ConfigureAwait(false);
        }
    }
}