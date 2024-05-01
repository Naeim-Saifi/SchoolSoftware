//using AdminDashboard.Server.API_Service.Service.Inventory;
using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Enquiry;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
//using AIS.Data.RequestResponseModel.Inventory.ItemVender;
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
//using AdminDashboard.Server.API_Service.Interface.Inventory;
using System.Linq;


namespace AdminDashboard.Server.User_Pages.Inventory.ItemMaster
{
    public class ItemMasterBase: ComponentBase
    { 

        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Inject]
        public ISnackbar snackBar { get; set; }


       //[Inject]
       // public IInventoryService inventoryService { get; set; }

       

        public ItemMasterViewModel itemMasterViewModel = new ItemMasterViewModel();
        public List<ItemMasterListMoel> _ItemMasterListMoel = new List<ItemMasterListMoel>();
        public ItemMasterAPIModel itemMasterAPIModel { get; set; }


        public SfGrid<ItemMasterListMoel> sfItemDetails;
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

        public List<string> EnquirytoolBarItems = (new List<string>() { "Add Item", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });



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

            ItemMasterParaModel itemMasterParaModel = new ItemMasterParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                ItemId = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            // _ItemVenderListModel = (await enquiryService.GET_EnquiryDetails_List(itemMasterParaModel)).ToList();
        }





        public void EditItemDetail(CommandClickEventArgs<ItemMasterListMoel> args)
        {
            // Perform required operations here
            string buttontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Item Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                // ddEnable = false;
                itemMasterViewModel = new ItemMasterViewModel()
                {
                    itemName= args.RowData.itemName,
                    itemCode= args.RowData.itemCode,
                    displayOrder= args.RowData.displayOrder,
                    itemDescription = args.RowData.itemDescription,
                   

                  

                };


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Item Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                itemMasterViewModel = new ItemMasterViewModel()
                {

                    itemName = args.RowData.itemName,
                    itemCode = args.RowData.itemCode,
                    displayOrder = args.RowData.displayOrder,
                    itemDescription = args.RowData.itemDescription,

                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Item")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Item Details";
                OperationType = "Add";
                HeaderText = "Add Item";
                ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfItemDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfItemDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfItemDetails.ExpandAllGroupAsync();
            }
        }









        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                itemMasterAPIModel = new ItemMasterAPIModel()
                {
                    ItemName = itemMasterViewModel.itemName,
                    ItemCode= itemMasterViewModel.itemCode,
                    DisplayOrder=itemMasterViewModel.displayOrder,
                    ItemDescription = itemMasterViewModel.itemDescription,


                   
                };
                if (OperationType == "Add")
                {
                    itemMasterAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    itemMasterAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    itemMasterAPIModel.OperationType = OperationAction.Delete;
                }

                SaveItemMasterDetails(itemMasterAPIModel);

            }
            else
            {
                // Form has invalid inputs.
            }

        }
         
        private async void SaveItemMasterDetails(ItemMasterAPIModel itemMasterAPIModel)
        {
            try
            {
                if (itemMasterAPIModel.OperationType != "NA")
                {
                 //    aPIReturnModel = await inventoryService.CRUD_ItemMasterDetails(itemMasterAPIModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        ItemMasterParaModel itemMasterParaModel = new ItemMasterParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            ItemId = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                     //    _ItemMasterListMoel  = (await inventoryService.GET_ItemMasterList(itemMasterParaModel)).ToList();

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
            itemMasterViewModel = new ItemMasterViewModel();

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
