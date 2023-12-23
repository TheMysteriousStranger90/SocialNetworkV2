using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasOne(m => m.Recipient).WithMany(a => a.MessagesReceived)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(m => m.Sender).WithMany(a => a.MessagesSent)
            .OnDelete(DeleteBehavior.Restrict);
    }
}