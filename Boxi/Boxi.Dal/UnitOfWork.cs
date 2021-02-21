﻿using System;
using System.Threading.Tasks;
using Boxi.Core.Domain;
using Boxi.Dal.Interfaces;
using Boxi.Dal.Models;
using Boxi.Dal.Repositories;

namespace Boxi.Dal
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly BoxiDataContext _context;

        public UnitOfWork(BoxiDataContext dataContext, IBoxRepository boxStoreRepository
          , IBaseRepository<Item> itemRepository)
        {
            _context = dataContext;
            BoxRepo = boxStoreRepository;
            ItemRepo = itemRepository;
        }

        /// <summary>
        /// Constructor used for UnitTests;
        /// </summary>
        /// <param name="dataContext"></param>
        public UnitOfWork(BoxiDataContext dataContext)
        {
            _context = dataContext;
            BoxRepo ??= new BoxRepository(dataContext);
            ItemRepo ??= new BaseRepository<Item>(dataContext);
        }

        public IBaseRepository<Item> ItemRepo { get; }
        public IBoxRepository BoxRepo { get; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _context?.Dispose();
        }
    }
}