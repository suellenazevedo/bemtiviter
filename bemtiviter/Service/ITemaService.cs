﻿using bemtiviter.Model;

namespace bemtiviter.Service
{
    public interface ITemaService
    {
        Task<IEnumerable<Tema>> GetAll();

        Task<Tema?> GetById(long id);

        Task<Tema> Create(Tema tema);

        Task<Tema?> Update(Tema tema);

        Task Delete(Tema tema);
    }
}
