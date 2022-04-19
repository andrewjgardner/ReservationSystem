﻿using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystemTests.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSystemTests.Fixtures
{
    public class NullDatabaseFixture
    {
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=ReservationSystemTests;Trusted_Connection=True";

        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public NullDatabaseFixture()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
        
                    }

                    _databaseInitialized = true;
                }
            }
        }

    }
}
