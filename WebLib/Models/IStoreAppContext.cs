using System;
using System.Data.Entity;
using WebLib.Models;

namespace StoreApp.Models
{
    public interface IStoreAppContext : IDisposable
    {
        DbSet<Case> Cases { get; }
        int SaveChanges();
        void MarkAsModified(Case item);
    }
}