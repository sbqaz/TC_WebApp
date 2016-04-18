using System;
using System.Data.Entity;
using WebLib.Models;

namespace WebLib.InterfaceAppContext
{
    public interface IPositionAppContext : IDisposable
    {
        DbSet<Position> Positions { get; }

        int SaveChanges();

        void MarkAsModified(Position item);
    }
}