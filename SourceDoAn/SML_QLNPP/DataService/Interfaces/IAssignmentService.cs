using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace DataService.Interfaces
{
    public interface IAssignmentService
    {
        bool CreateAssignment(Assignment assig);
        bool DeleteAssignment(Assignment assig);
        Assignment GetAssignment(int idStaff, int idDis);
    }
}
