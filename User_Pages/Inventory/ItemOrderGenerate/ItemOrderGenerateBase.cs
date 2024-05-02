using AIS.Data.APIReturnModel;
using AIS.Data.RequestResponseModel.Expense;
using AIS.Data.RequestResponseModel.Inventory.ItemMaster;
using AIS.Data.RequestResponseModel.Inventory.ItemOrderGenerate;
using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AIS.Data.GeneralConversion.GeneralConversion;

namespace AdminDashboard.Server.User_Pages.Inventory.ItemOrderGenerate
{
    public class ItemOrderGenerateBase:ComponentBase
    {



        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;

        [Inject]
        public ISnackbar snackBar { get; set; }



        public ItemOrderGenerateViewModel itemOrderGenerateViewModel = new ItemOrderGenerateViewModel();
        public List<ItemOrderGenerateOutPutListModel> _ItemOrderGenerateOutPutListModel = new List<ItemOrderGenerateOutPutListModel>();
        public ItemOrderGenerateAPIModel itemOrderGenerateAPIModel { get; set; }


        public SfGrid<ItemOrderGenerateOutPutListModel> sfOrderGenerateOutPutDetails;
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





        public List<object> MenuItems = new List<object>()
            { "AutoFit", "AutoFitAll", "SortAscending", "SortDescending",
                "Copy", "ExcelExport", "CsvExport", "FirstPage", "PrevPage", "LastPage", "NextPage"
            };

        public List<string> EnquirytoolBarItems = (new List<string>() { "Add Oder", "Print", "ExportExcel", "Collapse All", "Expand All", "Search" });





        private int _itemCount = 1; // Initial item count

        // Method to add an item
        public void AddItem()
        {
            _itemCount++;
        }

        // Method to cancel an item
        public void CancelItem()
        {
            if (_itemCount > 1)
            {
                _itemCount--;
            }
        }

        // Method to get the current item count
        public int GetItemCount()
        {
            return _itemCount;
        }









        public class VednorCode
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<VednorCode> VednorCodeDetails = new List<VednorCode>
        {
            new VednorCode(){Id=1,Value="X01"},
            new VednorCode(){Id=2,Value="XL"},
        };












        public class ItemName
        {
        public string Vegetable { get; set; }
        public string Category { get; set; }
        public string ID { get; set; }
        }

        public List<ItemName> ItemNameDetails = new List<ItemName>
        {
             new ItemName(){Vegetable = "Shirt", Category = "Cloth", ID = "item1" },
            new ItemName(){ Vegetable = "Jean", Category = "Cloth", ID = "item2"},
             new ItemName(){Vegetable = "Book", Category = "Stationary", ID = "item3"},
            new ItemName(){ Vegetable = "NootBook", Category = "Stationary", ID = "item4"},
        };




        public async Task AddBillItems()
        {
            //   slNo = slNo + 1;


            _ItemOrderGenerateOutPutListModel.Add(new ItemOrderGenerateOutPutListModel()
            {
                vendorId = Convert.ToInt32(itemOrderGenerateViewModel.vendorId),
                vendorName = itemOrderGenerateViewModel.vendorName,
                // itemSlNo = slNo,
                deilevryDate = itemOrderGenerateViewModel.deilevryDate,
                itemName = itemOrderGenerateViewModel.itemName,
                itemCode = Convert.ToInt32(itemOrderGenerateViewModel.itemCode),
                itemQuantity = Convert.ToInt32(itemOrderGenerateViewModel.itemQuantity),
                //quantityType = QuantityType,
                //Quantity = Convert.ToDecimal(billItemviewModel.Quantity),
                //purchaseRate = Convert.ToDecimal(billItemviewModel.PurchaseRate),
                //totalAmount = Convert.ToDecimal(Convert.ToDecimal(billItemviewModel.Quantity) * Convert.ToDecimal(billItemviewModel.PurchaseRate))
           
            });
            //totalAmount = totalAmount + (Convert.ToDecimal(Convert.ToDecimal(billItemviewModel.Quantity) * Convert.ToDecimal(billItemviewModel.PurchaseRate)));
            //totalQTY = totalQTY + Convert.ToDecimal(billItemviewModel.Quantity);

            //discount = Convert.ToDecimal(billItemviewModel.Discount);
            //billItemviewModel.TotalAmount = totalAmount;

            //billItemviewModel.Discount = discount;
            //billItemviewModel.NetAmount = (totalAmount - discount);
            //billItemviewModel.NetQuantity = totalQTY;

            //billItemviewModel.SlipNo = string.Empty;
            //billItemviewModel.ItemName = string.Empty;
            //billItemviewModel.Quantity = 0;
            //billItemviewModel.PurchaseRate = 0;

            sfOrderGenerateOutPutDetails.Refresh();
            StateHasChanged();
            //billItemviewModel.ItemName=string.Empty;
            //billItemviewModel.Quantity = 0;
            //billItemviewModel.PurchaseRate = 0;

        }






        public void RowBound(RowDataBoundEventArgs<ItemOrderGenerateOutPutListModel> Args)
        {
            //if (Args.Data.dueAmount == 0)
            //{
            //    Args.Row.AddClass(new string[] { "e-removeEditcommand" });
            //}
            //else
            //{
            //    // Args.Row.AddClass(new string[] { "e-removeDeletecommand" });
            //}
        }



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

            ItemOrderGenerateParaModel itemOrderGenerateParaModel = new ItemOrderGenerateParaModel()
            {
                financialYear = _sessionModel.FinancialYear,
                schoolCode = _sessionModel.SchoolCode,
                OrderId = 0,
                userRoleId = _sessionModel.RoleId,
                reportType = ReportType.All
            };

            // _ItemOrderGenerateOutPutListModel  = (await enquiryService.GET_EnquiryDetails_List(itemOrderGenerateParaModel)).ToList();
        }












        public void EditOrderItemDetail(CommandClickEventArgs<ItemOrderGenerateOutPutListModel> args)
        {
            // Perform required operations here
            string buttcontext = args.CommandColumn.ButtonOption.Content;
            //int testId = args.RowData.testID;
            if (buttcontext == "Edit")
            {
                //navigationManager.NavigateTo($"/OnlineExam/ViewResult/{ testId}");
                IsVisible = true;
                DialogHeaderName = "Update Order Details";
                HeaderText = "Update Record";
                OperationType = "Update";
                btncss = "e-flat e-info e-outline";
                // ddEnable = false;
                itemOrderGenerateViewModel = new ItemOrderGenerateViewModel()
                {
                    vendorId = args.RowData.vendorId,
                    vendorName = args.RowData.vendorName,
                    deilevryDate = args.RowData.deilevryDate,
                    itemName = args.RowData.itemName,
                    itemCode = args.RowData.itemCode,
                    //itemQuantity = args.RowData.itemQuantity,
                    //itemDeliveredQty = args.RowData.itemDeliveredQty,
                    itemQuantity = args.RowData.itemQuantity,




                };


            }
            else
            {
                IsVisible = true;
                DialogHeaderName = "Delete Order Details";
                OperationType = "Delete";
                HeaderText = "Delete Record";
                btncss = "e-flat e-danger e-outline";
                ddEnable = false;
                itemOrderGenerateViewModel = new ItemOrderGenerateViewModel()
                {
                    vendorId = args.RowData.vendorId,
                    vendorName = args.RowData.vendorName,
                    deilevryDate = args.RowData.deilevryDate,
                    itemName = args.RowData.itemName,
                    itemCode = args.RowData.itemCode,
                    //itemQuantity = args.RowData.itemQuantity,
                    //itemDeliveredQty = args.RowData.itemDeliveredQty,
                    itemQuantity = args.RowData.itemQuantity,


                };
            }
            //else
            //{
            //    navigationManager.NavigateTo($"/OnlineExam/OnlineTestDetails/{testId}");
            //}

        }
        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add Oder")
            {
                //navigationManager.NavigateTo("/OnlineExam/TestList");
                //perform your actions here
                IsVisible = true;
                OperationType = "";
                btncss = "e-flat e-primary e-outline";
                DialogHeaderName = "Add Item Details";
                OperationType = "Add";
                HeaderText = "Add Order";
                ddEnable = true;

            }
            else if (args.Item.Text == "ExportExcel")
            {
                this.sfOrderGenerateOutPutDetails.ExportToExcelAsync();
            }
            else if (args.Item.Text == "Collapse All")
            {
                this.sfOrderGenerateOutPutDetails.CollapseAllGroupAsync();
            }
            else if (args.Item.Text == "Expand All")
            {
                this.sfOrderGenerateOutPutDetails.ExpandAllGroupAsync();
            }
        }



        public async void OnValidSubmit(EditContext contex)
        {
            bool isValid = contex.Validate();
            if (isValid)
            {
                itemOrderGenerateAPIModel = new ItemOrderGenerateAPIModel()
                {
                    VendorId = Convert.ToInt32(itemOrderGenerateViewModel.vendorId),
                    VendorName = itemOrderGenerateViewModel.vendorName,
                    DeilevryDate = Convert.ToDateTime(itemOrderGenerateViewModel.deilevryDate),
                    ItemName= itemOrderGenerateViewModel.itemName,
                  //  TotalQunatity = Convert.ToInt32(itemOrderGenerateViewModel.totalQunatity),
                    ItemCode = Convert.ToInt32(itemOrderGenerateViewModel.itemCode),
                    //ItemQuantity = Convert.ToInt32( itemOrderGenerateViewModel.itemQuantity),
                    // ItemDeliveredQty = Convert.ToInt32( itemOrderGenerateViewModel.itemDeliveredQty),
                    ItemQuantity = Convert.ToInt32(itemOrderGenerateViewModel.itemQuantity),



                };
                if (OperationType == "Add")
                {
                    itemOrderGenerateAPIModel.OperationType = OperationAction.Add;
                }
                else if (OperationType == "Update")
                {
                    itemOrderGenerateAPIModel.OperationType = OperationAction.Update;
                }
                //Delete Operation
                else
                {
                    itemOrderGenerateAPIModel.OperationType = OperationAction.Delete;
                }

                SaveEnquiryDetails(itemOrderGenerateAPIModel);

            }
            else
            {
                // Form has invalid inputs.
            }

        }
        private async void SaveEnquiryDetails(ItemOrderGenerateAPIModel itemOrderGenerateAPIModel)
        {
            try
            {
                if (itemOrderGenerateAPIModel.OperationType != "NA")
                {
                    //  aPIReturnModel = await enquiryService.CRUD_EnquiryDetails(itemOrderGenerateAPIModel);

                    if (aPIReturnModel.status == false)
                    {
                        snackBar.Add(aPIReturnModel.message, Severity.Success);


                        ItemOrderGenerateParaModel itemOrderGenerateParaModel = new ItemOrderGenerateParaModel()
                        {
                            financialYear = _sessionModel.FinancialYear,
                            schoolCode = _sessionModel.SchoolCode,
                            OrderId = 0,
                            userRoleId = _sessionModel.RoleId,
                            reportType = ReportType.All
                        };

                        //   _ItemMasterListMoel  = (await enquiryService.GET_EnquiryDetails_List(itemOrderGenerateParaModel)).ToList();

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
            itemOrderGenerateViewModel = new ItemOrderGenerateViewModel();

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
