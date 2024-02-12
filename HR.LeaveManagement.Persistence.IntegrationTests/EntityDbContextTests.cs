using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests
{
    public class EntityDbContextTests
    {
        private readonly EntityDbContext _entityDbContext;

        public EntityDbContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<EntityDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _entityDbContext = new EntityDbContext(dbOptions);
        }

        [Fact]
        public async void Save_SetDateCreatedValue_Success()
        {
            // Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            // Act
            await _entityDbContext.LeaveTypes.AddAsync(leaveType);
            await _entityDbContext.SaveChangesAsync();

            // Assert
            leaveType.DateCreated.ShouldNotBeNull();
        }

        [Fact]
        public async void Save_SetDateModifiedValue_Success()
        {
            // Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            // Act
            await _entityDbContext.LeaveTypes.AddAsync(leaveType);
            await _entityDbContext.SaveChangesAsync();

            // Assert
            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}