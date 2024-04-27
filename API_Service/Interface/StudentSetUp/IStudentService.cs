using AIS.Data.Model;
using AIS.Data.RequestResponseModel.StudentSetUp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.API_Service.Interface.StudentSetUp
{
   public interface IStudentSetupService
    {
        Task<IEnumerable<Student_List_Output_Model>> GET_Student_List(Student_List_Input_Para_Model student_List_Input_Para_Model);
        //Task<IEnumerable<StudentGeneric_Output_Model>> get_StudentGenericData_List(StudentGeneric_Input_Para_Model studentGeneric_Input_Para_Model);
        Task<StudentGeneric_Output_Model> get_StudentGenericData_List(StudentGeneric_Input_Para_Model studentGeneric_Input_Para_Model);

    }
}
