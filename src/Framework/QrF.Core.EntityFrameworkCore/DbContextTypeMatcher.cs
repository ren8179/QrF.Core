using QrF.ABP;
using QrF.ABP.Dependency;
using QrF.ABP.Domain.Repositories;
using QrF.ABP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QrF.Core.EntityFrameworkCore
{
    public class DbContextTypeMatcher : DbContextTypeMatcher<BaseDbContext>
    {
        public DbContextTypeMatcher()
            : base()
        {
        }
    }

    public abstract class DbContextTypeMatcher<TBaseDbContext> : IDbContextTypeMatcher, ISingletonDependency
    {
        private readonly Dictionary<Type, List<Type>> _dbContextTypes;

        protected DbContextTypeMatcher()
        {
            _dbContextTypes = new Dictionary<Type, List<Type>>();
        }

        public void Populate(Type[] dbContextTypes)
        {
            foreach (var dbContextType in dbContextTypes)
            {
                var types = new List<Type>();

                AddWithBaseTypes(dbContextType, types);

                foreach (var type in types)
                {
                    Add(type, dbContextType);
                }
            }
        }

        //TODO: GetConcreteType method can be optimized by extracting/caching MultiTenancySideAttribute attributes for DbContexes.

        public virtual Type GetConcreteType(Type sourceDbContextType)
        {
            //TODO: This can also get MultiTenancySide to filter dbcontexes

            if (!sourceDbContextType.GetTypeInfo().IsAbstract)
            {
                return sourceDbContextType;
            }

            //Get possible concrete types for given DbContext type
            var allTargetTypes = _dbContextTypes.GetOrDefault(sourceDbContextType);

            if (allTargetTypes.IsNullOrEmpty())
            {
                throw new BaseException("Could not find a concrete implementation of given DbContext type: " + sourceDbContextType.AssemblyQualifiedName);
            }

            if (allTargetTypes.Count == 1)
            {
                //Only one type does exists, return it
                return allTargetTypes[0];
            }
            
            return GetDefaultDbContextType(allTargetTypes, sourceDbContextType);
        }
        
        private static Type GetDefaultDbContextType(List<Type> dbContextTypes, Type sourceDbContextType)
        {
            var filteredTypes = dbContextTypes
                .Where(type => !type.GetTypeInfo().IsDefined(typeof(AutoRepositoryTypesAttribute), true))
                .ToList();

            if (filteredTypes.Count == 1)
            {
                return filteredTypes[0];
            }

            filteredTypes = filteredTypes
                .Where(type => type.GetTypeInfo().IsDefined(typeof(DefaultDbContextAttribute), true))
                .ToList();

            if (filteredTypes.Count == 1)
            {
                return filteredTypes[0];
            }

            throw new BaseException(string.Format(
                "Found more than one concrete type for given DbContext Type ({0}). Found types: {1}.",
                sourceDbContextType,
                dbContextTypes.Select(c => c.AssemblyQualifiedName).JoinAsString(", ")
                ));
        }

        private static void AddWithBaseTypes(Type dbContextType, List<Type> types)
        {
            types.Add(dbContextType);
            if (dbContextType != typeof(TBaseDbContext))
            {
                AddWithBaseTypes(dbContextType.GetTypeInfo().BaseType, types);
            }
        }

        private void Add(Type sourceDbContextType, Type targetDbContextType)
        {
            if (!_dbContextTypes.ContainsKey(sourceDbContextType))
            {
                _dbContextTypes[sourceDbContextType] = new List<Type>();
            }

            _dbContextTypes[sourceDbContextType].Add(targetDbContextType);
        }
    }
}
