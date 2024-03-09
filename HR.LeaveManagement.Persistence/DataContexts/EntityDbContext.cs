using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DataContexts
{
	public class EntityDbContext : DbContext
	{
        private readonly IUserService _userService;

        public EntityDbContext(
            DbContextOptions<EntityDbContext> options,
            IUserService userService) : base(options)
		{
            _userService = userService;
        }

		public DbSet<LeaveType> LeaveTypes { get; set; }
		public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
		public DbSet<LeaveRequest> LeaveRequests { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach(var entry in base.ChangeTracker.Entries<BaseEntity>()
				.Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
			{
				entry.Entity.DateModified = DateTime.UtcNow;
				entry.Entity.ModifiedBy = _userService.UserId;

				if(entry.State == EntityState.Added)
				{
					entry.Entity.DateCreated = DateTime.UtcNow;
					entry.Entity.CreatedBy = _userService.UserId;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
