using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetSecu_DemoFormulaire.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace NetSecu_DemoFormulaire.Models.Domain.Configurations
{
    internal class UtilisateurConfig : IEntityTypeConfiguration<Utilisateur>
    {
        public void Configure(EntityTypeBuilder<Utilisateur> builder)
        {
            builder.ToTable(nameof(Utilisateur));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nom)
                .IsRequired()
                .HasColumnType("NVARCHAR(75)");

            builder.Property(x => x.Prenom)
                .IsRequired()
                .HasColumnType("NVARCHAR(75)");

            builder.Property(x => x.Prenom)
                .IsRequired()
                .HasColumnType("NVARCHAR(384)");

            builder.Property(x => x.Passwd)
                .IsRequired()
                .HasColumnType("BINARY(64)")
                .HasConversion(new ValueConverter<string, byte[]>(s => s.Hash(), byteArray => Convert.ToBase64String(byteArray)));
        }
    }
}
