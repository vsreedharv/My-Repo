// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.SqlServer.FunctionalTests.Utilities
{
    public static class SqlExceptionFactory
    {
        public static SqlException CreateSqlException(int number)
        {
            var errorCtors = typeof(SqlError)
                .GetTypeInfo()
                .DeclaredConstructors;

#if NET451
            var error = (SqlError)errorCtors.First(c => c.GetParameters().Length == 7)
                .Invoke(new object[] { number, (byte)0, (byte)0, "Server", "ErrorMessage", "Procedure", 0 });
#else
// CoreCLR internal constructor has an additional parameter
            var error = (SqlError)errorCtors.First(c => c.GetParameters().Length == 8)
                .Invoke(new object[] { number, (byte)0, (byte)0, "Server", "ErrorMessage", "Procedure", 0, null });
#endif
            var errors = (SqlErrorCollection)typeof(SqlErrorCollection)
                .GetTypeInfo()
                .DeclaredConstructors
                .Single()
                .Invoke(null);

            typeof(SqlErrorCollection).GetRuntimeMethods().Single(m => m.Name == "Add").Invoke(errors, new object[] { error });

            var exceptionCtors = typeof(SqlException)
                .GetTypeInfo()
                .DeclaredConstructors;

            return (SqlException)exceptionCtors.First(c => c.GetParameters().Length == 4)
                .Invoke(new object[] { "Bang!", errors, null, Guid.NewGuid() });
        }
    }
}
