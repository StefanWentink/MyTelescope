namespace MyTelescope.Api.DataLayer.Factories
{
    using Core.Utilities.Helpers;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Utilities.Interfaces.Connector;
    using Utilities.Models.Filter;

    public abstract class SingletonFactory<TModel> : ISingletonFactory<TModel>
            where TModel : class, new()
    {
        protected IConnector<TModel> Connector { get; }

        public List<TModel> List { get; }

        protected TModel GetSingleByFunction(Func<TModel, bool> function)
        {
            return List.Single(function);
        }

        protected SingletonFactory(IConnector<TModel> connector)
        {
            Connector = connector;

            try
            {
                List = Connector.Read(new FilterModel());
            }
            catch (AggregateException exception)
            {
                LogHelper.LogWarning($"Possible migration not executed. SingletonFactoryLoader constructor threw an error. {exception.Message}");

                foreach (var inner in exception.InnerExceptions)
                {
                    if (!(inner is SqlException sqlException))
                    {
                        LogHelper.LogCritical(exception);
                        throw;
                    }

                    LogHelper.LogError(sqlException);
                }
            }
            catch (SqlException exception)
            {
                LogHelper.LogWarning($"Possible migration not executed. SingletonFactoryLoader constructor threw an error. {exception.Message}");
                LogHelper.LogError(exception);
            }
            catch (Exception exception)
            {
                LogHelper.LogCritical(exception);
                throw;
            }
        }

        protected SingletonFactory(List<TModel> list)
        {
            List = list;
        }
    }
}