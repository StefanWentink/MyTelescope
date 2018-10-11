namespace MyTelescope.Ef.Utilities.Helpers
{
    using Core.Utilities.Helpers;
    using Exceptions;
    using Microsoft.EntityFrameworkCore;
    using MyTelescope.Utilities.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;

    public static class DbContextHelper
    {
        /// <summary>
        /// Gets DbSet for given DbContext
        /// </summary>
        /// Must be Class
        /// <typeparam name="T">
        /// Must be Class
        /// </typeparam>
        /// <param name="context"> context provided </param>
        /// <returns> return query </returns>
        public static DbSet<T> GetObjectSet<T>(this DbContext context)
            where T : class
        {
            return context.Set<T>();
        }

        /// <summary>
        /// Gets DbQuery for given DbContext
        /// </summary>
        /// <typeparam name="T">
        /// Must be Class
        /// </typeparam>
        /// <param name="context"> context provided </param>
        /// <returns> return query </returns>
        private static IQueryable<T> GetObjectQuery<T>(this DbContext context)
            where T : class
        {
            return context.GetObjectQuery<T>(null);
        }

        /// <summary>
        /// Gets DbQuery for given DbContext
        /// </summary>
        /// <typeparam name="T">Must be Class</typeparam>
        /// <param name="context"> context provided </param>
        /// <param name="includelist">List of included DB relations</param>
        /// <returns> return query </returns>
        private static IQueryable<T> GetObjectQuery<T>(this DbContext context, IEnumerable<string> includelist)
            where T : class
        {
            IQueryable<T> result = GetObjectSet<T>(context);

            foreach (var include in includelist.ParseList())
            {
                result = result.Include(include);
            }

            return result;
        }

        public static void Save(this DbContext context)
        {
            try
            {
                if (context.SaveChanges() < 0)
                {
                    throw new ContextContainerException("Save failed.");
                }
            }
            catch (InvalidOperationException exception)
            {
                LogHelper.LogError(exception);
                throw new ContextContainerException(exception.GetInnerMostException().Message);
            }
            catch (DbUpdateException exception)
            {
                LogHelper.LogError(exception);
                throw new ContextContainerException(exception.GetInnerMostException().Message);
            }
            catch (System.Data.SqlClient.SqlException exception)
            {
                LogHelper.LogError(exception);
                throw new ContextContainerException(exception.GetInnerMostException().Message);
            }
        }

        /// <summary>
        /// Opens a contexts connection
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="System.ArgumentException">Thrown when connectionstate is broken</exception>
        public static void OpenConnection(this DbContext context)
        {
            var currentState = context.CurrentConnectionState();

            if (currentState == ConnectionState.Broken)
            {
                throw new ArgumentException("Context in current connection state cannot be opened");
            }

            if (currentState != ConnectionState.Open)
            {
                context.Database.GetDbConnection().Open();
            }
        }

        /// <summary>
        /// Closes a contexts connection
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="System.ArgumentException">Thrown when connectionstate is broken</exception>
        public static void CloseConnection(this DbContext context)
        {
            var currentState = context.CurrentConnectionState();

            if (currentState == ConnectionState.Broken)
            {
                throw new ArgumentException("Context in current connection state cannot be closed");
            }

            if (currentState != ConnectionState.Closed)
            {
                context.Database.GetDbConnection().Close();
            }
        }

        /// <summary>
        /// Returns a contexts current connection state, returns broken if no original connection or state is found
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ConnectionState CurrentConnectionState(this DbContext context)
        {
            return context.GetActiveConnection()?.State ?? ConnectionState.Broken;
        }

        /// <summary>
        /// Returns the contexts active connection. Returns Null if context is disposed
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static DbConnection GetActiveConnection(this DbContext context)
        {
            var isDisposed = Convert.ToBoolean(
                typeof(DbContext)
                    .GetField("_disposed", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(context));

            return isDisposed ? null : context.Database.GetDbConnection();
        }

        public static IQueryable<TModel> GetNoTrackingQuery<TModel>(this DbContext context)
            where TModel : class
        {
            return context.GetNoTrackingQuery<TModel>(null);
        }

        public static IQueryable<TModel> GetNoTrackingQuery<TModel>(this DbContext context, List<string> includes)
            where TModel : class
        {
            return includes.IsNullOrEmpty()
                ? context.GetObjectQuery<TModel>().AsNoTracking()
                : context.GetObjectQuery<TModel>(includes).AsNoTracking();
        }
    }
}