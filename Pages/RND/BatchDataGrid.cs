using AIS.Model.UserLogin;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Pages.RND
{
    public class BatchDataGrid : ComponentBase
    {
        [Inject]
        Blazored.SessionStorage.ISessionStorageService session { get; set; }
        public SessionModel _sessionModel;
        public List<Order> Orders { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _sessionModel = await session.GetItemAsync<SessionModel>("sessionUser");
            Orders = Enumerable.Range(1, 75).Select(x => new Order()
            {
                OrderID = 1000 + x,
                CustomerID = (new string[] { "ALFKI", "ANANTR", "ANTON", "BLONP", "BOLID" })[new Random().Next(5)],
                Freight = 2.1 * x,
                OrderDate = DateTime.Now.AddDays(-x),
            }).ToList();
        }

        public class Order
        {
            public int? OrderID { get; set; }
            public string CustomerID { get; set; }
            public DateTime? OrderDate { get; set; }
            public double? Freight { get; set; }
        }

        //public void BatchSaveHandler(BeforeBatchSaveArgs<Order> args)
        //{
        //    // Here, you can customize your code.
        //    var updates = args.BatchChanges.ChangedRecords;


        //}
        public void BatchSaveHandler(BeforeBatchSaveArgs<Order> Args)
        {
            var updates = Args.BatchChanges.ChangedRecords;
            var inserts = Args.BatchChanges.AddedRecords;
            var deletes = Args.BatchChanges.DeletedRecords;
        }
    }
}

