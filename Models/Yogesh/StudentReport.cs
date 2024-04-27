using System.Data;

namespace AdminDashboard.Server.Models.Yogesh
{
    public class StudentReport
    {
        public DataTable GetStudentInfo()
        {
            var dt = new DataTable();
            dt.Columns.Add("StudentId");
            dt.Columns.Add("StudentName");
            dt.Columns.Add("StudentAge");

            DataRow dr;
            for (int i = 1; i <= 5; i++)
            {
                dr = dt.NewRow();
                dr["StudentId"] = i;
                dr["StudentName"] = "Yogesh" + i;
                dr["StudentAge"] = "25";
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
