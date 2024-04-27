using AIS.Model;
using AIS.Model.PaymentGatewaySetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Server.Service.Interface.PaymentDetail
{
    public interface IPaymentGatewayDetailService
    {
        Task<IEnumerable<PaymentGatewayListModel>> GetPaymentGatewayList(string SchoolCode, int SchoolId, int PaymentDetailId, int Status, string OperationType);
        Task<APIReturnModel> AddUpdatePaymentGatewayDetail(PaymentGatewayModel PaymentGatewayModel);
    }
}
