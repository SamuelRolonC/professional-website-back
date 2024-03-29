﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetByLanguageAsync(string language);
    }
}
