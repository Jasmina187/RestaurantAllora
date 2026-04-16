using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectData.Configurations
{
    public class EmployeeProfileConfiguration : IEntityTypeConfiguration<EmployeeProfile>
    {
        public void Configure(EntityTypeBuilder<EmployeeProfile> builder)
        {
            builder
                .HasOne(ep => ep.User)
                .WithOne(u => u.EmployeeProfile)
                .HasForeignKey<EmployeeProfile>(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
