using System;
using System.Data.Entity;
using WebLib.Models;

namespace WebLib.InterfaceAppContext
{
    public interface IInstallationAppContext : IDisposable
    {
        DbSet<Installation> Installations { get; }

        int SaveChanges();

        void MarkAsModified(Installation item);
    }
}