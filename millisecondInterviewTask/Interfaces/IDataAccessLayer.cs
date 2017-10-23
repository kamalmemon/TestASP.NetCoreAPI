using millisecondInterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace millisecondInterviewTask.Interfaces
{
    public interface IDataAccessLayer
    {
        Task CreateDocumentAsync(DeploymentRoot deploymentObject);
        List<DeploymentRoot> GetAllDocumentsAsync();
    }
}
