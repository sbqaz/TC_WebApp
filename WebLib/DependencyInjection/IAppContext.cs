using System;
using System.Data.Entity;
using WebLib.Models;

namespace WebLib.DependencyInjection
{
    public interface IAppContext : IDisposable
    {
        DbSet<Case> Cases { get; } 
        DbSet<Installation> Installations { get; }
        DbSet<Position> Positions { get; }
        DbSet<User> Users { get; }
        DbSet<Notification> Notifications { get; } 
        int SaveChanges();
        void MarkAsModified(Case item);
        void MarkAsModified(Installation item);
        void MarkAsModified(Position item);
        void MarkAsModified(User item);
        void MarkAsModified(Notification item);
    }
}