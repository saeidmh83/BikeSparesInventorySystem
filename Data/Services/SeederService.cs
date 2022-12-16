﻿using BikeSparesInventorySystem.Data.Models;
using BikeSparesInventorySystem.Data.Providers;
using BikeSparesInventorySystem.Data.Repositories;
using BikeSparesInventorySystem.Data.Utils;

namespace BikeSparesInventorySystem.Data.Services
{
    internal class SeederService
    {
        internal int MinUsers { get; set; } = 6;
        internal int MaxUsers { get; set; } = 10;
        internal int MinSpares { get; set; } = 12;
        internal int MaxSpares { get; set; } = 24;
        internal int MinActivityLogs { get; set; } = 30;
        internal int MaxActivityLogs { get; set; } = 80;

        private readonly Repository<User> _userRepository;
        private readonly Repository<Spare> _spareRepository;
        private readonly Repository<ActivityLog> _activityLogRepository;

        public SeederService(Repository<User> userRepository, Repository<Spare> spareRepository, Repository<ActivityLog> activityLogRepository)
        {
            _userRepository = userRepository;
            _spareRepository = spareRepository;
            _activityLogRepository = activityLogRepository;
        }

        public async Task SeedAsync()
        {
            var users = _userRepository._sourceData = Seeder.GenerateUsers(MinUsers, MaxUsers);
            var spares = _spareRepository._sourceData = Seeder.GenerateSpares(MinSpares, MaxSpares);
            _activityLogRepository._sourceData = Seeder.GenerateActivityLogs(users, spares, MinActivityLogs, MaxActivityLogs);
            await _userRepository.FlushAsync();
            await _spareRepository.FlushAsync();
            await _activityLogRepository.FlushAsync();
        }
    }
}
