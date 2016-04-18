using System;
using System.Data.Entity;
using WebLib.Models;

namespace WebLib.InterfaceAppContext
{
    public interface IUserAppContext : IDisposable
    {
        DbSet<User> Users { get; }
        int SaveChanges();
        void MarkAsModified(User item);
    }
}