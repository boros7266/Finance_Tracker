using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Tracker.Services
{
    public interface IProjectService
    {
        Task<Project> GetProjectById(Guid id);
        Task<Project[]> GetProject();
        void CreateProject([FromBody] Project project);
        Task<bool> DeleteProjectById(Guid id);
    }
}
