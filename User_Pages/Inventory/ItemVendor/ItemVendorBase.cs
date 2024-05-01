using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.Inventory.ItemVender;
using AIS.Model.GeneralConversion;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminDashboard.Server.User_Pages.Inventory.ItemVendor
{
    public class ItemVendorBase:ComponentBase
    {


        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Inject]
        public ISnackbar snackBar { get; set; }



        public ItemVenderViewModel itemVenderViewModel = new ItemVenderViewModel();
        public List<ItemVenderListModel> _ItemVenderListModel=new List<ItemVenderListModel>();
        public ItemVenderAPIModel itemVenderAPIModel { get; set; }


        public SfGrid<ItemVenderListModel> sfVendorDetails;
        public DialogEffect AnimationEffect = DialogEffect.Zoom;
        public string HeaderStyles { get; set; } = "e-background e-accent";
        public SfDialog DialogRef;


        public bool IsVisible { get; set; } = false;
        public bool IsDeleteVisible { get; set; } = false;
        public string DialogHeaderName = string.Empty;
        public bool ddEnable = true;
        public string btncss = "";
        public string HeaderText = string.Empty;
        public string OperationType = "";

        public APIReturnModel aPIReturnModel;


        public class venderSupply
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<venderSupply> venderSupplyDetails = new List<venderSupply>
        {
            new venderSupply(){Id=1,Value="Cloth"},
            new venderSupply(){Id=2,Value="Staionary"},
        };




        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

        public List<string> EnquirytoolBarItems = (new List<string>() { "Add Vendor", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });







        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            //Master_CLass_List_Input_Para_Model master_CLass_List_Input_Para_Model = new Master_CLass_List_Input_Para_Model()
            //{
            //    classId = 0,
            //    userId = _sessionModel.UserId,
            //    financialYear = _sessionModel.FinancialYear,
            //    schoolCode = _sessionModel.SchoolCode,
            //    reportType = ReportType.All
            //};
            //_classList = (await masterDataSetupService.GET_Master_ClassLIST(master_CLass_List_Input_Para_Model)).ToList();

            ItemVenderParaModel itemVenderParaModel = new ItemVenderParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                VednorId = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

           // _ItemVenderListModel = (await enquiryService.GET_EnquiryDetails_List(itemVenderParaModel)).ToList();
        }





        public void EditVendorDetails(CommandClickEventArgs<ItemVenderListModel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Enquiry Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                // ddEnable = false;
                itemVenderViewModel = new ItemVenderViewModel()
                {

                    //enquiryID = args.RowData.enquiryID,
                    //studentName = args.RowData.studentName,
                    //fatherName = args.RowData.fatherName,
                    //fMobileNo = args.RowData.fMobileNo,
                    //motherName = args.RowData.motherName,
                    //mMobileNo = args.RowData.mMobileNo,
                    //Gender = args.RowData.genderID.ToString(),
                    //PreviousSchoolDetails = args.RowData.previousSchoolName,
                    //DOB = args.RowData.dob,
                    //VisitingRemarks = args.RowData.visitingRemrks,
                    //VisitingType = args.RowData.visitingType.ToString(),
                    //VisitingStatus = args.RowData.visitingStatus.ToString(),
                    //EmailId = args.RowData.emailID,

                    //NextClass = args.RowData.previousClassid.ToString(),
                    //Address = args.RowData.address,

                };


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Enquiry Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                itemVenderViewModel = new ItemVenderViewModel()
                {

                    //enquiryID = args.RowData.enquiryID,
                    //studentName = args.RowData.studentName,
                    //fatherName = args.RowData.fatherName,
                    //fMobileNo = args.RowData.fMobileNo,
                    //motherName = args.RowData.motherName,
                    //mMobileNo = args.RowData.mMobileNo,
                    //Gender = args.RowData.genderID.ToString(),
                    //PreviousSchoolDetails = args.RowData.previousSchoolName,
                    //DOB = args.RowData.dob,
                    //VisitingRemarks = args.RowData.visitingRemrks,
                    //VisitingType = args.RowData.visitingType.ToString(),
                    //VisitingStatus = args.RowData.visitingStatus.ToString(),
                    //EmailId = args.RowData.emailID,
                    //NextClass = args.RowData.previousClassid.ToString(),
                    //Address = args.RowData.address,

                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Vendor")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Enquiry Details";
                OperationType = "Add";
                HeaderText = "Add Enquiry";
                ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfVendorDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfVendorDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfVendorDetails.ExpandAllGroupAsync();
            }
        }










        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                itemVenderAPIModel = new ItemVenderAPIModel()
                {
                    //EnquiryID = Convert.ToInt16(enquiryViewModel.enquiryID),
                    //StudentName = enquiryViewModel.studentName,
                    //FatherName = enquiryViewModel.fatherName,
                    //FMobileNo = enquiryViewModel.fMobileNo,
                    //MotherName = enquiryViewModel.motherName,
                    //MMobileNo = enquiryViewModel.mMobileNo,
                    //GenderID = Convert.ToInt32(enquiryViewModel.Gender),
                    //PreviousSchoolName = enquiryViewModel.PreviousSchoolDetails,
                    //Dob = Convert.ToDateTime(enquiryViewModel.DOB),
                    //VisitingRemrks = enquiryViewModel.VisitingRemarks,
                    //VisitingType = Convert.ToInt32(enquiryViewModel.VisitingType),
                    //VisitingStatus = Convert.ToInt32(enquiryViewModel.VisitingStatus),
                    //EmailID = enquiryViewModel.EmailId,
                    ////CurrentClassID = Convert.ToInt32(enquiryViewModel.CurrentClass),
                    //PromotedClassID = Convert.ToInt32(enquiryViewModel.NextClass),
                    //Address = enquiryViewModel.Address,
                    ////operationType = "Add",
                    //SchoolCode = _sessionModel.SchoolCode,
                    //FinancialYear = _sessionModel.FinancialYear,
                    //CreatedByUserId = _sessionModel.UserId

                };
                if (OperationType == "Add")
                {
                    itemVenderAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    itemVenderAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    itemVenderAPIModel.OperationType = OperationAction.Delete;
                }

                SaveEnquiryDetails(itemVenderAPIModel);

            }
            else
            {
                // Form has invalid inputs.
            }

        }
        private async void SaveEnquiryDetails(ItemVenderAPIModel itemVenderAPIModel)
        {
            try
            {
                if (itemVenderAPIModel.OperationType != "NA")
                {
                  //  aPIReturnModel = await enquiryService.CRUD_EnquiryDetails(itemVenderAPIModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        ItemVenderParaModel itemVenderParaModel = new ItemVenderParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            VednorId = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                    //    _ItemVenderListModel = (await enquiryService.GET_EnquiryDetails_List(itemVenderParaModel)).ToList();

                        ClearData();
                        StateHasChanged();
                    }
                    else
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Error);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }




        private void ClearData()
        {
            itemVenderViewModel = new ItemVenderViewModel();

        }
        public void ShowDialog()
        {
            IsVisible = true;
        }
        public void onOpen(Syncfusion.Blazor.Popups.BeforeOpenEventArgs args)
        {
            // setting maximum height to the Dialog
            args.MaxHeight = "750px";

        }
        public async Task CloseDialog()
        {
            IsVisible = false;
            await this.DialogRef.HideAsync();
        }
    }
}
