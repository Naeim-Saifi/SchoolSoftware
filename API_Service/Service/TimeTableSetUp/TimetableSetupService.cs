using AdminDashboard.Server.API_Service.Interface.TimeTableSetUp;
using AdminDashboard.Server.Service.Interface.LocalStorage;
using AIS.Data.APIReturnModel;
using AIS.Data.Model;
using AIS.Data.RequestResponseModel.Employee;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Data.RequestResponseModel.TimeTableSetUp;
using AIS.Model.GeneralConversion;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.API_Service.Service.TimeTableSetUp
{
    public class TimeTableSetupService : ITimeTableSetUpService
    {
        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public TimeTableSetupService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            _localStorageService = localStorageService;
        }
        public async Task<APIReturnModel> CRUD_TimeTableSetup(TimetableModel timetableModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/TimeTableSetUp/TimeTableSetUp", timetableModel);
            return response;
        }
        public async Task<APIReturnModel> CRUD_PeriodMaster(PeriodMasterModel periodMasterModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/TimeTableSetUp/CRUD_PeriodMaster", periodMasterModel);
            return response;
        }
        public async Task<IEnumerable<TimeTableOutputModel>> Get_Time_Table_List(Time_Table_Input_Para_Model time_Table_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TimeTableSetUp/Get_Time_Table_List?SchoolCode={time_Table_Input_Para_Model.schoolCode}" +
                      $"&classId={time_Table_Input_Para_Model.classId}" +
                      $"&sectionID={time_Table_Input_Para_Model.sectionID}" +
                      $"&subjectID={time_Table_Input_Para_Model.subjectID}" +
                      $"&userID={time_Table_Input_Para_Model.userID}" +
                      $"&dayId={time_Table_Input_Para_Model.dayId}" +
                      $"&periodId={time_Table_Input_Para_Model.periodId}" +
                      $"&FinancialYear={time_Table_Input_Para_Model.financialYear}" +
                      $"&reportType={time_Table_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<TimeTableOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<IEnumerable<PeriodOutputModel>> Get_Period_List(Period_Input_Para_Model period_Input_Para_Model)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var r = await httpClient.GetJsonAsync<Response>($"/api/TimeTableSetUp/Get_Period_List?SchoolCode={period_Input_Para_Model.schoolCode}" +
                    $"&periodId={period_Input_Para_Model.periodId}" +
                    $"&FinancialYear={period_Input_Para_Model.financialYear}" +
                    $"&reportType={period_Input_Para_Model.reportType}");
                JsonElement el;
                var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.Data.GetRawText()).TryGetProperty("data", out el);

                if (r.IsError == false)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<PeriodOutputModel[]>(el.GetRawText());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
        public async Task<IEnumerable<DayModelList>> GetDaysNameList()
        {
            var dayList = new List<DayModelList>() {
                new DayModelList(){ DayId = 1, DayName="Sunday"},
                new DayModelList(){ DayId = 2, DayName="Monday"},
                new DayModelList(){ DayId = 3, DayName="Tuesday"},
                new DayModelList(){ DayId = 4, DayName="Wednesday"},
                new DayModelList(){ DayId = 5, DayName="Thursday"},
                new DayModelList(){ DayId = 6, DayName="Friday"},
                new DayModelList(){ DayId = 7, DayName="Saturday"},

            };

            return dayList;
        }



        public List<TimeTableOutputModel> _userWiseTimeTableList = new List<TimeTableOutputModel>();

        //public List<TimeTableOutputModel> _userWiseTimeTableList = new List<TimeTableOutputModel>();
        public List<TimeTableOutputModel> _userWiseDayTimeTableList = new List<TimeTableOutputModel>();
        public List<TimeTableOutputModel> _userWisePeriodTimeTableList = new List<TimeTableOutputModel>();
        public List<TeacherTimeTableView> _tacherTimeTableView = new List<TeacherTimeTableView>();

        public List<TimeTableTabularFormate> _timetableTablurFormate = new List<TimeTableTabularFormate>();
        public List<ClassSectionTimeTableView> _classSectionTimeTableView = new List<ClassSectionTimeTableView>();
        //public List<PeriodOutputModel> _periodListModels = new List<PeriodOutputModel>(); 

        public async Task<IEnumerable<TimeTableTabularFormate>> GetTimeTableTabularFormat(string _ReportType, int UserID, int _ClassID, int _SectionID,
            List<DayModelList> dayModelLists,
            List<PeriodOutputModel> _periodListModels,
            IEnumerable<TimeTableOutputModel> timeTableOutputModels)
        {


            //step-1 get days wise details
            string _teacherName = string.Empty;
            string _dayName = string.Empty;
            string _periodName = string.Empty;
            string _classSection = string.Empty;
            string _subjectName = string.Empty;
            string _startTime = string.Empty;
            string _endTime = string.Empty;
            string _className = string.Empty;
            string _sectionName = string.Empty;
            int periodID = 0;

            string _period1 = string.Empty;
            string _period2 = string.Empty;
            string _period3 = string.Empty;
            string _period4 = string.Empty;
            string _period5 = string.Empty;
            string _period6 = string.Empty;
            string _period7 = string.Empty;
            string _period8 = string.Empty;
            string _period9 = string.Empty;
            string periodDetails = string.Empty;
            int totalPeriod = 0;

            try
            {
                if (_timetableTablurFormate.Count > 0)
                { _timetableTablurFormate.Clear(); }
                

                if (_ReportType == "SingleTeacher")
                {
                    _userWiseTimeTableList = timeTableOutputModels.Where(m => m.teacherid == UserID).ToList();
                }
                //Single Student class and section wise
                else
                {
                    _userWiseTimeTableList = timeTableOutputModels.Where(m => m.classID == _ClassID && m.sectionID == _SectionID).ToList();
                }

                for (int iday = 1; iday <= dayModelLists.Count; iday++)
                {
                    // single Teacher table formate 


                    _userWiseDayTimeTableList = _userWiseTimeTableList.Where(m => m.dayID == iday).ToList();

                    // student wise time table formate.

                    if (_userWiseDayTimeTableList.Count > 0)
                    {
                        for (int iperioid = 0; iperioid <= _periodListModels.Count - 1; iperioid++)
                        {
                            periodID = _periodListModels[iperioid].periodId;
                            _userWisePeriodTimeTableList = _userWiseDayTimeTableList.Where(m => m.periodId == periodID).ToList();
                            if (_userWisePeriodTimeTableList.Count > 0)
                            {
                                _teacherName = _userWisePeriodTimeTableList[0].teacherName.ToString();
                                _dayName = _userWisePeriodTimeTableList[0].dayNameDetails.ToString();
                                _classSection = _userWisePeriodTimeTableList[0].className.ToString() + '-' + _userWisePeriodTimeTableList[0].sectionName.ToString(); ;
                                _startTime = _userWisePeriodTimeTableList[0].startTime.ToString();
                                _endTime = _userWisePeriodTimeTableList[0].endTime.ToString();
                                _subjectName = _userWisePeriodTimeTableList[0].subjectName.ToString();
                                _className = _userWisePeriodTimeTableList[0].className.ToString();
                                _sectionName = _userWisePeriodTimeTableList[0].sectionName.ToString();

                                if (_ReportType == "SingleTeacher")
                                {
                                    periodDetails =
                                    // _userWisePeriodTimeTableList[0].teacherName.ToString()
                                     _userWisePeriodTimeTableList[0].className.ToString() + '-'
                                    + ' ' + _userWisePeriodTimeTableList[0].sectionName.ToString()
                                    + ' ' + _userWisePeriodTimeTableList[0].subjectName.ToString()
                                    + ' ' + _userWisePeriodTimeTableList[0].startTime.ToString()
                                    + ' ' + _userWisePeriodTimeTableList[0].endTime.ToString();
                                }
                                else
                                {
                                    periodDetails =
                                    _userWisePeriodTimeTableList[0].teacherName.ToString()                                   
                                   + ' ' + _userWisePeriodTimeTableList[0].subjectName.ToString()
                                   + ' ' + _userWisePeriodTimeTableList[0].startTime.ToString()
                                   + ' ' + _userWisePeriodTimeTableList[0].endTime.ToString();
                                }


                                if (periodID == 1)
                                {
                                    _period1 = periodDetails.ToString(); periodDetails = string.Empty;
                                    totalPeriod = totalPeriod + 1;
                                }
                                else if (periodID == 2)
                                { _period2 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                else if (periodID == 3)
                                { _period3 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                else if (periodID == 4)
                                { _period4 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                else if (periodID == 5)
                                { _period5 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                else if (periodID == 6)
                                { _period6 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                else if (periodID == 7)
                                { _period7 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                else if (periodID == 8)
                                { _period8 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                else if (periodID == 9)
                                { _period9 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }


                            }

                        }
                        //if (_ReportType == "SingleTeacher")
                        //{
                            _timetableTablurFormate.Add(new TimeTableTabularFormate
                            {
                                teacherName = _teacherName,
                                className = _className,
                                sectionName = _sectionName,
                                dayName = _dayName,
                                period1 = _period1,
                                period2 = _period2,
                                period3 = _period3,
                                period4 = _period4,
                                period5 = _period5,
                                period6 = _period6,
                                period7 = _period7,
                                period8 = _period8,
                                period9 = _period9,
                                totalPeriod = totalPeriod,
                            });
                        
                        _period1 = string.Empty;
                        _period2 = string.Empty;
                        _period3 = string.Empty;
                        _period4 = string.Empty;
                        _period5 = string.Empty;
                        _period6 = string.Empty;
                        _period7 = string.Empty;
                        _period8 = string.Empty;
                        totalPeriod = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                //return ex.Message.ToString();
            }

            return _timetableTablurFormate;

        }



        public async Task<IEnumerable<TimeTableTabularFormate>> GetTimeTableTabularFormatAll( string _RepotType,
                                                                        List<Employee_User_List_Output_Model> userList,
                                                                        List<Master_CLass_List_Output_Model> classList,
                                                                        List<Master_Section_List_Output_Model> sectionList,
           List<DayModelList> dayModelLists,
           List<PeriodOutputModel> _periodListModels,
           IEnumerable<TimeTableOutputModel> timeTableOutputModels)
        {


            //step-1 get days wise details
            string _teacherName = string.Empty;
            string _dayName = string.Empty;
            string _periodName = string.Empty;
            string _classSection = string.Empty;
            string _subjectName = string.Empty;
            string _startTime = string.Empty;
            string _endTime = string.Empty;
            string _className = string.Empty;
            string _sectionName = string.Empty;
            int periodID = 0;

            string _period1 = string.Empty;
            string _period2 = string.Empty;
            string _period3 = string.Empty;
            string _period4 = string.Empty;
            string _period5 = string.Empty;
            string _period6 = string.Empty;
            string _period7 = string.Empty;
            string _period8 = string.Empty;
            string _period9 = string.Empty;
            string periodDetails = string.Empty;
            int totalPeriod = 0;


            //For all

            int _UserId= 0;
            int _classID = 0, _sectionID = 0;
            try
            {
                if (_timetableTablurFormate.Count > 0)
                { _timetableTablurFormate.Clear(); }


                if (_RepotType == "AllTeacher")
                {
                    for(int iuserCount=0;iuserCount<= userList.Count;iuserCount++)
                    {
                        _UserId = Convert.ToInt16( userList[iuserCount].id);

                        _userWiseTimeTableList = timeTableOutputModels.Where(m => m.teacherid == _UserId).ToList();
                        for (int iday = 1; iday <= dayModelLists.Count; iday++)
                        {
                            // single Teacher table formate 


                            _userWiseDayTimeTableList = _userWiseTimeTableList.Where(m => m.dayID == iday).ToList();

                            // student wise time table formate.

                            if (_userWiseDayTimeTableList.Count > 0)
                            {
                                for (int iperioid = 0; iperioid <= _periodListModels.Count - 1; iperioid++)
                                {
                                    periodID = _periodListModels[iperioid].periodId;
                                    _userWisePeriodTimeTableList = _userWiseDayTimeTableList.Where(m => m.periodId == periodID).ToList();
                                    if (_userWisePeriodTimeTableList.Count > 0)
                                    {
                                        _teacherName = _userWisePeriodTimeTableList[0].teacherName.ToString();
                                        _dayName = _userWisePeriodTimeTableList[0].dayNameDetails.ToString();
                                        _classSection = _userWisePeriodTimeTableList[0].className.ToString() + '-' + _userWisePeriodTimeTableList[0].sectionName.ToString(); ;
                                        _startTime = _userWisePeriodTimeTableList[0].startTime.ToString();
                                        _endTime = _userWisePeriodTimeTableList[0].endTime.ToString();
                                        _subjectName = _userWisePeriodTimeTableList[0].subjectName.ToString();
                                        _className = _userWisePeriodTimeTableList[0].className.ToString();
                                        _sectionName = _userWisePeriodTimeTableList[0].sectionName.ToString();

                                        if (_RepotType == "AllTeacher")
                                        {
                                            periodDetails =
                                             // _userWisePeriodTimeTableList[0].teacherName.ToString()
                                             _userWisePeriodTimeTableList[0].className.ToString() + '-'
                                            + ' ' + _userWisePeriodTimeTableList[0].sectionName.ToString()
                                            + ' ' + _userWisePeriodTimeTableList[0].subjectName.ToString()
                                            + ' ' + _userWisePeriodTimeTableList[0].startTime.ToString()
                                            + ' ' + _userWisePeriodTimeTableList[0].endTime.ToString();
                                        }
                                        else
                                        {
                                            periodDetails =
                                            _userWisePeriodTimeTableList[0].teacherName.ToString()
                                           + ' ' + _userWisePeriodTimeTableList[0].subjectName.ToString()
                                           + ' ' + _userWisePeriodTimeTableList[0].startTime.ToString()
                                           + ' ' + _userWisePeriodTimeTableList[0].endTime.ToString();
                                        }


                                        if (periodID == 1)
                                        {
                                            _period1 = periodDetails.ToString(); periodDetails = string.Empty;
                                            totalPeriod = totalPeriod + 1;
                                        }
                                        else if (periodID == 2)
                                        { _period2 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                        else if (periodID == 3)
                                        { _period3 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                        else if (periodID == 4)
                                        { _period4 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                        else if (periodID == 5)
                                        { _period5 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                        else if (periodID == 6)
                                        { _period6 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                        else if (periodID == 7)
                                        { _period7 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                        else if (periodID == 8)
                                        { _period8 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                        else if (periodID == 9)
                                        { _period9 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }


                                    }

                                }
                                //if (_ReportType == "SingleTeacher")
                                //{
                                _timetableTablurFormate.Add(new TimeTableTabularFormate
                                {
                                    teacherName = _teacherName,
                                    className = _className,
                                    sectionName = _sectionName,
                                    dayName = _dayName,
                                    period1 = _period1,
                                    period2 = _period2,
                                    period3 = _period3,
                                    period4 = _period4,
                                    period5 = _period5,
                                    period6 = _period6,
                                    period7 = _period7,
                                    period8 = _period8,
                                    period9 = _period9,
                                    totalPeriod = totalPeriod,
                                });

                                _period1 = string.Empty;
                                _period2 = string.Empty;
                                _period3 = string.Empty;
                                _period4 = string.Empty;
                                _period5 = string.Empty;
                                _period6 = string.Empty;
                                _period7 = string.Empty;
                                _period8 = string.Empty;
                                _period9 = string.Empty;
                                totalPeriod = 0;

                            }
                        }
                    }
                    
                }
                //Single Student class and section wise
                else
                {
                    for(int iclassID=0;iclassID<= classList.Count;iclassID++)
                    {
                        _classID = Convert.ToInt32(classList[iclassID].classId);
                        for(int isectionID=0;isectionID< sectionList.Count;isectionID++)
                        {
                            _sectionID = Convert.ToInt16(sectionList[isectionID].sectionId);
                            _userWiseTimeTableList = timeTableOutputModels.Where(m => m.classID == _classID && m.sectionID == _sectionID).ToList();
                            for (int iday = 1; iday <= dayModelLists.Count; iday++)
                            {
                                // single Teacher table formate 
                                _userWiseDayTimeTableList = _userWiseTimeTableList.Where(m => m.dayID == iday).ToList();
                                // student wise time table formate.

                                if (_userWiseDayTimeTableList.Count > 0)
                                {
                                    for (int iperioid = 0; iperioid <= _periodListModels.Count - 1; iperioid++)
                                    {
                                        periodID = _periodListModels[iperioid].periodId;
                                        _userWisePeriodTimeTableList = _userWiseDayTimeTableList.Where(m => m.periodId == periodID).ToList();
                                        if (_userWisePeriodTimeTableList.Count > 0)
                                        {
                                            _teacherName = _userWisePeriodTimeTableList[0].teacherName.ToString();
                                            _dayName = _userWisePeriodTimeTableList[0].dayNameDetails.ToString();
                                            _classSection = _userWisePeriodTimeTableList[0].className.ToString() + '-' + _userWisePeriodTimeTableList[0].sectionName.ToString(); ;
                                            _startTime = _userWisePeriodTimeTableList[0].startTime.ToString();
                                            _endTime = _userWisePeriodTimeTableList[0].endTime.ToString();
                                            _subjectName = _userWisePeriodTimeTableList[0].subjectName.ToString();
                                            _className = _userWisePeriodTimeTableList[0].className.ToString();
                                            _sectionName = _userWisePeriodTimeTableList[0].sectionName.ToString();

                                            if (_RepotType == "AllTeacher")
                                            {
                                                periodDetails =
                                                 // _userWisePeriodTimeTableList[0].teacherName.ToString()
                                                 _userWisePeriodTimeTableList[0].className.ToString() + '-'
                                                + ' ' + _userWisePeriodTimeTableList[0].sectionName.ToString()
                                                + ' ' + _userWisePeriodTimeTableList[0].subjectName.ToString()
                                                + ' ' + _userWisePeriodTimeTableList[0].startTime.ToString()
                                                + ' ' + _userWisePeriodTimeTableList[0].endTime.ToString();
                                            }
                                            else
                                            {
                                                periodDetails =
                                                _userWisePeriodTimeTableList[0].teacherName.ToString()
                                               + ' ' + _userWisePeriodTimeTableList[0].subjectName.ToString()
                                               + ' ' + _userWisePeriodTimeTableList[0].startTime.ToString()
                                               + ' ' + _userWisePeriodTimeTableList[0].endTime.ToString();
                                            }


                                            if (periodID == 1)
                                            {
                                                _period1 = periodDetails.ToString(); periodDetails = string.Empty;
                                                totalPeriod = totalPeriod + 1;
                                            }
                                            else if (periodID == 2)
                                            { _period2 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                            else if (periodID == 3)
                                            { _period3 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                            else if (periodID == 4)
                                            { _period4 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                            else if (periodID == 5)
                                            { _period5 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                            else if (periodID == 6)
                                            { _period6 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                            else if (periodID == 7)
                                            { _period7 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                            else if (periodID == 8)
                                            { _period8 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }
                                            else if (periodID == 9)
                                            { _period9 = periodDetails.ToString(); periodDetails = string.Empty; totalPeriod = totalPeriod + 1; }


                                        }

                                    }
                                    //if (_ReportType == "SingleTeacher")
                                    //{
                                    _timetableTablurFormate.Add(new TimeTableTabularFormate
                                    {
                                        teacherName = _teacherName,
                                        className = _className,
                                        sectionName = _sectionName,
                                        dayName = _dayName,
                                        period1 = _period1,
                                        period2 = _period2,
                                        period3 = _period3,
                                        period4 = _period4,
                                        period5 = _period5,
                                        period6 = _period6,
                                        period7 = _period7,
                                        period8 = _period8,
                                        period9 = _period9,
                                        totalPeriod = totalPeriod,
                                    });

                                    _period1 = string.Empty;
                                    _period2 = string.Empty;
                                    _period3 = string.Empty;
                                    _period4 = string.Empty;
                                    _period5 = string.Empty;
                                    _period6 = string.Empty;
                                    _period7 = string.Empty;
                                    _period8 = string.Empty;
                                    _period9 = string.Empty;
                                    totalPeriod = 0;

                                }
                            }
                        }
                       
                    }
                    
                }

               
            }
            catch (Exception ex)
            {
                //return ex.Message.ToString();
            }

            return _timetableTablurFormate;

        }

    }
}



         
