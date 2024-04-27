using AIS.Model;
using AIS.Model.MasterData;
using AIS.Model.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS.FrontEnd_07062021.Service.Interface
{
    public interface IStudentListService
    {
        Task<IEnumerable<StudentListModel>> GetStudentList(int ClassId,int SectionId, int SchoolId, int StudentId, int StatusId,string OperationType);
        Task<StudentDetailsListModel> GetStudentDetailsList( string SchoolCode, string FinancialYear,int ClassID,int SectionID, int StudentId, string OperationType);
        //Task<StudentListModel> GetStudentDetails(int ClassId, int SectionId, int SchoolId, int StudentId, int StatusId, string OperationType);

    }
}
