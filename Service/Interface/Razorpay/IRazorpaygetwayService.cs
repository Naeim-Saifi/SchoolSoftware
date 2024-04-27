using AIS.Model;
using AIS.Model.MasterData;
using AIS.Model.PaymentGatewaySetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.Razorpay
{
    public interface IRazorpaygetwayService
    {
        //Task<IEnumerable<ClassMasterListModel>> GetRazorpaygetwayDetailList(string SchoolCode, int SchoolId, int ClassMasterId, int Status, string OperationType);
        public Task<APIReturnModel> AddUpdateRazorpaygetwayDetail(PaymentGatewayModel paymentGatewayModel);
    }
}
