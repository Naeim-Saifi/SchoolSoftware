﻿using AdminDashboard.Server.API_Service.Interface.MasterDataSetUp;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.MasterDataSetUp;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.MasterData
{
    public class MasterDataListBase : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        [Inject]
        public IMasterDataSetupService masterDataSetupService { get; set; }

        //list Model        
        public List<Master_CLass_List_Output_Model> _cLass_List = new List<Master_CLass_List_Output_Model>();
        public List<Master_Section_List_Output_Model> _section_List = new List<Master_Section_List_Output_Model>();
        public List<Master_Subject_List_Output_Model> _subject_List = new List<Master_Subject_List_Output_Model>();
        public List<Master_Map_Subject_With_Class_List_Output_Model> _mapSubject_List = new List<Master_Map_Subject_With_Class_List_Output_Model>();
        public List<Master_Role_List_Output_Model> _role_List = new List<Master_Role_List_Output_Model>();
        public SessionModel sessionModel { get; }
        public List<string> toolclassBarItems = (new List<string>() {  "Print", "Search" });
        public List<string> toolsectionBarItems = (new List<string>() { "Print", "Search" });
        public List<string> toolsubjectBarItems = (new List<string>() { "Print", "Search" });
        public List<string> toolmapsubjectBarItems = (new List<string>() { "Add Subject", "Print", "Search" });
        public List<string> tooluserRoleBarItems = (new List<string>() {"Add Role", "Print", "Search" });
       
        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };
        public SfGrid<Master_CLass_List_Output_Model> sfclass;
        public SfGrid<Master_Section_List_Output_Model> sfsection;
        public SfGrid<Master_Subject_List_Output_Model> sfsubject;
        public SfGrid<Master_Map_Subject_With_Class_List_Output_Model> sfmapsubjectwithclass;
        public SfGrid<Master_Role_List_Output_Model> sfrole;
        public SessionModel _sessionModel;

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            {
                classId = 0, 
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _cLass_List = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();
            Master_Section_List_Input_Para_Model master_section_List_Input_Para_Model = new Master_Section_List_Input_Para_Model()
            {
                sectionId = 0, 
                userId = _sessionModel.UserId,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _section_List = (await masterDataSetupService.GET_Master_SectionLIST(master_section_List_Input_Para_Model)).ToList();

            Master_Subject_List_Input_Para_Model master_Subject_List_Input_Para_Model = new Master_Subject_List_Input_Para_Model()
            {
                 subjectID = 0,                  
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _subject_List = (await masterDataSetupService.GET_Master_SubjectLIST(master_Subject_List_Input_Para_Model)).ToList();

            Master_Map_Subject_With_ClassList_Input_Para_Model master_Map_Subject_With_ClassList_Input_Para_Model = new Master_Map_Subject_With_ClassList_Input_Para_Model()
            {
                 classID = 0, userId = _sessionModel.UserId,
                subjectID = 0,
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                 
                reportType = ReportType.All
            };
            _mapSubject_List = (await masterDataSetupService.GET_Mapp_SubjectwithClassLIST(master_Map_Subject_With_ClassList_Input_Para_Model)).ToList();

            Master_Role_List_Input_Para_Model master_Role_List_Input_Para_Model = new Master_Role_List_Input_Para_Model()
            {
                 roleID = 0,                  
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                reportType = ReportType.All
            };
            _role_List = (await masterDataSetupService.GET_Master_RoleList(master_Role_List_Input_Para_Model)).ToList();

        }
        /*Master Role Code start*/

        public RoleMasterViewModel roleMasterViewModel = new RoleMasterViewModel();

        
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;
        public bool IsVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        //this model use for send data to API ,binding view model with API model
        public MasterRoleAPIModel  masterRoleAPIModel { get; set; }
        
        public string OperationType = "";

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Role")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add New Role Details";
                OperationType = "Add";
                HeaderText = "Add Role";
                ddEnable = true;
                roleMasterViewModel.roleName = null;
                roleMasterViewModel.roleDescription = null;
                roleMasterViewModel.displayOrder = 0;
                
            }
        }
        public void EditRoleMaster(CommandClickEventArgs<Master_Role_List_Output_Model> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Role Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                ddEnable = false;
                roleMasterViewModel = new RoleMasterViewModel()
                {
                    masterRoleId=args.RowData.masterRoleId,
                    displayOrder=Convert.ToInt16(args.RowData.displayOrder),
                     roleDescription = args.RowData.roleDescription,
                      roleName = args.RowData.roleName,
                     
                };
            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Unit Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                roleMasterViewModel = new RoleMasterViewModel()
                {
                    masterRoleId = args.RowData.masterRoleId,
                    displayOrder = Convert.ToInt16(args.RowData.displayOrder),
                    roleDescription = args.RowData.roleDescription,
                    roleName = args.RowData.roleName,

                };
            }

        }
        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                masterRoleAPIModel = new MasterRoleAPIModel()
                {
                    masterRoleId= roleMasterViewModel.masterRoleId,
                    roleName = roleMasterViewModel.roleName,
                    roleDescription= roleMasterViewModel.roleDescription,
                    displayOrder=roleMasterViewModel.displayOrder.ToString(),
                    CreatedByUserId = _sessionModel.UserId,
                    UpdatedByUserId = _sessionModel.UserId,
                    SchoolCode = _sessionModel.SchoolCode,
                    FinancialYear = _sessionModel.FinancialYear,
                     
                };

                if (OperationType == "Add")
                {
                    masterRoleAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    masterRoleAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    masterRoleAPIModel.OperationType = OperationAction.Delete;
                }
                RoleSave(masterRoleAPIModel);
            };
        }

        private async void RoleSave(MasterRoleAPIModel  masterRoleAPIModel)
        {
            try
            {
                if (masterRoleAPIModel.OperationType != "NA")
                {
                    aPIReturnModel = (await masterDataSetupService.CRUD_MasterRole(masterRoleAPIModel));

                    if (aPIReturnModel.status == false)
                    {
                        if (masterRoleAPIModel.OperationType == "Add")
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Success);
                        }
                        else if (masterRoleAPIModel.OperationType == "Update")
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Info);
                        }
                        else
                        {
                            snackBar.Add(aPIReturnModel.message, Severity.Error);
                        }
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                    Master_Role_List_Input_Para_Model master_Role_List_Input_Para_Model = new Master_Role_List_Input_Para_Model()
                    {
                        roleID = 0,
                        financialYear = _sessionModel.FinancialYear,
                        schoolCode = _sessionModel.SchoolCode,
                        reportType = ReportType.All
                    };
                    _role_List = (await masterDataSetupService.GET_Master_RoleList(master_Role_List_Input_Para_Model)).ToList();
                    StateHasChanged();
                    ClearData();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void ClearData()
        {
            roleMasterViewModel = new RoleMasterViewModel();

        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
        public APIReturnModel aPIReturnModel;
        [Inject]
        public ISnackbar snackBar { get; set; }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
    }
}
