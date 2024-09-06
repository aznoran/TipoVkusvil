using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TipoVkusvil.DataAccess.Configurations;

public partial class UserRoleConfiguration
    : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.HasKey(r => new { r.UserId, r.RoleId });
    }
}